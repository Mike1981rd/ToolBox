using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace ToolBox.Migrations
{
    /// <inheritdoc />
    public partial class UserAnswers : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "UserAnswers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UserId = table.Column<int>(type: "integer", nullable: false),
                    QuestionId = table.Column<int>(type: "integer", nullable: false),
                    Score = table.Column<int>(type: "integer", nullable: false),
                    AnsweredAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserAnswers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserAnswers_Questions_QuestionId",
                        column: x => x.QuestionId,
                        principalTable: "Questions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserAnswers_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.UpdateData(
                table: "LifeAreas",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 5, 24, 14, 17, 49, 741, DateTimeKind.Utc).AddTicks(1954), new DateTime(2025, 5, 24, 14, 17, 49, 741, DateTimeKind.Utc).AddTicks(1954) });

            migrationBuilder.UpdateData(
                table: "LifeAreas",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 5, 24, 14, 17, 49, 741, DateTimeKind.Utc).AddTicks(1954), new DateTime(2025, 5, 24, 14, 17, 49, 741, DateTimeKind.Utc).AddTicks(1954) });

            migrationBuilder.UpdateData(
                table: "LifeAreas",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 5, 24, 14, 17, 49, 741, DateTimeKind.Utc).AddTicks(1954), new DateTime(2025, 5, 24, 14, 17, 49, 741, DateTimeKind.Utc).AddTicks(1954) });

            migrationBuilder.UpdateData(
                table: "LifeAreas",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 5, 24, 14, 17, 49, 741, DateTimeKind.Utc).AddTicks(1954), new DateTime(2025, 5, 24, 14, 17, 49, 741, DateTimeKind.Utc).AddTicks(1954) });

            migrationBuilder.UpdateData(
                table: "LifeAreas",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 5, 24, 14, 17, 49, 741, DateTimeKind.Utc).AddTicks(1954), new DateTime(2025, 5, 24, 14, 17, 49, 741, DateTimeKind.Utc).AddTicks(1954) });

            migrationBuilder.UpdateData(
                table: "LifeAreas",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 5, 24, 14, 17, 49, 741, DateTimeKind.Utc).AddTicks(1954), new DateTime(2025, 5, 24, 14, 17, 49, 741, DateTimeKind.Utc).AddTicks(1954) });

            migrationBuilder.UpdateData(
                table: "LifeAreas",
                keyColumn: "Id",
                keyValue: 7,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 5, 24, 14, 17, 49, 741, DateTimeKind.Utc).AddTicks(1954), new DateTime(2025, 5, 24, 14, 17, 49, 741, DateTimeKind.Utc).AddTicks(1954) });

            migrationBuilder.UpdateData(
                table: "LifeAreas",
                keyColumn: "Id",
                keyValue: 8,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 5, 24, 14, 17, 49, 741, DateTimeKind.Utc).AddTicks(1954), new DateTime(2025, 5, 24, 14, 17, 49, 741, DateTimeKind.Utc).AddTicks(1954) });

            migrationBuilder.UpdateData(
                table: "LifeAreas",
                keyColumn: "Id",
                keyValue: 9,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 5, 24, 14, 17, 49, 741, DateTimeKind.Utc).AddTicks(1954), new DateTime(2025, 5, 24, 14, 17, 49, 741, DateTimeKind.Utc).AddTicks(1954) });

            migrationBuilder.UpdateData(
                table: "LifeAreas",
                keyColumn: "Id",
                keyValue: 10,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 5, 24, 14, 17, 49, 741, DateTimeKind.Utc).AddTicks(1954), new DateTime(2025, 5, 24, 14, 17, 49, 741, DateTimeKind.Utc).AddTicks(1954) });

            migrationBuilder.UpdateData(
                table: "Questions",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 5, 24, 14, 17, 49, 741, DateTimeKind.Utc).AddTicks(7280), new DateTime(2025, 5, 24, 14, 17, 49, 741, DateTimeKind.Utc).AddTicks(7280) });

            migrationBuilder.UpdateData(
                table: "Questions",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 5, 24, 14, 17, 49, 741, DateTimeKind.Utc).AddTicks(7280), new DateTime(2025, 5, 24, 14, 17, 49, 741, DateTimeKind.Utc).AddTicks(7280) });

            migrationBuilder.UpdateData(
                table: "Questions",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 5, 24, 14, 17, 49, 741, DateTimeKind.Utc).AddTicks(7280), new DateTime(2025, 5, 24, 14, 17, 49, 741, DateTimeKind.Utc).AddTicks(7280) });

            migrationBuilder.UpdateData(
                table: "Questions",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 5, 24, 14, 17, 49, 741, DateTimeKind.Utc).AddTicks(7280), new DateTime(2025, 5, 24, 14, 17, 49, 741, DateTimeKind.Utc).AddTicks(7280) });

            migrationBuilder.UpdateData(
                table: "Questions",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 5, 24, 14, 17, 49, 741, DateTimeKind.Utc).AddTicks(7280), new DateTime(2025, 5, 24, 14, 17, 49, 741, DateTimeKind.Utc).AddTicks(7280) });

            migrationBuilder.UpdateData(
                table: "Questions",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 5, 24, 14, 17, 49, 741, DateTimeKind.Utc).AddTicks(7280), new DateTime(2025, 5, 24, 14, 17, 49, 741, DateTimeKind.Utc).AddTicks(7280) });

            migrationBuilder.UpdateData(
                table: "Questions",
                keyColumn: "Id",
                keyValue: 7,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 5, 24, 14, 17, 49, 741, DateTimeKind.Utc).AddTicks(7280), new DateTime(2025, 5, 24, 14, 17, 49, 741, DateTimeKind.Utc).AddTicks(7280) });

            migrationBuilder.UpdateData(
                table: "Questions",
                keyColumn: "Id",
                keyValue: 8,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 5, 24, 14, 17, 49, 741, DateTimeKind.Utc).AddTicks(7280), new DateTime(2025, 5, 24, 14, 17, 49, 741, DateTimeKind.Utc).AddTicks(7280) });

            migrationBuilder.UpdateData(
                table: "Questions",
                keyColumn: "Id",
                keyValue: 9,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 5, 24, 14, 17, 49, 741, DateTimeKind.Utc).AddTicks(7280), new DateTime(2025, 5, 24, 14, 17, 49, 741, DateTimeKind.Utc).AddTicks(7280) });

            migrationBuilder.UpdateData(
                table: "Questions",
                keyColumn: "Id",
                keyValue: 10,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 5, 24, 14, 17, 49, 741, DateTimeKind.Utc).AddTicks(7280), new DateTime(2025, 5, 24, 14, 17, 49, 741, DateTimeKind.Utc).AddTicks(7280) });

            migrationBuilder.CreateIndex(
                name: "IX_UserAnswers_QuestionId",
                table: "UserAnswers",
                column: "QuestionId");

            migrationBuilder.CreateIndex(
                name: "IX_UserAnswers_UserId_QuestionId",
                table: "UserAnswers",
                columns: new[] { "UserId", "QuestionId" },
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserAnswers");

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

            migrationBuilder.UpdateData(
                table: "Questions",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 5, 24, 13, 21, 56, 110, DateTimeKind.Utc).AddTicks(9738), new DateTime(2025, 5, 24, 13, 21, 56, 110, DateTimeKind.Utc).AddTicks(9738) });

            migrationBuilder.UpdateData(
                table: "Questions",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 5, 24, 13, 21, 56, 110, DateTimeKind.Utc).AddTicks(9738), new DateTime(2025, 5, 24, 13, 21, 56, 110, DateTimeKind.Utc).AddTicks(9738) });

            migrationBuilder.UpdateData(
                table: "Questions",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 5, 24, 13, 21, 56, 110, DateTimeKind.Utc).AddTicks(9738), new DateTime(2025, 5, 24, 13, 21, 56, 110, DateTimeKind.Utc).AddTicks(9738) });

            migrationBuilder.UpdateData(
                table: "Questions",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 5, 24, 13, 21, 56, 110, DateTimeKind.Utc).AddTicks(9738), new DateTime(2025, 5, 24, 13, 21, 56, 110, DateTimeKind.Utc).AddTicks(9738) });

            migrationBuilder.UpdateData(
                table: "Questions",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 5, 24, 13, 21, 56, 110, DateTimeKind.Utc).AddTicks(9738), new DateTime(2025, 5, 24, 13, 21, 56, 110, DateTimeKind.Utc).AddTicks(9738) });

            migrationBuilder.UpdateData(
                table: "Questions",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 5, 24, 13, 21, 56, 110, DateTimeKind.Utc).AddTicks(9738), new DateTime(2025, 5, 24, 13, 21, 56, 110, DateTimeKind.Utc).AddTicks(9738) });

            migrationBuilder.UpdateData(
                table: "Questions",
                keyColumn: "Id",
                keyValue: 7,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 5, 24, 13, 21, 56, 110, DateTimeKind.Utc).AddTicks(9738), new DateTime(2025, 5, 24, 13, 21, 56, 110, DateTimeKind.Utc).AddTicks(9738) });

            migrationBuilder.UpdateData(
                table: "Questions",
                keyColumn: "Id",
                keyValue: 8,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 5, 24, 13, 21, 56, 110, DateTimeKind.Utc).AddTicks(9738), new DateTime(2025, 5, 24, 13, 21, 56, 110, DateTimeKind.Utc).AddTicks(9738) });

            migrationBuilder.UpdateData(
                table: "Questions",
                keyColumn: "Id",
                keyValue: 9,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 5, 24, 13, 21, 56, 110, DateTimeKind.Utc).AddTicks(9738), new DateTime(2025, 5, 24, 13, 21, 56, 110, DateTimeKind.Utc).AddTicks(9738) });

            migrationBuilder.UpdateData(
                table: "Questions",
                keyColumn: "Id",
                keyValue: 10,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 5, 24, 13, 21, 56, 110, DateTimeKind.Utc).AddTicks(9738), new DateTime(2025, 5, 24, 13, 21, 56, 110, DateTimeKind.Utc).AddTicks(9738) });
        }
    }
}
