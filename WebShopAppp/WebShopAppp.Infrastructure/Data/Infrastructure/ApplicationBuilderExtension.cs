using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;

using WebShopAppp.Infrastructure.Data.Domain;

namespace WebShopAppp.Infrastructure.Data.Infrastructure
{
    public static class ApplicationBuilderExtension
    {
        public static async Task<IApplicationBuilder> PrepareDatabase(this IApplicationBuilder app)
        {
            using var serviceScope = app.ApplicationServices.CreateScope();

            var services = serviceScope.ServiceProvider;
            await RoleSeeder(services);
            await SeedAdministator(services);

            var dataCategory = serviceScope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
            SeedCategories(dataCategory);

            var dataBrand = serviceScope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
            SeedBrands(dataBrand);

            return app;
        }

        private static void SeedCategories(ApplicationDbContext dataCategory)
        {
            if(dataCategory.Categories.Any())
            {
                return;
            }
            dataCategory.Categories.AddRange(new[]
            {
             new Category {CategoryName = "Laptop"},
             new Category {CategoryName = "Computer"},
             new Category {CategoryName = "Monitor"},
             new Category {CategoryName = "Accessory"},
             new Category {CategoryName = "TV"},
             new Category {CategoryName = "Mobile Phone"},
             new Category {CategoryName = "Smart watch"},
            });
            dataCategory.SaveChanges();

        }

        private static void SeedBrands(ApplicationDbContext dataBrand)
        {
            if (dataBrand.Brands.Any())
            {
                return;
            }
            dataBrand.Brands.AddRange(new[]
            {
             new Brand {BrandName = "Acer"},
             new Brand {BrandName = "Asus"},
             new Brand {BrandName = "Apple"},
             new Brand {BrandName = "Dell"},
             new Brand {BrandName = "HP"},
             new Brand {BrandName = "Huawei"},
              new Brand {BrandName = "Lenovo"},
             new Brand {BrandName = "Samsung"},
            });
            dataBrand.SaveChanges();

        }

        private static async Task RoleSeeder(IServiceProvider serviceProvider)
        {
            var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();

            string[] roleNames = { "Administator", "Client" };

            IdentityResult roleResult;

            foreach (var role in roleNames)
            {
                var roleExist = await roleManager.RoleExistsAsync(role);

                {
                    if (!roleExist)
                    {
                        roleResult = await roleManager.CreateAsync(new IdentityRole(role));
                    }
                }
            }
        }

        private static async Task SeedAdministator(IServiceProvider serviceProvider)
        {
            var userManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();

            if (await userManager.FindByNameAsync("admin") == null)
            {
                ApplicationUser user = new ApplicationUser();
                user.FirstName = "admin";
                user.UserName = "admin";
                user.LastName = "admin";
                user.Email = "admin@admin.com";
                user.Adress = "admin address";
                user.PhoneNumber = "0000000000";

                var result = await userManager.CreateAsync(user, "Admin123456");

                if (result.Succeeded)
                {
                    userManager.AddToRoleAsync(user, "Administrator").Wait();
                }
            }
        }
    }
}
