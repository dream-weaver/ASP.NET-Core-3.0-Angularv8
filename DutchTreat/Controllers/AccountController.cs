using DutchTreat.Entities;
using DutchTreat.ViewModel;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
 using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace DutchTreat.Controllers
{
    public class AccountController : Controller
    {
        private readonly ILogger<AccountController> logger;
        private readonly SignInManager<StoreUser> signInManager;
        private readonly UserManager<StoreUser> userManager;
        private readonly IConfiguration config;

        public AccountController(ILogger<AccountController> _logger, 
                                SignInManager<StoreUser> _signInManager, 
                                UserManager<StoreUser> _userManager,
                                IConfiguration _config)
        {
            logger = _logger;
            signInManager = _signInManager;
            userManager = _userManager;
            config = _config;
        }

        public IActionResult Login()
        {
            if (this.User.Identity.IsAuthenticated){
                return RedirectToAction("Index", "Home");
            }

            return View();   
        }
        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel loginViewModel)
        {
            if (ModelState.IsValid)
            {                
                var result = await signInManager.PasswordSignInAsync(loginViewModel.UserName, loginViewModel.Password, loginViewModel.RememberMe, false);
                if (result.Succeeded)
                {
                    if(Request.Query.Keys.Contains("ReturnUrl")){
                        return Redirect(Request.Query["ReturnUrl"].First());
                    }else{
                        return RedirectToAction("Shop", "Home");
                    }                  
                }
            }
            ModelState.AddModelError("", "Failed to Login!");
            return View();
        }
        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            await signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }
        [HttpPost]
        public async Task<IActionResult> CreateToken([FromBody] LoginViewModel loginViewModel)
        {
            if (ModelState.IsValid)
            {
                var user = await userManager.FindByNameAsync(loginViewModel.UserName);
                if (user!=null)
                {
                    var result = await signInManager.CheckPasswordSignInAsync(user, loginViewModel.Password, false);
                    if (result.Succeeded)
                    {
                        //Create the token
                        var claims = new[]
                        {
                           new Claim(System.IdentityModel.Tokens.Jwt.JwtRegisteredClaimNames.Sub, user.Email),
                           new Claim(System.IdentityModel.Tokens.Jwt.JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                           new Claim(JwtRegisteredClaimNames.UniqueName, user.UserName)
                        };
                        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["Tokens:Key"]));
                        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

                        var token = new JwtSecurityToken(
                            config["Tokens:Issuer"],
                            config["Tokens:Audience"],
                            claims,
                            expires: DateTime.Now.AddMinutes(30),
                            signingCredentials: creds
                        );
                        var results = new {
                            token = new JwtSecurityTokenHandler().WriteToken(token),
                            expiration = token.ValidTo
                        };
                        return Created("", results);
                    }
                }
                
            }
            return BadRequest(); 
        }
    }
}