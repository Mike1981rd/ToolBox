using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace ToolBox.Migrations
{
    /// <inheritdoc />
    public partial class CreateLifeEventsTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "LifeEvents",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UserId = table.Column<int>(type: "integer", nullable: false),
                    EventTitle = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    Description = table.Column<string>(type: "TEXT", nullable: true),
                    EventYear = table.Column<int>(type: "integer", nullable: false),
                    ImpactScore = table.Column<int>(type: "integer", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LifeEvents", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LifeEvents_Users_UserId",
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
                value: new DateTime(2025, 5, 29, 19, 5, 5, 406, DateTimeKind.Utc).AddTicks(1017));

            migrationBuilder.UpdateData(
                table: "AreasProgreso",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2025, 5, 29, 19, 5, 5, 406, DateTimeKind.Utc).AddTicks(1017));

            migrationBuilder.UpdateData(
                table: "AreasProgreso",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedAt",
                value: new DateTime(2025, 5, 29, 19, 5, 5, 406, DateTimeKind.Utc).AddTicks(1017));

            migrationBuilder.UpdateData(
                table: "AreasProgreso",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedAt",
                value: new DateTime(2025, 5, 29, 19, 5, 5, 406, DateTimeKind.Utc).AddTicks(1017));

            migrationBuilder.UpdateData(
                table: "AreasProgreso",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreatedAt",
                value: new DateTime(2025, 5, 29, 19, 5, 5, 406, DateTimeKind.Utc).AddTicks(1017));

            migrationBuilder.UpdateData(
                table: "CategoriasHabitos",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "FechaCreacion" },
                values: new object[] { new DateTime(2025, 5, 29, 19, 5, 5, 407, DateTimeKind.Utc).AddTicks(1655), new DateTime(2025, 5, 29, 19, 5, 5, 407, DateTimeKind.Utc).AddTicks(1659) });

            migrationBuilder.UpdateData(
                table: "CategoriasHabitos",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedAt", "FechaCreacion" },
                values: new object[] { new DateTime(2025, 5, 29, 19, 5, 5, 407, DateTimeKind.Utc).AddTicks(1655), new DateTime(2025, 5, 29, 19, 5, 5, 407, DateTimeKind.Utc).AddTicks(1665) });

            migrationBuilder.UpdateData(
                table: "CategoriasHabitos",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedAt", "FechaCreacion" },
                values: new object[] { new DateTime(2025, 5, 29, 19, 5, 5, 407, DateTimeKind.Utc).AddTicks(1655), new DateTime(2025, 5, 29, 19, 5, 5, 407, DateTimeKind.Utc).AddTicks(1668) });

            migrationBuilder.UpdateData(
                table: "CategoriasHabitos",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "CreatedAt", "FechaCreacion" },
                values: new object[] { new DateTime(2025, 5, 29, 19, 5, 5, 407, DateTimeKind.Utc).AddTicks(1655), new DateTime(2025, 5, 29, 19, 5, 5, 407, DateTimeKind.Utc).AddTicks(1669) });

            migrationBuilder.UpdateData(
                table: "CategoriasHabitos",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "CreatedAt", "FechaCreacion" },
                values: new object[] { new DateTime(2025, 5, 29, 19, 5, 5, 407, DateTimeKind.Utc).AddTicks(1655), new DateTime(2025, 5, 29, 19, 5, 5, 407, DateTimeKind.Utc).AddTicks(1671) });

            migrationBuilder.UpdateData(
                table: "CategoriasHabitos",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "CreatedAt", "FechaCreacion" },
                values: new object[] { new DateTime(2025, 5, 29, 19, 5, 5, 407, DateTimeKind.Utc).AddTicks(1655), new DateTime(2025, 5, 29, 19, 5, 5, 407, DateTimeKind.Utc).AddTicks(1673) });

            migrationBuilder.UpdateData(
                table: "CategoriasHabitos",
                keyColumn: "Id",
                keyValue: 7,
                columns: new[] { "CreatedAt", "FechaCreacion" },
                values: new object[] { new DateTime(2025, 5, 29, 19, 5, 5, 407, DateTimeKind.Utc).AddTicks(1655), new DateTime(2025, 5, 29, 19, 5, 5, 407, DateTimeKind.Utc).AddTicks(1675) });

            migrationBuilder.UpdateData(
                table: "CategoriasHabitos",
                keyColumn: "Id",
                keyValue: 8,
                columns: new[] { "CreatedAt", "FechaCreacion" },
                values: new object[] { new DateTime(2025, 5, 29, 19, 5, 5, 407, DateTimeKind.Utc).AddTicks(1655), new DateTime(2025, 5, 29, 19, 5, 5, 407, DateTimeKind.Utc).AddTicks(1677) });

            migrationBuilder.UpdateData(
                table: "CategoriasProgreso",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2025, 5, 29, 19, 5, 5, 406, DateTimeKind.Utc).AddTicks(1017));

            migrationBuilder.UpdateData(
                table: "CategoriasProgreso",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2025, 5, 29, 19, 5, 5, 406, DateTimeKind.Utc).AddTicks(1017));

            migrationBuilder.UpdateData(
                table: "CategoriasProgreso",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedAt",
                value: new DateTime(2025, 5, 29, 19, 5, 5, 406, DateTimeKind.Utc).AddTicks(1017));

            migrationBuilder.UpdateData(
                table: "CategoriasProgreso",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedAt",
                value: new DateTime(2025, 5, 29, 19, 5, 5, 406, DateTimeKind.Utc).AddTicks(1017));

            migrationBuilder.UpdateData(
                table: "CategoriasProgreso",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreatedAt",
                value: new DateTime(2025, 5, 29, 19, 5, 5, 406, DateTimeKind.Utc).AddTicks(1017));

            migrationBuilder.UpdateData(
                table: "CategoriasProgreso",
                keyColumn: "Id",
                keyValue: 6,
                column: "CreatedAt",
                value: new DateTime(2025, 5, 29, 19, 5, 5, 406, DateTimeKind.Utc).AddTicks(1017));

            migrationBuilder.UpdateData(
                table: "CategoriasProgreso",
                keyColumn: "Id",
                keyValue: 7,
                column: "CreatedAt",
                value: new DateTime(2025, 5, 29, 19, 5, 5, 406, DateTimeKind.Utc).AddTicks(1017));

            migrationBuilder.UpdateData(
                table: "CategoriasProgreso",
                keyColumn: "Id",
                keyValue: 8,
                column: "CreatedAt",
                value: new DateTime(2025, 5, 29, 19, 5, 5, 406, DateTimeKind.Utc).AddTicks(1017));

            migrationBuilder.UpdateData(
                table: "CategoriasProgreso",
                keyColumn: "Id",
                keyValue: 9,
                column: "CreatedAt",
                value: new DateTime(2025, 5, 29, 19, 5, 5, 406, DateTimeKind.Utc).AddTicks(1017));

            migrationBuilder.UpdateData(
                table: "CategoriasProgreso",
                keyColumn: "Id",
                keyValue: 10,
                column: "CreatedAt",
                value: new DateTime(2025, 5, 29, 19, 5, 5, 406, DateTimeKind.Utc).AddTicks(1017));

            migrationBuilder.UpdateData(
                table: "CategoriasProgreso",
                keyColumn: "Id",
                keyValue: 11,
                column: "CreatedAt",
                value: new DateTime(2025, 5, 29, 19, 5, 5, 406, DateTimeKind.Utc).AddTicks(1017));

            migrationBuilder.UpdateData(
                table: "CategoriasProgreso",
                keyColumn: "Id",
                keyValue: 12,
                column: "CreatedAt",
                value: new DateTime(2025, 5, 29, 19, 5, 5, 406, DateTimeKind.Utc).AddTicks(1017));

            migrationBuilder.UpdateData(
                table: "CategoriasProgreso",
                keyColumn: "Id",
                keyValue: 13,
                column: "CreatedAt",
                value: new DateTime(2025, 5, 29, 19, 5, 5, 406, DateTimeKind.Utc).AddTicks(1017));

            migrationBuilder.UpdateData(
                table: "CategoriasProgreso",
                keyColumn: "Id",
                keyValue: 14,
                column: "CreatedAt",
                value: new DateTime(2025, 5, 29, 19, 5, 5, 406, DateTimeKind.Utc).AddTicks(1017));

            migrationBuilder.UpdateData(
                table: "CategoriasProgreso",
                keyColumn: "Id",
                keyValue: 15,
                column: "CreatedAt",
                value: new DateTime(2025, 5, 29, 19, 5, 5, 406, DateTimeKind.Utc).AddTicks(1017));

            migrationBuilder.UpdateData(
                table: "FrecuenciasHabitos",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "FechaCreacion" },
                values: new object[] { new DateTime(2025, 5, 29, 19, 5, 5, 407, DateTimeKind.Utc).AddTicks(1655), new DateTime(2025, 5, 29, 19, 5, 5, 407, DateTimeKind.Utc).AddTicks(1680) });

            migrationBuilder.UpdateData(
                table: "FrecuenciasHabitos",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedAt", "FechaCreacion" },
                values: new object[] { new DateTime(2025, 5, 29, 19, 5, 5, 407, DateTimeKind.Utc).AddTicks(1655), new DateTime(2025, 5, 29, 19, 5, 5, 407, DateTimeKind.Utc).AddTicks(1684) });

            migrationBuilder.UpdateData(
                table: "FrecuenciasHabitos",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedAt", "FechaCreacion" },
                values: new object[] { new DateTime(2025, 5, 29, 19, 5, 5, 407, DateTimeKind.Utc).AddTicks(1655), new DateTime(2025, 5, 29, 19, 5, 5, 407, DateTimeKind.Utc).AddTicks(1686) });

            migrationBuilder.UpdateData(
                table: "FrecuenciasHabitos",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "CreatedAt", "FechaCreacion" },
                values: new object[] { new DateTime(2025, 5, 29, 19, 5, 5, 407, DateTimeKind.Utc).AddTicks(1655), new DateTime(2025, 5, 29, 19, 5, 5, 407, DateTimeKind.Utc).AddTicks(1688) });

            migrationBuilder.UpdateData(
                table: "FrecuenciasHabitos",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "CreatedAt", "FechaCreacion" },
                values: new object[] { new DateTime(2025, 5, 29, 19, 5, 5, 407, DateTimeKind.Utc).AddTicks(1655), new DateTime(2025, 5, 29, 19, 5, 5, 407, DateTimeKind.Utc).AddTicks(1690) });

            migrationBuilder.UpdateData(
                table: "FrecuenciasHabitos",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "CreatedAt", "FechaCreacion" },
                values: new object[] { new DateTime(2025, 5, 29, 19, 5, 5, 407, DateTimeKind.Utc).AddTicks(1655), new DateTime(2025, 5, 29, 19, 5, 5, 407, DateTimeKind.Utc).AddTicks(1692) });

            migrationBuilder.UpdateData(
                table: "LifeAreas",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 5, 29, 19, 5, 5, 404, DateTimeKind.Utc).AddTicks(6835), new DateTime(2025, 5, 29, 19, 5, 5, 404, DateTimeKind.Utc).AddTicks(6835) });

            migrationBuilder.UpdateData(
                table: "LifeAreas",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 5, 29, 19, 5, 5, 404, DateTimeKind.Utc).AddTicks(6835), new DateTime(2025, 5, 29, 19, 5, 5, 404, DateTimeKind.Utc).AddTicks(6835) });

            migrationBuilder.UpdateData(
                table: "LifeAreas",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 5, 29, 19, 5, 5, 404, DateTimeKind.Utc).AddTicks(6835), new DateTime(2025, 5, 29, 19, 5, 5, 404, DateTimeKind.Utc).AddTicks(6835) });

            migrationBuilder.UpdateData(
                table: "LifeAreas",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 5, 29, 19, 5, 5, 404, DateTimeKind.Utc).AddTicks(6835), new DateTime(2025, 5, 29, 19, 5, 5, 404, DateTimeKind.Utc).AddTicks(6835) });

            migrationBuilder.UpdateData(
                table: "LifeAreas",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 5, 29, 19, 5, 5, 404, DateTimeKind.Utc).AddTicks(6835), new DateTime(2025, 5, 29, 19, 5, 5, 404, DateTimeKind.Utc).AddTicks(6835) });

            migrationBuilder.UpdateData(
                table: "LifeAreas",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 5, 29, 19, 5, 5, 404, DateTimeKind.Utc).AddTicks(6835), new DateTime(2025, 5, 29, 19, 5, 5, 404, DateTimeKind.Utc).AddTicks(6835) });

            migrationBuilder.UpdateData(
                table: "LifeAreas",
                keyColumn: "Id",
                keyValue: 7,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 5, 29, 19, 5, 5, 404, DateTimeKind.Utc).AddTicks(6835), new DateTime(2025, 5, 29, 19, 5, 5, 404, DateTimeKind.Utc).AddTicks(6835) });

            migrationBuilder.UpdateData(
                table: "LifeAreas",
                keyColumn: "Id",
                keyValue: 8,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 5, 29, 19, 5, 5, 404, DateTimeKind.Utc).AddTicks(6835), new DateTime(2025, 5, 29, 19, 5, 5, 404, DateTimeKind.Utc).AddTicks(6835) });

            migrationBuilder.UpdateData(
                table: "LifeAreas",
                keyColumn: "Id",
                keyValue: 9,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 5, 29, 19, 5, 5, 404, DateTimeKind.Utc).AddTicks(6835), new DateTime(2025, 5, 29, 19, 5, 5, 404, DateTimeKind.Utc).AddTicks(6835) });

            migrationBuilder.UpdateData(
                table: "LifeAreas",
                keyColumn: "Id",
                keyValue: 10,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 5, 29, 19, 5, 5, 404, DateTimeKind.Utc).AddTicks(6835), new DateTime(2025, 5, 29, 19, 5, 5, 404, DateTimeKind.Utc).AddTicks(6835) });

            migrationBuilder.UpdateData(
                table: "Questions",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 5, 29, 19, 5, 5, 406, DateTimeKind.Utc).AddTicks(930), new DateTime(2025, 5, 29, 19, 5, 5, 406, DateTimeKind.Utc).AddTicks(930) });

            migrationBuilder.UpdateData(
                table: "Questions",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 5, 29, 19, 5, 5, 406, DateTimeKind.Utc).AddTicks(930), new DateTime(2025, 5, 29, 19, 5, 5, 406, DateTimeKind.Utc).AddTicks(930) });

            migrationBuilder.UpdateData(
                table: "Questions",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 5, 29, 19, 5, 5, 406, DateTimeKind.Utc).AddTicks(930), new DateTime(2025, 5, 29, 19, 5, 5, 406, DateTimeKind.Utc).AddTicks(930) });

            migrationBuilder.UpdateData(
                table: "Questions",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 5, 29, 19, 5, 5, 406, DateTimeKind.Utc).AddTicks(930), new DateTime(2025, 5, 29, 19, 5, 5, 406, DateTimeKind.Utc).AddTicks(930) });

            migrationBuilder.UpdateData(
                table: "Questions",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 5, 29, 19, 5, 5, 406, DateTimeKind.Utc).AddTicks(930), new DateTime(2025, 5, 29, 19, 5, 5, 406, DateTimeKind.Utc).AddTicks(930) });

            migrationBuilder.UpdateData(
                table: "Questions",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 5, 29, 19, 5, 5, 406, DateTimeKind.Utc).AddTicks(930), new DateTime(2025, 5, 29, 19, 5, 5, 406, DateTimeKind.Utc).AddTicks(930) });

            migrationBuilder.UpdateData(
                table: "Questions",
                keyColumn: "Id",
                keyValue: 7,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 5, 29, 19, 5, 5, 406, DateTimeKind.Utc).AddTicks(930), new DateTime(2025, 5, 29, 19, 5, 5, 406, DateTimeKind.Utc).AddTicks(930) });

            migrationBuilder.UpdateData(
                table: "Questions",
                keyColumn: "Id",
                keyValue: 8,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 5, 29, 19, 5, 5, 406, DateTimeKind.Utc).AddTicks(930), new DateTime(2025, 5, 29, 19, 5, 5, 406, DateTimeKind.Utc).AddTicks(930) });

            migrationBuilder.UpdateData(
                table: "Questions",
                keyColumn: "Id",
                keyValue: 9,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 5, 29, 19, 5, 5, 406, DateTimeKind.Utc).AddTicks(930), new DateTime(2025, 5, 29, 19, 5, 5, 406, DateTimeKind.Utc).AddTicks(930) });

            migrationBuilder.UpdateData(
                table: "Questions",
                keyColumn: "Id",
                keyValue: 10,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 5, 29, 19, 5, 5, 406, DateTimeKind.Utc).AddTicks(930), new DateTime(2025, 5, 29, 19, 5, 5, 406, DateTimeKind.Utc).AddTicks(930) });

            migrationBuilder.CreateIndex(
                name: "IX_LifeEvents_UserId",
                table: "LifeEvents",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_LifeEvents_UserId_EventYear",
                table: "LifeEvents",
                columns: new[] { "UserId", "EventYear" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "LifeEvents");

            migrationBuilder.UpdateData(
                table: "AreasProgreso",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2025, 5, 28, 22, 38, 27, 287, DateTimeKind.Utc).AddTicks(7098));

            migrationBuilder.UpdateData(
                table: "AreasProgreso",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2025, 5, 28, 22, 38, 27, 287, DateTimeKind.Utc).AddTicks(7098));

            migrationBuilder.UpdateData(
                table: "AreasProgreso",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedAt",
                value: new DateTime(2025, 5, 28, 22, 38, 27, 287, DateTimeKind.Utc).AddTicks(7098));

            migrationBuilder.UpdateData(
                table: "AreasProgreso",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedAt",
                value: new DateTime(2025, 5, 28, 22, 38, 27, 287, DateTimeKind.Utc).AddTicks(7098));

            migrationBuilder.UpdateData(
                table: "AreasProgreso",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreatedAt",
                value: new DateTime(2025, 5, 28, 22, 38, 27, 287, DateTimeKind.Utc).AddTicks(7098));

            migrationBuilder.UpdateData(
                table: "CategoriasHabitos",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "FechaCreacion" },
                values: new object[] { new DateTime(2025, 5, 28, 22, 38, 27, 289, DateTimeKind.Utc).AddTicks(1388), new DateTime(2025, 5, 28, 22, 38, 27, 289, DateTimeKind.Utc).AddTicks(1391) });

            migrationBuilder.UpdateData(
                table: "CategoriasHabitos",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedAt", "FechaCreacion" },
                values: new object[] { new DateTime(2025, 5, 28, 22, 38, 27, 289, DateTimeKind.Utc).AddTicks(1388), new DateTime(2025, 5, 28, 22, 38, 27, 289, DateTimeKind.Utc).AddTicks(1397) });

            migrationBuilder.UpdateData(
                table: "CategoriasHabitos",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedAt", "FechaCreacion" },
                values: new object[] { new DateTime(2025, 5, 28, 22, 38, 27, 289, DateTimeKind.Utc).AddTicks(1388), new DateTime(2025, 5, 28, 22, 38, 27, 289, DateTimeKind.Utc).AddTicks(1400) });

            migrationBuilder.UpdateData(
                table: "CategoriasHabitos",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "CreatedAt", "FechaCreacion" },
                values: new object[] { new DateTime(2025, 5, 28, 22, 38, 27, 289, DateTimeKind.Utc).AddTicks(1388), new DateTime(2025, 5, 28, 22, 38, 27, 289, DateTimeKind.Utc).AddTicks(1405) });

            migrationBuilder.UpdateData(
                table: "CategoriasHabitos",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "CreatedAt", "FechaCreacion" },
                values: new object[] { new DateTime(2025, 5, 28, 22, 38, 27, 289, DateTimeKind.Utc).AddTicks(1388), new DateTime(2025, 5, 28, 22, 38, 27, 289, DateTimeKind.Utc).AddTicks(1407) });

            migrationBuilder.UpdateData(
                table: "CategoriasHabitos",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "CreatedAt", "FechaCreacion" },
                values: new object[] { new DateTime(2025, 5, 28, 22, 38, 27, 289, DateTimeKind.Utc).AddTicks(1388), new DateTime(2025, 5, 28, 22, 38, 27, 289, DateTimeKind.Utc).AddTicks(1409) });

            migrationBuilder.UpdateData(
                table: "CategoriasHabitos",
                keyColumn: "Id",
                keyValue: 7,
                columns: new[] { "CreatedAt", "FechaCreacion" },
                values: new object[] { new DateTime(2025, 5, 28, 22, 38, 27, 289, DateTimeKind.Utc).AddTicks(1388), new DateTime(2025, 5, 28, 22, 38, 27, 289, DateTimeKind.Utc).AddTicks(1411) });

            migrationBuilder.UpdateData(
                table: "CategoriasHabitos",
                keyColumn: "Id",
                keyValue: 8,
                columns: new[] { "CreatedAt", "FechaCreacion" },
                values: new object[] { new DateTime(2025, 5, 28, 22, 38, 27, 289, DateTimeKind.Utc).AddTicks(1388), new DateTime(2025, 5, 28, 22, 38, 27, 289, DateTimeKind.Utc).AddTicks(1413) });

            migrationBuilder.UpdateData(
                table: "CategoriasProgreso",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2025, 5, 28, 22, 38, 27, 287, DateTimeKind.Utc).AddTicks(7098));

            migrationBuilder.UpdateData(
                table: "CategoriasProgreso",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2025, 5, 28, 22, 38, 27, 287, DateTimeKind.Utc).AddTicks(7098));

            migrationBuilder.UpdateData(
                table: "CategoriasProgreso",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedAt",
                value: new DateTime(2025, 5, 28, 22, 38, 27, 287, DateTimeKind.Utc).AddTicks(7098));

            migrationBuilder.UpdateData(
                table: "CategoriasProgreso",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedAt",
                value: new DateTime(2025, 5, 28, 22, 38, 27, 287, DateTimeKind.Utc).AddTicks(7098));

            migrationBuilder.UpdateData(
                table: "CategoriasProgreso",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreatedAt",
                value: new DateTime(2025, 5, 28, 22, 38, 27, 287, DateTimeKind.Utc).AddTicks(7098));

            migrationBuilder.UpdateData(
                table: "CategoriasProgreso",
                keyColumn: "Id",
                keyValue: 6,
                column: "CreatedAt",
                value: new DateTime(2025, 5, 28, 22, 38, 27, 287, DateTimeKind.Utc).AddTicks(7098));

            migrationBuilder.UpdateData(
                table: "CategoriasProgreso",
                keyColumn: "Id",
                keyValue: 7,
                column: "CreatedAt",
                value: new DateTime(2025, 5, 28, 22, 38, 27, 287, DateTimeKind.Utc).AddTicks(7098));

            migrationBuilder.UpdateData(
                table: "CategoriasProgreso",
                keyColumn: "Id",
                keyValue: 8,
                column: "CreatedAt",
                value: new DateTime(2025, 5, 28, 22, 38, 27, 287, DateTimeKind.Utc).AddTicks(7098));

            migrationBuilder.UpdateData(
                table: "CategoriasProgreso",
                keyColumn: "Id",
                keyValue: 9,
                column: "CreatedAt",
                value: new DateTime(2025, 5, 28, 22, 38, 27, 287, DateTimeKind.Utc).AddTicks(7098));

            migrationBuilder.UpdateData(
                table: "CategoriasProgreso",
                keyColumn: "Id",
                keyValue: 10,
                column: "CreatedAt",
                value: new DateTime(2025, 5, 28, 22, 38, 27, 287, DateTimeKind.Utc).AddTicks(7098));

            migrationBuilder.UpdateData(
                table: "CategoriasProgreso",
                keyColumn: "Id",
                keyValue: 11,
                column: "CreatedAt",
                value: new DateTime(2025, 5, 28, 22, 38, 27, 287, DateTimeKind.Utc).AddTicks(7098));

            migrationBuilder.UpdateData(
                table: "CategoriasProgreso",
                keyColumn: "Id",
                keyValue: 12,
                column: "CreatedAt",
                value: new DateTime(2025, 5, 28, 22, 38, 27, 287, DateTimeKind.Utc).AddTicks(7098));

            migrationBuilder.UpdateData(
                table: "CategoriasProgreso",
                keyColumn: "Id",
                keyValue: 13,
                column: "CreatedAt",
                value: new DateTime(2025, 5, 28, 22, 38, 27, 287, DateTimeKind.Utc).AddTicks(7098));

            migrationBuilder.UpdateData(
                table: "CategoriasProgreso",
                keyColumn: "Id",
                keyValue: 14,
                column: "CreatedAt",
                value: new DateTime(2025, 5, 28, 22, 38, 27, 287, DateTimeKind.Utc).AddTicks(7098));

            migrationBuilder.UpdateData(
                table: "CategoriasProgreso",
                keyColumn: "Id",
                keyValue: 15,
                column: "CreatedAt",
                value: new DateTime(2025, 5, 28, 22, 38, 27, 287, DateTimeKind.Utc).AddTicks(7098));

            migrationBuilder.UpdateData(
                table: "FrecuenciasHabitos",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "FechaCreacion" },
                values: new object[] { new DateTime(2025, 5, 28, 22, 38, 27, 289, DateTimeKind.Utc).AddTicks(1388), new DateTime(2025, 5, 28, 22, 38, 27, 289, DateTimeKind.Utc).AddTicks(1416) });

            migrationBuilder.UpdateData(
                table: "FrecuenciasHabitos",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedAt", "FechaCreacion" },
                values: new object[] { new DateTime(2025, 5, 28, 22, 38, 27, 289, DateTimeKind.Utc).AddTicks(1388), new DateTime(2025, 5, 28, 22, 38, 27, 289, DateTimeKind.Utc).AddTicks(1420) });

            migrationBuilder.UpdateData(
                table: "FrecuenciasHabitos",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedAt", "FechaCreacion" },
                values: new object[] { new DateTime(2025, 5, 28, 22, 38, 27, 289, DateTimeKind.Utc).AddTicks(1388), new DateTime(2025, 5, 28, 22, 38, 27, 289, DateTimeKind.Utc).AddTicks(1422) });

            migrationBuilder.UpdateData(
                table: "FrecuenciasHabitos",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "CreatedAt", "FechaCreacion" },
                values: new object[] { new DateTime(2025, 5, 28, 22, 38, 27, 289, DateTimeKind.Utc).AddTicks(1388), new DateTime(2025, 5, 28, 22, 38, 27, 289, DateTimeKind.Utc).AddTicks(1424) });

            migrationBuilder.UpdateData(
                table: "FrecuenciasHabitos",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "CreatedAt", "FechaCreacion" },
                values: new object[] { new DateTime(2025, 5, 28, 22, 38, 27, 289, DateTimeKind.Utc).AddTicks(1388), new DateTime(2025, 5, 28, 22, 38, 27, 289, DateTimeKind.Utc).AddTicks(1426) });

            migrationBuilder.UpdateData(
                table: "FrecuenciasHabitos",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "CreatedAt", "FechaCreacion" },
                values: new object[] { new DateTime(2025, 5, 28, 22, 38, 27, 289, DateTimeKind.Utc).AddTicks(1388), new DateTime(2025, 5, 28, 22, 38, 27, 289, DateTimeKind.Utc).AddTicks(1428) });

            migrationBuilder.UpdateData(
                table: "LifeAreas",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 5, 28, 22, 38, 27, 285, DateTimeKind.Utc).AddTicks(6914), new DateTime(2025, 5, 28, 22, 38, 27, 285, DateTimeKind.Utc).AddTicks(6914) });

            migrationBuilder.UpdateData(
                table: "LifeAreas",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 5, 28, 22, 38, 27, 285, DateTimeKind.Utc).AddTicks(6914), new DateTime(2025, 5, 28, 22, 38, 27, 285, DateTimeKind.Utc).AddTicks(6914) });

            migrationBuilder.UpdateData(
                table: "LifeAreas",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 5, 28, 22, 38, 27, 285, DateTimeKind.Utc).AddTicks(6914), new DateTime(2025, 5, 28, 22, 38, 27, 285, DateTimeKind.Utc).AddTicks(6914) });

            migrationBuilder.UpdateData(
                table: "LifeAreas",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 5, 28, 22, 38, 27, 285, DateTimeKind.Utc).AddTicks(6914), new DateTime(2025, 5, 28, 22, 38, 27, 285, DateTimeKind.Utc).AddTicks(6914) });

            migrationBuilder.UpdateData(
                table: "LifeAreas",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 5, 28, 22, 38, 27, 285, DateTimeKind.Utc).AddTicks(6914), new DateTime(2025, 5, 28, 22, 38, 27, 285, DateTimeKind.Utc).AddTicks(6914) });

            migrationBuilder.UpdateData(
                table: "LifeAreas",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 5, 28, 22, 38, 27, 285, DateTimeKind.Utc).AddTicks(6914), new DateTime(2025, 5, 28, 22, 38, 27, 285, DateTimeKind.Utc).AddTicks(6914) });

            migrationBuilder.UpdateData(
                table: "LifeAreas",
                keyColumn: "Id",
                keyValue: 7,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 5, 28, 22, 38, 27, 285, DateTimeKind.Utc).AddTicks(6914), new DateTime(2025, 5, 28, 22, 38, 27, 285, DateTimeKind.Utc).AddTicks(6914) });

            migrationBuilder.UpdateData(
                table: "LifeAreas",
                keyColumn: "Id",
                keyValue: 8,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 5, 28, 22, 38, 27, 285, DateTimeKind.Utc).AddTicks(6914), new DateTime(2025, 5, 28, 22, 38, 27, 285, DateTimeKind.Utc).AddTicks(6914) });

            migrationBuilder.UpdateData(
                table: "LifeAreas",
                keyColumn: "Id",
                keyValue: 9,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 5, 28, 22, 38, 27, 285, DateTimeKind.Utc).AddTicks(6914), new DateTime(2025, 5, 28, 22, 38, 27, 285, DateTimeKind.Utc).AddTicks(6914) });

            migrationBuilder.UpdateData(
                table: "LifeAreas",
                keyColumn: "Id",
                keyValue: 10,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 5, 28, 22, 38, 27, 285, DateTimeKind.Utc).AddTicks(6914), new DateTime(2025, 5, 28, 22, 38, 27, 285, DateTimeKind.Utc).AddTicks(6914) });

            migrationBuilder.UpdateData(
                table: "Questions",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 5, 28, 22, 38, 27, 287, DateTimeKind.Utc).AddTicks(6973), new DateTime(2025, 5, 28, 22, 38, 27, 287, DateTimeKind.Utc).AddTicks(6973) });

            migrationBuilder.UpdateData(
                table: "Questions",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 5, 28, 22, 38, 27, 287, DateTimeKind.Utc).AddTicks(6973), new DateTime(2025, 5, 28, 22, 38, 27, 287, DateTimeKind.Utc).AddTicks(6973) });

            migrationBuilder.UpdateData(
                table: "Questions",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 5, 28, 22, 38, 27, 287, DateTimeKind.Utc).AddTicks(6973), new DateTime(2025, 5, 28, 22, 38, 27, 287, DateTimeKind.Utc).AddTicks(6973) });

            migrationBuilder.UpdateData(
                table: "Questions",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 5, 28, 22, 38, 27, 287, DateTimeKind.Utc).AddTicks(6973), new DateTime(2025, 5, 28, 22, 38, 27, 287, DateTimeKind.Utc).AddTicks(6973) });

            migrationBuilder.UpdateData(
                table: "Questions",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 5, 28, 22, 38, 27, 287, DateTimeKind.Utc).AddTicks(6973), new DateTime(2025, 5, 28, 22, 38, 27, 287, DateTimeKind.Utc).AddTicks(6973) });

            migrationBuilder.UpdateData(
                table: "Questions",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 5, 28, 22, 38, 27, 287, DateTimeKind.Utc).AddTicks(6973), new DateTime(2025, 5, 28, 22, 38, 27, 287, DateTimeKind.Utc).AddTicks(6973) });

            migrationBuilder.UpdateData(
                table: "Questions",
                keyColumn: "Id",
                keyValue: 7,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 5, 28, 22, 38, 27, 287, DateTimeKind.Utc).AddTicks(6973), new DateTime(2025, 5, 28, 22, 38, 27, 287, DateTimeKind.Utc).AddTicks(6973) });

            migrationBuilder.UpdateData(
                table: "Questions",
                keyColumn: "Id",
                keyValue: 8,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 5, 28, 22, 38, 27, 287, DateTimeKind.Utc).AddTicks(6973), new DateTime(2025, 5, 28, 22, 38, 27, 287, DateTimeKind.Utc).AddTicks(6973) });

            migrationBuilder.UpdateData(
                table: "Questions",
                keyColumn: "Id",
                keyValue: 9,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 5, 28, 22, 38, 27, 287, DateTimeKind.Utc).AddTicks(6973), new DateTime(2025, 5, 28, 22, 38, 27, 287, DateTimeKind.Utc).AddTicks(6973) });

            migrationBuilder.UpdateData(
                table: "Questions",
                keyColumn: "Id",
                keyValue: 10,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 5, 28, 22, 38, 27, 287, DateTimeKind.Utc).AddTicks(6973), new DateTime(2025, 5, 28, 22, 38, 27, 287, DateTimeKind.Utc).AddTicks(6973) });
        }
    }
}
