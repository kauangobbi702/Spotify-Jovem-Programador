using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SpotifyRecom.Data.Migrations
{
    /// <inheritdoc />
    public partial class ImpedirMusicasDuplicadasNaPlaylist : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_MusicasPlaylists_PlaylistId_MidiaId",
                table: "MusicasPlaylists",
                columns: new[] { "PlaylistId", "MidiaId" },
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_MusicasPlaylists_PlaylistId_MidiaId",
                table: "MusicasPlaylists");
        }
    }
}
