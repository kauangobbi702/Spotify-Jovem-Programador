namespace SpotifyRecom.Model;
public class Biblioteca
{
    public int IdBiblioteca { get; set; }
    public Usuario Usuario { get; set; }
    public int UsuarioId { get; set; }
    public List<Midia> Midias { get; set; }
    public List<Playlist> Playlists { get; set; }
    
}