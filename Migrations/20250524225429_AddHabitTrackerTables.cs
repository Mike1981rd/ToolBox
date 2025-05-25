using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ToolBox.Migrations
{
    /// <inheritdoc />
    public partial class AddHabitTrackerTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CategoriasHabitos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Nombre = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    Descripcion = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: true),
                    IconClass = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    Color = table.Column<string>(type: "character varying(7)", maxLength: 7, nullable: false),
                    OrdenVisualizacion = table.Column<int>(type: "integer", nullable: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    FechaCreacion = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CategoriasHabitos", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "FrecuenciasHabitos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Nombre = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    Descripcion = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: true),
                    DiasIntervalo = table.Column<int>(type: "integer", nullable: false),
                    OrdenVisualizacion = table.Column<int>(type: "integer", nullable: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    FechaCreacion = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FrecuenciasHabitos", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Habitos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UsuarioId = table.Column<int>(type: "integer", nullable: false),
                    Nombre = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Descripcion = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    Color = table.Column<string>(type: "character varying(7)", maxLength: 7, nullable: false),
                    CategoriaHabitoId = table.Column<int>(type: "integer", nullable: false),
                    FrecuenciaHabitoId = table.Column<int>(type: "integer", nullable: false),
                    HabilitarRecordatorios = table.Column<bool>(type: "boolean", nullable: false),
                    FechaCreacion = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    FechaActualizacion = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Habitos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Habitos_CategoriasHabitos_CategoriaHabitoId",
                        column: x => x.CategoriaHabitoId,
                        principalTable: "CategoriasHabitos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Habitos_FrecuenciasHabitos_FrecuenciaHabitoId",
                        column: x => x.FrecuenciaHabitoId,
                        principalTable: "FrecuenciasHabitos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Habitos_Users_UsuarioId",
                        column: x => x.UsuarioId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RegistrosCumplimientoHabitos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    HabitoId = table.Column<int>(type: "integer", nullable: false),
                    Fecha = table.Column<DateTime>(type: "date", nullable: false),
                    Cumplido = table.Column<bool>(type: "boolean", nullable: false),
                    FechaRegistro = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RegistrosCumplimientoHabitos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RegistrosCumplimientoHabitos_Habitos_HabitoId",
                        column: x => x.HabitoId,
                        principalTable: "Habitos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.UpdateData(
                table: "AreasProgreso",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2025, 5, 24, 22, 54, 28, 616, DateTimeKind.Utc).AddTicks(438));

            migrationBuilder.UpdateData(
                table: "AreasProgreso",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2025, 5, 24, 22, 54, 28, 616, DateTimeKind.Utc).AddTicks(438));

            migrationBuilder.UpdateData(
                table: "AreasProgreso",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedAt",
                value: new DateTime(2025, 5, 24, 22, 54, 28, 616, DateTimeKind.Utc).AddTicks(438));

            migrationBuilder.UpdateData(
                table: "AreasProgreso",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedAt",
                value: new DateTime(2025, 5, 24, 22, 54, 28, 616, DateTimeKind.Utc).AddTicks(438));

            migrationBuilder.UpdateData(
                table: "AreasProgreso",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreatedAt",
                value: new DateTime(2025, 5, 24, 22, 54, 28, 616, DateTimeKind.Utc).AddTicks(438));

            migrationBuilder.InsertData(
                table: "CategoriasHabitos",
                columns: new[] { "Id", "Color", "CreatedAt", "Descripcion", "FechaCreacion", "IconClass", "IsActive", "Nombre", "OrdenVisualizacion" },
                values: new object[,]
                {
                    { 1, "#e74c3c", new DateTime(2025, 5, 24, 22, 54, 28, 617, DateTimeKind.Utc).AddTicks(839), "Hábitos relacionados con bienestar físico y mental", new DateTime(2025, 5, 24, 22, 54, 28, 617, DateTimeKind.Utc).AddTicks(843), "fas fa-heartbeat", true, "Salud", 1 },
                    { 2, "#3498db", new DateTime(2025, 5, 24, 22, 54, 28, 617, DateTimeKind.Utc).AddTicks(839), "Hábitos que mejoran el rendimiento y eficiencia", new DateTime(2025, 5, 24, 22, 54, 28, 617, DateTimeKind.Utc).AddTicks(849), "fas fa-chart-line", true, "Productividad", 2 },
                    { 3, "#f39c12", new DateTime(2025, 5, 24, 22, 54, 28, 617, DateTimeKind.Utc).AddTicks(839), "Hábitos de educación y desarrollo personal", new DateTime(2025, 5, 24, 22, 54, 28, 617, DateTimeKind.Utc).AddTicks(851), "fas fa-graduation-cap", true, "Aprendizaje", 3 },
                    { 4, "#27ae60", new DateTime(2025, 5, 24, 22, 54, 28, 617, DateTimeKind.Utc).AddTicks(839), "Hábitos de meditación y atención plena", new DateTime(2025, 5, 24, 22, 54, 28, 617, DateTimeKind.Utc).AddTicks(853), "fas fa-leaf", true, "Mindfulness", 4 },
                    { 5, "#9b59b6", new DateTime(2025, 5, 24, 22, 54, 28, 617, DateTimeKind.Utc).AddTicks(839), "Hábitos relacionados con relaciones y vida social", new DateTime(2025, 5, 24, 22, 54, 28, 617, DateTimeKind.Utc).AddTicks(855), "fas fa-users", true, "Social", 5 },
                    { 6, "#e67e22", new DateTime(2025, 5, 24, 22, 54, 28, 617, DateTimeKind.Utc).AddTicks(839), "Hábitos artísticos y de expresión creativa", new DateTime(2025, 5, 24, 22, 54, 28, 617, DateTimeKind.Utc).AddTicks(857), "fas fa-palette", true, "Creatividad", 6 },
                    { 7, "#16a085", new DateTime(2025, 5, 24, 22, 54, 28, 617, DateTimeKind.Utc).AddTicks(839), "Hábitos de manejo financiero y ahorro", new DateTime(2025, 5, 24, 22, 54, 28, 617, DateTimeKind.Utc).AddTicks(859), "fas fa-dollar-sign", true, "Finanzas", 7 },
                    { 8, "#95a5a6", new DateTime(2025, 5, 24, 22, 54, 28, 617, DateTimeKind.Utc).AddTicks(839), "Hábitos de organización y cuidado del hogar", new DateTime(2025, 5, 24, 22, 54, 28, 617, DateTimeKind.Utc).AddTicks(861), "fas fa-home", true, "Hogar", 8 }
                });

            migrationBuilder.UpdateData(
                table: "CategoriasProgreso",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2025, 5, 24, 22, 54, 28, 616, DateTimeKind.Utc).AddTicks(438));

            migrationBuilder.UpdateData(
                table: "CategoriasProgreso",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2025, 5, 24, 22, 54, 28, 616, DateTimeKind.Utc).AddTicks(438));

            migrationBuilder.UpdateData(
                table: "CategoriasProgreso",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedAt",
                value: new DateTime(2025, 5, 24, 22, 54, 28, 616, DateTimeKind.Utc).AddTicks(438));

            migrationBuilder.UpdateData(
                table: "CategoriasProgreso",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedAt",
                value: new DateTime(2025, 5, 24, 22, 54, 28, 616, DateTimeKind.Utc).AddTicks(438));

            migrationBuilder.UpdateData(
                table: "CategoriasProgreso",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreatedAt",
                value: new DateTime(2025, 5, 24, 22, 54, 28, 616, DateTimeKind.Utc).AddTicks(438));

            migrationBuilder.UpdateData(
                table: "CategoriasProgreso",
                keyColumn: "Id",
                keyValue: 6,
                column: "CreatedAt",
                value: new DateTime(2025, 5, 24, 22, 54, 28, 616, DateTimeKind.Utc).AddTicks(438));

            migrationBuilder.UpdateData(
                table: "CategoriasProgreso",
                keyColumn: "Id",
                keyValue: 7,
                column: "CreatedAt",
                value: new DateTime(2025, 5, 24, 22, 54, 28, 616, DateTimeKind.Utc).AddTicks(438));

            migrationBuilder.UpdateData(
                table: "CategoriasProgreso",
                keyColumn: "Id",
                keyValue: 8,
                column: "CreatedAt",
                value: new DateTime(2025, 5, 24, 22, 54, 28, 616, DateTimeKind.Utc).AddTicks(438));

            migrationBuilder.UpdateData(
                table: "CategoriasProgreso",
                keyColumn: "Id",
                keyValue: 9,
                column: "CreatedAt",
                value: new DateTime(2025, 5, 24, 22, 54, 28, 616, DateTimeKind.Utc).AddTicks(438));

            migrationBuilder.UpdateData(
                table: "CategoriasProgreso",
                keyColumn: "Id",
                keyValue: 10,
                column: "CreatedAt",
                value: new DateTime(2025, 5, 24, 22, 54, 28, 616, DateTimeKind.Utc).AddTicks(438));

            migrationBuilder.UpdateData(
                table: "CategoriasProgreso",
                keyColumn: "Id",
                keyValue: 11,
                column: "CreatedAt",
                value: new DateTime(2025, 5, 24, 22, 54, 28, 616, DateTimeKind.Utc).AddTicks(438));

            migrationBuilder.UpdateData(
                table: "CategoriasProgreso",
                keyColumn: "Id",
                keyValue: 12,
                column: "CreatedAt",
                value: new DateTime(2025, 5, 24, 22, 54, 28, 616, DateTimeKind.Utc).AddTicks(438));

            migrationBuilder.UpdateData(
                table: "CategoriasProgreso",
                keyColumn: "Id",
                keyValue: 13,
                column: "CreatedAt",
                value: new DateTime(2025, 5, 24, 22, 54, 28, 616, DateTimeKind.Utc).AddTicks(438));

            migrationBuilder.UpdateData(
                table: "CategoriasProgreso",
                keyColumn: "Id",
                keyValue: 14,
                column: "CreatedAt",
                value: new DateTime(2025, 5, 24, 22, 54, 28, 616, DateTimeKind.Utc).AddTicks(438));

            migrationBuilder.UpdateData(
                table: "CategoriasProgreso",
                keyColumn: "Id",
                keyValue: 15,
                column: "CreatedAt",
                value: new DateTime(2025, 5, 24, 22, 54, 28, 616, DateTimeKind.Utc).AddTicks(438));

            migrationBuilder.InsertData(
                table: "FrecuenciasHabitos",
                columns: new[] { "Id", "CreatedAt", "Descripcion", "DiasIntervalo", "FechaCreacion", "IsActive", "Nombre", "OrdenVisualizacion" },
                values: new object[,]
                {
                    { 1, new DateTime(2025, 5, 24, 22, 54, 28, 617, DateTimeKind.Utc).AddTicks(839), "Todos los días", 1, new DateTime(2025, 5, 24, 22, 54, 28, 617, DateTimeKind.Utc).AddTicks(865), true, "Diario", 1 },
                    { 2, new DateTime(2025, 5, 24, 22, 54, 28, 617, DateTimeKind.Utc).AddTicks(839), "Una vez por semana", 7, new DateTime(2025, 5, 24, 22, 54, 28, 617, DateTimeKind.Utc).AddTicks(871), true, "Semanal", 2 },
                    { 3, new DateTime(2025, 5, 24, 22, 54, 28, 617, DateTimeKind.Utc).AddTicks(839), "Lunes, miércoles y viernes", 2, new DateTime(2025, 5, 24, 22, 54, 28, 617, DateTimeKind.Utc).AddTicks(874), true, "3 veces por semana", 3 },
                    { 4, new DateTime(2025, 5, 24, 22, 54, 28, 617, DateTimeKind.Utc).AddTicks(839), "Sábados y domingos", 7, new DateTime(2025, 5, 24, 22, 54, 28, 617, DateTimeKind.Utc).AddTicks(875), true, "Fines de semana", 4 },
                    { 5, new DateTime(2025, 5, 24, 22, 54, 28, 617, DateTimeKind.Utc).AddTicks(839), "Lunes a viernes", 1, new DateTime(2025, 5, 24, 22, 54, 28, 617, DateTimeKind.Utc).AddTicks(877), true, "Días laborales", 5 },
                    { 6, new DateTime(2025, 5, 24, 22, 54, 28, 617, DateTimeKind.Utc).AddTicks(839), "Una vez al mes", 30, new DateTime(2025, 5, 24, 22, 54, 28, 617, DateTimeKind.Utc).AddTicks(880), true, "Mensual", 6 }
                });

            migrationBuilder.UpdateData(
                table: "LifeAreas",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 5, 24, 22, 54, 28, 614, DateTimeKind.Utc).AddTicks(6407), new DateTime(2025, 5, 24, 22, 54, 28, 614, DateTimeKind.Utc).AddTicks(6407) });

            migrationBuilder.UpdateData(
                table: "LifeAreas",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 5, 24, 22, 54, 28, 614, DateTimeKind.Utc).AddTicks(6407), new DateTime(2025, 5, 24, 22, 54, 28, 614, DateTimeKind.Utc).AddTicks(6407) });

            migrationBuilder.UpdateData(
                table: "LifeAreas",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 5, 24, 22, 54, 28, 614, DateTimeKind.Utc).AddTicks(6407), new DateTime(2025, 5, 24, 22, 54, 28, 614, DateTimeKind.Utc).AddTicks(6407) });

            migrationBuilder.UpdateData(
                table: "LifeAreas",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 5, 24, 22, 54, 28, 614, DateTimeKind.Utc).AddTicks(6407), new DateTime(2025, 5, 24, 22, 54, 28, 614, DateTimeKind.Utc).AddTicks(6407) });

            migrationBuilder.UpdateData(
                table: "LifeAreas",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 5, 24, 22, 54, 28, 614, DateTimeKind.Utc).AddTicks(6407), new DateTime(2025, 5, 24, 22, 54, 28, 614, DateTimeKind.Utc).AddTicks(6407) });

            migrationBuilder.UpdateData(
                table: "LifeAreas",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 5, 24, 22, 54, 28, 614, DateTimeKind.Utc).AddTicks(6407), new DateTime(2025, 5, 24, 22, 54, 28, 614, DateTimeKind.Utc).AddTicks(6407) });

            migrationBuilder.UpdateData(
                table: "LifeAreas",
                keyColumn: "Id",
                keyValue: 7,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 5, 24, 22, 54, 28, 614, DateTimeKind.Utc).AddTicks(6407), new DateTime(2025, 5, 24, 22, 54, 28, 614, DateTimeKind.Utc).AddTicks(6407) });

            migrationBuilder.UpdateData(
                table: "LifeAreas",
                keyColumn: "Id",
                keyValue: 8,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 5, 24, 22, 54, 28, 614, DateTimeKind.Utc).AddTicks(6407), new DateTime(2025, 5, 24, 22, 54, 28, 614, DateTimeKind.Utc).AddTicks(6407) });

            migrationBuilder.UpdateData(
                table: "LifeAreas",
                keyColumn: "Id",
                keyValue: 9,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 5, 24, 22, 54, 28, 614, DateTimeKind.Utc).AddTicks(6407), new DateTime(2025, 5, 24, 22, 54, 28, 614, DateTimeKind.Utc).AddTicks(6407) });

            migrationBuilder.UpdateData(
                table: "LifeAreas",
                keyColumn: "Id",
                keyValue: 10,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 5, 24, 22, 54, 28, 614, DateTimeKind.Utc).AddTicks(6407), new DateTime(2025, 5, 24, 22, 54, 28, 614, DateTimeKind.Utc).AddTicks(6407) });

            migrationBuilder.UpdateData(
                table: "Questions",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 5, 24, 22, 54, 28, 616, DateTimeKind.Utc).AddTicks(346), new DateTime(2025, 5, 24, 22, 54, 28, 616, DateTimeKind.Utc).AddTicks(346) });

            migrationBuilder.UpdateData(
                table: "Questions",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 5, 24, 22, 54, 28, 616, DateTimeKind.Utc).AddTicks(346), new DateTime(2025, 5, 24, 22, 54, 28, 616, DateTimeKind.Utc).AddTicks(346) });

            migrationBuilder.UpdateData(
                table: "Questions",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 5, 24, 22, 54, 28, 616, DateTimeKind.Utc).AddTicks(346), new DateTime(2025, 5, 24, 22, 54, 28, 616, DateTimeKind.Utc).AddTicks(346) });

            migrationBuilder.UpdateData(
                table: "Questions",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 5, 24, 22, 54, 28, 616, DateTimeKind.Utc).AddTicks(346), new DateTime(2025, 5, 24, 22, 54, 28, 616, DateTimeKind.Utc).AddTicks(346) });

            migrationBuilder.UpdateData(
                table: "Questions",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 5, 24, 22, 54, 28, 616, DateTimeKind.Utc).AddTicks(346), new DateTime(2025, 5, 24, 22, 54, 28, 616, DateTimeKind.Utc).AddTicks(346) });

            migrationBuilder.UpdateData(
                table: "Questions",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 5, 24, 22, 54, 28, 616, DateTimeKind.Utc).AddTicks(346), new DateTime(2025, 5, 24, 22, 54, 28, 616, DateTimeKind.Utc).AddTicks(346) });

            migrationBuilder.UpdateData(
                table: "Questions",
                keyColumn: "Id",
                keyValue: 7,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 5, 24, 22, 54, 28, 616, DateTimeKind.Utc).AddTicks(346), new DateTime(2025, 5, 24, 22, 54, 28, 616, DateTimeKind.Utc).AddTicks(346) });

            migrationBuilder.UpdateData(
                table: "Questions",
                keyColumn: "Id",
                keyValue: 8,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 5, 24, 22, 54, 28, 616, DateTimeKind.Utc).AddTicks(346), new DateTime(2025, 5, 24, 22, 54, 28, 616, DateTimeKind.Utc).AddTicks(346) });

            migrationBuilder.UpdateData(
                table: "Questions",
                keyColumn: "Id",
                keyValue: 9,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 5, 24, 22, 54, 28, 616, DateTimeKind.Utc).AddTicks(346), new DateTime(2025, 5, 24, 22, 54, 28, 616, DateTimeKind.Utc).AddTicks(346) });

            migrationBuilder.UpdateData(
                table: "Questions",
                keyColumn: "Id",
                keyValue: 10,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 5, 24, 22, 54, 28, 616, DateTimeKind.Utc).AddTicks(346), new DateTime(2025, 5, 24, 22, 54, 28, 616, DateTimeKind.Utc).AddTicks(346) });

            migrationBuilder.CreateIndex(
                name: "IX_CategoriasHabitos_OrdenVisualizacion",
                table: "CategoriasHabitos",
                column: "OrdenVisualizacion");

            migrationBuilder.CreateIndex(
                name: "IX_FrecuenciasHabitos_OrdenVisualizacion",
                table: "FrecuenciasHabitos",
                column: "OrdenVisualizacion");

            migrationBuilder.CreateIndex(
                name: "IX_Habitos_CategoriaHabitoId",
                table: "Habitos",
                column: "CategoriaHabitoId");

            migrationBuilder.CreateIndex(
                name: "IX_Habitos_FrecuenciaHabitoId",
                table: "Habitos",
                column: "FrecuenciaHabitoId");

            migrationBuilder.CreateIndex(
                name: "IX_Habitos_UsuarioId_FechaCreacion",
                table: "Habitos",
                columns: new[] { "UsuarioId", "FechaCreacion" });

            migrationBuilder.CreateIndex(
                name: "IX_RegistrosCumplimientoHabitos_HabitoId_Fecha",
                table: "RegistrosCumplimientoHabitos",
                columns: new[] { "HabitoId", "Fecha" },
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RegistrosCumplimientoHabitos");

            migrationBuilder.DropTable(
                name: "Habitos");

            migrationBuilder.DropTable(
                name: "CategoriasHabitos");

            migrationBuilder.DropTable(
                name: "FrecuenciasHabitos");

            migrationBuilder.UpdateData(
                table: "AreasProgreso",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2025, 5, 24, 21, 55, 51, 498, DateTimeKind.Utc).AddTicks(2326));

            migrationBuilder.UpdateData(
                table: "AreasProgreso",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2025, 5, 24, 21, 55, 51, 498, DateTimeKind.Utc).AddTicks(2326));

            migrationBuilder.UpdateData(
                table: "AreasProgreso",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedAt",
                value: new DateTime(2025, 5, 24, 21, 55, 51, 498, DateTimeKind.Utc).AddTicks(2326));

            migrationBuilder.UpdateData(
                table: "AreasProgreso",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedAt",
                value: new DateTime(2025, 5, 24, 21, 55, 51, 498, DateTimeKind.Utc).AddTicks(2326));

            migrationBuilder.UpdateData(
                table: "AreasProgreso",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreatedAt",
                value: new DateTime(2025, 5, 24, 21, 55, 51, 498, DateTimeKind.Utc).AddTicks(2326));

            migrationBuilder.UpdateData(
                table: "CategoriasProgreso",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2025, 5, 24, 21, 55, 51, 498, DateTimeKind.Utc).AddTicks(2326));

            migrationBuilder.UpdateData(
                table: "CategoriasProgreso",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2025, 5, 24, 21, 55, 51, 498, DateTimeKind.Utc).AddTicks(2326));

            migrationBuilder.UpdateData(
                table: "CategoriasProgreso",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedAt",
                value: new DateTime(2025, 5, 24, 21, 55, 51, 498, DateTimeKind.Utc).AddTicks(2326));

            migrationBuilder.UpdateData(
                table: "CategoriasProgreso",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedAt",
                value: new DateTime(2025, 5, 24, 21, 55, 51, 498, DateTimeKind.Utc).AddTicks(2326));

            migrationBuilder.UpdateData(
                table: "CategoriasProgreso",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreatedAt",
                value: new DateTime(2025, 5, 24, 21, 55, 51, 498, DateTimeKind.Utc).AddTicks(2326));

            migrationBuilder.UpdateData(
                table: "CategoriasProgreso",
                keyColumn: "Id",
                keyValue: 6,
                column: "CreatedAt",
                value: new DateTime(2025, 5, 24, 21, 55, 51, 498, DateTimeKind.Utc).AddTicks(2326));

            migrationBuilder.UpdateData(
                table: "CategoriasProgreso",
                keyColumn: "Id",
                keyValue: 7,
                column: "CreatedAt",
                value: new DateTime(2025, 5, 24, 21, 55, 51, 498, DateTimeKind.Utc).AddTicks(2326));

            migrationBuilder.UpdateData(
                table: "CategoriasProgreso",
                keyColumn: "Id",
                keyValue: 8,
                column: "CreatedAt",
                value: new DateTime(2025, 5, 24, 21, 55, 51, 498, DateTimeKind.Utc).AddTicks(2326));

            migrationBuilder.UpdateData(
                table: "CategoriasProgreso",
                keyColumn: "Id",
                keyValue: 9,
                column: "CreatedAt",
                value: new DateTime(2025, 5, 24, 21, 55, 51, 498, DateTimeKind.Utc).AddTicks(2326));

            migrationBuilder.UpdateData(
                table: "CategoriasProgreso",
                keyColumn: "Id",
                keyValue: 10,
                column: "CreatedAt",
                value: new DateTime(2025, 5, 24, 21, 55, 51, 498, DateTimeKind.Utc).AddTicks(2326));

            migrationBuilder.UpdateData(
                table: "CategoriasProgreso",
                keyColumn: "Id",
                keyValue: 11,
                column: "CreatedAt",
                value: new DateTime(2025, 5, 24, 21, 55, 51, 498, DateTimeKind.Utc).AddTicks(2326));

            migrationBuilder.UpdateData(
                table: "CategoriasProgreso",
                keyColumn: "Id",
                keyValue: 12,
                column: "CreatedAt",
                value: new DateTime(2025, 5, 24, 21, 55, 51, 498, DateTimeKind.Utc).AddTicks(2326));

            migrationBuilder.UpdateData(
                table: "CategoriasProgreso",
                keyColumn: "Id",
                keyValue: 13,
                column: "CreatedAt",
                value: new DateTime(2025, 5, 24, 21, 55, 51, 498, DateTimeKind.Utc).AddTicks(2326));

            migrationBuilder.UpdateData(
                table: "CategoriasProgreso",
                keyColumn: "Id",
                keyValue: 14,
                column: "CreatedAt",
                value: new DateTime(2025, 5, 24, 21, 55, 51, 498, DateTimeKind.Utc).AddTicks(2326));

            migrationBuilder.UpdateData(
                table: "CategoriasProgreso",
                keyColumn: "Id",
                keyValue: 15,
                column: "CreatedAt",
                value: new DateTime(2025, 5, 24, 21, 55, 51, 498, DateTimeKind.Utc).AddTicks(2326));

            migrationBuilder.UpdateData(
                table: "LifeAreas",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 5, 24, 21, 55, 51, 496, DateTimeKind.Utc).AddTicks(5634), new DateTime(2025, 5, 24, 21, 55, 51, 496, DateTimeKind.Utc).AddTicks(5634) });

            migrationBuilder.UpdateData(
                table: "LifeAreas",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 5, 24, 21, 55, 51, 496, DateTimeKind.Utc).AddTicks(5634), new DateTime(2025, 5, 24, 21, 55, 51, 496, DateTimeKind.Utc).AddTicks(5634) });

            migrationBuilder.UpdateData(
                table: "LifeAreas",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 5, 24, 21, 55, 51, 496, DateTimeKind.Utc).AddTicks(5634), new DateTime(2025, 5, 24, 21, 55, 51, 496, DateTimeKind.Utc).AddTicks(5634) });

            migrationBuilder.UpdateData(
                table: "LifeAreas",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 5, 24, 21, 55, 51, 496, DateTimeKind.Utc).AddTicks(5634), new DateTime(2025, 5, 24, 21, 55, 51, 496, DateTimeKind.Utc).AddTicks(5634) });

            migrationBuilder.UpdateData(
                table: "LifeAreas",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 5, 24, 21, 55, 51, 496, DateTimeKind.Utc).AddTicks(5634), new DateTime(2025, 5, 24, 21, 55, 51, 496, DateTimeKind.Utc).AddTicks(5634) });

            migrationBuilder.UpdateData(
                table: "LifeAreas",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 5, 24, 21, 55, 51, 496, DateTimeKind.Utc).AddTicks(5634), new DateTime(2025, 5, 24, 21, 55, 51, 496, DateTimeKind.Utc).AddTicks(5634) });

            migrationBuilder.UpdateData(
                table: "LifeAreas",
                keyColumn: "Id",
                keyValue: 7,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 5, 24, 21, 55, 51, 496, DateTimeKind.Utc).AddTicks(5634), new DateTime(2025, 5, 24, 21, 55, 51, 496, DateTimeKind.Utc).AddTicks(5634) });

            migrationBuilder.UpdateData(
                table: "LifeAreas",
                keyColumn: "Id",
                keyValue: 8,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 5, 24, 21, 55, 51, 496, DateTimeKind.Utc).AddTicks(5634), new DateTime(2025, 5, 24, 21, 55, 51, 496, DateTimeKind.Utc).AddTicks(5634) });

            migrationBuilder.UpdateData(
                table: "LifeAreas",
                keyColumn: "Id",
                keyValue: 9,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 5, 24, 21, 55, 51, 496, DateTimeKind.Utc).AddTicks(5634), new DateTime(2025, 5, 24, 21, 55, 51, 496, DateTimeKind.Utc).AddTicks(5634) });

            migrationBuilder.UpdateData(
                table: "LifeAreas",
                keyColumn: "Id",
                keyValue: 10,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 5, 24, 21, 55, 51, 496, DateTimeKind.Utc).AddTicks(5634), new DateTime(2025, 5, 24, 21, 55, 51, 496, DateTimeKind.Utc).AddTicks(5634) });

            migrationBuilder.UpdateData(
                table: "Questions",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 5, 24, 21, 55, 51, 498, DateTimeKind.Utc).AddTicks(2124), new DateTime(2025, 5, 24, 21, 55, 51, 498, DateTimeKind.Utc).AddTicks(2124) });

            migrationBuilder.UpdateData(
                table: "Questions",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 5, 24, 21, 55, 51, 498, DateTimeKind.Utc).AddTicks(2124), new DateTime(2025, 5, 24, 21, 55, 51, 498, DateTimeKind.Utc).AddTicks(2124) });

            migrationBuilder.UpdateData(
                table: "Questions",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 5, 24, 21, 55, 51, 498, DateTimeKind.Utc).AddTicks(2124), new DateTime(2025, 5, 24, 21, 55, 51, 498, DateTimeKind.Utc).AddTicks(2124) });

            migrationBuilder.UpdateData(
                table: "Questions",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 5, 24, 21, 55, 51, 498, DateTimeKind.Utc).AddTicks(2124), new DateTime(2025, 5, 24, 21, 55, 51, 498, DateTimeKind.Utc).AddTicks(2124) });

            migrationBuilder.UpdateData(
                table: "Questions",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 5, 24, 21, 55, 51, 498, DateTimeKind.Utc).AddTicks(2124), new DateTime(2025, 5, 24, 21, 55, 51, 498, DateTimeKind.Utc).AddTicks(2124) });

            migrationBuilder.UpdateData(
                table: "Questions",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 5, 24, 21, 55, 51, 498, DateTimeKind.Utc).AddTicks(2124), new DateTime(2025, 5, 24, 21, 55, 51, 498, DateTimeKind.Utc).AddTicks(2124) });

            migrationBuilder.UpdateData(
                table: "Questions",
                keyColumn: "Id",
                keyValue: 7,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 5, 24, 21, 55, 51, 498, DateTimeKind.Utc).AddTicks(2124), new DateTime(2025, 5, 24, 21, 55, 51, 498, DateTimeKind.Utc).AddTicks(2124) });

            migrationBuilder.UpdateData(
                table: "Questions",
                keyColumn: "Id",
                keyValue: 8,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 5, 24, 21, 55, 51, 498, DateTimeKind.Utc).AddTicks(2124), new DateTime(2025, 5, 24, 21, 55, 51, 498, DateTimeKind.Utc).AddTicks(2124) });

            migrationBuilder.UpdateData(
                table: "Questions",
                keyColumn: "Id",
                keyValue: 9,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 5, 24, 21, 55, 51, 498, DateTimeKind.Utc).AddTicks(2124), new DateTime(2025, 5, 24, 21, 55, 51, 498, DateTimeKind.Utc).AddTicks(2124) });

            migrationBuilder.UpdateData(
                table: "Questions",
                keyColumn: "Id",
                keyValue: 10,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 5, 24, 21, 55, 51, 498, DateTimeKind.Utc).AddTicks(2124), new DateTime(2025, 5, 24, 21, 55, 51, 498, DateTimeKind.Utc).AddTicks(2124) });
        }
    }
}
