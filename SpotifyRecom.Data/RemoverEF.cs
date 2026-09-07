using Microsoft.EntityFrameworkCore;

namespace SpotifyRecom.Data;

public sealed class RemoverEF
{
    private readonly SpotifyRecomContext _context;

    public RemoverEF(SpotifyRecomContext context)
    {
        _context = context;
    }

    public bool RemoverUsuarioSegArtista(int usuarioId, int artistaId)
    {
        var relacionamento = _context.ArtistasSeguidos.SingleOrDefault(item =>
            item.UsuarioId == usuarioId && item.ArtistaId == artistaId);

        if (relacionamento is null)
        {
            return false;
        }

        _context.ArtistasSeguidos.Remove(relacionamento);
        _context.SaveChanges();
        return true;
    }

    public bool RemoverMusicaCurtida(int usuarioId, int midiaId)
    {
        var relacionamento = _context.MusicasCurtidas.SingleOrDefault(item =>
            item.UsuarioId == usuarioId && item.MidiaId == midiaId);

        if (relacionamento is null)
        {
            return false;
        }

        _context.MusicasCurtidas.Remove(relacionamento);
        _context.SaveChanges();
        return true;
    }

    public bool RemoverPlaylist(int usuarioId, int playlistId)
    {
        var playlist = _context.Playlists.SingleOrDefault(item =>
            item.IdPlaylist == playlistId && item.UsuarioId == usuarioId);

        if (playlist is null)
        {
            return false;
        }

        var musicasDaPlaylist = _context.MusicasPlaylists
            .Where(item => item.UsuarioId == usuarioId && item.PlaylistId == playlistId)
            .ToList();

        _context.MusicasPlaylists.RemoveRange(musicasDaPlaylist);
        _context.Playlists.Remove(playlist);
        _context.SaveChanges();
        return true;
    }

    public bool RemoverMusicaPlaylist(int usuarioId, int playlistId, int midiaId)
    {
        var relacionamento = _context.MusicasPlaylists.SingleOrDefault(item =>
            item.UsuarioId == usuarioId &&
            item.PlaylistId == playlistId &&
            item.MidiaId == midiaId);

        if (relacionamento is null ||
            !_context.Playlists.Any(playlist =>
                playlist.IdPlaylist == playlistId && playlist.UsuarioId == usuarioId))
        {
            return false;
        }

        _context.MusicasPlaylists.Remove(relacionamento);
        _context.SaveChanges();
        return true;
    }
}
