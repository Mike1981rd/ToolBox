using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace ToolBox.Migrations
{
    /// <inheritdoc />
    public partial class AddVideoManagementSystem : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Videos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Titulo = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    DescripcionHTML = table.Column<string>(type: "text", nullable: true),
                    AutorId = table.Column<int>(type: "integer", nullable: true),
                    TemaId = table.Column<int>(type: "integer", nullable: true),
                    TipoFuenteVideo = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    UrlVideoExterno = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    NombreArchivoVideoSubido = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    PathVideoSubido = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    Duracion = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: true),
                    MetaTituloSEO = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: true),
                    MetaDescripcionSEO = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    PalabrasClaveSEO = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    EsDestacado = table.Column<bool>(type: "boolean", nullable: false),
                    EstadoVideo = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    FechaSubida = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UsuarioCreadorId = table.Column<int>(type: "integer", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Videos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Videos_Topics_TemaId",
                        column: x => x.TemaId,
                        principalTable: "Topics",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_Videos_Users_AutorId",
                        column: x => x.AutorId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_Videos_Users_UsuarioCreadorId",
                        column: x => x.UsuarioCreadorId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                });

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

            migrationBuilder.CreateIndex(
                name: "IX_Videos_AutorId",
                table: "Videos",
                column: "AutorId");

            migrationBuilder.CreateIndex(
                name: "IX_Videos_EstadoVideo",
                table: "Videos",
                column: "EstadoVideo");

            migrationBuilder.CreateIndex(
                name: "IX_Videos_TemaId",
                table: "Videos",
                column: "TemaId");

            migrationBuilder.CreateIndex(
                name: "IX_Videos_Titulo",
                table: "Videos",
                column: "Titulo");

            migrationBuilder.CreateIndex(
                name: "IX_Videos_UsuarioCreadorId",
                table: "Videos",
                column: "UsuarioCreadorId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Videos");

            migrationBuilder.UpdateData(
                table: "AreasProgreso",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2025, 5, 25, 14, 44, 52, 100, DateTimeKind.Utc).AddTicks(5570));

            migrationBuilder.UpdateData(
                table: "AreasProgreso",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2025, 5, 25, 14, 44, 52, 100, DateTimeKind.Utc).AddTicks(5570));

            migrationBuilder.UpdateData(
                table: "AreasProgreso",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedAt",
                value: new DateTime(2025, 5, 25, 14, 44, 52, 100, DateTimeKind.Utc).AddTicks(5570));

            migrationBuilder.UpdateData(
                table: "AreasProgreso",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedAt",
                value: new DateTime(2025, 5, 25, 14, 44, 52, 100, DateTimeKind.Utc).AddTicks(5570));

            migrationBuilder.UpdateData(
                table: "AreasProgreso",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreatedAt",
                value: new DateTime(2025, 5, 25, 14, 44, 52, 100, DateTimeKind.Utc).AddTicks(5570));

            migrationBuilder.UpdateData(
                table: "CategoriasHabitos",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "FechaCreacion" },
                values: new object[] { new DateTime(2025, 5, 25, 14, 44, 52, 101, DateTimeKind.Utc).AddTicks(6441), new DateTime(2025, 5, 25, 14, 44, 52, 101, DateTimeKind.Utc).AddTicks(6444) });

            migrationBuilder.UpdateData(
                table: "CategoriasHabitos",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedAt", "FechaCreacion" },
                values: new object[] { new DateTime(2025, 5, 25, 14, 44, 52, 101, DateTimeKind.Utc).AddTicks(6441), new DateTime(2025, 5, 25, 14, 44, 52, 101, DateTimeKind.Utc).AddTicks(6454) });

            migrationBuilder.UpdateData(
                table: "CategoriasHabitos",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedAt", "FechaCreacion" },
                values: new object[] { new DateTime(2025, 5, 25, 14, 44, 52, 101, DateTimeKind.Utc).AddTicks(6441), new DateTime(2025, 5, 25, 14, 44, 52, 101, DateTimeKind.Utc).AddTicks(6457) });

            migrationBuilder.UpdateData(
                table: "CategoriasHabitos",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "CreatedAt", "FechaCreacion" },
                values: new object[] { new DateTime(2025, 5, 25, 14, 44, 52, 101, DateTimeKind.Utc).AddTicks(6441), new DateTime(2025, 5, 25, 14, 44, 52, 101, DateTimeKind.Utc).AddTicks(6459) });

            migrationBuilder.UpdateData(
                table: "CategoriasHabitos",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "CreatedAt", "FechaCreacion" },
                values: new object[] { new DateTime(2025, 5, 25, 14, 44, 52, 101, DateTimeKind.Utc).AddTicks(6441), new DateTime(2025, 5, 25, 14, 44, 52, 101, DateTimeKind.Utc).AddTicks(6461) });

            migrationBuilder.UpdateData(
                table: "CategoriasHabitos",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "CreatedAt", "FechaCreacion" },
                values: new object[] { new DateTime(2025, 5, 25, 14, 44, 52, 101, DateTimeKind.Utc).AddTicks(6441), new DateTime(2025, 5, 25, 14, 44, 52, 101, DateTimeKind.Utc).AddTicks(6462) });

            migrationBuilder.UpdateData(
                table: "CategoriasHabitos",
                keyColumn: "Id",
                keyValue: 7,
                columns: new[] { "CreatedAt", "FechaCreacion" },
                values: new object[] { new DateTime(2025, 5, 25, 14, 44, 52, 101, DateTimeKind.Utc).AddTicks(6441), new DateTime(2025, 5, 25, 14, 44, 52, 101, DateTimeKind.Utc).AddTicks(6464) });

            migrationBuilder.UpdateData(
                table: "CategoriasHabitos",
                keyColumn: "Id",
                keyValue: 8,
                columns: new[] { "CreatedAt", "FechaCreacion" },
                values: new object[] { new DateTime(2025, 5, 25, 14, 44, 52, 101, DateTimeKind.Utc).AddTicks(6441), new DateTime(2025, 5, 25, 14, 44, 52, 101, DateTimeKind.Utc).AddTicks(6466) });

            migrationBuilder.UpdateData(
                table: "CategoriasProgreso",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2025, 5, 25, 14, 44, 52, 100, DateTimeKind.Utc).AddTicks(5570));

            migrationBuilder.UpdateData(
                table: "CategoriasProgreso",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedAt",
                value: new DateTime(2025, 5, 25, 14, 44, 52, 100, DateTimeKind.Utc).AddTicks(5570));

            migrationBuilder.UpdateData(
                table: "CategoriasProgreso",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedAt",
                value: new DateTime(2025, 5, 25, 14, 44, 52, 100, DateTimeKind.Utc).AddTicks(5570));

            migrationBuilder.UpdateData(
                table: "CategoriasProgreso",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedAt",
                value: new DateTime(2025, 5, 25, 14, 44, 52, 100, DateTimeKind.Utc).AddTicks(5570));

            migrationBuilder.UpdateData(
                table: "CategoriasProgreso",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreatedAt",
                value: new DateTime(2025, 5, 25, 14, 44, 52, 100, DateTimeKind.Utc).AddTicks(5570));

            migrationBuilder.UpdateData(
                table: "CategoriasProgreso",
                keyColumn: "Id",
                keyValue: 6,
                column: "CreatedAt",
                value: new DateTime(2025, 5, 25, 14, 44, 52, 100, DateTimeKind.Utc).AddTicks(5570));

            migrationBuilder.UpdateData(
                table: "CategoriasProgreso",
                keyColumn: "Id",
                keyValue: 7,
                column: "CreatedAt",
                value: new DateTime(2025, 5, 25, 14, 44, 52, 100, DateTimeKind.Utc).AddTicks(5570));

            migrationBuilder.UpdateData(
                table: "CategoriasProgreso",
                keyColumn: "Id",
                keyValue: 8,
                column: "CreatedAt",
                value: new DateTime(2025, 5, 25, 14, 44, 52, 100, DateTimeKind.Utc).AddTicks(5570));

            migrationBuilder.UpdateData(
                table: "CategoriasProgreso",
                keyColumn: "Id",
                keyValue: 9,
                column: "CreatedAt",
                value: new DateTime(2025, 5, 25, 14, 44, 52, 100, DateTimeKind.Utc).AddTicks(5570));

            migrationBuilder.UpdateData(
                table: "CategoriasProgreso",
                keyColumn: "Id",
                keyValue: 10,
                column: "CreatedAt",
                value: new DateTime(2025, 5, 25, 14, 44, 52, 100, DateTimeKind.Utc).AddTicks(5570));

            migrationBuilder.UpdateData(
                table: "CategoriasProgreso",
                keyColumn: "Id",
                keyValue: 11,
                column: "CreatedAt",
                value: new DateTime(2025, 5, 25, 14, 44, 52, 100, DateTimeKind.Utc).AddTicks(5570));

            migrationBuilder.UpdateData(
                table: "CategoriasProgreso",
                keyColumn: "Id",
                keyValue: 12,
                column: "CreatedAt",
                value: new DateTime(2025, 5, 25, 14, 44, 52, 100, DateTimeKind.Utc).AddTicks(5570));

            migrationBuilder.UpdateData(
                table: "CategoriasProgreso",
                keyColumn: "Id",
                keyValue: 13,
                column: "CreatedAt",
                value: new DateTime(2025, 5, 25, 14, 44, 52, 100, DateTimeKind.Utc).AddTicks(5570));

            migrationBuilder.UpdateData(
                table: "CategoriasProgreso",
                keyColumn: "Id",
                keyValue: 14,
                column: "CreatedAt",
                value: new DateTime(2025, 5, 25, 14, 44, 52, 100, DateTimeKind.Utc).AddTicks(5570));

            migrationBuilder.UpdateData(
                table: "CategoriasProgreso",
                keyColumn: "Id",
                keyValue: 15,
                column: "CreatedAt",
                value: new DateTime(2025, 5, 25, 14, 44, 52, 100, DateTimeKind.Utc).AddTicks(5570));

            migrationBuilder.UpdateData(
                table: "FrecuenciasHabitos",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "FechaCreacion" },
                values: new object[] { new DateTime(2025, 5, 25, 14, 44, 52, 101, DateTimeKind.Utc).AddTicks(6441), new DateTime(2025, 5, 25, 14, 44, 52, 101, DateTimeKind.Utc).AddTicks(6470) });

            migrationBuilder.UpdateData(
                table: "FrecuenciasHabitos",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedAt", "FechaCreacion" },
                values: new object[] { new DateTime(2025, 5, 25, 14, 44, 52, 101, DateTimeKind.Utc).AddTicks(6441), new DateTime(2025, 5, 25, 14, 44, 52, 101, DateTimeKind.Utc).AddTicks(6475) });

            migrationBuilder.UpdateData(
                table: "FrecuenciasHabitos",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedAt", "FechaCreacion" },
                values: new object[] { new DateTime(2025, 5, 25, 14, 44, 52, 101, DateTimeKind.Utc).AddTicks(6441), new DateTime(2025, 5, 25, 14, 44, 52, 101, DateTimeKind.Utc).AddTicks(6477) });

            migrationBuilder.UpdateData(
                table: "FrecuenciasHabitos",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "CreatedAt", "FechaCreacion" },
                values: new object[] { new DateTime(2025, 5, 25, 14, 44, 52, 101, DateTimeKind.Utc).AddTicks(6441), new DateTime(2025, 5, 25, 14, 44, 52, 101, DateTimeKind.Utc).AddTicks(6479) });

            migrationBuilder.UpdateData(
                table: "FrecuenciasHabitos",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "CreatedAt", "FechaCreacion" },
                values: new object[] { new DateTime(2025, 5, 25, 14, 44, 52, 101, DateTimeKind.Utc).AddTicks(6441), new DateTime(2025, 5, 25, 14, 44, 52, 101, DateTimeKind.Utc).AddTicks(6481) });

            migrationBuilder.UpdateData(
                table: "FrecuenciasHabitos",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "CreatedAt", "FechaCreacion" },
                values: new object[] { new DateTime(2025, 5, 25, 14, 44, 52, 101, DateTimeKind.Utc).AddTicks(6441), new DateTime(2025, 5, 25, 14, 44, 52, 101, DateTimeKind.Utc).AddTicks(6483) });

            migrationBuilder.UpdateData(
                table: "LifeAreas",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 5, 25, 14, 44, 52, 99, DateTimeKind.Utc).AddTicks(672), new DateTime(2025, 5, 25, 14, 44, 52, 99, DateTimeKind.Utc).AddTicks(672) });

            migrationBuilder.UpdateData(
                table: "LifeAreas",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 5, 25, 14, 44, 52, 99, DateTimeKind.Utc).AddTicks(672), new DateTime(2025, 5, 25, 14, 44, 52, 99, DateTimeKind.Utc).AddTicks(672) });

            migrationBuilder.UpdateData(
                table: "LifeAreas",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 5, 25, 14, 44, 52, 99, DateTimeKind.Utc).AddTicks(672), new DateTime(2025, 5, 25, 14, 44, 52, 99, DateTimeKind.Utc).AddTicks(672) });

            migrationBuilder.UpdateData(
                table: "LifeAreas",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 5, 25, 14, 44, 52, 99, DateTimeKind.Utc).AddTicks(672), new DateTime(2025, 5, 25, 14, 44, 52, 99, DateTimeKind.Utc).AddTicks(672) });

            migrationBuilder.UpdateData(
                table: "LifeAreas",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 5, 25, 14, 44, 52, 99, DateTimeKind.Utc).AddTicks(672), new DateTime(2025, 5, 25, 14, 44, 52, 99, DateTimeKind.Utc).AddTicks(672) });

            migrationBuilder.UpdateData(
                table: "LifeAreas",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 5, 25, 14, 44, 52, 99, DateTimeKind.Utc).AddTicks(672), new DateTime(2025, 5, 25, 14, 44, 52, 99, DateTimeKind.Utc).AddTicks(672) });

            migrationBuilder.UpdateData(
                table: "LifeAreas",
                keyColumn: "Id",
                keyValue: 7,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 5, 25, 14, 44, 52, 99, DateTimeKind.Utc).AddTicks(672), new DateTime(2025, 5, 25, 14, 44, 52, 99, DateTimeKind.Utc).AddTicks(672) });

            migrationBuilder.UpdateData(
                table: "LifeAreas",
                keyColumn: "Id",
                keyValue: 8,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 5, 25, 14, 44, 52, 99, DateTimeKind.Utc).AddTicks(672), new DateTime(2025, 5, 25, 14, 44, 52, 99, DateTimeKind.Utc).AddTicks(672) });

            migrationBuilder.UpdateData(
                table: "LifeAreas",
                keyColumn: "Id",
                keyValue: 9,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 5, 25, 14, 44, 52, 99, DateTimeKind.Utc).AddTicks(672), new DateTime(2025, 5, 25, 14, 44, 52, 99, DateTimeKind.Utc).AddTicks(672) });

            migrationBuilder.UpdateData(
                table: "LifeAreas",
                keyColumn: "Id",
                keyValue: 10,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 5, 25, 14, 44, 52, 99, DateTimeKind.Utc).AddTicks(672), new DateTime(2025, 5, 25, 14, 44, 52, 99, DateTimeKind.Utc).AddTicks(672) });

            migrationBuilder.UpdateData(
                table: "Questions",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 5, 25, 14, 44, 52, 100, DateTimeKind.Utc).AddTicks(5480), new DateTime(2025, 5, 25, 14, 44, 52, 100, DateTimeKind.Utc).AddTicks(5480) });

            migrationBuilder.UpdateData(
                table: "Questions",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 5, 25, 14, 44, 52, 100, DateTimeKind.Utc).AddTicks(5480), new DateTime(2025, 5, 25, 14, 44, 52, 100, DateTimeKind.Utc).AddTicks(5480) });

            migrationBuilder.UpdateData(
                table: "Questions",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 5, 25, 14, 44, 52, 100, DateTimeKind.Utc).AddTicks(5480), new DateTime(2025, 5, 25, 14, 44, 52, 100, DateTimeKind.Utc).AddTicks(5480) });

            migrationBuilder.UpdateData(
                table: "Questions",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 5, 25, 14, 44, 52, 100, DateTimeKind.Utc).AddTicks(5480), new DateTime(2025, 5, 25, 14, 44, 52, 100, DateTimeKind.Utc).AddTicks(5480) });

            migrationBuilder.UpdateData(
                table: "Questions",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 5, 25, 14, 44, 52, 100, DateTimeKind.Utc).AddTicks(5480), new DateTime(2025, 5, 25, 14, 44, 52, 100, DateTimeKind.Utc).AddTicks(5480) });

            migrationBuilder.UpdateData(
                table: "Questions",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 5, 25, 14, 44, 52, 100, DateTimeKind.Utc).AddTicks(5480), new DateTime(2025, 5, 25, 14, 44, 52, 100, DateTimeKind.Utc).AddTicks(5480) });

            migrationBuilder.UpdateData(
                table: "Questions",
                keyColumn: "Id",
                keyValue: 7,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 5, 25, 14, 44, 52, 100, DateTimeKind.Utc).AddTicks(5480), new DateTime(2025, 5, 25, 14, 44, 52, 100, DateTimeKind.Utc).AddTicks(5480) });

            migrationBuilder.UpdateData(
                table: "Questions",
                keyColumn: "Id",
                keyValue: 8,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 5, 25, 14, 44, 52, 100, DateTimeKind.Utc).AddTicks(5480), new DateTime(2025, 5, 25, 14, 44, 52, 100, DateTimeKind.Utc).AddTicks(5480) });

            migrationBuilder.UpdateData(
                table: "Questions",
                keyColumn: "Id",
                keyValue: 9,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 5, 25, 14, 44, 52, 100, DateTimeKind.Utc).AddTicks(5480), new DateTime(2025, 5, 25, 14, 44, 52, 100, DateTimeKind.Utc).AddTicks(5480) });

            migrationBuilder.UpdateData(
                table: "Questions",
                keyColumn: "Id",
                keyValue: 10,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 5, 25, 14, 44, 52, 100, DateTimeKind.Utc).AddTicks(5480), new DateTime(2025, 5, 25, 14, 44, 52, 100, DateTimeKind.Utc).AddTicks(5480) });
        }
    }
}
