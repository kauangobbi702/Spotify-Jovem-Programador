using SpotifyRecom.Business.Excecoes;
using SpotifyRecom.Data;
namespace SpotifyRecom.Business;

public sealed class ArtistaBusiness
{
    private readonly AdicionarEF _adicionar;
    private readonly RemoverEF _remover;
    private readonly ListagemEF _listagem;

    public ArtistaBusiness(AdicionarEF adicionar, RemoverEF remover, ListagemEF listagem)
    {
        _adicionar = adicionar;
        _remover = remover;
        _listagem = listagem;
    }

    public void SeguirArtista(int usuarioId, int artistaId)
    {
        if (!_listagem.ListarTodosArtistas().Any(artista => artista.IdArtista == artistaId))
            throw new ArtistaNaoEncontradoException();

        if (!_adicionar.AdicionarUsuarioSegArtista(usuarioId, artistaId))
            throw new RelacaoJaExistenteException("Você já segue esse artista.");
    }

    public void DeixarDeSeguirArtista(int usuarioId, int artistaId)
    {
        if (!_remover.RemoverUsuarioSegArtista(usuarioId, artistaId))
            throw new RelacaoNaoEncontradaException("Você não segue esse artista.");
    }

    public void CurtirMusica(int usuarioId, int midiaId)
    {
        if (!_adicionar.AdicionarMusicaCurtida(usuarioId, midiaId))
            throw new RelacaoJaExistenteException("Essa música já está nas suas curtidas.");
    }

    public void DescurtirMusica(int usuarioId, int midiaId)
    {
        if (!_remover.RemoverMusicaCurtida(usuarioId, midiaId))
            throw new RelacaoNaoEncontradaException("Essa música não está nas suas curtidas.");
    }
}