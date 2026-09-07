using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SpotifyRecom.Data.Migrations
{
    /// <inheritdoc />
    public partial class CriandoBD : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Albums",
                columns: table => new
                {
                    IdAlbum = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Nome = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ArtistaId = table.Column<int>(type: "int", nullable: false),
                    MidiaId = table.Column<int>(type: "int", nullable: false),
                    AnoLancamento = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Albums", x => x.IdAlbum);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Artistas",
                columns: table => new
                {
                    IdArtista = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Nome = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Artistas", x => x.IdArtista);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Generos",
                columns: table => new
                {
                    IdGenero = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Nome = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Generos", x => x.IdGenero);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Planos",
                columns: table => new
                {
                    IdPlano = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Descricao = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Valor = table.Column<decimal>(type: "decimal(65,30)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Planos", x => x.IdPlano);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Midias",
                columns: table => new
                {
                    IdMidia = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Titulo = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ArtistaId = table.Column<int>(type: "int", nullable: false),
                    AlbumId = table.Column<int>(type: "int", nullable: false),
                    Duracao = table.Column<TimeSpan>(type: "time(6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Midias", x => x.IdMidia);
                    table.ForeignKey(
                        name: "FK_Midias_Albums_AlbumId",
                        column: x => x.AlbumId,
                        principalTable: "Albums",
                        principalColumn: "IdAlbum",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "AlbumArtista",
                columns: table => new
                {
                    AlbunsIdAlbum = table.Column<int>(type: "int", nullable: false),
                    ArtistasIdArtista = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AlbumArtista", x => new { x.AlbunsIdAlbum, x.ArtistasIdArtista });
                    table.ForeignKey(
                        name: "FK_AlbumArtista_Albums_AlbunsIdAlbum",
                        column: x => x.AlbunsIdAlbum,
                        principalTable: "Albums",
                        principalColumn: "IdAlbum",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AlbumArtista_Artistas_ArtistasIdArtista",
                        column: x => x.ArtistasIdArtista,
                        principalTable: "Artistas",
                        principalColumn: "IdArtista",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "ArtistaGenero",
                columns: table => new
                {
                    ArtistasIdArtista = table.Column<int>(type: "int", nullable: false),
                    GenerosArtistaIdGenero = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ArtistaGenero", x => new { x.ArtistasIdArtista, x.GenerosArtistaIdGenero });
                    table.ForeignKey(
                        name: "FK_ArtistaGenero_Artistas_ArtistasIdArtista",
                        column: x => x.ArtistasIdArtista,
                        principalTable: "Artistas",
                        principalColumn: "IdArtista",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ArtistaGenero_Generos_GenerosArtistaIdGenero",
                        column: x => x.GenerosArtistaIdGenero,
                        principalTable: "Generos",
                        principalColumn: "IdGenero",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Usuarios",
                columns: table => new
                {
                    IdUsuario = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Nome = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Email = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Senha = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    PlanoId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Usuarios", x => x.IdUsuario);
                    table.ForeignKey(
                        name: "FK_Usuarios_Planos_PlanoId",
                        column: x => x.PlanoId,
                        principalTable: "Planos",
                        principalColumn: "IdPlano",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "ArtistaMidia",
                columns: table => new
                {
                    ArtistasIdArtista = table.Column<int>(type: "int", nullable: false),
                    MidiasIdMidia = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ArtistaMidia", x => new { x.ArtistasIdArtista, x.MidiasIdMidia });
                    table.ForeignKey(
                        name: "FK_ArtistaMidia_Artistas_ArtistasIdArtista",
                        column: x => x.ArtistasIdArtista,
                        principalTable: "Artistas",
                        principalColumn: "IdArtista",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ArtistaMidia_Midias_MidiasIdMidia",
                        column: x => x.MidiasIdMidia,
                        principalTable: "Midias",
                        principalColumn: "IdMidia",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "GeneroMidia",
                columns: table => new
                {
                    GenerosMidiaIdGenero = table.Column<int>(type: "int", nullable: false),
                    MidiasIdMidia = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GeneroMidia", x => new { x.GenerosMidiaIdGenero, x.MidiasIdMidia });
                    table.ForeignKey(
                        name: "FK_GeneroMidia_Generos_GenerosMidiaIdGenero",
                        column: x => x.GenerosMidiaIdGenero,
                        principalTable: "Generos",
                        principalColumn: "IdGenero",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_GeneroMidia_Midias_MidiasIdMidia",
                        column: x => x.MidiasIdMidia,
                        principalTable: "Midias",
                        principalColumn: "IdMidia",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Bibliotecas",
                columns: table => new
                {
                    IdBiblioteca = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    UsuarioId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Bibliotecas", x => x.IdBiblioteca);
                    table.ForeignKey(
                        name: "FK_Bibliotecas_Usuarios_UsuarioId",
                        column: x => x.UsuarioId,
                        principalTable: "Usuarios",
                        principalColumn: "IdUsuario",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Playlists",
                columns: table => new
                {
                    IdPlaylist = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    NomePlaylist = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    UsuarioId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Playlists", x => x.IdPlaylist);
                    table.ForeignKey(
                        name: "FK_Playlists_Usuarios_UsuarioId",
                        column: x => x.UsuarioId,
                        principalTable: "Usuarios",
                        principalColumn: "IdUsuario",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "BibliotecaMidia",
                columns: table => new
                {
                    BibliotecasIdBiblioteca = table.Column<int>(type: "int", nullable: false),
                    MidiasIdMidia = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BibliotecaMidia", x => new { x.BibliotecasIdBiblioteca, x.MidiasIdMidia });
                    table.ForeignKey(
                        name: "FK_BibliotecaMidia_Bibliotecas_BibliotecasIdBiblioteca",
                        column: x => x.BibliotecasIdBiblioteca,
                        principalTable: "Bibliotecas",
                        principalColumn: "IdBiblioteca",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BibliotecaMidia_Midias_MidiasIdMidia",
                        column: x => x.MidiasIdMidia,
                        principalTable: "Midias",
                        principalColumn: "IdMidia",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "BibliotecaPlaylist",
                columns: table => new
                {
                    BibliotecasIdBiblioteca = table.Column<int>(type: "int", nullable: false),
                    PlaylistsIdPlaylist = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BibliotecaPlaylist", x => new { x.BibliotecasIdBiblioteca, x.PlaylistsIdPlaylist });
                    table.ForeignKey(
                        name: "FK_BibliotecaPlaylist_Bibliotecas_BibliotecasIdBiblioteca",
                        column: x => x.BibliotecasIdBiblioteca,
                        principalTable: "Bibliotecas",
                        principalColumn: "IdBiblioteca",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BibliotecaPlaylist_Playlists_PlaylistsIdPlaylist",
                        column: x => x.PlaylistsIdPlaylist,
                        principalTable: "Playlists",
                        principalColumn: "IdPlaylist",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "MidiaPlaylist",
                columns: table => new
                {
                    MidiasIdMidia = table.Column<int>(type: "int", nullable: false),
                    PlaylistsIdPlaylist = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MidiaPlaylist", x => new { x.MidiasIdMidia, x.PlaylistsIdPlaylist });
                    table.ForeignKey(
                        name: "FK_MidiaPlaylist_Midias_MidiasIdMidia",
                        column: x => x.MidiasIdMidia,
                        principalTable: "Midias",
                        principalColumn: "IdMidia",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MidiaPlaylist_Playlists_PlaylistsIdPlaylist",
                        column: x => x.PlaylistsIdPlaylist,
                        principalTable: "Playlists",
                        principalColumn: "IdPlaylist",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_AlbumArtista_ArtistasIdArtista",
                table: "AlbumArtista",
                column: "ArtistasIdArtista");

            migrationBuilder.CreateIndex(
                name: "IX_ArtistaGenero_GenerosArtistaIdGenero",
                table: "ArtistaGenero",
                column: "GenerosArtistaIdGenero");

            migrationBuilder.CreateIndex(
                name: "IX_ArtistaMidia_MidiasIdMidia",
                table: "ArtistaMidia",
                column: "MidiasIdMidia");

            migrationBuilder.CreateIndex(
                name: "IX_BibliotecaMidia_MidiasIdMidia",
                table: "BibliotecaMidia",
                column: "MidiasIdMidia");

            migrationBuilder.CreateIndex(
                name: "IX_BibliotecaPlaylist_PlaylistsIdPlaylist",
                table: "BibliotecaPlaylist",
                column: "PlaylistsIdPlaylist");

            migrationBuilder.CreateIndex(
                name: "IX_Bibliotecas_UsuarioId",
                table: "Bibliotecas",
                column: "UsuarioId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_GeneroMidia_MidiasIdMidia",
                table: "GeneroMidia",
                column: "MidiasIdMidia");

            migrationBuilder.CreateIndex(
                name: "IX_MidiaPlaylist_PlaylistsIdPlaylist",
                table: "MidiaPlaylist",
                column: "PlaylistsIdPlaylist");

            migrationBuilder.CreateIndex(
                name: "IX_Midias_AlbumId",
                table: "Midias",
                column: "AlbumId");

            migrationBuilder.CreateIndex(
                name: "IX_Playlists_UsuarioId",
                table: "Playlists",
                column: "UsuarioId");

            migrationBuilder.CreateIndex(
                name: "IX_Usuarios_PlanoId",
                table: "Usuarios",
                column: "PlanoId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AlbumArtista");

            migrationBuilder.DropTable(
                name: "ArtistaGenero");

            migrationBuilder.DropTable(
                name: "ArtistaMidia");

            migrationBuilder.DropTable(
                name: "BibliotecaMidia");

            migrationBuilder.DropTable(
                name: "BibliotecaPlaylist");

            migrationBuilder.DropTable(
                name: "GeneroMidia");

            migrationBuilder.DropTable(
                name: "MidiaPlaylist");

            migrationBuilder.DropTable(
                name: "Artistas");

            migrationBuilder.DropTable(
                name: "Bibliotecas");

            migrationBuilder.DropTable(
                name: "Generos");

            migrationBuilder.DropTable(
                name: "Midias");

            migrationBuilder.DropTable(
                name: "Playlists");

            migrationBuilder.DropTable(
                name: "Albums");

            migrationBuilder.DropTable(
                name: "Usuarios");

            migrationBuilder.DropTable(
                name: "Planos");
        }
    }
}
