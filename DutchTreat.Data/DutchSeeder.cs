using DutchTreat.Entities;
using DutchTreat.Entity;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Internal;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DutchTreat.Data
{
    public class DutchSeeder
    {
        private readonly DutchTreatDBContext _ctx;
        private readonly IHostingEnvironment _hosting;
        private readonly UserManager<StoreUser> _userManager;

        public DutchSeeder(DutchTreatDBContext ctx, IHostingEnvironment hosting, UserManager<StoreUser> userManager)
        {
            _ctx = ctx;
            _hosting = hosting;
            _userManager = userManager;
        }
        public async Task SeedAsync()
        {
            _ctx.Database.EnsureCreated();

            StoreUser user = await _userManager.FindByEmailAsync("eyetanvir@gmail.com");
            if (user==null)
            {
               
                user = new StoreUser() 
                {
                    FirstName = "Tanvir",
                    LastName = "Ahmed",
                    Email = "eyetanvir@gmail.com",
                    UserName = "eyetanvir@gmail.com"
                };
                var result = await _userManager.CreateAsync(user, "P@ssw0rd1");
                if (result != IdentityResult.Success)
                {
                    throw new InvalidOperationException("Could not create new user in the seeder");
                }
            }

            if (!_ctx.Products.Any())
            {
                //create sample data
                var filepath = Path.Combine(_hosting.ContentRootPath, "data/art.json");
                var json = File.ReadAllText(filepath);
                var products = JsonConvert.DeserializeObject<IEnumerable<Product>>(json);
                _ctx.Products.AddRange(products);

                var order = _ctx.Orders.Where(o => o.Id == 1).FirstOrDefault();
                if(order!=null)
                {
                   order.user = user;
                   order.Items = new List<OrderItem>()
                    {
                        new OrderItem()
                        {
                            Product = products.First(),
                            Quantity = 5,
                            UnitPrice = products.First().Price
                        }
                    };
                }

                _ctx.SaveChanges(); 
            }
        }
    }
}
