using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ToolBox.Migrations
{
    /// <inheritdoc />
    public partial class AddDefaultLanguageToUsers : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "DefaultLanguage",
                table: "Users",
                type: "character varying(5)",
                maxLength: 5,
                nullable: false,
                defaultValue: "");

            migrationBuilder.UpdateData(
                table: "AreasProgreso",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2025, 5, 26, 17, 14, 4, 754, DateTimeKind.Utc).AddTicks(6199));

            migrationBuilder.UpdateData(
                table: "AreasProgreso",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2025, 5, 26, 17, 14, 4, 754, DateTimeKind.Utc).AddTicks(6199));

            migrationBuilder.UpdateData(
                table: "AreasProgreso",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedAt",
                value: new DateTime(2025, 5, 26, 17, 14, 4, 754, DateTimeKind.Utc).AddTicks(6199));

            migrationBuilder.UpdateData(
                table: "AreasProgreso",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedAt",
                value: new DateTime(2025, 5, 26, 17, 14, 4, 754, DateTimeKind.Utc).AddTicks(6199));

            migrationBuilder.UpdateData(
                table: "AreasProgreso",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreatedAt",
                value: new DateTime(2025, 5, 26, 17, 14, 4, 754, DateTimeKind.Utc).AddTicks(6199));

            migrationBuilder.UpdateData(
                table: "CategoriasHabitos",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "FechaCreacion" },
                values: new object[] { new DateTime(2025, 5, 26, 17, 14, 4, 755, DateTimeKind.Utc).AddTicks(6906), new DateTime(2025, 5, 26, 17, 14, 4, 755, DateTimeKind.Utc).AddTicks(6911) });

            migrationBuilder.UpdateData(
                table: "CategoriasHabitos",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedAt", "FechaCreacion" },
                values: new object[] { new DateTime(2025, 5, 26, 17, 14, 4, 755, DateTimeKind.Utc).AddTicks(6906), new DateTime(2025, 5, 26, 17, 14, 4, 755, DateTimeKind.Utc).AddTicks(6917) });

            migrationBuilder.UpdateData(
                table: "CategoriasHabitos",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedAt", "FechaCreacion" },
                values: new object[] { new DateTime(2025, 5, 26, 17, 14, 4, 755, DateTimeKind.Utc).AddTicks(6906), new DateTime(2025, 5, 26, 17, 14, 4, 755, DateTimeKind.Utc).AddTicks(6919) });

            migrationBuilder.UpdateData(
                table: "CategoriasHabitos",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "CreatedAt", "FechaCreacion" },
                values: new object[] { new DateTime(2025, 5, 26, 17, 14, 4, 755, DateTimeKind.Utc).AddTicks(6906), new DateTime(2025, 5, 26, 17, 14, 4, 755, DateTimeKind.Utc).AddTicks(6921) });

            migrationBuilder.UpdateData(
                table: "CategoriasHabitos",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "CreatedAt", "FechaCreacion" },
                values: new object[] { new DateTime(2025, 5, 26, 17, 14, 4, 755, DateTimeKind.Utc).AddTicks(6906), new DateTime(2025, 5, 26, 17, 14, 4, 755, DateTimeKind.Utc).AddTicks(6923) });

            migrationBuilder.UpdateData(
                table: "CategoriasHabitos",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "CreatedAt", "FechaCreacion" },
                values: new object[] { new DateTime(2025, 5, 26, 17, 14, 4, 755, DateTimeKind.Utc).AddTicks(6906), new DateTime(2025, 5, 26, 17, 14, 4, 755, DateTimeKind.Utc).AddTicks(6925) });

            migrationBuilder.UpdateData(
                table: "CategoriasHabitos",
                keyColumn: "Id",
                keyValue: 7,
                columns: new[] { "CreatedAt", "FechaCreacion" },
                values: new object[] { new DateTime(2025, 5, 26, 17, 14, 4, 755, DateTimeKind.Utc).AddTicks(6906), new DateTime(2025, 5, 26, 17, 14, 4, 755, DateTimeKind.Utc).AddTicks(6927) });

            migrationBuilder.UpdateData(
                table: "CategoriasHabitos",
                keyColumn: "Id",
                keyValue: 8,
                columns: new[] { "CreatedAt", "FechaCreacion" },
                values: new object[] { new DateTime(2025, 5, 26, 17, 14, 4, 755, DateTimeKind.Utc).AddTicks(6906), new DateTime(2025, 5, 26, 17, 14, 4, 755, DateTimeKind.Utc).AddTicks(6929) });

            migrationBuilder.UpdateData(
                table: "CategoriasProgreso",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2025, 5, 26, 17, 14, 4, 754, DateTimeKind.Utc).AddTicks(6199));

            migrationBuilder.UpdateData(
                table: "CategoriasProgreso",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2025, 5, 26, 17, 14, 4, 754, DateTimeKind.Utc).AddTicks(6199));

            migrationBuilder.UpdateData(
                table: "CategoriasProgreso",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedAt",
                value: new DateTime(2025, 5, 26, 17, 14, 4, 754, DateTimeKind.Utc).AddTicks(6199));

            migrationBuilder.UpdateData(
                table: "CategoriasProgreso",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedAt",
                value: new DateTime(2025, 5, 26, 17, 14, 4, 754, DateTimeKind.Utc).AddTicks(6199));

            migrationBuilder.UpdateData(
                table: "CategoriasProgreso",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreatedAt",
                value: new DateTime(2025, 5, 26, 17, 14, 4, 754, DateTimeKind.Utc).AddTicks(6199));

            migrationBuilder.UpdateData(
                table: "CategoriasProgreso",
                keyColumn: "Id",
                keyValue: 6,
                column: "CreatedAt",
                value: new DateTime(2025, 5, 26, 17, 14, 4, 754, DateTimeKind.Utc).AddTicks(6199));

            migrationBuilder.UpdateData(
                table: "CategoriasProgreso",
                keyColumn: "Id",
                keyValue: 7,
                column: "CreatedAt",
                value: new DateTime(2025, 5, 26, 17, 14, 4, 754, DateTimeKind.Utc).AddTicks(6199));

            migrationBuilder.UpdateData(
                table: "CategoriasProgreso",
                keyColumn: "Id",
                keyValue: 8,
                column: "CreatedAt",
                value: new DateTime(2025, 5, 26, 17, 14, 4, 754, DateTimeKind.Utc).AddTicks(6199));

            migrationBuilder.UpdateData(
                table: "CategoriasProgreso",
                keyColumn: "Id",
                keyValue: 9,
                column: "CreatedAt",
                value: new DateTime(2025, 5, 26, 17, 14, 4, 754, DateTimeKind.Utc).AddTicks(6199));

            migrationBuilder.UpdateData(
                table: "CategoriasProgreso",
                keyColumn: "Id",
                keyValue: 10,
                column: "CreatedAt",
                value: new DateTime(2025, 5, 26, 17, 14, 4, 754, DateTimeKind.Utc).AddTicks(6199));

            migrationBuilder.UpdateData(
                table: "CategoriasProgreso",
                keyColumn: "Id",
                keyValue: 11,
                column: "CreatedAt",
                value: new DateTime(2025, 5, 26, 17, 14, 4, 754, DateTimeKind.Utc).AddTicks(6199));

            migrationBuilder.UpdateData(
                table: "CategoriasProgreso",
                keyColumn: "Id",
                keyValue: 12,
                column: "CreatedAt",
                value: new DateTime(2025, 5, 26, 17, 14, 4, 754, DateTimeKind.Utc).AddTicks(6199));

            migrationBuilder.UpdateData(
                table: "CategoriasProgreso",
                keyColumn: "Id",
                keyValue: 13,
                column: "CreatedAt",
                value: new DateTime(2025, 5, 26, 17, 14, 4, 754, DateTimeKind.Utc).AddTicks(6199));

            migrationBuilder.UpdateData(
                table: "CategoriasProgreso",
                keyColumn: "Id",
                keyValue: 14,
                column: "CreatedAt",
                value: new DateTime(2025, 5, 26, 17, 14, 4, 754, DateTimeKind.Utc).AddTicks(6199));

            migrationBuilder.UpdateData(
                table: "CategoriasProgreso",
                keyColumn: "Id",
                keyValue: 15,
                column: "CreatedAt",
                value: new DateTime(2025, 5, 26, 17, 14, 4, 754, DateTimeKind.Utc).AddTicks(6199));

            migrationBuilder.UpdateData(
                table: "FrecuenciasHabitos",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "FechaCreacion" },
                values: new object[] { new DateTime(2025, 5, 26, 17, 14, 4, 755, DateTimeKind.Utc).AddTicks(6906), new DateTime(2025, 5, 26, 17, 14, 4, 755, DateTimeKind.Utc).AddTicks(6934) });

            migrationBuilder.UpdateData(
                table: "FrecuenciasHabitos",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedAt", "FechaCreacion" },
                values: new object[] { new DateTime(2025, 5, 26, 17, 14, 4, 755, DateTimeKind.Utc).AddTicks(6906), new DateTime(2025, 5, 26, 17, 14, 4, 755, DateTimeKind.Utc).AddTicks(6939) });

            migrationBuilder.UpdateData(
                table: "FrecuenciasHabitos",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedAt", "FechaCreacion" },
                values: new object[] { new DateTime(2025, 5, 26, 17, 14, 4, 755, DateTimeKind.Utc).AddTicks(6906), new DateTime(2025, 5, 26, 17, 14, 4, 755, DateTimeKind.Utc).AddTicks(6942) });

            migrationBuilder.UpdateData(
                table: "FrecuenciasHabitos",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "CreatedAt", "FechaCreacion" },
                values: new object[] { new DateTime(2025, 5, 26, 17, 14, 4, 755, DateTimeKind.Utc).AddTicks(6906), new DateTime(2025, 5, 26, 17, 14, 4, 755, DateTimeKind.Utc).AddTicks(6944) });

            migrationBuilder.UpdateData(
                table: "FrecuenciasHabitos",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "CreatedAt", "FechaCreacion" },
                values: new object[] { new DateTime(2025, 5, 26, 17, 14, 4, 755, DateTimeKind.Utc).AddTicks(6906), new DateTime(2025, 5, 26, 17, 14, 4, 755, DateTimeKind.Utc).AddTicks(6945) });

            migrationBuilder.UpdateData(
                table: "FrecuenciasHabitos",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "CreatedAt", "FechaCreacion" },
                values: new object[] { new DateTime(2025, 5, 26, 17, 14, 4, 755, DateTimeKind.Utc).AddTicks(6906), new DateTime(2025, 5, 26, 17, 14, 4, 755, DateTimeKind.Utc).AddTicks(6947) });

            migrationBuilder.UpdateData(
                table: "LifeAreas",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 5, 26, 17, 14, 4, 753, DateTimeKind.Utc).AddTicks(2131), new DateTime(2025, 5, 26, 17, 14, 4, 753, DateTimeKind.Utc).AddTicks(2131) });

            migrationBuilder.UpdateData(
                table: "LifeAreas",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 5, 26, 17, 14, 4, 753, DateTimeKind.Utc).AddTicks(2131), new DateTime(2025, 5, 26, 17, 14, 4, 753, DateTimeKind.Utc).AddTicks(2131) });

            migrationBuilder.UpdateData(
                table: "LifeAreas",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 5, 26, 17, 14, 4, 753, DateTimeKind.Utc).AddTicks(2131), new DateTime(2025, 5, 26, 17, 14, 4, 753, DateTimeKind.Utc).AddTicks(2131) });

            migrationBuilder.UpdateData(
                table: "LifeAreas",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 5, 26, 17, 14, 4, 753, DateTimeKind.Utc).AddTicks(2131), new DateTime(2025, 5, 26, 17, 14, 4, 753, DateTimeKind.Utc).AddTicks(2131) });

            migrationBuilder.UpdateData(
                table: "LifeAreas",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 5, 26, 17, 14, 4, 753, DateTimeKind.Utc).AddTicks(2131), new DateTime(2025, 5, 26, 17, 14, 4, 753, DateTimeKind.Utc).AddTicks(2131) });

            migrationBuilder.UpdateData(
                table: "LifeAreas",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 5, 26, 17, 14, 4, 753, DateTimeKind.Utc).AddTicks(2131), new DateTime(2025, 5, 26, 17, 14, 4, 753, DateTimeKind.Utc).AddTicks(2131) });

            migrationBuilder.UpdateData(
                table: "LifeAreas",
                keyColumn: "Id",
                keyValue: 7,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 5, 26, 17, 14, 4, 753, DateTimeKind.Utc).AddTicks(2131), new DateTime(2025, 5, 26, 17, 14, 4, 753, DateTimeKind.Utc).AddTicks(2131) });

            migrationBuilder.UpdateData(
                table: "LifeAreas",
                keyColumn: "Id",
                keyValue: 8,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 5, 26, 17, 14, 4, 753, DateTimeKind.Utc).AddTicks(2131), new DateTime(2025, 5, 26, 17, 14, 4, 753, DateTimeKind.Utc).AddTicks(2131) });

            migrationBuilder.UpdateData(
                table: "LifeAreas",
                keyColumn: "Id",
                keyValue: 9,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 5, 26, 17, 14, 4, 753, DateTimeKind.Utc).AddTicks(2131), new DateTime(2025, 5, 26, 17, 14, 4, 753, DateTimeKind.Utc).AddTicks(2131) });

            migrationBuilder.UpdateData(
                table: "LifeAreas",
                keyColumn: "Id",
                keyValue: 10,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 5, 26, 17, 14, 4, 753, DateTimeKind.Utc).AddTicks(2131), new DateTime(2025, 5, 26, 17, 14, 4, 753, DateTimeKind.Utc).AddTicks(2131) });

            migrationBuilder.UpdateData(
                table: "Questions",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 5, 26, 17, 14, 4, 754, DateTimeKind.Utc).AddTicks(5986), new DateTime(2025, 5, 26, 17, 14, 4, 754, DateTimeKind.Utc).AddTicks(5986) });

            migrationBuilder.UpdateData(
                table: "Questions",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 5, 26, 17, 14, 4, 754, DateTimeKind.Utc).AddTicks(5986), new DateTime(2025, 5, 26, 17, 14, 4, 754, DateTimeKind.Utc).AddTicks(5986) });

            migrationBuilder.UpdateData(
                table: "Questions",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 5, 26, 17, 14, 4, 754, DateTimeKind.Utc).AddTicks(5986), new DateTime(2025, 5, 26, 17, 14, 4, 754, DateTimeKind.Utc).AddTicks(5986) });

            migrationBuilder.UpdateData(
                table: "Questions",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 5, 26, 17, 14, 4, 754, DateTimeKind.Utc).AddTicks(5986), new DateTime(2025, 5, 26, 17, 14, 4, 754, DateTimeKind.Utc).AddTicks(5986) });

            migrationBuilder.UpdateData(
                table: "Questions",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 5, 26, 17, 14, 4, 754, DateTimeKind.Utc).AddTicks(5986), new DateTime(2025, 5, 26, 17, 14, 4, 754, DateTimeKind.Utc).AddTicks(5986) });

            migrationBuilder.UpdateData(
                table: "Questions",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 5, 26, 17, 14, 4, 754, DateTimeKind.Utc).AddTicks(5986), new DateTime(2025, 5, 26, 17, 14, 4, 754, DateTimeKind.Utc).AddTicks(5986) });

            migrationBuilder.UpdateData(
                table: "Questions",
                keyColumn: "Id",
                keyValue: 7,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 5, 26, 17, 14, 4, 754, DateTimeKind.Utc).AddTicks(5986), new DateTime(2025, 5, 26, 17, 14, 4, 754, DateTimeKind.Utc).AddTicks(5986) });

            migrationBuilder.UpdateData(
                table: "Questions",
                keyColumn: "Id",
                keyValue: 8,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 5, 26, 17, 14, 4, 754, DateTimeKind.Utc).AddTicks(5986), new DateTime(2025, 5, 26, 17, 14, 4, 754, DateTimeKind.Utc).AddTicks(5986) });

            migrationBuilder.UpdateData(
                table: "Questions",
                keyColumn: "Id",
                keyValue: 9,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 5, 26, 17, 14, 4, 754, DateTimeKind.Utc).AddTicks(5986), new DateTime(2025, 5, 26, 17, 14, 4, 754, DateTimeKind.Utc).AddTicks(5986) });

            migrationBuilder.UpdateData(
                table: "Questions",
                keyColumn: "Id",
                keyValue: 10,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 5, 26, 17, 14, 4, 754, DateTimeKind.Utc).AddTicks(5986), new DateTime(2025, 5, 26, 17, 14, 4, 754, DateTimeKind.Utc).AddTicks(5986) });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DefaultLanguage",
                table: "Users");

            migrationBuilder.UpdateData(
                table: "AreasProgreso",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2025, 5, 26, 14, 39, 28, 712, DateTimeKind.Utc).AddTicks(8197));

            migrationBuilder.UpdateData(
                table: "AreasProgreso",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2025, 5, 26, 14, 39, 28, 712, DateTimeKind.Utc).AddTicks(8197));

            migrationBuilder.UpdateData(
                table: "AreasProgreso",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedAt",
                value: new DateTime(2025, 5, 26, 14, 39, 28, 712, DateTimeKind.Utc).AddTicks(8197));

            migrationBuilder.UpdateData(
                table: "AreasProgreso",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedAt",
                value: new DateTime(2025, 5, 26, 14, 39, 28, 712, DateTimeKind.Utc).AddTicks(8197));

            migrationBuilder.UpdateData(
                table: "AreasProgreso",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreatedAt",
                value: new DateTime(2025, 5, 26, 14, 39, 28, 712, DateTimeKind.Utc).AddTicks(8197));

            migrationBuilder.UpdateData(
                table: "CategoriasHabitos",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "FechaCreacion" },
                values: new object[] { new DateTime(2025, 5, 26, 14, 39, 28, 713, DateTimeKind.Utc).AddTicks(8961), new DateTime(2025, 5, 26, 14, 39, 28, 713, DateTimeKind.Utc).AddTicks(8964) });

            migrationBuilder.UpdateData(
                table: "CategoriasHabitos",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedAt", "FechaCreacion" },
                values: new object[] { new DateTime(2025, 5, 26, 14, 39, 28, 713, DateTimeKind.Utc).AddTicks(8961), new DateTime(2025, 5, 26, 14, 39, 28, 713, DateTimeKind.Utc).AddTicks(8971) });

            migrationBuilder.UpdateData(
                table: "CategoriasHabitos",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedAt", "FechaCreacion" },
                values: new object[] { new DateTime(2025, 5, 26, 14, 39, 28, 713, DateTimeKind.Utc).AddTicks(8961), new DateTime(2025, 5, 26, 14, 39, 28, 713, DateTimeKind.Utc).AddTicks(8973) });

            migrationBuilder.UpdateData(
                table: "CategoriasHabitos",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "CreatedAt", "FechaCreacion" },
                values: new object[] { new DateTime(2025, 5, 26, 14, 39, 28, 713, DateTimeKind.Utc).AddTicks(8961), new DateTime(2025, 5, 26, 14, 39, 28, 713, DateTimeKind.Utc).AddTicks(8975) });

            migrationBuilder.UpdateData(
                table: "CategoriasHabitos",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "CreatedAt", "FechaCreacion" },
                values: new object[] { new DateTime(2025, 5, 26, 14, 39, 28, 713, DateTimeKind.Utc).AddTicks(8961), new DateTime(2025, 5, 26, 14, 39, 28, 713, DateTimeKind.Utc).AddTicks(8978) });

            migrationBuilder.UpdateData(
                table: "CategoriasHabitos",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "CreatedAt", "FechaCreacion" },
                values: new object[] { new DateTime(2025, 5, 26, 14, 39, 28, 713, DateTimeKind.Utc).AddTicks(8961), new DateTime(2025, 5, 26, 14, 39, 28, 713, DateTimeKind.Utc).AddTicks(8980) });

            migrationBuilder.UpdateData(
                table: "CategoriasHabitos",
                keyColumn: "Id",
                keyValue: 7,
                columns: new[] { "CreatedAt", "FechaCreacion" },
                values: new object[] { new DateTime(2025, 5, 26, 14, 39, 28, 713, DateTimeKind.Utc).AddTicks(8961), new DateTime(2025, 5, 26, 14, 39, 28, 713, DateTimeKind.Utc).AddTicks(8981) });

            migrationBuilder.UpdateData(
                table: "CategoriasHabitos",
                keyColumn: "Id",
                keyValue: 8,
                columns: new[] { "CreatedAt", "FechaCreacion" },
                values: new object[] { new DateTime(2025, 5, 26, 14, 39, 28, 713, DateTimeKind.Utc).AddTicks(8961), new DateTime(2025, 5, 26, 14, 39, 28, 713, DateTimeKind.Utc).AddTicks(8984) });

            migrationBuilder.UpdateData(
                table: "CategoriasProgreso",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2025, 5, 26, 14, 39, 28, 712, DateTimeKind.Utc).AddTicks(8197));

            migrationBuilder.UpdateData(
                table: "CategoriasProgreso",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2025, 5, 26, 14, 39, 28, 712, DateTimeKind.Utc).AddTicks(8197));

            migrationBuilder.UpdateData(
                table: "CategoriasProgreso",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedAt",
                value: new DateTime(2025, 5, 26, 14, 39, 28, 712, DateTimeKind.Utc).AddTicks(8197));

            migrationBuilder.UpdateData(
                table: "CategoriasProgreso",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedAt",
                value: new DateTime(2025, 5, 26, 14, 39, 28, 712, DateTimeKind.Utc).AddTicks(8197));

            migrationBuilder.UpdateData(
                table: "CategoriasProgreso",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreatedAt",
                value: new DateTime(2025, 5, 26, 14, 39, 28, 712, DateTimeKind.Utc).AddTicks(8197));

            migrationBuilder.UpdateData(
                table: "CategoriasProgreso",
                keyColumn: "Id",
                keyValue: 6,
                column: "CreatedAt",
                value: new DateTime(2025, 5, 26, 14, 39, 28, 712, DateTimeKind.Utc).AddTicks(8197));

            migrationBuilder.UpdateData(
                table: "CategoriasProgreso",
                keyColumn: "Id",
                keyValue: 7,
                column: "CreatedAt",
                value: new DateTime(2025, 5, 26, 14, 39, 28, 712, DateTimeKind.Utc).AddTicks(8197));

            migrationBuilder.UpdateData(
                table: "CategoriasProgreso",
                keyColumn: "Id",
                keyValue: 8,
                column: "CreatedAt",
                value: new DateTime(2025, 5, 26, 14, 39, 28, 712, DateTimeKind.Utc).AddTicks(8197));

            migrationBuilder.UpdateData(
                table: "CategoriasProgreso",
                keyColumn: "Id",
                keyValue: 9,
                column: "CreatedAt",
                value: new DateTime(2025, 5, 26, 14, 39, 28, 712, DateTimeKind.Utc).AddTicks(8197));

            migrationBuilder.UpdateData(
                table: "CategoriasProgreso",
                keyColumn: "Id",
                keyValue: 10,
                column: "CreatedAt",
                value: new DateTime(2025, 5, 26, 14, 39, 28, 712, DateTimeKind.Utc).AddTicks(8197));

            migrationBuilder.UpdateData(
                table: "CategoriasProgreso",
                keyColumn: "Id",
                keyValue: 11,
                column: "CreatedAt",
                value: new DateTime(2025, 5, 26, 14, 39, 28, 712, DateTimeKind.Utc).AddTicks(8197));

            migrationBuilder.UpdateData(
                table: "CategoriasProgreso",
                keyColumn: "Id",
                keyValue: 12,
                column: "CreatedAt",
                value: new DateTime(2025, 5, 26, 14, 39, 28, 712, DateTimeKind.Utc).AddTicks(8197));

            migrationBuilder.UpdateData(
                table: "CategoriasProgreso",
                keyColumn: "Id",
                keyValue: 13,
                column: "CreatedAt",
                value: new DateTime(2025, 5, 26, 14, 39, 28, 712, DateTimeKind.Utc).AddTicks(8197));

            migrationBuilder.UpdateData(
                table: "CategoriasProgreso",
                keyColumn: "Id",
                keyValue: 14,
                column: "CreatedAt",
                value: new DateTime(2025, 5, 26, 14, 39, 28, 712, DateTimeKind.Utc).AddTicks(8197));

            migrationBuilder.UpdateData(
                table: "CategoriasProgreso",
                keyColumn: "Id",
                keyValue: 15,
                column: "CreatedAt",
                value: new DateTime(2025, 5, 26, 14, 39, 28, 712, DateTimeKind.Utc).AddTicks(8197));

            migrationBuilder.UpdateData(
                table: "FrecuenciasHabitos",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "FechaCreacion" },
                values: new object[] { new DateTime(2025, 5, 26, 14, 39, 28, 713, DateTimeKind.Utc).AddTicks(8961), new DateTime(2025, 5, 26, 14, 39, 28, 713, DateTimeKind.Utc).AddTicks(8987) });

            migrationBuilder.UpdateData(
                table: "FrecuenciasHabitos",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedAt", "FechaCreacion" },
                values: new object[] { new DateTime(2025, 5, 26, 14, 39, 28, 713, DateTimeKind.Utc).AddTicks(8961), new DateTime(2025, 5, 26, 14, 39, 28, 713, DateTimeKind.Utc).AddTicks(8992) });

            migrationBuilder.UpdateData(
                table: "FrecuenciasHabitos",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedAt", "FechaCreacion" },
                values: new object[] { new DateTime(2025, 5, 26, 14, 39, 28, 713, DateTimeKind.Utc).AddTicks(8961), new DateTime(2025, 5, 26, 14, 39, 28, 713, DateTimeKind.Utc).AddTicks(8994) });

            migrationBuilder.UpdateData(
                table: "FrecuenciasHabitos",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "CreatedAt", "FechaCreacion" },
                values: new object[] { new DateTime(2025, 5, 26, 14, 39, 28, 713, DateTimeKind.Utc).AddTicks(8961), new DateTime(2025, 5, 26, 14, 39, 28, 713, DateTimeKind.Utc).AddTicks(8996) });

            migrationBuilder.UpdateData(
                table: "FrecuenciasHabitos",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "CreatedAt", "FechaCreacion" },
                values: new object[] { new DateTime(2025, 5, 26, 14, 39, 28, 713, DateTimeKind.Utc).AddTicks(8961), new DateTime(2025, 5, 26, 14, 39, 28, 713, DateTimeKind.Utc).AddTicks(8998) });

            migrationBuilder.UpdateData(
                table: "FrecuenciasHabitos",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "CreatedAt", "FechaCreacion" },
                values: new object[] { new DateTime(2025, 5, 26, 14, 39, 28, 713, DateTimeKind.Utc).AddTicks(8961), new DateTime(2025, 5, 26, 14, 39, 28, 713, DateTimeKind.Utc).AddTicks(9000) });

            migrationBuilder.UpdateData(
                table: "LifeAreas",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 5, 26, 14, 39, 28, 711, DateTimeKind.Utc).AddTicks(3727), new DateTime(2025, 5, 26, 14, 39, 28, 711, DateTimeKind.Utc).AddTicks(3727) });

            migrationBuilder.UpdateData(
                table: "LifeAreas",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 5, 26, 14, 39, 28, 711, DateTimeKind.Utc).AddTicks(3727), new DateTime(2025, 5, 26, 14, 39, 28, 711, DateTimeKind.Utc).AddTicks(3727) });

            migrationBuilder.UpdateData(
                table: "LifeAreas",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 5, 26, 14, 39, 28, 711, DateTimeKind.Utc).AddTicks(3727), new DateTime(2025, 5, 26, 14, 39, 28, 711, DateTimeKind.Utc).AddTicks(3727) });

            migrationBuilder.UpdateData(
                table: "LifeAreas",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 5, 26, 14, 39, 28, 711, DateTimeKind.Utc).AddTicks(3727), new DateTime(2025, 5, 26, 14, 39, 28, 711, DateTimeKind.Utc).AddTicks(3727) });

            migrationBuilder.UpdateData(
                table: "LifeAreas",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 5, 26, 14, 39, 28, 711, DateTimeKind.Utc).AddTicks(3727), new DateTime(2025, 5, 26, 14, 39, 28, 711, DateTimeKind.Utc).AddTicks(3727) });

            migrationBuilder.UpdateData(
                table: "LifeAreas",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 5, 26, 14, 39, 28, 711, DateTimeKind.Utc).AddTicks(3727), new DateTime(2025, 5, 26, 14, 39, 28, 711, DateTimeKind.Utc).AddTicks(3727) });

            migrationBuilder.UpdateData(
                table: "LifeAreas",
                keyColumn: "Id",
                keyValue: 7,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 5, 26, 14, 39, 28, 711, DateTimeKind.Utc).AddTicks(3727), new DateTime(2025, 5, 26, 14, 39, 28, 711, DateTimeKind.Utc).AddTicks(3727) });

            migrationBuilder.UpdateData(
                table: "LifeAreas",
                keyColumn: "Id",
                keyValue: 8,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 5, 26, 14, 39, 28, 711, DateTimeKind.Utc).AddTicks(3727), new DateTime(2025, 5, 26, 14, 39, 28, 711, DateTimeKind.Utc).AddTicks(3727) });

            migrationBuilder.UpdateData(
                table: "LifeAreas",
                keyColumn: "Id",
                keyValue: 9,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 5, 26, 14, 39, 28, 711, DateTimeKind.Utc).AddTicks(3727), new DateTime(2025, 5, 26, 14, 39, 28, 711, DateTimeKind.Utc).AddTicks(3727) });

            migrationBuilder.UpdateData(
                table: "LifeAreas",
                keyColumn: "Id",
                keyValue: 10,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 5, 26, 14, 39, 28, 711, DateTimeKind.Utc).AddTicks(3727), new DateTime(2025, 5, 26, 14, 39, 28, 711, DateTimeKind.Utc).AddTicks(3727) });

            migrationBuilder.UpdateData(
                table: "Questions",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 5, 26, 14, 39, 28, 712, DateTimeKind.Utc).AddTicks(7899), new DateTime(2025, 5, 26, 14, 39, 28, 712, DateTimeKind.Utc).AddTicks(7899) });

            migrationBuilder.UpdateData(
                table: "Questions",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 5, 26, 14, 39, 28, 712, DateTimeKind.Utc).AddTicks(7899), new DateTime(2025, 5, 26, 14, 39, 28, 712, DateTimeKind.Utc).AddTicks(7899) });

            migrationBuilder.UpdateData(
                table: "Questions",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 5, 26, 14, 39, 28, 712, DateTimeKind.Utc).AddTicks(7899), new DateTime(2025, 5, 26, 14, 39, 28, 712, DateTimeKind.Utc).AddTicks(7899) });

            migrationBuilder.UpdateData(
                table: "Questions",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 5, 26, 14, 39, 28, 712, DateTimeKind.Utc).AddTicks(7899), new DateTime(2025, 5, 26, 14, 39, 28, 712, DateTimeKind.Utc).AddTicks(7899) });

            migrationBuilder.UpdateData(
                table: "Questions",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 5, 26, 14, 39, 28, 712, DateTimeKind.Utc).AddTicks(7899), new DateTime(2025, 5, 26, 14, 39, 28, 712, DateTimeKind.Utc).AddTicks(7899) });

            migrationBuilder.UpdateData(
                table: "Questions",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 5, 26, 14, 39, 28, 712, DateTimeKind.Utc).AddTicks(7899), new DateTime(2025, 5, 26, 14, 39, 28, 712, DateTimeKind.Utc).AddTicks(7899) });

            migrationBuilder.UpdateData(
                table: "Questions",
                keyColumn: "Id",
                keyValue: 7,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 5, 26, 14, 39, 28, 712, DateTimeKind.Utc).AddTicks(7899), new DateTime(2025, 5, 26, 14, 39, 28, 712, DateTimeKind.Utc).AddTicks(7899) });

            migrationBuilder.UpdateData(
                table: "Questions",
                keyColumn: "Id",
                keyValue: 8,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 5, 26, 14, 39, 28, 712, DateTimeKind.Utc).AddTicks(7899), new DateTime(2025, 5, 26, 14, 39, 28, 712, DateTimeKind.Utc).AddTicks(7899) });

            migrationBuilder.UpdateData(
                table: "Questions",
                keyColumn: "Id",
                keyValue: 9,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 5, 26, 14, 39, 28, 712, DateTimeKind.Utc).AddTicks(7899), new DateTime(2025, 5, 26, 14, 39, 28, 712, DateTimeKind.Utc).AddTicks(7899) });

            migrationBuilder.UpdateData(
                table: "Questions",
                keyColumn: "Id",
                keyValue: 10,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 5, 26, 14, 39, 28, 712, DateTimeKind.Utc).AddTicks(7899), new DateTime(2025, 5, 26, 14, 39, 28, 712, DateTimeKind.Utc).AddTicks(7899) });
        }
    }
}
