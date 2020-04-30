using DutchTreat.Service;
using DutchTreat.Service.Implementation;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore;
using DutchTreat.Data;
using AutoMapper;
using System.Reflection;
using Microsoft.AspNetCore.Identity;
using DutchTreat.Entities;

namespace DutchTreat
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        public IConfiguration Configuration { get; }
        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddIdentity<StoreUser, IdentityRole>(cfg => 
            {
                cfg.User.RequireUniqueEmail = true;

            }).AddEntityFrameworkStores<DutchTreatDBContext>();

            services.AddDbContextPool<DutchTreatDBContext>(options =>
            {
                options.UseSqlServer(Configuration.GetConnectionString("DutchTreatDB"));
            });

            services.AddControllersWithViews().AddNewtonsoftJson(options =>
               options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
            );
            services.AddTransient<DutchSeeder>();
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            services.AddTransient<IMailService, MailService>();
            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<IOrderService, OrderService>();
            services.AddAuthentication()
                .AddCookie()
                .AddJwtBearer(cfg=>
                    {
                        cfg.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters()
                        {
                            ValidIssuer = Configuration["Tokens:Issuer"],
                            ValidAudience = Configuration["Tokens:Audience"],
                            IssuerSigningKey = new Microsoft.IdentityModel.Tokens.SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(Configuration["Tokens:Key"]))
                        };
                    });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }
            app.UseStaticFiles();
            app.UseAuthentication();
            app.UseRouting();

            app.UseAuthorization();
      

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
