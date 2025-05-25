using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace ToolBox.Migrations
{
    /// <inheritdoc />
    public partial class AddTareas : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Tareas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UsuarioId = table.Column<int>(type: "integer", nullable: false),
                    Descripcion = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: false),
                    EsUrgente = table.Column<bool>(type: "boolean", nullable: false),
                    EsImportante = table.Column<bool>(type: "boolean", nullable: false),
                    EstaCompletada = table.Column<bool>(type: "boolean", nullable: false),
                    FechaCreacion = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    FechaCompletado = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    FechaActualizacion = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tareas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Tareas_Users_UsuarioId",
                        column: x => x.UsuarioId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.UpdateData(
                table: "AreasProgreso",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2025, 5, 24, 21, 26, 4, 839, DateTimeKind.Utc).AddTicks(4669));

            migrationBuilder.UpdateData(
                table: "AreasProgreso",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2025, 5, 24, 21, 26, 4, 839, DateTimeKind.Utc).AddTicks(4669));

            migrationBuilder.UpdateData(
                table: "AreasProgreso",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedAt",
                value: new DateTime(2025, 5, 24, 21, 26, 4, 839, DateTimeKind.Utc).AddTicks(4669));

            migrationBuilder.UpdateData(
                table: "AreasProgreso",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedAt",
                value: new DateTime(2025, 5, 24, 21, 26, 4, 839, DateTimeKind.Utc).AddTicks(4669));

            migrationBuilder.UpdateData(
                table: "AreasProgreso",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreatedAt",
                value: new DateTime(2025, 5, 24, 21, 26, 4, 839, DateTimeKind.Utc).AddTicks(4669));

            migrationBuilder.UpdateData(
                table: "CategoriasProgreso",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2025, 5, 24, 21, 26, 4, 839, DateTimeKind.Utc).AddTicks(4669));

            migrationBuilder.UpdateData(
                table: "CategoriasProgreso",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2025, 5, 24, 21, 26, 4, 839, DateTimeKind.Utc).AddTicks(4669));

            migrationBuilder.UpdateData(
                table: "CategoriasProgreso",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedAt",
                value: new DateTime(2025, 5, 24, 21, 26, 4, 839, DateTimeKind.Utc).AddTicks(4669));

            migrationBuilder.UpdateData(
                table: "CategoriasProgreso",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedAt",
                value: new DateTime(2025, 5, 24, 21, 26, 4, 839, DateTimeKind.Utc).AddTicks(4669));

            migrationBuilder.UpdateData(
                table: "CategoriasProgreso",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreatedAt",
                value: new DateTime(2025, 5, 24, 21, 26, 4, 839, DateTimeKind.Utc).AddTicks(4669));

            migrationBuilder.UpdateData(
                table: "CategoriasProgreso",
                keyColumn: "Id",
                keyValue: 6,
                column: "CreatedAt",
                value: new DateTime(2025, 5, 24, 21, 26, 4, 839, DateTimeKind.Utc).AddTicks(4669));

            migrationBuilder.UpdateData(
                table: "CategoriasProgreso",
                keyColumn: "Id",
                keyValue: 7,
                column: "CreatedAt",
                value: new DateTime(2025, 5, 24, 21, 26, 4, 839, DateTimeKind.Utc).AddTicks(4669));

            migrationBuilder.UpdateData(
                table: "CategoriasProgreso",
                keyColumn: "Id",
                keyValue: 8,
                column: "CreatedAt",
                value: new DateTime(2025, 5, 24, 21, 26, 4, 839, DateTimeKind.Utc).AddTicks(4669));

            migrationBuilder.UpdateData(
                table: "CategoriasProgreso",
                keyColumn: "Id",
                keyValue: 9,
                column: "CreatedAt",
                value: new DateTime(2025, 5, 24, 21, 26, 4, 839, DateTimeKind.Utc).AddTicks(4669));

            migrationBuilder.UpdateData(
                table: "CategoriasProgreso",
                keyColumn: "Id",
                keyValue: 10,
                column: "CreatedAt",
                value: new DateTime(2025, 5, 24, 21, 26, 4, 839, DateTimeKind.Utc).AddTicks(4669));

            migrationBuilder.UpdateData(
                table: "CategoriasProgreso",
                keyColumn: "Id",
                keyValue: 11,
                column: "CreatedAt",
                value: new DateTime(2025, 5, 24, 21, 26, 4, 839, DateTimeKind.Utc).AddTicks(4669));

            migrationBuilder.UpdateData(
                table: "CategoriasProgreso",
                keyColumn: "Id",
                keyValue: 12,
                column: "CreatedAt",
                value: new DateTime(2025, 5, 24, 21, 26, 4, 839, DateTimeKind.Utc).AddTicks(4669));

            migrationBuilder.UpdateData(
                table: "CategoriasProgreso",
                keyColumn: "Id",
                keyValue: 13,
                column: "CreatedAt",
                value: new DateTime(2025, 5, 24, 21, 26, 4, 839, DateTimeKind.Utc).AddTicks(4669));

            migrationBuilder.UpdateData(
                table: "CategoriasProgreso",
                keyColumn: "Id",
                keyValue: 14,
                column: "CreatedAt",
                value: new DateTime(2025, 5, 24, 21, 26, 4, 839, DateTimeKind.Utc).AddTicks(4669));

            migrationBuilder.UpdateData(
                table: "CategoriasProgreso",
                keyColumn: "Id",
                keyValue: 15,
                column: "CreatedAt",
                value: new DateTime(2025, 5, 24, 21, 26, 4, 839, DateTimeKind.Utc).AddTicks(4669));

            migrationBuilder.UpdateData(
                table: "LifeAreas",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 5, 24, 21, 26, 4, 837, DateTimeKind.Utc).AddTicks(7801), new DateTime(2025, 5, 24, 21, 26, 4, 837, DateTimeKind.Utc).AddTicks(7801) });

            migrationBuilder.UpdateData(
                table: "LifeAreas",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 5, 24, 21, 26, 4, 837, DateTimeKind.Utc).AddTicks(7801), new DateTime(2025, 5, 24, 21, 26, 4, 837, DateTimeKind.Utc).AddTicks(7801) });

            migrationBuilder.UpdateData(
                table: "LifeAreas",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 5, 24, 21, 26, 4, 837, DateTimeKind.Utc).AddTicks(7801), new DateTime(2025, 5, 24, 21, 26, 4, 837, DateTimeKind.Utc).AddTicks(7801) });

            migrationBuilder.UpdateData(
                table: "LifeAreas",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 5, 24, 21, 26, 4, 837, DateTimeKind.Utc).AddTicks(7801), new DateTime(2025, 5, 24, 21, 26, 4, 837, DateTimeKind.Utc).AddTicks(7801) });

            migrationBuilder.UpdateData(
                table: "LifeAreas",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 5, 24, 21, 26, 4, 837, DateTimeKind.Utc).AddTicks(7801), new DateTime(2025, 5, 24, 21, 26, 4, 837, DateTimeKind.Utc).AddTicks(7801) });

            migrationBuilder.UpdateData(
                table: "LifeAreas",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 5, 24, 21, 26, 4, 837, DateTimeKind.Utc).AddTicks(7801), new DateTime(2025, 5, 24, 21, 26, 4, 837, DateTimeKind.Utc).AddTicks(7801) });

            migrationBuilder.UpdateData(
                table: "LifeAreas",
                keyColumn: "Id",
                keyValue: 7,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 5, 24, 21, 26, 4, 837, DateTimeKind.Utc).AddTicks(7801), new DateTime(2025, 5, 24, 21, 26, 4, 837, DateTimeKind.Utc).AddTicks(7801) });

            migrationBuilder.UpdateData(
                table: "LifeAreas",
                keyColumn: "Id",
                keyValue: 8,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 5, 24, 21, 26, 4, 837, DateTimeKind.Utc).AddTicks(7801), new DateTime(2025, 5, 24, 21, 26, 4, 837, DateTimeKind.Utc).AddTicks(7801) });

            migrationBuilder.UpdateData(
                table: "LifeAreas",
                keyColumn: "Id",
                keyValue: 9,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 5, 24, 21, 26, 4, 837, DateTimeKind.Utc).AddTicks(7801), new DateTime(2025, 5, 24, 21, 26, 4, 837, DateTimeKind.Utc).AddTicks(7801) });

            migrationBuilder.UpdateData(
                table: "LifeAreas",
                keyColumn: "Id",
                keyValue: 10,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 5, 24, 21, 26, 4, 837, DateTimeKind.Utc).AddTicks(7801), new DateTime(2025, 5, 24, 21, 26, 4, 837, DateTimeKind.Utc).AddTicks(7801) });

            migrationBuilder.UpdateData(
                table: "Questions",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 5, 24, 21, 26, 4, 839, DateTimeKind.Utc).AddTicks(4452), new DateTime(2025, 5, 24, 21, 26, 4, 839, DateTimeKind.Utc).AddTicks(4452) });

            migrationBuilder.UpdateData(
                table: "Questions",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 5, 24, 21, 26, 4, 839, DateTimeKind.Utc).AddTicks(4452), new DateTime(2025, 5, 24, 21, 26, 4, 839, DateTimeKind.Utc).AddTicks(4452) });

            migrationBuilder.UpdateData(
                table: "Questions",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 5, 24, 21, 26, 4, 839, DateTimeKind.Utc).AddTicks(4452), new DateTime(2025, 5, 24, 21, 26, 4, 839, DateTimeKind.Utc).AddTicks(4452) });

            migrationBuilder.UpdateData(
                table: "Questions",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 5, 24, 21, 26, 4, 839, DateTimeKind.Utc).AddTicks(4452), new DateTime(2025, 5, 24, 21, 26, 4, 839, DateTimeKind.Utc).AddTicks(4452) });

            migrationBuilder.UpdateData(
                table: "Questions",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 5, 24, 21, 26, 4, 839, DateTimeKind.Utc).AddTicks(4452), new DateTime(2025, 5, 24, 21, 26, 4, 839, DateTimeKind.Utc).AddTicks(4452) });

            migrationBuilder.UpdateData(
                table: "Questions",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 5, 24, 21, 26, 4, 839, DateTimeKind.Utc).AddTicks(4452), new DateTime(2025, 5, 24, 21, 26, 4, 839, DateTimeKind.Utc).AddTicks(4452) });

            migrationBuilder.UpdateData(
                table: "Questions",
                keyColumn: "Id",
                keyValue: 7,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 5, 24, 21, 26, 4, 839, DateTimeKind.Utc).AddTicks(4452), new DateTime(2025, 5, 24, 21, 26, 4, 839, DateTimeKind.Utc).AddTicks(4452) });

            migrationBuilder.UpdateData(
                table: "Questions",
                keyColumn: "Id",
                keyValue: 8,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 5, 24, 21, 26, 4, 839, DateTimeKind.Utc).AddTicks(4452), new DateTime(2025, 5, 24, 21, 26, 4, 839, DateTimeKind.Utc).AddTicks(4452) });

            migrationBuilder.UpdateData(
                table: "Questions",
                keyColumn: "Id",
                keyValue: 9,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 5, 24, 21, 26, 4, 839, DateTimeKind.Utc).AddTicks(4452), new DateTime(2025, 5, 24, 21, 26, 4, 839, DateTimeKind.Utc).AddTicks(4452) });

            migrationBuilder.UpdateData(
                table: "Questions",
                keyColumn: "Id",
                keyValue: 10,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 5, 24, 21, 26, 4, 839, DateTimeKind.Utc).AddTicks(4452), new DateTime(2025, 5, 24, 21, 26, 4, 839, DateTimeKind.Utc).AddTicks(4452) });

            migrationBuilder.CreateIndex(
                name: "IX_Tareas_UsuarioId_FechaCreacion",
                table: "Tareas",
                columns: new[] { "UsuarioId", "FechaCreacion" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Tareas");

            migrationBuilder.UpdateData(
                table: "AreasProgreso",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2025, 5, 24, 18, 45, 3, 823, DateTimeKind.Utc).AddTicks(3607));

            migrationBuilder.UpdateData(
                table: "AreasProgreso",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2025, 5, 24, 18, 45, 3, 823, DateTimeKind.Utc).AddTicks(3607));

            migrationBuilder.UpdateData(
                table: "AreasProgreso",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedAt",
                value: new DateTime(2025, 5, 24, 18, 45, 3, 823, DateTimeKind.Utc).AddTicks(3607));

            migrationBuilder.UpdateData(
                table: "AreasProgreso",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedAt",
                value: new DateTime(2025, 5, 24, 18, 45, 3, 823, DateTimeKind.Utc).AddTicks(3607));

            migrationBuilder.UpdateData(
                table: "AreasProgreso",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreatedAt",
                value: new DateTime(2025, 5, 24, 18, 45, 3, 823, DateTimeKind.Utc).AddTicks(3607));

            migrationBuilder.UpdateData(
                table: "CategoriasProgreso",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2025, 5, 24, 18, 45, 3, 823, DateTimeKind.Utc).AddTicks(3607));

            migrationBuilder.UpdateData(
                table: "CategoriasProgreso",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2025, 5, 24, 18, 45, 3, 823, DateTimeKind.Utc).AddTicks(3607));

            migrationBuilder.UpdateData(
                table: "CategoriasProgreso",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedAt",
                value: new DateTime(2025, 5, 24, 18, 45, 3, 823, DateTimeKind.Utc).AddTicks(3607));

            migrationBuilder.UpdateData(
                table: "CategoriasProgreso",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedAt",
                value: new DateTime(2025, 5, 24, 18, 45, 3, 823, DateTimeKind.Utc).AddTicks(3607));

            migrationBuilder.UpdateData(
                table: "CategoriasProgreso",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreatedAt",
                value: new DateTime(2025, 5, 24, 18, 45, 3, 823, DateTimeKind.Utc).AddTicks(3607));

            migrationBuilder.UpdateData(
                table: "CategoriasProgreso",
                keyColumn: "Id",
                keyValue: 6,
                column: "CreatedAt",
                value: new DateTime(2025, 5, 24, 18, 45, 3, 823, DateTimeKind.Utc).AddTicks(3607));

            migrationBuilder.UpdateData(
                table: "CategoriasProgreso",
                keyColumn: "Id",
                keyValue: 7,
                column: "CreatedAt",
                value: new DateTime(2025, 5, 24, 18, 45, 3, 823, DateTimeKind.Utc).AddTicks(3607));

            migrationBuilder.UpdateData(
                table: "CategoriasProgreso",
                keyColumn: "Id",
                keyValue: 8,
                column: "CreatedAt",
                value: new DateTime(2025, 5, 24, 18, 45, 3, 823, DateTimeKind.Utc).AddTicks(3607));

            migrationBuilder.UpdateData(
                table: "CategoriasProgreso",
                keyColumn: "Id",
                keyValue: 9,
                column: "CreatedAt",
                value: new DateTime(2025, 5, 24, 18, 45, 3, 823, DateTimeKind.Utc).AddTicks(3607));

            migrationBuilder.UpdateData(
                table: "CategoriasProgreso",
                keyColumn: "Id",
                keyValue: 10,
                column: "CreatedAt",
                value: new DateTime(2025, 5, 24, 18, 45, 3, 823, DateTimeKind.Utc).AddTicks(3607));

            migrationBuilder.UpdateData(
                table: "CategoriasProgreso",
                keyColumn: "Id",
                keyValue: 11,
                column: "CreatedAt",
                value: new DateTime(2025, 5, 24, 18, 45, 3, 823, DateTimeKind.Utc).AddTicks(3607));

            migrationBuilder.UpdateData(
                table: "CategoriasProgreso",
                keyColumn: "Id",
                keyValue: 12,
                column: "CreatedAt",
                value: new DateTime(2025, 5, 24, 18, 45, 3, 823, DateTimeKind.Utc).AddTicks(3607));

            migrationBuilder.UpdateData(
                table: "CategoriasProgreso",
                keyColumn: "Id",
                keyValue: 13,
                column: "CreatedAt",
                value: new DateTime(2025, 5, 24, 18, 45, 3, 823, DateTimeKind.Utc).AddTicks(3607));

            migrationBuilder.UpdateData(
                table: "CategoriasProgreso",
                keyColumn: "Id",
                keyValue: 14,
                column: "CreatedAt",
                value: new DateTime(2025, 5, 24, 18, 45, 3, 823, DateTimeKind.Utc).AddTicks(3607));

            migrationBuilder.UpdateData(
                table: "CategoriasProgreso",
                keyColumn: "Id",
                keyValue: 15,
                column: "CreatedAt",
                value: new DateTime(2025, 5, 24, 18, 45, 3, 823, DateTimeKind.Utc).AddTicks(3607));

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
        }
    }
}
