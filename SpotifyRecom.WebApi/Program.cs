using System.Text.Json.Serialization;
using SpotifyRecom.Data;
using SpotifyRecom.Model;
using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);

builder.Services.ConfigureHttpJsonOptions(options =>
{
    options.SerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
});

var app = builder.Build();

app.MapGet("/", () => "Hello World!");

app.MapGet("/spotify/planos", () =>
{
    List<Plano> planos = new ListagemEF(new SpotifyRecomContext()).ListarPlanos();
    return planos;
});

app.MapGet("/spotify/artistas", () =>
{
    List<Artista> artistas = new ListagemEF(new SpotifyRecomContext()).ListarTodosArtistas();
    return artistas;
});

app.MapGet("/spotify/albuns_artista", (int artistaId) =>
{
    List<Album> albuns = new ListagemEF(new SpotifyRecomContext()).ListarAlbunsDoArtista(artistaId);
    return albuns;
});

app.MapGet("/spotify/musicas_album", (int albumId) =>
{
    List<Midia> musicas = new ListagemEF(new SpotifyRecomContext()).ListarMusicasDoAlbum(albumId);
    return musicas;
});

app.MapGet("/spotify/musicas_artista", (int artistaId) =>
{
    List<Midia> musicasArtista = new ListagemEF(new SpotifyRecomContext()).ListarMusicasDoArtista(artistaId);
    return musicasArtista;
});

app.MapGet("/spotify/artistas_seguidos", (int usuarioId) =>
{
    List<Artista> artistasSeguidos = new ListagemEF(new SpotifyRecomContext()).ListarArtistasSeguidos(usuarioId);
    return artistasSeguidos;
});

app.MapGet("/spotify/musicas_curtidas", (int usuarioId) =>
{
    List<Midia> musicasCurtidas = new ListagemEF(new SpotifyRecomContext()).ListarMusicasCurtidas(usuarioId);
    return musicasCurtidas;
});

app.MapGet("/spotify/playlists_usuario", (int usuarioId) =>
{
    List<Playlist> musicas = new ListagemEF(new SpotifyRecomContext()).ListarPlaylists(usuarioId);
    return musicas;
});

app.MapGet("/spotify/musicas_playlist", (int playlistId) =>
{
    List<Midia> musicasPlaylist = new ListagemEF(new SpotifyRecomContext()).ListarMusicasDaPlaylist(playlistId);
    return musicasPlaylist;
});

app.MapGet("/spotify/musicas_mood", (int emocaoId, int atividadeId) =>
{
    List<Midia> musicasMood = new ListagemEF(new SpotifyRecomContext()).ListarMusicasPorMood(emocaoId, atividadeId);
    return musicasMood;
});

app.Run();
