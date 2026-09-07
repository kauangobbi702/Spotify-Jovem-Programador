using SpotifyRecom.Data;
using SpotifyRecom.Model;

namespace SpotifyRecom.App;

public sealed class Menu
{
    private readonly SpotifyRecomContext _context;
    private readonly AutenticacaoEF _autenticacao;
    private readonly ListagemEF _listagem;
    private readonly AdicionarEF _adicionar;
    private readonly RemoverEF _remover;
    private Usuario? _usuarioLogado;

    public Menu(SpotifyRecomContext context)
    {
        _context = context;
        _autenticacao = new AutenticacaoEF(context);
        _listagem = new ListagemEF(context);
        _adicionar = new AdicionarEF(context);
        _remover = new RemoverEF(context);
    }

    public void Iniciar()
    {
        while (true)
        {
            Console.Clear();
            Console.WriteLine("===== SPOTIFEI =====");
            Console.WriteLine("1 - Entrar");
            Console.WriteLine("2 - Criar conta");
            Console.WriteLine("0 - Sair");

            switch (Console.ReadLine())
            {
                case "1": Entrar(); break;
                case "2": CadastrarUsuario(); break;
                case "0": return;
                default: Pausar("Opcao invalida."); break;
            }
        }
    }

    private void Entrar()
    {
        Console.Write("E-mail: ");
        var email = Console.ReadLine() ?? string.Empty;
        Console.Write("Senha: ");
        var senha = Console.ReadLine() ?? string.Empty;

        _usuarioLogado = _autenticacao.ValidarLogin(email, senha);
        if (_usuarioLogado is null)
        {
            Pausar("E-mail ou senha invalidos.");
            return;
        }

        MenuUsuario();
    }

    private void CadastrarUsuario()
    {
        Console.Clear();
        Console.Write("Nome: ");
        var nome = Console.ReadLine() ?? string.Empty;
        Console.Write("E-mail: ");
        var email = Console.ReadLine() ?? string.Empty;
        Console.Write("Senha: ");
        var senha = Console.ReadLine() ?? string.Empty;

        var planos = _listagem.ListarPlanos();
        if (planos.Count == 0)
        {
            Pausar("Nenhum plano cadastrado.");
            return;
        }

        for (var indice = 0; indice < planos.Count; indice++)
        {
            Console.WriteLine($"{indice + 1} - {planos[indice].Descricao} ({planos[indice].Valor:C})");
        }

        Console.Write("Plano: ");
        if (!int.TryParse(Console.ReadLine(), out var planoEscolhido) ||
            planoEscolhido < 1 || planoEscolhido > planos.Count)
        {
            Pausar("Plano invalido.");
            return;
        }

        var usuario = new Usuario(nome, email, senha, planos[planoEscolhido - 1]);
        Pausar(_adicionar.AdicionarUsuario(usuario)
            ? "Usuario cadastrado com sucesso."
            : "Nao foi possivel cadastrar o usuario.");
    }

    private void MenuUsuario()
    {
        while (_usuarioLogado is not null)
        {
            Console.Clear();
            Console.WriteLine($"Usuario: {_usuarioLogado.Nome}");
            Console.WriteLine("1 - Artistas");
            Console.WriteLine("2 - Artistas seguidos");
            Console.WriteLine("3 - Musicas curtidas");
            Console.WriteLine("4 - Playlists");
            Console.WriteLine("0 - Sair da conta");

            switch (Console.ReadLine())
            {
                case "1": MenuArtistas(); break;
                case "2": ListarArtistasSeguidos(); break;
                case "3": ListarMusicasCurtidas(); break;
                case "4": MenuPlaylists(); break;
                case "0": _usuarioLogado = null; break;
                default: Pausar("Opcao invalida."); break;
            }
        }
    }

    private void MenuArtistas()
    {
        var artistas = _listagem.ListarTodosArtistas();
        Mostrar(artistas.Select(artista => $"{artista.IdArtista} - {artista.Nome}"));
        Console.Write("ID do artista (0 para voltar): ");
        if (!int.TryParse(Console.ReadLine(), out var artistaId) || artistaId == 0) return;

        var artista = artistas.SingleOrDefault(item => item.IdArtista == artistaId);
        if (artista is null) { Pausar("Artista invalido."); return; }

        Console.WriteLine("1 - Ver albuns e musicas");
        Console.WriteLine("2 - Seguir artista");
        if (Console.ReadLine() == "2")
        {
            Pausar(_adicionar.AdicionarUsuarioSegArtista(_usuarioLogado!.IdUsuario, artistaId)
                ? "Artista seguido."
                : "Artista ja seguido ou invalido.");
            return;
        }

        var albuns = _listagem.ListarAlbunsDoArtista(artistaId);
        foreach (var album in albuns)
        {
            Console.WriteLine($"Album: {album.Nome}");
            foreach (var musica in _listagem.ListarMusicasDoAlbum(album.IdAlbum))
                Console.WriteLine($"  {musica.IdMidia} - {musica.Titulo}");
        }

        Console.Write("ID da musica para curtir (0 para voltar): ");
        if (int.TryParse(Console.ReadLine(), out var midiaId) && midiaId != 0)
            Pausar(_adicionar.AdicionarMusicaCurtida(_usuarioLogado!.IdUsuario, midiaId)
                ? "Musica curtida."
                : "Musica invalida ou ja curtida.");
        else
            Pausar();
    }

    private void ListarArtistasSeguidos()
    {
        var artistas = _listagem.ListarArtistasSeguidos(_usuarioLogado!.IdUsuario);
        Mostrar(artistas.Select(artista => $"{artista.IdArtista} - {artista.Nome}"));
        Console.Write("ID do artista para deixar de seguir (0 para voltar): ");
        if (int.TryParse(Console.ReadLine(), out var artistaId) && artistaId != 0)
            _remover.RemoverUsuarioSegArtista(_usuarioLogado.IdUsuario, artistaId);
        Pausar();
    }

    private void ListarMusicasCurtidas()
    {
        var musicas = _listagem.ListarMusicasCurtidas(_usuarioLogado!.IdUsuario);
        Mostrar(musicas.Select(musica => $"{musica.IdMidia} - {musica.Titulo}"));
        Console.Write("ID da musica para deixar de curtir (0 para voltar): ");
        if (int.TryParse(Console.ReadLine(), out var midiaId) && midiaId != 0)
            _remover.RemoverMusicaCurtida(_usuarioLogado.IdUsuario, midiaId);
        Pausar();
    }

    private void MenuPlaylists()
    {
        var playlists = _listagem.ListarPlaylists(_usuarioLogado!.IdUsuario);
        Mostrar(playlists.Select(playlist => $"{playlist.IdPlaylist} - {playlist.NomePlaylist}"));
        Console.WriteLine("1 - Criar playlist");
        Console.WriteLine("2 - Ver playlist");
        Console.WriteLine("3 - Remover playlist");
        Console.WriteLine("0 - Voltar");

        switch (Console.ReadLine())
        {
            case "1":
                Console.Write("Nome: ");
                var playlist = _adicionar.AdicionarPlaylist(
                    Console.ReadLine() ?? string.Empty, _usuarioLogado.IdUsuario);
                Pausar(playlist is null ? "Nao foi possivel criar a playlist." : "Playlist criada.");
                break;
            case "2": VerPlaylist(playlists); break;
            case "3": RemoverPlaylist(playlists); break;
        }
    }

    private void VerPlaylist(IReadOnlyList<SpotifyRecom.Model.Playlist> playlists)
    {
        Console.Write("ID da playlist: ");
        if (!int.TryParse(Console.ReadLine(), out var playlistId)) return;
        if (!playlists.Any(playlist => playlist.IdPlaylist == playlistId))
        {
            Pausar("Playlist invalida.");
            return;
        }

        var musicas = _listagem.ListarMusicasDaPlaylist(playlistId);
        Mostrar(musicas.Select(musica => $"{musica.IdMidia} - {musica.Titulo}"));
        Console.WriteLine("1 - Adicionar musica");
        Console.WriteLine("2 - Remover musica");
        Console.WriteLine("0 - Voltar");
        var acao = Console.ReadLine();
        if (acao == "0") return;

        Console.Write("ID da musica: ");
        if (!int.TryParse(Console.ReadLine(), out var midiaId) || midiaId == 0) return;

        var sucesso = acao == "1"
            ? _adicionar.AdicionarMusicaPlaylist(_usuarioLogado!.IdUsuario, playlistId, midiaId)
            : acao == "2" && _remover.RemoverMusicaPlaylist(_usuarioLogado!.IdUsuario, playlistId, midiaId);

        Pausar(sucesso ? "Operacao realizada." : "Nao foi possivel realizar a operacao.");
    }

    private void RemoverPlaylist(IReadOnlyList<SpotifyRecom.Model.Playlist> playlists)
    {
        Console.Write("ID da playlist: ");
        if (!int.TryParse(Console.ReadLine(), out var playlistId)) return;
        Pausar(_remover.RemoverPlaylist(_usuarioLogado!.IdUsuario, playlistId)
            ? "Playlist removida."
            : "Playlist invalida.");
    }

    private static void Mostrar(IEnumerable<string> itens)
    {
        var lista = itens.ToList();
        if (lista.Count == 0) Console.WriteLine("Nenhum registro encontrado.");
        else foreach (var item in lista) Console.WriteLine(item);
    }

    private static void Pausar(string mensagem = "Pressione qualquer tecla para continuar.")
    {
        if (!string.IsNullOrWhiteSpace(mensagem)) Console.WriteLine(mensagem);
        Console.ReadKey();
    }
}
