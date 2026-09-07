namespace SpotifyRecom.Model;
public class Artista
{
    public int IdArtista { get; private set; }
    public string Nome { get; private set; }
    public List<Genero> GenerosArtista { get; set; }
    public List<Album> Albuns { get; set; }
    public List<Midia> Midias { get; set; }

    private Artista()
    {
    }

    public Artista(int id, string nome)
    {
        IdArtista = id;
        Nome = nome;
    }
}