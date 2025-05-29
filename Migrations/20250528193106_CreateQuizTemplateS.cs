using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace ToolBox.Migrations
{
    /// <inheritdoc />
    public partial class CreateQuizTemplateS : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "QuestionnaireTemplates",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Title = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    Description = table.Column<string>(type: "TEXT", nullable: true),
                    CoachId = table.Column<int>(type: "integer", nullable: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_QuestionnaireTemplates", x => x.Id);
                    table.ForeignKey(
                        name: "FK_QuestionnaireTemplates_Users_CoachId",
                        column: x => x.CoachId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "QuestionTemplates",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    QuestionnaireTemplateId = table.Column<long>(type: "bigint", nullable: false),
                    QuestionText = table.Column<string>(type: "TEXT", nullable: false),
                    Type = table.Column<int>(type: "integer", nullable: false),
                    OptionsJson = table.Column<string>(type: "TEXT", nullable: true),
                    IsRequired = table.Column<bool>(type: "boolean", nullable: false),
                    Order = table.Column<int>(type: "integer", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_QuestionTemplates", x => x.Id);
                    table.ForeignKey(
                        name: "FK_QuestionTemplates_QuestionnaireTemplates_QuestionnaireTempl~",
                        column: x => x.QuestionnaireTemplateId,
                        principalTable: "QuestionnaireTemplates",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.UpdateData(
                table: "AreasProgreso",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2025, 5, 28, 19, 31, 5, 308, DateTimeKind.Utc).AddTicks(8835));

            migrationBuilder.UpdateData(
                table: "AreasProgreso",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2025, 5, 28, 19, 31, 5, 308, DateTimeKind.Utc).AddTicks(8835));

            migrationBuilder.UpdateData(
                table: "AreasProgreso",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedAt",
                value: new DateTime(2025, 5, 28, 19, 31, 5, 308, DateTimeKind.Utc).AddTicks(8835));

            migrationBuilder.UpdateData(
                table: "AreasProgreso",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedAt",
                value: new DateTime(2025, 5, 28, 19, 31, 5, 308, DateTimeKind.Utc).AddTicks(8835));

            migrationBuilder.UpdateData(
                table: "AreasProgreso",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreatedAt",
                value: new DateTime(2025, 5, 28, 19, 31, 5, 308, DateTimeKind.Utc).AddTicks(8835));

            migrationBuilder.UpdateData(
                table: "CategoriasHabitos",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "FechaCreacion" },
                values: new object[] { new DateTime(2025, 5, 28, 19, 31, 5, 309, DateTimeKind.Utc).AddTicks(9661), new DateTime(2025, 5, 28, 19, 31, 5, 309, DateTimeKind.Utc).AddTicks(9664) });

            migrationBuilder.UpdateData(
                table: "CategoriasHabitos",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedAt", "FechaCreacion" },
                values: new object[] { new DateTime(2025, 5, 28, 19, 31, 5, 309, DateTimeKind.Utc).AddTicks(9661), new DateTime(2025, 5, 28, 19, 31, 5, 309, DateTimeKind.Utc).AddTicks(9670) });

            migrationBuilder.UpdateData(
                table: "CategoriasHabitos",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedAt", "FechaCreacion" },
                values: new object[] { new DateTime(2025, 5, 28, 19, 31, 5, 309, DateTimeKind.Utc).AddTicks(9661), new DateTime(2025, 5, 28, 19, 31, 5, 309, DateTimeKind.Utc).AddTicks(9672) });

            migrationBuilder.UpdateData(
                table: "CategoriasHabitos",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "CreatedAt", "FechaCreacion" },
                values: new object[] { new DateTime(2025, 5, 28, 19, 31, 5, 309, DateTimeKind.Utc).AddTicks(9661), new DateTime(2025, 5, 28, 19, 31, 5, 309, DateTimeKind.Utc).AddTicks(9674) });

            migrationBuilder.UpdateData(
                table: "CategoriasHabitos",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "CreatedAt", "FechaCreacion" },
                values: new object[] { new DateTime(2025, 5, 28, 19, 31, 5, 309, DateTimeKind.Utc).AddTicks(9661), new DateTime(2025, 5, 28, 19, 31, 5, 309, DateTimeKind.Utc).AddTicks(9676) });

            migrationBuilder.UpdateData(
                table: "CategoriasHabitos",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "CreatedAt", "FechaCreacion" },
                values: new object[] { new DateTime(2025, 5, 28, 19, 31, 5, 309, DateTimeKind.Utc).AddTicks(9661), new DateTime(2025, 5, 28, 19, 31, 5, 309, DateTimeKind.Utc).AddTicks(9678) });

            migrationBuilder.UpdateData(
                table: "CategoriasHabitos",
                keyColumn: "Id",
                keyValue: 7,
                columns: new[] { "CreatedAt", "FechaCreacion" },
                values: new object[] { new DateTime(2025, 5, 28, 19, 31, 5, 309, DateTimeKind.Utc).AddTicks(9661), new DateTime(2025, 5, 28, 19, 31, 5, 309, DateTimeKind.Utc).AddTicks(9680) });

            migrationBuilder.UpdateData(
                table: "CategoriasHabitos",
                keyColumn: "Id",
                keyValue: 8,
                columns: new[] { "CreatedAt", "FechaCreacion" },
                values: new object[] { new DateTime(2025, 5, 28, 19, 31, 5, 309, DateTimeKind.Utc).AddTicks(9661), new DateTime(2025, 5, 28, 19, 31, 5, 309, DateTimeKind.Utc).AddTicks(9681) });

            migrationBuilder.UpdateData(
                table: "CategoriasProgreso",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2025, 5, 28, 19, 31, 5, 308, DateTimeKind.Utc).AddTicks(8835));

            migrationBuilder.UpdateData(
                table: "CategoriasProgreso",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2025, 5, 28, 19, 31, 5, 308, DateTimeKind.Utc).AddTicks(8835));

            migrationBuilder.UpdateData(
                table: "CategoriasProgreso",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedAt",
                value: new DateTime(2025, 5, 28, 19, 31, 5, 308, DateTimeKind.Utc).AddTicks(8835));

            migrationBuilder.UpdateData(
                table: "CategoriasProgreso",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedAt",
                value: new DateTime(2025, 5, 28, 19, 31, 5, 308, DateTimeKind.Utc).AddTicks(8835));

            migrationBuilder.UpdateData(
                table: "CategoriasProgreso",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreatedAt",
                value: new DateTime(2025, 5, 28, 19, 31, 5, 308, DateTimeKind.Utc).AddTicks(8835));

            migrationBuilder.UpdateData(
                table: "CategoriasProgreso",
                keyColumn: "Id",
                keyValue: 6,
                column: "CreatedAt",
                value: new DateTime(2025, 5, 28, 19, 31, 5, 308, DateTimeKind.Utc).AddTicks(8835));

            migrationBuilder.UpdateData(
                table: "CategoriasProgreso",
                keyColumn: "Id",
                keyValue: 7,
                column: "CreatedAt",
                value: new DateTime(2025, 5, 28, 19, 31, 5, 308, DateTimeKind.Utc).AddTicks(8835));

            migrationBuilder.UpdateData(
                table: "CategoriasProgreso",
                keyColumn: "Id",
                keyValue: 8,
                column: "CreatedAt",
                value: new DateTime(2025, 5, 28, 19, 31, 5, 308, DateTimeKind.Utc).AddTicks(8835));

            migrationBuilder.UpdateData(
                table: "CategoriasProgreso",
                keyColumn: "Id",
                keyValue: 9,
                column: "CreatedAt",
                value: new DateTime(2025, 5, 28, 19, 31, 5, 308, DateTimeKind.Utc).AddTicks(8835));

            migrationBuilder.UpdateData(
                table: "CategoriasProgreso",
                keyColumn: "Id",
                keyValue: 10,
                column: "CreatedAt",
                value: new DateTime(2025, 5, 28, 19, 31, 5, 308, DateTimeKind.Utc).AddTicks(8835));

            migrationBuilder.UpdateData(
                table: "CategoriasProgreso",
                keyColumn: "Id",
                keyValue: 11,
                column: "CreatedAt",
                value: new DateTime(2025, 5, 28, 19, 31, 5, 308, DateTimeKind.Utc).AddTicks(8835));

            migrationBuilder.UpdateData(
                table: "CategoriasProgreso",
                keyColumn: "Id",
                keyValue: 12,
                column: "CreatedAt",
                value: new DateTime(2025, 5, 28, 19, 31, 5, 308, DateTimeKind.Utc).AddTicks(8835));

            migrationBuilder.UpdateData(
                table: "CategoriasProgreso",
                keyColumn: "Id",
                keyValue: 13,
                column: "CreatedAt",
                value: new DateTime(2025, 5, 28, 19, 31, 5, 308, DateTimeKind.Utc).AddTicks(8835));

            migrationBuilder.UpdateData(
                table: "CategoriasProgreso",
                keyColumn: "Id",
                keyValue: 14,
                column: "CreatedAt",
                value: new DateTime(2025, 5, 28, 19, 31, 5, 308, DateTimeKind.Utc).AddTicks(8835));

            migrationBuilder.UpdateData(
                table: "CategoriasProgreso",
                keyColumn: "Id",
                keyValue: 15,
                column: "CreatedAt",
                value: new DateTime(2025, 5, 28, 19, 31, 5, 308, DateTimeKind.Utc).AddTicks(8835));

            migrationBuilder.UpdateData(
                table: "FrecuenciasHabitos",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "FechaCreacion" },
                values: new object[] { new DateTime(2025, 5, 28, 19, 31, 5, 309, DateTimeKind.Utc).AddTicks(9661), new DateTime(2025, 5, 28, 19, 31, 5, 309, DateTimeKind.Utc).AddTicks(9745) });

            migrationBuilder.UpdateData(
                table: "FrecuenciasHabitos",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedAt", "FechaCreacion" },
                values: new object[] { new DateTime(2025, 5, 28, 19, 31, 5, 309, DateTimeKind.Utc).AddTicks(9661), new DateTime(2025, 5, 28, 19, 31, 5, 309, DateTimeKind.Utc).AddTicks(9750) });

            migrationBuilder.UpdateData(
                table: "FrecuenciasHabitos",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedAt", "FechaCreacion" },
                values: new object[] { new DateTime(2025, 5, 28, 19, 31, 5, 309, DateTimeKind.Utc).AddTicks(9661), new DateTime(2025, 5, 28, 19, 31, 5, 309, DateTimeKind.Utc).AddTicks(9752) });

            migrationBuilder.UpdateData(
                table: "FrecuenciasHabitos",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "CreatedAt", "FechaCreacion" },
                values: new object[] { new DateTime(2025, 5, 28, 19, 31, 5, 309, DateTimeKind.Utc).AddTicks(9661), new DateTime(2025, 5, 28, 19, 31, 5, 309, DateTimeKind.Utc).AddTicks(9754) });

            migrationBuilder.UpdateData(
                table: "FrecuenciasHabitos",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "CreatedAt", "FechaCreacion" },
                values: new object[] { new DateTime(2025, 5, 28, 19, 31, 5, 309, DateTimeKind.Utc).AddTicks(9661), new DateTime(2025, 5, 28, 19, 31, 5, 309, DateTimeKind.Utc).AddTicks(9755) });

            migrationBuilder.UpdateData(
                table: "FrecuenciasHabitos",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "CreatedAt", "FechaCreacion" },
                values: new object[] { new DateTime(2025, 5, 28, 19, 31, 5, 309, DateTimeKind.Utc).AddTicks(9661), new DateTime(2025, 5, 28, 19, 31, 5, 309, DateTimeKind.Utc).AddTicks(9757) });

            migrationBuilder.UpdateData(
                table: "LifeAreas",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 5, 28, 19, 31, 5, 307, DateTimeKind.Utc).AddTicks(3713), new DateTime(2025, 5, 28, 19, 31, 5, 307, DateTimeKind.Utc).AddTicks(3713) });

            migrationBuilder.UpdateData(
                table: "LifeAreas",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 5, 28, 19, 31, 5, 307, DateTimeKind.Utc).AddTicks(3713), new DateTime(2025, 5, 28, 19, 31, 5, 307, DateTimeKind.Utc).AddTicks(3713) });

            migrationBuilder.UpdateData(
                table: "LifeAreas",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 5, 28, 19, 31, 5, 307, DateTimeKind.Utc).AddTicks(3713), new DateTime(2025, 5, 28, 19, 31, 5, 307, DateTimeKind.Utc).AddTicks(3713) });

            migrationBuilder.UpdateData(
                table: "LifeAreas",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 5, 28, 19, 31, 5, 307, DateTimeKind.Utc).AddTicks(3713), new DateTime(2025, 5, 28, 19, 31, 5, 307, DateTimeKind.Utc).AddTicks(3713) });

            migrationBuilder.UpdateData(
                table: "LifeAreas",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 5, 28, 19, 31, 5, 307, DateTimeKind.Utc).AddTicks(3713), new DateTime(2025, 5, 28, 19, 31, 5, 307, DateTimeKind.Utc).AddTicks(3713) });

            migrationBuilder.UpdateData(
                table: "LifeAreas",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 5, 28, 19, 31, 5, 307, DateTimeKind.Utc).AddTicks(3713), new DateTime(2025, 5, 28, 19, 31, 5, 307, DateTimeKind.Utc).AddTicks(3713) });

            migrationBuilder.UpdateData(
                table: "LifeAreas",
                keyColumn: "Id",
                keyValue: 7,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 5, 28, 19, 31, 5, 307, DateTimeKind.Utc).AddTicks(3713), new DateTime(2025, 5, 28, 19, 31, 5, 307, DateTimeKind.Utc).AddTicks(3713) });

            migrationBuilder.UpdateData(
                table: "LifeAreas",
                keyColumn: "Id",
                keyValue: 8,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 5, 28, 19, 31, 5, 307, DateTimeKind.Utc).AddTicks(3713), new DateTime(2025, 5, 28, 19, 31, 5, 307, DateTimeKind.Utc).AddTicks(3713) });

            migrationBuilder.UpdateData(
                table: "LifeAreas",
                keyColumn: "Id",
                keyValue: 9,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 5, 28, 19, 31, 5, 307, DateTimeKind.Utc).AddTicks(3713), new DateTime(2025, 5, 28, 19, 31, 5, 307, DateTimeKind.Utc).AddTicks(3713) });

            migrationBuilder.UpdateData(
                table: "LifeAreas",
                keyColumn: "Id",
                keyValue: 10,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 5, 28, 19, 31, 5, 307, DateTimeKind.Utc).AddTicks(3713), new DateTime(2025, 5, 28, 19, 31, 5, 307, DateTimeKind.Utc).AddTicks(3713) });

            migrationBuilder.UpdateData(
                table: "Questions",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 5, 28, 19, 31, 5, 308, DateTimeKind.Utc).AddTicks(8738), new DateTime(2025, 5, 28, 19, 31, 5, 308, DateTimeKind.Utc).AddTicks(8738) });

            migrationBuilder.UpdateData(
                table: "Questions",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 5, 28, 19, 31, 5, 308, DateTimeKind.Utc).AddTicks(8738), new DateTime(2025, 5, 28, 19, 31, 5, 308, DateTimeKind.Utc).AddTicks(8738) });

            migrationBuilder.UpdateData(
                table: "Questions",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 5, 28, 19, 31, 5, 308, DateTimeKind.Utc).AddTicks(8738), new DateTime(2025, 5, 28, 19, 31, 5, 308, DateTimeKind.Utc).AddTicks(8738) });

            migrationBuilder.UpdateData(
                table: "Questions",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 5, 28, 19, 31, 5, 308, DateTimeKind.Utc).AddTicks(8738), new DateTime(2025, 5, 28, 19, 31, 5, 308, DateTimeKind.Utc).AddTicks(8738) });

            migrationBuilder.UpdateData(
                table: "Questions",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 5, 28, 19, 31, 5, 308, DateTimeKind.Utc).AddTicks(8738), new DateTime(2025, 5, 28, 19, 31, 5, 308, DateTimeKind.Utc).AddTicks(8738) });

            migrationBuilder.UpdateData(
                table: "Questions",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 5, 28, 19, 31, 5, 308, DateTimeKind.Utc).AddTicks(8738), new DateTime(2025, 5, 28, 19, 31, 5, 308, DateTimeKind.Utc).AddTicks(8738) });

            migrationBuilder.UpdateData(
                table: "Questions",
                keyColumn: "Id",
                keyValue: 7,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 5, 28, 19, 31, 5, 308, DateTimeKind.Utc).AddTicks(8738), new DateTime(2025, 5, 28, 19, 31, 5, 308, DateTimeKind.Utc).AddTicks(8738) });

            migrationBuilder.UpdateData(
                table: "Questions",
                keyColumn: "Id",
                keyValue: 8,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 5, 28, 19, 31, 5, 308, DateTimeKind.Utc).AddTicks(8738), new DateTime(2025, 5, 28, 19, 31, 5, 308, DateTimeKind.Utc).AddTicks(8738) });

            migrationBuilder.UpdateData(
                table: "Questions",
                keyColumn: "Id",
                keyValue: 9,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 5, 28, 19, 31, 5, 308, DateTimeKind.Utc).AddTicks(8738), new DateTime(2025, 5, 28, 19, 31, 5, 308, DateTimeKind.Utc).AddTicks(8738) });

            migrationBuilder.UpdateData(
                table: "Questions",
                keyColumn: "Id",
                keyValue: 10,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 5, 28, 19, 31, 5, 308, DateTimeKind.Utc).AddTicks(8738), new DateTime(2025, 5, 28, 19, 31, 5, 308, DateTimeKind.Utc).AddTicks(8738) });

            migrationBuilder.CreateIndex(
                name: "IX_QuestionnaireTemplates_CoachId",
                table: "QuestionnaireTemplates",
                column: "CoachId");

            migrationBuilder.CreateIndex(
                name: "IX_QuestionnaireTemplates_CreatedAt",
                table: "QuestionnaireTemplates",
                column: "CreatedAt");

            migrationBuilder.CreateIndex(
                name: "IX_QuestionTemplates_QuestionnaireTemplateId_Order",
                table: "QuestionTemplates",
                columns: new[] { "QuestionnaireTemplateId", "Order" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "QuestionTemplates");

            migrationBuilder.DropTable(
                name: "QuestionnaireTemplates");

            migrationBuilder.UpdateData(
                table: "AreasProgreso",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2025, 5, 28, 16, 52, 12, 561, DateTimeKind.Utc).AddTicks(7210));

            migrationBuilder.UpdateData(
                table: "AreasProgreso",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2025, 5, 28, 16, 52, 12, 561, DateTimeKind.Utc).AddTicks(7210));

            migrationBuilder.UpdateData(
                table: "AreasProgreso",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedAt",
                value: new DateTime(2025, 5, 28, 16, 52, 12, 561, DateTimeKind.Utc).AddTicks(7210));

            migrationBuilder.UpdateData(
                table: "AreasProgreso",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedAt",
                value: new DateTime(2025, 5, 28, 16, 52, 12, 561, DateTimeKind.Utc).AddTicks(7210));

            migrationBuilder.UpdateData(
                table: "AreasProgreso",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreatedAt",
                value: new DateTime(2025, 5, 28, 16, 52, 12, 561, DateTimeKind.Utc).AddTicks(7210));

            migrationBuilder.UpdateData(
                table: "CategoriasHabitos",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "FechaCreacion" },
                values: new object[] { new DateTime(2025, 5, 28, 16, 52, 12, 562, DateTimeKind.Utc).AddTicks(7526), new DateTime(2025, 5, 28, 16, 52, 12, 562, DateTimeKind.Utc).AddTicks(7529) });

            migrationBuilder.UpdateData(
                table: "CategoriasHabitos",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedAt", "FechaCreacion" },
                values: new object[] { new DateTime(2025, 5, 28, 16, 52, 12, 562, DateTimeKind.Utc).AddTicks(7526), new DateTime(2025, 5, 28, 16, 52, 12, 562, DateTimeKind.Utc).AddTicks(7536) });

            migrationBuilder.UpdateData(
                table: "CategoriasHabitos",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedAt", "FechaCreacion" },
                values: new object[] { new DateTime(2025, 5, 28, 16, 52, 12, 562, DateTimeKind.Utc).AddTicks(7526), new DateTime(2025, 5, 28, 16, 52, 12, 562, DateTimeKind.Utc).AddTicks(7539) });

            migrationBuilder.UpdateData(
                table: "CategoriasHabitos",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "CreatedAt", "FechaCreacion" },
                values: new object[] { new DateTime(2025, 5, 28, 16, 52, 12, 562, DateTimeKind.Utc).AddTicks(7526), new DateTime(2025, 5, 28, 16, 52, 12, 562, DateTimeKind.Utc).AddTicks(7541) });

            migrationBuilder.UpdateData(
                table: "CategoriasHabitos",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "CreatedAt", "FechaCreacion" },
                values: new object[] { new DateTime(2025, 5, 28, 16, 52, 12, 562, DateTimeKind.Utc).AddTicks(7526), new DateTime(2025, 5, 28, 16, 52, 12, 562, DateTimeKind.Utc).AddTicks(7543) });

            migrationBuilder.UpdateData(
                table: "CategoriasHabitos",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "CreatedAt", "FechaCreacion" },
                values: new object[] { new DateTime(2025, 5, 28, 16, 52, 12, 562, DateTimeKind.Utc).AddTicks(7526), new DateTime(2025, 5, 28, 16, 52, 12, 562, DateTimeKind.Utc).AddTicks(7545) });

            migrationBuilder.UpdateData(
                table: "CategoriasHabitos",
                keyColumn: "Id",
                keyValue: 7,
                columns: new[] { "CreatedAt", "FechaCreacion" },
                values: new object[] { new DateTime(2025, 5, 28, 16, 52, 12, 562, DateTimeKind.Utc).AddTicks(7526), new DateTime(2025, 5, 28, 16, 52, 12, 562, DateTimeKind.Utc).AddTicks(7547) });

            migrationBuilder.UpdateData(
                table: "CategoriasHabitos",
                keyColumn: "Id",
                keyValue: 8,
                columns: new[] { "CreatedAt", "FechaCreacion" },
                values: new object[] { new DateTime(2025, 5, 28, 16, 52, 12, 562, DateTimeKind.Utc).AddTicks(7526), new DateTime(2025, 5, 28, 16, 52, 12, 562, DateTimeKind.Utc).AddTicks(7549) });

            migrationBuilder.UpdateData(
                table: "CategoriasProgreso",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2025, 5, 28, 16, 52, 12, 561, DateTimeKind.Utc).AddTicks(7210));

            migrationBuilder.UpdateData(
                table: "CategoriasProgreso",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2025, 5, 28, 16, 52, 12, 561, DateTimeKind.Utc).AddTicks(7210));

            migrationBuilder.UpdateData(
                table: "CategoriasProgreso",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedAt",
                value: new DateTime(2025, 5, 28, 16, 52, 12, 561, DateTimeKind.Utc).AddTicks(7210));

            migrationBuilder.UpdateData(
                table: "CategoriasProgreso",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedAt",
                value: new DateTime(2025, 5, 28, 16, 52, 12, 561, DateTimeKind.Utc).AddTicks(7210));

            migrationBuilder.UpdateData(
                table: "CategoriasProgreso",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreatedAt",
                value: new DateTime(2025, 5, 28, 16, 52, 12, 561, DateTimeKind.Utc).AddTicks(7210));

            migrationBuilder.UpdateData(
                table: "CategoriasProgreso",
                keyColumn: "Id",
                keyValue: 6,
                column: "CreatedAt",
                value: new DateTime(2025, 5, 28, 16, 52, 12, 561, DateTimeKind.Utc).AddTicks(7210));

            migrationBuilder.UpdateData(
                table: "CategoriasProgreso",
                keyColumn: "Id",
                keyValue: 7,
                column: "CreatedAt",
                value: new DateTime(2025, 5, 28, 16, 52, 12, 561, DateTimeKind.Utc).AddTicks(7210));

            migrationBuilder.UpdateData(
                table: "CategoriasProgreso",
                keyColumn: "Id",
                keyValue: 8,
                column: "CreatedAt",
                value: new DateTime(2025, 5, 28, 16, 52, 12, 561, DateTimeKind.Utc).AddTicks(7210));

            migrationBuilder.UpdateData(
                table: "CategoriasProgreso",
                keyColumn: "Id",
                keyValue: 9,
                column: "CreatedAt",
                value: new DateTime(2025, 5, 28, 16, 52, 12, 561, DateTimeKind.Utc).AddTicks(7210));

            migrationBuilder.UpdateData(
                table: "CategoriasProgreso",
                keyColumn: "Id",
                keyValue: 10,
                column: "CreatedAt",
                value: new DateTime(2025, 5, 28, 16, 52, 12, 561, DateTimeKind.Utc).AddTicks(7210));

            migrationBuilder.UpdateData(
                table: "CategoriasProgreso",
                keyColumn: "Id",
                keyValue: 11,
                column: "CreatedAt",
                value: new DateTime(2025, 5, 28, 16, 52, 12, 561, DateTimeKind.Utc).AddTicks(7210));

            migrationBuilder.UpdateData(
                table: "CategoriasProgreso",
                keyColumn: "Id",
                keyValue: 12,
                column: "CreatedAt",
                value: new DateTime(2025, 5, 28, 16, 52, 12, 561, DateTimeKind.Utc).AddTicks(7210));

            migrationBuilder.UpdateData(
                table: "CategoriasProgreso",
                keyColumn: "Id",
                keyValue: 13,
                column: "CreatedAt",
                value: new DateTime(2025, 5, 28, 16, 52, 12, 561, DateTimeKind.Utc).AddTicks(7210));

            migrationBuilder.UpdateData(
                table: "CategoriasProgreso",
                keyColumn: "Id",
                keyValue: 14,
                column: "CreatedAt",
                value: new DateTime(2025, 5, 28, 16, 52, 12, 561, DateTimeKind.Utc).AddTicks(7210));

            migrationBuilder.UpdateData(
                table: "CategoriasProgreso",
                keyColumn: "Id",
                keyValue: 15,
                column: "CreatedAt",
                value: new DateTime(2025, 5, 28, 16, 52, 12, 561, DateTimeKind.Utc).AddTicks(7210));

            migrationBuilder.UpdateData(
                table: "FrecuenciasHabitos",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "FechaCreacion" },
                values: new object[] { new DateTime(2025, 5, 28, 16, 52, 12, 562, DateTimeKind.Utc).AddTicks(7526), new DateTime(2025, 5, 28, 16, 52, 12, 562, DateTimeKind.Utc).AddTicks(7652) });

            migrationBuilder.UpdateData(
                table: "FrecuenciasHabitos",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedAt", "FechaCreacion" },
                values: new object[] { new DateTime(2025, 5, 28, 16, 52, 12, 562, DateTimeKind.Utc).AddTicks(7526), new DateTime(2025, 5, 28, 16, 52, 12, 562, DateTimeKind.Utc).AddTicks(7659) });

            migrationBuilder.UpdateData(
                table: "FrecuenciasHabitos",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedAt", "FechaCreacion" },
                values: new object[] { new DateTime(2025, 5, 28, 16, 52, 12, 562, DateTimeKind.Utc).AddTicks(7526), new DateTime(2025, 5, 28, 16, 52, 12, 562, DateTimeKind.Utc).AddTicks(7661) });

            migrationBuilder.UpdateData(
                table: "FrecuenciasHabitos",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "CreatedAt", "FechaCreacion" },
                values: new object[] { new DateTime(2025, 5, 28, 16, 52, 12, 562, DateTimeKind.Utc).AddTicks(7526), new DateTime(2025, 5, 28, 16, 52, 12, 562, DateTimeKind.Utc).AddTicks(7663) });

            migrationBuilder.UpdateData(
                table: "FrecuenciasHabitos",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "CreatedAt", "FechaCreacion" },
                values: new object[] { new DateTime(2025, 5, 28, 16, 52, 12, 562, DateTimeKind.Utc).AddTicks(7526), new DateTime(2025, 5, 28, 16, 52, 12, 562, DateTimeKind.Utc).AddTicks(7665) });

            migrationBuilder.UpdateData(
                table: "FrecuenciasHabitos",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "CreatedAt", "FechaCreacion" },
                values: new object[] { new DateTime(2025, 5, 28, 16, 52, 12, 562, DateTimeKind.Utc).AddTicks(7526), new DateTime(2025, 5, 28, 16, 52, 12, 562, DateTimeKind.Utc).AddTicks(7666) });

            migrationBuilder.UpdateData(
                table: "LifeAreas",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 5, 28, 16, 52, 12, 560, DateTimeKind.Utc).AddTicks(3293), new DateTime(2025, 5, 28, 16, 52, 12, 560, DateTimeKind.Utc).AddTicks(3293) });

            migrationBuilder.UpdateData(
                table: "LifeAreas",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 5, 28, 16, 52, 12, 560, DateTimeKind.Utc).AddTicks(3293), new DateTime(2025, 5, 28, 16, 52, 12, 560, DateTimeKind.Utc).AddTicks(3293) });

            migrationBuilder.UpdateData(
                table: "LifeAreas",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 5, 28, 16, 52, 12, 560, DateTimeKind.Utc).AddTicks(3293), new DateTime(2025, 5, 28, 16, 52, 12, 560, DateTimeKind.Utc).AddTicks(3293) });

            migrationBuilder.UpdateData(
                table: "LifeAreas",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 5, 28, 16, 52, 12, 560, DateTimeKind.Utc).AddTicks(3293), new DateTime(2025, 5, 28, 16, 52, 12, 560, DateTimeKind.Utc).AddTicks(3293) });

            migrationBuilder.UpdateData(
                table: "LifeAreas",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 5, 28, 16, 52, 12, 560, DateTimeKind.Utc).AddTicks(3293), new DateTime(2025, 5, 28, 16, 52, 12, 560, DateTimeKind.Utc).AddTicks(3293) });

            migrationBuilder.UpdateData(
                table: "LifeAreas",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 5, 28, 16, 52, 12, 560, DateTimeKind.Utc).AddTicks(3293), new DateTime(2025, 5, 28, 16, 52, 12, 560, DateTimeKind.Utc).AddTicks(3293) });

            migrationBuilder.UpdateData(
                table: "LifeAreas",
                keyColumn: "Id",
                keyValue: 7,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 5, 28, 16, 52, 12, 560, DateTimeKind.Utc).AddTicks(3293), new DateTime(2025, 5, 28, 16, 52, 12, 560, DateTimeKind.Utc).AddTicks(3293) });

            migrationBuilder.UpdateData(
                table: "LifeAreas",
                keyColumn: "Id",
                keyValue: 8,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 5, 28, 16, 52, 12, 560, DateTimeKind.Utc).AddTicks(3293), new DateTime(2025, 5, 28, 16, 52, 12, 560, DateTimeKind.Utc).AddTicks(3293) });

            migrationBuilder.UpdateData(
                table: "LifeAreas",
                keyColumn: "Id",
                keyValue: 9,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 5, 28, 16, 52, 12, 560, DateTimeKind.Utc).AddTicks(3293), new DateTime(2025, 5, 28, 16, 52, 12, 560, DateTimeKind.Utc).AddTicks(3293) });

            migrationBuilder.UpdateData(
                table: "LifeAreas",
                keyColumn: "Id",
                keyValue: 10,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 5, 28, 16, 52, 12, 560, DateTimeKind.Utc).AddTicks(3293), new DateTime(2025, 5, 28, 16, 52, 12, 560, DateTimeKind.Utc).AddTicks(3293) });

            migrationBuilder.UpdateData(
                table: "Questions",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 5, 28, 16, 52, 12, 561, DateTimeKind.Utc).AddTicks(7125), new DateTime(2025, 5, 28, 16, 52, 12, 561, DateTimeKind.Utc).AddTicks(7125) });

            migrationBuilder.UpdateData(
                table: "Questions",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 5, 28, 16, 52, 12, 561, DateTimeKind.Utc).AddTicks(7125), new DateTime(2025, 5, 28, 16, 52, 12, 561, DateTimeKind.Utc).AddTicks(7125) });

            migrationBuilder.UpdateData(
                table: "Questions",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 5, 28, 16, 52, 12, 561, DateTimeKind.Utc).AddTicks(7125), new DateTime(2025, 5, 28, 16, 52, 12, 561, DateTimeKind.Utc).AddTicks(7125) });

            migrationBuilder.UpdateData(
                table: "Questions",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 5, 28, 16, 52, 12, 561, DateTimeKind.Utc).AddTicks(7125), new DateTime(2025, 5, 28, 16, 52, 12, 561, DateTimeKind.Utc).AddTicks(7125) });

            migrationBuilder.UpdateData(
                table: "Questions",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 5, 28, 16, 52, 12, 561, DateTimeKind.Utc).AddTicks(7125), new DateTime(2025, 5, 28, 16, 52, 12, 561, DateTimeKind.Utc).AddTicks(7125) });

            migrationBuilder.UpdateData(
                table: "Questions",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 5, 28, 16, 52, 12, 561, DateTimeKind.Utc).AddTicks(7125), new DateTime(2025, 5, 28, 16, 52, 12, 561, DateTimeKind.Utc).AddTicks(7125) });

            migrationBuilder.UpdateData(
                table: "Questions",
                keyColumn: "Id",
                keyValue: 7,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 5, 28, 16, 52, 12, 561, DateTimeKind.Utc).AddTicks(7125), new DateTime(2025, 5, 28, 16, 52, 12, 561, DateTimeKind.Utc).AddTicks(7125) });

            migrationBuilder.UpdateData(
                table: "Questions",
                keyColumn: "Id",
                keyValue: 8,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 5, 28, 16, 52, 12, 561, DateTimeKind.Utc).AddTicks(7125), new DateTime(2025, 5, 28, 16, 52, 12, 561, DateTimeKind.Utc).AddTicks(7125) });

            migrationBuilder.UpdateData(
                table: "Questions",
                keyColumn: "Id",
                keyValue: 9,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 5, 28, 16, 52, 12, 561, DateTimeKind.Utc).AddTicks(7125), new DateTime(2025, 5, 28, 16, 52, 12, 561, DateTimeKind.Utc).AddTicks(7125) });

            migrationBuilder.UpdateData(
                table: "Questions",
                keyColumn: "Id",
                keyValue: 10,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 5, 28, 16, 52, 12, 561, DateTimeKind.Utc).AddTicks(7125), new DateTime(2025, 5, 28, 16, 52, 12, 561, DateTimeKind.Utc).AddTicks(7125) });
        }
    }
}
