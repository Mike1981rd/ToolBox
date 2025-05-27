using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace ToolBox.Migrations
{
    /// <inheritdoc />
    public partial class CreateCalendarTablesv2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "SesionesCalendario",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    CoachId = table.Column<int>(type: "integer", nullable: false),
                    Titulo = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    Descripcion = table.Column<string>(type: "character varying(1000)", maxLength: 1000, nullable: true),
                    FechaHoraInicio = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    FechaHoraFin = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UbicacionOEnlace = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    EstadoSesion = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SesionesCalendario", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SesionesCalendario_Users_CoachId",
                        column: x => x.CoachId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "SesionesClientes",
                columns: table => new
                {
                    SesionCalendarioId = table.Column<int>(type: "integer", nullable: false),
                    ClienteId = table.Column<int>(type: "integer", nullable: false),
                    FechaConfirmacion = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    Asistio = table.Column<bool>(type: "boolean", nullable: false),
                    Notas = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SesionesClientes", x => new { x.SesionCalendarioId, x.ClienteId });
                    table.ForeignKey(
                        name: "FK_SesionesClientes_Customers_ClienteId",
                        column: x => x.ClienteId,
                        principalTable: "Customers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SesionesClientes_SesionesCalendario_SesionCalendarioId",
                        column: x => x.SesionCalendarioId,
                        principalTable: "SesionesCalendario",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.UpdateData(
                table: "AreasProgreso",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2025, 5, 27, 2, 52, 2, 228, DateTimeKind.Utc).AddTicks(8912));

            migrationBuilder.UpdateData(
                table: "AreasProgreso",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2025, 5, 27, 2, 52, 2, 228, DateTimeKind.Utc).AddTicks(8912));

            migrationBuilder.UpdateData(
                table: "AreasProgreso",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedAt",
                value: new DateTime(2025, 5, 27, 2, 52, 2, 228, DateTimeKind.Utc).AddTicks(8912));

            migrationBuilder.UpdateData(
                table: "AreasProgreso",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedAt",
                value: new DateTime(2025, 5, 27, 2, 52, 2, 228, DateTimeKind.Utc).AddTicks(8912));

            migrationBuilder.UpdateData(
                table: "AreasProgreso",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreatedAt",
                value: new DateTime(2025, 5, 27, 2, 52, 2, 228, DateTimeKind.Utc).AddTicks(8912));

            migrationBuilder.UpdateData(
                table: "CategoriasHabitos",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "FechaCreacion" },
                values: new object[] { new DateTime(2025, 5, 27, 2, 52, 2, 229, DateTimeKind.Utc).AddTicks(9376), new DateTime(2025, 5, 27, 2, 52, 2, 229, DateTimeKind.Utc).AddTicks(9379) });

            migrationBuilder.UpdateData(
                table: "CategoriasHabitos",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedAt", "FechaCreacion" },
                values: new object[] { new DateTime(2025, 5, 27, 2, 52, 2, 229, DateTimeKind.Utc).AddTicks(9376), new DateTime(2025, 5, 27, 2, 52, 2, 229, DateTimeKind.Utc).AddTicks(9385) });

            migrationBuilder.UpdateData(
                table: "CategoriasHabitos",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedAt", "FechaCreacion" },
                values: new object[] { new DateTime(2025, 5, 27, 2, 52, 2, 229, DateTimeKind.Utc).AddTicks(9376), new DateTime(2025, 5, 27, 2, 52, 2, 229, DateTimeKind.Utc).AddTicks(9387) });

            migrationBuilder.UpdateData(
                table: "CategoriasHabitos",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "CreatedAt", "FechaCreacion" },
                values: new object[] { new DateTime(2025, 5, 27, 2, 52, 2, 229, DateTimeKind.Utc).AddTicks(9376), new DateTime(2025, 5, 27, 2, 52, 2, 229, DateTimeKind.Utc).AddTicks(9390) });

            migrationBuilder.UpdateData(
                table: "CategoriasHabitos",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "CreatedAt", "FechaCreacion" },
                values: new object[] { new DateTime(2025, 5, 27, 2, 52, 2, 229, DateTimeKind.Utc).AddTicks(9376), new DateTime(2025, 5, 27, 2, 52, 2, 229, DateTimeKind.Utc).AddTicks(9392) });

            migrationBuilder.UpdateData(
                table: "CategoriasHabitos",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "CreatedAt", "FechaCreacion" },
                values: new object[] { new DateTime(2025, 5, 27, 2, 52, 2, 229, DateTimeKind.Utc).AddTicks(9376), new DateTime(2025, 5, 27, 2, 52, 2, 229, DateTimeKind.Utc).AddTicks(9393) });

            migrationBuilder.UpdateData(
                table: "CategoriasHabitos",
                keyColumn: "Id",
                keyValue: 7,
                columns: new[] { "CreatedAt", "FechaCreacion" },
                values: new object[] { new DateTime(2025, 5, 27, 2, 52, 2, 229, DateTimeKind.Utc).AddTicks(9376), new DateTime(2025, 5, 27, 2, 52, 2, 229, DateTimeKind.Utc).AddTicks(9395) });

            migrationBuilder.UpdateData(
                table: "CategoriasHabitos",
                keyColumn: "Id",
                keyValue: 8,
                columns: new[] { "CreatedAt", "FechaCreacion" },
                values: new object[] { new DateTime(2025, 5, 27, 2, 52, 2, 229, DateTimeKind.Utc).AddTicks(9376), new DateTime(2025, 5, 27, 2, 52, 2, 229, DateTimeKind.Utc).AddTicks(9397) });

            migrationBuilder.UpdateData(
                table: "CategoriasProgreso",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2025, 5, 27, 2, 52, 2, 228, DateTimeKind.Utc).AddTicks(8912));

            migrationBuilder.UpdateData(
                table: "CategoriasProgreso",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2025, 5, 27, 2, 52, 2, 228, DateTimeKind.Utc).AddTicks(8912));

            migrationBuilder.UpdateData(
                table: "CategoriasProgreso",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedAt",
                value: new DateTime(2025, 5, 27, 2, 52, 2, 228, DateTimeKind.Utc).AddTicks(8912));

            migrationBuilder.UpdateData(
                table: "CategoriasProgreso",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedAt",
                value: new DateTime(2025, 5, 27, 2, 52, 2, 228, DateTimeKind.Utc).AddTicks(8912));

            migrationBuilder.UpdateData(
                table: "CategoriasProgreso",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreatedAt",
                value: new DateTime(2025, 5, 27, 2, 52, 2, 228, DateTimeKind.Utc).AddTicks(8912));

            migrationBuilder.UpdateData(
                table: "CategoriasProgreso",
                keyColumn: "Id",
                keyValue: 6,
                column: "CreatedAt",
                value: new DateTime(2025, 5, 27, 2, 52, 2, 228, DateTimeKind.Utc).AddTicks(8912));

            migrationBuilder.UpdateData(
                table: "CategoriasProgreso",
                keyColumn: "Id",
                keyValue: 7,
                column: "CreatedAt",
                value: new DateTime(2025, 5, 27, 2, 52, 2, 228, DateTimeKind.Utc).AddTicks(8912));

            migrationBuilder.UpdateData(
                table: "CategoriasProgreso",
                keyColumn: "Id",
                keyValue: 8,
                column: "CreatedAt",
                value: new DateTime(2025, 5, 27, 2, 52, 2, 228, DateTimeKind.Utc).AddTicks(8912));

            migrationBuilder.UpdateData(
                table: "CategoriasProgreso",
                keyColumn: "Id",
                keyValue: 9,
                column: "CreatedAt",
                value: new DateTime(2025, 5, 27, 2, 52, 2, 228, DateTimeKind.Utc).AddTicks(8912));

            migrationBuilder.UpdateData(
                table: "CategoriasProgreso",
                keyColumn: "Id",
                keyValue: 10,
                column: "CreatedAt",
                value: new DateTime(2025, 5, 27, 2, 52, 2, 228, DateTimeKind.Utc).AddTicks(8912));

            migrationBuilder.UpdateData(
                table: "CategoriasProgreso",
                keyColumn: "Id",
                keyValue: 11,
                column: "CreatedAt",
                value: new DateTime(2025, 5, 27, 2, 52, 2, 228, DateTimeKind.Utc).AddTicks(8912));

            migrationBuilder.UpdateData(
                table: "CategoriasProgreso",
                keyColumn: "Id",
                keyValue: 12,
                column: "CreatedAt",
                value: new DateTime(2025, 5, 27, 2, 52, 2, 228, DateTimeKind.Utc).AddTicks(8912));

            migrationBuilder.UpdateData(
                table: "CategoriasProgreso",
                keyColumn: "Id",
                keyValue: 13,
                column: "CreatedAt",
                value: new DateTime(2025, 5, 27, 2, 52, 2, 228, DateTimeKind.Utc).AddTicks(8912));

            migrationBuilder.UpdateData(
                table: "CategoriasProgreso",
                keyColumn: "Id",
                keyValue: 14,
                column: "CreatedAt",
                value: new DateTime(2025, 5, 27, 2, 52, 2, 228, DateTimeKind.Utc).AddTicks(8912));

            migrationBuilder.UpdateData(
                table: "CategoriasProgreso",
                keyColumn: "Id",
                keyValue: 15,
                column: "CreatedAt",
                value: new DateTime(2025, 5, 27, 2, 52, 2, 228, DateTimeKind.Utc).AddTicks(8912));

            migrationBuilder.UpdateData(
                table: "FrecuenciasHabitos",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "FechaCreacion" },
                values: new object[] { new DateTime(2025, 5, 27, 2, 52, 2, 229, DateTimeKind.Utc).AddTicks(9376), new DateTime(2025, 5, 27, 2, 52, 2, 229, DateTimeKind.Utc).AddTicks(9401) });

            migrationBuilder.UpdateData(
                table: "FrecuenciasHabitos",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedAt", "FechaCreacion" },
                values: new object[] { new DateTime(2025, 5, 27, 2, 52, 2, 229, DateTimeKind.Utc).AddTicks(9376), new DateTime(2025, 5, 27, 2, 52, 2, 229, DateTimeKind.Utc).AddTicks(9406) });

            migrationBuilder.UpdateData(
                table: "FrecuenciasHabitos",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedAt", "FechaCreacion" },
                values: new object[] { new DateTime(2025, 5, 27, 2, 52, 2, 229, DateTimeKind.Utc).AddTicks(9376), new DateTime(2025, 5, 27, 2, 52, 2, 229, DateTimeKind.Utc).AddTicks(9408) });

            migrationBuilder.UpdateData(
                table: "FrecuenciasHabitos",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "CreatedAt", "FechaCreacion" },
                values: new object[] { new DateTime(2025, 5, 27, 2, 52, 2, 229, DateTimeKind.Utc).AddTicks(9376), new DateTime(2025, 5, 27, 2, 52, 2, 229, DateTimeKind.Utc).AddTicks(9410) });

            migrationBuilder.UpdateData(
                table: "FrecuenciasHabitos",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "CreatedAt", "FechaCreacion" },
                values: new object[] { new DateTime(2025, 5, 27, 2, 52, 2, 229, DateTimeKind.Utc).AddTicks(9376), new DateTime(2025, 5, 27, 2, 52, 2, 229, DateTimeKind.Utc).AddTicks(9412) });

            migrationBuilder.UpdateData(
                table: "FrecuenciasHabitos",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "CreatedAt", "FechaCreacion" },
                values: new object[] { new DateTime(2025, 5, 27, 2, 52, 2, 229, DateTimeKind.Utc).AddTicks(9376), new DateTime(2025, 5, 27, 2, 52, 2, 229, DateTimeKind.Utc).AddTicks(9413) });

            migrationBuilder.UpdateData(
                table: "LifeAreas",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 5, 27, 2, 52, 2, 227, DateTimeKind.Utc).AddTicks(4699), new DateTime(2025, 5, 27, 2, 52, 2, 227, DateTimeKind.Utc).AddTicks(4699) });

            migrationBuilder.UpdateData(
                table: "LifeAreas",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 5, 27, 2, 52, 2, 227, DateTimeKind.Utc).AddTicks(4699), new DateTime(2025, 5, 27, 2, 52, 2, 227, DateTimeKind.Utc).AddTicks(4699) });

            migrationBuilder.UpdateData(
                table: "LifeAreas",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 5, 27, 2, 52, 2, 227, DateTimeKind.Utc).AddTicks(4699), new DateTime(2025, 5, 27, 2, 52, 2, 227, DateTimeKind.Utc).AddTicks(4699) });

            migrationBuilder.UpdateData(
                table: "LifeAreas",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 5, 27, 2, 52, 2, 227, DateTimeKind.Utc).AddTicks(4699), new DateTime(2025, 5, 27, 2, 52, 2, 227, DateTimeKind.Utc).AddTicks(4699) });

            migrationBuilder.UpdateData(
                table: "LifeAreas",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 5, 27, 2, 52, 2, 227, DateTimeKind.Utc).AddTicks(4699), new DateTime(2025, 5, 27, 2, 52, 2, 227, DateTimeKind.Utc).AddTicks(4699) });

            migrationBuilder.UpdateData(
                table: "LifeAreas",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 5, 27, 2, 52, 2, 227, DateTimeKind.Utc).AddTicks(4699), new DateTime(2025, 5, 27, 2, 52, 2, 227, DateTimeKind.Utc).AddTicks(4699) });

            migrationBuilder.UpdateData(
                table: "LifeAreas",
                keyColumn: "Id",
                keyValue: 7,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 5, 27, 2, 52, 2, 227, DateTimeKind.Utc).AddTicks(4699), new DateTime(2025, 5, 27, 2, 52, 2, 227, DateTimeKind.Utc).AddTicks(4699) });

            migrationBuilder.UpdateData(
                table: "LifeAreas",
                keyColumn: "Id",
                keyValue: 8,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 5, 27, 2, 52, 2, 227, DateTimeKind.Utc).AddTicks(4699), new DateTime(2025, 5, 27, 2, 52, 2, 227, DateTimeKind.Utc).AddTicks(4699) });

            migrationBuilder.UpdateData(
                table: "LifeAreas",
                keyColumn: "Id",
                keyValue: 9,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 5, 27, 2, 52, 2, 227, DateTimeKind.Utc).AddTicks(4699), new DateTime(2025, 5, 27, 2, 52, 2, 227, DateTimeKind.Utc).AddTicks(4699) });

            migrationBuilder.UpdateData(
                table: "LifeAreas",
                keyColumn: "Id",
                keyValue: 10,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 5, 27, 2, 52, 2, 227, DateTimeKind.Utc).AddTicks(4699), new DateTime(2025, 5, 27, 2, 52, 2, 227, DateTimeKind.Utc).AddTicks(4699) });

            migrationBuilder.UpdateData(
                table: "Questions",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 5, 27, 2, 52, 2, 228, DateTimeKind.Utc).AddTicks(8831), new DateTime(2025, 5, 27, 2, 52, 2, 228, DateTimeKind.Utc).AddTicks(8831) });

            migrationBuilder.UpdateData(
                table: "Questions",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 5, 27, 2, 52, 2, 228, DateTimeKind.Utc).AddTicks(8831), new DateTime(2025, 5, 27, 2, 52, 2, 228, DateTimeKind.Utc).AddTicks(8831) });

            migrationBuilder.UpdateData(
                table: "Questions",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 5, 27, 2, 52, 2, 228, DateTimeKind.Utc).AddTicks(8831), new DateTime(2025, 5, 27, 2, 52, 2, 228, DateTimeKind.Utc).AddTicks(8831) });

            migrationBuilder.UpdateData(
                table: "Questions",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 5, 27, 2, 52, 2, 228, DateTimeKind.Utc).AddTicks(8831), new DateTime(2025, 5, 27, 2, 52, 2, 228, DateTimeKind.Utc).AddTicks(8831) });

            migrationBuilder.UpdateData(
                table: "Questions",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 5, 27, 2, 52, 2, 228, DateTimeKind.Utc).AddTicks(8831), new DateTime(2025, 5, 27, 2, 52, 2, 228, DateTimeKind.Utc).AddTicks(8831) });

            migrationBuilder.UpdateData(
                table: "Questions",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 5, 27, 2, 52, 2, 228, DateTimeKind.Utc).AddTicks(8831), new DateTime(2025, 5, 27, 2, 52, 2, 228, DateTimeKind.Utc).AddTicks(8831) });

            migrationBuilder.UpdateData(
                table: "Questions",
                keyColumn: "Id",
                keyValue: 7,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 5, 27, 2, 52, 2, 228, DateTimeKind.Utc).AddTicks(8831), new DateTime(2025, 5, 27, 2, 52, 2, 228, DateTimeKind.Utc).AddTicks(8831) });

            migrationBuilder.UpdateData(
                table: "Questions",
                keyColumn: "Id",
                keyValue: 8,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 5, 27, 2, 52, 2, 228, DateTimeKind.Utc).AddTicks(8831), new DateTime(2025, 5, 27, 2, 52, 2, 228, DateTimeKind.Utc).AddTicks(8831) });

            migrationBuilder.UpdateData(
                table: "Questions",
                keyColumn: "Id",
                keyValue: 9,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 5, 27, 2, 52, 2, 228, DateTimeKind.Utc).AddTicks(8831), new DateTime(2025, 5, 27, 2, 52, 2, 228, DateTimeKind.Utc).AddTicks(8831) });

            migrationBuilder.UpdateData(
                table: "Questions",
                keyColumn: "Id",
                keyValue: 10,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 5, 27, 2, 52, 2, 228, DateTimeKind.Utc).AddTicks(8831), new DateTime(2025, 5, 27, 2, 52, 2, 228, DateTimeKind.Utc).AddTicks(8831) });

            migrationBuilder.CreateIndex(
                name: "IX_SesionesCalendario_CoachId_FechaHoraInicio",
                table: "SesionesCalendario",
                columns: new[] { "CoachId", "FechaHoraInicio" });

            migrationBuilder.CreateIndex(
                name: "IX_SesionesCalendario_FechaHoraInicio",
                table: "SesionesCalendario",
                column: "FechaHoraInicio");

            migrationBuilder.CreateIndex(
                name: "IX_SesionesClientes_ClienteId",
                table: "SesionesClientes",
                column: "ClienteId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SesionesClientes");

            migrationBuilder.DropTable(
                name: "SesionesCalendario");

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
    }
}
