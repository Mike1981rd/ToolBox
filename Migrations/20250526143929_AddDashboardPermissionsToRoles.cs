using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ToolBox.Migrations
{
    /// <inheritdoc />
    public partial class AddDashboardPermissionsToRoles : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "PuedeVerCardClientesActivosDashboard",
                table: "Roles",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "PuedeVerCardTotalClientesDashboard",
                table: "Roles",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "PuedeVerMensajeBienvenidaDashboard",
                table: "Roles",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "PuedeVerVideoBienvenidaDashboard",
                table: "Roles",
                type: "boolean",
                nullable: false,
                defaultValue: false);

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PuedeVerCardClientesActivosDashboard",
                table: "Roles");

            migrationBuilder.DropColumn(
                name: "PuedeVerCardTotalClientesDashboard",
                table: "Roles");

            migrationBuilder.DropColumn(
                name: "PuedeVerMensajeBienvenidaDashboard",
                table: "Roles");

            migrationBuilder.DropColumn(
                name: "PuedeVerVideoBienvenidaDashboard",
                table: "Roles");

            migrationBuilder.UpdateData(
                table: "AreasProgreso",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2025, 5, 25, 21, 44, 21, 786, DateTimeKind.Utc).AddTicks(4256));

            migrationBuilder.UpdateData(
                table: "AreasProgreso",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2025, 5, 25, 21, 44, 21, 786, DateTimeKind.Utc).AddTicks(4256));

            migrationBuilder.UpdateData(
                table: "AreasProgreso",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedAt",
                value: new DateTime(2025, 5, 25, 21, 44, 21, 786, DateTimeKind.Utc).AddTicks(4256));

            migrationBuilder.UpdateData(
                table: "AreasProgreso",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedAt",
                value: new DateTime(2025, 5, 25, 21, 44, 21, 786, DateTimeKind.Utc).AddTicks(4256));

            migrationBuilder.UpdateData(
                table: "AreasProgreso",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreatedAt",
                value: new DateTime(2025, 5, 25, 21, 44, 21, 786, DateTimeKind.Utc).AddTicks(4256));

            migrationBuilder.UpdateData(
                table: "CategoriasHabitos",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "FechaCreacion" },
                values: new object[] { new DateTime(2025, 5, 25, 21, 44, 21, 787, DateTimeKind.Utc).AddTicks(5467), new DateTime(2025, 5, 25, 21, 44, 21, 787, DateTimeKind.Utc).AddTicks(5470) });

            migrationBuilder.UpdateData(
                table: "CategoriasHabitos",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedAt", "FechaCreacion" },
                values: new object[] { new DateTime(2025, 5, 25, 21, 44, 21, 787, DateTimeKind.Utc).AddTicks(5467), new DateTime(2025, 5, 25, 21, 44, 21, 787, DateTimeKind.Utc).AddTicks(5475) });

            migrationBuilder.UpdateData(
                table: "CategoriasHabitos",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedAt", "FechaCreacion" },
                values: new object[] { new DateTime(2025, 5, 25, 21, 44, 21, 787, DateTimeKind.Utc).AddTicks(5467), new DateTime(2025, 5, 25, 21, 44, 21, 787, DateTimeKind.Utc).AddTicks(5478) });

            migrationBuilder.UpdateData(
                table: "CategoriasHabitos",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "CreatedAt", "FechaCreacion" },
                values: new object[] { new DateTime(2025, 5, 25, 21, 44, 21, 787, DateTimeKind.Utc).AddTicks(5467), new DateTime(2025, 5, 25, 21, 44, 21, 787, DateTimeKind.Utc).AddTicks(5480) });

            migrationBuilder.UpdateData(
                table: "CategoriasHabitos",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "CreatedAt", "FechaCreacion" },
                values: new object[] { new DateTime(2025, 5, 25, 21, 44, 21, 787, DateTimeKind.Utc).AddTicks(5467), new DateTime(2025, 5, 25, 21, 44, 21, 787, DateTimeKind.Utc).AddTicks(5482) });

            migrationBuilder.UpdateData(
                table: "CategoriasHabitos",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "CreatedAt", "FechaCreacion" },
                values: new object[] { new DateTime(2025, 5, 25, 21, 44, 21, 787, DateTimeKind.Utc).AddTicks(5467), new DateTime(2025, 5, 25, 21, 44, 21, 787, DateTimeKind.Utc).AddTicks(5484) });

            migrationBuilder.UpdateData(
                table: "CategoriasHabitos",
                keyColumn: "Id",
                keyValue: 7,
                columns: new[] { "CreatedAt", "FechaCreacion" },
                values: new object[] { new DateTime(2025, 5, 25, 21, 44, 21, 787, DateTimeKind.Utc).AddTicks(5467), new DateTime(2025, 5, 25, 21, 44, 21, 787, DateTimeKind.Utc).AddTicks(5485) });

            migrationBuilder.UpdateData(
                table: "CategoriasHabitos",
                keyColumn: "Id",
                keyValue: 8,
                columns: new[] { "CreatedAt", "FechaCreacion" },
                values: new object[] { new DateTime(2025, 5, 25, 21, 44, 21, 787, DateTimeKind.Utc).AddTicks(5467), new DateTime(2025, 5, 25, 21, 44, 21, 787, DateTimeKind.Utc).AddTicks(5487) });

            migrationBuilder.UpdateData(
                table: "CategoriasProgreso",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2025, 5, 25, 21, 44, 21, 786, DateTimeKind.Utc).AddTicks(4256));

            migrationBuilder.UpdateData(
                table: "CategoriasProgreso",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2025, 5, 25, 21, 44, 21, 786, DateTimeKind.Utc).AddTicks(4256));

            migrationBuilder.UpdateData(
                table: "CategoriasProgreso",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedAt",
                value: new DateTime(2025, 5, 25, 21, 44, 21, 786, DateTimeKind.Utc).AddTicks(4256));

            migrationBuilder.UpdateData(
                table: "CategoriasProgreso",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedAt",
                value: new DateTime(2025, 5, 25, 21, 44, 21, 786, DateTimeKind.Utc).AddTicks(4256));

            migrationBuilder.UpdateData(
                table: "CategoriasProgreso",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreatedAt",
                value: new DateTime(2025, 5, 25, 21, 44, 21, 786, DateTimeKind.Utc).AddTicks(4256));

            migrationBuilder.UpdateData(
                table: "CategoriasProgreso",
                keyColumn: "Id",
                keyValue: 6,
                column: "CreatedAt",
                value: new DateTime(2025, 5, 25, 21, 44, 21, 786, DateTimeKind.Utc).AddTicks(4256));

            migrationBuilder.UpdateData(
                table: "CategoriasProgreso",
                keyColumn: "Id",
                keyValue: 7,
                column: "CreatedAt",
                value: new DateTime(2025, 5, 25, 21, 44, 21, 786, DateTimeKind.Utc).AddTicks(4256));

            migrationBuilder.UpdateData(
                table: "CategoriasProgreso",
                keyColumn: "Id",
                keyValue: 8,
                column: "CreatedAt",
                value: new DateTime(2025, 5, 25, 21, 44, 21, 786, DateTimeKind.Utc).AddTicks(4256));

            migrationBuilder.UpdateData(
                table: "CategoriasProgreso",
                keyColumn: "Id",
                keyValue: 9,
                column: "CreatedAt",
                value: new DateTime(2025, 5, 25, 21, 44, 21, 786, DateTimeKind.Utc).AddTicks(4256));

            migrationBuilder.UpdateData(
                table: "CategoriasProgreso",
                keyColumn: "Id",
                keyValue: 10,
                column: "CreatedAt",
                value: new DateTime(2025, 5, 25, 21, 44, 21, 786, DateTimeKind.Utc).AddTicks(4256));

            migrationBuilder.UpdateData(
                table: "CategoriasProgreso",
                keyColumn: "Id",
                keyValue: 11,
                column: "CreatedAt",
                value: new DateTime(2025, 5, 25, 21, 44, 21, 786, DateTimeKind.Utc).AddTicks(4256));

            migrationBuilder.UpdateData(
                table: "CategoriasProgreso",
                keyColumn: "Id",
                keyValue: 12,
                column: "CreatedAt",
                value: new DateTime(2025, 5, 25, 21, 44, 21, 786, DateTimeKind.Utc).AddTicks(4256));

            migrationBuilder.UpdateData(
                table: "CategoriasProgreso",
                keyColumn: "Id",
                keyValue: 13,
                column: "CreatedAt",
                value: new DateTime(2025, 5, 25, 21, 44, 21, 786, DateTimeKind.Utc).AddTicks(4256));

            migrationBuilder.UpdateData(
                table: "CategoriasProgreso",
                keyColumn: "Id",
                keyValue: 14,
                column: "CreatedAt",
                value: new DateTime(2025, 5, 25, 21, 44, 21, 786, DateTimeKind.Utc).AddTicks(4256));

            migrationBuilder.UpdateData(
                table: "CategoriasProgreso",
                keyColumn: "Id",
                keyValue: 15,
                column: "CreatedAt",
                value: new DateTime(2025, 5, 25, 21, 44, 21, 786, DateTimeKind.Utc).AddTicks(4256));

            migrationBuilder.UpdateData(
                table: "FrecuenciasHabitos",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "FechaCreacion" },
                values: new object[] { new DateTime(2025, 5, 25, 21, 44, 21, 787, DateTimeKind.Utc).AddTicks(5467), new DateTime(2025, 5, 25, 21, 44, 21, 787, DateTimeKind.Utc).AddTicks(5491) });

            migrationBuilder.UpdateData(
                table: "FrecuenciasHabitos",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedAt", "FechaCreacion" },
                values: new object[] { new DateTime(2025, 5, 25, 21, 44, 21, 787, DateTimeKind.Utc).AddTicks(5467), new DateTime(2025, 5, 25, 21, 44, 21, 787, DateTimeKind.Utc).AddTicks(5496) });

            migrationBuilder.UpdateData(
                table: "FrecuenciasHabitos",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedAt", "FechaCreacion" },
                values: new object[] { new DateTime(2025, 5, 25, 21, 44, 21, 787, DateTimeKind.Utc).AddTicks(5467), new DateTime(2025, 5, 25, 21, 44, 21, 787, DateTimeKind.Utc).AddTicks(5499) });

            migrationBuilder.UpdateData(
                table: "FrecuenciasHabitos",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "CreatedAt", "FechaCreacion" },
                values: new object[] { new DateTime(2025, 5, 25, 21, 44, 21, 787, DateTimeKind.Utc).AddTicks(5467), new DateTime(2025, 5, 25, 21, 44, 21, 787, DateTimeKind.Utc).AddTicks(5501) });

            migrationBuilder.UpdateData(
                table: "FrecuenciasHabitos",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "CreatedAt", "FechaCreacion" },
                values: new object[] { new DateTime(2025, 5, 25, 21, 44, 21, 787, DateTimeKind.Utc).AddTicks(5467), new DateTime(2025, 5, 25, 21, 44, 21, 787, DateTimeKind.Utc).AddTicks(5502) });

            migrationBuilder.UpdateData(
                table: "FrecuenciasHabitos",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "CreatedAt", "FechaCreacion" },
                values: new object[] { new DateTime(2025, 5, 25, 21, 44, 21, 787, DateTimeKind.Utc).AddTicks(5467), new DateTime(2025, 5, 25, 21, 44, 21, 787, DateTimeKind.Utc).AddTicks(5504) });

            migrationBuilder.UpdateData(
                table: "LifeAreas",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 5, 25, 21, 44, 21, 785, DateTimeKind.Utc).AddTicks(244), new DateTime(2025, 5, 25, 21, 44, 21, 785, DateTimeKind.Utc).AddTicks(244) });

            migrationBuilder.UpdateData(
                table: "LifeAreas",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 5, 25, 21, 44, 21, 785, DateTimeKind.Utc).AddTicks(244), new DateTime(2025, 5, 25, 21, 44, 21, 785, DateTimeKind.Utc).AddTicks(244) });

            migrationBuilder.UpdateData(
                table: "LifeAreas",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 5, 25, 21, 44, 21, 785, DateTimeKind.Utc).AddTicks(244), new DateTime(2025, 5, 25, 21, 44, 21, 785, DateTimeKind.Utc).AddTicks(244) });

            migrationBuilder.UpdateData(
                table: "LifeAreas",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 5, 25, 21, 44, 21, 785, DateTimeKind.Utc).AddTicks(244), new DateTime(2025, 5, 25, 21, 44, 21, 785, DateTimeKind.Utc).AddTicks(244) });

            migrationBuilder.UpdateData(
                table: "LifeAreas",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 5, 25, 21, 44, 21, 785, DateTimeKind.Utc).AddTicks(244), new DateTime(2025, 5, 25, 21, 44, 21, 785, DateTimeKind.Utc).AddTicks(244) });

            migrationBuilder.UpdateData(
                table: "LifeAreas",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 5, 25, 21, 44, 21, 785, DateTimeKind.Utc).AddTicks(244), new DateTime(2025, 5, 25, 21, 44, 21, 785, DateTimeKind.Utc).AddTicks(244) });

            migrationBuilder.UpdateData(
                table: "LifeAreas",
                keyColumn: "Id",
                keyValue: 7,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 5, 25, 21, 44, 21, 785, DateTimeKind.Utc).AddTicks(244), new DateTime(2025, 5, 25, 21, 44, 21, 785, DateTimeKind.Utc).AddTicks(244) });

            migrationBuilder.UpdateData(
                table: "LifeAreas",
                keyColumn: "Id",
                keyValue: 8,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 5, 25, 21, 44, 21, 785, DateTimeKind.Utc).AddTicks(244), new DateTime(2025, 5, 25, 21, 44, 21, 785, DateTimeKind.Utc).AddTicks(244) });

            migrationBuilder.UpdateData(
                table: "LifeAreas",
                keyColumn: "Id",
                keyValue: 9,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 5, 25, 21, 44, 21, 785, DateTimeKind.Utc).AddTicks(244), new DateTime(2025, 5, 25, 21, 44, 21, 785, DateTimeKind.Utc).AddTicks(244) });

            migrationBuilder.UpdateData(
                table: "LifeAreas",
                keyColumn: "Id",
                keyValue: 10,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 5, 25, 21, 44, 21, 785, DateTimeKind.Utc).AddTicks(244), new DateTime(2025, 5, 25, 21, 44, 21, 785, DateTimeKind.Utc).AddTicks(244) });

            migrationBuilder.UpdateData(
                table: "Questions",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 5, 25, 21, 44, 21, 786, DateTimeKind.Utc).AddTicks(4171), new DateTime(2025, 5, 25, 21, 44, 21, 786, DateTimeKind.Utc).AddTicks(4171) });

            migrationBuilder.UpdateData(
                table: "Questions",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 5, 25, 21, 44, 21, 786, DateTimeKind.Utc).AddTicks(4171), new DateTime(2025, 5, 25, 21, 44, 21, 786, DateTimeKind.Utc).AddTicks(4171) });

            migrationBuilder.UpdateData(
                table: "Questions",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 5, 25, 21, 44, 21, 786, DateTimeKind.Utc).AddTicks(4171), new DateTime(2025, 5, 25, 21, 44, 21, 786, DateTimeKind.Utc).AddTicks(4171) });

            migrationBuilder.UpdateData(
                table: "Questions",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 5, 25, 21, 44, 21, 786, DateTimeKind.Utc).AddTicks(4171), new DateTime(2025, 5, 25, 21, 44, 21, 786, DateTimeKind.Utc).AddTicks(4171) });

            migrationBuilder.UpdateData(
                table: "Questions",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 5, 25, 21, 44, 21, 786, DateTimeKind.Utc).AddTicks(4171), new DateTime(2025, 5, 25, 21, 44, 21, 786, DateTimeKind.Utc).AddTicks(4171) });

            migrationBuilder.UpdateData(
                table: "Questions",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 5, 25, 21, 44, 21, 786, DateTimeKind.Utc).AddTicks(4171), new DateTime(2025, 5, 25, 21, 44, 21, 786, DateTimeKind.Utc).AddTicks(4171) });

            migrationBuilder.UpdateData(
                table: "Questions",
                keyColumn: "Id",
                keyValue: 7,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 5, 25, 21, 44, 21, 786, DateTimeKind.Utc).AddTicks(4171), new DateTime(2025, 5, 25, 21, 44, 21, 786, DateTimeKind.Utc).AddTicks(4171) });

            migrationBuilder.UpdateData(
                table: "Questions",
                keyColumn: "Id",
                keyValue: 8,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 5, 25, 21, 44, 21, 786, DateTimeKind.Utc).AddTicks(4171), new DateTime(2025, 5, 25, 21, 44, 21, 786, DateTimeKind.Utc).AddTicks(4171) });

            migrationBuilder.UpdateData(
                table: "Questions",
                keyColumn: "Id",
                keyValue: 9,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 5, 25, 21, 44, 21, 786, DateTimeKind.Utc).AddTicks(4171), new DateTime(2025, 5, 25, 21, 44, 21, 786, DateTimeKind.Utc).AddTicks(4171) });

            migrationBuilder.UpdateData(
                table: "Questions",
                keyColumn: "Id",
                keyValue: 10,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 5, 25, 21, 44, 21, 786, DateTimeKind.Utc).AddTicks(4171), new DateTime(2025, 5, 25, 21, 44, 21, 786, DateTimeKind.Utc).AddTicks(4171) });
        }
    }
}
