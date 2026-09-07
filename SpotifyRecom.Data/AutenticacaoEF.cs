using Microsoft.EntityFrameworkCore;
using SpotifyRecom.Model;

namespace SpotifyRecom.Data;

public sealed class AutenticacaoEF
{
    private readonly SpotifyRecomContext _context;

    public AutenticacaoEF(SpotifyRecomContext context)
    {
        _context = context;
    }

    public Usuario? ValidarLogin(string email, string senha)
    {
        return _context.Usuarios
            .AsNoTracking()
            .Include(usuario => usuario.Plano)
            .Include(usuario => usuario.Playlists)
            .SingleOrDefault(usuario =>
                usuario.Email == email &&
                usuario.Senha == senha);
    }
}
