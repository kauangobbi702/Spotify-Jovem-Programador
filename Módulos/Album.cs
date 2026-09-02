public class Album : Biblioteca
{
    private List<Midia> _midiasAlbum = new(); //Lista de mídias do álbum
    public IReadOnlyCollection<Midia> MidiasAlbum => _midiasAlbum.AsReadOnly();
    public int IdAlbum { get; set; }
    public string Nome { get; private set; }
    public Artista Artista { get; private set; } //Artista ao qual o álbum pertence
    public int AnoLancamento { get; private set; }

    public Album(int idAlbum, string nome)
    {
        IdAlbum = idAlbum;
        Nome = nome;
    }
    public Album(string nome, Artista artista, int anoLancamento)
    {
        Nome = nome;
        Artista = artista;
        artista.AdicionaAlbumArtista(this);
        AnoLancamento = anoLancamento;
    }

    public void AdicionarMidia(Midia midia)
    {
        _midiasAlbum.Add(midia);
    }

    public void MostrarMidias()
    {
        for (int i = 0; i < _midiasAlbum.Count; i++)
        {
            Console.WriteLine($"{i+1} | {_midiasAlbum[i].Titulo} - {_midiasAlbum[i].Duracao}");
        }
    }
}