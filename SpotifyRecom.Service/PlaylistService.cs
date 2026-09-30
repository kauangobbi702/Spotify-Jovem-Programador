using SpotifyRecom.Business;
using SpotifyRecom.Data;
using SpotifyRecom.Model;

namespace SpotifyRecom.Service;

public sealed class PlaylistService
{
    private readonly PlaylistBusiness _playlistBusiness;
    private readonly ListagemEF _listagem;

    public PlaylistService(SpotifyRecomContext context)
    {
        _listagem = new ListagemEF(context);
        _playlistBusiness = new PlaylistBusiness(
            new AdicionarEF(context),
            new RemoverEF(context));
    }

    public List<Playlist> ListarPlaylists(int usuarioId)
    {
        return _listagem.ListarPlaylists(usuarioId);
    }

    public List<Midia> ListarMusicasDaPlaylist(int playlistId)
    {
        return _listagem.ListarMusicasDaPlaylist(playlistId);
    }

    public Playlist CriarPlaylist(string nomePlaylist, int usuarioId)
    {
        return _playlistBusiness.CriarPlaylist(nomePlaylist, usuarioId);
    }

    public void RemoverPlaylist(int usuarioId, int playlistId)
    {
        _playlistBusiness.RemoverPlaylist(usuarioId, playlistId);
    }

    public void AdicionarMusica(int usuarioId, int playlistId, int midiaId)
    {
        _playlistBusiness.AdicionarMusica(usuarioId, playlistId, midiaId);
    }

    public void RemoverMusica(int usuarioId, int playlistId, int midiaId)
    {
        _playlistBusiness.RemoverMusica(usuarioId, playlistId, midiaId);
    }
}