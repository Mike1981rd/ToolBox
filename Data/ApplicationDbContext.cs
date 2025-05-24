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
        public DbSet<LifeArea> LifeAreas { get; set; }
        public DbSet<Question> Questions { get; set; }
        public DbSet<UserAnswer> UserAnswers { get; set; }
        public DbSet<WheelOfLifeScore> WheelOfLifeScores { get; set; }

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

            // LifeArea configuration
            modelBuilder.Entity<LifeArea>()
                .HasIndex(la => la.DisplayOrder);

            modelBuilder.Entity<LifeArea>()
                .HasOne(la => la.CreatedByUser)
                .WithMany()
                .HasForeignKey(la => la.CreatedByUserId)
                .IsRequired(false)
                .OnDelete(DeleteBehavior.SetNull);

            // Seed Permissions data
            SeedPermissions(modelBuilder);
            
            // Seed LifeAreas data
            SeedLifeAreas(modelBuilder);
            
            // Question configuration
            modelBuilder.Entity<Question>()
                .HasOne(q => q.LifeArea)
                .WithMany()
                .HasForeignKey(q => q.LifeAreaId)
                .OnDelete(DeleteBehavior.Cascade);
                
            // UserAnswer configuration
            modelBuilder.Entity<UserAnswer>()
                .HasKey(ua => ua.Id);
                
            modelBuilder.Entity<UserAnswer>()
                .HasOne(ua => ua.User)
                .WithMany()
                .HasForeignKey(ua => ua.UserId)
                .OnDelete(DeleteBehavior.Cascade);
                
            modelBuilder.Entity<UserAnswer>()
                .HasOne(ua => ua.Question)
                .WithMany()
                .HasForeignKey(ua => ua.QuestionId)
                .OnDelete(DeleteBehavior.Cascade);
                
            // Create unique index for UserId + QuestionId (one answer per user per question)
            modelBuilder.Entity<UserAnswer>()
                .HasIndex(ua => new { ua.UserId, ua.QuestionId })
                .IsUnique();
                
            // WheelOfLifeScore configuration
            modelBuilder.Entity<WheelOfLifeScore>()
                .HasKey(w => w.Id);
                
            modelBuilder.Entity<WheelOfLifeScore>()
                .HasOne(w => w.User)
                .WithMany()
                .HasForeignKey(w => w.UserId)
                .OnDelete(DeleteBehavior.Cascade);
                
            modelBuilder.Entity<WheelOfLifeScore>()
                .HasOne(w => w.LifeArea)
                .WithMany()
                .HasForeignKey(w => w.LifeAreaId)
                .OnDelete(DeleteBehavior.Cascade);
                
            // Create unique index for UserId + LifeAreaId (one score per user per life area)
            modelBuilder.Entity<WheelOfLifeScore>()
                .HasIndex(w => new { w.UserId, w.LifeAreaId })
                .IsUnique();
                
            // Seed Questions data
            SeedQuestions(modelBuilder);
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

        private void SeedLifeAreas(ModelBuilder modelBuilder)
        {
            var now = DateTime.UtcNow;
            var lifeAreas = new[]
            {
                new LifeArea { Id = 1, Title = "Spiritual", IconClass = "fas fa-pray", IconColor = "#8e44ad", Description = "Connection with your inner self and beliefs", DisplayOrder = 1, IsActive = true, CreatedAt = now, UpdatedAt = now },
                new LifeArea { Id = 2, Title = "Physical Health", IconClass = "fas fa-heartbeat", IconColor = "#e74c3c", Description = "Physical wellness and fitness", DisplayOrder = 2, IsActive = true, CreatedAt = now, UpdatedAt = now },
                new LifeArea { Id = 3, Title = "Family & Friends", IconClass = "fas fa-users", IconColor = "#3498db", Description = "Relationships with loved ones", DisplayOrder = 3, IsActive = true, CreatedAt = now, UpdatedAt = now },
                new LifeArea { Id = 4, Title = "Partner", IconClass = "fas fa-heart", IconColor = "#e91e63", Description = "Romantic relationships and partnerships", DisplayOrder = 4, IsActive = true, CreatedAt = now, UpdatedAt = now },
                new LifeArea { Id = 5, Title = "Mission/Career", IconClass = "fas fa-briefcase", IconColor = "#34495e", Description = "Professional growth and purpose", DisplayOrder = 5, IsActive = true, CreatedAt = now, UpdatedAt = now },
                new LifeArea { Id = 6, Title = "Finances", IconClass = "fas fa-dollar-sign", IconColor = "#27ae60", Description = "Financial stability and growth", DisplayOrder = 6, IsActive = true, CreatedAt = now, UpdatedAt = now },
                new LifeArea { Id = 7, Title = "Personal Growth", IconClass = "fas fa-graduation-cap", IconColor = "#f39c12", Description = "Self-improvement and learning", DisplayOrder = 7, IsActive = true, CreatedAt = now, UpdatedAt = now },
                new LifeArea { Id = 8, Title = "Fun & Recreation", IconClass = "fas fa-gamepad", IconColor = "#9b59b6", Description = "Leisure activities and hobbies", DisplayOrder = 8, IsActive = true, CreatedAt = now, UpdatedAt = now },
                new LifeArea { Id = 9, Title = "Experiences", IconClass = "fas fa-plane", IconColor = "#1abc9c", Description = "Travel and new adventures", DisplayOrder = 9, IsActive = true, CreatedAt = now, UpdatedAt = now },
                new LifeArea { Id = 10, Title = "Environment", IconClass = "fas fa-home", IconColor = "#95a5a6", Description = "Living space and surroundings", DisplayOrder = 10, IsActive = true, CreatedAt = now, UpdatedAt = now }
            };

            modelBuilder.Entity<LifeArea>().HasData(lifeAreas);
        }
        
        private void SeedQuestions(ModelBuilder modelBuilder)
        {
            var now = DateTime.UtcNow;
            var questions = new[]
            {
                // Spiritual Questions
                new Question { Id = 1, LifeAreaId = 1, QuestionText = "¿Cuál es mi propósito en la vida?", CreatedAt = now, UpdatedAt = now },
                new Question { Id = 2, LifeAreaId = 1, QuestionText = "¿Qué prácticas espirituales me conectan con mi ser interior?", CreatedAt = now, UpdatedAt = now },
                
                // Physical Health Questions  
                new Question { Id = 3, LifeAreaId = 2, QuestionText = "¿Qué hábitos de salud necesito mejorar?", CreatedAt = now, UpdatedAt = now },
                new Question { Id = 4, LifeAreaId = 2, QuestionText = "¿Cómo puedo mantener un equilibrio entre ejercicio y descanso?", CreatedAt = now, UpdatedAt = now },
                
                // Family & Friends Questions
                new Question { Id = 5, LifeAreaId = 3, QuestionText = "¿Cómo puedo fortalecer mis relaciones familiares?", CreatedAt = now, UpdatedAt = now },
                new Question { Id = 6, LifeAreaId = 3, QuestionText = "¿Qué amistades aportan valor a mi vida?", CreatedAt = now, UpdatedAt = now },
                
                // Partner Questions
                new Question { Id = 7, LifeAreaId = 4, QuestionText = "¿Qué cualidades busco en una pareja?", CreatedAt = now, UpdatedAt = now },
                new Question { Id = 8, LifeAreaId = 4, QuestionText = "¿Cómo puedo mejorar mi comunicación en pareja?", CreatedAt = now, UpdatedAt = now },
                
                // Mission/Career Questions
                new Question { Id = 9, LifeAreaId = 5, QuestionText = "¿Mi trabajo actual se alinea con mi misión de vida?", CreatedAt = now, UpdatedAt = now },
                new Question { Id = 10, LifeAreaId = 5, QuestionText = "¿Qué habilidades necesito desarrollar para mi crecimiento profesional?", CreatedAt = now, UpdatedAt = now }
            };
            
            modelBuilder.Entity<Question>().HasData(questions);
        }
    }
}