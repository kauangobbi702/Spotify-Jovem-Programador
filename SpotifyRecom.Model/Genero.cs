namespace SpotifyRecom.Model;
public class Genero
{
    public int IdGenero { get; set; }
    public string Nome { get; set; }
    public List<Artista> Artistas { get; set; }
    public List<Midia> Midias { get; set; }
}