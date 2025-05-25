using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ToolBox.Migrations
{
    /// <inheritdoc />
    public partial class MakeTareaDescripcionNullable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Descripcion",
                table: "Tareas",
                type: "character varying(500)",
                maxLength: 500,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "character varying(500)",
                oldMaxLength: 500);

            migrationBuilder.UpdateData(
                table: "AreasProgreso",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2025, 5, 24, 21, 55, 51, 498, DateTimeKind.Utc).AddTicks(2326));

            migrationBuilder.UpdateData(
                table: "AreasProgreso",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2025, 5, 24, 21, 55, 51, 498, DateTimeKind.Utc).AddTicks(2326));

            migrationBuilder.UpdateData(
                table: "AreasProgreso",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedAt",
                value: new DateTime(2025, 5, 24, 21, 55, 51, 498, DateTimeKind.Utc).AddTicks(2326));

            migrationBuilder.UpdateData(
                table: "AreasProgreso",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedAt",
                value: new DateTime(2025, 5, 24, 21, 55, 51, 498, DateTimeKind.Utc).AddTicks(2326));

            migrationBuilder.UpdateData(
                table: "AreasProgreso",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreatedAt",
                value: new DateTime(2025, 5, 24, 21, 55, 51, 498, DateTimeKind.Utc).AddTicks(2326));

            migrationBuilder.UpdateData(
                table: "CategoriasProgreso",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2025, 5, 24, 21, 55, 51, 498, DateTimeKind.Utc).AddTicks(2326));

            migrationBuilder.UpdateData(
                table: "CategoriasProgreso",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2025, 5, 24, 21, 55, 51, 498, DateTimeKind.Utc).AddTicks(2326));

            migrationBuilder.UpdateData(
                table: "CategoriasProgreso",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedAt",
                value: new DateTime(2025, 5, 24, 21, 55, 51, 498, DateTimeKind.Utc).AddTicks(2326));

            migrationBuilder.UpdateData(
                table: "CategoriasProgreso",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedAt",
                value: new DateTime(2025, 5, 24, 21, 55, 51, 498, DateTimeKind.Utc).AddTicks(2326));

            migrationBuilder.UpdateData(
                table: "CategoriasProgreso",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreatedAt",
                value: new DateTime(2025, 5, 24, 21, 55, 51, 498, DateTimeKind.Utc).AddTicks(2326));

            migrationBuilder.UpdateData(
                table: "CategoriasProgreso",
                keyColumn: "Id",
                keyValue: 6,
                column: "CreatedAt",
                value: new DateTime(2025, 5, 24, 21, 55, 51, 498, DateTimeKind.Utc).AddTicks(2326));

            migrationBuilder.UpdateData(
                table: "CategoriasProgreso",
                keyColumn: "Id",
                keyValue: 7,
                column: "CreatedAt",
                value: new DateTime(2025, 5, 24, 21, 55, 51, 498, DateTimeKind.Utc).AddTicks(2326));

            migrationBuilder.UpdateData(
                table: "CategoriasProgreso",
                keyColumn: "Id",
                keyValue: 8,
                column: "CreatedAt",
                value: new DateTime(2025, 5, 24, 21, 55, 51, 498, DateTimeKind.Utc).AddTicks(2326));

            migrationBuilder.UpdateData(
                table: "CategoriasProgreso",
                keyColumn: "Id",
                keyValue: 9,
                column: "CreatedAt",
                value: new DateTime(2025, 5, 24, 21, 55, 51, 498, DateTimeKind.Utc).AddTicks(2326));

            migrationBuilder.UpdateData(
                table: "CategoriasProgreso",
                keyColumn: "Id",
                keyValue: 10,
                column: "CreatedAt",
                value: new DateTime(2025, 5, 24, 21, 55, 51, 498, DateTimeKind.Utc).AddTicks(2326));

            migrationBuilder.UpdateData(
                table: "CategoriasProgreso",
                keyColumn: "Id",
                keyValue: 11,
                column: "CreatedAt",
                value: new DateTime(2025, 5, 24, 21, 55, 51, 498, DateTimeKind.Utc).AddTicks(2326));

            migrationBuilder.UpdateData(
                table: "CategoriasProgreso",
                keyColumn: "Id",
                keyValue: 12,
                column: "CreatedAt",
                value: new DateTime(2025, 5, 24, 21, 55, 51, 498, DateTimeKind.Utc).AddTicks(2326));

            migrationBuilder.UpdateData(
                table: "CategoriasProgreso",
                keyColumn: "Id",
                keyValue: 13,
                column: "CreatedAt",
                value: new DateTime(2025, 5, 24, 21, 55, 51, 498, DateTimeKind.Utc).AddTicks(2326));

            migrationBuilder.UpdateData(
                table: "CategoriasProgreso",
                keyColumn: "Id",
                keyValue: 14,
                column: "CreatedAt",
                value: new DateTime(2025, 5, 24, 21, 55, 51, 498, DateTimeKind.Utc).AddTicks(2326));

            migrationBuilder.UpdateData(
                table: "CategoriasProgreso",
                keyColumn: "Id",
                keyValue: 15,
                column: "CreatedAt",
                value: new DateTime(2025, 5, 24, 21, 55, 51, 498, DateTimeKind.Utc).AddTicks(2326));

            migrationBuilder.UpdateData(
                table: "LifeAreas",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 5, 24, 21, 55, 51, 496, DateTimeKind.Utc).AddTicks(5634), new DateTime(2025, 5, 24, 21, 55, 51, 496, DateTimeKind.Utc).AddTicks(5634) });

            migrationBuilder.UpdateData(
                table: "LifeAreas",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 5, 24, 21, 55, 51, 496, DateTimeKind.Utc).AddTicks(5634), new DateTime(2025, 5, 24, 21, 55, 51, 496, DateTimeKind.Utc).AddTicks(5634) });

            migrationBuilder.UpdateData(
                table: "LifeAreas",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 5, 24, 21, 55, 51, 496, DateTimeKind.Utc).AddTicks(5634), new DateTime(2025, 5, 24, 21, 55, 51, 496, DateTimeKind.Utc).AddTicks(5634) });

            migrationBuilder.UpdateData(
                table: "LifeAreas",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 5, 24, 21, 55, 51, 496, DateTimeKind.Utc).AddTicks(5634), new DateTime(2025, 5, 24, 21, 55, 51, 496, DateTimeKind.Utc).AddTicks(5634) });

            migrationBuilder.UpdateData(
                table: "LifeAreas",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 5, 24, 21, 55, 51, 496, DateTimeKind.Utc).AddTicks(5634), new DateTime(2025, 5, 24, 21, 55, 51, 496, DateTimeKind.Utc).AddTicks(5634) });

            migrationBuilder.UpdateData(
                table: "LifeAreas",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 5, 24, 21, 55, 51, 496, DateTimeKind.Utc).AddTicks(5634), new DateTime(2025, 5, 24, 21, 55, 51, 496, DateTimeKind.Utc).AddTicks(5634) });

            migrationBuilder.UpdateData(
                table: "LifeAreas",
                keyColumn: "Id",
                keyValue: 7,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 5, 24, 21, 55, 51, 496, DateTimeKind.Utc).AddTicks(5634), new DateTime(2025, 5, 24, 21, 55, 51, 496, DateTimeKind.Utc).AddTicks(5634) });

            migrationBuilder.UpdateData(
                table: "LifeAreas",
                keyColumn: "Id",
                keyValue: 8,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 5, 24, 21, 55, 51, 496, DateTimeKind.Utc).AddTicks(5634), new DateTime(2025, 5, 24, 21, 55, 51, 496, DateTimeKind.Utc).AddTicks(5634) });

            migrationBuilder.UpdateData(
                table: "LifeAreas",
                keyColumn: "Id",
                keyValue: 9,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 5, 24, 21, 55, 51, 496, DateTimeKind.Utc).AddTicks(5634), new DateTime(2025, 5, 24, 21, 55, 51, 496, DateTimeKind.Utc).AddTicks(5634) });

            migrationBuilder.UpdateData(
                table: "LifeAreas",
                keyColumn: "Id",
                keyValue: 10,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 5, 24, 21, 55, 51, 496, DateTimeKind.Utc).AddTicks(5634), new DateTime(2025, 5, 24, 21, 55, 51, 496, DateTimeKind.Utc).AddTicks(5634) });

            migrationBuilder.UpdateData(
                table: "Questions",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 5, 24, 21, 55, 51, 498, DateTimeKind.Utc).AddTicks(2124), new DateTime(2025, 5, 24, 21, 55, 51, 498, DateTimeKind.Utc).AddTicks(2124) });

            migrationBuilder.UpdateData(
                table: "Questions",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 5, 24, 21, 55, 51, 498, DateTimeKind.Utc).AddTicks(2124), new DateTime(2025, 5, 24, 21, 55, 51, 498, DateTimeKind.Utc).AddTicks(2124) });

            migrationBuilder.UpdateData(
                table: "Questions",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 5, 24, 21, 55, 51, 498, DateTimeKind.Utc).AddTicks(2124), new DateTime(2025, 5, 24, 21, 55, 51, 498, DateTimeKind.Utc).AddTicks(2124) });

            migrationBuilder.UpdateData(
                table: "Questions",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 5, 24, 21, 55, 51, 498, DateTimeKind.Utc).AddTicks(2124), new DateTime(2025, 5, 24, 21, 55, 51, 498, DateTimeKind.Utc).AddTicks(2124) });

            migrationBuilder.UpdateData(
                table: "Questions",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 5, 24, 21, 55, 51, 498, DateTimeKind.Utc).AddTicks(2124), new DateTime(2025, 5, 24, 21, 55, 51, 498, DateTimeKind.Utc).AddTicks(2124) });

            migrationBuilder.UpdateData(
                table: "Questions",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 5, 24, 21, 55, 51, 498, DateTimeKind.Utc).AddTicks(2124), new DateTime(2025, 5, 24, 21, 55, 51, 498, DateTimeKind.Utc).AddTicks(2124) });

            migrationBuilder.UpdateData(
                table: "Questions",
                keyColumn: "Id",
                keyValue: 7,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 5, 24, 21, 55, 51, 498, DateTimeKind.Utc).AddTicks(2124), new DateTime(2025, 5, 24, 21, 55, 51, 498, DateTimeKind.Utc).AddTicks(2124) });

            migrationBuilder.UpdateData(
                table: "Questions",
                keyColumn: "Id",
                keyValue: 8,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 5, 24, 21, 55, 51, 498, DateTimeKind.Utc).AddTicks(2124), new DateTime(2025, 5, 24, 21, 55, 51, 498, DateTimeKind.Utc).AddTicks(2124) });

            migrationBuilder.UpdateData(
                table: "Questions",
                keyColumn: "Id",
                keyValue: 9,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 5, 24, 21, 55, 51, 498, DateTimeKind.Utc).AddTicks(2124), new DateTime(2025, 5, 24, 21, 55, 51, 498, DateTimeKind.Utc).AddTicks(2124) });

            migrationBuilder.UpdateData(
                table: "Questions",
                keyColumn: "Id",
                keyValue: 10,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 5, 24, 21, 55, 51, 498, DateTimeKind.Utc).AddTicks(2124), new DateTime(2025, 5, 24, 21, 55, 51, 498, DateTimeKind.Utc).AddTicks(2124) });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Descripcion",
                table: "Tareas",
                type: "character varying(500)",
                maxLength: 500,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "character varying(500)",
                oldMaxLength: 500,
                oldNullable: true);

            migrationBuilder.UpdateData(
                table: "AreasProgreso",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2025, 5, 24, 21, 26, 4, 839, DateTimeKind.Utc).AddTicks(4669));

            migrationBuilder.UpdateData(
                table: "AreasProgreso",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2025, 5, 24, 21, 26, 4, 839, DateTimeKind.Utc).AddTicks(4669));

            migrationBuilder.UpdateData(
                table: "AreasProgreso",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedAt",
                value: new DateTime(2025, 5, 24, 21, 26, 4, 839, DateTimeKind.Utc).AddTicks(4669));

            migrationBuilder.UpdateData(
                table: "AreasProgreso",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedAt",
                value: new DateTime(2025, 5, 24, 21, 26, 4, 839, DateTimeKind.Utc).AddTicks(4669));

            migrationBuilder.UpdateData(
                table: "AreasProgreso",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreatedAt",
                value: new DateTime(2025, 5, 24, 21, 26, 4, 839, DateTimeKind.Utc).AddTicks(4669));

            migrationBuilder.UpdateData(
                table: "CategoriasProgreso",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2025, 5, 24, 21, 26, 4, 839, DateTimeKind.Utc).AddTicks(4669));

            migrationBuilder.UpdateData(
                table: "CategoriasProgreso",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2025, 5, 24, 21, 26, 4, 839, DateTimeKind.Utc).AddTicks(4669));

            migrationBuilder.UpdateData(
                table: "CategoriasProgreso",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedAt",
                value: new DateTime(2025, 5, 24, 21, 26, 4, 839, DateTimeKind.Utc).AddTicks(4669));

            migrationBuilder.UpdateData(
                table: "CategoriasProgreso",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedAt",
                value: new DateTime(2025, 5, 24, 21, 26, 4, 839, DateTimeKind.Utc).AddTicks(4669));

            migrationBuilder.UpdateData(
                table: "CategoriasProgreso",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreatedAt",
                value: new DateTime(2025, 5, 24, 21, 26, 4, 839, DateTimeKind.Utc).AddTicks(4669));

            migrationBuilder.UpdateData(
                table: "CategoriasProgreso",
                keyColumn: "Id",
                keyValue: 6,
                column: "CreatedAt",
                value: new DateTime(2025, 5, 24, 21, 26, 4, 839, DateTimeKind.Utc).AddTicks(4669));

            migrationBuilder.UpdateData(
                table: "CategoriasProgreso",
                keyColumn: "Id",
                keyValue: 7,
                column: "CreatedAt",
                value: new DateTime(2025, 5, 24, 21, 26, 4, 839, DateTimeKind.Utc).AddTicks(4669));

            migrationBuilder.UpdateData(
                table: "CategoriasProgreso",
                keyColumn: "Id",
                keyValue: 8,
                column: "CreatedAt",
                value: new DateTime(2025, 5, 24, 21, 26, 4, 839, DateTimeKind.Utc).AddTicks(4669));

            migrationBuilder.UpdateData(
                table: "CategoriasProgreso",
                keyColumn: "Id",
                keyValue: 9,
                column: "CreatedAt",
                value: new DateTime(2025, 5, 24, 21, 26, 4, 839, DateTimeKind.Utc).AddTicks(4669));

            migrationBuilder.UpdateData(
                table: "CategoriasProgreso",
                keyColumn: "Id",
                keyValue: 10,
                column: "CreatedAt",
                value: new DateTime(2025, 5, 24, 21, 26, 4, 839, DateTimeKind.Utc).AddTicks(4669));

            migrationBuilder.UpdateData(
                table: "CategoriasProgreso",
                keyColumn: "Id",
                keyValue: 11,
                column: "CreatedAt",
                value: new DateTime(2025, 5, 24, 21, 26, 4, 839, DateTimeKind.Utc).AddTicks(4669));

            migrationBuilder.UpdateData(
                table: "CategoriasProgreso",
                keyColumn: "Id",
                keyValue: 12,
                column: "CreatedAt",
                value: new DateTime(2025, 5, 24, 21, 26, 4, 839, DateTimeKind.Utc).AddTicks(4669));

            migrationBuilder.UpdateData(
                table: "CategoriasProgreso",
                keyColumn: "Id",
                keyValue: 13,
                column: "CreatedAt",
                value: new DateTime(2025, 5, 24, 21, 26, 4, 839, DateTimeKind.Utc).AddTicks(4669));

            migrationBuilder.UpdateData(
                table: "CategoriasProgreso",
                keyColumn: "Id",
                keyValue: 14,
                column: "CreatedAt",
                value: new DateTime(2025, 5, 24, 21, 26, 4, 839, DateTimeKind.Utc).AddTicks(4669));

            migrationBuilder.UpdateData(
                table: "CategoriasProgreso",
                keyColumn: "Id",
                keyValue: 15,
                column: "CreatedAt",
                value: new DateTime(2025, 5, 24, 21, 26, 4, 839, DateTimeKind.Utc).AddTicks(4669));

            migrationBuilder.UpdateData(
                table: "LifeAreas",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 5, 24, 21, 26, 4, 837, DateTimeKind.Utc).AddTicks(7801), new DateTime(2025, 5, 24, 21, 26, 4, 837, DateTimeKind.Utc).AddTicks(7801) });

            migrationBuilder.UpdateData(
                table: "LifeAreas",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 5, 24, 21, 26, 4, 837, DateTimeKind.Utc).AddTicks(7801), new DateTime(2025, 5, 24, 21, 26, 4, 837, DateTimeKind.Utc).AddTicks(7801) });

            migrationBuilder.UpdateData(
                table: "LifeAreas",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 5, 24, 21, 26, 4, 837, DateTimeKind.Utc).AddTicks(7801), new DateTime(2025, 5, 24, 21, 26, 4, 837, DateTimeKind.Utc).AddTicks(7801) });

            migrationBuilder.UpdateData(
                table: "LifeAreas",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 5, 24, 21, 26, 4, 837, DateTimeKind.Utc).AddTicks(7801), new DateTime(2025, 5, 24, 21, 26, 4, 837, DateTimeKind.Utc).AddTicks(7801) });

            migrationBuilder.UpdateData(
                table: "LifeAreas",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 5, 24, 21, 26, 4, 837, DateTimeKind.Utc).AddTicks(7801), new DateTime(2025, 5, 24, 21, 26, 4, 837, DateTimeKind.Utc).AddTicks(7801) });

            migrationBuilder.UpdateData(
                table: "LifeAreas",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 5, 24, 21, 26, 4, 837, DateTimeKind.Utc).AddTicks(7801), new DateTime(2025, 5, 24, 21, 26, 4, 837, DateTimeKind.Utc).AddTicks(7801) });

            migrationBuilder.UpdateData(
                table: "LifeAreas",
                keyColumn: "Id",
                keyValue: 7,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 5, 24, 21, 26, 4, 837, DateTimeKind.Utc).AddTicks(7801), new DateTime(2025, 5, 24, 21, 26, 4, 837, DateTimeKind.Utc).AddTicks(7801) });

            migrationBuilder.UpdateData(
                table: "LifeAreas",
                keyColumn: "Id",
                keyValue: 8,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 5, 24, 21, 26, 4, 837, DateTimeKind.Utc).AddTicks(7801), new DateTime(2025, 5, 24, 21, 26, 4, 837, DateTimeKind.Utc).AddTicks(7801) });

            migrationBuilder.UpdateData(
                table: "LifeAreas",
                keyColumn: "Id",
                keyValue: 9,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 5, 24, 21, 26, 4, 837, DateTimeKind.Utc).AddTicks(7801), new DateTime(2025, 5, 24, 21, 26, 4, 837, DateTimeKind.Utc).AddTicks(7801) });

            migrationBuilder.UpdateData(
                table: "LifeAreas",
                keyColumn: "Id",
                keyValue: 10,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 5, 24, 21, 26, 4, 837, DateTimeKind.Utc).AddTicks(7801), new DateTime(2025, 5, 24, 21, 26, 4, 837, DateTimeKind.Utc).AddTicks(7801) });

            migrationBuilder.UpdateData(
                table: "Questions",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 5, 24, 21, 26, 4, 839, DateTimeKind.Utc).AddTicks(4452), new DateTime(2025, 5, 24, 21, 26, 4, 839, DateTimeKind.Utc).AddTicks(4452) });

            migrationBuilder.UpdateData(
                table: "Questions",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 5, 24, 21, 26, 4, 839, DateTimeKind.Utc).AddTicks(4452), new DateTime(2025, 5, 24, 21, 26, 4, 839, DateTimeKind.Utc).AddTicks(4452) });

            migrationBuilder.UpdateData(
                table: "Questions",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 5, 24, 21, 26, 4, 839, DateTimeKind.Utc).AddTicks(4452), new DateTime(2025, 5, 24, 21, 26, 4, 839, DateTimeKind.Utc).AddTicks(4452) });

            migrationBuilder.UpdateData(
                table: "Questions",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 5, 24, 21, 26, 4, 839, DateTimeKind.Utc).AddTicks(4452), new DateTime(2025, 5, 24, 21, 26, 4, 839, DateTimeKind.Utc).AddTicks(4452) });

            migrationBuilder.UpdateData(
                table: "Questions",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 5, 24, 21, 26, 4, 839, DateTimeKind.Utc).AddTicks(4452), new DateTime(2025, 5, 24, 21, 26, 4, 839, DateTimeKind.Utc).AddTicks(4452) });

            migrationBuilder.UpdateData(
                table: "Questions",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 5, 24, 21, 26, 4, 839, DateTimeKind.Utc).AddTicks(4452), new DateTime(2025, 5, 24, 21, 26, 4, 839, DateTimeKind.Utc).AddTicks(4452) });

            migrationBuilder.UpdateData(
                table: "Questions",
                keyColumn: "Id",
                keyValue: 7,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 5, 24, 21, 26, 4, 839, DateTimeKind.Utc).AddTicks(4452), new DateTime(2025, 5, 24, 21, 26, 4, 839, DateTimeKind.Utc).AddTicks(4452) });

            migrationBuilder.UpdateData(
                table: "Questions",
                keyColumn: "Id",
                keyValue: 8,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 5, 24, 21, 26, 4, 839, DateTimeKind.Utc).AddTicks(4452), new DateTime(2025, 5, 24, 21, 26, 4, 839, DateTimeKind.Utc).AddTicks(4452) });

            migrationBuilder.UpdateData(
                table: "Questions",
                keyColumn: "Id",
                keyValue: 9,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 5, 24, 21, 26, 4, 839, DateTimeKind.Utc).AddTicks(4452), new DateTime(2025, 5, 24, 21, 26, 4, 839, DateTimeKind.Utc).AddTicks(4452) });

            migrationBuilder.UpdateData(
                table: "Questions",
                keyColumn: "Id",
                keyValue: 10,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 5, 24, 21, 26, 4, 839, DateTimeKind.Utc).AddTicks(4452), new DateTime(2025, 5, 24, 21, 26, 4, 839, DateTimeKind.Utc).AddTicks(4452) });
        }
    }
}
