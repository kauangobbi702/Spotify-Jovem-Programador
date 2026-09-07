namespace SpotifyRecom.Model;

public class MusicaPlaylist
{
    public int UsuarioId { get; set; }
    public Usuario Usuario { get; set; } = null!;

    public int MidiaId { get; set; }
    public Midia Midia { get; set; } = null!;

    public int PlaylistId { get; set; }
    public Playlist Playlist { get; set; } = null!;
}
