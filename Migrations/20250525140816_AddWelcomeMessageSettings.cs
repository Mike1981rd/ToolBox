using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace ToolBox.Migrations
{
    /// <inheritdoc />
    public partial class AddWelcomeMessageSettings : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "WelcomeMessageSettings",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Title = table.Column<string>(type: "text", nullable: false),
                    DescriptionHTML = table.Column<string>(type: "text", nullable: false),
                    VideoType = table.Column<string>(type: "text", nullable: false),
                    VideoUrl = table.Column<string>(type: "text", nullable: true),
                    UploadedVideoFileName = table.Column<string>(type: "text", nullable: true),
                    UploadedVideoPath = table.Column<string>(type: "text", nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WelcomeMessageSettings", x => x.Id);
                });

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "WelcomeMessageSettings");

            migrationBuilder.UpdateData(
                table: "AreasProgreso",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2025, 5, 24, 22, 56, 48, 935, DateTimeKind.Utc).AddTicks(9964));

            migrationBuilder.UpdateData(
                table: "AreasProgreso",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2025, 5, 24, 22, 56, 48, 935, DateTimeKind.Utc).AddTicks(9964));

            migrationBuilder.UpdateData(
                table: "AreasProgreso",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedAt",
                value: new DateTime(2025, 5, 24, 22, 56, 48, 935, DateTimeKind.Utc).AddTicks(9964));

            migrationBuilder.UpdateData(
                table: "AreasProgreso",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedAt",
                value: new DateTime(2025, 5, 24, 22, 56, 48, 935, DateTimeKind.Utc).AddTicks(9964));

            migrationBuilder.UpdateData(
                table: "AreasProgreso",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreatedAt",
                value: new DateTime(2025, 5, 24, 22, 56, 48, 935, DateTimeKind.Utc).AddTicks(9964));

            migrationBuilder.UpdateData(
                table: "CategoriasHabitos",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "FechaCreacion" },
                values: new object[] { new DateTime(2025, 5, 24, 22, 56, 48, 937, DateTimeKind.Utc).AddTicks(8048), new DateTime(2025, 5, 24, 22, 56, 48, 937, DateTimeKind.Utc).AddTicks(8052) });

            migrationBuilder.UpdateData(
                table: "CategoriasHabitos",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedAt", "FechaCreacion" },
                values: new object[] { new DateTime(2025, 5, 24, 22, 56, 48, 937, DateTimeKind.Utc).AddTicks(8048), new DateTime(2025, 5, 24, 22, 56, 48, 937, DateTimeKind.Utc).AddTicks(8062) });

            migrationBuilder.UpdateData(
                table: "CategoriasHabitos",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedAt", "FechaCreacion" },
                values: new object[] { new DateTime(2025, 5, 24, 22, 56, 48, 937, DateTimeKind.Utc).AddTicks(8048), new DateTime(2025, 5, 24, 22, 56, 48, 937, DateTimeKind.Utc).AddTicks(8065) });

            migrationBuilder.UpdateData(
                table: "CategoriasHabitos",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "CreatedAt", "FechaCreacion" },
                values: new object[] { new DateTime(2025, 5, 24, 22, 56, 48, 937, DateTimeKind.Utc).AddTicks(8048), new DateTime(2025, 5, 24, 22, 56, 48, 937, DateTimeKind.Utc).AddTicks(8068) });

            migrationBuilder.UpdateData(
                table: "CategoriasHabitos",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "CreatedAt", "FechaCreacion" },
                values: new object[] { new DateTime(2025, 5, 24, 22, 56, 48, 937, DateTimeKind.Utc).AddTicks(8048), new DateTime(2025, 5, 24, 22, 56, 48, 937, DateTimeKind.Utc).AddTicks(8071) });

            migrationBuilder.UpdateData(
                table: "CategoriasHabitos",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "CreatedAt", "FechaCreacion" },
                values: new object[] { new DateTime(2025, 5, 24, 22, 56, 48, 937, DateTimeKind.Utc).AddTicks(8048), new DateTime(2025, 5, 24, 22, 56, 48, 937, DateTimeKind.Utc).AddTicks(8073) });

            migrationBuilder.UpdateData(
                table: "CategoriasHabitos",
                keyColumn: "Id",
                keyValue: 7,
                columns: new[] { "CreatedAt", "FechaCreacion" },
                values: new object[] { new DateTime(2025, 5, 24, 22, 56, 48, 937, DateTimeKind.Utc).AddTicks(8048), new DateTime(2025, 5, 24, 22, 56, 48, 937, DateTimeKind.Utc).AddTicks(8076) });

            migrationBuilder.UpdateData(
                table: "CategoriasHabitos",
                keyColumn: "Id",
                keyValue: 8,
                columns: new[] { "CreatedAt", "FechaCreacion" },
                values: new object[] { new DateTime(2025, 5, 24, 22, 56, 48, 937, DateTimeKind.Utc).AddTicks(8048), new DateTime(2025, 5, 24, 22, 56, 48, 937, DateTimeKind.Utc).AddTicks(8079) });

            migrationBuilder.UpdateData(
                table: "CategoriasProgreso",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2025, 5, 24, 22, 56, 48, 935, DateTimeKind.Utc).AddTicks(9964));

            migrationBuilder.UpdateData(
                table: "CategoriasProgreso",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2025, 5, 24, 22, 56, 48, 935, DateTimeKind.Utc).AddTicks(9964));

            migrationBuilder.UpdateData(
                table: "CategoriasProgreso",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedAt",
                value: new DateTime(2025, 5, 24, 22, 56, 48, 935, DateTimeKind.Utc).AddTicks(9964));

            migrationBuilder.UpdateData(
                table: "CategoriasProgreso",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedAt",
                value: new DateTime(2025, 5, 24, 22, 56, 48, 935, DateTimeKind.Utc).AddTicks(9964));

            migrationBuilder.UpdateData(
                table: "CategoriasProgreso",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreatedAt",
                value: new DateTime(2025, 5, 24, 22, 56, 48, 935, DateTimeKind.Utc).AddTicks(9964));

            migrationBuilder.UpdateData(
                table: "CategoriasProgreso",
                keyColumn: "Id",
                keyValue: 6,
                column: "CreatedAt",
                value: new DateTime(2025, 5, 24, 22, 56, 48, 935, DateTimeKind.Utc).AddTicks(9964));

            migrationBuilder.UpdateData(
                table: "CategoriasProgreso",
                keyColumn: "Id",
                keyValue: 7,
                column: "CreatedAt",
                value: new DateTime(2025, 5, 24, 22, 56, 48, 935, DateTimeKind.Utc).AddTicks(9964));

            migrationBuilder.UpdateData(
                table: "CategoriasProgreso",
                keyColumn: "Id",
                keyValue: 8,
                column: "CreatedAt",
                value: new DateTime(2025, 5, 24, 22, 56, 48, 935, DateTimeKind.Utc).AddTicks(9964));

            migrationBuilder.UpdateData(
                table: "CategoriasProgreso",
                keyColumn: "Id",
                keyValue: 9,
                column: "CreatedAt",
                value: new DateTime(2025, 5, 24, 22, 56, 48, 935, DateTimeKind.Utc).AddTicks(9964));

            migrationBuilder.UpdateData(
                table: "CategoriasProgreso",
                keyColumn: "Id",
                keyValue: 10,
                column: "CreatedAt",
                value: new DateTime(2025, 5, 24, 22, 56, 48, 935, DateTimeKind.Utc).AddTicks(9964));

            migrationBuilder.UpdateData(
                table: "CategoriasProgreso",
                keyColumn: "Id",
                keyValue: 11,
                column: "CreatedAt",
                value: new DateTime(2025, 5, 24, 22, 56, 48, 935, DateTimeKind.Utc).AddTicks(9964));

            migrationBuilder.UpdateData(
                table: "CategoriasProgreso",
                keyColumn: "Id",
                keyValue: 12,
                column: "CreatedAt",
                value: new DateTime(2025, 5, 24, 22, 56, 48, 935, DateTimeKind.Utc).AddTicks(9964));

            migrationBuilder.UpdateData(
                table: "CategoriasProgreso",
                keyColumn: "Id",
                keyValue: 13,
                column: "CreatedAt",
                value: new DateTime(2025, 5, 24, 22, 56, 48, 935, DateTimeKind.Utc).AddTicks(9964));

            migrationBuilder.UpdateData(
                table: "CategoriasProgreso",
                keyColumn: "Id",
                keyValue: 14,
                column: "CreatedAt",
                value: new DateTime(2025, 5, 24, 22, 56, 48, 935, DateTimeKind.Utc).AddTicks(9964));

            migrationBuilder.UpdateData(
                table: "CategoriasProgreso",
                keyColumn: "Id",
                keyValue: 15,
                column: "CreatedAt",
                value: new DateTime(2025, 5, 24, 22, 56, 48, 935, DateTimeKind.Utc).AddTicks(9964));

            migrationBuilder.UpdateData(
                table: "FrecuenciasHabitos",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "FechaCreacion" },
                values: new object[] { new DateTime(2025, 5, 24, 22, 56, 48, 937, DateTimeKind.Utc).AddTicks(8048), new DateTime(2025, 5, 24, 22, 56, 48, 937, DateTimeKind.Utc).AddTicks(8083) });

            migrationBuilder.UpdateData(
                table: "FrecuenciasHabitos",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedAt", "FechaCreacion" },
                values: new object[] { new DateTime(2025, 5, 24, 22, 56, 48, 937, DateTimeKind.Utc).AddTicks(8048), new DateTime(2025, 5, 24, 22, 56, 48, 937, DateTimeKind.Utc).AddTicks(8089) });

            migrationBuilder.UpdateData(
                table: "FrecuenciasHabitos",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedAt", "FechaCreacion" },
                values: new object[] { new DateTime(2025, 5, 24, 22, 56, 48, 937, DateTimeKind.Utc).AddTicks(8048), new DateTime(2025, 5, 24, 22, 56, 48, 937, DateTimeKind.Utc).AddTicks(8092) });

            migrationBuilder.UpdateData(
                table: "FrecuenciasHabitos",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "CreatedAt", "FechaCreacion" },
                values: new object[] { new DateTime(2025, 5, 24, 22, 56, 48, 937, DateTimeKind.Utc).AddTicks(8048), new DateTime(2025, 5, 24, 22, 56, 48, 937, DateTimeKind.Utc).AddTicks(8094) });

            migrationBuilder.UpdateData(
                table: "FrecuenciasHabitos",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "CreatedAt", "FechaCreacion" },
                values: new object[] { new DateTime(2025, 5, 24, 22, 56, 48, 937, DateTimeKind.Utc).AddTicks(8048), new DateTime(2025, 5, 24, 22, 56, 48, 937, DateTimeKind.Utc).AddTicks(8096) });

            migrationBuilder.UpdateData(
                table: "FrecuenciasHabitos",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "CreatedAt", "FechaCreacion" },
                values: new object[] { new DateTime(2025, 5, 24, 22, 56, 48, 937, DateTimeKind.Utc).AddTicks(8048), new DateTime(2025, 5, 24, 22, 56, 48, 937, DateTimeKind.Utc).AddTicks(8099) });

            migrationBuilder.UpdateData(
                table: "LifeAreas",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 5, 24, 22, 56, 48, 933, DateTimeKind.Utc).AddTicks(2060), new DateTime(2025, 5, 24, 22, 56, 48, 933, DateTimeKind.Utc).AddTicks(2060) });

            migrationBuilder.UpdateData(
                table: "LifeAreas",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 5, 24, 22, 56, 48, 933, DateTimeKind.Utc).AddTicks(2060), new DateTime(2025, 5, 24, 22, 56, 48, 933, DateTimeKind.Utc).AddTicks(2060) });

            migrationBuilder.UpdateData(
                table: "LifeAreas",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 5, 24, 22, 56, 48, 933, DateTimeKind.Utc).AddTicks(2060), new DateTime(2025, 5, 24, 22, 56, 48, 933, DateTimeKind.Utc).AddTicks(2060) });

            migrationBuilder.UpdateData(
                table: "LifeAreas",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 5, 24, 22, 56, 48, 933, DateTimeKind.Utc).AddTicks(2060), new DateTime(2025, 5, 24, 22, 56, 48, 933, DateTimeKind.Utc).AddTicks(2060) });

            migrationBuilder.UpdateData(
                table: "LifeAreas",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 5, 24, 22, 56, 48, 933, DateTimeKind.Utc).AddTicks(2060), new DateTime(2025, 5, 24, 22, 56, 48, 933, DateTimeKind.Utc).AddTicks(2060) });

            migrationBuilder.UpdateData(
                table: "LifeAreas",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 5, 24, 22, 56, 48, 933, DateTimeKind.Utc).AddTicks(2060), new DateTime(2025, 5, 24, 22, 56, 48, 933, DateTimeKind.Utc).AddTicks(2060) });

            migrationBuilder.UpdateData(
                table: "LifeAreas",
                keyColumn: "Id",
                keyValue: 7,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 5, 24, 22, 56, 48, 933, DateTimeKind.Utc).AddTicks(2060), new DateTime(2025, 5, 24, 22, 56, 48, 933, DateTimeKind.Utc).AddTicks(2060) });

            migrationBuilder.UpdateData(
                table: "LifeAreas",
                keyColumn: "Id",
                keyValue: 8,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 5, 24, 22, 56, 48, 933, DateTimeKind.Utc).AddTicks(2060), new DateTime(2025, 5, 24, 22, 56, 48, 933, DateTimeKind.Utc).AddTicks(2060) });

            migrationBuilder.UpdateData(
                table: "LifeAreas",
                keyColumn: "Id",
                keyValue: 9,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 5, 24, 22, 56, 48, 933, DateTimeKind.Utc).AddTicks(2060), new DateTime(2025, 5, 24, 22, 56, 48, 933, DateTimeKind.Utc).AddTicks(2060) });

            migrationBuilder.UpdateData(
                table: "LifeAreas",
                keyColumn: "Id",
                keyValue: 10,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 5, 24, 22, 56, 48, 933, DateTimeKind.Utc).AddTicks(2060), new DateTime(2025, 5, 24, 22, 56, 48, 933, DateTimeKind.Utc).AddTicks(2060) });

            migrationBuilder.UpdateData(
                table: "Questions",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 5, 24, 22, 56, 48, 935, DateTimeKind.Utc).AddTicks(9743), new DateTime(2025, 5, 24, 22, 56, 48, 935, DateTimeKind.Utc).AddTicks(9743) });

            migrationBuilder.UpdateData(
                table: "Questions",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 5, 24, 22, 56, 48, 935, DateTimeKind.Utc).AddTicks(9743), new DateTime(2025, 5, 24, 22, 56, 48, 935, DateTimeKind.Utc).AddTicks(9743) });

            migrationBuilder.UpdateData(
                table: "Questions",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 5, 24, 22, 56, 48, 935, DateTimeKind.Utc).AddTicks(9743), new DateTime(2025, 5, 24, 22, 56, 48, 935, DateTimeKind.Utc).AddTicks(9743) });

            migrationBuilder.UpdateData(
                table: "Questions",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 5, 24, 22, 56, 48, 935, DateTimeKind.Utc).AddTicks(9743), new DateTime(2025, 5, 24, 22, 56, 48, 935, DateTimeKind.Utc).AddTicks(9743) });

            migrationBuilder.UpdateData(
                table: "Questions",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 5, 24, 22, 56, 48, 935, DateTimeKind.Utc).AddTicks(9743), new DateTime(2025, 5, 24, 22, 56, 48, 935, DateTimeKind.Utc).AddTicks(9743) });

            migrationBuilder.UpdateData(
                table: "Questions",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 5, 24, 22, 56, 48, 935, DateTimeKind.Utc).AddTicks(9743), new DateTime(2025, 5, 24, 22, 56, 48, 935, DateTimeKind.Utc).AddTicks(9743) });

            migrationBuilder.UpdateData(
                table: "Questions",
                keyColumn: "Id",
                keyValue: 7,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 5, 24, 22, 56, 48, 935, DateTimeKind.Utc).AddTicks(9743), new DateTime(2025, 5, 24, 22, 56, 48, 935, DateTimeKind.Utc).AddTicks(9743) });

            migrationBuilder.UpdateData(
                table: "Questions",
                keyColumn: "Id",
                keyValue: 8,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 5, 24, 22, 56, 48, 935, DateTimeKind.Utc).AddTicks(9743), new DateTime(2025, 5, 24, 22, 56, 48, 935, DateTimeKind.Utc).AddTicks(9743) });

            migrationBuilder.UpdateData(
                table: "Questions",
                keyColumn: "Id",
                keyValue: 9,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 5, 24, 22, 56, 48, 935, DateTimeKind.Utc).AddTicks(9743), new DateTime(2025, 5, 24, 22, 56, 48, 935, DateTimeKind.Utc).AddTicks(9743) });

            migrationBuilder.UpdateData(
                table: "Questions",
                keyColumn: "Id",
                keyValue: 10,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 5, 24, 22, 56, 48, 935, DateTimeKind.Utc).AddTicks(9743), new DateTime(2025, 5, 24, 22, 56, 48, 935, DateTimeKind.Utc).AddTicks(9743) });
        }
    }
}
