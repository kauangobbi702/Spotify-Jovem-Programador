using SpotifyRecom.Business.Excecoes;
using SpotifyRecom.Data;
using SpotifyRecom.Model;
namespace SpotifyRecom.Business;

public sealed class PlaylistBusiness
{
    private readonly AdicionarEF _adicionar;
    private readonly RemoverEF _remover;

    public PlaylistBusiness(AdicionarEF adicionar, RemoverEF remover)
    {
        _adicionar = adicionar;
        _remover = remover;
    }

    public Playlist CriarPlaylist(string nomePlaylist, int usuarioId)
    {
        if (string.IsNullOrWhiteSpace(nomePlaylist))
            throw new NomePlaylistInvalidoException();

        var playlist = _adicionar.AdicionarPlaylist(nomePlaylist, usuarioId);
        if (playlist is null)
            throw new UsuarioNaoEncontradoException();

        return playlist;
    }

    public void RemoverPlaylist(int usuarioId, int playlistId)
    {
        if (!_remover.RemoverPlaylist(usuarioId, playlistId))
            throw new PlaylistNaoEncontradaException();
    }

    public void AdicionarMusica(int usuarioId, int playlistId, int midiaId)
    {
        if (!_adicionar.AdicionarMusicaPlaylist(usuarioId, playlistId, midiaId))
            throw new RelacaoJaExistenteException(
                "Essa música já está na playlist (ou a playlist/música não existe).");
    }

    public void RemoverMusica(int usuarioId, int playlistId, int midiaId)
    {
        if (!_remover.RemoverMusicaPlaylist(usuarioId, playlistId, midiaId))
            throw new RelacaoNaoEncontradaException("Essa música não está na playlist.");
    }
}