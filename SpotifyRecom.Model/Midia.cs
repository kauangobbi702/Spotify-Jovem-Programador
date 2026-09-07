namespace SpotifyRecom.Model;

public class Midia
{
    public int IdMidia { get; private set; }
    public string Titulo { get; private set; }
    public List<Artista> Artistas { get; private set; }
    public int ArtistaId { get; set; }
    public Album Album { get; set; }
    public int AlbumId { get; set; }
    public List<MusicaEmocao> MusicasEmocoes { get; set; } = new();
    public List<MusicaAtividade> MusicasAtividades { get; set; } = new();
    public TimeSpan Duracao { get; private set; }
    public List<Genero> GenerosMidia { get; private set; }
    public List<Playlist> Playlists { get; private set; }
    public List<Biblioteca> Bibliotecas { get; set; }

    private Midia()
    {
    }

    public Midia (int id)
    {
        IdMidia = id;
    }
}