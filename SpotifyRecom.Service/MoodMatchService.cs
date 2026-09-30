using SpotifyRecom.Data;
using SpotifyRecom.Model;

namespace SpotifyRecom.Service;

public sealed class MoodMatchService
{
    private readonly ListagemEF _listagem;

    public MoodMatchService(SpotifyRecomContext context)
    {
        _listagem = new ListagemEF(context);
    }

    public List<Midia> ListarMusicasPorMood(int emocaoId, int atividadeId)
    {
        return _listagem.ListarMusicasPorMood(emocaoId, atividadeId);
    }
}