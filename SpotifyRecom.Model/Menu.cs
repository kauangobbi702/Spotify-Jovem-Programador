using SpotifyRecom.Data;
using SpotifyRecom.Model;

namespace SpotifyRecom.App;

public sealed class Menu
{
    private readonly AutenticacaoEF _autenticacao;
    private readonly ListagemEF _listagem;
    private readonly AdicionarEF _adicionar;
    private readonly RemoverEF _remover;
    private Usuario? _usuarioLogado;

    public Menu(SpotifyRecomContext context)
    {
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
            Cabecalho("BEM-VINDO AO SPOTIFEI");
            Console.WriteLine("1 - Entrar");
            Console.WriteLine("2 - Criar conta");
            Console.WriteLine("0 - Sair");
            Separador();

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
        Console.Clear();
        Cabecalho("ENTRAR");
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
        Cabecalho("CRIAR CONTA");
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

        Console.WriteLine();
        Console.WriteLine("Escolha seu plano:");
        Mostrar(planos.Select((plano, indice) =>
            $"{indice + 1} - {plano.Descricao} ({plano.Valor:C})"));
        Separador();
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
            Cabecalho($"BEM-VINDO, {_usuarioLogado.Nome.ToUpperInvariant()}!");
            Console.WriteLine("1 - Artistas");
            Console.WriteLine("2 - Artistas seguidos");
            Console.WriteLine("3 - Musicas curtidas");
            Console.WriteLine("4 - Playlists");
            Console.WriteLine("0 - Sair da conta");
            Separador();

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
        Console.Clear();
        Cabecalho("ARTISTAS");
        var artistas = _listagem.ListarTodosArtistas();
        Mostrar(artistas.Select((artista, indice) =>
            $"{indice + 1} - {artista.Nome}"));
        Separador();
        Console.Write("Escolha um artista (0 para voltar): ");

        if (!int.TryParse(Console.ReadLine(), out var escolha) || escolha == 0)
            return;
        if (escolha < 1 || escolha > artistas.Count)
        {
            Pausar("Artista invalido.");
            return;
        }

        var artista = artistas[escolha - 1];
        Console.Clear();
        Cabecalho($"ARTISTA: {artista.Nome.ToUpperInvariant()}");
        Console.WriteLine("1 - Ver albuns e musicas");
        Console.WriteLine("2 - Seguir artista");
        Console.WriteLine("0 - Voltar");
        Separador();

        switch (Console.ReadLine())
        {
            case "1": VerAlbunsEMusicas(artista.IdArtista, artista.Nome); break;
            case "2":
                Pausar(_adicionar.AdicionarUsuarioSegArtista(_usuarioLogado!.IdUsuario, artista.IdArtista)
                    ? "Artista seguido."
                    : "Artista ja seguido ou invalido.");
                break;
        }
    }

    private void VerAlbunsEMusicas(int artistaId, string nomeArtista)
    {
        Console.Clear();
        Cabecalho($"ALBUNS E MUSICAS DE {nomeArtista.ToUpperInvariant()}");
        var albuns = _listagem.ListarAlbunsDoArtista(artistaId);
        if (albuns.Count == 0)
        {
            Pausar("Este artista ainda nao possui albuns cadastrados.");
            return;
        }

        foreach (var album in albuns)
        {
            Console.WriteLine();
            Console.WriteLine($"Album: {album.Nome}");
            var musicas = _listagem.ListarMusicasDoAlbum(album.IdAlbum);
            Mostrar(musicas.Select(musica =>
                $"  ID {musica.IdMidia} - {musica.Titulo}"));
        }

        Console.WriteLine();
        Console.WriteLine("1 - Curtir musica");
        Console.WriteLine("2 - Adicionar musica a playlist");
        Console.WriteLine("3 - Voltar");
        Separador();

        switch (Console.ReadLine())
        {
            case "1": CurtirMusica(artistaId); break;
            case "2": AdicionarMusicaAPlaylist(artistaId); break;
            case "3": return;
            default: Pausar("Opcao invalida."); break;
        }
    }

    private void CurtirMusica(int artistaId)
    {
        var musicas = _listagem.ListarMusicasDoArtista(artistaId);
        Console.Write("ID da musica para curtir (0 para voltar): ");
        if (!int.TryParse(Console.ReadLine(), out var midiaId) || midiaId == 0) return;

        if (!musicas.Any(musica => musica.IdMidia == midiaId))
        {
            Pausar("A musica nao pertence a este artista.");
            return;
        }

        Pausar(_adicionar.AdicionarMusicaCurtida(_usuarioLogado!.IdUsuario, midiaId)
            ? "Musica curtida."
            : "Musica invalida ou ja curtida.");
    }

    private void AdicionarMusicaAPlaylist(int artistaId)
    {
        var musicas = _listagem.ListarMusicasDoArtista(artistaId);
        var playlists = _listagem.ListarPlaylists(_usuarioLogado!.IdUsuario);
        if (playlists.Count == 0)
        {
            Pausar("Voce ainda nao possui playlists.");
            return;
        }

        Console.Write("ID da musica: ");
        if (!int.TryParse(Console.ReadLine(), out var midiaId) ||
            !musicas.Any(musica => musica.IdMidia == midiaId))
        {
            Pausar("Musica invalida para este artista.");
            return;
        }

        Console.WriteLine("Playlists disponiveis:");
        Mostrar(playlists.Select(playlist =>
            $"ID {playlist.IdPlaylist} - {playlist.NomePlaylist}"));
        Console.Write("ID da playlist: ");
        if (!int.TryParse(Console.ReadLine(), out var playlistId)) return;

        Pausar(_adicionar.AdicionarMusicaPlaylist(_usuarioLogado.IdUsuario, playlistId, midiaId)
            ? "Musica adicionada a playlist."
            : "Nao foi possivel adicionar a musica.");
    }

    private void ListarArtistasSeguidos()
    {
        Console.Clear();
        Cabecalho("ARTISTAS SEGUIDOS");
        var artistas = _listagem.ListarArtistasSeguidos(_usuarioLogado!.IdUsuario);
        Mostrar(artistas.Select((artista, indice) =>
            $"{indice + 1} - {artista.Nome}"));
        Separador();
        Console.Write("Escolha um artista (0 para voltar): ");
        if (!int.TryParse(Console.ReadLine(), out var escolha) || escolha == 0)
            return;
        if (escolha < 1 || escolha > artistas.Count)
        {
            Pausar("Artista invalido.");
            return;
        }

        var artista = artistas[escolha - 1];
        Console.Clear();
        Cabecalho($"ARTISTA: {artista.Nome.ToUpperInvariant()}");
        Console.WriteLine("1 - Visitar pagina do artista");
        Console.WriteLine("2 - Deixar de seguir o artista");
        Console.WriteLine("0 - Voltar");
        Separador();

        switch (Console.ReadLine())
        {
            case "1": VerAlbunsEMusicas(artista.IdArtista, artista.Nome); break;
            case "2":
                Pausar(_remover.RemoverUsuarioSegArtista(
                    _usuarioLogado.IdUsuario, artista.IdArtista)
                    ? "Voce deixou de seguir o artista."
                    : "Nao foi possivel deixar de seguir o artista.");
                break;
        }
    }

    private void ListarMusicasCurtidas()
    {
        Console.Clear();
        Cabecalho("MUSICAS CURTIDAS");
        var musicas = _listagem.ListarMusicasCurtidas(_usuarioLogado!.IdUsuario);
        Mostrar(musicas.Select((musica, indice) =>
            $"{indice + 1} - {musica.Titulo} (ID: {musica.IdMidia})"));
        Separador();
        Console.Write("ID da musica para deixar de curtir (0 para voltar): ");
        if (int.TryParse(Console.ReadLine(), out var midiaId) && midiaId != 0)
            _remover.RemoverMusicaCurtida(_usuarioLogado.IdUsuario, midiaId);
        Pausar();
    }

    private void MenuPlaylists()
    {
        while (true)
        {
            Console.Clear();
            Cabecalho("PLAYLISTS");
            var playlists = _listagem.ListarPlaylists(_usuarioLogado!.IdUsuario);
            Mostrar(playlists.Select(playlist =>
                $"ID: {playlist.IdPlaylist} - {playlist.NomePlaylist}"));
            Console.WriteLine();
            Console.WriteLine("1 - Criar playlist");
            Console.WriteLine("2 - Ver playlist");
            Console.WriteLine("3 - Remover playlist");
            Console.WriteLine("0 - Voltar");
            Separador();

            switch (Console.ReadLine())
            {
                case "1": CriarPlaylist(); break;
                case "2": VerPlaylist(playlists); break;
                case "3": RemoverPlaylist(playlists); break;
                case "0": return;
                default: Pausar("Opcao invalida."); break;
            }
        }
    }

    private void CriarPlaylist()
    {
        Console.Write("Nome da playlist: ");
        var playlist = _adicionar.AdicionarPlaylist(
            Console.ReadLine() ?? string.Empty, _usuarioLogado!.IdUsuario);
        Pausar(playlist is null ? "Nao foi possivel criar a playlist." : "Playlist criada.");
    }

    private void VerPlaylist(IReadOnlyList<Playlist> playlists)
    {
        Console.Write("ID da playlist (0 para voltar): ");
        if (!int.TryParse(Console.ReadLine(), out var playlistId) || playlistId == 0) return;
        if (!playlists.Any(playlist => playlist.IdPlaylist == playlistId))
        {
            Pausar("Playlist invalida.");
            return;
        }

        while (true)
        {
            Console.Clear();
            Cabecalho("MUSICAS DA PLAYLIST");
            var musicas = _listagem.ListarMusicasDaPlaylist(playlistId);
            Mostrar(musicas.Select((musica, indice) =>
                $"{indice + 1} - {musica.Titulo} (ID: {musica.IdMidia})"));
            Console.WriteLine();
            Console.WriteLine("1 - Adicionar musica");
            Console.WriteLine("2 - Remover musica");
            Console.WriteLine("0 - Voltar");
            Separador();
            var acao = Console.ReadLine();
            if (acao == "0") return;

            Console.Write("ID da musica: ");
            if (!int.TryParse(Console.ReadLine(), out var midiaId))
            {
                Pausar("ID invalido.");
                continue;
            }

            var sucesso = acao == "1"
                ? _adicionar.AdicionarMusicaPlaylist(_usuarioLogado!.IdUsuario, playlistId, midiaId)
                : acao == "2" && _remover.RemoverMusicaPlaylist(
                    _usuarioLogado!.IdUsuario, playlistId, midiaId);
            Pausar(sucesso ? "Operacao realizada." : "Nao foi possivel realizar a operacao.");
        }
    }

    private void RemoverPlaylist(IReadOnlyList<Playlist> playlists)
    {
        Console.Write("ID da playlist para remover (0 para voltar): ");
        if (!int.TryParse(Console.ReadLine(), out var playlistId) || playlistId == 0) return;
        Pausar(_remover.RemoverPlaylist(_usuarioLogado!.IdUsuario, playlistId)
            ? "Playlist removida."
            : "Playlist invalida.");
    }

    private static void Cabecalho(string titulo)
    {
        Separador();
        Console.WriteLine(titulo);
        Separador();
    }

    private static void Separador()
    {
        Console.WriteLine("===============================");
    }

    private static void Mostrar(IEnumerable<string> itens)
    {
        var lista = itens.ToList();
        if (lista.Count == 0)
            Console.WriteLine("Nenhum registro encontrado.");
        else
            foreach (var item in lista)
                Console.WriteLine(item);
    }

    private static void Pausar(string mensagem = "Pressione qualquer tecla para continuar.")
    {
        if (!string.IsNullOrWhiteSpace(mensagem))
            Console.WriteLine(mensagem);
        Console.ReadKey();
    }
}
