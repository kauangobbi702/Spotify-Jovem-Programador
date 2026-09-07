using Microsoft.EntityFrameworkCore;
using SpotifyRecom.Model;

namespace SpotifyRecom.Data;

public sealed class AdicionarEF
{
    private readonly SpotifyRecomContext _context;

    public AdicionarEF(SpotifyRecomContext context)
    {
        _context = context;
    }

    public bool AdicionarUsuario(Usuario usuario)
    {
        if (usuario is null ||
            usuario.Plano is null ||
            _context.Usuarios.Any(item => item.Email == usuario.Email) ||
            !_context.Planos.Any(plano => plano.IdPlano == usuario.Plano.IdPlano))
        {
            return false;
        }

        _context.Entry(usuario.Plano).State = EntityState.Unchanged;
        _context.Usuarios.Add(usuario);
        _context.SaveChanges();
        return true;
    }

    public bool AdicionarUsuarioSegArtista(int usuarioId, int artistaId)
    {
        if (!_context.Usuarios.Any(usuario => usuario.IdUsuario == usuarioId) ||
            !_context.Artistas.Any(artista => artista.IdArtista == artistaId) ||
            _context.ArtistasSeguidos.Any(item =>
                item.UsuarioId == usuarioId && item.ArtistaId == artistaId))
        {
            return false;
        }

        _context.ArtistasSeguidos.Add(new ArtistaSeguido
        {
            UsuarioId = usuarioId,
            ArtistaId = artistaId
        });
        _context.SaveChanges();
        return true;
    }

    public bool AdicionarMusicaCurtida(int usuarioId, int midiaId)
    {
        if (!_context.Usuarios.Any(usuario => usuario.IdUsuario == usuarioId) ||
            !_context.Midias.Any(midia => midia.IdMidia == midiaId) ||
            _context.MusicasCurtidas.Any(item =>
                item.UsuarioId == usuarioId && item.MidiaId == midiaId))
        {
            return false;
        }

        _context.MusicasCurtidas.Add(new MusicaCurtida
        {
            UsuarioId = usuarioId,
            MidiaId = midiaId
        });
        _context.SaveChanges();
        return true;
    }

    public Playlist? AdicionarPlaylist(string nomePlaylist, int usuarioId)
    {
        if (string.IsNullOrWhiteSpace(nomePlaylist) ||
            !_context.Usuarios.Any(usuario => usuario.IdUsuario == usuarioId))
        {
            return null;
        }

        var playlist = new Playlist(nomePlaylist, usuarioId);
        _context.Playlists.Add(playlist);
        _context.SaveChanges();
        return playlist;
    }

    public bool AdicionarMusicaPlaylist(int usuarioId, int playlistId, int midiaId)
    {
        if (!_context.Playlists.Any(playlist =>
                playlist.IdPlaylist == playlistId && playlist.UsuarioId == usuarioId) ||
            !_context.Midias.Any(midia => midia.IdMidia == midiaId) ||
            _context.MusicasPlaylists.Any(item =>
                item.PlaylistId == playlistId &&
                item.MidiaId == midiaId))
        {
            return false;
        }

        _context.MusicasPlaylists.Add(new MusicaPlaylist
        {
            UsuarioId = usuarioId,
            PlaylistId = playlistId,
            MidiaId = midiaId
        });
        _context.SaveChanges();
        return true;
    }
}
