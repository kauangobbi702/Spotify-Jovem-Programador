public class Playlist : Biblioteca
{
    public int IdPlaylist { get; private set; }
    public string NomePlaylist { get; private set; }
    public Usuario Usuario { get; private set; }
    public int UsuarioId { get; private set; }
    public List<Midia> Midias { get; set; }
    public List<Biblioteca> Bibliotecas { get; set; }

}