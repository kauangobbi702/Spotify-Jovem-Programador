public class Playlist : Biblioteca
{
    private static List<Playlist> _playlists = new(); //Lista de todas as playlists
    public static IReadOnlyCollection<Playlist> Playlists => _playlists.AsReadOnly();
    private List<Midia> _midias = new(); //Lista de mídias de uma playlist
    public IReadOnlyCollection<Midia> Midias => _midias.AsReadOnly();
    public int IdPlaylist { get; private set; }
    private static int ProximoId;
    public string NomePlaylist { get; private set; }
    public Usuario Usuario { get; private set; } //Usuário ao qual a playlist pertence (só ele pode editar)

    public Playlist(string nomePlaylist, Usuario usuario)
    {   
        IdPlaylist = GetProximoId();
        NomePlaylist = nomePlaylist;
        Usuario = usuario;    
        _playlists.Add(this);    
        ProximoId++;
    }

    public Playlist(string nome)
    {
        NomePlaylist = nome;
    }

    public Playlist(int id)
    {
        IdPlaylist = id;
    }

    public int GetProximoId()
    {
        return ProximoId + 1;
    }

    public void AddMusPlaylist(Midia midia)
    {
        _midias.Add(midia);
    }

    public void RemoveMusPlaylist(Midia midia)
    {
        _midias.Remove(midia);
    }

    public void MostraPlaylist()
    {
        for (int i = 0; i < _midias.Count; i++)
        {
            string duracaoFormatada = _midias[i].Duracao.ToString(@"mm\:ss");
            Console.WriteLine($"{i + 1} | {_midias[i].Titulo} - {duracaoFormatada}");
        }
    }

}