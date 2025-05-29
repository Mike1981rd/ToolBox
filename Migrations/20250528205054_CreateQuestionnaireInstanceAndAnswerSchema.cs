using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ToolBox.Migrations
{
    /// <inheritdoc />
    public partial class CreateQuestionnaireInstanceAndAnswerSchema : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "QuestionnaireInstances",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    QuestionnaireTemplateId = table.Column<long>(type: "bigint", nullable: false),
                    ClientId = table.Column<int>(type: "integer", nullable: false),
                    AssignedByCoachId = table.Column<int>(type: "integer", nullable: false),
                    Status = table.Column<int>(type: "integer", nullable: false),
                    AssignedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    StartedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    CompletedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_QuestionnaireInstances", x => x.Id);
                    table.ForeignKey(
                        name: "FK_QuestionnaireInstances_QuestionnaireTemplates_Questionnaire~",
                        column: x => x.QuestionnaireTemplateId,
                        principalTable: "QuestionnaireTemplates",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_QuestionnaireInstances_Users_AssignedByCoachId",
                        column: x => x.AssignedByCoachId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_QuestionnaireInstances_Users_ClientId",
                        column: x => x.ClientId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Answers",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    QuestionnaireInstanceId = table.Column<long>(type: "bigint", nullable: false),
                    QuestionTemplateId = table.Column<long>(type: "bigint", nullable: false),
                    ResponseText = table.Column<string>(type: "TEXT", nullable: true),
                    AnsweredAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Answers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Answers_QuestionTemplates_QuestionTemplateId",
                        column: x => x.QuestionTemplateId,
                        principalTable: "QuestionTemplates",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Answers_QuestionnaireInstances_QuestionnaireInstanceId",
                        column: x => x.QuestionnaireInstanceId,
                        principalTable: "QuestionnaireInstances",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.UpdateData(
                table: "AreasProgreso",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2025, 5, 28, 20, 50, 52, 802, DateTimeKind.Utc).AddTicks(2786));

            migrationBuilder.UpdateData(
                table: "AreasProgreso",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2025, 5, 28, 20, 50, 52, 802, DateTimeKind.Utc).AddTicks(2786));

            migrationBuilder.UpdateData(
                table: "AreasProgreso",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedAt",
                value: new DateTime(2025, 5, 28, 20, 50, 52, 802, DateTimeKind.Utc).AddTicks(2786));

            migrationBuilder.UpdateData(
                table: "AreasProgreso",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedAt",
                value: new DateTime(2025, 5, 28, 20, 50, 52, 802, DateTimeKind.Utc).AddTicks(2786));

            migrationBuilder.UpdateData(
                table: "AreasProgreso",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreatedAt",
                value: new DateTime(2025, 5, 28, 20, 50, 52, 802, DateTimeKind.Utc).AddTicks(2786));

            migrationBuilder.UpdateData(
                table: "CategoriasHabitos",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "FechaCreacion" },
                values: new object[] { new DateTime(2025, 5, 28, 20, 50, 52, 803, DateTimeKind.Utc).AddTicks(3012), new DateTime(2025, 5, 28, 20, 50, 52, 803, DateTimeKind.Utc).AddTicks(3015) });

            migrationBuilder.UpdateData(
                table: "CategoriasHabitos",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedAt", "FechaCreacion" },
                values: new object[] { new DateTime(2025, 5, 28, 20, 50, 52, 803, DateTimeKind.Utc).AddTicks(3012), new DateTime(2025, 5, 28, 20, 50, 52, 803, DateTimeKind.Utc).AddTicks(3021) });

            migrationBuilder.UpdateData(
                table: "CategoriasHabitos",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedAt", "FechaCreacion" },
                values: new object[] { new DateTime(2025, 5, 28, 20, 50, 52, 803, DateTimeKind.Utc).AddTicks(3012), new DateTime(2025, 5, 28, 20, 50, 52, 803, DateTimeKind.Utc).AddTicks(3023) });

            migrationBuilder.UpdateData(
                table: "CategoriasHabitos",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "CreatedAt", "FechaCreacion" },
                values: new object[] { new DateTime(2025, 5, 28, 20, 50, 52, 803, DateTimeKind.Utc).AddTicks(3012), new DateTime(2025, 5, 28, 20, 50, 52, 803, DateTimeKind.Utc).AddTicks(3067) });

            migrationBuilder.UpdateData(
                table: "CategoriasHabitos",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "CreatedAt", "FechaCreacion" },
                values: new object[] { new DateTime(2025, 5, 28, 20, 50, 52, 803, DateTimeKind.Utc).AddTicks(3012), new DateTime(2025, 5, 28, 20, 50, 52, 803, DateTimeKind.Utc).AddTicks(3069) });

            migrationBuilder.UpdateData(
                table: "CategoriasHabitos",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "CreatedAt", "FechaCreacion" },
                values: new object[] { new DateTime(2025, 5, 28, 20, 50, 52, 803, DateTimeKind.Utc).AddTicks(3012), new DateTime(2025, 5, 28, 20, 50, 52, 803, DateTimeKind.Utc).AddTicks(3071) });

            migrationBuilder.UpdateData(
                table: "CategoriasHabitos",
                keyColumn: "Id",
                keyValue: 7,
                columns: new[] { "CreatedAt", "FechaCreacion" },
                values: new object[] { new DateTime(2025, 5, 28, 20, 50, 52, 803, DateTimeKind.Utc).AddTicks(3012), new DateTime(2025, 5, 28, 20, 50, 52, 803, DateTimeKind.Utc).AddTicks(3073) });

            migrationBuilder.UpdateData(
                table: "CategoriasHabitos",
                keyColumn: "Id",
                keyValue: 8,
                columns: new[] { "CreatedAt", "FechaCreacion" },
                values: new object[] { new DateTime(2025, 5, 28, 20, 50, 52, 803, DateTimeKind.Utc).AddTicks(3012), new DateTime(2025, 5, 28, 20, 50, 52, 803, DateTimeKind.Utc).AddTicks(3075) });

            migrationBuilder.UpdateData(
                table: "CategoriasProgreso",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2025, 5, 28, 20, 50, 52, 802, DateTimeKind.Utc).AddTicks(2786));

            migrationBuilder.UpdateData(
                table: "CategoriasProgreso",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2025, 5, 28, 20, 50, 52, 802, DateTimeKind.Utc).AddTicks(2786));

            migrationBuilder.UpdateData(
                table: "CategoriasProgreso",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedAt",
                value: new DateTime(2025, 5, 28, 20, 50, 52, 802, DateTimeKind.Utc).AddTicks(2786));

            migrationBuilder.UpdateData(
                table: "CategoriasProgreso",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedAt",
                value: new DateTime(2025, 5, 28, 20, 50, 52, 802, DateTimeKind.Utc).AddTicks(2786));

            migrationBuilder.UpdateData(
                table: "CategoriasProgreso",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreatedAt",
                value: new DateTime(2025, 5, 28, 20, 50, 52, 802, DateTimeKind.Utc).AddTicks(2786));

            migrationBuilder.UpdateData(
                table: "CategoriasProgreso",
                keyColumn: "Id",
                keyValue: 6,
                column: "CreatedAt",
                value: new DateTime(2025, 5, 28, 20, 50, 52, 802, DateTimeKind.Utc).AddTicks(2786));

            migrationBuilder.UpdateData(
                table: "CategoriasProgreso",
                keyColumn: "Id",
                keyValue: 7,
                column: "CreatedAt",
                value: new DateTime(2025, 5, 28, 20, 50, 52, 802, DateTimeKind.Utc).AddTicks(2786));

            migrationBuilder.UpdateData(
                table: "CategoriasProgreso",
                keyColumn: "Id",
                keyValue: 8,
                column: "CreatedAt",
                value: new DateTime(2025, 5, 28, 20, 50, 52, 802, DateTimeKind.Utc).AddTicks(2786));

            migrationBuilder.UpdateData(
                table: "CategoriasProgreso",
                keyColumn: "Id",
                keyValue: 9,
                column: "CreatedAt",
                value: new DateTime(2025, 5, 28, 20, 50, 52, 802, DateTimeKind.Utc).AddTicks(2786));

            migrationBuilder.UpdateData(
                table: "CategoriasProgreso",
                keyColumn: "Id",
                keyValue: 10,
                column: "CreatedAt",
                value: new DateTime(2025, 5, 28, 20, 50, 52, 802, DateTimeKind.Utc).AddTicks(2786));

            migrationBuilder.UpdateData(
                table: "CategoriasProgreso",
                keyColumn: "Id",
                keyValue: 11,
                column: "CreatedAt",
                value: new DateTime(2025, 5, 28, 20, 50, 52, 802, DateTimeKind.Utc).AddTicks(2786));

            migrationBuilder.UpdateData(
                table: "CategoriasProgreso",
                keyColumn: "Id",
                keyValue: 12,
                column: "CreatedAt",
                value: new DateTime(2025, 5, 28, 20, 50, 52, 802, DateTimeKind.Utc).AddTicks(2786));

            migrationBuilder.UpdateData(
                table: "CategoriasProgreso",
                keyColumn: "Id",
                keyValue: 13,
                column: "CreatedAt",
                value: new DateTime(2025, 5, 28, 20, 50, 52, 802, DateTimeKind.Utc).AddTicks(2786));

            migrationBuilder.UpdateData(
                table: "CategoriasProgreso",
                keyColumn: "Id",
                keyValue: 14,
                column: "CreatedAt",
                value: new DateTime(2025, 5, 28, 20, 50, 52, 802, DateTimeKind.Utc).AddTicks(2786));

            migrationBuilder.UpdateData(
                table: "CategoriasProgreso",
                keyColumn: "Id",
                keyValue: 15,
                column: "CreatedAt",
                value: new DateTime(2025, 5, 28, 20, 50, 52, 802, DateTimeKind.Utc).AddTicks(2786));

            migrationBuilder.UpdateData(
                table: "FrecuenciasHabitos",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "FechaCreacion" },
                values: new object[] { new DateTime(2025, 5, 28, 20, 50, 52, 803, DateTimeKind.Utc).AddTicks(3012), new DateTime(2025, 5, 28, 20, 50, 52, 803, DateTimeKind.Utc).AddTicks(3079) });

            migrationBuilder.UpdateData(
                table: "FrecuenciasHabitos",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedAt", "FechaCreacion" },
                values: new object[] { new DateTime(2025, 5, 28, 20, 50, 52, 803, DateTimeKind.Utc).AddTicks(3012), new DateTime(2025, 5, 28, 20, 50, 52, 803, DateTimeKind.Utc).AddTicks(3084) });

            migrationBuilder.UpdateData(
                table: "FrecuenciasHabitos",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedAt", "FechaCreacion" },
                values: new object[] { new DateTime(2025, 5, 28, 20, 50, 52, 803, DateTimeKind.Utc).AddTicks(3012), new DateTime(2025, 5, 28, 20, 50, 52, 803, DateTimeKind.Utc).AddTicks(3086) });

            migrationBuilder.UpdateData(
                table: "FrecuenciasHabitos",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "CreatedAt", "FechaCreacion" },
                values: new object[] { new DateTime(2025, 5, 28, 20, 50, 52, 803, DateTimeKind.Utc).AddTicks(3012), new DateTime(2025, 5, 28, 20, 50, 52, 803, DateTimeKind.Utc).AddTicks(3088) });

            migrationBuilder.UpdateData(
                table: "FrecuenciasHabitos",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "CreatedAt", "FechaCreacion" },
                values: new object[] { new DateTime(2025, 5, 28, 20, 50, 52, 803, DateTimeKind.Utc).AddTicks(3012), new DateTime(2025, 5, 28, 20, 50, 52, 803, DateTimeKind.Utc).AddTicks(3090) });

            migrationBuilder.UpdateData(
                table: "FrecuenciasHabitos",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "CreatedAt", "FechaCreacion" },
                values: new object[] { new DateTime(2025, 5, 28, 20, 50, 52, 803, DateTimeKind.Utc).AddTicks(3012), new DateTime(2025, 5, 28, 20, 50, 52, 803, DateTimeKind.Utc).AddTicks(3092) });

            migrationBuilder.UpdateData(
                table: "LifeAreas",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 5, 28, 20, 50, 52, 800, DateTimeKind.Utc).AddTicks(8846), new DateTime(2025, 5, 28, 20, 50, 52, 800, DateTimeKind.Utc).AddTicks(8846) });

            migrationBuilder.UpdateData(
                table: "LifeAreas",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 5, 28, 20, 50, 52, 800, DateTimeKind.Utc).AddTicks(8846), new DateTime(2025, 5, 28, 20, 50, 52, 800, DateTimeKind.Utc).AddTicks(8846) });

            migrationBuilder.UpdateData(
                table: "LifeAreas",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 5, 28, 20, 50, 52, 800, DateTimeKind.Utc).AddTicks(8846), new DateTime(2025, 5, 28, 20, 50, 52, 800, DateTimeKind.Utc).AddTicks(8846) });

            migrationBuilder.UpdateData(
                table: "LifeAreas",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 5, 28, 20, 50, 52, 800, DateTimeKind.Utc).AddTicks(8846), new DateTime(2025, 5, 28, 20, 50, 52, 800, DateTimeKind.Utc).AddTicks(8846) });

            migrationBuilder.UpdateData(
                table: "LifeAreas",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 5, 28, 20, 50, 52, 800, DateTimeKind.Utc).AddTicks(8846), new DateTime(2025, 5, 28, 20, 50, 52, 800, DateTimeKind.Utc).AddTicks(8846) });

            migrationBuilder.UpdateData(
                table: "LifeAreas",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 5, 28, 20, 50, 52, 800, DateTimeKind.Utc).AddTicks(8846), new DateTime(2025, 5, 28, 20, 50, 52, 800, DateTimeKind.Utc).AddTicks(8846) });

            migrationBuilder.UpdateData(
                table: "LifeAreas",
                keyColumn: "Id",
                keyValue: 7,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 5, 28, 20, 50, 52, 800, DateTimeKind.Utc).AddTicks(8846), new DateTime(2025, 5, 28, 20, 50, 52, 800, DateTimeKind.Utc).AddTicks(8846) });

            migrationBuilder.UpdateData(
                table: "LifeAreas",
                keyColumn: "Id",
                keyValue: 8,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 5, 28, 20, 50, 52, 800, DateTimeKind.Utc).AddTicks(8846), new DateTime(2025, 5, 28, 20, 50, 52, 800, DateTimeKind.Utc).AddTicks(8846) });

            migrationBuilder.UpdateData(
                table: "LifeAreas",
                keyColumn: "Id",
                keyValue: 9,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 5, 28, 20, 50, 52, 800, DateTimeKind.Utc).AddTicks(8846), new DateTime(2025, 5, 28, 20, 50, 52, 800, DateTimeKind.Utc).AddTicks(8846) });

            migrationBuilder.UpdateData(
                table: "LifeAreas",
                keyColumn: "Id",
                keyValue: 10,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 5, 28, 20, 50, 52, 800, DateTimeKind.Utc).AddTicks(8846), new DateTime(2025, 5, 28, 20, 50, 52, 800, DateTimeKind.Utc).AddTicks(8846) });

            // Comentado porque estos permisos ya existen en la base de datos
            // migrationBuilder.InsertData(
            //     table: "Permissions",
            //     columns: new[] { "Id", "ActionName", "Category", "Description", "ModuleName" },
            //     values: new object[,]
            //     {
            //         { 61, "Read", "Herramientas de Evaluación", "Ver y listar en Cuestionarios", "QuestionnaireTemplates" },
            //         { 62, "Write", "Herramientas de Evaluación", "Editar y actualizar en Cuestionarios", "QuestionnaireTemplates" },
            //         { 63, "Create", "Herramientas de Evaluación", "Crear nuevos registros en Cuestionarios", "QuestionnaireTemplates" }
            //     });

            migrationBuilder.UpdateData(
                table: "Questions",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 5, 28, 20, 50, 52, 802, DateTimeKind.Utc).AddTicks(2703), new DateTime(2025, 5, 28, 20, 50, 52, 802, DateTimeKind.Utc).AddTicks(2703) });

            migrationBuilder.UpdateData(
                table: "Questions",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 5, 28, 20, 50, 52, 802, DateTimeKind.Utc).AddTicks(2703), new DateTime(2025, 5, 28, 20, 50, 52, 802, DateTimeKind.Utc).AddTicks(2703) });

            migrationBuilder.UpdateData(
                table: "Questions",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 5, 28, 20, 50, 52, 802, DateTimeKind.Utc).AddTicks(2703), new DateTime(2025, 5, 28, 20, 50, 52, 802, DateTimeKind.Utc).AddTicks(2703) });

            migrationBuilder.UpdateData(
                table: "Questions",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 5, 28, 20, 50, 52, 802, DateTimeKind.Utc).AddTicks(2703), new DateTime(2025, 5, 28, 20, 50, 52, 802, DateTimeKind.Utc).AddTicks(2703) });

            migrationBuilder.UpdateData(
                table: "Questions",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 5, 28, 20, 50, 52, 802, DateTimeKind.Utc).AddTicks(2703), new DateTime(2025, 5, 28, 20, 50, 52, 802, DateTimeKind.Utc).AddTicks(2703) });

            migrationBuilder.UpdateData(
                table: "Questions",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 5, 28, 20, 50, 52, 802, DateTimeKind.Utc).AddTicks(2703), new DateTime(2025, 5, 28, 20, 50, 52, 802, DateTimeKind.Utc).AddTicks(2703) });

            migrationBuilder.UpdateData(
                table: "Questions",
                keyColumn: "Id",
                keyValue: 7,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 5, 28, 20, 50, 52, 802, DateTimeKind.Utc).AddTicks(2703), new DateTime(2025, 5, 28, 20, 50, 52, 802, DateTimeKind.Utc).AddTicks(2703) });

            migrationBuilder.UpdateData(
                table: "Questions",
                keyColumn: "Id",
                keyValue: 8,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 5, 28, 20, 50, 52, 802, DateTimeKind.Utc).AddTicks(2703), new DateTime(2025, 5, 28, 20, 50, 52, 802, DateTimeKind.Utc).AddTicks(2703) });

            migrationBuilder.UpdateData(
                table: "Questions",
                keyColumn: "Id",
                keyValue: 9,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 5, 28, 20, 50, 52, 802, DateTimeKind.Utc).AddTicks(2703), new DateTime(2025, 5, 28, 20, 50, 52, 802, DateTimeKind.Utc).AddTicks(2703) });

            migrationBuilder.UpdateData(
                table: "Questions",
                keyColumn: "Id",
                keyValue: 10,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 5, 28, 20, 50, 52, 802, DateTimeKind.Utc).AddTicks(2703), new DateTime(2025, 5, 28, 20, 50, 52, 802, DateTimeKind.Utc).AddTicks(2703) });

            migrationBuilder.CreateIndex(
                name: "IX_Answers_QuestionnaireInstanceId_QuestionTemplateId",
                table: "Answers",
                columns: new[] { "QuestionnaireInstanceId", "QuestionTemplateId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Answers_QuestionTemplateId",
                table: "Answers",
                column: "QuestionTemplateId");

            migrationBuilder.CreateIndex(
                name: "IX_QuestionnaireInstances_AssignedAt",
                table: "QuestionnaireInstances",
                column: "AssignedAt");

            migrationBuilder.CreateIndex(
                name: "IX_QuestionnaireInstances_AssignedByCoachId",
                table: "QuestionnaireInstances",
                column: "AssignedByCoachId");

            migrationBuilder.CreateIndex(
                name: "IX_QuestionnaireInstances_ClientId_Status",
                table: "QuestionnaireInstances",
                columns: new[] { "ClientId", "Status" });

            migrationBuilder.CreateIndex(
                name: "IX_QuestionnaireInstances_QuestionnaireTemplateId",
                table: "QuestionnaireInstances",
                column: "QuestionnaireTemplateId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Answers");

            migrationBuilder.DropTable(
                name: "QuestionnaireInstances");

            // Comentado para no borrar permisos existentes
            // migrationBuilder.DeleteData(
            //     table: "Permissions",
            //     keyColumn: "Id",
            //     keyValue: 61);

            // migrationBuilder.DeleteData(
            //     table: "Permissions",
            //     keyColumn: "Id",
            //     keyValue: 62);

            // migrationBuilder.DeleteData(
            //     table: "Permissions",
            //     keyColumn: "Id",
            //     keyValue: 63);

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
        }
    }
}
