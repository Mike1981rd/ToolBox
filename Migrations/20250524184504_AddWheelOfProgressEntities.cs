using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ToolBox.Migrations
{
    /// <inheritdoc />
    public partial class AddWheelOfProgressEntities : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AreasProgreso",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Nombre = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Descripcion = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    IconClass = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    IconColor = table.Column<string>(type: "character varying(7)", maxLength: 7, nullable: false),
                    OrdenVisualizacion = table.Column<int>(type: "integer", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AreasProgreso", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CategoriasProgreso",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Nombre = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Descripcion = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    AreaProgresoId = table.Column<int>(type: "integer", nullable: false),
                    OrdenVisualizacion = table.Column<int>(type: "integer", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CategoriasProgreso", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CategoriasProgreso_AreasProgreso_AreaProgresoId",
                        column: x => x.AreaProgresoId,
                        principalTable: "AreasProgreso",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProgresosMetasUsuarios",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UserId = table.Column<int>(type: "integer", nullable: false),
                    CategoriaProgresoId = table.Column<int>(type: "integer", nullable: false),
                    Meta = table.Column<string>(type: "character varying(1000)", maxLength: 1000, nullable: true),
                    PorcentajeProgreso = table.Column<int>(type: "integer", nullable: false),
                    SiguienteAccion = table.Column<string>(type: "character varying(1000)", maxLength: 1000, nullable: true),
                    FechaLimite = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProgresosMetasUsuarios", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProgresosMetasUsuarios_CategoriasProgreso_CategoriaProgreso~",
                        column: x => x.CategoriaProgresoId,
                        principalTable: "CategoriasProgreso",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProgresosMetasUsuarios_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "AreasProgreso",
                columns: new[] { "Id", "CreatedAt", "Descripcion", "IconClass", "IconColor", "Nombre", "OrdenVisualizacion" },
                values: new object[,]
                {
                    { 1, new DateTime(2025, 5, 24, 18, 45, 3, 823, DateTimeKind.Utc).AddTicks(3607), "Carrera, negocios y crecimiento profesional", "fas fa-briefcase", "#2c3e50", "Vida Empresarial", 1 },
                    { 2, new DateTime(2025, 5, 24, 18, 45, 3, 823, DateTimeKind.Utc).AddTicks(3607), "Expresión artística y creatividad", "fas fa-palette", "#e74c3c", "Vida Creativa", 2 },
                    { 3, new DateTime(2025, 5, 24, 18, 45, 3, 823, DateTimeKind.Utc).AddTicks(3607), "Relaciones con amigos y comunidad", "fas fa-users", "#3498db", "Vida Social", 3 },
                    { 4, new DateTime(2025, 5, 24, 18, 45, 3, 823, DateTimeKind.Utc).AddTicks(3607), "Relaciones románticas y pareja", "fas fa-heart", "#e91e63", "Vida Amorosa", 4 },
                    { 5, new DateTime(2025, 5, 24, 18, 45, 3, 823, DateTimeKind.Utc).AddTicks(3607), "Misión personal y espiritualidad", "fas fa-compass", "#9b59b6", "Propósito de Vida", 5 }
                });

            migrationBuilder.UpdateData(
                table: "LifeAreas",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 5, 24, 18, 45, 3, 821, DateTimeKind.Utc).AddTicks(8730), new DateTime(2025, 5, 24, 18, 45, 3, 821, DateTimeKind.Utc).AddTicks(8730) });

            migrationBuilder.UpdateData(
                table: "LifeAreas",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 5, 24, 18, 45, 3, 821, DateTimeKind.Utc).AddTicks(8730), new DateTime(2025, 5, 24, 18, 45, 3, 821, DateTimeKind.Utc).AddTicks(8730) });

            migrationBuilder.UpdateData(
                table: "LifeAreas",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 5, 24, 18, 45, 3, 821, DateTimeKind.Utc).AddTicks(8730), new DateTime(2025, 5, 24, 18, 45, 3, 821, DateTimeKind.Utc).AddTicks(8730) });

            migrationBuilder.UpdateData(
                table: "LifeAreas",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 5, 24, 18, 45, 3, 821, DateTimeKind.Utc).AddTicks(8730), new DateTime(2025, 5, 24, 18, 45, 3, 821, DateTimeKind.Utc).AddTicks(8730) });

            migrationBuilder.UpdateData(
                table: "LifeAreas",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 5, 24, 18, 45, 3, 821, DateTimeKind.Utc).AddTicks(8730), new DateTime(2025, 5, 24, 18, 45, 3, 821, DateTimeKind.Utc).AddTicks(8730) });

            migrationBuilder.UpdateData(
                table: "LifeAreas",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 5, 24, 18, 45, 3, 821, DateTimeKind.Utc).AddTicks(8730), new DateTime(2025, 5, 24, 18, 45, 3, 821, DateTimeKind.Utc).AddTicks(8730) });

            migrationBuilder.UpdateData(
                table: "LifeAreas",
                keyColumn: "Id",
                keyValue: 7,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 5, 24, 18, 45, 3, 821, DateTimeKind.Utc).AddTicks(8730), new DateTime(2025, 5, 24, 18, 45, 3, 821, DateTimeKind.Utc).AddTicks(8730) });

            migrationBuilder.UpdateData(
                table: "LifeAreas",
                keyColumn: "Id",
                keyValue: 8,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 5, 24, 18, 45, 3, 821, DateTimeKind.Utc).AddTicks(8730), new DateTime(2025, 5, 24, 18, 45, 3, 821, DateTimeKind.Utc).AddTicks(8730) });

            migrationBuilder.UpdateData(
                table: "LifeAreas",
                keyColumn: "Id",
                keyValue: 9,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 5, 24, 18, 45, 3, 821, DateTimeKind.Utc).AddTicks(8730), new DateTime(2025, 5, 24, 18, 45, 3, 821, DateTimeKind.Utc).AddTicks(8730) });

            migrationBuilder.UpdateData(
                table: "LifeAreas",
                keyColumn: "Id",
                keyValue: 10,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 5, 24, 18, 45, 3, 821, DateTimeKind.Utc).AddTicks(8730), new DateTime(2025, 5, 24, 18, 45, 3, 821, DateTimeKind.Utc).AddTicks(8730) });

            migrationBuilder.UpdateData(
                table: "Questions",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 5, 24, 18, 45, 3, 823, DateTimeKind.Utc).AddTicks(3366), new DateTime(2025, 5, 24, 18, 45, 3, 823, DateTimeKind.Utc).AddTicks(3366) });

            migrationBuilder.UpdateData(
                table: "Questions",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 5, 24, 18, 45, 3, 823, DateTimeKind.Utc).AddTicks(3366), new DateTime(2025, 5, 24, 18, 45, 3, 823, DateTimeKind.Utc).AddTicks(3366) });

            migrationBuilder.UpdateData(
                table: "Questions",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 5, 24, 18, 45, 3, 823, DateTimeKind.Utc).AddTicks(3366), new DateTime(2025, 5, 24, 18, 45, 3, 823, DateTimeKind.Utc).AddTicks(3366) });

            migrationBuilder.UpdateData(
                table: "Questions",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 5, 24, 18, 45, 3, 823, DateTimeKind.Utc).AddTicks(3366), new DateTime(2025, 5, 24, 18, 45, 3, 823, DateTimeKind.Utc).AddTicks(3366) });

            migrationBuilder.UpdateData(
                table: "Questions",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 5, 24, 18, 45, 3, 823, DateTimeKind.Utc).AddTicks(3366), new DateTime(2025, 5, 24, 18, 45, 3, 823, DateTimeKind.Utc).AddTicks(3366) });

            migrationBuilder.UpdateData(
                table: "Questions",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 5, 24, 18, 45, 3, 823, DateTimeKind.Utc).AddTicks(3366), new DateTime(2025, 5, 24, 18, 45, 3, 823, DateTimeKind.Utc).AddTicks(3366) });

            migrationBuilder.UpdateData(
                table: "Questions",
                keyColumn: "Id",
                keyValue: 7,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 5, 24, 18, 45, 3, 823, DateTimeKind.Utc).AddTicks(3366), new DateTime(2025, 5, 24, 18, 45, 3, 823, DateTimeKind.Utc).AddTicks(3366) });

            migrationBuilder.UpdateData(
                table: "Questions",
                keyColumn: "Id",
                keyValue: 8,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 5, 24, 18, 45, 3, 823, DateTimeKind.Utc).AddTicks(3366), new DateTime(2025, 5, 24, 18, 45, 3, 823, DateTimeKind.Utc).AddTicks(3366) });

            migrationBuilder.UpdateData(
                table: "Questions",
                keyColumn: "Id",
                keyValue: 9,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 5, 24, 18, 45, 3, 823, DateTimeKind.Utc).AddTicks(3366), new DateTime(2025, 5, 24, 18, 45, 3, 823, DateTimeKind.Utc).AddTicks(3366) });

            migrationBuilder.UpdateData(
                table: "Questions",
                keyColumn: "Id",
                keyValue: 10,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 5, 24, 18, 45, 3, 823, DateTimeKind.Utc).AddTicks(3366), new DateTime(2025, 5, 24, 18, 45, 3, 823, DateTimeKind.Utc).AddTicks(3366) });

            migrationBuilder.InsertData(
                table: "CategoriasProgreso",
                columns: new[] { "Id", "AreaProgresoId", "CreatedAt", "Descripcion", "Nombre", "OrdenVisualizacion" },
                values: new object[,]
                {
                    { 1, 1, new DateTime(2025, 5, 24, 18, 45, 3, 823, DateTimeKind.Utc).AddTicks(3607), null, "Dinero y Finanzas", 1 },
                    { 2, 1, new DateTime(2025, 5, 24, 18, 45, 3, 823, DateTimeKind.Utc).AddTicks(3607), null, "Carrera y Misión", 2 },
                    { 3, 1, new DateTime(2025, 5, 24, 18, 45, 3, 823, DateTimeKind.Utc).AddTicks(3607), null, "Productividad", 3 },
                    { 4, 2, new DateTime(2025, 5, 24, 18, 45, 3, 823, DateTimeKind.Utc).AddTicks(3607), null, "Arte y Expresión", 1 },
                    { 5, 2, new DateTime(2025, 5, 24, 18, 45, 3, 823, DateTimeKind.Utc).AddTicks(3607), null, "Proyectos Creativos", 2 },
                    { 6, 2, new DateTime(2025, 5, 24, 18, 45, 3, 823, DateTimeKind.Utc).AddTicks(3607), null, "Inspiración", 3 },
                    { 7, 3, new DateTime(2025, 5, 24, 18, 45, 3, 823, DateTimeKind.Utc).AddTicks(3607), null, "Amistades", 1 },
                    { 8, 3, new DateTime(2025, 5, 24, 18, 45, 3, 823, DateTimeKind.Utc).AddTicks(3607), null, "Networking", 2 },
                    { 9, 3, new DateTime(2025, 5, 24, 18, 45, 3, 823, DateTimeKind.Utc).AddTicks(3607), null, "Comunidad", 3 },
                    { 10, 4, new DateTime(2025, 5, 24, 18, 45, 3, 823, DateTimeKind.Utc).AddTicks(3607), null, "Relación de Pareja", 1 },
                    { 11, 4, new DateTime(2025, 5, 24, 18, 45, 3, 823, DateTimeKind.Utc).AddTicks(3607), null, "Familia", 2 },
                    { 12, 4, new DateTime(2025, 5, 24, 18, 45, 3, 823, DateTimeKind.Utc).AddTicks(3607), null, "Amor Propio", 3 },
                    { 13, 5, new DateTime(2025, 5, 24, 18, 45, 3, 823, DateTimeKind.Utc).AddTicks(3607), null, "Espiritualidad", 1 },
                    { 14, 5, new DateTime(2025, 5, 24, 18, 45, 3, 823, DateTimeKind.Utc).AddTicks(3607), null, "Valores y Principios", 2 },
                    { 15, 5, new DateTime(2025, 5, 24, 18, 45, 3, 823, DateTimeKind.Utc).AddTicks(3607), null, "Salud y Fitness", 3 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_AreasProgreso_OrdenVisualizacion",
                table: "AreasProgreso",
                column: "OrdenVisualizacion");

            migrationBuilder.CreateIndex(
                name: "IX_CategoriasProgreso_AreaProgresoId_OrdenVisualizacion",
                table: "CategoriasProgreso",
                columns: new[] { "AreaProgresoId", "OrdenVisualizacion" });

            migrationBuilder.CreateIndex(
                name: "IX_ProgresosMetasUsuarios_CategoriaProgresoId",
                table: "ProgresosMetasUsuarios",
                column: "CategoriaProgresoId");

            migrationBuilder.CreateIndex(
                name: "IX_ProgresosMetasUsuarios_UserId_CategoriaProgresoId",
                table: "ProgresosMetasUsuarios",
                columns: new[] { "UserId", "CategoriaProgresoId" },
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ProgresosMetasUsuarios");

            migrationBuilder.DropTable(
                name: "CategoriasProgreso");

            migrationBuilder.DropTable(
                name: "AreasProgreso");

            migrationBuilder.UpdateData(
                table: "LifeAreas",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 5, 24, 17, 3, 0, 571, DateTimeKind.Utc).AddTicks(9079), new DateTime(2025, 5, 24, 17, 3, 0, 571, DateTimeKind.Utc).AddTicks(9079) });

            migrationBuilder.UpdateData(
                table: "LifeAreas",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 5, 24, 17, 3, 0, 571, DateTimeKind.Utc).AddTicks(9079), new DateTime(2025, 5, 24, 17, 3, 0, 571, DateTimeKind.Utc).AddTicks(9079) });

            migrationBuilder.UpdateData(
                table: "LifeAreas",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 5, 24, 17, 3, 0, 571, DateTimeKind.Utc).AddTicks(9079), new DateTime(2025, 5, 24, 17, 3, 0, 571, DateTimeKind.Utc).AddTicks(9079) });

            migrationBuilder.UpdateData(
                table: "LifeAreas",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 5, 24, 17, 3, 0, 571, DateTimeKind.Utc).AddTicks(9079), new DateTime(2025, 5, 24, 17, 3, 0, 571, DateTimeKind.Utc).AddTicks(9079) });

            migrationBuilder.UpdateData(
                table: "LifeAreas",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 5, 24, 17, 3, 0, 571, DateTimeKind.Utc).AddTicks(9079), new DateTime(2025, 5, 24, 17, 3, 0, 571, DateTimeKind.Utc).AddTicks(9079) });

            migrationBuilder.UpdateData(
                table: "LifeAreas",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 5, 24, 17, 3, 0, 571, DateTimeKind.Utc).AddTicks(9079), new DateTime(2025, 5, 24, 17, 3, 0, 571, DateTimeKind.Utc).AddTicks(9079) });

            migrationBuilder.UpdateData(
                table: "LifeAreas",
                keyColumn: "Id",
                keyValue: 7,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 5, 24, 17, 3, 0, 571, DateTimeKind.Utc).AddTicks(9079), new DateTime(2025, 5, 24, 17, 3, 0, 571, DateTimeKind.Utc).AddTicks(9079) });

            migrationBuilder.UpdateData(
                table: "LifeAreas",
                keyColumn: "Id",
                keyValue: 8,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 5, 24, 17, 3, 0, 571, DateTimeKind.Utc).AddTicks(9079), new DateTime(2025, 5, 24, 17, 3, 0, 571, DateTimeKind.Utc).AddTicks(9079) });

            migrationBuilder.UpdateData(
                table: "LifeAreas",
                keyColumn: "Id",
                keyValue: 9,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 5, 24, 17, 3, 0, 571, DateTimeKind.Utc).AddTicks(9079), new DateTime(2025, 5, 24, 17, 3, 0, 571, DateTimeKind.Utc).AddTicks(9079) });

            migrationBuilder.UpdateData(
                table: "LifeAreas",
                keyColumn: "Id",
                keyValue: 10,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 5, 24, 17, 3, 0, 571, DateTimeKind.Utc).AddTicks(9079), new DateTime(2025, 5, 24, 17, 3, 0, 571, DateTimeKind.Utc).AddTicks(9079) });

            migrationBuilder.UpdateData(
                table: "Questions",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 5, 24, 17, 3, 0, 573, DateTimeKind.Utc).AddTicks(956), new DateTime(2025, 5, 24, 17, 3, 0, 573, DateTimeKind.Utc).AddTicks(956) });

            migrationBuilder.UpdateData(
                table: "Questions",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 5, 24, 17, 3, 0, 573, DateTimeKind.Utc).AddTicks(956), new DateTime(2025, 5, 24, 17, 3, 0, 573, DateTimeKind.Utc).AddTicks(956) });

            migrationBuilder.UpdateData(
                table: "Questions",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 5, 24, 17, 3, 0, 573, DateTimeKind.Utc).AddTicks(956), new DateTime(2025, 5, 24, 17, 3, 0, 573, DateTimeKind.Utc).AddTicks(956) });

            migrationBuilder.UpdateData(
                table: "Questions",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 5, 24, 17, 3, 0, 573, DateTimeKind.Utc).AddTicks(956), new DateTime(2025, 5, 24, 17, 3, 0, 573, DateTimeKind.Utc).AddTicks(956) });

            migrationBuilder.UpdateData(
                table: "Questions",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 5, 24, 17, 3, 0, 573, DateTimeKind.Utc).AddTicks(956), new DateTime(2025, 5, 24, 17, 3, 0, 573, DateTimeKind.Utc).AddTicks(956) });

            migrationBuilder.UpdateData(
                table: "Questions",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 5, 24, 17, 3, 0, 573, DateTimeKind.Utc).AddTicks(956), new DateTime(2025, 5, 24, 17, 3, 0, 573, DateTimeKind.Utc).AddTicks(956) });

            migrationBuilder.UpdateData(
                table: "Questions",
                keyColumn: "Id",
                keyValue: 7,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 5, 24, 17, 3, 0, 573, DateTimeKind.Utc).AddTicks(956), new DateTime(2025, 5, 24, 17, 3, 0, 573, DateTimeKind.Utc).AddTicks(956) });

            migrationBuilder.UpdateData(
                table: "Questions",
                keyColumn: "Id",
                keyValue: 8,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 5, 24, 17, 3, 0, 573, DateTimeKind.Utc).AddTicks(956), new DateTime(2025, 5, 24, 17, 3, 0, 573, DateTimeKind.Utc).AddTicks(956) });

            migrationBuilder.UpdateData(
                table: "Questions",
                keyColumn: "Id",
                keyValue: 9,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 5, 24, 17, 3, 0, 573, DateTimeKind.Utc).AddTicks(956), new DateTime(2025, 5, 24, 17, 3, 0, 573, DateTimeKind.Utc).AddTicks(956) });

            migrationBuilder.UpdateData(
                table: "Questions",
                keyColumn: "Id",
                keyValue: 10,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 5, 24, 17, 3, 0, 573, DateTimeKind.Utc).AddTicks(956), new DateTime(2025, 5, 24, 17, 3, 0, 573, DateTimeKind.Utc).AddTicks(956) });
        }
    }
}
