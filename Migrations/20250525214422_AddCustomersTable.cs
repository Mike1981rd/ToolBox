using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace ToolBox.Migrations
{
    /// <inheritdoc />
    public partial class AddCustomersTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Customers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    FirstName = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    LastName = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Email = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    PhoneNumber = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    CompanyName = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: true),
                    Country = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    AvatarUrl = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    StatusDetail = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    CreatedByUserId = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Customers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Customers_Users_CreatedByUserId",
                        column: x => x.CreatedByUserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                });

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

            migrationBuilder.CreateIndex(
                name: "IX_Customers_CreatedAt",
                table: "Customers",
                column: "CreatedAt");

            migrationBuilder.CreateIndex(
                name: "IX_Customers_CreatedByUserId",
                table: "Customers",
                column: "CreatedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Customers_Email",
                table: "Customers",
                column: "Email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Customers_FirstName_LastName",
                table: "Customers",
                columns: new[] { "FirstName", "LastName" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Customers");

            migrationBuilder.UpdateData(
                table: "AreasProgreso",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2025, 5, 25, 21, 1, 19, 700, DateTimeKind.Utc).AddTicks(7409));

            migrationBuilder.UpdateData(
                table: "AreasProgreso",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2025, 5, 25, 21, 1, 19, 700, DateTimeKind.Utc).AddTicks(7409));

            migrationBuilder.UpdateData(
                table: "AreasProgreso",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedAt",
                value: new DateTime(2025, 5, 25, 21, 1, 19, 700, DateTimeKind.Utc).AddTicks(7409));

            migrationBuilder.UpdateData(
                table: "AreasProgreso",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedAt",
                value: new DateTime(2025, 5, 25, 21, 1, 19, 700, DateTimeKind.Utc).AddTicks(7409));

            migrationBuilder.UpdateData(
                table: "AreasProgreso",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreatedAt",
                value: new DateTime(2025, 5, 25, 21, 1, 19, 700, DateTimeKind.Utc).AddTicks(7409));

            migrationBuilder.UpdateData(
                table: "CategoriasHabitos",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "FechaCreacion" },
                values: new object[] { new DateTime(2025, 5, 25, 21, 1, 19, 702, DateTimeKind.Utc).AddTicks(1939), new DateTime(2025, 5, 25, 21, 1, 19, 702, DateTimeKind.Utc).AddTicks(1944) });

            migrationBuilder.UpdateData(
                table: "CategoriasHabitos",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedAt", "FechaCreacion" },
                values: new object[] { new DateTime(2025, 5, 25, 21, 1, 19, 702, DateTimeKind.Utc).AddTicks(1939), new DateTime(2025, 5, 25, 21, 1, 19, 702, DateTimeKind.Utc).AddTicks(1952) });

            migrationBuilder.UpdateData(
                table: "CategoriasHabitos",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedAt", "FechaCreacion" },
                values: new object[] { new DateTime(2025, 5, 25, 21, 1, 19, 702, DateTimeKind.Utc).AddTicks(1939), new DateTime(2025, 5, 25, 21, 1, 19, 702, DateTimeKind.Utc).AddTicks(1955) });

            migrationBuilder.UpdateData(
                table: "CategoriasHabitos",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "CreatedAt", "FechaCreacion" },
                values: new object[] { new DateTime(2025, 5, 25, 21, 1, 19, 702, DateTimeKind.Utc).AddTicks(1939), new DateTime(2025, 5, 25, 21, 1, 19, 702, DateTimeKind.Utc).AddTicks(1958) });

            migrationBuilder.UpdateData(
                table: "CategoriasHabitos",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "CreatedAt", "FechaCreacion" },
                values: new object[] { new DateTime(2025, 5, 25, 21, 1, 19, 702, DateTimeKind.Utc).AddTicks(1939), new DateTime(2025, 5, 25, 21, 1, 19, 702, DateTimeKind.Utc).AddTicks(1961) });

            migrationBuilder.UpdateData(
                table: "CategoriasHabitos",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "CreatedAt", "FechaCreacion" },
                values: new object[] { new DateTime(2025, 5, 25, 21, 1, 19, 702, DateTimeKind.Utc).AddTicks(1939), new DateTime(2025, 5, 25, 21, 1, 19, 702, DateTimeKind.Utc).AddTicks(1964) });

            migrationBuilder.UpdateData(
                table: "CategoriasHabitos",
                keyColumn: "Id",
                keyValue: 7,
                columns: new[] { "CreatedAt", "FechaCreacion" },
                values: new object[] { new DateTime(2025, 5, 25, 21, 1, 19, 702, DateTimeKind.Utc).AddTicks(1939), new DateTime(2025, 5, 25, 21, 1, 19, 702, DateTimeKind.Utc).AddTicks(1966) });

            migrationBuilder.UpdateData(
                table: "CategoriasHabitos",
                keyColumn: "Id",
                keyValue: 8,
                columns: new[] { "CreatedAt", "FechaCreacion" },
                values: new object[] { new DateTime(2025, 5, 25, 21, 1, 19, 702, DateTimeKind.Utc).AddTicks(1939), new DateTime(2025, 5, 25, 21, 1, 19, 702, DateTimeKind.Utc).AddTicks(1969) });

            migrationBuilder.UpdateData(
                table: "CategoriasProgreso",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2025, 5, 25, 21, 1, 19, 700, DateTimeKind.Utc).AddTicks(7409));

            migrationBuilder.UpdateData(
                table: "CategoriasProgreso",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2025, 5, 25, 21, 1, 19, 700, DateTimeKind.Utc).AddTicks(7409));

            migrationBuilder.UpdateData(
                table: "CategoriasProgreso",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedAt",
                value: new DateTime(2025, 5, 25, 21, 1, 19, 700, DateTimeKind.Utc).AddTicks(7409));

            migrationBuilder.UpdateData(
                table: "CategoriasProgreso",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedAt",
                value: new DateTime(2025, 5, 25, 21, 1, 19, 700, DateTimeKind.Utc).AddTicks(7409));

            migrationBuilder.UpdateData(
                table: "CategoriasProgreso",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreatedAt",
                value: new DateTime(2025, 5, 25, 21, 1, 19, 700, DateTimeKind.Utc).AddTicks(7409));

            migrationBuilder.UpdateData(
                table: "CategoriasProgreso",
                keyColumn: "Id",
                keyValue: 6,
                column: "CreatedAt",
                value: new DateTime(2025, 5, 25, 21, 1, 19, 700, DateTimeKind.Utc).AddTicks(7409));

            migrationBuilder.UpdateData(
                table: "CategoriasProgreso",
                keyColumn: "Id",
                keyValue: 7,
                column: "CreatedAt",
                value: new DateTime(2025, 5, 25, 21, 1, 19, 700, DateTimeKind.Utc).AddTicks(7409));

            migrationBuilder.UpdateData(
                table: "CategoriasProgreso",
                keyColumn: "Id",
                keyValue: 8,
                column: "CreatedAt",
                value: new DateTime(2025, 5, 25, 21, 1, 19, 700, DateTimeKind.Utc).AddTicks(7409));

            migrationBuilder.UpdateData(
                table: "CategoriasProgreso",
                keyColumn: "Id",
                keyValue: 9,
                column: "CreatedAt",
                value: new DateTime(2025, 5, 25, 21, 1, 19, 700, DateTimeKind.Utc).AddTicks(7409));

            migrationBuilder.UpdateData(
                table: "CategoriasProgreso",
                keyColumn: "Id",
                keyValue: 10,
                column: "CreatedAt",
                value: new DateTime(2025, 5, 25, 21, 1, 19, 700, DateTimeKind.Utc).AddTicks(7409));

            migrationBuilder.UpdateData(
                table: "CategoriasProgreso",
                keyColumn: "Id",
                keyValue: 11,
                column: "CreatedAt",
                value: new DateTime(2025, 5, 25, 21, 1, 19, 700, DateTimeKind.Utc).AddTicks(7409));

            migrationBuilder.UpdateData(
                table: "CategoriasProgreso",
                keyColumn: "Id",
                keyValue: 12,
                column: "CreatedAt",
                value: new DateTime(2025, 5, 25, 21, 1, 19, 700, DateTimeKind.Utc).AddTicks(7409));

            migrationBuilder.UpdateData(
                table: "CategoriasProgreso",
                keyColumn: "Id",
                keyValue: 13,
                column: "CreatedAt",
                value: new DateTime(2025, 5, 25, 21, 1, 19, 700, DateTimeKind.Utc).AddTicks(7409));

            migrationBuilder.UpdateData(
                table: "CategoriasProgreso",
                keyColumn: "Id",
                keyValue: 14,
                column: "CreatedAt",
                value: new DateTime(2025, 5, 25, 21, 1, 19, 700, DateTimeKind.Utc).AddTicks(7409));

            migrationBuilder.UpdateData(
                table: "CategoriasProgreso",
                keyColumn: "Id",
                keyValue: 15,
                column: "CreatedAt",
                value: new DateTime(2025, 5, 25, 21, 1, 19, 700, DateTimeKind.Utc).AddTicks(7409));

            migrationBuilder.UpdateData(
                table: "FrecuenciasHabitos",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "FechaCreacion" },
                values: new object[] { new DateTime(2025, 5, 25, 21, 1, 19, 702, DateTimeKind.Utc).AddTicks(1939), new DateTime(2025, 5, 25, 21, 1, 19, 702, DateTimeKind.Utc).AddTicks(1973) });

            migrationBuilder.UpdateData(
                table: "FrecuenciasHabitos",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedAt", "FechaCreacion" },
                values: new object[] { new DateTime(2025, 5, 25, 21, 1, 19, 702, DateTimeKind.Utc).AddTicks(1939), new DateTime(2025, 5, 25, 21, 1, 19, 702, DateTimeKind.Utc).AddTicks(1978) });

            migrationBuilder.UpdateData(
                table: "FrecuenciasHabitos",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedAt", "FechaCreacion" },
                values: new object[] { new DateTime(2025, 5, 25, 21, 1, 19, 702, DateTimeKind.Utc).AddTicks(1939), new DateTime(2025, 5, 25, 21, 1, 19, 702, DateTimeKind.Utc).AddTicks(1981) });

            migrationBuilder.UpdateData(
                table: "FrecuenciasHabitos",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "CreatedAt", "FechaCreacion" },
                values: new object[] { new DateTime(2025, 5, 25, 21, 1, 19, 702, DateTimeKind.Utc).AddTicks(1939), new DateTime(2025, 5, 25, 21, 1, 19, 702, DateTimeKind.Utc).AddTicks(1984) });

            migrationBuilder.UpdateData(
                table: "FrecuenciasHabitos",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "CreatedAt", "FechaCreacion" },
                values: new object[] { new DateTime(2025, 5, 25, 21, 1, 19, 702, DateTimeKind.Utc).AddTicks(1939), new DateTime(2025, 5, 25, 21, 1, 19, 702, DateTimeKind.Utc).AddTicks(1986) });

            migrationBuilder.UpdateData(
                table: "FrecuenciasHabitos",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "CreatedAt", "FechaCreacion" },
                values: new object[] { new DateTime(2025, 5, 25, 21, 1, 19, 702, DateTimeKind.Utc).AddTicks(1939), new DateTime(2025, 5, 25, 21, 1, 19, 702, DateTimeKind.Utc).AddTicks(1988) });

            migrationBuilder.UpdateData(
                table: "LifeAreas",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 5, 25, 21, 1, 19, 698, DateTimeKind.Utc).AddTicks(1679), new DateTime(2025, 5, 25, 21, 1, 19, 698, DateTimeKind.Utc).AddTicks(1679) });

            migrationBuilder.UpdateData(
                table: "LifeAreas",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 5, 25, 21, 1, 19, 698, DateTimeKind.Utc).AddTicks(1679), new DateTime(2025, 5, 25, 21, 1, 19, 698, DateTimeKind.Utc).AddTicks(1679) });

            migrationBuilder.UpdateData(
                table: "LifeAreas",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 5, 25, 21, 1, 19, 698, DateTimeKind.Utc).AddTicks(1679), new DateTime(2025, 5, 25, 21, 1, 19, 698, DateTimeKind.Utc).AddTicks(1679) });

            migrationBuilder.UpdateData(
                table: "LifeAreas",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 5, 25, 21, 1, 19, 698, DateTimeKind.Utc).AddTicks(1679), new DateTime(2025, 5, 25, 21, 1, 19, 698, DateTimeKind.Utc).AddTicks(1679) });

            migrationBuilder.UpdateData(
                table: "LifeAreas",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 5, 25, 21, 1, 19, 698, DateTimeKind.Utc).AddTicks(1679), new DateTime(2025, 5, 25, 21, 1, 19, 698, DateTimeKind.Utc).AddTicks(1679) });

            migrationBuilder.UpdateData(
                table: "LifeAreas",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 5, 25, 21, 1, 19, 698, DateTimeKind.Utc).AddTicks(1679), new DateTime(2025, 5, 25, 21, 1, 19, 698, DateTimeKind.Utc).AddTicks(1679) });

            migrationBuilder.UpdateData(
                table: "LifeAreas",
                keyColumn: "Id",
                keyValue: 7,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 5, 25, 21, 1, 19, 698, DateTimeKind.Utc).AddTicks(1679), new DateTime(2025, 5, 25, 21, 1, 19, 698, DateTimeKind.Utc).AddTicks(1679) });

            migrationBuilder.UpdateData(
                table: "LifeAreas",
                keyColumn: "Id",
                keyValue: 8,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 5, 25, 21, 1, 19, 698, DateTimeKind.Utc).AddTicks(1679), new DateTime(2025, 5, 25, 21, 1, 19, 698, DateTimeKind.Utc).AddTicks(1679) });

            migrationBuilder.UpdateData(
                table: "LifeAreas",
                keyColumn: "Id",
                keyValue: 9,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 5, 25, 21, 1, 19, 698, DateTimeKind.Utc).AddTicks(1679), new DateTime(2025, 5, 25, 21, 1, 19, 698, DateTimeKind.Utc).AddTicks(1679) });

            migrationBuilder.UpdateData(
                table: "LifeAreas",
                keyColumn: "Id",
                keyValue: 10,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 5, 25, 21, 1, 19, 698, DateTimeKind.Utc).AddTicks(1679), new DateTime(2025, 5, 25, 21, 1, 19, 698, DateTimeKind.Utc).AddTicks(1679) });

            migrationBuilder.UpdateData(
                table: "Questions",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 5, 25, 21, 1, 19, 700, DateTimeKind.Utc).AddTicks(7288), new DateTime(2025, 5, 25, 21, 1, 19, 700, DateTimeKind.Utc).AddTicks(7288) });

            migrationBuilder.UpdateData(
                table: "Questions",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 5, 25, 21, 1, 19, 700, DateTimeKind.Utc).AddTicks(7288), new DateTime(2025, 5, 25, 21, 1, 19, 700, DateTimeKind.Utc).AddTicks(7288) });

            migrationBuilder.UpdateData(
                table: "Questions",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 5, 25, 21, 1, 19, 700, DateTimeKind.Utc).AddTicks(7288), new DateTime(2025, 5, 25, 21, 1, 19, 700, DateTimeKind.Utc).AddTicks(7288) });

            migrationBuilder.UpdateData(
                table: "Questions",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 5, 25, 21, 1, 19, 700, DateTimeKind.Utc).AddTicks(7288), new DateTime(2025, 5, 25, 21, 1, 19, 700, DateTimeKind.Utc).AddTicks(7288) });

            migrationBuilder.UpdateData(
                table: "Questions",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 5, 25, 21, 1, 19, 700, DateTimeKind.Utc).AddTicks(7288), new DateTime(2025, 5, 25, 21, 1, 19, 700, DateTimeKind.Utc).AddTicks(7288) });

            migrationBuilder.UpdateData(
                table: "Questions",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 5, 25, 21, 1, 19, 700, DateTimeKind.Utc).AddTicks(7288), new DateTime(2025, 5, 25, 21, 1, 19, 700, DateTimeKind.Utc).AddTicks(7288) });

            migrationBuilder.UpdateData(
                table: "Questions",
                keyColumn: "Id",
                keyValue: 7,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 5, 25, 21, 1, 19, 700, DateTimeKind.Utc).AddTicks(7288), new DateTime(2025, 5, 25, 21, 1, 19, 700, DateTimeKind.Utc).AddTicks(7288) });

            migrationBuilder.UpdateData(
                table: "Questions",
                keyColumn: "Id",
                keyValue: 8,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 5, 25, 21, 1, 19, 700, DateTimeKind.Utc).AddTicks(7288), new DateTime(2025, 5, 25, 21, 1, 19, 700, DateTimeKind.Utc).AddTicks(7288) });

            migrationBuilder.UpdateData(
                table: "Questions",
                keyColumn: "Id",
                keyValue: 9,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 5, 25, 21, 1, 19, 700, DateTimeKind.Utc).AddTicks(7288), new DateTime(2025, 5, 25, 21, 1, 19, 700, DateTimeKind.Utc).AddTicks(7288) });

            migrationBuilder.UpdateData(
                table: "Questions",
                keyColumn: "Id",
                keyValue: 10,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 5, 25, 21, 1, 19, 700, DateTimeKind.Utc).AddTicks(7288), new DateTime(2025, 5, 25, 21, 1, 19, 700, DateTimeKind.Utc).AddTicks(7288) });
        }
    }
}
