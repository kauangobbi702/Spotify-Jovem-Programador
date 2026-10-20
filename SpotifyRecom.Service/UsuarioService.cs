using SpotifyRecom.Business;
using SpotifyRecom.Data;
using SpotifyRecom.Model;

namespace SpotifyRecom.Service;

public sealed class UsuarioService
{
    private readonly UsuarioBusiness _usuarioBusiness;
    private readonly ListagemEF _listagem;

    public UsuarioService(SpotifyRecomContext context)
    {
        _listagem = new ListagemEF(context);
        _usuarioBusiness = new UsuarioBusiness(
            new AdicionarEF(context),
            new AutenticacaoEF(context),
            _listagem);
    }

    public Usuario CadastrarUsuario(string nome, string email, string senha, int idPlano)
    {
        return _usuarioBusiness.CadastrarUsuario(nome, email, senha, idPlano);
    }

    public Usuario ValidarLogin(string email, string senha)
    {
        return _usuarioBusiness.ValidarLogin(email, senha);
    }

    public List<Plano> ListarPlanos()
    {
        return _listagem.ListarPlanos();
    }
}