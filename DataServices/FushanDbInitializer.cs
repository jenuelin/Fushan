using DataServices.Db;
using DataServices.Model;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;

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

        private static Random random = new Random();

        public static void SeeDepartments(FushanContext fushanContext)
        {
            var rows = fushanContext.Departments.Count();
            if (rows > 0)
                return;
            for (int i = 0; i < 10; i++)
            {
                var departments = new List<Department>();
                var count = 0;
                while (count < 100000)
                {
                    const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
                    var ranString = new string(Enumerable.Repeat(chars, 6)
                      .Select(s => s[random.Next(s.Length)]).ToArray());

                    var department = new Department
                    {
                        Id = Guid.NewGuid(),
                        Name = ranString,
                        DepartmentId = ranString,
                        CreatedByUsername = "Admin",
                        CreatedOn = DateTimeOffset.Now,
                        UpdatedByUsername = "Admin",
                        UpdatedOn = DateTimeOffset.Now,
                    };
                    departments.Add(department);
                    count++;
                }
                fushanContext.Departments.AddRange(departments);
                fushanContext.SaveChanges();
                i++;
            }
        }
    }
}