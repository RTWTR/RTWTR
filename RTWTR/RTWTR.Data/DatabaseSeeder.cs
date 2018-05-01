using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using RTWTR.Data.Models;
using RTWTR.Infrastructure;

namespace RTWTR.Data
{
    public class DatabaseSeeder
    {
        private readonly RTWTRDbContext dbContext;
        private readonly UserManager<User> userManager;
        private readonly RoleManager<IdentityRole> roleManager;

        public DatabaseSeeder(
            RTWTRDbContext dbContext, 
            UserManager<User> userManager, 
            RoleManager<IdentityRole> roleManager
        )
        {
            this.dbContext = dbContext ??
                throw new ArgumentNullException(nameof(dbContext));
            this.userManager = userManager ??
                throw new ArgumentNullException(nameof(userManager));
            this.roleManager = roleManager ?? 
                throw new ArgumentNullException(nameof(roleManager));
        }

        public async Task Initialize()
        {
            this.EnsureDatabaseCreated();

            await this.CreateUsers();
            await this.CreateRoles();
        }

        private void EnsureDatabaseCreated()
        {
            /* Create the database if it does not exist
               and apply any pending migrations afterwards */
            this.dbContext.Database.Migrate();
        }

        private async Task CreateUsers()
        {
            // Create the Admin
            await this.CreateUser("admin@rtwtr.com", "rtwtradmin");

            // Create the TestUser
            await this.CreateUser("user@rtwtr.com", "password");
        }

        private async Task CreateRoles()
        {
            // Create the Admin Role
            await this.CreateRole("Administrator", "admin@rtwtr.com");

            // Create the User Role
            await this.CreateRole("User", "user@rtwtr.com");
        }

        private async Task CreateUser(string email, string password)
        {
            var user = await this.userManager.FindByEmailAsync(email);
            if (user.IsNull())
            {
                user = new User { UserName = email, Email = email };
                await this.userManager.CreateAsync(user, password);
            }
        }

        private async Task CreateRole(string role, string userEmail)
        {
            IdentityResult userRole = null;
            if (!await this.roleManager.RoleExistsAsync(role))
            {
                userRole = await this.roleManager.CreateAsync(
                    new IdentityRole(role)
                );
            }

            var user = await this.userManager.FindByEmailAsync(userEmail);

            await this.userManager.AddToRoleAsync(user, role);
        }
    }
}