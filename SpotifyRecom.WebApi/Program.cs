using System.Text.Json.Serialization;
using System.Security.Claims;
using SpotifyRecom.Business.Excecoes;
using SpotifyRecom.Data;
using SpotifyRecom.Model;
using SpotifyRecom.Service;
using SpotifyRecom.WebApi;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);

builder.Services.ConfigureHttpJsonOptions(options =>
{
    options.SerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
});


var signingKey = TokenService.GetSigningKey(builder.Configuration);

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
        IssuerSigningKey = signingKey,
        ClockSkew = TimeSpan.Zero
    };
});

builder.Services.AddAuthorization();

var app = builder.Build();

app.UseAuthentication();
app.UseAuthorization();

app.MapPost("/login/auth", ([FromBody] LoginRequest request) =>
{
    if (string.IsNullOrWhiteSpace(request.Email) || string.IsNullOrWhiteSpace(request.Senha))
    {
        return Results.BadRequest(new { mensagem = "Informe e-mail e senha." });
    }

    try
    {
        using var context = new SpotifyRecomContext();
        var usuario = new UsuarioService(context).ValidarLogin(request.Email, request.Senha);
        var token = new TokenService(builder.Configuration).GerarToken(usuario);
        return Results.Ok(new { token });
    }
    catch (CredenciaisInvalidasException)
    {
        return Results.Unauthorized();
    }
});

var curtidasPlaylistsUsuarioGroup = app.MapGroup("/usuario")
    .RequireAuthorization(policy => policy.RequireClaim(ClaimTypes.NameIdentifier));

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

curtidasPlaylistsUsuarioGroup.MapGet("/spotify/artistas_seguidos", (ClaimsPrincipal usuario) =>
{
    var usuarioId = ObterIdUsuario(usuario);
    List<Artista> artistasSeguidos = new ListagemEF(new SpotifyRecomContext()).ListarArtistasSeguidos(usuarioId);
    return artistasSeguidos;
});

curtidasPlaylistsUsuarioGroup.MapGet("/spotify/musicas_curtidas", (ClaimsPrincipal usuario) =>
{
    var usuarioId = ObterIdUsuario(usuario);
    List<Midia> musicasCurtidas = new ListagemEF(new SpotifyRecomContext()).ListarMusicasCurtidas(usuarioId);
    return musicasCurtidas;
});

curtidasPlaylistsUsuarioGroup.MapGet("/spotify/playlists_usuario", (ClaimsPrincipal usuario) =>
{
    var usuarioId = ObterIdUsuario(usuario);
    List<Playlist> musicas = new ListagemEF(new SpotifyRecomContext()).ListarPlaylists(usuarioId);
    return musicas;
});

curtidasPlaylistsUsuarioGroup.MapGet("/spotify/musicas_playlist", (int playlistId, ClaimsPrincipal usuario) =>
{
    var usuarioId = ObterIdUsuario(usuario);
    List<Midia> musicasPlaylist = new ListagemEF(new SpotifyRecomContext())
        .ListarMusicasDaPlaylist(playlistId, usuarioId);
    return musicasPlaylist;
});

curtidasPlaylistsUsuarioGroup.MapGet("/spotify/musicas_mood", (int emocaoId, int atividadeId) =>
{
    List<Midia> musicasMood = new ListagemEF(new SpotifyRecomContext()).ListarMusicasPorMood(emocaoId, atividadeId);
    return musicasMood;
});

app.Run();

static int ObterIdUsuario(ClaimsPrincipal usuario) =>
    int.Parse(usuario.FindFirstValue(ClaimTypes.NameIdentifier)!);
