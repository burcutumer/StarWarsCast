using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Data.Entities;
using Microsoft.AspNetCore.Identity;

namespace API.Data
{
    public class DbInitializer
    {
        public static async Task Initialize(StoreContext context, RoleManager<AppRole> roleManager, UserManager<AppUser> userManager)
        {
            if (context.People.Any()) return;

            await roleManager.CreateAsync(new AppRole
            {
                Name = "user"
            });
            await roleManager.CreateAsync(new AppRole
            {
                Name = "admin"
            });
            var adminUser = new AppUser
            {
                Email = "admin@mail.com",
                NickName = "admin",
                UserName = "admin@mail.com"
            };
            await userManager.CreateAsync(adminUser, "Pa$$w0rd!");
            await userManager.AddToRoleAsync(adminUser, "admin");

            var people = new List<Person>
            {
                new Person
                {
                    Id = 1,
                    Name = "Anakin Skywalker",
                    Height = 188,
                    Mass = 84,
                    SkinColor= "fair",
                    EyeColor = "blue",
                    CreatedAt = DateTime.UtcNow
                },
                new Person
                {
                    Id = 2,
                    Name = "Darth Vader",
                    Height = 202,
                    Mass = 136,
                    SkinColor= "fair",
                    EyeColor = "yellow",
                    CreatedAt = DateTime.UtcNow
                },
                new Person
                {
                    Id = 3,
                    Name = "R2-D2",
                    Height = 96,
                    Mass = 32,
                    SkinColor= "white",
                    EyeColor = "red",
                    CreatedAt = DateTime.UtcNow
                },
                new Person
                {
                    Id = 4,
                    Name = "Obi-Wan Kenobi",
                    Height = 182,
                    Mass = 77,
                    SkinColor= "fair",
                    EyeColor = "blue",
                    CreatedAt = DateTime.UtcNow
                }
            };
            foreach (var person in people)
            {
                await context.People.AddAsync(person);
            }


            await context.SaveChangesAsync();
        }
    }
}