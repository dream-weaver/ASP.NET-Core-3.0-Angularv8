using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using DutchTreat.Models;
using DutchTreat.Service;
using DutchTreat.ViewModel;
using Microsoft.AspNetCore.Authorization;

namespace DutchTreat.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IMailService _mailService;
        private readonly IProductService _productService;
        public HomeController(ILogger<HomeController> logger, IMailService mailService, IProductService productService)
        {
            _logger = logger;
            _mailService = mailService;
            _productService = productService;
        }      
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }
        [HttpGet("contact")]
        public IActionResult Contact()
        {
            return View();
        }
        [HttpPost("contact")]
        public IActionResult Contact(ContactViewModel model)
        {
            if(ModelState.IsValid ){
                _mailService.SendMail("eyetanvir@gmail.com", model.Subject, $"From: {model.Name} - {model.Email}, Message: {model.Message}");
                ViewBag.userMessage = "Mail sent successfully";
                ModelState.Clear();
            }
            return View();
        }
      
        public IActionResult Shop()
        {
            var products = _productService.GetProducts();
            return View(products);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
