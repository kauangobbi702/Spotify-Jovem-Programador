namespace SpotifyRecom.Model;

public class Emocao
{
    public int IdEmocao { get; set; }
    public string Nome { get; set; } = string.Empty;
    public List<MusicaEmocao> MusicasEmocoes { get; set; } = new();
}
