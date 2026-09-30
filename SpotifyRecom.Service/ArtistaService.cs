using SpotifyRecom.Business;
using SpotifyRecom.Data;
using SpotifyRecom.Model;

namespace SpotifyRecom.Service;

public sealed class ArtistaService
{
    private readonly ArtistaBusiness _artistaBusiness;
    private readonly ListagemEF _listagem;

    public ArtistaService(SpotifyRecomContext context)
    {
        _listagem = new ListagemEF(context);
        _artistaBusiness = new ArtistaBusiness(
            new AdicionarEF(context),
            new RemoverEF(context),
            _listagem);
    }

    // Catálogo
    public List<Artista> ListarTodosArtistas()
    {
        return _listagem.ListarTodosArtistas();
    }

    public List<Album> ListarAlbunsDoArtista(int artistaId)
    {
        return _listagem.ListarAlbunsDoArtista(artistaId);
    }

    public List<Midia> ListarMusicasDoAlbum(int albumId)
    {
        return _listagem.ListarMusicasDoAlbum(albumId);
    }

    public List<Midia> ListarMusicasDoArtista(int artistaId)
    {
        return _listagem.ListarMusicasDoArtista(artistaId);
    }

    // Artistas seguidos
    public List<Artista> ListarArtistasSeguidos(int usuarioId)
    {
        return _listagem.ListarArtistasSeguidos(usuarioId);
    }

    public void SeguirArtista(int usuarioId, int artistaId)
    {
        _artistaBusiness.SeguirArtista(usuarioId, artistaId);
    }

    public void DeixarDeSeguirArtista(int usuarioId, int artistaId)
    {
        _artistaBusiness.DeixarDeSeguirArtista(usuarioId, artistaId);
    }

    // Músicas curtidas
    public List<Midia> ListarMusicasCurtidas(int usuarioId)
    {
        return _listagem.ListarMusicasCurtidas(usuarioId);
    }

    public void CurtirMusica(int usuarioId, int midiaId)
    {
        _artistaBusiness.CurtirMusica(usuarioId, midiaId);
    }

    public void DescurtirMusica(int usuarioId, int midiaId)
    {
        _artistaBusiness.DescurtirMusica(usuarioId, midiaId);
    }
}