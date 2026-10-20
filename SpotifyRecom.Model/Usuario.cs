namespace SpotifyRecom.Model;
public class Usuario
{
    public int IdUsuario { get; private set; }
    public string Nome { get; private set; } = string.Empty;
    public string Email { get; private set; } = string.Empty;
    public string Senha { get; private set; } = string.Empty;
    public Plano Plano { get; private set; } = null!;
    public List<Playlist> Playlists { get; set; } = [];
    public Biblioteca biblioteca { get; set; } = null!;
    public int PlanoId { get; private set; }

    private Usuario()
    {
    }

    public Usuario(string nome, string email, string senha, Plano plano)
    {
        Nome = nome;
        Email = email;
        Senha = senha;
        Plano = plano;
        PlanoId = plano.IdPlano;
    }
    
}