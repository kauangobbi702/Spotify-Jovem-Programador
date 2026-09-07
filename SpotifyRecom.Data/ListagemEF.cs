using Microsoft.EntityFrameworkCore;
using SpotifyRecom.Model;

namespace SpotifyRecom.Data;

public sealed class ListagemEF
{
    private readonly SpotifyRecomContext _context;

    public ListagemEF(SpotifyRecomContext context)
    {
        _context = context;
    }

    public List<Plano> ListarPlanos()
    {
        return _context.Planos
            .AsNoTracking()
            .OrderBy(plano => plano.IdPlano)
            .ToList();
    }

    public List<Artista> ListarTodosArtistas()
    {
        return _context.Artistas
            .AsNoTracking()
            .OrderBy(artista => artista.Nome)
            .ToList();
    }

    public List<Album> ListarAlbunsDoArtista(int artistaId)
    {
        return _context.Albums
            .AsNoTracking()
            .Where(album => album.Artistas.Any(artista => artista.IdArtista == artistaId))
            .OrderBy(album => album.Nome)
            .ToList();
    }

    public List<Midia> ListarMusicasDoAlbum(int albumId)
    {
        return _context.Midias
            .AsNoTracking()
            .Where(midia => midia.AlbumId == albumId)
            .OrderBy(midia => midia.Titulo)
            .ToList();
    }

    public List<Midia> ListarMusicasDoArtista(int artistaId)
    {
        return _context.Midias
            .AsNoTracking()
            .Where(midia => midia.Artistas.Any(artista => artista.IdArtista == artistaId))
            .OrderBy(midia => midia.Titulo)
            .ToList();
    }

    public List<Artista> ListarArtistasSeguidos(int usuarioId)
    {
        return _context.ArtistasSeguidos
            .AsNoTracking()
            .Where(seguidor => seguidor.UsuarioId == usuarioId)
            .Select(seguidor => seguidor.Artista)
            .OrderBy(artista => artista.Nome)
            .ToList();
    }

    public List<Midia> ListarMusicasCurtidas(int usuarioId)
    {
        return _context.MusicasCurtidas
            .AsNoTracking()
            .Where(curtida => curtida.UsuarioId == usuarioId)
            .Select(curtida => curtida.Midia)
            .OrderBy(midia => midia.Titulo)
            .ToList();
    }

    public List<Playlist> ListarPlaylists(int usuarioId)
    {
        return _context.Playlists
            .AsNoTracking()
            .Where(playlist => playlist.UsuarioId == usuarioId)
            .OrderBy(playlist => playlist.NomePlaylist)
            .ToList();
    }

    public List<Midia> ListarMusicasDaPlaylist(int playlistId)
    {
        return _context.MusicasPlaylists
            .AsNoTracking()
            .Where(item => item.PlaylistId == playlistId)
            .Select(item => item.Midia)
            .OrderBy(midia => midia.Titulo)
            .ToList();
    }
}
