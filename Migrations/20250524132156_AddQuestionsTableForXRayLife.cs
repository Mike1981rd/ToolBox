using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ToolBox.Migrations
{
    /// <inheritdoc />
    public partial class AddQuestionsTableForXRayLife : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Questions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    QuestionText = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: false),
                    LifeAreaId = table.Column<int>(type: "integer", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Questions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Questions_LifeAreas_LifeAreaId",
                        column: x => x.LifeAreaId,
                        principalTable: "LifeAreas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.UpdateData(
                table: "LifeAreas",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 5, 24, 13, 21, 56, 110, DateTimeKind.Utc).AddTicks(8242), new DateTime(2025, 5, 24, 13, 21, 56, 110, DateTimeKind.Utc).AddTicks(8242) });

            migrationBuilder.UpdateData(
                table: "LifeAreas",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 5, 24, 13, 21, 56, 110, DateTimeKind.Utc).AddTicks(8242), new DateTime(2025, 5, 24, 13, 21, 56, 110, DateTimeKind.Utc).AddTicks(8242) });

            migrationBuilder.UpdateData(
                table: "LifeAreas",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 5, 24, 13, 21, 56, 110, DateTimeKind.Utc).AddTicks(8242), new DateTime(2025, 5, 24, 13, 21, 56, 110, DateTimeKind.Utc).AddTicks(8242) });

            migrationBuilder.UpdateData(
                table: "LifeAreas",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 5, 24, 13, 21, 56, 110, DateTimeKind.Utc).AddTicks(8242), new DateTime(2025, 5, 24, 13, 21, 56, 110, DateTimeKind.Utc).AddTicks(8242) });

            migrationBuilder.UpdateData(
                table: "LifeAreas",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 5, 24, 13, 21, 56, 110, DateTimeKind.Utc).AddTicks(8242), new DateTime(2025, 5, 24, 13, 21, 56, 110, DateTimeKind.Utc).AddTicks(8242) });

            migrationBuilder.UpdateData(
                table: "LifeAreas",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 5, 24, 13, 21, 56, 110, DateTimeKind.Utc).AddTicks(8242), new DateTime(2025, 5, 24, 13, 21, 56, 110, DateTimeKind.Utc).AddTicks(8242) });

            migrationBuilder.UpdateData(
                table: "LifeAreas",
                keyColumn: "Id",
                keyValue: 7,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 5, 24, 13, 21, 56, 110, DateTimeKind.Utc).AddTicks(8242), new DateTime(2025, 5, 24, 13, 21, 56, 110, DateTimeKind.Utc).AddTicks(8242) });

            migrationBuilder.UpdateData(
                table: "LifeAreas",
                keyColumn: "Id",
                keyValue: 8,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 5, 24, 13, 21, 56, 110, DateTimeKind.Utc).AddTicks(8242), new DateTime(2025, 5, 24, 13, 21, 56, 110, DateTimeKind.Utc).AddTicks(8242) });

            migrationBuilder.UpdateData(
                table: "LifeAreas",
                keyColumn: "Id",
                keyValue: 9,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 5, 24, 13, 21, 56, 110, DateTimeKind.Utc).AddTicks(8242), new DateTime(2025, 5, 24, 13, 21, 56, 110, DateTimeKind.Utc).AddTicks(8242) });

            migrationBuilder.UpdateData(
                table: "LifeAreas",
                keyColumn: "Id",
                keyValue: 10,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 5, 24, 13, 21, 56, 110, DateTimeKind.Utc).AddTicks(8242), new DateTime(2025, 5, 24, 13, 21, 56, 110, DateTimeKind.Utc).AddTicks(8242) });

            migrationBuilder.InsertData(
                table: "Questions",
                columns: new[] { "Id", "CreatedAt", "LifeAreaId", "QuestionText", "UpdatedAt" },
                values: new object[,]
                {
                    { 1, new DateTime(2025, 5, 24, 13, 21, 56, 110, DateTimeKind.Utc).AddTicks(9738), 1, "¿Cuál es mi propósito en la vida?", new DateTime(2025, 5, 24, 13, 21, 56, 110, DateTimeKind.Utc).AddTicks(9738) },
                    { 2, new DateTime(2025, 5, 24, 13, 21, 56, 110, DateTimeKind.Utc).AddTicks(9738), 1, "¿Qué prácticas espirituales me conectan con mi ser interior?", new DateTime(2025, 5, 24, 13, 21, 56, 110, DateTimeKind.Utc).AddTicks(9738) },
                    { 3, new DateTime(2025, 5, 24, 13, 21, 56, 110, DateTimeKind.Utc).AddTicks(9738), 2, "¿Qué hábitos de salud necesito mejorar?", new DateTime(2025, 5, 24, 13, 21, 56, 110, DateTimeKind.Utc).AddTicks(9738) },
                    { 4, new DateTime(2025, 5, 24, 13, 21, 56, 110, DateTimeKind.Utc).AddTicks(9738), 2, "¿Cómo puedo mantener un equilibrio entre ejercicio y descanso?", new DateTime(2025, 5, 24, 13, 21, 56, 110, DateTimeKind.Utc).AddTicks(9738) },
                    { 5, new DateTime(2025, 5, 24, 13, 21, 56, 110, DateTimeKind.Utc).AddTicks(9738), 3, "¿Cómo puedo fortalecer mis relaciones familiares?", new DateTime(2025, 5, 24, 13, 21, 56, 110, DateTimeKind.Utc).AddTicks(9738) },
                    { 6, new DateTime(2025, 5, 24, 13, 21, 56, 110, DateTimeKind.Utc).AddTicks(9738), 3, "¿Qué amistades aportan valor a mi vida?", new DateTime(2025, 5, 24, 13, 21, 56, 110, DateTimeKind.Utc).AddTicks(9738) },
                    { 7, new DateTime(2025, 5, 24, 13, 21, 56, 110, DateTimeKind.Utc).AddTicks(9738), 4, "¿Qué cualidades busco en una pareja?", new DateTime(2025, 5, 24, 13, 21, 56, 110, DateTimeKind.Utc).AddTicks(9738) },
                    { 8, new DateTime(2025, 5, 24, 13, 21, 56, 110, DateTimeKind.Utc).AddTicks(9738), 4, "¿Cómo puedo mejorar mi comunicación en pareja?", new DateTime(2025, 5, 24, 13, 21, 56, 110, DateTimeKind.Utc).AddTicks(9738) },
                    { 9, new DateTime(2025, 5, 24, 13, 21, 56, 110, DateTimeKind.Utc).AddTicks(9738), 5, "¿Mi trabajo actual se alinea con mi misión de vida?", new DateTime(2025, 5, 24, 13, 21, 56, 110, DateTimeKind.Utc).AddTicks(9738) },
                    { 10, new DateTime(2025, 5, 24, 13, 21, 56, 110, DateTimeKind.Utc).AddTicks(9738), 5, "¿Qué habilidades necesito desarrollar para mi crecimiento profesional?", new DateTime(2025, 5, 24, 13, 21, 56, 110, DateTimeKind.Utc).AddTicks(9738) }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Questions_LifeAreaId",
                table: "Questions",
                column: "LifeAreaId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Questions");

            migrationBuilder.UpdateData(
                table: "LifeAreas",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 5, 24, 11, 45, 25, 907, DateTimeKind.Utc).AddTicks(4457), new DateTime(2025, 5, 24, 11, 45, 25, 907, DateTimeKind.Utc).AddTicks(4457) });

            migrationBuilder.UpdateData(
                table: "LifeAreas",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 5, 24, 11, 45, 25, 907, DateTimeKind.Utc).AddTicks(4457), new DateTime(2025, 5, 24, 11, 45, 25, 907, DateTimeKind.Utc).AddTicks(4457) });

            migrationBuilder.UpdateData(
                table: "LifeAreas",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 5, 24, 11, 45, 25, 907, DateTimeKind.Utc).AddTicks(4457), new DateTime(2025, 5, 24, 11, 45, 25, 907, DateTimeKind.Utc).AddTicks(4457) });

            migrationBuilder.UpdateData(
                table: "LifeAreas",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 5, 24, 11, 45, 25, 907, DateTimeKind.Utc).AddTicks(4457), new DateTime(2025, 5, 24, 11, 45, 25, 907, DateTimeKind.Utc).AddTicks(4457) });

            migrationBuilder.UpdateData(
                table: "LifeAreas",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 5, 24, 11, 45, 25, 907, DateTimeKind.Utc).AddTicks(4457), new DateTime(2025, 5, 24, 11, 45, 25, 907, DateTimeKind.Utc).AddTicks(4457) });

            migrationBuilder.UpdateData(
                table: "LifeAreas",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 5, 24, 11, 45, 25, 907, DateTimeKind.Utc).AddTicks(4457), new DateTime(2025, 5, 24, 11, 45, 25, 907, DateTimeKind.Utc).AddTicks(4457) });

            migrationBuilder.UpdateData(
                table: "LifeAreas",
                keyColumn: "Id",
                keyValue: 7,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 5, 24, 11, 45, 25, 907, DateTimeKind.Utc).AddTicks(4457), new DateTime(2025, 5, 24, 11, 45, 25, 907, DateTimeKind.Utc).AddTicks(4457) });

            migrationBuilder.UpdateData(
                table: "LifeAreas",
                keyColumn: "Id",
                keyValue: 8,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 5, 24, 11, 45, 25, 907, DateTimeKind.Utc).AddTicks(4457), new DateTime(2025, 5, 24, 11, 45, 25, 907, DateTimeKind.Utc).AddTicks(4457) });

            migrationBuilder.UpdateData(
                table: "LifeAreas",
                keyColumn: "Id",
                keyValue: 9,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 5, 24, 11, 45, 25, 907, DateTimeKind.Utc).AddTicks(4457), new DateTime(2025, 5, 24, 11, 45, 25, 907, DateTimeKind.Utc).AddTicks(4457) });

            migrationBuilder.UpdateData(
                table: "LifeAreas",
                keyColumn: "Id",
                keyValue: 10,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 5, 24, 11, 45, 25, 907, DateTimeKind.Utc).AddTicks(4457), new DateTime(2025, 5, 24, 11, 45, 25, 907, DateTimeKind.Utc).AddTicks(4457) });
        }
    }
}
