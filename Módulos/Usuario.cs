public class Usuario
{
    private static List<Usuario> _usuariosCadastrados = new(); //Lista de todos os usuários cadastrados
    public static IReadOnlyCollection<Usuario> UsuariosCadastrados => _usuariosCadastrados.AsReadOnly();
    public int IdUsuario { get; private set; }
    public static int TotalUsuarios { get; private set; }
    public string Nome { get; private set; }
    public string Email { get; private set; }
    public string Senha { get; private set; }
    public Plano Plano { get; private set; }
    public IReadOnlyCollection<Artista> ArtistasSeguidos => _artistasSeguidos.AsReadOnly();
    private List<Artista> _artistasSeguidos = new(); //Lista de artistas seguidos para ser mostrada na biblioteca
    public IReadOnlyCollection<Midia> MusicasCurtidas => _musicasCurtidas.AsReadOnly();
    private List<Midia> _musicasCurtidas = new(); //Lista de música curtida do usuário específico
    public IReadOnlyCollection<Midia> BibliotecaUsuario => _bibliotecaUsuario.AsReadOnly();
    private List<Midia> _bibliotecaUsuario = new(); //Lista contendo a biblioteca de um usuário específica
    //Acredito que na biblioteca devem existir as playlists seguidas e os artistas seguidos, "separado" de músicas curtidas e playlists próprias do usuário
    public IReadOnlyCollection<Playlist> PlaylistsUsuario => _playlistsUsuario.AsReadOnly();
    private List<Playlist> _playlistsUsuario = new(); //Lista contendo as playlists de um usuário específico

    public Usuario(string nome, string email, string senha, Plano plano)
    {
        Nome = nome;
        Email = email;
        Senha = senha;
        Plano = plano;
        _usuariosCadastrados.Add(this);   
    }

    public Usuario(int id, string nome)
    {
        IdUsuario = id;
        Nome = nome;
    }

    public Usuario(int id, string nome, Plano plano)
    {
        IdUsuario = id;
        Nome = nome;
        Plano = plano;
    }

    public int ProximoId()
    {
        return TotalUsuarios + 1;
    }


    public void CurteMusica(Midia midia)
    {
        _musicasCurtidas.Add(midia);
    }

    public void DescurteMusica(Midia midia)
    {
        _musicasCurtidas.Remove(midia);
    }

    public void CriaPlaylist(string nomePlayist)
    {
        Playlist novaPlaylist = new(nomePlayist, this);
        _playlistsUsuario.Add(novaPlaylist);

        new UsuarioDAO().AdicionarPlaylist(this, novaPlaylist);        
    }

    public void DeletaPlaylist(Playlist playlist)
    {
        _playlistsUsuario.Remove(playlist);
    }

    public void AddMusBiblioteca(Midia midia)
    {
        _bibliotecaUsuario.Add(midia);
    }

    public void SegueArtista(Artista artista)
    {
        artista.SegueArtista(this);
        _artistasSeguidos.Add(artista);
    }

    public void DeixaSeguirArtista(Artista artista)
    {
        artista.DeixaSeguirArtista(this);
        _artistasSeguidos.Remove(artista);
    }

}