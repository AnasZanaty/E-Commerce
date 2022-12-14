using E_Commerce.Data;
using E_Commerce.Data.Services;
using E_Commerce.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace E_Commerce
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
            services.AddControllersWithViews();
            //configure context and sqlserver
            services.AddDbContext<E_CommerceDbContext>(options => {
                options.UseSqlServer(Configuration.GetConnectionString("DeffaultConnection"));
                }
            
            
            );
            //?? ? ?????? ????????? ??? ?????? ?????? ?? ????????? ?????? ???? ?????
            services.AddScoped<IcategoryService, CategoryServices>();
            services.AddScoped<IProductServices, ProductServices>();

            services.AddSingleton<IHttpContextAccessor,HttpContextAccessor>();
            services.AddScoped(x => Cart.GetShoppingCart(x));
            services.AddSession();
            services.AddIdentity<ApplicationUser, IdentityRole>().AddEntityFrameworkStores<E_CommerceDbContext>();
services.AddMemoryCache();
            services.AddAuthentication();
            services.AddAuthorization();
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
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();
            app.UseSession();
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Products}/{action=Index}/{id?}");
            });
            AppDbInitializer.seed(app);
            AppDbInitializer.seed(app);
            AppDbInitializer.SeedUsersAndRolesAsync(app).Wait();

        }
    }
}
