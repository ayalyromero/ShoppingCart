using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using ShoppingCart.Infraestructure;
using ShoppingCart.Models;
using ShoppingCart.Persistence;
using ShoppingCart.Service.Implementations;
using ShoppingCart.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShoppingCart
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
            services.AddDbContext<ApiContext>(opt => opt.UseInMemoryDatabase("ShoppingCart"));

            services.AddScoped<IBasketService, BasketService>();
            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<IUserService, UserService>();

            //Add service for accessing current HttpContext
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            services.AddMvc();

            services.AddDistributedMemoryCache();

            services.AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromSeconds(10);
                options.Cookie.HttpOnly = true;
                options.Cookie.IsEssential = true;
            });
            //services.AddControllersWithViews();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IServiceProvider serviceProvider)
        {

            //var context = app.ApplicationServices.GetService<ApiContext>();
            var context = serviceProvider.GetService<ApiContext>();
            AppHttpContext.Services = app.ApplicationServices;

            AddTestData(context);

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();
            app.UseSession();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=User}/{action=Login}/{id?}");
            });
        }

        private static void AddTestData(ApiContext context)
        {

            // Users data
            context.Users.AddRange(new List<User>()
            {
                new User{
                    Id = 1,
                    FullName = "User 1",
                    Username = "user1",
                    Password = "abc123"
                },
                new User{
                    Id = 2,
                    FullName = "User 2",
                    Username = "user2",
                    Password = "abc123"
                },
            });

            // Categories data
            context.Category.AddRange(new List<Category>()
            {
                new Category
            {
                Id = 1,
                Name = "Books",
            },
                new Category
            {
                Id = 2,
                Name = "DVDs",
            },
            });

            // Products data
            context.Product.AddRange(new List<Product>() {
            new Product
            {
                Id = 10001,
                Name = "Lord of the Rings",
                Price = 10,
                CategoryId = 1
            },
            new Product
            {
                Id = 10002,
                Name = "The Hobbit",
                Price = 5,
                CategoryId = 1
            },
            new Product
            {
                Id = 20001,
                Name = "Game of Thrones",
                Price = 9,
                CategoryId = 2
            },
            new Product
            {
                Id = 20110,
                Name = "Breaking Bad",
                Price = 7,
                CategoryId = 2
            }
            });

            context.SaveChanges();
        }
    }
}
