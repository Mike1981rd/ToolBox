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
        public DbSet<AreaProgreso> AreasProgreso { get; set; }
        public DbSet<CategoriaProgreso> CategoriasProgreso { get; set; }
        public DbSet<ProgresoMetaUsuario> ProgresosMetasUsuarios { get; set; }
        public DbSet<Tarea> Tareas { get; set; }
        public DbSet<Habito> Habitos { get; set; }
        public DbSet<RegistroCumplimientoHabito> RegistrosCumplimientoHabitos { get; set; }
        public DbSet<CategoriaHabito> CategoriasHabitos { get; set; }
        public DbSet<FrecuenciaHabito> FrecuenciasHabitos { get; set; }
        public DbSet<WelcomeMessageSettings> WelcomeMessageSettings { get; set; }
        public DbSet<Topic> Topics { get; set; }
        public DbSet<Video> Videos { get; set; }
        public DbSet<WebsiteConfiguration> WebsiteConfiguration { get; set; }
        public DbSet<Customer> Customers { get; set; }

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
                
            // AreaProgreso configuration
            modelBuilder.Entity<AreaProgreso>()
                .HasKey(ap => ap.Id);
                
            modelBuilder.Entity<AreaProgreso>()
                .HasIndex(ap => ap.OrdenVisualizacion);
                
            // CategoriaProgreso configuration
            modelBuilder.Entity<CategoriaProgreso>()
                .HasKey(cp => cp.Id);
                
            modelBuilder.Entity<CategoriaProgreso>()
                .HasOne(cp => cp.AreaProgreso)
                .WithMany(ap => ap.Categorias)
                .HasForeignKey(cp => cp.AreaProgresoId)
                .OnDelete(DeleteBehavior.Cascade);
                
            modelBuilder.Entity<CategoriaProgreso>()
                .HasIndex(cp => new { cp.AreaProgresoId, cp.OrdenVisualizacion });
                
            // ProgresoMetaUsuario configuration
            modelBuilder.Entity<ProgresoMetaUsuario>()
                .HasKey(pmu => pmu.Id);
                
            modelBuilder.Entity<ProgresoMetaUsuario>()
                .HasOne(pmu => pmu.User)
                .WithMany()
                .HasForeignKey(pmu => pmu.UserId)
                .OnDelete(DeleteBehavior.Cascade);
                
            modelBuilder.Entity<ProgresoMetaUsuario>()
                .HasOne(pmu => pmu.CategoriaProgreso)
                .WithMany(cp => cp.ProgresosUsuarios)
                .HasForeignKey(pmu => pmu.CategoriaProgresoId)
                .OnDelete(DeleteBehavior.Cascade);
                
            // Create unique index for UserId + CategoriaProgresoId (one progress per user per category)
            modelBuilder.Entity<ProgresoMetaUsuario>()
                .HasIndex(pmu => new { pmu.UserId, pmu.CategoriaProgresoId })
                .IsUnique();
                
            // Seed Questions data
            SeedQuestions(modelBuilder);
            
            // Seed Wheel of Progress data
            SeedWheelOfProgressData(modelBuilder);
            
            // Tarea configuration
            modelBuilder.Entity<Tarea>()
                .HasKey(t => t.Id);
                
            modelBuilder.Entity<Tarea>()
                .Property(t => t.Descripcion)
                .IsRequired(false);
                
            modelBuilder.Entity<Tarea>()
                .HasOne(t => t.Usuario)
                .WithMany()
                .HasForeignKey(t => t.UsuarioId)
                .OnDelete(DeleteBehavior.Cascade);
                
            modelBuilder.Entity<Tarea>()
                .HasIndex(t => new { t.UsuarioId, t.FechaCreacion });
                
            // Habito configuration
            modelBuilder.Entity<Habito>()
                .HasKey(h => h.Id);
                
            modelBuilder.Entity<Habito>()
                .HasOne(h => h.Usuario)
                .WithMany()
                .HasForeignKey(h => h.UsuarioId)
                .OnDelete(DeleteBehavior.Cascade);
                
            modelBuilder.Entity<Habito>()
                .HasOne(h => h.CategoriaHabito)
                .WithMany(c => c.Habitos)
                .HasForeignKey(h => h.CategoriaHabitoId)
                .OnDelete(DeleteBehavior.Restrict);
                
            modelBuilder.Entity<Habito>()
                .HasOne(h => h.FrecuenciaHabito)
                .WithMany(f => f.Habitos)
                .HasForeignKey(h => h.FrecuenciaHabitoId)
                .OnDelete(DeleteBehavior.Restrict);
                
            modelBuilder.Entity<Habito>()
                .HasIndex(h => new { h.UsuarioId, h.FechaCreacion });
                
            // RegistroCumplimientoHabito configuration
            modelBuilder.Entity<RegistroCumplimientoHabito>()
                .HasKey(r => r.Id);
                
            modelBuilder.Entity<RegistroCumplimientoHabito>()
                .HasOne(r => r.Habito)
                .WithMany(h => h.RegistrosCumplimiento)
                .HasForeignKey(r => r.HabitoId)
                .OnDelete(DeleteBehavior.Cascade);
                
            // Índice único para evitar registros duplicados del mismo hábito en la misma fecha
            modelBuilder.Entity<RegistroCumplimientoHabito>()
                .HasIndex(r => new { r.HabitoId, r.Fecha })
                .IsUnique();
                
            // CategoriaHabito configuration
            modelBuilder.Entity<CategoriaHabito>()
                .HasKey(c => c.Id);
                
            modelBuilder.Entity<CategoriaHabito>()
                .HasIndex(c => c.OrdenVisualizacion);
                
            // FrecuenciaHabito configuration
            modelBuilder.Entity<FrecuenciaHabito>()
                .HasKey(f => f.Id);
                
            modelBuilder.Entity<FrecuenciaHabito>()
                .HasIndex(f => f.OrdenVisualizacion);
                
            // Seed Habit Tracker data
            SeedHabitTrackerData(modelBuilder);
            
            // Video configuration
            modelBuilder.Entity<Video>()
                .HasOne(v => v.Autor)
                .WithMany()
                .HasForeignKey(v => v.AutorId)
                .OnDelete(DeleteBehavior.SetNull);
                
            modelBuilder.Entity<Video>()
                .HasOne(v => v.Tema)
                .WithMany()
                .HasForeignKey(v => v.TemaId)
                .OnDelete(DeleteBehavior.SetNull);
                
            modelBuilder.Entity<Video>()
                .HasOne(v => v.UsuarioCreador)
                .WithMany()
                .HasForeignKey(v => v.UsuarioCreadorId)
                .OnDelete(DeleteBehavior.SetNull);
                
            modelBuilder.Entity<Video>()
                .HasIndex(v => v.Titulo);
                
            modelBuilder.Entity<Video>()
                .HasIndex(v => v.EstadoVideo);
                
            // WebsiteConfiguration configuration
            modelBuilder.Entity<WebsiteConfiguration>()
                .HasOne(w => w.LastUpdatedByUser)
                .WithMany()
                .HasForeignKey(w => w.LastUpdatedByUserId)
                .OnDelete(DeleteBehavior.SetNull);
                
            // Customer configuration
            modelBuilder.Entity<Customer>()
                .HasOne(c => c.CreatedByUser)
                .WithMany()
                .HasForeignKey(c => c.CreatedByUserId)
                .IsRequired(false)
                .OnDelete(DeleteBehavior.SetNull);
                
            // Índice único para email de clientes
            modelBuilder.Entity<Customer>()
                .HasIndex(c => c.Email)
                .IsUnique();
                
            // Índice para búsquedas por nombre
            modelBuilder.Entity<Customer>()
                .HasIndex(c => new { c.FirstName, c.LastName });
                
            // Índice por fecha de creación
            modelBuilder.Entity<Customer>()
                .HasIndex(c => c.CreatedAt);
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
        
        private void SeedWheelOfProgressData(ModelBuilder modelBuilder)
        {
            var now = DateTime.UtcNow;
            
            // Seed AreaProgreso (basado en la imagen original del sistema)
            var areasProgreso = new[]
            {
                new AreaProgreso { Id = 1, Nombre = "Vida Empresarial", Descripcion = "Carrera, negocios y crecimiento profesional", IconClass = "fas fa-briefcase", IconColor = "#2c3e50", OrdenVisualizacion = 1, CreatedAt = now },
                new AreaProgreso { Id = 2, Nombre = "Vida Creativa", Descripcion = "Expresión artística y creatividad", IconClass = "fas fa-palette", IconColor = "#e74c3c", OrdenVisualizacion = 2, CreatedAt = now },
                new AreaProgreso { Id = 3, Nombre = "Vida Social", Descripcion = "Relaciones con amigos y comunidad", IconClass = "fas fa-users", IconColor = "#3498db", OrdenVisualizacion = 3, CreatedAt = now },
                new AreaProgreso { Id = 4, Nombre = "Vida Amorosa", Descripcion = "Relaciones románticas y pareja", IconClass = "fas fa-heart", IconColor = "#e91e63", OrdenVisualizacion = 4, CreatedAt = now },
                new AreaProgreso { Id = 5, Nombre = "Propósito de Vida", Descripcion = "Misión personal y espiritualidad", IconClass = "fas fa-compass", IconColor = "#9b59b6", OrdenVisualizacion = 5, CreatedAt = now }
            };

            // Seed CategoriaProgreso (basado en el sistema original)
            var categoriasProgreso = new[]
            {
                // Vida Empresarial
                new CategoriaProgreso { Id = 1, Nombre = "Dinero y Finanzas", AreaProgresoId = 1, OrdenVisualizacion = 1, CreatedAt = now },
                new CategoriaProgreso { Id = 2, Nombre = "Carrera y Misión", AreaProgresoId = 1, OrdenVisualizacion = 2, CreatedAt = now },
                new CategoriaProgreso { Id = 3, Nombre = "Productividad", AreaProgresoId = 1, OrdenVisualizacion = 3, CreatedAt = now },
                
                // Vida Creativa
                new CategoriaProgreso { Id = 4, Nombre = "Arte y Expresión", AreaProgresoId = 2, OrdenVisualizacion = 1, CreatedAt = now },
                new CategoriaProgreso { Id = 5, Nombre = "Proyectos Creativos", AreaProgresoId = 2, OrdenVisualizacion = 2, CreatedAt = now },
                new CategoriaProgreso { Id = 6, Nombre = "Inspiración", AreaProgresoId = 2, OrdenVisualizacion = 3, CreatedAt = now },
                
                // Vida Social
                new CategoriaProgreso { Id = 7, Nombre = "Amistades", AreaProgresoId = 3, OrdenVisualizacion = 1, CreatedAt = now },
                new CategoriaProgreso { Id = 8, Nombre = "Networking", AreaProgresoId = 3, OrdenVisualizacion = 2, CreatedAt = now },
                new CategoriaProgreso { Id = 9, Nombre = "Comunidad", AreaProgresoId = 3, OrdenVisualizacion = 3, CreatedAt = now },
                
                // Vida Amorosa
                new CategoriaProgreso { Id = 10, Nombre = "Relación de Pareja", AreaProgresoId = 4, OrdenVisualizacion = 1, CreatedAt = now },
                new CategoriaProgreso { Id = 11, Nombre = "Familia", AreaProgresoId = 4, OrdenVisualizacion = 2, CreatedAt = now },
                new CategoriaProgreso { Id = 12, Nombre = "Amor Propio", AreaProgresoId = 4, OrdenVisualizacion = 3, CreatedAt = now },
                
                // Propósito de Vida
                new CategoriaProgreso { Id = 13, Nombre = "Espiritualidad", AreaProgresoId = 5, OrdenVisualizacion = 1, CreatedAt = now },
                new CategoriaProgreso { Id = 14, Nombre = "Valores y Principios", AreaProgresoId = 5, OrdenVisualizacion = 2, CreatedAt = now },
                new CategoriaProgreso { Id = 15, Nombre = "Salud y Fitness", AreaProgresoId = 5, OrdenVisualizacion = 3, CreatedAt = now }
            };

            modelBuilder.Entity<AreaProgreso>().HasData(areasProgreso);
            modelBuilder.Entity<CategoriaProgreso>().HasData(categoriasProgreso);
        }
        
        private void SeedHabitTrackerData(ModelBuilder modelBuilder)
        {
            var now = DateTime.UtcNow;
            
            // Seed CategoriaHabito
            var categoriasHabitos = new[]
            {
                new CategoriaHabito { Id = 1, Nombre = "Salud", Descripcion = "Hábitos relacionados con bienestar físico y mental", IconClass = "fas fa-heartbeat", Color = "#e74c3c", OrdenVisualizacion = 1, CreatedAt = now },
                new CategoriaHabito { Id = 2, Nombre = "Productividad", Descripcion = "Hábitos que mejoran el rendimiento y eficiencia", IconClass = "fas fa-chart-line", Color = "#3498db", OrdenVisualizacion = 2, CreatedAt = now },
                new CategoriaHabito { Id = 3, Nombre = "Aprendizaje", Descripcion = "Hábitos de educación y desarrollo personal", IconClass = "fas fa-graduation-cap", Color = "#f39c12", OrdenVisualizacion = 3, CreatedAt = now },
                new CategoriaHabito { Id = 4, Nombre = "Mindfulness", Descripcion = "Hábitos de meditación y atención plena", IconClass = "fas fa-leaf", Color = "#27ae60", OrdenVisualizacion = 4, CreatedAt = now },
                new CategoriaHabito { Id = 5, Nombre = "Social", Descripcion = "Hábitos relacionados con relaciones y vida social", IconClass = "fas fa-users", Color = "#9b59b6", OrdenVisualizacion = 5, CreatedAt = now },
                new CategoriaHabito { Id = 6, Nombre = "Creatividad", Descripcion = "Hábitos artísticos y de expresión creativa", IconClass = "fas fa-palette", Color = "#e67e22", OrdenVisualizacion = 6, CreatedAt = now },
                new CategoriaHabito { Id = 7, Nombre = "Finanzas", Descripcion = "Hábitos de manejo financiero y ahorro", IconClass = "fas fa-dollar-sign", Color = "#16a085", OrdenVisualizacion = 7, CreatedAt = now },
                new CategoriaHabito { Id = 8, Nombre = "Hogar", Descripcion = "Hábitos de organización y cuidado del hogar", IconClass = "fas fa-home", Color = "#95a5a6", OrdenVisualizacion = 8, CreatedAt = now }
            };

            // Seed FrecuenciaHabito
            var frecuenciasHabitos = new[]
            {
                new FrecuenciaHabito { Id = 1, Nombre = "Diario", Descripcion = "Todos los días", DiasIntervalo = 1, OrdenVisualizacion = 1, CreatedAt = now },
                new FrecuenciaHabito { Id = 2, Nombre = "Semanal", Descripcion = "Una vez por semana", DiasIntervalo = 7, OrdenVisualizacion = 2, CreatedAt = now },
                new FrecuenciaHabito { Id = 3, Nombre = "3 veces por semana", Descripcion = "Lunes, miércoles y viernes", DiasIntervalo = 2, OrdenVisualizacion = 3, CreatedAt = now },
                new FrecuenciaHabito { Id = 4, Nombre = "Fines de semana", Descripcion = "Sábados y domingos", DiasIntervalo = 7, OrdenVisualizacion = 4, CreatedAt = now },
                new FrecuenciaHabito { Id = 5, Nombre = "Días laborales", Descripcion = "Lunes a viernes", DiasIntervalo = 1, OrdenVisualizacion = 5, CreatedAt = now },
                new FrecuenciaHabito { Id = 6, Nombre = "Mensual", Descripcion = "Una vez al mes", DiasIntervalo = 30, OrdenVisualizacion = 6, CreatedAt = now }
            };

            modelBuilder.Entity<CategoriaHabito>().HasData(categoriasHabitos);
            modelBuilder.Entity<FrecuenciaHabito>().HasData(frecuenciasHabitos);
        }
    }
}