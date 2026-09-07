using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SpotifyRecom.Data.Migrations
{
    /// <inheritdoc />
    public partial class AdicionandoTabelasDeRelacionamento : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ArtistasSeguidos",
                columns: table => new
                {
                    UsuarioId = table.Column<int>(type: "int", nullable: false),
                    ArtistaId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ArtistasSeguidos", x => new { x.UsuarioId, x.ArtistaId });
                    table.ForeignKey(
                        name: "FK_ArtistasSeguidos_Artistas_ArtistaId",
                        column: x => x.ArtistaId,
                        principalTable: "Artistas",
                        principalColumn: "IdArtista",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ArtistasSeguidos_Usuarios_UsuarioId",
                        column: x => x.UsuarioId,
                        principalTable: "Usuarios",
                        principalColumn: "IdUsuario",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "MusicasCurtidas",
                columns: table => new
                {
                    UsuarioId = table.Column<int>(type: "int", nullable: false),
                    MidiaId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MusicasCurtidas", x => new { x.UsuarioId, x.MidiaId });
                    table.ForeignKey(
                        name: "FK_MusicasCurtidas_Midias_MidiaId",
                        column: x => x.MidiaId,
                        principalTable: "Midias",
                        principalColumn: "IdMidia",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MusicasCurtidas_Usuarios_UsuarioId",
                        column: x => x.UsuarioId,
                        principalTable: "Usuarios",
                        principalColumn: "IdUsuario",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_ArtistasSeguidos_ArtistaId",
                table: "ArtistasSeguidos",
                column: "ArtistaId");

            migrationBuilder.CreateIndex(
                name: "IX_MusicasCurtidas_MidiaId",
                table: "MusicasCurtidas",
                column: "MidiaId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ArtistasSeguidos");

            migrationBuilder.DropTable(
                name: "MusicasCurtidas");
        }
    }
}
