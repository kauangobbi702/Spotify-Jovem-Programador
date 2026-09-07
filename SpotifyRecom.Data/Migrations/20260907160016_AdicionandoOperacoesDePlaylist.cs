using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SpotifyRecom.Data.Migrations
{
    /// <inheritdoc />
    public partial class AdicionandoOperacoesDePlaylist : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "MusicasPlaylists",
                columns: table => new
                {
                    UsuarioId = table.Column<int>(type: "int", nullable: false),
                    MidiaId = table.Column<int>(type: "int", nullable: false),
                    PlaylistId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MusicasPlaylists", x => new { x.UsuarioId, x.MidiaId, x.PlaylistId });
                    table.ForeignKey(
                        name: "FK_MusicasPlaylists_Midias_MidiaId",
                        column: x => x.MidiaId,
                        principalTable: "Midias",
                        principalColumn: "IdMidia",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MusicasPlaylists_Playlists_PlaylistId",
                        column: x => x.PlaylistId,
                        principalTable: "Playlists",
                        principalColumn: "IdPlaylist",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MusicasPlaylists_Usuarios_UsuarioId",
                        column: x => x.UsuarioId,
                        principalTable: "Usuarios",
                        principalColumn: "IdUsuario",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_MusicasPlaylists_MidiaId",
                table: "MusicasPlaylists",
                column: "MidiaId");

            migrationBuilder.CreateIndex(
                name: "IX_MusicasPlaylists_PlaylistId",
                table: "MusicasPlaylists",
                column: "PlaylistId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MusicasPlaylists");
        }
    }
}
