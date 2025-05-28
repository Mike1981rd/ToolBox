using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace ToolBox.Migrations
{
    /// <inheritdoc />
    public partial class CreateNotificationsTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Notifications",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UserId = table.Column<int>(type: "integer", nullable: false),
                    NotificationType = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Data = table.Column<string>(type: "TEXT", nullable: false),
                    ReadAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Notifications", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Notifications_Users_UserId",
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
                value: new DateTime(2025, 5, 27, 19, 14, 50, 166, DateTimeKind.Utc).AddTicks(5976));

            migrationBuilder.UpdateData(
                table: "AreasProgreso",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2025, 5, 27, 19, 14, 50, 166, DateTimeKind.Utc).AddTicks(5976));

            migrationBuilder.UpdateData(
                table: "AreasProgreso",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedAt",
                value: new DateTime(2025, 5, 27, 19, 14, 50, 166, DateTimeKind.Utc).AddTicks(5976));

            migrationBuilder.UpdateData(
                table: "AreasProgreso",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedAt",
                value: new DateTime(2025, 5, 27, 19, 14, 50, 166, DateTimeKind.Utc).AddTicks(5976));

            migrationBuilder.UpdateData(
                table: "AreasProgreso",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreatedAt",
                value: new DateTime(2025, 5, 27, 19, 14, 50, 166, DateTimeKind.Utc).AddTicks(5976));

            migrationBuilder.UpdateData(
                table: "CategoriasHabitos",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "FechaCreacion" },
                values: new object[] { new DateTime(2025, 5, 27, 19, 14, 50, 168, DateTimeKind.Utc).AddTicks(1535), new DateTime(2025, 5, 27, 19, 14, 50, 168, DateTimeKind.Utc).AddTicks(1539) });

            migrationBuilder.UpdateData(
                table: "CategoriasHabitos",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedAt", "FechaCreacion" },
                values: new object[] { new DateTime(2025, 5, 27, 19, 14, 50, 168, DateTimeKind.Utc).AddTicks(1535), new DateTime(2025, 5, 27, 19, 14, 50, 168, DateTimeKind.Utc).AddTicks(1546) });

            migrationBuilder.UpdateData(
                table: "CategoriasHabitos",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedAt", "FechaCreacion" },
                values: new object[] { new DateTime(2025, 5, 27, 19, 14, 50, 168, DateTimeKind.Utc).AddTicks(1535), new DateTime(2025, 5, 27, 19, 14, 50, 168, DateTimeKind.Utc).AddTicks(1549) });

            migrationBuilder.UpdateData(
                table: "CategoriasHabitos",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "CreatedAt", "FechaCreacion" },
                values: new object[] { new DateTime(2025, 5, 27, 19, 14, 50, 168, DateTimeKind.Utc).AddTicks(1535), new DateTime(2025, 5, 27, 19, 14, 50, 168, DateTimeKind.Utc).AddTicks(1560) });

            migrationBuilder.UpdateData(
                table: "CategoriasHabitos",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "CreatedAt", "FechaCreacion" },
                values: new object[] { new DateTime(2025, 5, 27, 19, 14, 50, 168, DateTimeKind.Utc).AddTicks(1535), new DateTime(2025, 5, 27, 19, 14, 50, 168, DateTimeKind.Utc).AddTicks(1562) });

            migrationBuilder.UpdateData(
                table: "CategoriasHabitos",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "CreatedAt", "FechaCreacion" },
                values: new object[] { new DateTime(2025, 5, 27, 19, 14, 50, 168, DateTimeKind.Utc).AddTicks(1535), new DateTime(2025, 5, 27, 19, 14, 50, 168, DateTimeKind.Utc).AddTicks(1564) });

            migrationBuilder.UpdateData(
                table: "CategoriasHabitos",
                keyColumn: "Id",
                keyValue: 7,
                columns: new[] { "CreatedAt", "FechaCreacion" },
                values: new object[] { new DateTime(2025, 5, 27, 19, 14, 50, 168, DateTimeKind.Utc).AddTicks(1535), new DateTime(2025, 5, 27, 19, 14, 50, 168, DateTimeKind.Utc).AddTicks(1565) });

            migrationBuilder.UpdateData(
                table: "CategoriasHabitos",
                keyColumn: "Id",
                keyValue: 8,
                columns: new[] { "CreatedAt", "FechaCreacion" },
                values: new object[] { new DateTime(2025, 5, 27, 19, 14, 50, 168, DateTimeKind.Utc).AddTicks(1535), new DateTime(2025, 5, 27, 19, 14, 50, 168, DateTimeKind.Utc).AddTicks(1567) });

            migrationBuilder.UpdateData(
                table: "CategoriasProgreso",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2025, 5, 27, 19, 14, 50, 166, DateTimeKind.Utc).AddTicks(5976));

            migrationBuilder.UpdateData(
                table: "CategoriasProgreso",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2025, 5, 27, 19, 14, 50, 166, DateTimeKind.Utc).AddTicks(5976));

            migrationBuilder.UpdateData(
                table: "CategoriasProgreso",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedAt",
                value: new DateTime(2025, 5, 27, 19, 14, 50, 166, DateTimeKind.Utc).AddTicks(5976));

            migrationBuilder.UpdateData(
                table: "CategoriasProgreso",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedAt",
                value: new DateTime(2025, 5, 27, 19, 14, 50, 166, DateTimeKind.Utc).AddTicks(5976));

            migrationBuilder.UpdateData(
                table: "CategoriasProgreso",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreatedAt",
                value: new DateTime(2025, 5, 27, 19, 14, 50, 166, DateTimeKind.Utc).AddTicks(5976));

            migrationBuilder.UpdateData(
                table: "CategoriasProgreso",
                keyColumn: "Id",
                keyValue: 6,
                column: "CreatedAt",
                value: new DateTime(2025, 5, 27, 19, 14, 50, 166, DateTimeKind.Utc).AddTicks(5976));

            migrationBuilder.UpdateData(
                table: "CategoriasProgreso",
                keyColumn: "Id",
                keyValue: 7,
                column: "CreatedAt",
                value: new DateTime(2025, 5, 27, 19, 14, 50, 166, DateTimeKind.Utc).AddTicks(5976));

            migrationBuilder.UpdateData(
                table: "CategoriasProgreso",
                keyColumn: "Id",
                keyValue: 8,
                column: "CreatedAt",
                value: new DateTime(2025, 5, 27, 19, 14, 50, 166, DateTimeKind.Utc).AddTicks(5976));

            migrationBuilder.UpdateData(
                table: "CategoriasProgreso",
                keyColumn: "Id",
                keyValue: 9,
                column: "CreatedAt",
                value: new DateTime(2025, 5, 27, 19, 14, 50, 166, DateTimeKind.Utc).AddTicks(5976));

            migrationBuilder.UpdateData(
                table: "CategoriasProgreso",
                keyColumn: "Id",
                keyValue: 10,
                column: "CreatedAt",
                value: new DateTime(2025, 5, 27, 19, 14, 50, 166, DateTimeKind.Utc).AddTicks(5976));

            migrationBuilder.UpdateData(
                table: "CategoriasProgreso",
                keyColumn: "Id",
                keyValue: 11,
                column: "CreatedAt",
                value: new DateTime(2025, 5, 27, 19, 14, 50, 166, DateTimeKind.Utc).AddTicks(5976));

            migrationBuilder.UpdateData(
                table: "CategoriasProgreso",
                keyColumn: "Id",
                keyValue: 12,
                column: "CreatedAt",
                value: new DateTime(2025, 5, 27, 19, 14, 50, 166, DateTimeKind.Utc).AddTicks(5976));

            migrationBuilder.UpdateData(
                table: "CategoriasProgreso",
                keyColumn: "Id",
                keyValue: 13,
                column: "CreatedAt",
                value: new DateTime(2025, 5, 27, 19, 14, 50, 166, DateTimeKind.Utc).AddTicks(5976));

            migrationBuilder.UpdateData(
                table: "CategoriasProgreso",
                keyColumn: "Id",
                keyValue: 14,
                column: "CreatedAt",
                value: new DateTime(2025, 5, 27, 19, 14, 50, 166, DateTimeKind.Utc).AddTicks(5976));

            migrationBuilder.UpdateData(
                table: "CategoriasProgreso",
                keyColumn: "Id",
                keyValue: 15,
                column: "CreatedAt",
                value: new DateTime(2025, 5, 27, 19, 14, 50, 166, DateTimeKind.Utc).AddTicks(5976));

            migrationBuilder.UpdateData(
                table: "FrecuenciasHabitos",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "FechaCreacion" },
                values: new object[] { new DateTime(2025, 5, 27, 19, 14, 50, 168, DateTimeKind.Utc).AddTicks(1535), new DateTime(2025, 5, 27, 19, 14, 50, 168, DateTimeKind.Utc).AddTicks(1570) });

            migrationBuilder.UpdateData(
                table: "FrecuenciasHabitos",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedAt", "FechaCreacion" },
                values: new object[] { new DateTime(2025, 5, 27, 19, 14, 50, 168, DateTimeKind.Utc).AddTicks(1535), new DateTime(2025, 5, 27, 19, 14, 50, 168, DateTimeKind.Utc).AddTicks(1575) });

            migrationBuilder.UpdateData(
                table: "FrecuenciasHabitos",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedAt", "FechaCreacion" },
                values: new object[] { new DateTime(2025, 5, 27, 19, 14, 50, 168, DateTimeKind.Utc).AddTicks(1535), new DateTime(2025, 5, 27, 19, 14, 50, 168, DateTimeKind.Utc).AddTicks(1577) });

            migrationBuilder.UpdateData(
                table: "FrecuenciasHabitos",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "CreatedAt", "FechaCreacion" },
                values: new object[] { new DateTime(2025, 5, 27, 19, 14, 50, 168, DateTimeKind.Utc).AddTicks(1535), new DateTime(2025, 5, 27, 19, 14, 50, 168, DateTimeKind.Utc).AddTicks(1579) });

            migrationBuilder.UpdateData(
                table: "FrecuenciasHabitos",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "CreatedAt", "FechaCreacion" },
                values: new object[] { new DateTime(2025, 5, 27, 19, 14, 50, 168, DateTimeKind.Utc).AddTicks(1535), new DateTime(2025, 5, 27, 19, 14, 50, 168, DateTimeKind.Utc).AddTicks(1581) });

            migrationBuilder.UpdateData(
                table: "FrecuenciasHabitos",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "CreatedAt", "FechaCreacion" },
                values: new object[] { new DateTime(2025, 5, 27, 19, 14, 50, 168, DateTimeKind.Utc).AddTicks(1535), new DateTime(2025, 5, 27, 19, 14, 50, 168, DateTimeKind.Utc).AddTicks(1582) });

            migrationBuilder.UpdateData(
                table: "LifeAreas",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 5, 27, 19, 14, 50, 164, DateTimeKind.Utc).AddTicks(2008), new DateTime(2025, 5, 27, 19, 14, 50, 164, DateTimeKind.Utc).AddTicks(2008) });

            migrationBuilder.UpdateData(
                table: "LifeAreas",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 5, 27, 19, 14, 50, 164, DateTimeKind.Utc).AddTicks(2008), new DateTime(2025, 5, 27, 19, 14, 50, 164, DateTimeKind.Utc).AddTicks(2008) });

            migrationBuilder.UpdateData(
                table: "LifeAreas",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 5, 27, 19, 14, 50, 164, DateTimeKind.Utc).AddTicks(2008), new DateTime(2025, 5, 27, 19, 14, 50, 164, DateTimeKind.Utc).AddTicks(2008) });

            migrationBuilder.UpdateData(
                table: "LifeAreas",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 5, 27, 19, 14, 50, 164, DateTimeKind.Utc).AddTicks(2008), new DateTime(2025, 5, 27, 19, 14, 50, 164, DateTimeKind.Utc).AddTicks(2008) });

            migrationBuilder.UpdateData(
                table: "LifeAreas",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 5, 27, 19, 14, 50, 164, DateTimeKind.Utc).AddTicks(2008), new DateTime(2025, 5, 27, 19, 14, 50, 164, DateTimeKind.Utc).AddTicks(2008) });

            migrationBuilder.UpdateData(
                table: "LifeAreas",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 5, 27, 19, 14, 50, 164, DateTimeKind.Utc).AddTicks(2008), new DateTime(2025, 5, 27, 19, 14, 50, 164, DateTimeKind.Utc).AddTicks(2008) });

            migrationBuilder.UpdateData(
                table: "LifeAreas",
                keyColumn: "Id",
                keyValue: 7,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 5, 27, 19, 14, 50, 164, DateTimeKind.Utc).AddTicks(2008), new DateTime(2025, 5, 27, 19, 14, 50, 164, DateTimeKind.Utc).AddTicks(2008) });

            migrationBuilder.UpdateData(
                table: "LifeAreas",
                keyColumn: "Id",
                keyValue: 8,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 5, 27, 19, 14, 50, 164, DateTimeKind.Utc).AddTicks(2008), new DateTime(2025, 5, 27, 19, 14, 50, 164, DateTimeKind.Utc).AddTicks(2008) });

            migrationBuilder.UpdateData(
                table: "LifeAreas",
                keyColumn: "Id",
                keyValue: 9,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 5, 27, 19, 14, 50, 164, DateTimeKind.Utc).AddTicks(2008), new DateTime(2025, 5, 27, 19, 14, 50, 164, DateTimeKind.Utc).AddTicks(2008) });

            migrationBuilder.UpdateData(
                table: "LifeAreas",
                keyColumn: "Id",
                keyValue: 10,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 5, 27, 19, 14, 50, 164, DateTimeKind.Utc).AddTicks(2008), new DateTime(2025, 5, 27, 19, 14, 50, 164, DateTimeKind.Utc).AddTicks(2008) });

            migrationBuilder.UpdateData(
                table: "Questions",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 5, 27, 19, 14, 50, 166, DateTimeKind.Utc).AddTicks(5845), new DateTime(2025, 5, 27, 19, 14, 50, 166, DateTimeKind.Utc).AddTicks(5845) });

            migrationBuilder.UpdateData(
                table: "Questions",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 5, 27, 19, 14, 50, 166, DateTimeKind.Utc).AddTicks(5845), new DateTime(2025, 5, 27, 19, 14, 50, 166, DateTimeKind.Utc).AddTicks(5845) });

            migrationBuilder.UpdateData(
                table: "Questions",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 5, 27, 19, 14, 50, 166, DateTimeKind.Utc).AddTicks(5845), new DateTime(2025, 5, 27, 19, 14, 50, 166, DateTimeKind.Utc).AddTicks(5845) });

            migrationBuilder.UpdateData(
                table: "Questions",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 5, 27, 19, 14, 50, 166, DateTimeKind.Utc).AddTicks(5845), new DateTime(2025, 5, 27, 19, 14, 50, 166, DateTimeKind.Utc).AddTicks(5845) });

            migrationBuilder.UpdateData(
                table: "Questions",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 5, 27, 19, 14, 50, 166, DateTimeKind.Utc).AddTicks(5845), new DateTime(2025, 5, 27, 19, 14, 50, 166, DateTimeKind.Utc).AddTicks(5845) });

            migrationBuilder.UpdateData(
                table: "Questions",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 5, 27, 19, 14, 50, 166, DateTimeKind.Utc).AddTicks(5845), new DateTime(2025, 5, 27, 19, 14, 50, 166, DateTimeKind.Utc).AddTicks(5845) });

            migrationBuilder.UpdateData(
                table: "Questions",
                keyColumn: "Id",
                keyValue: 7,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 5, 27, 19, 14, 50, 166, DateTimeKind.Utc).AddTicks(5845), new DateTime(2025, 5, 27, 19, 14, 50, 166, DateTimeKind.Utc).AddTicks(5845) });

            migrationBuilder.UpdateData(
                table: "Questions",
                keyColumn: "Id",
                keyValue: 8,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 5, 27, 19, 14, 50, 166, DateTimeKind.Utc).AddTicks(5845), new DateTime(2025, 5, 27, 19, 14, 50, 166, DateTimeKind.Utc).AddTicks(5845) });

            migrationBuilder.UpdateData(
                table: "Questions",
                keyColumn: "Id",
                keyValue: 9,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 5, 27, 19, 14, 50, 166, DateTimeKind.Utc).AddTicks(5845), new DateTime(2025, 5, 27, 19, 14, 50, 166, DateTimeKind.Utc).AddTicks(5845) });

            migrationBuilder.UpdateData(
                table: "Questions",
                keyColumn: "Id",
                keyValue: 10,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 5, 27, 19, 14, 50, 166, DateTimeKind.Utc).AddTicks(5845), new DateTime(2025, 5, 27, 19, 14, 50, 166, DateTimeKind.Utc).AddTicks(5845) });

            migrationBuilder.CreateIndex(
                name: "IX_Notifications_UserId_CreatedAt",
                table: "Notifications",
                columns: new[] { "UserId", "CreatedAt" });

            migrationBuilder.CreateIndex(
                name: "IX_Notifications_UserId_ReadAt",
                table: "Notifications",
                columns: new[] { "UserId", "ReadAt" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Notifications");

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
        }
    }
}
