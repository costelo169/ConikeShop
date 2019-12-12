using System;
using System.Linq;
using System.Threading.Tasks;
using ContactManager.Models;
using ContactManager.Authorization;
using ContactManager.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using ApplicationCore.Entities;
using Infrastructure.Persistence;

namespace contactmanager.Data
{
    public class SeedData
    {
        public static async Task Initialize(IServiceProvider serviceProvider, string testUserPw)
        {
            var context = serviceProvider.GetRequiredService<ApplicationDbContext>();

            context.Database.EnsureCreated();

            var adminID = await EnsureUser(serviceProvider, testUserPw, "admin@contoso.com");
            await EnsureRole(serviceProvider, adminID, Constants.ContactAdministratorsRole);

            // allowed user can create and edit contacts that they create
            var managerID = await EnsureUser(serviceProvider, testUserPw, "manager@contoso.com");
            await EnsureRole(serviceProvider, managerID, Constants.ContactManagersRole);

            SeedDB(context, adminID);
        }
        private static async Task<string> EnsureUser(IServiceProvider serviceProvider, string testUserPw, string UserName)
        {
            var userManager = serviceProvider.GetService<UserManager<IdentityUser>>();

            var user = await userManager.FindByNameAsync(UserName);
            if (user == null)
            {
                user = new IdentityUser
                {
                    UserName = UserName,
                    EmailConfirmed = true
                };
                await userManager.CreateAsync(user, testUserPw);
            }

            if (user == null)
            {
                throw new Exception("The password is probably not strong enough!");
            }

            return user.Id;
        }

        private static async Task<IdentityResult> EnsureRole(IServiceProvider serviceProvider, string uid, string role)
        {
            IdentityResult IR = null;
            var roleManager = serviceProvider.GetService<RoleManager<IdentityRole>>();

            if (roleManager == null)
            {
                throw new Exception("roleManager null");
            }

            if (!await roleManager.RoleExistsAsync(role))
            {
                IR = await roleManager.CreateAsync(new IdentityRole(role));
            }

            var userManager = serviceProvider.GetService<UserManager<IdentityUser>>();

            var user = await userManager.FindByIdAsync(uid);

            if (user == null)
            {
                throw new Exception("The testUserPw password was probably not strong enough!");
            }

            IR = await userManager.AddToRoleAsync(user, role);

            return IR;
        }
        private static void SeedDB(ApplicationDbContext context, string adminID)
        {
            if (context.Contacts.Any()) return;

            context.Contacts.AddRange(
                new Contact
                {
                    Name = "Debra Garcia",
                    Address = "1234 Main St",
                    City = "Redmond",
                    State = "WA",
                    Zip = "10999",
                    Email = "debra@example.com",
                    Status = ContactStatus.Approved,
                    OwnerID = adminID
                },
                new Contact
                {
                    Name = "Thorsten Weinrich",
                    Address = "5678 1st Ave W",
                    City = "Redmond",
                    State = "WA",
                    Zip = "10999",
                    Email = "thorsten@example.com",
                    Status = ContactStatus.Submitted,
                    OwnerID = adminID
                },
                new Contact
                {
                    Name = "Yuhong Li",
                    Address = "9012 State st",
                    City = "Redmond",
                    State = "WA",
                    Zip = "10999",
                    Email = "yuhong@example.com",
                    Status = ContactStatus.Rejected,
                    OwnerID = adminID
                },
                new Contact
                {
                    Name = "Jon Orton",
                    Address = "3456 Maple St",
                    City = "Redmond",
                    State = "WA",
                    Zip = "10999",
                    Email = "jon@example.com",
                    Status = ContactStatus.Submitted,
                    OwnerID = adminID
                },
                new Contact
                {
                    Name = "Diliana Alexieva-Bosseva",
                    Address = "7890 2nd Ave E",
                    City = "Redmond",
                    State = "WA",
                    Zip = "10999",
                    Email = "diliana@example.com",
                    OwnerID = adminID
                }
                );
            context.SaveChanges();
        }
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new ConikeShopContext(
                serviceProvider.GetRequiredService<
                    DbContextOptions<ConikeShopContext>>()))
            {
                // Look for any movies.
                if (context.Products.Any())
                {
                    return;   // DB has been seeded
                }

                context.Products.AddRange(
                    new Product
                    {
                        Title = "Baseball Bat",
                        Genre = "Baseball",
                        Price = 21.99M
                    },
                    new Product
                    {
                        Title = "Basketball ball",
                        Genre = "Basketball",
                        Price = 3.49M
                    },new Product
                    {
                        Title = "Football ball",
                        Genre = "Football",
                        Price = 7.99M
                    },new Product
                    {
                        Title = "Volleyball Ball",
                        Genre = "Volleyball",
                        Price = 4.99M
                    },new Product
                    {
                        Title = "Golf Club",
                        Genre = "Golf",
                        Price = 30.99M
                    }
                    
                );
                context.SaveChanges();
            }
        }
    }
}