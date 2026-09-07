using System.Text.RegularExpressions;
using SpotifyRecom.Business.Excecoes;
using SpotifyRecom.Data;
using SpotifyRecom.Model;

namespace SpotifyRecom.Business;

public sealed class UsuarioBusiness
{
    private static readonly Regex RegexEmail =
        new(@"^[^@\s]+@[^@\s]+\.[^@\s]+$", RegexOptions.Compiled);

    private readonly AdicionarEF _adicionar;
    private readonly AutenticacaoEF _autenticacao;
    private readonly ListagemEF _listagem;

    public UsuarioBusiness(AdicionarEF adicionar, AutenticacaoEF autenticacao, ListagemEF listagem)
    {
        _adicionar = adicionar;
        _autenticacao = autenticacao;
        _listagem = listagem;
    }

    public Usuario CadastrarUsuario(string nome, string email, string senha, int idPlano)
    {
        if (string.IsNullOrWhiteSpace(nome))
            throw new NomeInvalidoException();

        if (string.IsNullOrWhiteSpace(email) || !RegexEmail.IsMatch(email))
            throw new EmailInvalidoException();

        if (string.IsNullOrWhiteSpace(senha) || senha.Length < 6)
            throw new SenhaInvalidaException();

        var plano = _listagem.ListarPlanos()
            .SingleOrDefault(item => item.IdPlano == idPlano);
        if (plano is null)
            throw new PlanoInvalidoException();

        var usuario = new Usuario(nome, email, senha, plano);

        if (!_adicionar.AdicionarUsuario(usuario))
            throw new EmailJaCadastradoException();

        return usuario;
    }

    public Usuario ValidarLogin(string email, string senha)
    {
        var usuario = _autenticacao.ValidarLogin(email, senha);
        if (usuario is null)
            throw new CredenciaisInvalidasException();

        return usuario;
    }
}