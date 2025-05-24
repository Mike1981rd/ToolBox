using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ToolBox.Migrations
{
    /// <inheritdoc />
    public partial class AddLifeAreaEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "LifeAreas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Title = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    IconClass = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    IconColor = table.Column<string>(type: "character varying(7)", maxLength: 7, nullable: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    DisplayOrder = table.Column<int>(type: "integer", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    CreatedByUserId = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LifeAreas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LifeAreas_Users_CreatedByUserId",
                        column: x => x.CreatedByUserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.InsertData(
                table: "LifeAreas",
                columns: new[] { "Id", "CreatedAt", "CreatedByUserId", "Description", "DisplayOrder", "IconClass", "IconColor", "IsActive", "Title", "UpdatedAt" },
                values: new object[,]
                {
                    { 1, new DateTime(2025, 5, 24, 11, 45, 25, 907, DateTimeKind.Utc).AddTicks(4457), null, "Connection with your inner self and beliefs", 1, "fas fa-pray", "#8e44ad", true, "Spiritual", new DateTime(2025, 5, 24, 11, 45, 25, 907, DateTimeKind.Utc).AddTicks(4457) },
                    { 2, new DateTime(2025, 5, 24, 11, 45, 25, 907, DateTimeKind.Utc).AddTicks(4457), null, "Physical wellness and fitness", 2, "fas fa-heartbeat", "#e74c3c", true, "Physical Health", new DateTime(2025, 5, 24, 11, 45, 25, 907, DateTimeKind.Utc).AddTicks(4457) },
                    { 3, new DateTime(2025, 5, 24, 11, 45, 25, 907, DateTimeKind.Utc).AddTicks(4457), null, "Relationships with loved ones", 3, "fas fa-users", "#3498db", true, "Family & Friends", new DateTime(2025, 5, 24, 11, 45, 25, 907, DateTimeKind.Utc).AddTicks(4457) },
                    { 4, new DateTime(2025, 5, 24, 11, 45, 25, 907, DateTimeKind.Utc).AddTicks(4457), null, "Romantic relationships and partnerships", 4, "fas fa-heart", "#e91e63", true, "Partner", new DateTime(2025, 5, 24, 11, 45, 25, 907, DateTimeKind.Utc).AddTicks(4457) },
                    { 5, new DateTime(2025, 5, 24, 11, 45, 25, 907, DateTimeKind.Utc).AddTicks(4457), null, "Professional growth and purpose", 5, "fas fa-briefcase", "#34495e", true, "Mission/Career", new DateTime(2025, 5, 24, 11, 45, 25, 907, DateTimeKind.Utc).AddTicks(4457) },
                    { 6, new DateTime(2025, 5, 24, 11, 45, 25, 907, DateTimeKind.Utc).AddTicks(4457), null, "Financial stability and growth", 6, "fas fa-dollar-sign", "#27ae60", true, "Finances", new DateTime(2025, 5, 24, 11, 45, 25, 907, DateTimeKind.Utc).AddTicks(4457) },
                    { 7, new DateTime(2025, 5, 24, 11, 45, 25, 907, DateTimeKind.Utc).AddTicks(4457), null, "Self-improvement and learning", 7, "fas fa-graduation-cap", "#f39c12", true, "Personal Growth", new DateTime(2025, 5, 24, 11, 45, 25, 907, DateTimeKind.Utc).AddTicks(4457) },
                    { 8, new DateTime(2025, 5, 24, 11, 45, 25, 907, DateTimeKind.Utc).AddTicks(4457), null, "Leisure activities and hobbies", 8, "fas fa-gamepad", "#9b59b6", true, "Fun & Recreation", new DateTime(2025, 5, 24, 11, 45, 25, 907, DateTimeKind.Utc).AddTicks(4457) },
                    { 9, new DateTime(2025, 5, 24, 11, 45, 25, 907, DateTimeKind.Utc).AddTicks(4457), null, "Travel and new adventures", 9, "fas fa-plane", "#1abc9c", true, "Experiences", new DateTime(2025, 5, 24, 11, 45, 25, 907, DateTimeKind.Utc).AddTicks(4457) },
                    { 10, new DateTime(2025, 5, 24, 11, 45, 25, 907, DateTimeKind.Utc).AddTicks(4457), null, "Living space and surroundings", 10, "fas fa-home", "#95a5a6", true, "Environment", new DateTime(2025, 5, 24, 11, 45, 25, 907, DateTimeKind.Utc).AddTicks(4457) }
                });

            migrationBuilder.CreateIndex(
                name: "IX_LifeAreas_CreatedByUserId",
                table: "LifeAreas",
                column: "CreatedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_LifeAreas_DisplayOrder",
                table: "LifeAreas",
                column: "DisplayOrder");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "LifeAreas");
        }
    }
}
