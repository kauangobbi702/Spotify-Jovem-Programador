namespace SpotifyRecom.Model;
public class Album
{
    public int IdAlbum { get; set; }
    public string Nome { get; set; }
    public List<Artista> Artistas { get; set;}
    public int ArtistaId { get; set; }
    public List<Midia> Midias { get; set; }
    public int MidiaId { get; set; }
    public int AnoLancamento { get; set; }
}