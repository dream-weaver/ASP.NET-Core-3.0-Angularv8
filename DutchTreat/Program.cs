using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DutchTreat.Data;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System.Threading;

namespace DutchTreat
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var host = CreateHostBuilder(args).Build();
            RunSeeding(host);
            host.Run();
        }

        public static void RunSeeding(IHost host)
        {
            try
            {
                var scopeFactory = host.Services.GetService<IServiceScopeFactory>();
                using(var scope = scopeFactory.CreateScope())
                {
                    var seeder =  scope.ServiceProvider.GetService<DutchSeeder>();
                    seeder.SeedAsync().Wait();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }          
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}