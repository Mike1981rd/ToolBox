using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace ToolBox.Migrations
{
    /// <inheritdoc />
    public partial class AddWebsiteConfiguration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "WebsiteConfiguration",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    SiteEmail = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    SitePhone = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    SiteAddress = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    FooterMessage = table.Column<string>(type: "character varying(1000)", maxLength: 1000, nullable: true),
                    FacebookUrl = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: true),
                    TwitterUrl = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: true),
                    GoogleUrl = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: true),
                    LinkedInUrl = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: true),
                    YouTubeUrl = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: true),
                    InstagramUrl = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: true),
                    TelegramUrl = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: true),
                    TikTokUrl = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: true),
                    DiscordUrl = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: true),
                    RedditUrl = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: true),
                    LogoPath = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    SiteName = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: true),
                    SiteDescription = table.Column<string>(type: "character varying(1000)", maxLength: 1000, nullable: true),
                    DefaultTimeZone = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    DefaultLanguage = table.Column<string>(type: "character varying(10)", maxLength: 10, nullable: true),
                    LastUpdated = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    LastUpdatedByUserId = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WebsiteConfiguration", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WebsiteConfiguration_Users_LastUpdatedByUserId",
                        column: x => x.LastUpdatedByUserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                });

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

            migrationBuilder.CreateIndex(
                name: "IX_WebsiteConfiguration_LastUpdatedByUserId",
                table: "WebsiteConfiguration",
                column: "LastUpdatedByUserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "WebsiteConfiguration");

            migrationBuilder.UpdateData(
                table: "AreasProgreso",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2025, 5, 25, 16, 15, 4, 674, DateTimeKind.Utc).AddTicks(4536));

            migrationBuilder.UpdateData(
                table: "AreasProgreso",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2025, 5, 25, 16, 15, 4, 674, DateTimeKind.Utc).AddTicks(4536));

            migrationBuilder.UpdateData(
                table: "AreasProgreso",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedAt",
                value: new DateTime(2025, 5, 25, 16, 15, 4, 674, DateTimeKind.Utc).AddTicks(4536));

            migrationBuilder.UpdateData(
                table: "AreasProgreso",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedAt",
                value: new DateTime(2025, 5, 25, 16, 15, 4, 674, DateTimeKind.Utc).AddTicks(4536));

            migrationBuilder.UpdateData(
                table: "AreasProgreso",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreatedAt",
                value: new DateTime(2025, 5, 25, 16, 15, 4, 674, DateTimeKind.Utc).AddTicks(4536));

            migrationBuilder.UpdateData(
                table: "CategoriasHabitos",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "FechaCreacion" },
                values: new object[] { new DateTime(2025, 5, 25, 16, 15, 4, 675, DateTimeKind.Utc).AddTicks(9966), new DateTime(2025, 5, 25, 16, 15, 4, 675, DateTimeKind.Utc).AddTicks(9970) });

            migrationBuilder.UpdateData(
                table: "CategoriasHabitos",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedAt", "FechaCreacion" },
                values: new object[] { new DateTime(2025, 5, 25, 16, 15, 4, 675, DateTimeKind.Utc).AddTicks(9966), new DateTime(2025, 5, 25, 16, 15, 4, 675, DateTimeKind.Utc).AddTicks(9978) });

            migrationBuilder.UpdateData(
                table: "CategoriasHabitos",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedAt", "FechaCreacion" },
                values: new object[] { new DateTime(2025, 5, 25, 16, 15, 4, 675, DateTimeKind.Utc).AddTicks(9966), new DateTime(2025, 5, 25, 16, 15, 4, 675, DateTimeKind.Utc).AddTicks(9980) });

            migrationBuilder.UpdateData(
                table: "CategoriasHabitos",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "CreatedAt", "FechaCreacion" },
                values: new object[] { new DateTime(2025, 5, 25, 16, 15, 4, 675, DateTimeKind.Utc).AddTicks(9966), new DateTime(2025, 5, 25, 16, 15, 4, 675, DateTimeKind.Utc).AddTicks(9982) });

            migrationBuilder.UpdateData(
                table: "CategoriasHabitos",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "CreatedAt", "FechaCreacion" },
                values: new object[] { new DateTime(2025, 5, 25, 16, 15, 4, 675, DateTimeKind.Utc).AddTicks(9966), new DateTime(2025, 5, 25, 16, 15, 4, 675, DateTimeKind.Utc).AddTicks(9985) });

            migrationBuilder.UpdateData(
                table: "CategoriasHabitos",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "CreatedAt", "FechaCreacion" },
                values: new object[] { new DateTime(2025, 5, 25, 16, 15, 4, 675, DateTimeKind.Utc).AddTicks(9966), new DateTime(2025, 5, 25, 16, 15, 4, 675, DateTimeKind.Utc).AddTicks(9986) });

            migrationBuilder.UpdateData(
                table: "CategoriasHabitos",
                keyColumn: "Id",
                keyValue: 7,
                columns: new[] { "CreatedAt", "FechaCreacion" },
                values: new object[] { new DateTime(2025, 5, 25, 16, 15, 4, 675, DateTimeKind.Utc).AddTicks(9966), new DateTime(2025, 5, 25, 16, 15, 4, 675, DateTimeKind.Utc).AddTicks(9988) });

            migrationBuilder.UpdateData(
                table: "CategoriasHabitos",
                keyColumn: "Id",
                keyValue: 8,
                columns: new[] { "CreatedAt", "FechaCreacion" },
                values: new object[] { new DateTime(2025, 5, 25, 16, 15, 4, 675, DateTimeKind.Utc).AddTicks(9966), new DateTime(2025, 5, 25, 16, 15, 4, 675, DateTimeKind.Utc).AddTicks(9990) });

            migrationBuilder.UpdateData(
                table: "CategoriasProgreso",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2025, 5, 25, 16, 15, 4, 674, DateTimeKind.Utc).AddTicks(4536));

            migrationBuilder.UpdateData(
                table: "CategoriasProgreso",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2025, 5, 25, 16, 15, 4, 674, DateTimeKind.Utc).AddTicks(4536));

            migrationBuilder.UpdateData(
                table: "CategoriasProgreso",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedAt",
                value: new DateTime(2025, 5, 25, 16, 15, 4, 674, DateTimeKind.Utc).AddTicks(4536));

            migrationBuilder.UpdateData(
                table: "CategoriasProgreso",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedAt",
                value: new DateTime(2025, 5, 25, 16, 15, 4, 674, DateTimeKind.Utc).AddTicks(4536));

            migrationBuilder.UpdateData(
                table: "CategoriasProgreso",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreatedAt",
                value: new DateTime(2025, 5, 25, 16, 15, 4, 674, DateTimeKind.Utc).AddTicks(4536));

            migrationBuilder.UpdateData(
                table: "CategoriasProgreso",
                keyColumn: "Id",
                keyValue: 6,
                column: "CreatedAt",
                value: new DateTime(2025, 5, 25, 16, 15, 4, 674, DateTimeKind.Utc).AddTicks(4536));

            migrationBuilder.UpdateData(
                table: "CategoriasProgreso",
                keyColumn: "Id",
                keyValue: 7,
                column: "CreatedAt",
                value: new DateTime(2025, 5, 25, 16, 15, 4, 674, DateTimeKind.Utc).AddTicks(4536));

            migrationBuilder.UpdateData(
                table: "CategoriasProgreso",
                keyColumn: "Id",
                keyValue: 8,
                column: "CreatedAt",
                value: new DateTime(2025, 5, 25, 16, 15, 4, 674, DateTimeKind.Utc).AddTicks(4536));

            migrationBuilder.UpdateData(
                table: "CategoriasProgreso",
                keyColumn: "Id",
                keyValue: 9,
                column: "CreatedAt",
                value: new DateTime(2025, 5, 25, 16, 15, 4, 674, DateTimeKind.Utc).AddTicks(4536));

            migrationBuilder.UpdateData(
                table: "CategoriasProgreso",
                keyColumn: "Id",
                keyValue: 10,
                column: "CreatedAt",
                value: new DateTime(2025, 5, 25, 16, 15, 4, 674, DateTimeKind.Utc).AddTicks(4536));

            migrationBuilder.UpdateData(
                table: "CategoriasProgreso",
                keyColumn: "Id",
                keyValue: 11,
                column: "CreatedAt",
                value: new DateTime(2025, 5, 25, 16, 15, 4, 674, DateTimeKind.Utc).AddTicks(4536));

            migrationBuilder.UpdateData(
                table: "CategoriasProgreso",
                keyColumn: "Id",
                keyValue: 12,
                column: "CreatedAt",
                value: new DateTime(2025, 5, 25, 16, 15, 4, 674, DateTimeKind.Utc).AddTicks(4536));

            migrationBuilder.UpdateData(
                table: "CategoriasProgreso",
                keyColumn: "Id",
                keyValue: 13,
                column: "CreatedAt",
                value: new DateTime(2025, 5, 25, 16, 15, 4, 674, DateTimeKind.Utc).AddTicks(4536));

            migrationBuilder.UpdateData(
                table: "CategoriasProgreso",
                keyColumn: "Id",
                keyValue: 14,
                column: "CreatedAt",
                value: new DateTime(2025, 5, 25, 16, 15, 4, 674, DateTimeKind.Utc).AddTicks(4536));

            migrationBuilder.UpdateData(
                table: "CategoriasProgreso",
                keyColumn: "Id",
                keyValue: 15,
                column: "CreatedAt",
                value: new DateTime(2025, 5, 25, 16, 15, 4, 674, DateTimeKind.Utc).AddTicks(4536));

            migrationBuilder.UpdateData(
                table: "FrecuenciasHabitos",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "FechaCreacion" },
                values: new object[] { new DateTime(2025, 5, 25, 16, 15, 4, 675, DateTimeKind.Utc).AddTicks(9966), new DateTime(2025, 5, 25, 16, 15, 4, 675, DateTimeKind.Utc).AddTicks(9994) });

            migrationBuilder.UpdateData(
                table: "FrecuenciasHabitos",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedAt", "FechaCreacion" },
                values: new object[] { new DateTime(2025, 5, 25, 16, 15, 4, 675, DateTimeKind.Utc).AddTicks(9966), new DateTime(2025, 5, 25, 16, 15, 4, 675, DateTimeKind.Utc).AddTicks(9999) });

            migrationBuilder.UpdateData(
                table: "FrecuenciasHabitos",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedAt", "FechaCreacion" },
                values: new object[] { new DateTime(2025, 5, 25, 16, 15, 4, 675, DateTimeKind.Utc).AddTicks(9966), new DateTime(2025, 5, 25, 16, 15, 4, 676, DateTimeKind.Utc).AddTicks(1) });

            migrationBuilder.UpdateData(
                table: "FrecuenciasHabitos",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "CreatedAt", "FechaCreacion" },
                values: new object[] { new DateTime(2025, 5, 25, 16, 15, 4, 675, DateTimeKind.Utc).AddTicks(9966), new DateTime(2025, 5, 25, 16, 15, 4, 676, DateTimeKind.Utc).AddTicks(3) });

            migrationBuilder.UpdateData(
                table: "FrecuenciasHabitos",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "CreatedAt", "FechaCreacion" },
                values: new object[] { new DateTime(2025, 5, 25, 16, 15, 4, 675, DateTimeKind.Utc).AddTicks(9966), new DateTime(2025, 5, 25, 16, 15, 4, 676, DateTimeKind.Utc).AddTicks(4) });

            migrationBuilder.UpdateData(
                table: "FrecuenciasHabitos",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "CreatedAt", "FechaCreacion" },
                values: new object[] { new DateTime(2025, 5, 25, 16, 15, 4, 675, DateTimeKind.Utc).AddTicks(9966), new DateTime(2025, 5, 25, 16, 15, 4, 676, DateTimeKind.Utc).AddTicks(6) });

            migrationBuilder.UpdateData(
                table: "LifeAreas",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 5, 25, 16, 15, 4, 671, DateTimeKind.Utc).AddTicks(285), new DateTime(2025, 5, 25, 16, 15, 4, 671, DateTimeKind.Utc).AddTicks(285) });

            migrationBuilder.UpdateData(
                table: "LifeAreas",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 5, 25, 16, 15, 4, 671, DateTimeKind.Utc).AddTicks(285), new DateTime(2025, 5, 25, 16, 15, 4, 671, DateTimeKind.Utc).AddTicks(285) });

            migrationBuilder.UpdateData(
                table: "LifeAreas",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 5, 25, 16, 15, 4, 671, DateTimeKind.Utc).AddTicks(285), new DateTime(2025, 5, 25, 16, 15, 4, 671, DateTimeKind.Utc).AddTicks(285) });

            migrationBuilder.UpdateData(
                table: "LifeAreas",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 5, 25, 16, 15, 4, 671, DateTimeKind.Utc).AddTicks(285), new DateTime(2025, 5, 25, 16, 15, 4, 671, DateTimeKind.Utc).AddTicks(285) });

            migrationBuilder.UpdateData(
                table: "LifeAreas",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 5, 25, 16, 15, 4, 671, DateTimeKind.Utc).AddTicks(285), new DateTime(2025, 5, 25, 16, 15, 4, 671, DateTimeKind.Utc).AddTicks(285) });

            migrationBuilder.UpdateData(
                table: "LifeAreas",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 5, 25, 16, 15, 4, 671, DateTimeKind.Utc).AddTicks(285), new DateTime(2025, 5, 25, 16, 15, 4, 671, DateTimeKind.Utc).AddTicks(285) });

            migrationBuilder.UpdateData(
                table: "LifeAreas",
                keyColumn: "Id",
                keyValue: 7,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 5, 25, 16, 15, 4, 671, DateTimeKind.Utc).AddTicks(285), new DateTime(2025, 5, 25, 16, 15, 4, 671, DateTimeKind.Utc).AddTicks(285) });

            migrationBuilder.UpdateData(
                table: "LifeAreas",
                keyColumn: "Id",
                keyValue: 8,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 5, 25, 16, 15, 4, 671, DateTimeKind.Utc).AddTicks(285), new DateTime(2025, 5, 25, 16, 15, 4, 671, DateTimeKind.Utc).AddTicks(285) });

            migrationBuilder.UpdateData(
                table: "LifeAreas",
                keyColumn: "Id",
                keyValue: 9,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 5, 25, 16, 15, 4, 671, DateTimeKind.Utc).AddTicks(285), new DateTime(2025, 5, 25, 16, 15, 4, 671, DateTimeKind.Utc).AddTicks(285) });

            migrationBuilder.UpdateData(
                table: "LifeAreas",
                keyColumn: "Id",
                keyValue: 10,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 5, 25, 16, 15, 4, 671, DateTimeKind.Utc).AddTicks(285), new DateTime(2025, 5, 25, 16, 15, 4, 671, DateTimeKind.Utc).AddTicks(285) });

            migrationBuilder.UpdateData(
                table: "Questions",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 5, 25, 16, 15, 4, 674, DateTimeKind.Utc).AddTicks(3965), new DateTime(2025, 5, 25, 16, 15, 4, 674, DateTimeKind.Utc).AddTicks(3965) });

            migrationBuilder.UpdateData(
                table: "Questions",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 5, 25, 16, 15, 4, 674, DateTimeKind.Utc).AddTicks(3965), new DateTime(2025, 5, 25, 16, 15, 4, 674, DateTimeKind.Utc).AddTicks(3965) });

            migrationBuilder.UpdateData(
                table: "Questions",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 5, 25, 16, 15, 4, 674, DateTimeKind.Utc).AddTicks(3965), new DateTime(2025, 5, 25, 16, 15, 4, 674, DateTimeKind.Utc).AddTicks(3965) });

            migrationBuilder.UpdateData(
                table: "Questions",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 5, 25, 16, 15, 4, 674, DateTimeKind.Utc).AddTicks(3965), new DateTime(2025, 5, 25, 16, 15, 4, 674, DateTimeKind.Utc).AddTicks(3965) });

            migrationBuilder.UpdateData(
                table: "Questions",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 5, 25, 16, 15, 4, 674, DateTimeKind.Utc).AddTicks(3965), new DateTime(2025, 5, 25, 16, 15, 4, 674, DateTimeKind.Utc).AddTicks(3965) });

            migrationBuilder.UpdateData(
                table: "Questions",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 5, 25, 16, 15, 4, 674, DateTimeKind.Utc).AddTicks(3965), new DateTime(2025, 5, 25, 16, 15, 4, 674, DateTimeKind.Utc).AddTicks(3965) });

            migrationBuilder.UpdateData(
                table: "Questions",
                keyColumn: "Id",
                keyValue: 7,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 5, 25, 16, 15, 4, 674, DateTimeKind.Utc).AddTicks(3965), new DateTime(2025, 5, 25, 16, 15, 4, 674, DateTimeKind.Utc).AddTicks(3965) });

            migrationBuilder.UpdateData(
                table: "Questions",
                keyColumn: "Id",
                keyValue: 8,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 5, 25, 16, 15, 4, 674, DateTimeKind.Utc).AddTicks(3965), new DateTime(2025, 5, 25, 16, 15, 4, 674, DateTimeKind.Utc).AddTicks(3965) });

            migrationBuilder.UpdateData(
                table: "Questions",
                keyColumn: "Id",
                keyValue: 9,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 5, 25, 16, 15, 4, 674, DateTimeKind.Utc).AddTicks(3965), new DateTime(2025, 5, 25, 16, 15, 4, 674, DateTimeKind.Utc).AddTicks(3965) });

            migrationBuilder.UpdateData(
                table: "Questions",
                keyColumn: "Id",
                keyValue: 10,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 5, 25, 16, 15, 4, 674, DateTimeKind.Utc).AddTicks(3965), new DateTime(2025, 5, 25, 16, 15, 4, 674, DateTimeKind.Utc).AddTicks(3965) });
        }
    }
}
