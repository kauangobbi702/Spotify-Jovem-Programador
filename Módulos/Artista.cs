public class Artista
{
    private static List<Artista> _artistas = new(); //Lista de todos os artistas da plataforma
    public static IReadOnlyCollection<Artista> Artistas => _artistas.AsReadOnly();
    private List<Midia> _midiasArtista = new(); //Lista de músicas desse artista específico
    public IReadOnlyCollection<Midia> MidiasArtista => _midiasArtista.AsReadOnly();
    private List<Album> _albunsArtista = new();
    public IReadOnlyCollection<Album> AlbunsArtista => _albunsArtista.AsReadOnly();
    public int IdArtista { get; private set; }
    public static int TotalArtistas { get; private set; }
    public string Nome { get; private set; }
    public string Genero { get; private set; }
    public int NumeroDeAlbuns { get; private set; }
    private List<Usuario> _seguidores = new(); //Lista de seguidores daquele artista
    public IReadOnlyCollection<Usuario> Seguidores => _seguidores.AsReadOnly();

    public Artista(int id, string nome)
    {
        IdArtista = id;
        Nome = nome;
        TotalArtistas++;
        _artistas.Add(this);
    }

    public Artista(int id)
    {
        IdArtista = id;
    }

    public void ContadorAlbuns()
    {
        NumeroDeAlbuns++;
    }

    public int ProximoId()
    {
        return TotalArtistas + 1;
    }

    public void ExibirInformacoes()
    {
        Console.WriteLine($"Nome: {Nome}");
        Console.WriteLine($"Gênero: {Genero}");
        Console.WriteLine($"Número de Álbuns: {NumeroDeAlbuns}");
    }

    public void SegueArtista(Usuario usuario)
    {
        if (!this._seguidores.Contains(usuario))
        {
            this._seguidores.Add(usuario);
        }
    }

    public void DeixaSeguirArtista(Usuario usuario)
    {
        if(this._seguidores.Contains(usuario))
        {
           _seguidores.Remove(usuario);  
        }
    }


    public void AdicionaMusicaArtista(Midia midia)
    {
        _midiasArtista.Add(midia);
    }

    public void AdicionaAlbumArtista(Album album)
    {
        _albunsArtista.Add(album);
    }

   public void MostrarMusicas()
    {
        for (int i = 0; i < _midiasArtista.Count; i++)
        {
            // O \: serve para escapar os dois pontos no C#
            string duracaoFormatada = _midiasArtista[i].Duracao.ToString(@"mm\:ss");
            
            Console.WriteLine($"{i + 1} | {_midiasArtista[i].Titulo} - {duracaoFormatada}");
        }
    }

    public void MostrarAlbuns()
    {
        for (int i = 0; i < _albunsArtista.Count; i++)
        {
            Console.WriteLine($"{i+1} | {_albunsArtista[i].Nome} - {_albunsArtista[i].AnoLancamento}");
        }
    }
}