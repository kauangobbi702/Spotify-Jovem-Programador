namespace SpotifyRecom.Model;

public class MusicaCurtida
{
    public int UsuarioId { get; set; }
    public Usuario Usuario { get; set; } = null!;

    public int MidiaId { get; set; }
    public Midia Midia { get; set; } = null!;
}
