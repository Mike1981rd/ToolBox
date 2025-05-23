using Microsoft.EntityFrameworkCore;
using ToolBox.Models;

namespace ToolBox.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        // Add DbSet properties for your entities here
        public DbSet<User> Users { get; set; }
        public DbSet<Permission> Permissions { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<RolePermission> RolePermissions { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            
            // Configure entity relationships, indexes, etc. here

            // Permission configuration
            modelBuilder.Entity<Permission>()
                .HasIndex(p => new { p.ModuleName, p.ActionName })
                .IsUnique();

            // Role configuration
            modelBuilder.Entity<Role>()
                .HasIndex(r => r.Name)
                .IsUnique();

            // RolePermission configuration (Many-to-Many relationship)
            modelBuilder.Entity<RolePermission>()
                .HasKey(rp => new { rp.RoleId, rp.PermissionId });

            modelBuilder.Entity<RolePermission>()
                .HasOne(rp => rp.Role)
                .WithMany(r => r.RolePermissions)
                .HasForeignKey(rp => rp.RoleId);

            modelBuilder.Entity<RolePermission>()
                .HasOne(rp => rp.Permission)
                .WithMany(p => p.RolePermissions)
                .HasForeignKey(rp => rp.PermissionId);

            // User configuration (relationship with Role)
            modelBuilder.Entity<User>()
                .HasOne(u => u.Role)
                .WithMany(r => r.Users)
                .HasForeignKey(u => u.RoleId)
                .IsRequired(false) // Makes RoleId optional (nullable)
                .OnDelete(DeleteBehavior.SetNull); // If a Role is deleted, RoleId in User is set to null

            // User unique indexes - COMENTADO TEMPORALMENTE
            // modelBuilder.Entity<User>()
            //     .HasIndex(u => u.UserName)
            //     .IsUnique();

            // modelBuilder.Entity<User>()
            //     .HasIndex(u => u.Email)
            //     .IsUnique();

            // Seed Permissions data
            SeedPermissions(modelBuilder);
        }

        private void SeedPermissions(ModelBuilder modelBuilder)
        {
            var permissions = new List<Permission>();
            int permissionId = 1;

            // Define modules and actions
            var modules = new[]
            {
                "Dashboard",
                "Topics",
                "VideoManagement",
                "Customers",
                "Users",
                "Roles",
                "Instructors",
                "ToolboxAcademy",
                "WheelOfLife",
                "WheelOfProgress",
                "XRayLife",
                "LifeAssessment",
                "LifeAreas",
                "Tasks",
                "HabitTracker",
                "EmailContents",
                "WebsiteSettings",
                "WelcomeMessage"
            };

            var actions = new[]
            {
                new { Name = "Read", Description = "Ver y listar" },
                new { Name = "Write", Description = "Editar y actualizar" },
                new { Name = "Create", Description = "Crear nuevos registros" }
            };

            // Generate permissions for each module and action
            foreach (var module in modules)
            {
                var category = GetCategoryForModule(module);
                
                foreach (var action in actions)
                {
                    permissions.Add(new Permission
                    {
                        Id = permissionId++,
                        ModuleName = module,
                        ActionName = action.Name,
                        Description = $"{action.Description} en {GetModuleDisplayName(module)}",
                        Category = category
                    });
                }
            }

            modelBuilder.Entity<Permission>().HasData(permissions);
        }

        private string GetCategoryForModule(string module)
        {
            return module switch
            {
                "Dashboard" => "General",
                "Topics" or "VideoManagement" or "ToolboxAcademy" => "Gestión de Contenido",
                "Customers" or "Users" or "Roles" or "Instructors" => "Gestión de Usuarios",
                "WheelOfLife" or "WheelOfProgress" or "XRayLife" or "LifeAssessment" or "LifeAreas" => "Herramientas de Vida",
                "Tasks" or "HabitTracker" => "Productividad",
                "EmailContents" or "WebsiteSettings" or "WelcomeMessage" => "Configuración",
                _ => "Otros"
            };
        }

        private string GetModuleDisplayName(string module)
        {
            return module switch
            {
                "Dashboard" => "Tablero",
                "Topics" => "Temas",
                "VideoManagement" => "Gestión de Videos",
                "Customers" => "Clientes",
                "Users" => "Usuarios",
                "Roles" => "Roles",
                "Instructors" => "Instructores",
                "ToolboxAcademy" => "Toolbox Academy",
                "WheelOfLife" => "Rueda de la Vida",
                "WheelOfProgress" => "Rueda del Progreso",
                "XRayLife" => "Rayos X de la Vida",
                "LifeAssessment" => "Evaluación de Vida",
                "LifeAreas" => "Áreas de Vida",
                "Tasks" => "Tareas",
                "HabitTracker" => "Seguimiento de Hábitos",
                "EmailContents" => "Contenido de Emails",
                "WebsiteSettings" => "Configuración del Sitio",
                "WelcomeMessage" => "Mensaje de Bienvenida",
                _ => module
            };
        }
    }
}