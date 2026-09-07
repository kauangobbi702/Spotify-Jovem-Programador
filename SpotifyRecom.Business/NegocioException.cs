namespace SpotifyRecom.Business.Excecoes;

public abstract class NegocioException : Exception
{
    protected NegocioException(string mensagem) : base(mensagem)
    {
    }
}

public sealed class NomeInvalidoException : NegocioException
{
    public NomeInvalidoException() : base("O nome não pode ser vazio.")
    {
    }
}

public sealed class EmailInvalidoException : NegocioException
{
    public EmailInvalidoException() : base("O e-mail informado não é valido.")
    {
    }
}

public sealed class EmailJaCadastradoException : NegocioException
{
    public EmailJaCadastradoException() : base("Já existe uma conta cadastrada com esse e-mail.")
    {
    }
}

public sealed class SenhaInvalidaException : NegocioException
{
    public SenhaInvalidaException() : base("A senha deve ter pelo menos 6 caracteres.")
    {
    }
}

public sealed class PlanoInvalidoException : NegocioException
{
    public PlanoInvalidoException() : base("O plano escolhido não existe.")
    {
    }
}

public sealed class CredenciaisInvalidasException : NegocioException
{
    public CredenciaisInvalidasException() : base("E-mail ou senha inválidos.")
    {
    }
}

public sealed class UsuarioNaoEncontradoException : NegocioException
{
    public UsuarioNaoEncontradoException() : base("Usuário não encontrado.")
    {
    }
}

public sealed class ArtistaNaoEncontradoException : NegocioException
{
    public ArtistaNaoEncontradoException() : base("Artista não encontrado.")
    {
    }
}

public sealed class NomePlaylistInvalidoException : NegocioException
{
    public NomePlaylistInvalidoException() : base("O nome da playlist não pode ser vazio.")
    {
    }
}

public sealed class PlaylistNaoEncontradaException : NegocioException
{
    public PlaylistNaoEncontradaException() : base("Playlist não encontrada.")
    {
    }
}

public sealed class RelacaoJaExistenteException : NegocioException
{
    public RelacaoJaExistenteException(string mensagem) : base(mensagem)
    {
    }
}

public sealed class RelacaoNaoEncontradaException : NegocioException
{
    public RelacaoNaoEncontradaException(string mensagem) : base(mensagem)
    {
    }
}