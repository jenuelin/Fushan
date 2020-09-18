using DataServices.Db;
using DataServices.Model;
using Microsoft.AspNetCore.Identity;
using System;

namespace DataServices
{
    public class FushanDbInitializer
    {
        public static void SeedRoles(FushanContext fushanContext)
        {
            fushanContext.Roles.AddRange(new[]
            {
                new Role
                {
                    Id = Guid.NewGuid(),
                    Name = "Admin",
                    NormalizedName = "Admin",
                    CreatedByUsername = "Admin",
                    CreatedOn = DateTimeOffset.Now,
                    UpdatedByUsername = "Admin",
                    UpdatedOn = DateTimeOffset.Now,
                },
                new Role
                {
                    Id = Guid.NewGuid(),
                    Name = "User",
                    NormalizedName = "User",
                    CreatedByUsername = "Admin",
                    CreatedOn = DateTimeOffset.Now,
                    UpdatedByUsername = "Admin",
                    UpdatedOn = DateTimeOffset.Now,
                }
            });
        }

        public static void SeedUsers(UserManager<AppUser> userManager, FushanContext fushanContext)
        {
            if (userManager.FindByEmailAsync("admin@gmail.com").Result == null)
            {
                var user = new AppUser
                {
                    UserName = "admin@gmail.com",
                    Email = "admin@gmail.com",
                    CreatedByUsername = "Admin",
                    CreatedOn = DateTimeOffset.Now,
                    UpdatedByUsername = "Admin",
                    UpdatedOn = DateTimeOffset.Now,
                };

                IdentityResult result = userManager.CreateAsync(user, "Tkadmin1!").Result;

                if (result.Succeeded)
                {
                    userManager.AddToRoleAsync(user, "Admin").Wait();
                }
            }
        }
    }
}