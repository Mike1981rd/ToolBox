using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace ToolBox.Migrations
{
    /// <inheritdoc />
    public partial class CommunicationWheelSchema : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CommunicationWheelTemplates",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Description = table.Column<string>(type: "TEXT", nullable: true),
                    CoachId = table.Column<int>(type: "integer", nullable: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CommunicationWheelTemplates", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CommunicationWheelTemplates_Users_CoachId",
                        column: x => x.CoachId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ClientCommunicationWheelInstances",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    CommunicationWheelTemplateId = table.Column<long>(type: "bigint", nullable: false),
                    ClientId = table.Column<int>(type: "integer", nullable: false),
                    AssignedByCoachId = table.Column<int>(type: "integer", nullable: false),
                    Status = table.Column<int>(type: "integer", nullable: false),
                    ClientNotes = table.Column<string>(type: "TEXT", nullable: true),
                    AssignedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    StartedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    CompletedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClientCommunicationWheelInstances", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ClientCommunicationWheelInstances_CommunicationWheelTemplat~",
                        column: x => x.CommunicationWheelTemplateId,
                        principalTable: "CommunicationWheelTemplates",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ClientCommunicationWheelInstances_Users_AssignedByCoachId",
                        column: x => x.AssignedByCoachId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ClientCommunicationWheelInstances_Users_ClientId",
                        column: x => x.ClientId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CommunicationDimensions",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    CommunicationWheelTemplateId = table.Column<long>(type: "bigint", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    GuidingQuestion = table.Column<string>(type: "TEXT", nullable: true),
                    Order = table.Column<int>(type: "integer", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CommunicationDimensions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CommunicationDimensions_CommunicationWheelTemplates_Communi~",
                        column: x => x.CommunicationWheelTemplateId,
                        principalTable: "CommunicationWheelTemplates",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DimensionScores",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ClientCommunicationWheelInstanceId = table.Column<long>(type: "bigint", nullable: false),
                    CommunicationDimensionId = table.Column<long>(type: "bigint", nullable: false),
                    Score = table.Column<int>(type: "integer", nullable: false),
                    ScoredAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DimensionScores", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DimensionScores_ClientCommunicationWheelInstances_ClientCom~",
                        column: x => x.ClientCommunicationWheelInstanceId,
                        principalTable: "ClientCommunicationWheelInstances",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DimensionScores_CommunicationDimensions_CommunicationDimens~",
                        column: x => x.CommunicationDimensionId,
                        principalTable: "CommunicationDimensions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.UpdateData(
                table: "AreasProgreso",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2025, 5, 29, 23, 10, 27, 182, DateTimeKind.Utc).AddTicks(9920));

            migrationBuilder.UpdateData(
                table: "AreasProgreso",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2025, 5, 29, 23, 10, 27, 182, DateTimeKind.Utc).AddTicks(9920));

            migrationBuilder.UpdateData(
                table: "AreasProgreso",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedAt",
                value: new DateTime(2025, 5, 29, 23, 10, 27, 182, DateTimeKind.Utc).AddTicks(9920));

            migrationBuilder.UpdateData(
                table: "AreasProgreso",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedAt",
                value: new DateTime(2025, 5, 29, 23, 10, 27, 182, DateTimeKind.Utc).AddTicks(9920));

            migrationBuilder.UpdateData(
                table: "AreasProgreso",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreatedAt",
                value: new DateTime(2025, 5, 29, 23, 10, 27, 182, DateTimeKind.Utc).AddTicks(9920));

            migrationBuilder.UpdateData(
                table: "CategoriasHabitos",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "FechaCreacion" },
                values: new object[] { new DateTime(2025, 5, 29, 23, 10, 27, 186, DateTimeKind.Utc).AddTicks(5612), new DateTime(2025, 5, 29, 23, 10, 27, 186, DateTimeKind.Utc).AddTicks(5619) });

            migrationBuilder.UpdateData(
                table: "CategoriasHabitos",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedAt", "FechaCreacion" },
                values: new object[] { new DateTime(2025, 5, 29, 23, 10, 27, 186, DateTimeKind.Utc).AddTicks(5612), new DateTime(2025, 5, 29, 23, 10, 27, 186, DateTimeKind.Utc).AddTicks(6094) });

            migrationBuilder.UpdateData(
                table: "CategoriasHabitos",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedAt", "FechaCreacion" },
                values: new object[] { new DateTime(2025, 5, 29, 23, 10, 27, 186, DateTimeKind.Utc).AddTicks(5612), new DateTime(2025, 5, 29, 23, 10, 27, 186, DateTimeKind.Utc).AddTicks(6103) });

            migrationBuilder.UpdateData(
                table: "CategoriasHabitos",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "CreatedAt", "FechaCreacion" },
                values: new object[] { new DateTime(2025, 5, 29, 23, 10, 27, 186, DateTimeKind.Utc).AddTicks(5612), new DateTime(2025, 5, 29, 23, 10, 27, 186, DateTimeKind.Utc).AddTicks(6120) });

            migrationBuilder.UpdateData(
                table: "CategoriasHabitos",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "CreatedAt", "FechaCreacion" },
                values: new object[] { new DateTime(2025, 5, 29, 23, 10, 27, 186, DateTimeKind.Utc).AddTicks(5612), new DateTime(2025, 5, 29, 23, 10, 27, 186, DateTimeKind.Utc).AddTicks(6123) });

            migrationBuilder.UpdateData(
                table: "CategoriasHabitos",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "CreatedAt", "FechaCreacion" },
                values: new object[] { new DateTime(2025, 5, 29, 23, 10, 27, 186, DateTimeKind.Utc).AddTicks(5612), new DateTime(2025, 5, 29, 23, 10, 27, 186, DateTimeKind.Utc).AddTicks(6126) });

            migrationBuilder.UpdateData(
                table: "CategoriasHabitos",
                keyColumn: "Id",
                keyValue: 7,
                columns: new[] { "CreatedAt", "FechaCreacion" },
                values: new object[] { new DateTime(2025, 5, 29, 23, 10, 27, 186, DateTimeKind.Utc).AddTicks(5612), new DateTime(2025, 5, 29, 23, 10, 27, 186, DateTimeKind.Utc).AddTicks(6128) });

            migrationBuilder.UpdateData(
                table: "CategoriasHabitos",
                keyColumn: "Id",
                keyValue: 8,
                columns: new[] { "CreatedAt", "FechaCreacion" },
                values: new object[] { new DateTime(2025, 5, 29, 23, 10, 27, 186, DateTimeKind.Utc).AddTicks(5612), new DateTime(2025, 5, 29, 23, 10, 27, 186, DateTimeKind.Utc).AddTicks(6131) });

            migrationBuilder.UpdateData(
                table: "CategoriasProgreso",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2025, 5, 29, 23, 10, 27, 182, DateTimeKind.Utc).AddTicks(9920));

            migrationBuilder.UpdateData(
                table: "CategoriasProgreso",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2025, 5, 29, 23, 10, 27, 182, DateTimeKind.Utc).AddTicks(9920));

            migrationBuilder.UpdateData(
                table: "CategoriasProgreso",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedAt",
                value: new DateTime(2025, 5, 29, 23, 10, 27, 182, DateTimeKind.Utc).AddTicks(9920));

            migrationBuilder.UpdateData(
                table: "CategoriasProgreso",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedAt",
                value: new DateTime(2025, 5, 29, 23, 10, 27, 182, DateTimeKind.Utc).AddTicks(9920));

            migrationBuilder.UpdateData(
                table: "CategoriasProgreso",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreatedAt",
                value: new DateTime(2025, 5, 29, 23, 10, 27, 182, DateTimeKind.Utc).AddTicks(9920));

            migrationBuilder.UpdateData(
                table: "CategoriasProgreso",
                keyColumn: "Id",
                keyValue: 6,
                column: "CreatedAt",
                value: new DateTime(2025, 5, 29, 23, 10, 27, 182, DateTimeKind.Utc).AddTicks(9920));

            migrationBuilder.UpdateData(
                table: "CategoriasProgreso",
                keyColumn: "Id",
                keyValue: 7,
                column: "CreatedAt",
                value: new DateTime(2025, 5, 29, 23, 10, 27, 182, DateTimeKind.Utc).AddTicks(9920));

            migrationBuilder.UpdateData(
                table: "CategoriasProgreso",
                keyColumn: "Id",
                keyValue: 8,
                column: "CreatedAt",
                value: new DateTime(2025, 5, 29, 23, 10, 27, 182, DateTimeKind.Utc).AddTicks(9920));

            migrationBuilder.UpdateData(
                table: "CategoriasProgreso",
                keyColumn: "Id",
                keyValue: 9,
                column: "CreatedAt",
                value: new DateTime(2025, 5, 29, 23, 10, 27, 182, DateTimeKind.Utc).AddTicks(9920));

            migrationBuilder.UpdateData(
                table: "CategoriasProgreso",
                keyColumn: "Id",
                keyValue: 10,
                column: "CreatedAt",
                value: new DateTime(2025, 5, 29, 23, 10, 27, 182, DateTimeKind.Utc).AddTicks(9920));

            migrationBuilder.UpdateData(
                table: "CategoriasProgreso",
                keyColumn: "Id",
                keyValue: 11,
                column: "CreatedAt",
                value: new DateTime(2025, 5, 29, 23, 10, 27, 182, DateTimeKind.Utc).AddTicks(9920));

            migrationBuilder.UpdateData(
                table: "CategoriasProgreso",
                keyColumn: "Id",
                keyValue: 12,
                column: "CreatedAt",
                value: new DateTime(2025, 5, 29, 23, 10, 27, 182, DateTimeKind.Utc).AddTicks(9920));

            migrationBuilder.UpdateData(
                table: "CategoriasProgreso",
                keyColumn: "Id",
                keyValue: 13,
                column: "CreatedAt",
                value: new DateTime(2025, 5, 29, 23, 10, 27, 182, DateTimeKind.Utc).AddTicks(9920));

            migrationBuilder.UpdateData(
                table: "CategoriasProgreso",
                keyColumn: "Id",
                keyValue: 14,
                column: "CreatedAt",
                value: new DateTime(2025, 5, 29, 23, 10, 27, 182, DateTimeKind.Utc).AddTicks(9920));

            migrationBuilder.UpdateData(
                table: "CategoriasProgreso",
                keyColumn: "Id",
                keyValue: 15,
                column: "CreatedAt",
                value: new DateTime(2025, 5, 29, 23, 10, 27, 182, DateTimeKind.Utc).AddTicks(9920));

            migrationBuilder.UpdateData(
                table: "FrecuenciasHabitos",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "FechaCreacion" },
                values: new object[] { new DateTime(2025, 5, 29, 23, 10, 27, 186, DateTimeKind.Utc).AddTicks(5612), new DateTime(2025, 5, 29, 23, 10, 27, 186, DateTimeKind.Utc).AddTicks(6135) });

            migrationBuilder.UpdateData(
                table: "FrecuenciasHabitos",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedAt", "FechaCreacion" },
                values: new object[] { new DateTime(2025, 5, 29, 23, 10, 27, 186, DateTimeKind.Utc).AddTicks(5612), new DateTime(2025, 5, 29, 23, 10, 27, 186, DateTimeKind.Utc).AddTicks(6141) });

            migrationBuilder.UpdateData(
                table: "FrecuenciasHabitos",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedAt", "FechaCreacion" },
                values: new object[] { new DateTime(2025, 5, 29, 23, 10, 27, 186, DateTimeKind.Utc).AddTicks(5612), new DateTime(2025, 5, 29, 23, 10, 27, 186, DateTimeKind.Utc).AddTicks(6143) });

            migrationBuilder.UpdateData(
                table: "FrecuenciasHabitos",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "CreatedAt", "FechaCreacion" },
                values: new object[] { new DateTime(2025, 5, 29, 23, 10, 27, 186, DateTimeKind.Utc).AddTicks(5612), new DateTime(2025, 5, 29, 23, 10, 27, 186, DateTimeKind.Utc).AddTicks(6146) });

            migrationBuilder.UpdateData(
                table: "FrecuenciasHabitos",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "CreatedAt", "FechaCreacion" },
                values: new object[] { new DateTime(2025, 5, 29, 23, 10, 27, 186, DateTimeKind.Utc).AddTicks(5612), new DateTime(2025, 5, 29, 23, 10, 27, 186, DateTimeKind.Utc).AddTicks(6149) });

            migrationBuilder.UpdateData(
                table: "FrecuenciasHabitos",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "CreatedAt", "FechaCreacion" },
                values: new object[] { new DateTime(2025, 5, 29, 23, 10, 27, 186, DateTimeKind.Utc).AddTicks(5612), new DateTime(2025, 5, 29, 23, 10, 27, 186, DateTimeKind.Utc).AddTicks(6151) });

            migrationBuilder.UpdateData(
                table: "LifeAreas",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 5, 29, 23, 10, 27, 178, DateTimeKind.Utc).AddTicks(6209), new DateTime(2025, 5, 29, 23, 10, 27, 178, DateTimeKind.Utc).AddTicks(6209) });

            migrationBuilder.UpdateData(
                table: "LifeAreas",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 5, 29, 23, 10, 27, 178, DateTimeKind.Utc).AddTicks(6209), new DateTime(2025, 5, 29, 23, 10, 27, 178, DateTimeKind.Utc).AddTicks(6209) });

            migrationBuilder.UpdateData(
                table: "LifeAreas",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 5, 29, 23, 10, 27, 178, DateTimeKind.Utc).AddTicks(6209), new DateTime(2025, 5, 29, 23, 10, 27, 178, DateTimeKind.Utc).AddTicks(6209) });

            migrationBuilder.UpdateData(
                table: "LifeAreas",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 5, 29, 23, 10, 27, 178, DateTimeKind.Utc).AddTicks(6209), new DateTime(2025, 5, 29, 23, 10, 27, 178, DateTimeKind.Utc).AddTicks(6209) });

            migrationBuilder.UpdateData(
                table: "LifeAreas",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 5, 29, 23, 10, 27, 178, DateTimeKind.Utc).AddTicks(6209), new DateTime(2025, 5, 29, 23, 10, 27, 178, DateTimeKind.Utc).AddTicks(6209) });

            migrationBuilder.UpdateData(
                table: "LifeAreas",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 5, 29, 23, 10, 27, 178, DateTimeKind.Utc).AddTicks(6209), new DateTime(2025, 5, 29, 23, 10, 27, 178, DateTimeKind.Utc).AddTicks(6209) });

            migrationBuilder.UpdateData(
                table: "LifeAreas",
                keyColumn: "Id",
                keyValue: 7,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 5, 29, 23, 10, 27, 178, DateTimeKind.Utc).AddTicks(6209), new DateTime(2025, 5, 29, 23, 10, 27, 178, DateTimeKind.Utc).AddTicks(6209) });

            migrationBuilder.UpdateData(
                table: "LifeAreas",
                keyColumn: "Id",
                keyValue: 8,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 5, 29, 23, 10, 27, 178, DateTimeKind.Utc).AddTicks(6209), new DateTime(2025, 5, 29, 23, 10, 27, 178, DateTimeKind.Utc).AddTicks(6209) });

            migrationBuilder.UpdateData(
                table: "LifeAreas",
                keyColumn: "Id",
                keyValue: 9,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 5, 29, 23, 10, 27, 178, DateTimeKind.Utc).AddTicks(6209), new DateTime(2025, 5, 29, 23, 10, 27, 178, DateTimeKind.Utc).AddTicks(6209) });

            migrationBuilder.UpdateData(
                table: "LifeAreas",
                keyColumn: "Id",
                keyValue: 10,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 5, 29, 23, 10, 27, 178, DateTimeKind.Utc).AddTicks(6209), new DateTime(2025, 5, 29, 23, 10, 27, 178, DateTimeKind.Utc).AddTicks(6209) });

            migrationBuilder.UpdateData(
                table: "Questions",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 5, 29, 23, 10, 27, 182, DateTimeKind.Utc).AddTicks(9652), new DateTime(2025, 5, 29, 23, 10, 27, 182, DateTimeKind.Utc).AddTicks(9652) });

            migrationBuilder.UpdateData(
                table: "Questions",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 5, 29, 23, 10, 27, 182, DateTimeKind.Utc).AddTicks(9652), new DateTime(2025, 5, 29, 23, 10, 27, 182, DateTimeKind.Utc).AddTicks(9652) });

            migrationBuilder.UpdateData(
                table: "Questions",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 5, 29, 23, 10, 27, 182, DateTimeKind.Utc).AddTicks(9652), new DateTime(2025, 5, 29, 23, 10, 27, 182, DateTimeKind.Utc).AddTicks(9652) });

            migrationBuilder.UpdateData(
                table: "Questions",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 5, 29, 23, 10, 27, 182, DateTimeKind.Utc).AddTicks(9652), new DateTime(2025, 5, 29, 23, 10, 27, 182, DateTimeKind.Utc).AddTicks(9652) });

            migrationBuilder.UpdateData(
                table: "Questions",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 5, 29, 23, 10, 27, 182, DateTimeKind.Utc).AddTicks(9652), new DateTime(2025, 5, 29, 23, 10, 27, 182, DateTimeKind.Utc).AddTicks(9652) });

            migrationBuilder.UpdateData(
                table: "Questions",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 5, 29, 23, 10, 27, 182, DateTimeKind.Utc).AddTicks(9652), new DateTime(2025, 5, 29, 23, 10, 27, 182, DateTimeKind.Utc).AddTicks(9652) });

            migrationBuilder.UpdateData(
                table: "Questions",
                keyColumn: "Id",
                keyValue: 7,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 5, 29, 23, 10, 27, 182, DateTimeKind.Utc).AddTicks(9652), new DateTime(2025, 5, 29, 23, 10, 27, 182, DateTimeKind.Utc).AddTicks(9652) });

            migrationBuilder.UpdateData(
                table: "Questions",
                keyColumn: "Id",
                keyValue: 8,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 5, 29, 23, 10, 27, 182, DateTimeKind.Utc).AddTicks(9652), new DateTime(2025, 5, 29, 23, 10, 27, 182, DateTimeKind.Utc).AddTicks(9652) });

            migrationBuilder.UpdateData(
                table: "Questions",
                keyColumn: "Id",
                keyValue: 9,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 5, 29, 23, 10, 27, 182, DateTimeKind.Utc).AddTicks(9652), new DateTime(2025, 5, 29, 23, 10, 27, 182, DateTimeKind.Utc).AddTicks(9652) });

            migrationBuilder.UpdateData(
                table: "Questions",
                keyColumn: "Id",
                keyValue: 10,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 5, 29, 23, 10, 27, 182, DateTimeKind.Utc).AddTicks(9652), new DateTime(2025, 5, 29, 23, 10, 27, 182, DateTimeKind.Utc).AddTicks(9652) });

            migrationBuilder.CreateIndex(
                name: "IX_ClientCommunicationWheelInstances_AssignedAt",
                table: "ClientCommunicationWheelInstances",
                column: "AssignedAt");

            migrationBuilder.CreateIndex(
                name: "IX_ClientCommunicationWheelInstances_AssignedByCoachId",
                table: "ClientCommunicationWheelInstances",
                column: "AssignedByCoachId");

            migrationBuilder.CreateIndex(
                name: "IX_ClientCommunicationWheelInstances_ClientId_Status",
                table: "ClientCommunicationWheelInstances",
                columns: new[] { "ClientId", "Status" });

            migrationBuilder.CreateIndex(
                name: "IX_ClientCommunicationWheelInstances_CommunicationWheelTemplat~",
                table: "ClientCommunicationWheelInstances",
                column: "CommunicationWheelTemplateId");

            migrationBuilder.CreateIndex(
                name: "IX_CommunicationDimensions_CommunicationWheelTemplateId_Order",
                table: "CommunicationDimensions",
                columns: new[] { "CommunicationWheelTemplateId", "Order" });

            migrationBuilder.CreateIndex(
                name: "IX_CommunicationWheelTemplates_CoachId",
                table: "CommunicationWheelTemplates",
                column: "CoachId");

            migrationBuilder.CreateIndex(
                name: "IX_CommunicationWheelTemplates_IsActive",
                table: "CommunicationWheelTemplates",
                column: "IsActive");

            migrationBuilder.CreateIndex(
                name: "IX_DimensionScores_ClientCommunicationWheelInstanceId_Communic~",
                table: "DimensionScores",
                columns: new[] { "ClientCommunicationWheelInstanceId", "CommunicationDimensionId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_DimensionScores_CommunicationDimensionId",
                table: "DimensionScores",
                column: "CommunicationDimensionId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DimensionScores");

            migrationBuilder.DropTable(
                name: "ClientCommunicationWheelInstances");

            migrationBuilder.DropTable(
                name: "CommunicationDimensions");

            migrationBuilder.DropTable(
                name: "CommunicationWheelTemplates");

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
        }
    }
}
