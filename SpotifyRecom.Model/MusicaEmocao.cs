namespace SpotifyRecom.Model;

public class MusicaEmocao
{
    public int MidiaId { get; set; }
    public Midia Midia { get; set; } = null!;

    public int EmocaoId { get; set; }
    public Emocao Emocao { get; set; } = null!;
}
