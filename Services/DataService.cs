using BlogProject.Data;
using BlogProject.Enums;
using BlogProject.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlogProject.Services
{
    public class DataService
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<BlogUser> _userManager;

        public DataService(ApplicationDbContext dbContext,
                            RoleManager<IdentityRole> roleManager,
                            UserManager<BlogUser> userManager)
        {
            _dbContext = dbContext;
            _roleManager = roleManager;
            _userManager = userManager;
        }

        public async Task ManageDataAsync()
        {
            // Create the DB from the Migrations
            await _dbContext.Database.MigrateAsync();

            // Seed a few Roles into the system
            await SeedRolesAsync();

            // Seed a few users into the system
            await SeedUsersAsync();
        }

        private async Task SeedRolesAsync()
        {
            // If at least one Role exists, do nothing
            if (_dbContext.Roles.Any())
            {
                return;
            }

            // Otherwise create the enumerated Roles
            foreach (var role in Enum.GetNames(typeof(BlogRole)))
            {
                // Use the Role Manager to create roles
                await _roleManager.CreateAsync(new IdentityRole(role));
            }
        }

        private async Task SeedUsersAsync()
        {
            // If at least one User exists, do nothing
            if (_dbContext.Users.Any())
            {
                return;
            }

            // ADD A BLOG USER
            // Create a new instance of BlogUser
            var adminUser = new BlogUser()
            {
                Email = "bobbybecket@gmail.com",
                UserName = "bobbybecket@gmail.com",
                FirstName = "Robert",
                LastName = "Becket",
                DisplayName = "Robert Becket",
                PhoneNumber = "(800) 555-1212",
                EmailConfirmed = true
            };

            // Use UserManager to add this user
            await _userManager.CreateAsync(adminUser, "Abc&123!");

            // Add this user to the Administrator Role
            await _userManager.AddToRoleAsync(adminUser, BlogRole.Administrator.ToString());

            // ADD A MODERATOR
            // Create a new instance of BlogUser
            var modUser = new BlogUser()
            {
                Email = "JasonTwichell@CoderFoundry.com",
                UserName = "JasonTwichell@CoderFoundry.com",
                FirstName = "Jason",
                LastName = "Twichell",
                DisplayName = "Jason Twichell",
                PhoneNumber = "(800) 555-1213",
                EmailConfirmed = true
            };

            // Use UserManager to add this user
            await _userManager.CreateAsync(modUser, "Abc&123!");

            // Add this user to the Moderator Role
            await _userManager.AddToRoleAsync(modUser, BlogRole.Moderator.ToString());
        }
    }
}
