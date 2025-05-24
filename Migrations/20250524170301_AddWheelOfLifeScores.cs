using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace ToolBox.Migrations
{
    /// <inheritdoc />
    public partial class AddWheelOfLifeScores : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "WheelOfLifeScores",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UserId = table.Column<int>(type: "integer", nullable: false),
                    LifeAreaId = table.Column<int>(type: "integer", nullable: false),
                    Score = table.Column<int>(type: "integer", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WheelOfLifeScores", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WheelOfLifeScores_LifeAreas_LifeAreaId",
                        column: x => x.LifeAreaId,
                        principalTable: "LifeAreas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_WheelOfLifeScores_Users_UserId",
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

            migrationBuilder.CreateIndex(
                name: "IX_WheelOfLifeScores_LifeAreaId",
                table: "WheelOfLifeScores",
                column: "LifeAreaId");

            migrationBuilder.CreateIndex(
                name: "IX_WheelOfLifeScores_UserId_LifeAreaId",
                table: "WheelOfLifeScores",
                columns: new[] { "UserId", "LifeAreaId" },
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "WheelOfLifeScores");

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
        }
    }
}
