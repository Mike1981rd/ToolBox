using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ToolBox.Migrations
{
    /// <inheritdoc />
    public partial class SeedPermissionsDataV2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Permissions",
                columns: new[] { "ModuleName", "ActionName", "Description", "Category" },
                values: new object[,]
                {
                    { "Dashboard", "Read", "Ver y listar en Tablero", "General" },
                    { "Dashboard", "Write", "Editar y actualizar en Tablero", "General" },
                    { "Dashboard", "Create", "Crear nuevos registros en Tablero", "General" },
                    { "Topics", "Read", "Ver y listar en Temas", "Gestión de Contenido" },
                    { "Topics", "Write", "Editar y actualizar en Temas", "Gestión de Contenido" },
                    { "Topics", "Create", "Crear nuevos registros en Temas", "Gestión de Contenido" },
                    { "VideoManagement", "Read", "Ver y listar en Gestión de Videos", "Gestión de Contenido" },
                    { "VideoManagement", "Write", "Editar y actualizar en Gestión de Videos", "Gestión de Contenido" },
                    { "VideoManagement", "Create", "Crear nuevos registros en Gestión de Videos", "Gestión de Contenido" },
                    { "Customers", "Read", "Ver y listar en Clientes", "Gestión de Usuarios" },
                    { "Customers", "Write", "Editar y actualizar en Clientes", "Gestión de Usuarios" },
                    { "Customers", "Create", "Crear nuevos registros en Clientes", "Gestión de Usuarios" },
                    { "Users", "Read", "Ver y listar en Usuarios", "Gestión de Usuarios" },
                    { "Users", "Write", "Editar y actualizar en Usuarios", "Gestión de Usuarios" },
                    { "Users", "Create", "Crear nuevos registros en Usuarios", "Gestión de Usuarios" },
                    { "Roles", "Read", "Ver y listar en Roles", "Gestión de Usuarios" },
                    { "Roles", "Write", "Editar y actualizar en Roles", "Gestión de Usuarios" },
                    { "Roles", "Create", "Crear nuevos registros en Roles", "Gestión de Usuarios" },
                    { "Instructors", "Read", "Ver y listar en Instructores", "Gestión de Usuarios" },
                    { "Instructors", "Write", "Editar y actualizar en Instructores", "Gestión de Usuarios" },
                    { "Instructors", "Create", "Crear nuevos registros en Instructores", "Gestión de Usuarios" },
                    { "ToolboxAcademy", "Read", "Ver y listar en Toolbox Academy", "Gestión de Contenido" },
                    { "ToolboxAcademy", "Write", "Editar y actualizar en Toolbox Academy", "Gestión de Contenido" },
                    { "ToolboxAcademy", "Create", "Crear nuevos registros en Toolbox Academy", "Gestión de Contenido" },
                    { "WheelOfLife", "Read", "Ver y listar en Rueda de la Vida", "Herramientas de Vida" },
                    { "WheelOfLife", "Write", "Editar y actualizar en Rueda de la Vida", "Herramientas de Vida" },
                    { "WheelOfLife", "Create", "Crear nuevos registros en Rueda de la Vida", "Herramientas de Vida" },
                    { "WheelOfProgress", "Read", "Ver y listar en Rueda del Progreso", "Herramientas de Vida" },
                    { "WheelOfProgress", "Write", "Editar y actualizar en Rueda del Progreso", "Herramientas de Vida" },
                    { "WheelOfProgress", "Create", "Crear nuevos registros en Rueda del Progreso", "Herramientas de Vida" },
                    { "XRayLife", "Read", "Ver y listar en Rayos X de la Vida", "Herramientas de Vida" },
                    { "XRayLife", "Write", "Editar y actualizar en Rayos X de la Vida", "Herramientas de Vida" },
                    { "XRayLife", "Create", "Crear nuevos registros en Rayos X de la Vida", "Herramientas de Vida" },
                    { "LifeAssessment", "Read", "Ver y listar en Evaluación de Vida", "Herramientas de Vida" },
                    { "LifeAssessment", "Write", "Editar y actualizar en Evaluación de Vida", "Herramientas de Vida" },
                    { "LifeAssessment", "Create", "Crear nuevos registros en Evaluación de Vida", "Herramientas de Vida" },
                    { "LifeAreas", "Read", "Ver y listar en Áreas de Vida", "Herramientas de Vida" },
                    { "LifeAreas", "Write", "Editar y actualizar en Áreas de Vida", "Herramientas de Vida" },
                    { "LifeAreas", "Create", "Crear nuevos registros en Áreas de Vida", "Herramientas de Vida" },
                    { "Tasks", "Read", "Ver y listar en Tareas", "Productividad" },
                    { "Tasks", "Write", "Editar y actualizar en Tareas", "Productividad" },
                    { "Tasks", "Create", "Crear nuevos registros en Tareas", "Productividad" },
                    { "HabitTracker", "Read", "Ver y listar en Seguimiento de Hábitos", "Productividad" },
                    { "HabitTracker", "Write", "Editar y actualizar en Seguimiento de Hábitos", "Productividad" },
                    { "HabitTracker", "Create", "Crear nuevos registros en Seguimiento de Hábitos", "Productividad" },
                    { "EmailContents", "Read", "Ver y listar en Contenido de Emails", "Configuración" },
                    { "EmailContents", "Write", "Editar y actualizar en Contenido de Emails", "Configuración" },
                    { "EmailContents", "Create", "Crear nuevos registros en Contenido de Emails", "Configuración" },
                    { "WebsiteSettings", "Read", "Ver y listar en Configuración del Sitio", "Configuración" },
                    { "WebsiteSettings", "Write", "Editar y actualizar en Configuración del Sitio", "Configuración" },
                    { "WebsiteSettings", "Create", "Crear nuevos registros en Configuración del Sitio", "Configuración" },
                    { "WelcomeMessage", "Read", "Ver y listar en Mensaje de Bienvenida", "Configuración" },
                    { "WelcomeMessage", "Write", "Editar y actualizar en Mensaje de Bienvenida", "Configuración" },
                    { "WelcomeMessage", "Create", "Crear nuevos registros en Mensaje de Bienvenida", "Configuración" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumns: new[] { "ModuleName", "ActionName" },
                keyValues: new object[,]
                {
                    { "Dashboard", "Read" },
                    { "Dashboard", "Write" },
                    { "Dashboard", "Create" },
                    { "Topics", "Read" },
                    { "Topics", "Write" },
                    { "Topics", "Create" },
                    { "VideoManagement", "Read" },
                    { "VideoManagement", "Write" },
                    { "VideoManagement", "Create" },
                    { "Customers", "Read" },
                    { "Customers", "Write" },
                    { "Customers", "Create" },
                    { "Users", "Read" },
                    { "Users", "Write" },
                    { "Users", "Create" },
                    { "Roles", "Read" },
                    { "Roles", "Write" },
                    { "Roles", "Create" },
                    { "Instructors", "Read" },
                    { "Instructors", "Write" },
                    { "Instructors", "Create" },
                    { "ToolboxAcademy", "Read" },
                    { "ToolboxAcademy", "Write" },
                    { "ToolboxAcademy", "Create" },
                    { "WheelOfLife", "Read" },
                    { "WheelOfLife", "Write" },
                    { "WheelOfLife", "Create" },
                    { "WheelOfProgress", "Read" },
                    { "WheelOfProgress", "Write" },
                    { "WheelOfProgress", "Create" },
                    { "XRayLife", "Read" },
                    { "XRayLife", "Write" },
                    { "XRayLife", "Create" },
                    { "LifeAssessment", "Read" },
                    { "LifeAssessment", "Write" },
                    { "LifeAssessment", "Create" },
                    { "LifeAreas", "Read" },
                    { "LifeAreas", "Write" },
                    { "LifeAreas", "Create" },
                    { "Tasks", "Read" },
                    { "Tasks", "Write" },
                    { "Tasks", "Create" },
                    { "HabitTracker", "Read" },
                    { "HabitTracker", "Write" },
                    { "HabitTracker", "Create" },
                    { "EmailContents", "Read" },
                    { "EmailContents", "Write" },
                    { "EmailContents", "Create" },
                    { "WebsiteSettings", "Read" },
                    { "WebsiteSettings", "Write" },
                    { "WebsiteSettings", "Create" },
                    { "WelcomeMessage", "Read" },
                    { "WelcomeMessage", "Write" },
                    { "WelcomeMessage", "Create" }
                });
        }
    }
}
