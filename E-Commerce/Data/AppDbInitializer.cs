using E_Commerce.Data.Static;
using E_Commerce.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace E_Commerce.Data
{
    public class AppDbInitializer
    {
        public static void seed(IApplicationBuilder builder)
        {
            using (var applicationservices = builder.ApplicationServices.CreateScope())
            {
                var context = applicationservices.ServiceProvider.GetService<E_CommerceDbContext>();
                context.Database.EnsureCreated();

                //category 
                if (!context.categories.Any())
                {
                    var categories = new List<category>()
                    {
                        new category()
                        {
                            Name="C1",
                            Description="c1"

                        },
                        new category()
                        {
                            Name= "c2",
                            Description="c2"
                        },
                        new category()
                        {
                            Name ="C3",
                            Description="C3"
                        }
                    };
                    context.categories.AddRange(categories);
                    context.SaveChanges();
                }
                //product 
                if (!context.products.Any())
                {
                    var products = new List<product>()
                    {
                        new product()
                        {
                            Name="p1",
                            Description="d1",
                            price=100,
                            ImgUrl ="https://m.media-amazon.com/images/I/71c0GpPsmUL._AC_SX425_.jpg",
                            ProductBrand= ProductBrand.OPTI ,
                            categoryId=1



                        },
                        new product()
                        {
                            Name= "p2",
                            Description="d2",
                            price=200,
                            ImgUrl ="https://content.optimumnutrition.com/i/on/serious-mass_Image_01?layer0=$PDP$",
                            ProductBrand= ProductBrand.NOW ,
                            categoryId=2
                        },
                        new product()
                        {
                            Name ="p3",
                            Description="d3",
                            price=300,
                            ImgUrl ="https://m.media-amazon.com/images/I/611ojH6Gx1L.jpg",
                            ProductBrand= ProductBrand.puritians ,
                            categoryId=3

                        },
                        new product()
                        {
                            Name ="p4",
                            Description="d4",
                            price=500,
                            ImgUrl ="https://cdn.shopify.com/s/files/1/0414/4060/8414/products/3.jpg?v=1662024730",
                            ProductBrand= ProductBrand.SOLARY ,
                            categoryId=3

                        }
                    };
                    context.products.AddRange(products);
                    context.SaveChanges();
                }

            }


        }
        public static async Task SeedUsersAndRolesAsync(IApplicationBuilder builder)
        {
            using (var applicationservices = builder.ApplicationServices.CreateScope())
            {
                #region Role
                var roleManager = applicationservices.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
                if (!await roleManager.RoleExistsAsync(UserRoles.Admin))
                {
                    await roleManager.CreateAsync(new IdentityRole(UserRoles.Admin));
                }
                if (!await roleManager.RoleExistsAsync(UserRoles.User))
                {
                    await roleManager.CreateAsync(new IdentityRole(UserRoles.User));
                }
                #endregion
                #region User
                var userManager = applicationservices.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();
                if (await userManager.FindByEmailAsync("admin@admin.com") == null)
                {
                    var newAdminUser = new ApplicationUser()
                    {
                        Email = "admin@admin.com",
                        EmailConfirmed = true,
                        FullName = "Admin User",
                        UserName = "Admin"
                    };
                    await userManager.CreateAsync(newAdminUser, "@Dmin123");
                    await userManager.AddToRoleAsync(newAdminUser, UserRoles.Admin);

                    if (await userManager.FindByEmailAsync("user@user.com") == null)
                    {
                        var newOridinalUser = new ApplicationUser()
                        {
                            Email = "user@user.com",
                            EmailConfirmed = true,
                            FullName = "Oridinal User",
                            UserName = "User"
                        };
                        await userManager.CreateAsync(newOridinalUser, "@User123");
                        await userManager.AddToRoleAsync(newOridinalUser, UserRoles.User);
                    }
                    #endregion
                }
            }
        }
    }
}