namespace SpotifyRecom.Model;

public class ArtistaSeguido
{
    public int UsuarioId { get; set; }
    public Usuario Usuario { get; set; } = null!;

    public int ArtistaId { get; set; }
    public Artista Artista { get; set; } = null!;
}
