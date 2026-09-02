public class Biblioteca : IGerenciamento
{
    private List<Playlist> _playlists = new();
    private List<Artista> _artistas = new();


    public void AdicionarArtista(Artista artista)
    {
        _artistas.Add(artista);
    }

    public void RemoverArtista(Artista artista)
    {
        _artistas.Remove(artista);
    }

    public void AdicionarPlaylist(Playlist playlist)
    {
       _playlists.Add(playlist); 
    }
    public void RemoverPlaylist(Playlist playlist)
    {
       _playlists.Remove(playlist); 
    }

}