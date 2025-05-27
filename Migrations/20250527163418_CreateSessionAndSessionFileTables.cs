using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ToolBox.Migrations
{
    /// <inheritdoc />
    public partial class CreateSessionAndSessionFileTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Sessions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ClientId = table.Column<int>(type: "integer", nullable: false),
                    SessionDateTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    NextSessionDateTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    KeyPoints = table.Column<string>(type: "TEXT", nullable: false),
                    WaysToDevelop = table.Column<string>(type: "TEXT", nullable: true),
                    Assignments = table.Column<string>(type: "TEXT", nullable: true),
                    Challenges = table.Column<string>(type: "TEXT", nullable: true),
                    Feedback = table.Column<string>(type: "TEXT", nullable: true),
                    Twitter = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    Status = table.Column<bool>(type: "boolean", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sessions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Sessions_Customers_ClientId",
                        column: x => x.ClientId,
                        principalTable: "Customers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SessionFiles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    SessionId = table.Column<int>(type: "integer", nullable: false),
                    OriginalName = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    FilePath = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    MimeType = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    Size = table.Column<int>(type: "integer", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SessionFiles", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SessionFiles_Sessions_SessionId",
                        column: x => x.SessionId,
                        principalTable: "Sessions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.UpdateData(
                table: "AreasProgreso",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2025, 5, 27, 16, 34, 17, 460, DateTimeKind.Utc).AddTicks(8333));

            migrationBuilder.UpdateData(
                table: "AreasProgreso",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2025, 5, 27, 16, 34, 17, 460, DateTimeKind.Utc).AddTicks(8333));

            migrationBuilder.UpdateData(
                table: "AreasProgreso",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedAt",
                value: new DateTime(2025, 5, 27, 16, 34, 17, 460, DateTimeKind.Utc).AddTicks(8333));

            migrationBuilder.UpdateData(
                table: "AreasProgreso",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedAt",
                value: new DateTime(2025, 5, 27, 16, 34, 17, 460, DateTimeKind.Utc).AddTicks(8333));

            migrationBuilder.UpdateData(
                table: "AreasProgreso",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreatedAt",
                value: new DateTime(2025, 5, 27, 16, 34, 17, 460, DateTimeKind.Utc).AddTicks(8333));

            migrationBuilder.UpdateData(
                table: "CategoriasHabitos",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "FechaCreacion" },
                values: new object[] { new DateTime(2025, 5, 27, 16, 34, 17, 461, DateTimeKind.Utc).AddTicks(8408), new DateTime(2025, 5, 27, 16, 34, 17, 461, DateTimeKind.Utc).AddTicks(8410) });

            migrationBuilder.UpdateData(
                table: "CategoriasHabitos",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedAt", "FechaCreacion" },
                values: new object[] { new DateTime(2025, 5, 27, 16, 34, 17, 461, DateTimeKind.Utc).AddTicks(8408), new DateTime(2025, 5, 27, 16, 34, 17, 461, DateTimeKind.Utc).AddTicks(8416) });

            migrationBuilder.UpdateData(
                table: "CategoriasHabitos",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedAt", "FechaCreacion" },
                values: new object[] { new DateTime(2025, 5, 27, 16, 34, 17, 461, DateTimeKind.Utc).AddTicks(8408), new DateTime(2025, 5, 27, 16, 34, 17, 461, DateTimeKind.Utc).AddTicks(8419) });

            migrationBuilder.UpdateData(
                table: "CategoriasHabitos",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "CreatedAt", "FechaCreacion" },
                values: new object[] { new DateTime(2025, 5, 27, 16, 34, 17, 461, DateTimeKind.Utc).AddTicks(8408), new DateTime(2025, 5, 27, 16, 34, 17, 461, DateTimeKind.Utc).AddTicks(8421) });

            migrationBuilder.UpdateData(
                table: "CategoriasHabitos",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "CreatedAt", "FechaCreacion" },
                values: new object[] { new DateTime(2025, 5, 27, 16, 34, 17, 461, DateTimeKind.Utc).AddTicks(8408), new DateTime(2025, 5, 27, 16, 34, 17, 461, DateTimeKind.Utc).AddTicks(8422) });

            migrationBuilder.UpdateData(
                table: "CategoriasHabitos",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "CreatedAt", "FechaCreacion" },
                values: new object[] { new DateTime(2025, 5, 27, 16, 34, 17, 461, DateTimeKind.Utc).AddTicks(8408), new DateTime(2025, 5, 27, 16, 34, 17, 461, DateTimeKind.Utc).AddTicks(8425) });

            migrationBuilder.UpdateData(
                table: "CategoriasHabitos",
                keyColumn: "Id",
                keyValue: 7,
                columns: new[] { "CreatedAt", "FechaCreacion" },
                values: new object[] { new DateTime(2025, 5, 27, 16, 34, 17, 461, DateTimeKind.Utc).AddTicks(8408), new DateTime(2025, 5, 27, 16, 34, 17, 461, DateTimeKind.Utc).AddTicks(8426) });

            migrationBuilder.UpdateData(
                table: "CategoriasHabitos",
                keyColumn: "Id",
                keyValue: 8,
                columns: new[] { "CreatedAt", "FechaCreacion" },
                values: new object[] { new DateTime(2025, 5, 27, 16, 34, 17, 461, DateTimeKind.Utc).AddTicks(8408), new DateTime(2025, 5, 27, 16, 34, 17, 461, DateTimeKind.Utc).AddTicks(8428) });

            migrationBuilder.UpdateData(
                table: "CategoriasProgreso",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2025, 5, 27, 16, 34, 17, 460, DateTimeKind.Utc).AddTicks(8333));

            migrationBuilder.UpdateData(
                table: "CategoriasProgreso",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2025, 5, 27, 16, 34, 17, 460, DateTimeKind.Utc).AddTicks(8333));

            migrationBuilder.UpdateData(
                table: "CategoriasProgreso",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedAt",
                value: new DateTime(2025, 5, 27, 16, 34, 17, 460, DateTimeKind.Utc).AddTicks(8333));

            migrationBuilder.UpdateData(
                table: "CategoriasProgreso",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedAt",
                value: new DateTime(2025, 5, 27, 16, 34, 17, 460, DateTimeKind.Utc).AddTicks(8333));

            migrationBuilder.UpdateData(
                table: "CategoriasProgreso",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreatedAt",
                value: new DateTime(2025, 5, 27, 16, 34, 17, 460, DateTimeKind.Utc).AddTicks(8333));

            migrationBuilder.UpdateData(
                table: "CategoriasProgreso",
                keyColumn: "Id",
                keyValue: 6,
                column: "CreatedAt",
                value: new DateTime(2025, 5, 27, 16, 34, 17, 460, DateTimeKind.Utc).AddTicks(8333));

            migrationBuilder.UpdateData(
                table: "CategoriasProgreso",
                keyColumn: "Id",
                keyValue: 7,
                column: "CreatedAt",
                value: new DateTime(2025, 5, 27, 16, 34, 17, 460, DateTimeKind.Utc).AddTicks(8333));

            migrationBuilder.UpdateData(
                table: "CategoriasProgreso",
                keyColumn: "Id",
                keyValue: 8,
                column: "CreatedAt",
                value: new DateTime(2025, 5, 27, 16, 34, 17, 460, DateTimeKind.Utc).AddTicks(8333));

            migrationBuilder.UpdateData(
                table: "CategoriasProgreso",
                keyColumn: "Id",
                keyValue: 9,
                column: "CreatedAt",
                value: new DateTime(2025, 5, 27, 16, 34, 17, 460, DateTimeKind.Utc).AddTicks(8333));

            migrationBuilder.UpdateData(
                table: "CategoriasProgreso",
                keyColumn: "Id",
                keyValue: 10,
                column: "CreatedAt",
                value: new DateTime(2025, 5, 27, 16, 34, 17, 460, DateTimeKind.Utc).AddTicks(8333));

            migrationBuilder.UpdateData(
                table: "CategoriasProgreso",
                keyColumn: "Id",
                keyValue: 11,
                column: "CreatedAt",
                value: new DateTime(2025, 5, 27, 16, 34, 17, 460, DateTimeKind.Utc).AddTicks(8333));

            migrationBuilder.UpdateData(
                table: "CategoriasProgreso",
                keyColumn: "Id",
                keyValue: 12,
                column: "CreatedAt",
                value: new DateTime(2025, 5, 27, 16, 34, 17, 460, DateTimeKind.Utc).AddTicks(8333));

            migrationBuilder.UpdateData(
                table: "CategoriasProgreso",
                keyColumn: "Id",
                keyValue: 13,
                column: "CreatedAt",
                value: new DateTime(2025, 5, 27, 16, 34, 17, 460, DateTimeKind.Utc).AddTicks(8333));

            migrationBuilder.UpdateData(
                table: "CategoriasProgreso",
                keyColumn: "Id",
                keyValue: 14,
                column: "CreatedAt",
                value: new DateTime(2025, 5, 27, 16, 34, 17, 460, DateTimeKind.Utc).AddTicks(8333));

            migrationBuilder.UpdateData(
                table: "CategoriasProgreso",
                keyColumn: "Id",
                keyValue: 15,
                column: "CreatedAt",
                value: new DateTime(2025, 5, 27, 16, 34, 17, 460, DateTimeKind.Utc).AddTicks(8333));

            migrationBuilder.UpdateData(
                table: "FrecuenciasHabitos",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "FechaCreacion" },
                values: new object[] { new DateTime(2025, 5, 27, 16, 34, 17, 461, DateTimeKind.Utc).AddTicks(8408), new DateTime(2025, 5, 27, 16, 34, 17, 461, DateTimeKind.Utc).AddTicks(8432) });

            migrationBuilder.UpdateData(
                table: "FrecuenciasHabitos",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedAt", "FechaCreacion" },
                values: new object[] { new DateTime(2025, 5, 27, 16, 34, 17, 461, DateTimeKind.Utc).AddTicks(8408), new DateTime(2025, 5, 27, 16, 34, 17, 461, DateTimeKind.Utc).AddTicks(8436) });

            migrationBuilder.UpdateData(
                table: "FrecuenciasHabitos",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedAt", "FechaCreacion" },
                values: new object[] { new DateTime(2025, 5, 27, 16, 34, 17, 461, DateTimeKind.Utc).AddTicks(8408), new DateTime(2025, 5, 27, 16, 34, 17, 461, DateTimeKind.Utc).AddTicks(8438) });

            migrationBuilder.UpdateData(
                table: "FrecuenciasHabitos",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "CreatedAt", "FechaCreacion" },
                values: new object[] { new DateTime(2025, 5, 27, 16, 34, 17, 461, DateTimeKind.Utc).AddTicks(8408), new DateTime(2025, 5, 27, 16, 34, 17, 461, DateTimeKind.Utc).AddTicks(8440) });

            migrationBuilder.UpdateData(
                table: "FrecuenciasHabitos",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "CreatedAt", "FechaCreacion" },
                values: new object[] { new DateTime(2025, 5, 27, 16, 34, 17, 461, DateTimeKind.Utc).AddTicks(8408), new DateTime(2025, 5, 27, 16, 34, 17, 461, DateTimeKind.Utc).AddTicks(8442) });

            migrationBuilder.UpdateData(
                table: "FrecuenciasHabitos",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "CreatedAt", "FechaCreacion" },
                values: new object[] { new DateTime(2025, 5, 27, 16, 34, 17, 461, DateTimeKind.Utc).AddTicks(8408), new DateTime(2025, 5, 27, 16, 34, 17, 461, DateTimeKind.Utc).AddTicks(8444) });

            migrationBuilder.UpdateData(
                table: "LifeAreas",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 5, 27, 16, 34, 17, 459, DateTimeKind.Utc).AddTicks(4101), new DateTime(2025, 5, 27, 16, 34, 17, 459, DateTimeKind.Utc).AddTicks(4101) });

            migrationBuilder.UpdateData(
                table: "LifeAreas",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 5, 27, 16, 34, 17, 459, DateTimeKind.Utc).AddTicks(4101), new DateTime(2025, 5, 27, 16, 34, 17, 459, DateTimeKind.Utc).AddTicks(4101) });

            migrationBuilder.UpdateData(
                table: "LifeAreas",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 5, 27, 16, 34, 17, 459, DateTimeKind.Utc).AddTicks(4101), new DateTime(2025, 5, 27, 16, 34, 17, 459, DateTimeKind.Utc).AddTicks(4101) });

            migrationBuilder.UpdateData(
                table: "LifeAreas",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 5, 27, 16, 34, 17, 459, DateTimeKind.Utc).AddTicks(4101), new DateTime(2025, 5, 27, 16, 34, 17, 459, DateTimeKind.Utc).AddTicks(4101) });

            migrationBuilder.UpdateData(
                table: "LifeAreas",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 5, 27, 16, 34, 17, 459, DateTimeKind.Utc).AddTicks(4101), new DateTime(2025, 5, 27, 16, 34, 17, 459, DateTimeKind.Utc).AddTicks(4101) });

            migrationBuilder.UpdateData(
                table: "LifeAreas",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 5, 27, 16, 34, 17, 459, DateTimeKind.Utc).AddTicks(4101), new DateTime(2025, 5, 27, 16, 34, 17, 459, DateTimeKind.Utc).AddTicks(4101) });

            migrationBuilder.UpdateData(
                table: "LifeAreas",
                keyColumn: "Id",
                keyValue: 7,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 5, 27, 16, 34, 17, 459, DateTimeKind.Utc).AddTicks(4101), new DateTime(2025, 5, 27, 16, 34, 17, 459, DateTimeKind.Utc).AddTicks(4101) });

            migrationBuilder.UpdateData(
                table: "LifeAreas",
                keyColumn: "Id",
                keyValue: 8,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 5, 27, 16, 34, 17, 459, DateTimeKind.Utc).AddTicks(4101), new DateTime(2025, 5, 27, 16, 34, 17, 459, DateTimeKind.Utc).AddTicks(4101) });

            migrationBuilder.UpdateData(
                table: "LifeAreas",
                keyColumn: "Id",
                keyValue: 9,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 5, 27, 16, 34, 17, 459, DateTimeKind.Utc).AddTicks(4101), new DateTime(2025, 5, 27, 16, 34, 17, 459, DateTimeKind.Utc).AddTicks(4101) });

            migrationBuilder.UpdateData(
                table: "LifeAreas",
                keyColumn: "Id",
                keyValue: 10,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 5, 27, 16, 34, 17, 459, DateTimeKind.Utc).AddTicks(4101), new DateTime(2025, 5, 27, 16, 34, 17, 459, DateTimeKind.Utc).AddTicks(4101) });

            // Comentado porque los permisos ya fueron insertados manualmente
            /*
            migrationBuilder.InsertData(
                table: "Permissions",
                columns: new[] { "Id", "ActionName", "Category", "Description", "ModuleName" },
                values: new object[,]
                {
                    { 55, "Read", "Gestión de Sesiones", "Ver y listar en Calendario de Sesiones", "Calendario" },
                    { 56, "Write", "Gestión de Sesiones", "Editar y actualizar en Calendario de Sesiones", "Calendario" },
                    { 57, "Create", "Gestión de Sesiones", "Crear nuevos registros en Calendario de Sesiones", "Calendario" },
                    { 58, "Read", "Otros", "Ver y listar en Sessions", "Sessions" },
                    { 59, "Write", "Otros", "Editar y actualizar en Sessions", "Sessions" },
                    { 60, "Create", "Otros", "Crear nuevos registros en Sessions", "Sessions" }
                });
            */

            migrationBuilder.UpdateData(
                table: "Questions",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 5, 27, 16, 34, 17, 460, DateTimeKind.Utc).AddTicks(8247), new DateTime(2025, 5, 27, 16, 34, 17, 460, DateTimeKind.Utc).AddTicks(8247) });

            migrationBuilder.UpdateData(
                table: "Questions",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 5, 27, 16, 34, 17, 460, DateTimeKind.Utc).AddTicks(8247), new DateTime(2025, 5, 27, 16, 34, 17, 460, DateTimeKind.Utc).AddTicks(8247) });

            migrationBuilder.UpdateData(
                table: "Questions",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 5, 27, 16, 34, 17, 460, DateTimeKind.Utc).AddTicks(8247), new DateTime(2025, 5, 27, 16, 34, 17, 460, DateTimeKind.Utc).AddTicks(8247) });

            migrationBuilder.UpdateData(
                table: "Questions",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 5, 27, 16, 34, 17, 460, DateTimeKind.Utc).AddTicks(8247), new DateTime(2025, 5, 27, 16, 34, 17, 460, DateTimeKind.Utc).AddTicks(8247) });

            migrationBuilder.UpdateData(
                table: "Questions",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 5, 27, 16, 34, 17, 460, DateTimeKind.Utc).AddTicks(8247), new DateTime(2025, 5, 27, 16, 34, 17, 460, DateTimeKind.Utc).AddTicks(8247) });

            migrationBuilder.UpdateData(
                table: "Questions",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 5, 27, 16, 34, 17, 460, DateTimeKind.Utc).AddTicks(8247), new DateTime(2025, 5, 27, 16, 34, 17, 460, DateTimeKind.Utc).AddTicks(8247) });

            migrationBuilder.UpdateData(
                table: "Questions",
                keyColumn: "Id",
                keyValue: 7,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 5, 27, 16, 34, 17, 460, DateTimeKind.Utc).AddTicks(8247), new DateTime(2025, 5, 27, 16, 34, 17, 460, DateTimeKind.Utc).AddTicks(8247) });

            migrationBuilder.UpdateData(
                table: "Questions",
                keyColumn: "Id",
                keyValue: 8,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 5, 27, 16, 34, 17, 460, DateTimeKind.Utc).AddTicks(8247), new DateTime(2025, 5, 27, 16, 34, 17, 460, DateTimeKind.Utc).AddTicks(8247) });

            migrationBuilder.UpdateData(
                table: "Questions",
                keyColumn: "Id",
                keyValue: 9,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 5, 27, 16, 34, 17, 460, DateTimeKind.Utc).AddTicks(8247), new DateTime(2025, 5, 27, 16, 34, 17, 460, DateTimeKind.Utc).AddTicks(8247) });

            migrationBuilder.UpdateData(
                table: "Questions",
                keyColumn: "Id",
                keyValue: 10,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 5, 27, 16, 34, 17, 460, DateTimeKind.Utc).AddTicks(8247), new DateTime(2025, 5, 27, 16, 34, 17, 460, DateTimeKind.Utc).AddTicks(8247) });

            migrationBuilder.CreateIndex(
                name: "IX_SessionFiles_SessionId",
                table: "SessionFiles",
                column: "SessionId");

            migrationBuilder.CreateIndex(
                name: "IX_Sessions_ClientId_SessionDateTime",
                table: "Sessions",
                columns: new[] { "ClientId", "SessionDateTime" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SessionFiles");

            migrationBuilder.DropTable(
                name: "Sessions");

            // Comentado porque los permisos ya fueron insertados manualmente
            /*
            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 55);

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 56);

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 57);

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 58);

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 59);

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 60);
            */

            migrationBuilder.UpdateData(
                table: "AreasProgreso",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2025, 5, 27, 2, 52, 2, 228, DateTimeKind.Utc).AddTicks(8912));

            migrationBuilder.UpdateData(
                table: "AreasProgreso",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2025, 5, 27, 2, 52, 2, 228, DateTimeKind.Utc).AddTicks(8912));

            migrationBuilder.UpdateData(
                table: "AreasProgreso",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedAt",
                value: new DateTime(2025, 5, 27, 2, 52, 2, 228, DateTimeKind.Utc).AddTicks(8912));

            migrationBuilder.UpdateData(
                table: "AreasProgreso",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedAt",
                value: new DateTime(2025, 5, 27, 2, 52, 2, 228, DateTimeKind.Utc).AddTicks(8912));

            migrationBuilder.UpdateData(
                table: "AreasProgreso",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreatedAt",
                value: new DateTime(2025, 5, 27, 2, 52, 2, 228, DateTimeKind.Utc).AddTicks(8912));

            migrationBuilder.UpdateData(
                table: "CategoriasHabitos",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "FechaCreacion" },
                values: new object[] { new DateTime(2025, 5, 27, 2, 52, 2, 229, DateTimeKind.Utc).AddTicks(9376), new DateTime(2025, 5, 27, 2, 52, 2, 229, DateTimeKind.Utc).AddTicks(9379) });

            migrationBuilder.UpdateData(
                table: "CategoriasHabitos",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedAt", "FechaCreacion" },
                values: new object[] { new DateTime(2025, 5, 27, 2, 52, 2, 229, DateTimeKind.Utc).AddTicks(9376), new DateTime(2025, 5, 27, 2, 52, 2, 229, DateTimeKind.Utc).AddTicks(9385) });

            migrationBuilder.UpdateData(
                table: "CategoriasHabitos",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedAt", "FechaCreacion" },
                values: new object[] { new DateTime(2025, 5, 27, 2, 52, 2, 229, DateTimeKind.Utc).AddTicks(9376), new DateTime(2025, 5, 27, 2, 52, 2, 229, DateTimeKind.Utc).AddTicks(9387) });

            migrationBuilder.UpdateData(
                table: "CategoriasHabitos",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "CreatedAt", "FechaCreacion" },
                values: new object[] { new DateTime(2025, 5, 27, 2, 52, 2, 229, DateTimeKind.Utc).AddTicks(9376), new DateTime(2025, 5, 27, 2, 52, 2, 229, DateTimeKind.Utc).AddTicks(9390) });

            migrationBuilder.UpdateData(
                table: "CategoriasHabitos",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "CreatedAt", "FechaCreacion" },
                values: new object[] { new DateTime(2025, 5, 27, 2, 52, 2, 229, DateTimeKind.Utc).AddTicks(9376), new DateTime(2025, 5, 27, 2, 52, 2, 229, DateTimeKind.Utc).AddTicks(9392) });

            migrationBuilder.UpdateData(
                table: "CategoriasHabitos",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "CreatedAt", "FechaCreacion" },
                values: new object[] { new DateTime(2025, 5, 27, 2, 52, 2, 229, DateTimeKind.Utc).AddTicks(9376), new DateTime(2025, 5, 27, 2, 52, 2, 229, DateTimeKind.Utc).AddTicks(9393) });

            migrationBuilder.UpdateData(
                table: "CategoriasHabitos",
                keyColumn: "Id",
                keyValue: 7,
                columns: new[] { "CreatedAt", "FechaCreacion" },
                values: new object[] { new DateTime(2025, 5, 27, 2, 52, 2, 229, DateTimeKind.Utc).AddTicks(9376), new DateTime(2025, 5, 27, 2, 52, 2, 229, DateTimeKind.Utc).AddTicks(9395) });

            migrationBuilder.UpdateData(
                table: "CategoriasHabitos",
                keyColumn: "Id",
                keyValue: 8,
                columns: new[] { "CreatedAt", "FechaCreacion" },
                values: new object[] { new DateTime(2025, 5, 27, 2, 52, 2, 229, DateTimeKind.Utc).AddTicks(9376), new DateTime(2025, 5, 27, 2, 52, 2, 229, DateTimeKind.Utc).AddTicks(9397) });

            migrationBuilder.UpdateData(
                table: "CategoriasProgreso",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2025, 5, 27, 2, 52, 2, 228, DateTimeKind.Utc).AddTicks(8912));

            migrationBuilder.UpdateData(
                table: "CategoriasProgreso",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2025, 5, 27, 2, 52, 2, 228, DateTimeKind.Utc).AddTicks(8912));

            migrationBuilder.UpdateData(
                table: "CategoriasProgreso",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedAt",
                value: new DateTime(2025, 5, 27, 2, 52, 2, 228, DateTimeKind.Utc).AddTicks(8912));

            migrationBuilder.UpdateData(
                table: "CategoriasProgreso",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedAt",
                value: new DateTime(2025, 5, 27, 2, 52, 2, 228, DateTimeKind.Utc).AddTicks(8912));

            migrationBuilder.UpdateData(
                table: "CategoriasProgreso",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreatedAt",
                value: new DateTime(2025, 5, 27, 2, 52, 2, 228, DateTimeKind.Utc).AddTicks(8912));

            migrationBuilder.UpdateData(
                table: "CategoriasProgreso",
                keyColumn: "Id",
                keyValue: 6,
                column: "CreatedAt",
                value: new DateTime(2025, 5, 27, 2, 52, 2, 228, DateTimeKind.Utc).AddTicks(8912));

            migrationBuilder.UpdateData(
                table: "CategoriasProgreso",
                keyColumn: "Id",
                keyValue: 7,
                column: "CreatedAt",
                value: new DateTime(2025, 5, 27, 2, 52, 2, 228, DateTimeKind.Utc).AddTicks(8912));

            migrationBuilder.UpdateData(
                table: "CategoriasProgreso",
                keyColumn: "Id",
                keyValue: 8,
                column: "CreatedAt",
                value: new DateTime(2025, 5, 27, 2, 52, 2, 228, DateTimeKind.Utc).AddTicks(8912));

            migrationBuilder.UpdateData(
                table: "CategoriasProgreso",
                keyColumn: "Id",
                keyValue: 9,
                column: "CreatedAt",
                value: new DateTime(2025, 5, 27, 2, 52, 2, 228, DateTimeKind.Utc).AddTicks(8912));

            migrationBuilder.UpdateData(
                table: "CategoriasProgreso",
                keyColumn: "Id",
                keyValue: 10,
                column: "CreatedAt",
                value: new DateTime(2025, 5, 27, 2, 52, 2, 228, DateTimeKind.Utc).AddTicks(8912));

            migrationBuilder.UpdateData(
                table: "CategoriasProgreso",
                keyColumn: "Id",
                keyValue: 11,
                column: "CreatedAt",
                value: new DateTime(2025, 5, 27, 2, 52, 2, 228, DateTimeKind.Utc).AddTicks(8912));

            migrationBuilder.UpdateData(
                table: "CategoriasProgreso",
                keyColumn: "Id",
                keyValue: 12,
                column: "CreatedAt",
                value: new DateTime(2025, 5, 27, 2, 52, 2, 228, DateTimeKind.Utc).AddTicks(8912));

            migrationBuilder.UpdateData(
                table: "CategoriasProgreso",
                keyColumn: "Id",
                keyValue: 13,
                column: "CreatedAt",
                value: new DateTime(2025, 5, 27, 2, 52, 2, 228, DateTimeKind.Utc).AddTicks(8912));

            migrationBuilder.UpdateData(
                table: "CategoriasProgreso",
                keyColumn: "Id",
                keyValue: 14,
                column: "CreatedAt",
                value: new DateTime(2025, 5, 27, 2, 52, 2, 228, DateTimeKind.Utc).AddTicks(8912));

            migrationBuilder.UpdateData(
                table: "CategoriasProgreso",
                keyColumn: "Id",
                keyValue: 15,
                column: "CreatedAt",
                value: new DateTime(2025, 5, 27, 2, 52, 2, 228, DateTimeKind.Utc).AddTicks(8912));

            migrationBuilder.UpdateData(
                table: "FrecuenciasHabitos",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "FechaCreacion" },
                values: new object[] { new DateTime(2025, 5, 27, 2, 52, 2, 229, DateTimeKind.Utc).AddTicks(9376), new DateTime(2025, 5, 27, 2, 52, 2, 229, DateTimeKind.Utc).AddTicks(9401) });

            migrationBuilder.UpdateData(
                table: "FrecuenciasHabitos",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedAt", "FechaCreacion" },
                values: new object[] { new DateTime(2025, 5, 27, 2, 52, 2, 229, DateTimeKind.Utc).AddTicks(9376), new DateTime(2025, 5, 27, 2, 52, 2, 229, DateTimeKind.Utc).AddTicks(9406) });

            migrationBuilder.UpdateData(
                table: "FrecuenciasHabitos",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedAt", "FechaCreacion" },
                values: new object[] { new DateTime(2025, 5, 27, 2, 52, 2, 229, DateTimeKind.Utc).AddTicks(9376), new DateTime(2025, 5, 27, 2, 52, 2, 229, DateTimeKind.Utc).AddTicks(9408) });

            migrationBuilder.UpdateData(
                table: "FrecuenciasHabitos",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "CreatedAt", "FechaCreacion" },
                values: new object[] { new DateTime(2025, 5, 27, 2, 52, 2, 229, DateTimeKind.Utc).AddTicks(9376), new DateTime(2025, 5, 27, 2, 52, 2, 229, DateTimeKind.Utc).AddTicks(9410) });

            migrationBuilder.UpdateData(
                table: "FrecuenciasHabitos",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "CreatedAt", "FechaCreacion" },
                values: new object[] { new DateTime(2025, 5, 27, 2, 52, 2, 229, DateTimeKind.Utc).AddTicks(9376), new DateTime(2025, 5, 27, 2, 52, 2, 229, DateTimeKind.Utc).AddTicks(9412) });

            migrationBuilder.UpdateData(
                table: "FrecuenciasHabitos",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "CreatedAt", "FechaCreacion" },
                values: new object[] { new DateTime(2025, 5, 27, 2, 52, 2, 229, DateTimeKind.Utc).AddTicks(9376), new DateTime(2025, 5, 27, 2, 52, 2, 229, DateTimeKind.Utc).AddTicks(9413) });

            migrationBuilder.UpdateData(
                table: "LifeAreas",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 5, 27, 2, 52, 2, 227, DateTimeKind.Utc).AddTicks(4699), new DateTime(2025, 5, 27, 2, 52, 2, 227, DateTimeKind.Utc).AddTicks(4699) });

            migrationBuilder.UpdateData(
                table: "LifeAreas",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 5, 27, 2, 52, 2, 227, DateTimeKind.Utc).AddTicks(4699), new DateTime(2025, 5, 27, 2, 52, 2, 227, DateTimeKind.Utc).AddTicks(4699) });

            migrationBuilder.UpdateData(
                table: "LifeAreas",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 5, 27, 2, 52, 2, 227, DateTimeKind.Utc).AddTicks(4699), new DateTime(2025, 5, 27, 2, 52, 2, 227, DateTimeKind.Utc).AddTicks(4699) });

            migrationBuilder.UpdateData(
                table: "LifeAreas",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 5, 27, 2, 52, 2, 227, DateTimeKind.Utc).AddTicks(4699), new DateTime(2025, 5, 27, 2, 52, 2, 227, DateTimeKind.Utc).AddTicks(4699) });

            migrationBuilder.UpdateData(
                table: "LifeAreas",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 5, 27, 2, 52, 2, 227, DateTimeKind.Utc).AddTicks(4699), new DateTime(2025, 5, 27, 2, 52, 2, 227, DateTimeKind.Utc).AddTicks(4699) });

            migrationBuilder.UpdateData(
                table: "LifeAreas",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 5, 27, 2, 52, 2, 227, DateTimeKind.Utc).AddTicks(4699), new DateTime(2025, 5, 27, 2, 52, 2, 227, DateTimeKind.Utc).AddTicks(4699) });

            migrationBuilder.UpdateData(
                table: "LifeAreas",
                keyColumn: "Id",
                keyValue: 7,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 5, 27, 2, 52, 2, 227, DateTimeKind.Utc).AddTicks(4699), new DateTime(2025, 5, 27, 2, 52, 2, 227, DateTimeKind.Utc).AddTicks(4699) });

            migrationBuilder.UpdateData(
                table: "LifeAreas",
                keyColumn: "Id",
                keyValue: 8,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 5, 27, 2, 52, 2, 227, DateTimeKind.Utc).AddTicks(4699), new DateTime(2025, 5, 27, 2, 52, 2, 227, DateTimeKind.Utc).AddTicks(4699) });

            migrationBuilder.UpdateData(
                table: "LifeAreas",
                keyColumn: "Id",
                keyValue: 9,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 5, 27, 2, 52, 2, 227, DateTimeKind.Utc).AddTicks(4699), new DateTime(2025, 5, 27, 2, 52, 2, 227, DateTimeKind.Utc).AddTicks(4699) });

            migrationBuilder.UpdateData(
                table: "LifeAreas",
                keyColumn: "Id",
                keyValue: 10,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 5, 27, 2, 52, 2, 227, DateTimeKind.Utc).AddTicks(4699), new DateTime(2025, 5, 27, 2, 52, 2, 227, DateTimeKind.Utc).AddTicks(4699) });

            migrationBuilder.UpdateData(
                table: "Questions",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 5, 27, 2, 52, 2, 228, DateTimeKind.Utc).AddTicks(8831), new DateTime(2025, 5, 27, 2, 52, 2, 228, DateTimeKind.Utc).AddTicks(8831) });

            migrationBuilder.UpdateData(
                table: "Questions",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 5, 27, 2, 52, 2, 228, DateTimeKind.Utc).AddTicks(8831), new DateTime(2025, 5, 27, 2, 52, 2, 228, DateTimeKind.Utc).AddTicks(8831) });

            migrationBuilder.UpdateData(
                table: "Questions",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 5, 27, 2, 52, 2, 228, DateTimeKind.Utc).AddTicks(8831), new DateTime(2025, 5, 27, 2, 52, 2, 228, DateTimeKind.Utc).AddTicks(8831) });

            migrationBuilder.UpdateData(
                table: "Questions",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 5, 27, 2, 52, 2, 228, DateTimeKind.Utc).AddTicks(8831), new DateTime(2025, 5, 27, 2, 52, 2, 228, DateTimeKind.Utc).AddTicks(8831) });

            migrationBuilder.UpdateData(
                table: "Questions",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 5, 27, 2, 52, 2, 228, DateTimeKind.Utc).AddTicks(8831), new DateTime(2025, 5, 27, 2, 52, 2, 228, DateTimeKind.Utc).AddTicks(8831) });

            migrationBuilder.UpdateData(
                table: "Questions",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 5, 27, 2, 52, 2, 228, DateTimeKind.Utc).AddTicks(8831), new DateTime(2025, 5, 27, 2, 52, 2, 228, DateTimeKind.Utc).AddTicks(8831) });

            migrationBuilder.UpdateData(
                table: "Questions",
                keyColumn: "Id",
                keyValue: 7,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 5, 27, 2, 52, 2, 228, DateTimeKind.Utc).AddTicks(8831), new DateTime(2025, 5, 27, 2, 52, 2, 228, DateTimeKind.Utc).AddTicks(8831) });

            migrationBuilder.UpdateData(
                table: "Questions",
                keyColumn: "Id",
                keyValue: 8,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 5, 27, 2, 52, 2, 228, DateTimeKind.Utc).AddTicks(8831), new DateTime(2025, 5, 27, 2, 52, 2, 228, DateTimeKind.Utc).AddTicks(8831) });

            migrationBuilder.UpdateData(
                table: "Questions",
                keyColumn: "Id",
                keyValue: 9,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 5, 27, 2, 52, 2, 228, DateTimeKind.Utc).AddTicks(8831), new DateTime(2025, 5, 27, 2, 52, 2, 228, DateTimeKind.Utc).AddTicks(8831) });

            migrationBuilder.UpdateData(
                table: "Questions",
                keyColumn: "Id",
                keyValue: 10,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 5, 27, 2, 52, 2, 228, DateTimeKind.Utc).AddTicks(8831), new DateTime(2025, 5, 27, 2, 52, 2, 228, DateTimeKind.Utc).AddTicks(8831) });
        }
    }
}
