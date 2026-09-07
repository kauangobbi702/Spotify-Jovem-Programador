namespace SpotifyRecom.Model;

public class MusicaAtividade
{
    public int MidiaId { get; set; }
    public Midia Midia { get; set; } = null!;

    public int AtividadeId { get; set; }
    public Atividade Atividade { get; set; } = null!;
}
