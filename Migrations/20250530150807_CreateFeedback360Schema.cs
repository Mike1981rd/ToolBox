using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace ToolBox.Migrations
{
    /// <inheritdoc />
    public partial class CreateFeedback360Schema : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Feedback360Instances",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    SubjectUserId = table.Column<int>(type: "integer", nullable: false),
                    InitiatedByCoachId = table.Column<int>(type: "integer", nullable: false),
                    Status = table.Column<int>(type: "integer", nullable: false),
                    InstanceTitle = table.Column<string>(type: "text", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    FeedbackDeadline = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    ReportGeneratedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Feedback360Instances", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Feedback360Instances_Users_InitiatedByCoachId",
                        column: x => x.InitiatedByCoachId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Feedback360Instances_Users_SubjectUserId",
                        column: x => x.SubjectUserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Feedback360Raters",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Feedback360InstanceId = table.Column<long>(type: "bigint", nullable: false),
                    RaterUserId = table.Column<int>(type: "integer", nullable: true),
                    ExternalRaterEmail = table.Column<string>(type: "text", nullable: true),
                    ExternalRaterName = table.Column<string>(type: "text", nullable: true),
                    Category = table.Column<int>(type: "integer", nullable: false),
                    UniqueResponseToken = table.Column<string>(type: "text", nullable: false),
                    Status = table.Column<int>(type: "integer", nullable: false),
                    InvitedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    LastReminderSentAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    CompletedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Feedback360Raters", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Feedback360Raters_Feedback360Instances_Feedback360InstanceId",
                        column: x => x.Feedback360InstanceId,
                        principalTable: "Feedback360Instances",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Feedback360Raters_Users_RaterUserId",
                        column: x => x.RaterUserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "Feedback360ResponseOpens",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Feedback360RaterId = table.Column<long>(type: "bigint", nullable: false),
                    QuestionCode = table.Column<string>(type: "text", nullable: false),
                    ResponseText = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Feedback360ResponseOpens", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Feedback360ResponseOpens_Feedback360Raters_Feedback360Rater~",
                        column: x => x.Feedback360RaterId,
                        principalTable: "Feedback360Raters",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Feedback360ResponseScales",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Feedback360RaterId = table.Column<long>(type: "bigint", nullable: false),
                    QuestionCode = table.Column<string>(type: "text", nullable: false),
                    Score = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Feedback360ResponseScales", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Feedback360ResponseScales_Feedback360Raters_Feedback360Rate~",
                        column: x => x.Feedback360RaterId,
                        principalTable: "Feedback360Raters",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.UpdateData(
                table: "AreasProgreso",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2025, 5, 30, 15, 8, 5, 784, DateTimeKind.Utc).AddTicks(1537));

            migrationBuilder.UpdateData(
                table: "AreasProgreso",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2025, 5, 30, 15, 8, 5, 784, DateTimeKind.Utc).AddTicks(1537));

            migrationBuilder.UpdateData(
                table: "AreasProgreso",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedAt",
                value: new DateTime(2025, 5, 30, 15, 8, 5, 784, DateTimeKind.Utc).AddTicks(1537));

            migrationBuilder.UpdateData(
                table: "AreasProgreso",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedAt",
                value: new DateTime(2025, 5, 30, 15, 8, 5, 784, DateTimeKind.Utc).AddTicks(1537));

            migrationBuilder.UpdateData(
                table: "AreasProgreso",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreatedAt",
                value: new DateTime(2025, 5, 30, 15, 8, 5, 784, DateTimeKind.Utc).AddTicks(1537));

            migrationBuilder.UpdateData(
                table: "CategoriasHabitos",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "FechaCreacion" },
                values: new object[] { new DateTime(2025, 5, 30, 15, 8, 5, 785, DateTimeKind.Utc).AddTicks(6685), new DateTime(2025, 5, 30, 15, 8, 5, 785, DateTimeKind.Utc).AddTicks(6691) });

            migrationBuilder.UpdateData(
                table: "CategoriasHabitos",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedAt", "FechaCreacion" },
                values: new object[] { new DateTime(2025, 5, 30, 15, 8, 5, 785, DateTimeKind.Utc).AddTicks(6685), new DateTime(2025, 5, 30, 15, 8, 5, 785, DateTimeKind.Utc).AddTicks(6697) });

            migrationBuilder.UpdateData(
                table: "CategoriasHabitos",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedAt", "FechaCreacion" },
                values: new object[] { new DateTime(2025, 5, 30, 15, 8, 5, 785, DateTimeKind.Utc).AddTicks(6685), new DateTime(2025, 5, 30, 15, 8, 5, 785, DateTimeKind.Utc).AddTicks(6700) });

            migrationBuilder.UpdateData(
                table: "CategoriasHabitos",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "CreatedAt", "FechaCreacion" },
                values: new object[] { new DateTime(2025, 5, 30, 15, 8, 5, 785, DateTimeKind.Utc).AddTicks(6685), new DateTime(2025, 5, 30, 15, 8, 5, 785, DateTimeKind.Utc).AddTicks(6707) });

            migrationBuilder.UpdateData(
                table: "CategoriasHabitos",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "CreatedAt", "FechaCreacion" },
                values: new object[] { new DateTime(2025, 5, 30, 15, 8, 5, 785, DateTimeKind.Utc).AddTicks(6685), new DateTime(2025, 5, 30, 15, 8, 5, 785, DateTimeKind.Utc).AddTicks(6709) });

            migrationBuilder.UpdateData(
                table: "CategoriasHabitos",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "CreatedAt", "FechaCreacion" },
                values: new object[] { new DateTime(2025, 5, 30, 15, 8, 5, 785, DateTimeKind.Utc).AddTicks(6685), new DateTime(2025, 5, 30, 15, 8, 5, 785, DateTimeKind.Utc).AddTicks(6711) });

            migrationBuilder.UpdateData(
                table: "CategoriasHabitos",
                keyColumn: "Id",
                keyValue: 7,
                columns: new[] { "CreatedAt", "FechaCreacion" },
                values: new object[] { new DateTime(2025, 5, 30, 15, 8, 5, 785, DateTimeKind.Utc).AddTicks(6685), new DateTime(2025, 5, 30, 15, 8, 5, 785, DateTimeKind.Utc).AddTicks(6713) });

            migrationBuilder.UpdateData(
                table: "CategoriasHabitos",
                keyColumn: "Id",
                keyValue: 8,
                columns: new[] { "CreatedAt", "FechaCreacion" },
                values: new object[] { new DateTime(2025, 5, 30, 15, 8, 5, 785, DateTimeKind.Utc).AddTicks(6685), new DateTime(2025, 5, 30, 15, 8, 5, 785, DateTimeKind.Utc).AddTicks(6715) });

            migrationBuilder.UpdateData(
                table: "CategoriasProgreso",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2025, 5, 30, 15, 8, 5, 784, DateTimeKind.Utc).AddTicks(1537));

            migrationBuilder.UpdateData(
                table: "CategoriasProgreso",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2025, 5, 30, 15, 8, 5, 784, DateTimeKind.Utc).AddTicks(1537));

            migrationBuilder.UpdateData(
                table: "CategoriasProgreso",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedAt",
                value: new DateTime(2025, 5, 30, 15, 8, 5, 784, DateTimeKind.Utc).AddTicks(1537));

            migrationBuilder.UpdateData(
                table: "CategoriasProgreso",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedAt",
                value: new DateTime(2025, 5, 30, 15, 8, 5, 784, DateTimeKind.Utc).AddTicks(1537));

            migrationBuilder.UpdateData(
                table: "CategoriasProgreso",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreatedAt",
                value: new DateTime(2025, 5, 30, 15, 8, 5, 784, DateTimeKind.Utc).AddTicks(1537));

            migrationBuilder.UpdateData(
                table: "CategoriasProgreso",
                keyColumn: "Id",
                keyValue: 6,
                column: "CreatedAt",
                value: new DateTime(2025, 5, 30, 15, 8, 5, 784, DateTimeKind.Utc).AddTicks(1537));

            migrationBuilder.UpdateData(
                table: "CategoriasProgreso",
                keyColumn: "Id",
                keyValue: 7,
                column: "CreatedAt",
                value: new DateTime(2025, 5, 30, 15, 8, 5, 784, DateTimeKind.Utc).AddTicks(1537));

            migrationBuilder.UpdateData(
                table: "CategoriasProgreso",
                keyColumn: "Id",
                keyValue: 8,
                column: "CreatedAt",
                value: new DateTime(2025, 5, 30, 15, 8, 5, 784, DateTimeKind.Utc).AddTicks(1537));

            migrationBuilder.UpdateData(
                table: "CategoriasProgreso",
                keyColumn: "Id",
                keyValue: 9,
                column: "CreatedAt",
                value: new DateTime(2025, 5, 30, 15, 8, 5, 784, DateTimeKind.Utc).AddTicks(1537));

            migrationBuilder.UpdateData(
                table: "CategoriasProgreso",
                keyColumn: "Id",
                keyValue: 10,
                column: "CreatedAt",
                value: new DateTime(2025, 5, 30, 15, 8, 5, 784, DateTimeKind.Utc).AddTicks(1537));

            migrationBuilder.UpdateData(
                table: "CategoriasProgreso",
                keyColumn: "Id",
                keyValue: 11,
                column: "CreatedAt",
                value: new DateTime(2025, 5, 30, 15, 8, 5, 784, DateTimeKind.Utc).AddTicks(1537));

            migrationBuilder.UpdateData(
                table: "CategoriasProgreso",
                keyColumn: "Id",
                keyValue: 12,
                column: "CreatedAt",
                value: new DateTime(2025, 5, 30, 15, 8, 5, 784, DateTimeKind.Utc).AddTicks(1537));

            migrationBuilder.UpdateData(
                table: "CategoriasProgreso",
                keyColumn: "Id",
                keyValue: 13,
                column: "CreatedAt",
                value: new DateTime(2025, 5, 30, 15, 8, 5, 784, DateTimeKind.Utc).AddTicks(1537));

            migrationBuilder.UpdateData(
                table: "CategoriasProgreso",
                keyColumn: "Id",
                keyValue: 14,
                column: "CreatedAt",
                value: new DateTime(2025, 5, 30, 15, 8, 5, 784, DateTimeKind.Utc).AddTicks(1537));

            migrationBuilder.UpdateData(
                table: "CategoriasProgreso",
                keyColumn: "Id",
                keyValue: 15,
                column: "CreatedAt",
                value: new DateTime(2025, 5, 30, 15, 8, 5, 784, DateTimeKind.Utc).AddTicks(1537));

            migrationBuilder.UpdateData(
                table: "FrecuenciasHabitos",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "FechaCreacion" },
                values: new object[] { new DateTime(2025, 5, 30, 15, 8, 5, 785, DateTimeKind.Utc).AddTicks(6685), new DateTime(2025, 5, 30, 15, 8, 5, 785, DateTimeKind.Utc).AddTicks(6718) });

            migrationBuilder.UpdateData(
                table: "FrecuenciasHabitos",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedAt", "FechaCreacion" },
                values: new object[] { new DateTime(2025, 5, 30, 15, 8, 5, 785, DateTimeKind.Utc).AddTicks(6685), new DateTime(2025, 5, 30, 15, 8, 5, 785, DateTimeKind.Utc).AddTicks(6723) });

            migrationBuilder.UpdateData(
                table: "FrecuenciasHabitos",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedAt", "FechaCreacion" },
                values: new object[] { new DateTime(2025, 5, 30, 15, 8, 5, 785, DateTimeKind.Utc).AddTicks(6685), new DateTime(2025, 5, 30, 15, 8, 5, 785, DateTimeKind.Utc).AddTicks(6725) });

            migrationBuilder.UpdateData(
                table: "FrecuenciasHabitos",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "CreatedAt", "FechaCreacion" },
                values: new object[] { new DateTime(2025, 5, 30, 15, 8, 5, 785, DateTimeKind.Utc).AddTicks(6685), new DateTime(2025, 5, 30, 15, 8, 5, 785, DateTimeKind.Utc).AddTicks(6727) });

            migrationBuilder.UpdateData(
                table: "FrecuenciasHabitos",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "CreatedAt", "FechaCreacion" },
                values: new object[] { new DateTime(2025, 5, 30, 15, 8, 5, 785, DateTimeKind.Utc).AddTicks(6685), new DateTime(2025, 5, 30, 15, 8, 5, 785, DateTimeKind.Utc).AddTicks(6729) });

            migrationBuilder.UpdateData(
                table: "FrecuenciasHabitos",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "CreatedAt", "FechaCreacion" },
                values: new object[] { new DateTime(2025, 5, 30, 15, 8, 5, 785, DateTimeKind.Utc).AddTicks(6685), new DateTime(2025, 5, 30, 15, 8, 5, 785, DateTimeKind.Utc).AddTicks(6731) });

            migrationBuilder.UpdateData(
                table: "LifeAreas",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 5, 30, 15, 8, 5, 780, DateTimeKind.Utc).AddTicks(4331), new DateTime(2025, 5, 30, 15, 8, 5, 780, DateTimeKind.Utc).AddTicks(4331) });

            migrationBuilder.UpdateData(
                table: "LifeAreas",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 5, 30, 15, 8, 5, 780, DateTimeKind.Utc).AddTicks(4331), new DateTime(2025, 5, 30, 15, 8, 5, 780, DateTimeKind.Utc).AddTicks(4331) });

            migrationBuilder.UpdateData(
                table: "LifeAreas",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 5, 30, 15, 8, 5, 780, DateTimeKind.Utc).AddTicks(4331), new DateTime(2025, 5, 30, 15, 8, 5, 780, DateTimeKind.Utc).AddTicks(4331) });

            migrationBuilder.UpdateData(
                table: "LifeAreas",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 5, 30, 15, 8, 5, 780, DateTimeKind.Utc).AddTicks(4331), new DateTime(2025, 5, 30, 15, 8, 5, 780, DateTimeKind.Utc).AddTicks(4331) });

            migrationBuilder.UpdateData(
                table: "LifeAreas",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 5, 30, 15, 8, 5, 780, DateTimeKind.Utc).AddTicks(4331), new DateTime(2025, 5, 30, 15, 8, 5, 780, DateTimeKind.Utc).AddTicks(4331) });

            migrationBuilder.UpdateData(
                table: "LifeAreas",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 5, 30, 15, 8, 5, 780, DateTimeKind.Utc).AddTicks(4331), new DateTime(2025, 5, 30, 15, 8, 5, 780, DateTimeKind.Utc).AddTicks(4331) });

            migrationBuilder.UpdateData(
                table: "LifeAreas",
                keyColumn: "Id",
                keyValue: 7,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 5, 30, 15, 8, 5, 780, DateTimeKind.Utc).AddTicks(4331), new DateTime(2025, 5, 30, 15, 8, 5, 780, DateTimeKind.Utc).AddTicks(4331) });

            migrationBuilder.UpdateData(
                table: "LifeAreas",
                keyColumn: "Id",
                keyValue: 8,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 5, 30, 15, 8, 5, 780, DateTimeKind.Utc).AddTicks(4331), new DateTime(2025, 5, 30, 15, 8, 5, 780, DateTimeKind.Utc).AddTicks(4331) });

            migrationBuilder.UpdateData(
                table: "LifeAreas",
                keyColumn: "Id",
                keyValue: 9,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 5, 30, 15, 8, 5, 780, DateTimeKind.Utc).AddTicks(4331), new DateTime(2025, 5, 30, 15, 8, 5, 780, DateTimeKind.Utc).AddTicks(4331) });

            migrationBuilder.UpdateData(
                table: "LifeAreas",
                keyColumn: "Id",
                keyValue: 10,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 5, 30, 15, 8, 5, 780, DateTimeKind.Utc).AddTicks(4331), new DateTime(2025, 5, 30, 15, 8, 5, 780, DateTimeKind.Utc).AddTicks(4331) });

            migrationBuilder.UpdateData(
                table: "Questions",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 5, 30, 15, 8, 5, 784, DateTimeKind.Utc).AddTicks(1405), new DateTime(2025, 5, 30, 15, 8, 5, 784, DateTimeKind.Utc).AddTicks(1405) });

            migrationBuilder.UpdateData(
                table: "Questions",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 5, 30, 15, 8, 5, 784, DateTimeKind.Utc).AddTicks(1405), new DateTime(2025, 5, 30, 15, 8, 5, 784, DateTimeKind.Utc).AddTicks(1405) });

            migrationBuilder.UpdateData(
                table: "Questions",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 5, 30, 15, 8, 5, 784, DateTimeKind.Utc).AddTicks(1405), new DateTime(2025, 5, 30, 15, 8, 5, 784, DateTimeKind.Utc).AddTicks(1405) });

            migrationBuilder.UpdateData(
                table: "Questions",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 5, 30, 15, 8, 5, 784, DateTimeKind.Utc).AddTicks(1405), new DateTime(2025, 5, 30, 15, 8, 5, 784, DateTimeKind.Utc).AddTicks(1405) });

            migrationBuilder.UpdateData(
                table: "Questions",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 5, 30, 15, 8, 5, 784, DateTimeKind.Utc).AddTicks(1405), new DateTime(2025, 5, 30, 15, 8, 5, 784, DateTimeKind.Utc).AddTicks(1405) });

            migrationBuilder.UpdateData(
                table: "Questions",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 5, 30, 15, 8, 5, 784, DateTimeKind.Utc).AddTicks(1405), new DateTime(2025, 5, 30, 15, 8, 5, 784, DateTimeKind.Utc).AddTicks(1405) });

            migrationBuilder.UpdateData(
                table: "Questions",
                keyColumn: "Id",
                keyValue: 7,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 5, 30, 15, 8, 5, 784, DateTimeKind.Utc).AddTicks(1405), new DateTime(2025, 5, 30, 15, 8, 5, 784, DateTimeKind.Utc).AddTicks(1405) });

            migrationBuilder.UpdateData(
                table: "Questions",
                keyColumn: "Id",
                keyValue: 8,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 5, 30, 15, 8, 5, 784, DateTimeKind.Utc).AddTicks(1405), new DateTime(2025, 5, 30, 15, 8, 5, 784, DateTimeKind.Utc).AddTicks(1405) });

            migrationBuilder.UpdateData(
                table: "Questions",
                keyColumn: "Id",
                keyValue: 9,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 5, 30, 15, 8, 5, 784, DateTimeKind.Utc).AddTicks(1405), new DateTime(2025, 5, 30, 15, 8, 5, 784, DateTimeKind.Utc).AddTicks(1405) });

            migrationBuilder.UpdateData(
                table: "Questions",
                keyColumn: "Id",
                keyValue: 10,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 5, 30, 15, 8, 5, 784, DateTimeKind.Utc).AddTicks(1405), new DateTime(2025, 5, 30, 15, 8, 5, 784, DateTimeKind.Utc).AddTicks(1405) });

            migrationBuilder.CreateIndex(
                name: "IX_Feedback360Instances_CreatedAt",
                table: "Feedback360Instances",
                column: "CreatedAt");

            migrationBuilder.CreateIndex(
                name: "IX_Feedback360Instances_InitiatedByCoachId",
                table: "Feedback360Instances",
                column: "InitiatedByCoachId");

            migrationBuilder.CreateIndex(
                name: "IX_Feedback360Instances_SubjectUserId",
                table: "Feedback360Instances",
                column: "SubjectUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Feedback360Raters_Feedback360InstanceId",
                table: "Feedback360Raters",
                column: "Feedback360InstanceId");

            migrationBuilder.CreateIndex(
                name: "IX_Feedback360Raters_RaterUserId",
                table: "Feedback360Raters",
                column: "RaterUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Feedback360Raters_UniqueResponseToken",
                table: "Feedback360Raters",
                column: "UniqueResponseToken",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Feedback360ResponseOpens_Feedback360RaterId_QuestionCode",
                table: "Feedback360ResponseOpens",
                columns: new[] { "Feedback360RaterId", "QuestionCode" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Feedback360ResponseScales_Feedback360RaterId_QuestionCode",
                table: "Feedback360ResponseScales",
                columns: new[] { "Feedback360RaterId", "QuestionCode" },
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Feedback360ResponseOpens");

            migrationBuilder.DropTable(
                name: "Feedback360ResponseScales");

            migrationBuilder.DropTable(
                name: "Feedback360Raters");

            migrationBuilder.DropTable(
                name: "Feedback360Instances");

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
        }
    }
}
