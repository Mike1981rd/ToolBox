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
                    Id = 1, // Forzar ID=1 para compatibilidad con GetCurrentUserId()
                    FullName = "John Smith",
                    UserName = "jsmith",
                    Email = "admin@example.com",
                    PasswordHash = BCrypt.Net.BCrypt.HashPassword("password123"),
                    RoleId = adminRole.Id,
                    IsActive = true,
                    CreatedAt = DateTime.UtcNow.AddMonths(-6)
                },
                new User
                {
                    FullName = "Jane Doe",
                    UserName = "jdoe",
                    Email = "jane.doe@example.com",
                    PasswordHash = BCrypt.Net.BCrypt.HashPassword("password123"),
                    RoleId = managerRole.Id,
                    IsActive = true,
                    CreatedAt = DateTime.UtcNow.AddMonths(-4)
                },
                new User
                {
                    FullName = "Robert Johnson",
                    UserName = "rjohnson",
                    Email = "robert.johnson@example.com",
                    PasswordHash = BCrypt.Net.BCrypt.HashPassword("password123"),
                    RoleId = userRole.Id,
                    IsActive = true,
                    CreatedAt = DateTime.UtcNow.AddMonths(-2)
                },
                new User
                {
                    FullName = "Maria Garcia",
                    UserName = "mgarcia",
                    Email = "maria.garcia@example.com",
                    PasswordHash = BCrypt.Net.BCrypt.HashPassword("password123"),
                    RoleId = userRole.Id,
                    IsActive = false,
                    CreatedAt = DateTime.UtcNow.AddMonths(-1)
                },
                new User
                {
                    FullName = "David Lee",
                    UserName = "dlee",
                    Email = "david.lee@example.com",
                    PasswordHash = BCrypt.Net.BCrypt.HashPassword("password123"),
                    RoleId = developerRole.Id,
                    IsActive = true,
                    CreatedAt = DateTime.UtcNow.AddDays(-15)
                },
                new User
                {
                    FullName = "Sarah Wilson",
                    UserName = "swilson",
                    Email = "sarah.wilson@example.com",
                    PasswordHash = BCrypt.Net.BCrypt.HashPassword("password123"),
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