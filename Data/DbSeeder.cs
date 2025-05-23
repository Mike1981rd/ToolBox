using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using ToolBox.Models;

namespace ToolBox.Data
{
    public static class DbSeeder
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new ApplicationDbContext(
                serviceProvider.GetRequiredService<DbContextOptions<ApplicationDbContext>>()))
            {
                // Look for any existing users
                if (context.Users.Any())
                {
                    return; // Database has been seeded
                }

                SeedUsers(context);
            }
        }

        private static void SeedUsers(ApplicationDbContext context)
        {
            // First, create roles
            var adminRole = new Role { Name = "Admin", Description = "Administrator role", AssignedDashboard = "Admin" };
            var managerRole = new Role { Name = "Manager", Description = "Manager role", AssignedDashboard = "Admin" };
            var userRole = new Role { Name = "User", Description = "Regular user role", AssignedDashboard = "Client" };
            var developerRole = new Role { Name = "Developer", Description = "Developer role", AssignedDashboard = "Admin" };
            var testerRole = new Role { Name = "Tester", Description = "Tester role", AssignedDashboard = "Admin" };

            context.Roles.AddRange(adminRole, managerRole, userRole, developerRole, testerRole);
            context.SaveChanges();

            var users = new User[]
            {
                new User
                {
                    Name = "John Smith",
                    Email = "admin@example.com",
                    RoleId = adminRole.Id,
                    IsActive = true,
                    CreatedAt = DateTime.UtcNow.AddMonths(-6)
                },
                new User
                {
                    Name = "Jane Doe",
                    Email = "jane.doe@example.com",
                    RoleId = managerRole.Id,
                    IsActive = true,
                    CreatedAt = DateTime.UtcNow.AddMonths(-4)
                },
                new User
                {
                    Name = "Robert Johnson",
                    Email = "robert.johnson@example.com",
                    RoleId = userRole.Id,
                    IsActive = true,
                    CreatedAt = DateTime.UtcNow.AddMonths(-2)
                },
                new User
                {
                    Name = "Maria Garcia",
                    Email = "maria.garcia@example.com",
                    RoleId = userRole.Id,
                    IsActive = false,
                    CreatedAt = DateTime.UtcNow.AddMonths(-1)
                },
                new User
                {
                    Name = "David Lee",
                    Email = "david.lee@example.com",
                    RoleId = developerRole.Id,
                    IsActive = true,
                    CreatedAt = DateTime.UtcNow.AddDays(-15)
                },
                new User
                {
                    Name = "Sarah Wilson",
                    Email = "sarah.wilson@example.com",
                    RoleId = testerRole.Id,
                    IsActive = true,
                    CreatedAt = DateTime.UtcNow.AddDays(-7)
                }
            };

            context.Users.AddRange(users);
            context.SaveChanges();
        }
    }
}