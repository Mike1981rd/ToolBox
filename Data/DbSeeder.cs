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
            var users = new User[]
            {
                new User
                {
                    Name = "John Smith",
                    Email = "admin@example.com",
                    Role = "Admin",
                    IsActive = true,
                    CreatedAt = DateTime.UtcNow.AddMonths(-6)
                },
                new User
                {
                    Name = "Jane Doe",
                    Email = "jane.doe@example.com",
                    Role = "Manager",
                    IsActive = true,
                    CreatedAt = DateTime.UtcNow.AddMonths(-4)
                },
                new User
                {
                    Name = "Robert Johnson",
                    Email = "robert.johnson@example.com",
                    Role = "User",
                    IsActive = true,
                    CreatedAt = DateTime.UtcNow.AddMonths(-2)
                },
                new User
                {
                    Name = "Maria Garcia",
                    Email = "maria.garcia@example.com",
                    Role = "User",
                    IsActive = false,
                    CreatedAt = DateTime.UtcNow.AddMonths(-1)
                },
                new User
                {
                    Name = "David Lee",
                    Email = "david.lee@example.com",
                    Role = "Developer",
                    IsActive = true,
                    CreatedAt = DateTime.UtcNow.AddDays(-15)
                },
                new User
                {
                    Name = "Sarah Wilson",
                    Email = "sarah.wilson@example.com",
                    Role = "Tester",
                    IsActive = true,
                    CreatedAt = DateTime.UtcNow.AddDays(-7)
                }
            };

            context.Users.AddRange(users);
            context.SaveChanges();
        }
    }
}