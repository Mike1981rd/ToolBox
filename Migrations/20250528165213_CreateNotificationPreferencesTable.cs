using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace ToolBox.Migrations
{
    /// <inheritdoc />
    public partial class CreateNotificationPreferencesTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "NotificationPreferences",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UserId = table.Column<int>(type: "integer", nullable: false),
                    NotificationType = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    IsEnabledInApp = table.Column<bool>(type: "boolean", nullable: false),
                    IsEnabledEmail = table.Column<bool>(type: "boolean", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NotificationPreferences", x => x.Id);
                    table.ForeignKey(
                        name: "FK_NotificationPreferences_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

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

            migrationBuilder.CreateIndex(
                name: "IX_NotificationPreferences_UserId_NotificationType",
                table: "NotificationPreferences",
                columns: new[] { "UserId", "NotificationType" },
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "NotificationPreferences");

            migrationBuilder.UpdateData(
                table: "AreasProgreso",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2025, 5, 28, 15, 53, 3, 954, DateTimeKind.Utc).AddTicks(2814));

            migrationBuilder.UpdateData(
                table: "AreasProgreso",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2025, 5, 28, 15, 53, 3, 954, DateTimeKind.Utc).AddTicks(2814));

            migrationBuilder.UpdateData(
                table: "AreasProgreso",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedAt",
                value: new DateTime(2025, 5, 28, 15, 53, 3, 954, DateTimeKind.Utc).AddTicks(2814));

            migrationBuilder.UpdateData(
                table: "AreasProgreso",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedAt",
                value: new DateTime(2025, 5, 28, 15, 53, 3, 954, DateTimeKind.Utc).AddTicks(2814));

            migrationBuilder.UpdateData(
                table: "AreasProgreso",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreatedAt",
                value: new DateTime(2025, 5, 28, 15, 53, 3, 954, DateTimeKind.Utc).AddTicks(2814));

            migrationBuilder.UpdateData(
                table: "CategoriasHabitos",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "FechaCreacion" },
                values: new object[] { new DateTime(2025, 5, 28, 15, 53, 3, 955, DateTimeKind.Utc).AddTicks(3635), new DateTime(2025, 5, 28, 15, 53, 3, 955, DateTimeKind.Utc).AddTicks(3639) });

            migrationBuilder.UpdateData(
                table: "CategoriasHabitos",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedAt", "FechaCreacion" },
                values: new object[] { new DateTime(2025, 5, 28, 15, 53, 3, 955, DateTimeKind.Utc).AddTicks(3635), new DateTime(2025, 5, 28, 15, 53, 3, 955, DateTimeKind.Utc).AddTicks(3646) });

            migrationBuilder.UpdateData(
                table: "CategoriasHabitos",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedAt", "FechaCreacion" },
                values: new object[] { new DateTime(2025, 5, 28, 15, 53, 3, 955, DateTimeKind.Utc).AddTicks(3635), new DateTime(2025, 5, 28, 15, 53, 3, 955, DateTimeKind.Utc).AddTicks(3649) });

            migrationBuilder.UpdateData(
                table: "CategoriasHabitos",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "CreatedAt", "FechaCreacion" },
                values: new object[] { new DateTime(2025, 5, 28, 15, 53, 3, 955, DateTimeKind.Utc).AddTicks(3635), new DateTime(2025, 5, 28, 15, 53, 3, 955, DateTimeKind.Utc).AddTicks(3651) });

            migrationBuilder.UpdateData(
                table: "CategoriasHabitos",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "CreatedAt", "FechaCreacion" },
                values: new object[] { new DateTime(2025, 5, 28, 15, 53, 3, 955, DateTimeKind.Utc).AddTicks(3635), new DateTime(2025, 5, 28, 15, 53, 3, 955, DateTimeKind.Utc).AddTicks(3652) });

            migrationBuilder.UpdateData(
                table: "CategoriasHabitos",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "CreatedAt", "FechaCreacion" },
                values: new object[] { new DateTime(2025, 5, 28, 15, 53, 3, 955, DateTimeKind.Utc).AddTicks(3635), new DateTime(2025, 5, 28, 15, 53, 3, 955, DateTimeKind.Utc).AddTicks(3654) });

            migrationBuilder.UpdateData(
                table: "CategoriasHabitos",
                keyColumn: "Id",
                keyValue: 7,
                columns: new[] { "CreatedAt", "FechaCreacion" },
                values: new object[] { new DateTime(2025, 5, 28, 15, 53, 3, 955, DateTimeKind.Utc).AddTicks(3635), new DateTime(2025, 5, 28, 15, 53, 3, 955, DateTimeKind.Utc).AddTicks(3656) });

            migrationBuilder.UpdateData(
                table: "CategoriasHabitos",
                keyColumn: "Id",
                keyValue: 8,
                columns: new[] { "CreatedAt", "FechaCreacion" },
                values: new object[] { new DateTime(2025, 5, 28, 15, 53, 3, 955, DateTimeKind.Utc).AddTicks(3635), new DateTime(2025, 5, 28, 15, 53, 3, 955, DateTimeKind.Utc).AddTicks(3658) });

            migrationBuilder.UpdateData(
                table: "CategoriasProgreso",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2025, 5, 28, 15, 53, 3, 954, DateTimeKind.Utc).AddTicks(2814));

            migrationBuilder.UpdateData(
                table: "CategoriasProgreso",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2025, 5, 28, 15, 53, 3, 954, DateTimeKind.Utc).AddTicks(2814));

            migrationBuilder.UpdateData(
                table: "CategoriasProgreso",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedAt",
                value: new DateTime(2025, 5, 28, 15, 53, 3, 954, DateTimeKind.Utc).AddTicks(2814));

            migrationBuilder.UpdateData(
                table: "CategoriasProgreso",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedAt",
                value: new DateTime(2025, 5, 28, 15, 53, 3, 954, DateTimeKind.Utc).AddTicks(2814));

            migrationBuilder.UpdateData(
                table: "CategoriasProgreso",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreatedAt",
                value: new DateTime(2025, 5, 28, 15, 53, 3, 954, DateTimeKind.Utc).AddTicks(2814));

            migrationBuilder.UpdateData(
                table: "CategoriasProgreso",
                keyColumn: "Id",
                keyValue: 6,
                column: "CreatedAt",
                value: new DateTime(2025, 5, 28, 15, 53, 3, 954, DateTimeKind.Utc).AddTicks(2814));

            migrationBuilder.UpdateData(
                table: "CategoriasProgreso",
                keyColumn: "Id",
                keyValue: 7,
                column: "CreatedAt",
                value: new DateTime(2025, 5, 28, 15, 53, 3, 954, DateTimeKind.Utc).AddTicks(2814));

            migrationBuilder.UpdateData(
                table: "CategoriasProgreso",
                keyColumn: "Id",
                keyValue: 8,
                column: "CreatedAt",
                value: new DateTime(2025, 5, 28, 15, 53, 3, 954, DateTimeKind.Utc).AddTicks(2814));

            migrationBuilder.UpdateData(
                table: "CategoriasProgreso",
                keyColumn: "Id",
                keyValue: 9,
                column: "CreatedAt",
                value: new DateTime(2025, 5, 28, 15, 53, 3, 954, DateTimeKind.Utc).AddTicks(2814));

            migrationBuilder.UpdateData(
                table: "CategoriasProgreso",
                keyColumn: "Id",
                keyValue: 10,
                column: "CreatedAt",
                value: new DateTime(2025, 5, 28, 15, 53, 3, 954, DateTimeKind.Utc).AddTicks(2814));

            migrationBuilder.UpdateData(
                table: "CategoriasProgreso",
                keyColumn: "Id",
                keyValue: 11,
                column: "CreatedAt",
                value: new DateTime(2025, 5, 28, 15, 53, 3, 954, DateTimeKind.Utc).AddTicks(2814));

            migrationBuilder.UpdateData(
                table: "CategoriasProgreso",
                keyColumn: "Id",
                keyValue: 12,
                column: "CreatedAt",
                value: new DateTime(2025, 5, 28, 15, 53, 3, 954, DateTimeKind.Utc).AddTicks(2814));

            migrationBuilder.UpdateData(
                table: "CategoriasProgreso",
                keyColumn: "Id",
                keyValue: 13,
                column: "CreatedAt",
                value: new DateTime(2025, 5, 28, 15, 53, 3, 954, DateTimeKind.Utc).AddTicks(2814));

            migrationBuilder.UpdateData(
                table: "CategoriasProgreso",
                keyColumn: "Id",
                keyValue: 14,
                column: "CreatedAt",
                value: new DateTime(2025, 5, 28, 15, 53, 3, 954, DateTimeKind.Utc).AddTicks(2814));

            migrationBuilder.UpdateData(
                table: "CategoriasProgreso",
                keyColumn: "Id",
                keyValue: 15,
                column: "CreatedAt",
                value: new DateTime(2025, 5, 28, 15, 53, 3, 954, DateTimeKind.Utc).AddTicks(2814));

            migrationBuilder.UpdateData(
                table: "FrecuenciasHabitos",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "FechaCreacion" },
                values: new object[] { new DateTime(2025, 5, 28, 15, 53, 3, 955, DateTimeKind.Utc).AddTicks(3635), new DateTime(2025, 5, 28, 15, 53, 3, 955, DateTimeKind.Utc).AddTicks(3706) });

            migrationBuilder.UpdateData(
                table: "FrecuenciasHabitos",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedAt", "FechaCreacion" },
                values: new object[] { new DateTime(2025, 5, 28, 15, 53, 3, 955, DateTimeKind.Utc).AddTicks(3635), new DateTime(2025, 5, 28, 15, 53, 3, 955, DateTimeKind.Utc).AddTicks(3710) });

            migrationBuilder.UpdateData(
                table: "FrecuenciasHabitos",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedAt", "FechaCreacion" },
                values: new object[] { new DateTime(2025, 5, 28, 15, 53, 3, 955, DateTimeKind.Utc).AddTicks(3635), new DateTime(2025, 5, 28, 15, 53, 3, 955, DateTimeKind.Utc).AddTicks(3713) });

            migrationBuilder.UpdateData(
                table: "FrecuenciasHabitos",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "CreatedAt", "FechaCreacion" },
                values: new object[] { new DateTime(2025, 5, 28, 15, 53, 3, 955, DateTimeKind.Utc).AddTicks(3635), new DateTime(2025, 5, 28, 15, 53, 3, 955, DateTimeKind.Utc).AddTicks(3714) });

            migrationBuilder.UpdateData(
                table: "FrecuenciasHabitos",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "CreatedAt", "FechaCreacion" },
                values: new object[] { new DateTime(2025, 5, 28, 15, 53, 3, 955, DateTimeKind.Utc).AddTicks(3635), new DateTime(2025, 5, 28, 15, 53, 3, 955, DateTimeKind.Utc).AddTicks(3716) });

            migrationBuilder.UpdateData(
                table: "FrecuenciasHabitos",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "CreatedAt", "FechaCreacion" },
                values: new object[] { new DateTime(2025, 5, 28, 15, 53, 3, 955, DateTimeKind.Utc).AddTicks(3635), new DateTime(2025, 5, 28, 15, 53, 3, 955, DateTimeKind.Utc).AddTicks(3718) });

            migrationBuilder.UpdateData(
                table: "LifeAreas",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 5, 28, 15, 53, 3, 952, DateTimeKind.Utc).AddTicks(668), new DateTime(2025, 5, 28, 15, 53, 3, 952, DateTimeKind.Utc).AddTicks(668) });

            migrationBuilder.UpdateData(
                table: "LifeAreas",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 5, 28, 15, 53, 3, 952, DateTimeKind.Utc).AddTicks(668), new DateTime(2025, 5, 28, 15, 53, 3, 952, DateTimeKind.Utc).AddTicks(668) });

            migrationBuilder.UpdateData(
                table: "LifeAreas",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 5, 28, 15, 53, 3, 952, DateTimeKind.Utc).AddTicks(668), new DateTime(2025, 5, 28, 15, 53, 3, 952, DateTimeKind.Utc).AddTicks(668) });

            migrationBuilder.UpdateData(
                table: "LifeAreas",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 5, 28, 15, 53, 3, 952, DateTimeKind.Utc).AddTicks(668), new DateTime(2025, 5, 28, 15, 53, 3, 952, DateTimeKind.Utc).AddTicks(668) });

            migrationBuilder.UpdateData(
                table: "LifeAreas",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 5, 28, 15, 53, 3, 952, DateTimeKind.Utc).AddTicks(668), new DateTime(2025, 5, 28, 15, 53, 3, 952, DateTimeKind.Utc).AddTicks(668) });

            migrationBuilder.UpdateData(
                table: "LifeAreas",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 5, 28, 15, 53, 3, 952, DateTimeKind.Utc).AddTicks(668), new DateTime(2025, 5, 28, 15, 53, 3, 952, DateTimeKind.Utc).AddTicks(668) });

            migrationBuilder.UpdateData(
                table: "LifeAreas",
                keyColumn: "Id",
                keyValue: 7,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 5, 28, 15, 53, 3, 952, DateTimeKind.Utc).AddTicks(668), new DateTime(2025, 5, 28, 15, 53, 3, 952, DateTimeKind.Utc).AddTicks(668) });

            migrationBuilder.UpdateData(
                table: "LifeAreas",
                keyColumn: "Id",
                keyValue: 8,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 5, 28, 15, 53, 3, 952, DateTimeKind.Utc).AddTicks(668), new DateTime(2025, 5, 28, 15, 53, 3, 952, DateTimeKind.Utc).AddTicks(668) });

            migrationBuilder.UpdateData(
                table: "LifeAreas",
                keyColumn: "Id",
                keyValue: 9,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 5, 28, 15, 53, 3, 952, DateTimeKind.Utc).AddTicks(668), new DateTime(2025, 5, 28, 15, 53, 3, 952, DateTimeKind.Utc).AddTicks(668) });

            migrationBuilder.UpdateData(
                table: "LifeAreas",
                keyColumn: "Id",
                keyValue: 10,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 5, 28, 15, 53, 3, 952, DateTimeKind.Utc).AddTicks(668), new DateTime(2025, 5, 28, 15, 53, 3, 952, DateTimeKind.Utc).AddTicks(668) });

            migrationBuilder.UpdateData(
                table: "Questions",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 5, 28, 15, 53, 3, 954, DateTimeKind.Utc).AddTicks(2711), new DateTime(2025, 5, 28, 15, 53, 3, 954, DateTimeKind.Utc).AddTicks(2711) });

            migrationBuilder.UpdateData(
                table: "Questions",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 5, 28, 15, 53, 3, 954, DateTimeKind.Utc).AddTicks(2711), new DateTime(2025, 5, 28, 15, 53, 3, 954, DateTimeKind.Utc).AddTicks(2711) });

            migrationBuilder.UpdateData(
                table: "Questions",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 5, 28, 15, 53, 3, 954, DateTimeKind.Utc).AddTicks(2711), new DateTime(2025, 5, 28, 15, 53, 3, 954, DateTimeKind.Utc).AddTicks(2711) });

            migrationBuilder.UpdateData(
                table: "Questions",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 5, 28, 15, 53, 3, 954, DateTimeKind.Utc).AddTicks(2711), new DateTime(2025, 5, 28, 15, 53, 3, 954, DateTimeKind.Utc).AddTicks(2711) });

            migrationBuilder.UpdateData(
                table: "Questions",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 5, 28, 15, 53, 3, 954, DateTimeKind.Utc).AddTicks(2711), new DateTime(2025, 5, 28, 15, 53, 3, 954, DateTimeKind.Utc).AddTicks(2711) });

            migrationBuilder.UpdateData(
                table: "Questions",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 5, 28, 15, 53, 3, 954, DateTimeKind.Utc).AddTicks(2711), new DateTime(2025, 5, 28, 15, 53, 3, 954, DateTimeKind.Utc).AddTicks(2711) });

            migrationBuilder.UpdateData(
                table: "Questions",
                keyColumn: "Id",
                keyValue: 7,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 5, 28, 15, 53, 3, 954, DateTimeKind.Utc).AddTicks(2711), new DateTime(2025, 5, 28, 15, 53, 3, 954, DateTimeKind.Utc).AddTicks(2711) });

            migrationBuilder.UpdateData(
                table: "Questions",
                keyColumn: "Id",
                keyValue: 8,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 5, 28, 15, 53, 3, 954, DateTimeKind.Utc).AddTicks(2711), new DateTime(2025, 5, 28, 15, 53, 3, 954, DateTimeKind.Utc).AddTicks(2711) });

            migrationBuilder.UpdateData(
                table: "Questions",
                keyColumn: "Id",
                keyValue: 9,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 5, 28, 15, 53, 3, 954, DateTimeKind.Utc).AddTicks(2711), new DateTime(2025, 5, 28, 15, 53, 3, 954, DateTimeKind.Utc).AddTicks(2711) });

            migrationBuilder.UpdateData(
                table: "Questions",
                keyColumn: "Id",
                keyValue: 10,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 5, 28, 15, 53, 3, 954, DateTimeKind.Utc).AddTicks(2711), new DateTime(2025, 5, 28, 15, 53, 3, 954, DateTimeKind.Utc).AddTicks(2711) });
        }
    }
}
