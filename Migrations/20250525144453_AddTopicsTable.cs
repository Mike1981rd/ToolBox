using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace ToolBox.Migrations
{
    /// <inheritdoc />
    public partial class AddTopicsTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Topics",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    CreatedByUserId = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Topics", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Topics_Users_CreatedByUserId",
                        column: x => x.CreatedByUserId,
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.UpdateData(
                table: "AreasProgreso",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2025, 5, 25, 14, 44, 52, 100, DateTimeKind.Utc).AddTicks(5570));

            migrationBuilder.UpdateData(
                table: "AreasProgreso",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2025, 5, 25, 14, 44, 52, 100, DateTimeKind.Utc).AddTicks(5570));

            migrationBuilder.UpdateData(
                table: "AreasProgreso",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedAt",
                value: new DateTime(2025, 5, 25, 14, 44, 52, 100, DateTimeKind.Utc).AddTicks(5570));

            migrationBuilder.UpdateData(
                table: "AreasProgreso",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedAt",
                value: new DateTime(2025, 5, 25, 14, 44, 52, 100, DateTimeKind.Utc).AddTicks(5570));

            migrationBuilder.UpdateData(
                table: "AreasProgreso",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreatedAt",
                value: new DateTime(2025, 5, 25, 14, 44, 52, 100, DateTimeKind.Utc).AddTicks(5570));

            migrationBuilder.UpdateData(
                table: "CategoriasHabitos",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "FechaCreacion" },
                values: new object[] { new DateTime(2025, 5, 25, 14, 44, 52, 101, DateTimeKind.Utc).AddTicks(6441), new DateTime(2025, 5, 25, 14, 44, 52, 101, DateTimeKind.Utc).AddTicks(6444) });

            migrationBuilder.UpdateData(
                table: "CategoriasHabitos",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedAt", "FechaCreacion" },
                values: new object[] { new DateTime(2025, 5, 25, 14, 44, 52, 101, DateTimeKind.Utc).AddTicks(6441), new DateTime(2025, 5, 25, 14, 44, 52, 101, DateTimeKind.Utc).AddTicks(6454) });

            migrationBuilder.UpdateData(
                table: "CategoriasHabitos",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedAt", "FechaCreacion" },
                values: new object[] { new DateTime(2025, 5, 25, 14, 44, 52, 101, DateTimeKind.Utc).AddTicks(6441), new DateTime(2025, 5, 25, 14, 44, 52, 101, DateTimeKind.Utc).AddTicks(6457) });

            migrationBuilder.UpdateData(
                table: "CategoriasHabitos",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "CreatedAt", "FechaCreacion" },
                values: new object[] { new DateTime(2025, 5, 25, 14, 44, 52, 101, DateTimeKind.Utc).AddTicks(6441), new DateTime(2025, 5, 25, 14, 44, 52, 101, DateTimeKind.Utc).AddTicks(6459) });

            migrationBuilder.UpdateData(
                table: "CategoriasHabitos",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "CreatedAt", "FechaCreacion" },
                values: new object[] { new DateTime(2025, 5, 25, 14, 44, 52, 101, DateTimeKind.Utc).AddTicks(6441), new DateTime(2025, 5, 25, 14, 44, 52, 101, DateTimeKind.Utc).AddTicks(6461) });

            migrationBuilder.UpdateData(
                table: "CategoriasHabitos",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "CreatedAt", "FechaCreacion" },
                values: new object[] { new DateTime(2025, 5, 25, 14, 44, 52, 101, DateTimeKind.Utc).AddTicks(6441), new DateTime(2025, 5, 25, 14, 44, 52, 101, DateTimeKind.Utc).AddTicks(6462) });

            migrationBuilder.UpdateData(
                table: "CategoriasHabitos",
                keyColumn: "Id",
                keyValue: 7,
                columns: new[] { "CreatedAt", "FechaCreacion" },
                values: new object[] { new DateTime(2025, 5, 25, 14, 44, 52, 101, DateTimeKind.Utc).AddTicks(6441), new DateTime(2025, 5, 25, 14, 44, 52, 101, DateTimeKind.Utc).AddTicks(6464) });

            migrationBuilder.UpdateData(
                table: "CategoriasHabitos",
                keyColumn: "Id",
                keyValue: 8,
                columns: new[] { "CreatedAt", "FechaCreacion" },
                values: new object[] { new DateTime(2025, 5, 25, 14, 44, 52, 101, DateTimeKind.Utc).AddTicks(6441), new DateTime(2025, 5, 25, 14, 44, 52, 101, DateTimeKind.Utc).AddTicks(6466) });

            migrationBuilder.UpdateData(
                table: "CategoriasProgreso",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2025, 5, 25, 14, 44, 52, 100, DateTimeKind.Utc).AddTicks(5570));

            migrationBuilder.UpdateData(
                table: "CategoriasProgreso",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2025, 5, 25, 14, 44, 52, 100, DateTimeKind.Utc).AddTicks(5570));

            migrationBuilder.UpdateData(
                table: "CategoriasProgreso",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedAt",
                value: new DateTime(2025, 5, 25, 14, 44, 52, 100, DateTimeKind.Utc).AddTicks(5570));

            migrationBuilder.UpdateData(
                table: "CategoriasProgreso",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedAt",
                value: new DateTime(2025, 5, 25, 14, 44, 52, 100, DateTimeKind.Utc).AddTicks(5570));

            migrationBuilder.UpdateData(
                table: "CategoriasProgreso",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreatedAt",
                value: new DateTime(2025, 5, 25, 14, 44, 52, 100, DateTimeKind.Utc).AddTicks(5570));

            migrationBuilder.UpdateData(
                table: "CategoriasProgreso",
                keyColumn: "Id",
                keyValue: 6,
                column: "CreatedAt",
                value: new DateTime(2025, 5, 25, 14, 44, 52, 100, DateTimeKind.Utc).AddTicks(5570));

            migrationBuilder.UpdateData(
                table: "CategoriasProgreso",
                keyColumn: "Id",
                keyValue: 7,
                column: "CreatedAt",
                value: new DateTime(2025, 5, 25, 14, 44, 52, 100, DateTimeKind.Utc).AddTicks(5570));

            migrationBuilder.UpdateData(
                table: "CategoriasProgreso",
                keyColumn: "Id",
                keyValue: 8,
                column: "CreatedAt",
                value: new DateTime(2025, 5, 25, 14, 44, 52, 100, DateTimeKind.Utc).AddTicks(5570));

            migrationBuilder.UpdateData(
                table: "CategoriasProgreso",
                keyColumn: "Id",
                keyValue: 9,
                column: "CreatedAt",
                value: new DateTime(2025, 5, 25, 14, 44, 52, 100, DateTimeKind.Utc).AddTicks(5570));

            migrationBuilder.UpdateData(
                table: "CategoriasProgreso",
                keyColumn: "Id",
                keyValue: 10,
                column: "CreatedAt",
                value: new DateTime(2025, 5, 25, 14, 44, 52, 100, DateTimeKind.Utc).AddTicks(5570));

            migrationBuilder.UpdateData(
                table: "CategoriasProgreso",
                keyColumn: "Id",
                keyValue: 11,
                column: "CreatedAt",
                value: new DateTime(2025, 5, 25, 14, 44, 52, 100, DateTimeKind.Utc).AddTicks(5570));

            migrationBuilder.UpdateData(
                table: "CategoriasProgreso",
                keyColumn: "Id",
                keyValue: 12,
                column: "CreatedAt",
                value: new DateTime(2025, 5, 25, 14, 44, 52, 100, DateTimeKind.Utc).AddTicks(5570));

            migrationBuilder.UpdateData(
                table: "CategoriasProgreso",
                keyColumn: "Id",
                keyValue: 13,
                column: "CreatedAt",
                value: new DateTime(2025, 5, 25, 14, 44, 52, 100, DateTimeKind.Utc).AddTicks(5570));

            migrationBuilder.UpdateData(
                table: "CategoriasProgreso",
                keyColumn: "Id",
                keyValue: 14,
                column: "CreatedAt",
                value: new DateTime(2025, 5, 25, 14, 44, 52, 100, DateTimeKind.Utc).AddTicks(5570));

            migrationBuilder.UpdateData(
                table: "CategoriasProgreso",
                keyColumn: "Id",
                keyValue: 15,
                column: "CreatedAt",
                value: new DateTime(2025, 5, 25, 14, 44, 52, 100, DateTimeKind.Utc).AddTicks(5570));

            migrationBuilder.UpdateData(
                table: "FrecuenciasHabitos",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "FechaCreacion" },
                values: new object[] { new DateTime(2025, 5, 25, 14, 44, 52, 101, DateTimeKind.Utc).AddTicks(6441), new DateTime(2025, 5, 25, 14, 44, 52, 101, DateTimeKind.Utc).AddTicks(6470) });

            migrationBuilder.UpdateData(
                table: "FrecuenciasHabitos",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedAt", "FechaCreacion" },
                values: new object[] { new DateTime(2025, 5, 25, 14, 44, 52, 101, DateTimeKind.Utc).AddTicks(6441), new DateTime(2025, 5, 25, 14, 44, 52, 101, DateTimeKind.Utc).AddTicks(6475) });

            migrationBuilder.UpdateData(
                table: "FrecuenciasHabitos",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedAt", "FechaCreacion" },
                values: new object[] { new DateTime(2025, 5, 25, 14, 44, 52, 101, DateTimeKind.Utc).AddTicks(6441), new DateTime(2025, 5, 25, 14, 44, 52, 101, DateTimeKind.Utc).AddTicks(6477) });

            migrationBuilder.UpdateData(
                table: "FrecuenciasHabitos",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "CreatedAt", "FechaCreacion" },
                values: new object[] { new DateTime(2025, 5, 25, 14, 44, 52, 101, DateTimeKind.Utc).AddTicks(6441), new DateTime(2025, 5, 25, 14, 44, 52, 101, DateTimeKind.Utc).AddTicks(6479) });

            migrationBuilder.UpdateData(
                table: "FrecuenciasHabitos",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "CreatedAt", "FechaCreacion" },
                values: new object[] { new DateTime(2025, 5, 25, 14, 44, 52, 101, DateTimeKind.Utc).AddTicks(6441), new DateTime(2025, 5, 25, 14, 44, 52, 101, DateTimeKind.Utc).AddTicks(6481) });

            migrationBuilder.UpdateData(
                table: "FrecuenciasHabitos",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "CreatedAt", "FechaCreacion" },
                values: new object[] { new DateTime(2025, 5, 25, 14, 44, 52, 101, DateTimeKind.Utc).AddTicks(6441), new DateTime(2025, 5, 25, 14, 44, 52, 101, DateTimeKind.Utc).AddTicks(6483) });

            migrationBuilder.UpdateData(
                table: "LifeAreas",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 5, 25, 14, 44, 52, 99, DateTimeKind.Utc).AddTicks(672), new DateTime(2025, 5, 25, 14, 44, 52, 99, DateTimeKind.Utc).AddTicks(672) });

            migrationBuilder.UpdateData(
                table: "LifeAreas",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 5, 25, 14, 44, 52, 99, DateTimeKind.Utc).AddTicks(672), new DateTime(2025, 5, 25, 14, 44, 52, 99, DateTimeKind.Utc).AddTicks(672) });

            migrationBuilder.UpdateData(
                table: "LifeAreas",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 5, 25, 14, 44, 52, 99, DateTimeKind.Utc).AddTicks(672), new DateTime(2025, 5, 25, 14, 44, 52, 99, DateTimeKind.Utc).AddTicks(672) });

            migrationBuilder.UpdateData(
                table: "LifeAreas",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 5, 25, 14, 44, 52, 99, DateTimeKind.Utc).AddTicks(672), new DateTime(2025, 5, 25, 14, 44, 52, 99, DateTimeKind.Utc).AddTicks(672) });

            migrationBuilder.UpdateData(
                table: "LifeAreas",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 5, 25, 14, 44, 52, 99, DateTimeKind.Utc).AddTicks(672), new DateTime(2025, 5, 25, 14, 44, 52, 99, DateTimeKind.Utc).AddTicks(672) });

            migrationBuilder.UpdateData(
                table: "LifeAreas",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 5, 25, 14, 44, 52, 99, DateTimeKind.Utc).AddTicks(672), new DateTime(2025, 5, 25, 14, 44, 52, 99, DateTimeKind.Utc).AddTicks(672) });

            migrationBuilder.UpdateData(
                table: "LifeAreas",
                keyColumn: "Id",
                keyValue: 7,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 5, 25, 14, 44, 52, 99, DateTimeKind.Utc).AddTicks(672), new DateTime(2025, 5, 25, 14, 44, 52, 99, DateTimeKind.Utc).AddTicks(672) });

            migrationBuilder.UpdateData(
                table: "LifeAreas",
                keyColumn: "Id",
                keyValue: 8,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 5, 25, 14, 44, 52, 99, DateTimeKind.Utc).AddTicks(672), new DateTime(2025, 5, 25, 14, 44, 52, 99, DateTimeKind.Utc).AddTicks(672) });

            migrationBuilder.UpdateData(
                table: "LifeAreas",
                keyColumn: "Id",
                keyValue: 9,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 5, 25, 14, 44, 52, 99, DateTimeKind.Utc).AddTicks(672), new DateTime(2025, 5, 25, 14, 44, 52, 99, DateTimeKind.Utc).AddTicks(672) });

            migrationBuilder.UpdateData(
                table: "LifeAreas",
                keyColumn: "Id",
                keyValue: 10,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 5, 25, 14, 44, 52, 99, DateTimeKind.Utc).AddTicks(672), new DateTime(2025, 5, 25, 14, 44, 52, 99, DateTimeKind.Utc).AddTicks(672) });

            migrationBuilder.UpdateData(
                table: "Questions",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 5, 25, 14, 44, 52, 100, DateTimeKind.Utc).AddTicks(5480), new DateTime(2025, 5, 25, 14, 44, 52, 100, DateTimeKind.Utc).AddTicks(5480) });

            migrationBuilder.UpdateData(
                table: "Questions",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 5, 25, 14, 44, 52, 100, DateTimeKind.Utc).AddTicks(5480), new DateTime(2025, 5, 25, 14, 44, 52, 100, DateTimeKind.Utc).AddTicks(5480) });

            migrationBuilder.UpdateData(
                table: "Questions",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 5, 25, 14, 44, 52, 100, DateTimeKind.Utc).AddTicks(5480), new DateTime(2025, 5, 25, 14, 44, 52, 100, DateTimeKind.Utc).AddTicks(5480) });

            migrationBuilder.UpdateData(
                table: "Questions",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 5, 25, 14, 44, 52, 100, DateTimeKind.Utc).AddTicks(5480), new DateTime(2025, 5, 25, 14, 44, 52, 100, DateTimeKind.Utc).AddTicks(5480) });

            migrationBuilder.UpdateData(
                table: "Questions",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 5, 25, 14, 44, 52, 100, DateTimeKind.Utc).AddTicks(5480), new DateTime(2025, 5, 25, 14, 44, 52, 100, DateTimeKind.Utc).AddTicks(5480) });

            migrationBuilder.UpdateData(
                table: "Questions",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 5, 25, 14, 44, 52, 100, DateTimeKind.Utc).AddTicks(5480), new DateTime(2025, 5, 25, 14, 44, 52, 100, DateTimeKind.Utc).AddTicks(5480) });

            migrationBuilder.UpdateData(
                table: "Questions",
                keyColumn: "Id",
                keyValue: 7,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 5, 25, 14, 44, 52, 100, DateTimeKind.Utc).AddTicks(5480), new DateTime(2025, 5, 25, 14, 44, 52, 100, DateTimeKind.Utc).AddTicks(5480) });

            migrationBuilder.UpdateData(
                table: "Questions",
                keyColumn: "Id",
                keyValue: 8,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 5, 25, 14, 44, 52, 100, DateTimeKind.Utc).AddTicks(5480), new DateTime(2025, 5, 25, 14, 44, 52, 100, DateTimeKind.Utc).AddTicks(5480) });

            migrationBuilder.UpdateData(
                table: "Questions",
                keyColumn: "Id",
                keyValue: 9,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 5, 25, 14, 44, 52, 100, DateTimeKind.Utc).AddTicks(5480), new DateTime(2025, 5, 25, 14, 44, 52, 100, DateTimeKind.Utc).AddTicks(5480) });

            migrationBuilder.UpdateData(
                table: "Questions",
                keyColumn: "Id",
                keyValue: 10,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 5, 25, 14, 44, 52, 100, DateTimeKind.Utc).AddTicks(5480), new DateTime(2025, 5, 25, 14, 44, 52, 100, DateTimeKind.Utc).AddTicks(5480) });

            migrationBuilder.CreateIndex(
                name: "IX_Topics_CreatedByUserId",
                table: "Topics",
                column: "CreatedByUserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Topics");

            migrationBuilder.UpdateData(
                table: "AreasProgreso",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2025, 5, 25, 14, 8, 15, 41, DateTimeKind.Utc).AddTicks(1554));

            migrationBuilder.UpdateData(
                table: "AreasProgreso",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2025, 5, 25, 14, 8, 15, 41, DateTimeKind.Utc).AddTicks(1554));

            migrationBuilder.UpdateData(
                table: "AreasProgreso",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedAt",
                value: new DateTime(2025, 5, 25, 14, 8, 15, 41, DateTimeKind.Utc).AddTicks(1554));

            migrationBuilder.UpdateData(
                table: "AreasProgreso",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedAt",
                value: new DateTime(2025, 5, 25, 14, 8, 15, 41, DateTimeKind.Utc).AddTicks(1554));

            migrationBuilder.UpdateData(
                table: "AreasProgreso",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreatedAt",
                value: new DateTime(2025, 5, 25, 14, 8, 15, 41, DateTimeKind.Utc).AddTicks(1554));

            migrationBuilder.UpdateData(
                table: "CategoriasHabitos",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "FechaCreacion" },
                values: new object[] { new DateTime(2025, 5, 25, 14, 8, 15, 42, DateTimeKind.Utc).AddTicks(2995), new DateTime(2025, 5, 25, 14, 8, 15, 42, DateTimeKind.Utc).AddTicks(3000) });

            migrationBuilder.UpdateData(
                table: "CategoriasHabitos",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedAt", "FechaCreacion" },
                values: new object[] { new DateTime(2025, 5, 25, 14, 8, 15, 42, DateTimeKind.Utc).AddTicks(2995), new DateTime(2025, 5, 25, 14, 8, 15, 42, DateTimeKind.Utc).AddTicks(3005) });

            migrationBuilder.UpdateData(
                table: "CategoriasHabitos",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedAt", "FechaCreacion" },
                values: new object[] { new DateTime(2025, 5, 25, 14, 8, 15, 42, DateTimeKind.Utc).AddTicks(2995), new DateTime(2025, 5, 25, 14, 8, 15, 42, DateTimeKind.Utc).AddTicks(3008) });

            migrationBuilder.UpdateData(
                table: "CategoriasHabitos",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "CreatedAt", "FechaCreacion" },
                values: new object[] { new DateTime(2025, 5, 25, 14, 8, 15, 42, DateTimeKind.Utc).AddTicks(2995), new DateTime(2025, 5, 25, 14, 8, 15, 42, DateTimeKind.Utc).AddTicks(3010) });

            migrationBuilder.UpdateData(
                table: "CategoriasHabitos",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "CreatedAt", "FechaCreacion" },
                values: new object[] { new DateTime(2025, 5, 25, 14, 8, 15, 42, DateTimeKind.Utc).AddTicks(2995), new DateTime(2025, 5, 25, 14, 8, 15, 42, DateTimeKind.Utc).AddTicks(3012) });

            migrationBuilder.UpdateData(
                table: "CategoriasHabitos",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "CreatedAt", "FechaCreacion" },
                values: new object[] { new DateTime(2025, 5, 25, 14, 8, 15, 42, DateTimeKind.Utc).AddTicks(2995), new DateTime(2025, 5, 25, 14, 8, 15, 42, DateTimeKind.Utc).AddTicks(3014) });

            migrationBuilder.UpdateData(
                table: "CategoriasHabitos",
                keyColumn: "Id",
                keyValue: 7,
                columns: new[] { "CreatedAt", "FechaCreacion" },
                values: new object[] { new DateTime(2025, 5, 25, 14, 8, 15, 42, DateTimeKind.Utc).AddTicks(2995), new DateTime(2025, 5, 25, 14, 8, 15, 42, DateTimeKind.Utc).AddTicks(3016) });

            migrationBuilder.UpdateData(
                table: "CategoriasHabitos",
                keyColumn: "Id",
                keyValue: 8,
                columns: new[] { "CreatedAt", "FechaCreacion" },
                values: new object[] { new DateTime(2025, 5, 25, 14, 8, 15, 42, DateTimeKind.Utc).AddTicks(2995), new DateTime(2025, 5, 25, 14, 8, 15, 42, DateTimeKind.Utc).AddTicks(3018) });

            migrationBuilder.UpdateData(
                table: "CategoriasProgreso",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2025, 5, 25, 14, 8, 15, 41, DateTimeKind.Utc).AddTicks(1554));

            migrationBuilder.UpdateData(
                table: "CategoriasProgreso",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2025, 5, 25, 14, 8, 15, 41, DateTimeKind.Utc).AddTicks(1554));

            migrationBuilder.UpdateData(
                table: "CategoriasProgreso",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedAt",
                value: new DateTime(2025, 5, 25, 14, 8, 15, 41, DateTimeKind.Utc).AddTicks(1554));

            migrationBuilder.UpdateData(
                table: "CategoriasProgreso",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedAt",
                value: new DateTime(2025, 5, 25, 14, 8, 15, 41, DateTimeKind.Utc).AddTicks(1554));

            migrationBuilder.UpdateData(
                table: "CategoriasProgreso",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreatedAt",
                value: new DateTime(2025, 5, 25, 14, 8, 15, 41, DateTimeKind.Utc).AddTicks(1554));

            migrationBuilder.UpdateData(
                table: "CategoriasProgreso",
                keyColumn: "Id",
                keyValue: 6,
                column: "CreatedAt",
                value: new DateTime(2025, 5, 25, 14, 8, 15, 41, DateTimeKind.Utc).AddTicks(1554));

            migrationBuilder.UpdateData(
                table: "CategoriasProgreso",
                keyColumn: "Id",
                keyValue: 7,
                column: "CreatedAt",
                value: new DateTime(2025, 5, 25, 14, 8, 15, 41, DateTimeKind.Utc).AddTicks(1554));

            migrationBuilder.UpdateData(
                table: "CategoriasProgreso",
                keyColumn: "Id",
                keyValue: 8,
                column: "CreatedAt",
                value: new DateTime(2025, 5, 25, 14, 8, 15, 41, DateTimeKind.Utc).AddTicks(1554));

            migrationBuilder.UpdateData(
                table: "CategoriasProgreso",
                keyColumn: "Id",
                keyValue: 9,
                column: "CreatedAt",
                value: new DateTime(2025, 5, 25, 14, 8, 15, 41, DateTimeKind.Utc).AddTicks(1554));

            migrationBuilder.UpdateData(
                table: "CategoriasProgreso",
                keyColumn: "Id",
                keyValue: 10,
                column: "CreatedAt",
                value: new DateTime(2025, 5, 25, 14, 8, 15, 41, DateTimeKind.Utc).AddTicks(1554));

            migrationBuilder.UpdateData(
                table: "CategoriasProgreso",
                keyColumn: "Id",
                keyValue: 11,
                column: "CreatedAt",
                value: new DateTime(2025, 5, 25, 14, 8, 15, 41, DateTimeKind.Utc).AddTicks(1554));

            migrationBuilder.UpdateData(
                table: "CategoriasProgreso",
                keyColumn: "Id",
                keyValue: 12,
                column: "CreatedAt",
                value: new DateTime(2025, 5, 25, 14, 8, 15, 41, DateTimeKind.Utc).AddTicks(1554));

            migrationBuilder.UpdateData(
                table: "CategoriasProgreso",
                keyColumn: "Id",
                keyValue: 13,
                column: "CreatedAt",
                value: new DateTime(2025, 5, 25, 14, 8, 15, 41, DateTimeKind.Utc).AddTicks(1554));

            migrationBuilder.UpdateData(
                table: "CategoriasProgreso",
                keyColumn: "Id",
                keyValue: 14,
                column: "CreatedAt",
                value: new DateTime(2025, 5, 25, 14, 8, 15, 41, DateTimeKind.Utc).AddTicks(1554));

            migrationBuilder.UpdateData(
                table: "CategoriasProgreso",
                keyColumn: "Id",
                keyValue: 15,
                column: "CreatedAt",
                value: new DateTime(2025, 5, 25, 14, 8, 15, 41, DateTimeKind.Utc).AddTicks(1554));

            migrationBuilder.UpdateData(
                table: "FrecuenciasHabitos",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "FechaCreacion" },
                values: new object[] { new DateTime(2025, 5, 25, 14, 8, 15, 42, DateTimeKind.Utc).AddTicks(2995), new DateTime(2025, 5, 25, 14, 8, 15, 42, DateTimeKind.Utc).AddTicks(3022) });

            migrationBuilder.UpdateData(
                table: "FrecuenciasHabitos",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedAt", "FechaCreacion" },
                values: new object[] { new DateTime(2025, 5, 25, 14, 8, 15, 42, DateTimeKind.Utc).AddTicks(2995), new DateTime(2025, 5, 25, 14, 8, 15, 42, DateTimeKind.Utc).AddTicks(3028) });

            migrationBuilder.UpdateData(
                table: "FrecuenciasHabitos",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedAt", "FechaCreacion" },
                values: new object[] { new DateTime(2025, 5, 25, 14, 8, 15, 42, DateTimeKind.Utc).AddTicks(2995), new DateTime(2025, 5, 25, 14, 8, 15, 42, DateTimeKind.Utc).AddTicks(3030) });

            migrationBuilder.UpdateData(
                table: "FrecuenciasHabitos",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "CreatedAt", "FechaCreacion" },
                values: new object[] { new DateTime(2025, 5, 25, 14, 8, 15, 42, DateTimeKind.Utc).AddTicks(2995), new DateTime(2025, 5, 25, 14, 8, 15, 42, DateTimeKind.Utc).AddTicks(3032) });

            migrationBuilder.UpdateData(
                table: "FrecuenciasHabitos",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "CreatedAt", "FechaCreacion" },
                values: new object[] { new DateTime(2025, 5, 25, 14, 8, 15, 42, DateTimeKind.Utc).AddTicks(2995), new DateTime(2025, 5, 25, 14, 8, 15, 42, DateTimeKind.Utc).AddTicks(3034) });

            migrationBuilder.UpdateData(
                table: "FrecuenciasHabitos",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "CreatedAt", "FechaCreacion" },
                values: new object[] { new DateTime(2025, 5, 25, 14, 8, 15, 42, DateTimeKind.Utc).AddTicks(2995), new DateTime(2025, 5, 25, 14, 8, 15, 42, DateTimeKind.Utc).AddTicks(3036) });

            migrationBuilder.UpdateData(
                table: "LifeAreas",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 5, 25, 14, 8, 15, 38, DateTimeKind.Utc).AddTicks(1206), new DateTime(2025, 5, 25, 14, 8, 15, 38, DateTimeKind.Utc).AddTicks(1206) });

            migrationBuilder.UpdateData(
                table: "LifeAreas",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 5, 25, 14, 8, 15, 38, DateTimeKind.Utc).AddTicks(1206), new DateTime(2025, 5, 25, 14, 8, 15, 38, DateTimeKind.Utc).AddTicks(1206) });

            migrationBuilder.UpdateData(
                table: "LifeAreas",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 5, 25, 14, 8, 15, 38, DateTimeKind.Utc).AddTicks(1206), new DateTime(2025, 5, 25, 14, 8, 15, 38, DateTimeKind.Utc).AddTicks(1206) });

            migrationBuilder.UpdateData(
                table: "LifeAreas",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 5, 25, 14, 8, 15, 38, DateTimeKind.Utc).AddTicks(1206), new DateTime(2025, 5, 25, 14, 8, 15, 38, DateTimeKind.Utc).AddTicks(1206) });

            migrationBuilder.UpdateData(
                table: "LifeAreas",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 5, 25, 14, 8, 15, 38, DateTimeKind.Utc).AddTicks(1206), new DateTime(2025, 5, 25, 14, 8, 15, 38, DateTimeKind.Utc).AddTicks(1206) });

            migrationBuilder.UpdateData(
                table: "LifeAreas",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 5, 25, 14, 8, 15, 38, DateTimeKind.Utc).AddTicks(1206), new DateTime(2025, 5, 25, 14, 8, 15, 38, DateTimeKind.Utc).AddTicks(1206) });

            migrationBuilder.UpdateData(
                table: "LifeAreas",
                keyColumn: "Id",
                keyValue: 7,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 5, 25, 14, 8, 15, 38, DateTimeKind.Utc).AddTicks(1206), new DateTime(2025, 5, 25, 14, 8, 15, 38, DateTimeKind.Utc).AddTicks(1206) });

            migrationBuilder.UpdateData(
                table: "LifeAreas",
                keyColumn: "Id",
                keyValue: 8,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 5, 25, 14, 8, 15, 38, DateTimeKind.Utc).AddTicks(1206), new DateTime(2025, 5, 25, 14, 8, 15, 38, DateTimeKind.Utc).AddTicks(1206) });

            migrationBuilder.UpdateData(
                table: "LifeAreas",
                keyColumn: "Id",
                keyValue: 9,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 5, 25, 14, 8, 15, 38, DateTimeKind.Utc).AddTicks(1206), new DateTime(2025, 5, 25, 14, 8, 15, 38, DateTimeKind.Utc).AddTicks(1206) });

            migrationBuilder.UpdateData(
                table: "LifeAreas",
                keyColumn: "Id",
                keyValue: 10,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 5, 25, 14, 8, 15, 38, DateTimeKind.Utc).AddTicks(1206), new DateTime(2025, 5, 25, 14, 8, 15, 38, DateTimeKind.Utc).AddTicks(1206) });

            migrationBuilder.UpdateData(
                table: "Questions",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 5, 25, 14, 8, 15, 41, DateTimeKind.Utc).AddTicks(1448), new DateTime(2025, 5, 25, 14, 8, 15, 41, DateTimeKind.Utc).AddTicks(1448) });

            migrationBuilder.UpdateData(
                table: "Questions",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 5, 25, 14, 8, 15, 41, DateTimeKind.Utc).AddTicks(1448), new DateTime(2025, 5, 25, 14, 8, 15, 41, DateTimeKind.Utc).AddTicks(1448) });

            migrationBuilder.UpdateData(
                table: "Questions",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 5, 25, 14, 8, 15, 41, DateTimeKind.Utc).AddTicks(1448), new DateTime(2025, 5, 25, 14, 8, 15, 41, DateTimeKind.Utc).AddTicks(1448) });

            migrationBuilder.UpdateData(
                table: "Questions",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 5, 25, 14, 8, 15, 41, DateTimeKind.Utc).AddTicks(1448), new DateTime(2025, 5, 25, 14, 8, 15, 41, DateTimeKind.Utc).AddTicks(1448) });

            migrationBuilder.UpdateData(
                table: "Questions",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 5, 25, 14, 8, 15, 41, DateTimeKind.Utc).AddTicks(1448), new DateTime(2025, 5, 25, 14, 8, 15, 41, DateTimeKind.Utc).AddTicks(1448) });

            migrationBuilder.UpdateData(
                table: "Questions",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 5, 25, 14, 8, 15, 41, DateTimeKind.Utc).AddTicks(1448), new DateTime(2025, 5, 25, 14, 8, 15, 41, DateTimeKind.Utc).AddTicks(1448) });

            migrationBuilder.UpdateData(
                table: "Questions",
                keyColumn: "Id",
                keyValue: 7,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 5, 25, 14, 8, 15, 41, DateTimeKind.Utc).AddTicks(1448), new DateTime(2025, 5, 25, 14, 8, 15, 41, DateTimeKind.Utc).AddTicks(1448) });

            migrationBuilder.UpdateData(
                table: "Questions",
                keyColumn: "Id",
                keyValue: 8,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 5, 25, 14, 8, 15, 41, DateTimeKind.Utc).AddTicks(1448), new DateTime(2025, 5, 25, 14, 8, 15, 41, DateTimeKind.Utc).AddTicks(1448) });

            migrationBuilder.UpdateData(
                table: "Questions",
                keyColumn: "Id",
                keyValue: 9,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 5, 25, 14, 8, 15, 41, DateTimeKind.Utc).AddTicks(1448), new DateTime(2025, 5, 25, 14, 8, 15, 41, DateTimeKind.Utc).AddTicks(1448) });

            migrationBuilder.UpdateData(
                table: "Questions",
                keyColumn: "Id",
                keyValue: 10,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 5, 25, 14, 8, 15, 41, DateTimeKind.Utc).AddTicks(1448), new DateTime(2025, 5, 25, 14, 8, 15, 41, DateTimeKind.Utc).AddTicks(1448) });
        }
    }
}
