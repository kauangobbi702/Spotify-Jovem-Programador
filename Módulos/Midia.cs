using System.Numerics;

public class Midia
{
    private static List<Midia> _midias = new(); //Lista de todas as mídias existentes
    public static IReadOnlyCollection<Midia> Midias => _midias.AsReadOnly();
    public int IdMidia { get; private set; }
    public static int TotalMidias { get; private set; }
    public string Titulo { get; private set; }
    public Artista Artista { get; private set; }
    public TimeSpan Duracao { get; private set; }
    public long Reproducoes { get; private set; }
    public string Genero { get; private set; }

    public Midia (int id)
    {
        IdMidia = id;
    }

    public Midia (string titulo, Artista artista, Album album, TimeSpan duracao, string genero)
    {
        IdMidia = ProximoId();
        Titulo = titulo;
        Artista = artista;
        Duracao = duracao;
        Genero = genero;
        artista.AdicionaMusicaArtista(this);
        album.AdicionarMidia(this);
        TotalMidias++;  
        _midias.Add(this);
    }



    public void TocarMidia(Midia midia)
    {
        Console.WriteLine($"A mídia {midia.Titulo} está tocando");
        Reproducoes++;
    }

    public int ProximoId()
    {
        return TotalMidias + 1;
    }

    //Método pra pegar o número de reproduções conforme o BD
    public long ContaReproducoes()
    {
        return Reproducoes;
    }

    public void MostrarMidia(Midia midia)
    {
        Console.WriteLine($"{midia.Titulo} - {midia.Duracao}");
    }
}