namespace SpotifyRecom.Model;
public class Playlist
{
    public int IdPlaylist { get; private set; }
    public string NomePlaylist { get; private set; } = string.Empty;
    public Usuario Usuario { get; private set; } = null!;
    public int UsuarioId { get; private set; }
    public List<Midia> Midias { get; set; } = [];
    public List<Biblioteca> Bibliotecas { get; set; } = [];

    public Playlist(string nomePlaylist, int usuarioId)
    {
        NomePlaylist = nomePlaylist;
        UsuarioId = usuarioId;
    }

}