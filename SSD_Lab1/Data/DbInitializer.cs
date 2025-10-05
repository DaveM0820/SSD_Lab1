using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using SSD_Lab1.Models;

namespace SSD_Lab1.Data
{
    public static class DbInitializer
    {
        /// <summary>
        /// Applies migrations, creates roles Supervisor/Employee,
        /// and seeds one user for each using credentials from AppSecrets.
        /// Returns 0 on success; non-zero for basic failure diagnostics.
        /// </summary>
        public static async Task<int> SeedUsersAndRolesAsync(IServiceProvider services, AppSecrets secrets)
        {
            var context = services.GetRequiredService<ApplicationDbContext>();
            await context.Database.MigrateAsync();

            var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();
            var userManager = services.GetRequiredService<UserManager<ApplicationUser>>();

            // Validate inputs (fail fast with clear codes)
            if (string.IsNullOrWhiteSpace(secrets.AdminEmail) || string.IsNullOrWhiteSpace(secrets.AdminPwd))
                return 10;
            if (string.IsNullOrWhiteSpace(secrets.EmployeeEmail) || string.IsNullOrWhiteSpace(secrets.EmployeePwd))
                return 11;

            // Ensure roles exist
            foreach (var roleName in new[] { "Supervisor", "Employee" })
            {
                if (!await roleManager.RoleExistsAsync(roleName))
                {
                    var r = await roleManager.CreateAsync(new IdentityRole(roleName));
                    if (!r.Succeeded) return 20;
                }
            }

            // Seed Supervisor
            var supervisor = await userManager.FindByEmailAsync(secrets.AdminEmail);
            if (supervisor is null)
            {
                supervisor = new ApplicationUser
                {
                    UserName = secrets.AdminEmail,
                    Email = secrets.AdminEmail,
                    FirstName = "Super",
                    LastName = "Visor",
                    EmailConfirmed = true // important if RequireConfirmedAccount = true
                };
                var create = await userManager.CreateAsync(supervisor, secrets.AdminPwd);
                if (!create.Succeeded) return 30;

                var addRole = await userManager.AddToRoleAsync(supervisor, "Supervisor");
                if (!addRole.Succeeded) return 31;
            }

            // Seed Employee
            var employee = await userManager.FindByEmailAsync(secrets.EmployeeEmail);
            if (employee is null)
            {
                employee = new ApplicationUser
                {
                    UserName = secrets.EmployeeEmail,
                    Email = secrets.EmployeeEmail,
                    FirstName = "Emp",
                    LastName = "Loyee",
                    EmailConfirmed = true
                };
                var create = await userManager.CreateAsync(employee, secrets.EmployeePwd);
                if (!create.Succeeded) return 40;

                var addRole = await userManager.AddToRoleAsync(employee, "Employee");
                if (!addRole.Succeeded) return 41;
            }

            return 0;
        }
    }
}
