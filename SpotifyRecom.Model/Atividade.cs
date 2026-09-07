namespace SpotifyRecom.Model;

public class Atividade
{
    public int IdAtividade { get; set; }
    public string Nome { get; set; } = string.Empty;
    public List<MusicaAtividade> MusicasAtividades { get; set; } = new();
}
