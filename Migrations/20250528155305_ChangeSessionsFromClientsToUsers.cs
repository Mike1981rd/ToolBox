using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ToolBox.Migrations
{
    /// <inheritdoc />
    public partial class ChangeSessionsFromClientsToUsers : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Sessions_Customers_ClientId",
                table: "Sessions");

            migrationBuilder.RenameColumn(
                name: "ClientId",
                table: "Sessions",
                newName: "UserId");

            migrationBuilder.RenameIndex(
                name: "IX_Sessions_ClientId_SessionDateTime",
                table: "Sessions",
                newName: "IX_Sessions_UserId_SessionDateTime");

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

            // Commented out to avoid duplicate key conflicts
            // migrationBuilder.InsertData(
            //     table: "Permissions",
            //     columns: new[] { "Id", "ActionName", "Category", "Description", "ModuleName" },
            //     values: new object[,]
            //     {
            //         { 1, "Read", "General", "Ver y listar en Tablero", "Dashboard" },
            //         // ... all other permissions commented out
            //     });

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

            migrationBuilder.AddForeignKey(
                name: "FK_Sessions_Users_UserId",
                table: "Sessions",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Sessions_Users_UserId",
                table: "Sessions");

            // Commented out to avoid affecting existing permissions
            // migrationBuilder.DeleteData(
            //     table: "Permissions",
            //     keyColumn: "Id",
            //     keyValue: 1);
            // ... all other deletes commented out

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "Sessions",
                newName: "ClientId");

            migrationBuilder.RenameIndex(
                name: "IX_Sessions_UserId_SessionDateTime",
                table: "Sessions",
                newName: "IX_Sessions_ClientId_SessionDateTime");

            migrationBuilder.UpdateData(
                table: "AreasProgreso",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2025, 5, 27, 20, 50, 57, 836, DateTimeKind.Utc).AddTicks(6912));

            migrationBuilder.UpdateData(
                table: "AreasProgreso",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2025, 5, 27, 20, 50, 57, 836, DateTimeKind.Utc).AddTicks(6912));

            migrationBuilder.UpdateData(
                table: "AreasProgreso",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedAt",
                value: new DateTime(2025, 5, 27, 20, 50, 57, 836, DateTimeKind.Utc).AddTicks(6912));

            migrationBuilder.UpdateData(
                table: "AreasProgreso",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedAt",
                value: new DateTime(2025, 5, 27, 20, 50, 57, 836, DateTimeKind.Utc).AddTicks(6912));

            migrationBuilder.UpdateData(
                table: "AreasProgreso",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreatedAt",
                value: new DateTime(2025, 5, 27, 20, 50, 57, 836, DateTimeKind.Utc).AddTicks(6912));

            migrationBuilder.UpdateData(
                table: "CategoriasHabitos",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "FechaCreacion" },
                values: new object[] { new DateTime(2025, 5, 27, 20, 50, 57, 837, DateTimeKind.Utc).AddTicks(8355), new DateTime(2025, 5, 27, 20, 50, 57, 837, DateTimeKind.Utc).AddTicks(8359) });

            migrationBuilder.UpdateData(
                table: "CategoriasHabitos",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedAt", "FechaCreacion" },
                values: new object[] { new DateTime(2025, 5, 27, 20, 50, 57, 837, DateTimeKind.Utc).AddTicks(8355), new DateTime(2025, 5, 27, 20, 50, 57, 837, DateTimeKind.Utc).AddTicks(8365) });

            migrationBuilder.UpdateData(
                table: "CategoriasHabitos",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedAt", "FechaCreacion" },
                values: new object[] { new DateTime(2025, 5, 27, 20, 50, 57, 837, DateTimeKind.Utc).AddTicks(8355), new DateTime(2025, 5, 27, 20, 50, 57, 837, DateTimeKind.Utc).AddTicks(8368) });

            migrationBuilder.UpdateData(
                table: "CategoriasHabitos",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "CreatedAt", "FechaCreacion" },
                values: new object[] { new DateTime(2025, 5, 27, 20, 50, 57, 837, DateTimeKind.Utc).AddTicks(8355), new DateTime(2025, 5, 27, 20, 50, 57, 837, DateTimeKind.Utc).AddTicks(8370) });

            migrationBuilder.UpdateData(
                table: "CategoriasHabitos",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "CreatedAt", "FechaCreacion" },
                values: new object[] { new DateTime(2025, 5, 27, 20, 50, 57, 837, DateTimeKind.Utc).AddTicks(8355), new DateTime(2025, 5, 27, 20, 50, 57, 837, DateTimeKind.Utc).AddTicks(8371) });

            migrationBuilder.UpdateData(
                table: "CategoriasHabitos",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "CreatedAt", "FechaCreacion" },
                values: new object[] { new DateTime(2025, 5, 27, 20, 50, 57, 837, DateTimeKind.Utc).AddTicks(8355), new DateTime(2025, 5, 27, 20, 50, 57, 837, DateTimeKind.Utc).AddTicks(8373) });

            migrationBuilder.UpdateData(
                table: "CategoriasHabitos",
                keyColumn: "Id",
                keyValue: 7,
                columns: new[] { "CreatedAt", "FechaCreacion" },
                values: new object[] { new DateTime(2025, 5, 27, 20, 50, 57, 837, DateTimeKind.Utc).AddTicks(8355), new DateTime(2025, 5, 27, 20, 50, 57, 837, DateTimeKind.Utc).AddTicks(8375) });

            migrationBuilder.UpdateData(
                table: "CategoriasHabitos",
                keyColumn: "Id",
                keyValue: 8,
                columns: new[] { "CreatedAt", "FechaCreacion" },
                values: new object[] { new DateTime(2025, 5, 27, 20, 50, 57, 837, DateTimeKind.Utc).AddTicks(8355), new DateTime(2025, 5, 27, 20, 50, 57, 837, DateTimeKind.Utc).AddTicks(8377) });

            migrationBuilder.UpdateData(
                table: "CategoriasProgreso",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2025, 5, 27, 20, 50, 57, 836, DateTimeKind.Utc).AddTicks(6912));

            migrationBuilder.UpdateData(
                table: "CategoriasProgreso",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2025, 5, 27, 20, 50, 57, 836, DateTimeKind.Utc).AddTicks(6912));

            migrationBuilder.UpdateData(
                table: "CategoriasProgreso",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedAt",
                value: new DateTime(2025, 5, 27, 20, 50, 57, 836, DateTimeKind.Utc).AddTicks(6912));

            migrationBuilder.UpdateData(
                table: "CategoriasProgreso",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedAt",
                value: new DateTime(2025, 5, 27, 20, 50, 57, 836, DateTimeKind.Utc).AddTicks(6912));

            migrationBuilder.UpdateData(
                table: "CategoriasProgreso",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreatedAt",
                value: new DateTime(2025, 5, 27, 20, 50, 57, 836, DateTimeKind.Utc).AddTicks(6912));

            migrationBuilder.UpdateData(
                table: "CategoriasProgreso",
                keyColumn: "Id",
                keyValue: 6,
                column: "CreatedAt",
                value: new DateTime(2025, 5, 27, 20, 50, 57, 836, DateTimeKind.Utc).AddTicks(6912));

            migrationBuilder.UpdateData(
                table: "CategoriasProgreso",
                keyColumn: "Id",
                keyValue: 7,
                column: "CreatedAt",
                value: new DateTime(2025, 5, 27, 20, 50, 57, 836, DateTimeKind.Utc).AddTicks(6912));

            migrationBuilder.UpdateData(
                table: "CategoriasProgreso",
                keyColumn: "Id",
                keyValue: 8,
                column: "CreatedAt",
                value: new DateTime(2025, 5, 27, 20, 50, 57, 836, DateTimeKind.Utc).AddTicks(6912));

            migrationBuilder.UpdateData(
                table: "CategoriasProgreso",
                keyColumn: "Id",
                keyValue: 9,
                column: "CreatedAt",
                value: new DateTime(2025, 5, 27, 20, 50, 57, 836, DateTimeKind.Utc).AddTicks(6912));

            migrationBuilder.UpdateData(
                table: "CategoriasProgreso",
                keyColumn: "Id",
                keyValue: 10,
                column: "CreatedAt",
                value: new DateTime(2025, 5, 27, 20, 50, 57, 836, DateTimeKind.Utc).AddTicks(6912));

            migrationBuilder.UpdateData(
                table: "CategoriasProgreso",
                keyColumn: "Id",
                keyValue: 11,
                column: "CreatedAt",
                value: new DateTime(2025, 5, 27, 20, 50, 57, 836, DateTimeKind.Utc).AddTicks(6912));

            migrationBuilder.UpdateData(
                table: "CategoriasProgreso",
                keyColumn: "Id",
                keyValue: 12,
                column: "CreatedAt",
                value: new DateTime(2025, 5, 27, 20, 50, 57, 836, DateTimeKind.Utc).AddTicks(6912));

            migrationBuilder.UpdateData(
                table: "CategoriasProgreso",
                keyColumn: "Id",
                keyValue: 13,
                column: "CreatedAt",
                value: new DateTime(2025, 5, 27, 20, 50, 57, 836, DateTimeKind.Utc).AddTicks(6912));

            migrationBuilder.UpdateData(
                table: "CategoriasProgreso",
                keyColumn: "Id",
                keyValue: 14,
                column: "CreatedAt",
                value: new DateTime(2025, 5, 27, 20, 50, 57, 836, DateTimeKind.Utc).AddTicks(6912));

            migrationBuilder.UpdateData(
                table: "CategoriasProgreso",
                keyColumn: "Id",
                keyValue: 15,
                column: "CreatedAt",
                value: new DateTime(2025, 5, 27, 20, 50, 57, 836, DateTimeKind.Utc).AddTicks(6912));

            migrationBuilder.UpdateData(
                table: "FrecuenciasHabitos",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "FechaCreacion" },
                values: new object[] { new DateTime(2025, 5, 27, 20, 50, 57, 837, DateTimeKind.Utc).AddTicks(8355), new DateTime(2025, 5, 27, 20, 50, 57, 837, DateTimeKind.Utc).AddTicks(8380) });

            migrationBuilder.UpdateData(
                table: "FrecuenciasHabitos",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedAt", "FechaCreacion" },
                values: new object[] { new DateTime(2025, 5, 27, 20, 50, 57, 837, DateTimeKind.Utc).AddTicks(8355), new DateTime(2025, 5, 27, 20, 50, 57, 837, DateTimeKind.Utc).AddTicks(8385) });

            migrationBuilder.UpdateData(
                table: "FrecuenciasHabitos",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedAt", "FechaCreacion" },
                values: new object[] { new DateTime(2025, 5, 27, 20, 50, 57, 837, DateTimeKind.Utc).AddTicks(8355), new DateTime(2025, 5, 27, 20, 50, 57, 837, DateTimeKind.Utc).AddTicks(8388) });

            migrationBuilder.UpdateData(
                table: "FrecuenciasHabitos",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "CreatedAt", "FechaCreacion" },
                values: new object[] { new DateTime(2025, 5, 27, 20, 50, 57, 837, DateTimeKind.Utc).AddTicks(8355), new DateTime(2025, 5, 27, 20, 50, 57, 837, DateTimeKind.Utc).AddTicks(8390) });

            migrationBuilder.UpdateData(
                table: "FrecuenciasHabitos",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "CreatedAt", "FechaCreacion" },
                values: new object[] { new DateTime(2025, 5, 27, 20, 50, 57, 837, DateTimeKind.Utc).AddTicks(8355), new DateTime(2025, 5, 27, 20, 50, 57, 837, DateTimeKind.Utc).AddTicks(8392) });

            migrationBuilder.UpdateData(
                table: "FrecuenciasHabitos",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "CreatedAt", "FechaCreacion" },
                values: new object[] { new DateTime(2025, 5, 27, 20, 50, 57, 837, DateTimeKind.Utc).AddTicks(8355), new DateTime(2025, 5, 27, 20, 50, 57, 837, DateTimeKind.Utc).AddTicks(8394) });

            migrationBuilder.UpdateData(
                table: "LifeAreas",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 5, 27, 20, 50, 57, 835, DateTimeKind.Utc).AddTicks(738), new DateTime(2025, 5, 27, 20, 50, 57, 835, DateTimeKind.Utc).AddTicks(738) });

            migrationBuilder.UpdateData(
                table: "LifeAreas",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 5, 27, 20, 50, 57, 835, DateTimeKind.Utc).AddTicks(738), new DateTime(2025, 5, 27, 20, 50, 57, 835, DateTimeKind.Utc).AddTicks(738) });

            migrationBuilder.UpdateData(
                table: "LifeAreas",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 5, 27, 20, 50, 57, 835, DateTimeKind.Utc).AddTicks(738), new DateTime(2025, 5, 27, 20, 50, 57, 835, DateTimeKind.Utc).AddTicks(738) });

            migrationBuilder.UpdateData(
                table: "LifeAreas",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 5, 27, 20, 50, 57, 835, DateTimeKind.Utc).AddTicks(738), new DateTime(2025, 5, 27, 20, 50, 57, 835, DateTimeKind.Utc).AddTicks(738) });

            migrationBuilder.UpdateData(
                table: "LifeAreas",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 5, 27, 20, 50, 57, 835, DateTimeKind.Utc).AddTicks(738), new DateTime(2025, 5, 27, 20, 50, 57, 835, DateTimeKind.Utc).AddTicks(738) });

            migrationBuilder.UpdateData(
                table: "LifeAreas",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 5, 27, 20, 50, 57, 835, DateTimeKind.Utc).AddTicks(738), new DateTime(2025, 5, 27, 20, 50, 57, 835, DateTimeKind.Utc).AddTicks(738) });

            migrationBuilder.UpdateData(
                table: "LifeAreas",
                keyColumn: "Id",
                keyValue: 7,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 5, 27, 20, 50, 57, 835, DateTimeKind.Utc).AddTicks(738), new DateTime(2025, 5, 27, 20, 50, 57, 835, DateTimeKind.Utc).AddTicks(738) });

            migrationBuilder.UpdateData(
                table: "LifeAreas",
                keyColumn: "Id",
                keyValue: 8,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 5, 27, 20, 50, 57, 835, DateTimeKind.Utc).AddTicks(738), new DateTime(2025, 5, 27, 20, 50, 57, 835, DateTimeKind.Utc).AddTicks(738) });

            migrationBuilder.UpdateData(
                table: "LifeAreas",
                keyColumn: "Id",
                keyValue: 9,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 5, 27, 20, 50, 57, 835, DateTimeKind.Utc).AddTicks(738), new DateTime(2025, 5, 27, 20, 50, 57, 835, DateTimeKind.Utc).AddTicks(738) });

            migrationBuilder.UpdateData(
                table: "LifeAreas",
                keyColumn: "Id",
                keyValue: 10,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 5, 27, 20, 50, 57, 835, DateTimeKind.Utc).AddTicks(738), new DateTime(2025, 5, 27, 20, 50, 57, 835, DateTimeKind.Utc).AddTicks(738) });

            migrationBuilder.UpdateData(
                table: "Questions",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 5, 27, 20, 50, 57, 836, DateTimeKind.Utc).AddTicks(6826), new DateTime(2025, 5, 27, 20, 50, 57, 836, DateTimeKind.Utc).AddTicks(6826) });

            migrationBuilder.UpdateData(
                table: "Questions",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 5, 27, 20, 50, 57, 836, DateTimeKind.Utc).AddTicks(6826), new DateTime(2025, 5, 27, 20, 50, 57, 836, DateTimeKind.Utc).AddTicks(6826) });

            migrationBuilder.UpdateData(
                table: "Questions",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 5, 27, 20, 50, 57, 836, DateTimeKind.Utc).AddTicks(6826), new DateTime(2025, 5, 27, 20, 50, 57, 836, DateTimeKind.Utc).AddTicks(6826) });

            migrationBuilder.UpdateData(
                table: "Questions",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 5, 27, 20, 50, 57, 836, DateTimeKind.Utc).AddTicks(6826), new DateTime(2025, 5, 27, 20, 50, 57, 836, DateTimeKind.Utc).AddTicks(6826) });

            migrationBuilder.UpdateData(
                table: "Questions",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 5, 27, 20, 50, 57, 836, DateTimeKind.Utc).AddTicks(6826), new DateTime(2025, 5, 27, 20, 50, 57, 836, DateTimeKind.Utc).AddTicks(6826) });

            migrationBuilder.UpdateData(
                table: "Questions",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 5, 27, 20, 50, 57, 836, DateTimeKind.Utc).AddTicks(6826), new DateTime(2025, 5, 27, 20, 50, 57, 836, DateTimeKind.Utc).AddTicks(6826) });

            migrationBuilder.UpdateData(
                table: "Questions",
                keyColumn: "Id",
                keyValue: 7,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 5, 27, 20, 50, 57, 836, DateTimeKind.Utc).AddTicks(6826), new DateTime(2025, 5, 27, 20, 50, 57, 836, DateTimeKind.Utc).AddTicks(6826) });

            migrationBuilder.UpdateData(
                table: "Questions",
                keyColumn: "Id",
                keyValue: 8,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 5, 27, 20, 50, 57, 836, DateTimeKind.Utc).AddTicks(6826), new DateTime(2025, 5, 27, 20, 50, 57, 836, DateTimeKind.Utc).AddTicks(6826) });

            migrationBuilder.UpdateData(
                table: "Questions",
                keyColumn: "Id",
                keyValue: 9,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 5, 27, 20, 50, 57, 836, DateTimeKind.Utc).AddTicks(6826), new DateTime(2025, 5, 27, 20, 50, 57, 836, DateTimeKind.Utc).AddTicks(6826) });

            migrationBuilder.UpdateData(
                table: "Questions",
                keyColumn: "Id",
                keyValue: 10,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 5, 27, 20, 50, 57, 836, DateTimeKind.Utc).AddTicks(6826), new DateTime(2025, 5, 27, 20, 50, 57, 836, DateTimeKind.Utc).AddTicks(6826) });

            migrationBuilder.AddForeignKey(
                name: "FK_Sessions_Customers_ClientId",
                table: "Sessions",
                column: "ClientId",
                principalTable: "Customers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
