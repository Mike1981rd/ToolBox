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
        public DbSet<SesionCalendario> SesionesCalendario { get; set; }
        public DbSet<SesionCliente> SesionesClientes { get; set; }
        public DbSet<SesionUsuario> SesionesUsuarios { get; set; }
        public DbSet<Session> Sessions { get; set; }
        public DbSet<SessionFile> SessionFiles { get; set; }
        public DbSet<Notification> Notifications { get; set; }
        public DbSet<NotificationPreference> NotificationPreferences { get; set; }
        public DbSet<QuestionnaireTemplate> QuestionnaireTemplates { get; set; }
        public DbSet<QuestionTemplate> QuestionTemplates { get; set; }
        public DbSet<QuestionnaireInstance> QuestionnaireInstances { get; set; }
        public DbSet<Answer> Answers { get; set; }
        public DbSet<LifeEvent> LifeEvents { get; set; }
        public DbSet<CommunicationWheelTemplate> CommunicationWheelTemplates { get; set; }
        public DbSet<CommunicationDimension> CommunicationDimensions { get; set; }
        public DbSet<ClientCommunicationWheelInstance> ClientCommunicationWheelInstances { get; set; }
        public DbSet<DimensionScore> DimensionScores { get; set; }
        
        // Feedback 360 entities
        public DbSet<Feedback360Instance> Feedback360Instances { get; set; }
        public DbSet<Feedback360Rater> Feedback360Raters { get; set; }
        public DbSet<Feedback360ResponseScale> Feedback360ResponseScales { get; set; }
        public DbSet<Feedback360ResponseOpen> Feedback360ResponseOpens { get; set; }

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
                
            // SesionCalendario configuration
            modelBuilder.Entity<SesionCalendario>()
                .HasOne(sc => sc.Coach)
                .WithMany()
                .HasForeignKey(sc => sc.CoachId)
                .OnDelete(DeleteBehavior.Restrict);
                
            modelBuilder.Entity<SesionCalendario>()
                .HasIndex(sc => sc.FechaHoraInicio);
                
            modelBuilder.Entity<SesionCalendario>()
                .HasIndex(sc => new { sc.CoachId, sc.FechaHoraInicio });
                
            // SesionCliente configuration (Many-to-Many relationship)
            modelBuilder.Entity<SesionCliente>()
                .HasKey(sc => new { sc.SesionCalendarioId, sc.ClienteId });
                
            modelBuilder.Entity<SesionCliente>()
                .HasOne(sc => sc.SesionCalendario)
                .WithMany(s => s.SesionClientes)
                .HasForeignKey(sc => sc.SesionCalendarioId)
                .OnDelete(DeleteBehavior.Cascade);
                
            modelBuilder.Entity<SesionCliente>()
                .HasOne(sc => sc.Cliente)
                .WithMany()
                .HasForeignKey(sc => sc.ClienteId)
                .OnDelete(DeleteBehavior.Cascade);
                
            // SesionUsuario configuration (Many-to-Many relationship)
            modelBuilder.Entity<SesionUsuario>()
                .HasKey(su => new { su.SesionCalendarioId, su.UsuarioId });
                
            modelBuilder.Entity<SesionUsuario>()
                .HasOne(su => su.SesionCalendario)
                .WithMany(s => s.SesionUsuarios)
                .HasForeignKey(su => su.SesionCalendarioId)
                .OnDelete(DeleteBehavior.Cascade);
                
            modelBuilder.Entity<SesionUsuario>()
                .HasOne(su => su.Usuario)
                .WithMany()
                .HasForeignKey(su => su.UsuarioId)
                .OnDelete(DeleteBehavior.Cascade);
                
            // Configure nullable DateTime for PostgreSQL
            modelBuilder.Entity<SesionUsuario>()
                .Property(su => su.FechaConfirmacion)
                .HasColumnType("timestamp with time zone");

            // Session configuration
            modelBuilder.Entity<Session>()
                .HasKey(s => s.Id);

            modelBuilder.Entity<Session>()
                .HasOne(s => s.User)
                .WithMany()
                .HasForeignKey(s => s.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Session>()
                .HasIndex(s => new { s.UserId, s.SessionDateTime });

            modelBuilder.Entity<Session>()
                .Property(s => s.KeyPoints)
                .HasColumnType("TEXT");

            modelBuilder.Entity<Session>()
                .Property(s => s.WaysToDevelop)
                .HasColumnType("TEXT");

            modelBuilder.Entity<Session>()
                .Property(s => s.Assignments)
                .HasColumnType("TEXT");

            modelBuilder.Entity<Session>()
                .Property(s => s.Challenges)
                .HasColumnType("TEXT");

            modelBuilder.Entity<Session>()
                .Property(s => s.Feedback)
                .HasColumnType("TEXT");

            // SessionFile configuration
            modelBuilder.Entity<SessionFile>()
                .HasKey(sf => sf.Id);

            modelBuilder.Entity<SessionFile>()
                .HasOne(sf => sf.Session)
                .WithMany(s => s.SessionFiles)
                .HasForeignKey(sf => sf.SessionId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<SessionFile>()
                .HasIndex(sf => sf.SessionId);

            // Notification configuration
            modelBuilder.Entity<Notification>()
                .HasKey(n => n.Id);

            modelBuilder.Entity<Notification>()
                .HasOne(n => n.User)
                .WithMany()
                .HasForeignKey(n => n.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Notification>()
                .HasIndex(n => new { n.UserId, n.CreatedAt });

            modelBuilder.Entity<Notification>()
                .HasIndex(n => new { n.UserId, n.ReadAt });

            modelBuilder.Entity<Notification>()
                .Property(n => n.Data)
                .HasColumnType("TEXT");

            // NotificationPreference configuration
            modelBuilder.Entity<NotificationPreference>()
                .HasKey(np => np.Id);

            modelBuilder.Entity<NotificationPreference>()
                .HasOne(np => np.User)
                .WithMany()
                .HasForeignKey(np => np.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<NotificationPreference>()
                .HasIndex(np => new { np.UserId, np.NotificationType })
                .IsUnique();

            // QuestionnaireTemplate configuration
            modelBuilder.Entity<QuestionnaireTemplate>()
                .HasKey(qt => qt.Id);

            modelBuilder.Entity<QuestionnaireTemplate>()
                .HasOne(qt => qt.Coach)
                .WithMany()
                .HasForeignKey(qt => qt.CoachId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<QuestionnaireTemplate>()
                .HasIndex(qt => qt.CoachId);

            modelBuilder.Entity<QuestionnaireTemplate>()
                .HasIndex(qt => qt.CreatedAt);

            modelBuilder.Entity<QuestionnaireTemplate>()
                .Property(qt => qt.Description)
                .HasColumnType("TEXT");

            // QuestionTemplate configuration
            modelBuilder.Entity<QuestionTemplate>()
                .HasKey(q => q.Id);

            modelBuilder.Entity<QuestionTemplate>()
                .HasOne(q => q.QuestionnaireTemplate)
                .WithMany(qt => qt.Questions)
                .HasForeignKey(q => q.QuestionnaireTemplateId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<QuestionTemplate>()
                .HasIndex(q => new { q.QuestionnaireTemplateId, q.Order });

            modelBuilder.Entity<QuestionTemplate>()
                .Property(q => q.QuestionText)
                .HasColumnType("TEXT");

            modelBuilder.Entity<QuestionTemplate>()
                .Property(q => q.OptionsJson)
                .HasColumnType("TEXT");

            // QuestionnaireInstance configuration
            modelBuilder.Entity<QuestionnaireInstance>()
                .ToTable("QuestionnaireInstances");
                
            modelBuilder.Entity<QuestionnaireInstance>()
                .HasKey(qi => qi.Id);

            modelBuilder.Entity<QuestionnaireInstance>()
                .HasOne(qi => qi.QuestionnaireTemplate)
                .WithMany()
                .HasForeignKey(qi => qi.QuestionnaireTemplateId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<QuestionnaireInstance>()
                .HasOne(qi => qi.Client)
                .WithMany()
                .HasForeignKey(qi => qi.ClientId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<QuestionnaireInstance>()
                .HasOne(qi => qi.AssignedByCoach)
                .WithMany()
                .HasForeignKey(qi => qi.AssignedByCoachId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<QuestionnaireInstance>()
                .HasIndex(qi => new { qi.ClientId, qi.Status });

            modelBuilder.Entity<QuestionnaireInstance>()
                .HasIndex(qi => qi.AssignedAt);

            // Answer configuration
            modelBuilder.Entity<Answer>()
                .ToTable("Answers");
                
            modelBuilder.Entity<Answer>()
                .HasKey(a => a.Id);

            modelBuilder.Entity<Answer>()
                .HasOne(a => a.QuestionnaireInstance)
                .WithMany(qi => qi.Answers)
                .HasForeignKey(a => a.QuestionnaireInstanceId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Answer>()
                .HasOne(a => a.QuestionTemplate)
                .WithMany()
                .HasForeignKey(a => a.QuestionTemplateId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Answer>()
                .Property(a => a.ResponseText)
                .HasColumnType("TEXT");

            // Índice único para evitar respuestas duplicadas
            modelBuilder.Entity<Answer>()
                .HasIndex(a => new { a.QuestionnaireInstanceId, a.QuestionTemplateId })
                .IsUnique();

            // Feedback 360 configurations
            ConfigureFeedback360Entities(modelBuilder);
        }

        private void ConfigureFeedback360Entities(ModelBuilder modelBuilder)
        {
            // Feedback360Instance configuration
            modelBuilder.Entity<Feedback360Instance>()
                .HasKey(f => f.Id);

            modelBuilder.Entity<Feedback360Instance>()
                .HasOne(f => f.SubjectUser)
                .WithMany()
                .HasForeignKey(f => f.SubjectUserId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Feedback360Instance>()
                .HasOne(f => f.InitiatedByCoach)
                .WithMany()
                .HasForeignKey(f => f.InitiatedByCoachId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Feedback360Instance>()
                .HasIndex(f => f.SubjectUserId);

            modelBuilder.Entity<Feedback360Instance>()
                .HasIndex(f => f.InitiatedByCoachId);

            modelBuilder.Entity<Feedback360Instance>()
                .HasIndex(f => f.CreatedAt);

            // Feedback360Rater configuration
            modelBuilder.Entity<Feedback360Rater>()
                .HasKey(r => r.Id);

            modelBuilder.Entity<Feedback360Rater>()
                .HasOne(r => r.Feedback360Instance)
                .WithMany(f => f.Raters)
                .HasForeignKey(r => r.Feedback360InstanceId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Feedback360Rater>()
                .HasOne(r => r.RaterUser)
                .WithMany()
                .HasForeignKey(r => r.RaterUserId)
                .OnDelete(DeleteBehavior.SetNull);

            modelBuilder.Entity<Feedback360Rater>()
                .HasIndex(r => r.UniqueResponseToken)
                .IsUnique();

            modelBuilder.Entity<Feedback360Rater>()
                .HasIndex(r => r.Feedback360InstanceId);

            modelBuilder.Entity<Feedback360Rater>()
                .HasIndex(r => r.RaterUserId);

            // Feedback360ResponseScale configuration
            modelBuilder.Entity<Feedback360ResponseScale>()
                .HasKey(rs => rs.Id);

            modelBuilder.Entity<Feedback360ResponseScale>()
                .HasOne(rs => rs.Feedback360Rater)
                .WithMany(r => r.ScaleResponses)
                .HasForeignKey(rs => rs.Feedback360RaterId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Feedback360ResponseScale>()
                .HasIndex(rs => new { rs.Feedback360RaterId, rs.QuestionCode })
                .IsUnique();

            // Feedback360ResponseOpen configuration
            modelBuilder.Entity<Feedback360ResponseOpen>()
                .HasKey(ro => ro.Id);

            modelBuilder.Entity<Feedback360ResponseOpen>()
                .HasOne(ro => ro.Feedback360Rater)
                .WithMany(r => r.OpenEndedResponses)
                .HasForeignKey(ro => ro.Feedback360RaterId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Feedback360ResponseOpen>()
                .Property(ro => ro.ResponseText)
                .HasColumnType("TEXT");

            modelBuilder.Entity<Feedback360ResponseOpen>()
                .HasIndex(ro => new { ro.Feedback360RaterId, ro.QuestionCode })
                .IsUnique();
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
                "WelcomeMessage",
                "Calendario",
                "Sessions",
                "QuestionnaireTemplates"
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

            // Seed permissions data
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
                "Calendario" or "Sessions" => "Gestión de Sesiones",
                "Notifications" => "General",
                "QuestionnaireTemplates" => "Herramientas de Evaluación",
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
                "Calendario" => "Calendario de Sesiones",
                "Sessions" => "Sesiones",
                "Notifications" => "Notificaciones",
                "QuestionnaireTemplates" => "Cuestionarios",
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

            // LifeEvent configuration
            modelBuilder.Entity<LifeEvent>()
                .HasKey(le => le.Id);

            modelBuilder.Entity<LifeEvent>()
                .HasOne(le => le.User)
                .WithMany()
                .HasForeignKey(le => le.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<LifeEvent>()
                .Property(le => le.EventTitle)
                .HasMaxLength(200)
                .IsRequired();

            modelBuilder.Entity<LifeEvent>()
                .Property(le => le.Description)
                .HasColumnType("TEXT");

            // Create indexes for better query performance
            modelBuilder.Entity<LifeEvent>()
                .HasIndex(le => le.UserId);

            modelBuilder.Entity<LifeEvent>()
                .HasIndex(le => new { le.UserId, le.EventYear });
                
            // CommunicationWheelTemplate configuration
            modelBuilder.Entity<CommunicationWheelTemplate>()
                .HasKey(cwt => cwt.Id);
                
            modelBuilder.Entity<CommunicationWheelTemplate>()
                .HasOne(cwt => cwt.Coach)
                .WithMany()
                .HasForeignKey(cwt => cwt.CoachId)
                .OnDelete(DeleteBehavior.Restrict);
                
            modelBuilder.Entity<CommunicationWheelTemplate>()
                .Property(cwt => cwt.Description)
                .HasColumnType("TEXT");
                
            modelBuilder.Entity<CommunicationWheelTemplate>()
                .HasIndex(cwt => cwt.CoachId);
                
            modelBuilder.Entity<CommunicationWheelTemplate>()
                .HasIndex(cwt => cwt.IsActive);
                
            // CommunicationDimension configuration
            modelBuilder.Entity<CommunicationDimension>()
                .HasKey(cd => cd.Id);
                
            modelBuilder.Entity<CommunicationDimension>()
                .HasOne(cd => cd.CommunicationWheelTemplate)
                .WithMany(cwt => cwt.Dimensions)
                .HasForeignKey(cd => cd.CommunicationWheelTemplateId)
                .OnDelete(DeleteBehavior.Cascade);
                
            modelBuilder.Entity<CommunicationDimension>()
                .Property(cd => cd.GuidingQuestion)
                .HasColumnType("TEXT");
                
            modelBuilder.Entity<CommunicationDimension>()
                .HasIndex(cd => new { cd.CommunicationWheelTemplateId, cd.Order });
                
            // ClientCommunicationWheelInstance configuration
            modelBuilder.Entity<ClientCommunicationWheelInstance>()
                .HasKey(ccwi => ccwi.Id);
                
            modelBuilder.Entity<ClientCommunicationWheelInstance>()
                .HasOne(ccwi => ccwi.CommunicationWheelTemplate)
                .WithMany()
                .HasForeignKey(ccwi => ccwi.CommunicationWheelTemplateId)
                .OnDelete(DeleteBehavior.Restrict);
                
            modelBuilder.Entity<ClientCommunicationWheelInstance>()
                .HasOne(ccwi => ccwi.Client)
                .WithMany()
                .HasForeignKey(ccwi => ccwi.ClientId)
                .OnDelete(DeleteBehavior.Cascade);
                
            modelBuilder.Entity<ClientCommunicationWheelInstance>()
                .HasOne(ccwi => ccwi.AssignedByCoach)
                .WithMany()
                .HasForeignKey(ccwi => ccwi.AssignedByCoachId)
                .OnDelete(DeleteBehavior.Restrict);
                
            modelBuilder.Entity<ClientCommunicationWheelInstance>()
                .Property(ccwi => ccwi.ClientNotes)
                .HasColumnType("TEXT");
                
            modelBuilder.Entity<ClientCommunicationWheelInstance>()
                .HasIndex(ccwi => new { ccwi.ClientId, ccwi.Status });
                
            modelBuilder.Entity<ClientCommunicationWheelInstance>()
                .HasIndex(ccwi => ccwi.AssignedAt);
                
            // DimensionScore configuration
            modelBuilder.Entity<DimensionScore>()
                .HasKey(ds => ds.Id);
                
            modelBuilder.Entity<DimensionScore>()
                .HasOne(ds => ds.ClientCommunicationWheelInstance)
                .WithMany(ccwi => ccwi.Scores)
                .HasForeignKey(ds => ds.ClientCommunicationWheelInstanceId)
                .OnDelete(DeleteBehavior.Cascade);
                
            modelBuilder.Entity<DimensionScore>()
                .HasOne(ds => ds.CommunicationDimension)
                .WithMany()
                .HasForeignKey(ds => ds.CommunicationDimensionId)
                .OnDelete(DeleteBehavior.Restrict);
                
            // Unique index to prevent duplicate scores
            modelBuilder.Entity<DimensionScore>()
                .HasIndex(ds => new { ds.ClientCommunicationWheelInstanceId, ds.CommunicationDimensionId })
                .IsUnique();
        }
    }
}