using System.Text.Json.Serialization;
using SpotifyRecom.Data;
using SpotifyRecom.Model;
using Microsoft.AspNetCore.Mvc;
using System.Text;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);

builder.Services.ConfigureHttpJsonOptions(options =>
{
    options.SerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
});

var jwtSettings = builder.Configuration.GetSection("JWTSettings");
        
var chaveSecretaBytes = Encoding.ASCII.GetBytes(jwtSettings["SecretKey"]);

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = "JwtBearer";
    options.DefaultChallengeScheme = "JwtBearer";
}).AddJwtBearer("JwtBearer", options =>
{
    options.RequireHttpsMetadata = false;
    options.SaveToken = true;
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = false,
        ValidateAudience = false,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(chaveSecretaBytes),
        ClockSkew = TimeSpan.Zero
    };
});

builder.Services.AddAuthorization();

var app = builder.Build();

app.UseAuthentication();
app.UseAuthorization();

app.MapPost("/login/auth", ([FromBody] Usuario usuario) =>
{
   TokenService authService = new TokenService(builder.Configuration);
   return authService.GerarToken(usuario);
});

var curtidasPlaylistsUsuarioGroup = app.MapGroup("/usuario").RequireAuthorization();

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

curtidasPlaylistsUsuarioGroup.MapGet("/spotify/artistas_seguidos", (int usuarioId) =>
{
    List<Artista> artistasSeguidos = new ListagemEF(new SpotifyRecomContext()).ListarArtistasSeguidos(usuarioId);
    return artistasSeguidos;
});

curtidasPlaylistsUsuarioGroup.MapGet("/spotify/musicas_curtidas", (int usuarioId) =>
{
    List<Midia> musicasCurtidas = new ListagemEF(new SpotifyRecomContext()).ListarMusicasCurtidas(usuarioId);
    return musicasCurtidas;
});

curtidasPlaylistsUsuarioGroup.MapGet("/spotify/playlists_usuario", (int usuarioId) =>
{
    List<Playlist> musicas = new ListagemEF(new SpotifyRecomContext()).ListarPlaylists(usuarioId);
    return musicas;
});

curtidasPlaylistsUsuarioGroup.MapGet("/spotify/musicas_playlist", (int playlistId) =>
{
    List<Midia> musicasPlaylist = new ListagemEF(new SpotifyRecomContext()).ListarMusicasDaPlaylist(playlistId);
    return musicasPlaylist;
});

curtidasPlaylistsUsuarioGroup.MapGet("/spotify/musicas_mood", (int emocaoId, int atividadeId) =>
{
    List<Midia> musicasMood = new ListagemEF(new SpotifyRecomContext()).ListarMusicasPorMood(emocaoId, atividadeId);
    return musicasMood;
});

app.Run();
