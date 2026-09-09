using System.Diagnostics;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using SpotifyRecom.Business;
using SpotifyRecom.Business.Excecoes;
using SpotifyRecom.Data;
using SpotifyRecom.Model;

namespace SpotifyRecom.App;

public sealed class Menu
{
    private readonly AutenticacaoEF _autenticacao;
    private readonly ListagemEF _listagem;
    private readonly AdicionarEF _adicionar;
    private readonly RemoverEF _remover;
    private readonly UsuarioBusiness _usuarioBusiness;
    private readonly ArtistaBusiness _artistaBusiness;
    private readonly PlaylistBusiness _playlistBusiness;
    private Usuario? _usuarioLogado;

    public Menu(SpotifyRecomContext context)
    {
        _autenticacao = new AutenticacaoEF(context);
        _listagem = new ListagemEF(context);
        _adicionar = new AdicionarEF(context);
        _remover = new RemoverEF(context);

        _usuarioBusiness = new UsuarioBusiness(_adicionar, _autenticacao, _listagem);
        _artistaBusiness = new ArtistaBusiness(_adicionar, _remover, _listagem);
        _playlistBusiness = new PlaylistBusiness(_adicionar, _remover);
    }

    #region MENUS - ENTRADA
    public void Iniciar()
    {
        while (true)
        {
            Console.Clear();
            Cabecalho("BEM-VINDO AO SPOTIFEI", "MENU");
            Console.WriteLine("1 - Entrar");
            Console.WriteLine("2 - Criar conta");
            Console.WriteLine("0 - Sair");
            Separador();

            switch (Console.ReadLine())
            {
                case "1": Entrar(); break;
                case "2": CadastrarUsuario(); break;
                case "0": return;
                default: Pausar("Opção inválida."); break;
            }
        }
    }

    private void Entrar()
    {
        Console.Clear();
        Cabecalho("ENTRAR", "LOGIN");
        Console.Write("E-mail: ");
        var email = Console.ReadLine() ?? string.Empty;
        Console.Write("Senha: ");
        var senha = Console.ReadLine() ?? string.Empty;

        try
        {
            _usuarioLogado = _usuarioBusiness.ValidarLogin(email, senha);
        }
        catch (NegocioException excecao)
        {
            Pausar(excecao.Message);
            return;
        }

        MenuUsuario();
    }

    private void CadastrarUsuario()
    {
        Console.Clear();
        Cabecalho("CRIAR CONTA", "CADASTRO");
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
            Pausar("Plano inválido.");
            return;
        }

        try
        {
            _usuarioBusiness.CadastrarUsuario(nome, email, senha, planos[planoEscolhido - 1].IdPlano);
            Pausar("Usuario cadastrado com sucesso.");
        }
        catch (NegocioException excecao)
        {
            Pausar(excecao.Message);
        }
    }

    private void MenuUsuario()
    {
        while (_usuarioLogado is not null)
        {
            Console.Clear();
            Cabecalho($"BEM-VINDO, {_usuarioLogado.Nome.ToUpperInvariant()}!", "MENU");
            Console.WriteLine("1 - Artistas");
            Console.WriteLine("2 - Artistas seguidos");
            Console.WriteLine("3 - Músicas curtidas");
            Console.WriteLine("4 - Playlists");
            Console.WriteLine("5 - Mood Match");
            Console.WriteLine("0 - Sair da conta");
            Separador();

            switch (Console.ReadLine())
            {
                case "1": MenuArtistas(); break;
                case "2": ListarArtistasSeguidos(); break;
                case "3": ListarMusicasCurtidas(); break;
                case "4": MenuPlaylists(); break;
                case "5": MenuMoodMatch(); break;
                case "0": _usuarioLogado = null; break;
                default: Pausar("Opção inválida."); break;
            }
        }
    }

    #endregion




    #region SUBMENU
    private void MenuArtistas()
    {
        Console.Clear();
        Cabecalho("ARTISTAS", "MENU");
        var artistas = _listagem.ListarTodosArtistas();
        Mostrar(artistas.Select((artista, indice) =>
            $"{indice + 1} - {artista.Nome}"));
        Separador();
        Console.Write("Escolha um artista (0 para voltar): ");

        if (!int.TryParse(Console.ReadLine(), out var escolha) || escolha == 0)
            return;
        if (escolha < 1 || escolha > artistas.Count)
        {
            Pausar("Artista inválido.");
            return;
        }

        var artista = artistas[escolha - 1];
        Console.Clear();
        Cabecalho($"ARTISTA: {artista.Nome.ToUpperInvariant()}", "MENU");
        Console.WriteLine("1 - Ver álbuns e músicas");
        Console.WriteLine("2 - Seguir artista");
        Console.WriteLine("0 - Voltar");
        Separador();

        switch (Console.ReadLine())
        {
            case "1": VerAlbunsEMusicas(artista.IdArtista, artista.Nome); break;
            case "2":
                try
                {
                    _artistaBusiness.SeguirArtista(_usuarioLogado!.IdUsuario, artista.IdArtista);
                    Pausar("Artista seguido.");
                }
                catch (NegocioException excecao)
                {
                    Pausar(excecao.Message);
                }
                break;
        }
    }
    private void MenuPlaylists()
    {
        while (true)
        {
            Console.Clear();
            Cabecalho("PLAYLISTS", "MENU");
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
                default: Pausar("Opção inválida."); break;
            }
        }
    }

    private void MenuMoodMatch()
    {
        Console.Clear();
        Cabecalho("MOOD MATCH", "SENTIMENTO");
        Console.WriteLine("Como você está se sentindo hoje?");
        Console.WriteLine("1 - Alegre");
        Console.WriteLine("2 - Triste");
        Console.WriteLine("3 - Energetico");
        Console.WriteLine("4 - Motivado");
        Console.WriteLine("5 - Reflexivo");
        Console.WriteLine("6 - Preguicoso");
        Console.WriteLine("7 - Enfurecido");
        Separador();
        Console.Write("Digite o número escolhido: ");

        if (!int.TryParse(Console.ReadLine(), out var emocaoId) || emocaoId < 1 || emocaoId > 7)
        {
            Pausar("Emoção inválida.");
            return;
        }

        Console.Clear();
        Cabecalho("MOOD MATCH", "ATIVIDADE");
        Console.WriteLine("O que você está fazendo agora?");
        Console.WriteLine("1 - Caminhando");
        Console.WriteLine("2 - Cozinhando");
        Console.WriteLine("3 - Jogando");
        Console.WriteLine("4 - Estudando");
        Console.WriteLine("5 - Relaxando");
        Console.WriteLine("6 - Trabalhando");
        Separador();
        Console.Write("Digite o número escolhido: ");

        if (!int.TryParse(Console.ReadLine(), out var atividadeId) || atividadeId < 1 || atividadeId > 6)
        {
            Pausar("Atividade inválida.");
            return;
        }

        var musicas = _listagem.ListarMusicasPorMood(emocaoId, atividadeId);
        Console.Clear();
        Cabecalho("PLAYLIST SUGERIDA", "MOOD MATCH");
        Console.WriteLine($"Emoção: {NomeEmocao(emocaoId)}");
        Console.WriteLine($"Atividade: {NomeAtividade(atividadeId)}");
        Console.WriteLine();
        Mostrar(musicas.Select((musica, indice) =>
            $"{indice + 1} - {musica.Titulo}"));

        if (musicas.Count == 0)
        {
            Pausar("Nenhuma música encontrada para essa combinação.");
            return;
        }

        Console.WriteLine();
        Console.WriteLine("1 - Salvar playlist");
        Console.WriteLine("0 - Voltar");
        Separador();

        if (Console.ReadLine() == "1")
            SalvarPlaylistMood(musicas);
    }

    #endregion





    #region DISPLAY MUSICA
    private void TocarMusicasPlayList(List<Midia> musicasPlaylist)
    {
        for (int indice = 0; indice < musicasPlaylist.Count;)
        {

            if (indice < 0)
            {
                Destaque("Não há músicas anteriores nesta PlayList.", ConsoleColor.DarkRed);
                Console.WriteLine();
                indice++;
                Console.Clear();
                continue;
            }
            Console.Clear();
            Midia midia = musicasPlaylist[indice];

            ConsoleKey resultado = DisplayMusica(midia.Duracao, midia.Titulo, midia.IdMidia);

            switch (resultado)
            {
                case ConsoleKey.LeftArrow:
                    indice--;
                    break;

                case ConsoleKey.RightArrow:
                    indice++;
                    continue;
            }
        }
    }

    private ConsoleKey DisplayMusica(TimeSpan duracao, string titulo, int id)
    {
        Cabecalho(titulo, Convert.ToString(id));
        Console.WriteLine(titulo, "⏱" + duracao + "\n");
        Console.WriteLine("|⫷      ᐅ     ⫸|");

        Stopwatch cronometro = Stopwatch.StartNew();

        while (cronometro.Elapsed < duracao)
        {
            if (Console.KeyAvailable)
            {
                ConsoleKey tecla = Console.ReadKey(true).Key;

                if (tecla == ConsoleKey.LeftArrow)
                    return ConsoleKey.LeftArrow;

                if (tecla == ConsoleKey.RightArrow)
                    return ConsoleKey.RightArrow;
            }

            Thread.Sleep(50);
        }

        return ConsoleKey.RightArrow;
    }

    #endregion




    #region LISTAGENS
    private void VerAlbunsEMusicas(int artistaId, string nomeArtista)
    {
        List<Midia> midias = new List<Midia>();
        Console.Clear();
        Cabecalho($"ÁLBUNS E MÚSICAS ", $"{nomeArtista.ToUpperInvariant()}");
        var albuns = _listagem.ListarAlbunsDoArtista(artistaId);
        if (albuns.Count == 0)
        {
            Pausar("Este artista ainda não possui álbuns cadastrados.");
            return;
        }

        foreach (var album in albuns)
        {
            Console.WriteLine();
            Console.WriteLine($"Album: {album.Nome}");
            var musicas = _listagem.ListarMusicasDoAlbum(album.IdAlbum);
            for (int indice = 0; indice < musicas.Count; indice++)
            {
                midias.Add(musicas[indice]);
            }

            Mostrar(musicas.Select(musica =>
                $"  ID {musica.IdMidia} - {musica.Titulo}"));
        }

        Console.WriteLine();
        Console.WriteLine("1 - Curtir música");
        Console.WriteLine("2 - Adicionar música à playlist");
        Console.WriteLine($"3 - Tocar {nomeArtista.ToUpperInvariant()}");
        Console.WriteLine("4 - Voltar");
        Separador();

        switch (Console.ReadLine())
        {
            case "1": CurtirMusica(artistaId); break;
            case "2": AdicionarMusicaAPlaylist(artistaId); break;
            case "3": TocarMusicasPlayList(midias); break;
            case "4": return;
            default: Pausar("Opção inválida."); break;
        }
    }


    private void CurtirMusica(int artistaId)
    {
        var musicas = _listagem.ListarMusicasDoArtista(artistaId);
        Console.Write("ID da musica para curtir (0 para voltar): ");
        if (!int.TryParse(Console.ReadLine(), out var midiaId) || midiaId == 0) return;

        if (!musicas.Any(musica => musica.IdMidia == midiaId))
        {
            Pausar("A música não pertence a este artista.");
            return;
        }

        try
        {
            _artistaBusiness.CurtirMusica(_usuarioLogado!.IdUsuario, midiaId);
            Pausar("Música curtida.");
        }
        catch (NegocioException excecao)
        {
            Pausar(excecao.Message);
        }
    }

    private void ListarArtistasSeguidos()
    {
        Console.Clear();
        Cabecalho("ARTISTAS", "SEGUIDOS");
        var artistas = _listagem.ListarArtistasSeguidos(_usuarioLogado!.IdUsuario);
        Mostrar(artistas.Select((artista, indice) =>
            $"{indice + 1} - {artista.Nome}"));
        Separador();
        Console.Write("Escolha um artista (0 para voltar): ");
        if (!int.TryParse(Console.ReadLine(), out var escolha) || escolha == 0)
            return;
        if (escolha < 1 || escolha > artistas.Count)
        {
            Pausar("Artista inválido.");
            return;
        }

        var artista = artistas[escolha - 1];
        Console.Clear();
        Cabecalho($"ARTISTA: {artista.Nome.ToUpperInvariant()}", "MENU");
        Console.WriteLine("1 - Visitar página do artista");
        Console.WriteLine("2 - Deixar de seguir o artista");
        Console.WriteLine("0 - Voltar");
        Separador();

        switch (Console.ReadLine())
        {
            case "1": VerAlbunsEMusicas(artista.IdArtista, artista.Nome); break;
            case "2":
                try
                {
                    _artistaBusiness.DeixarDeSeguirArtista(_usuarioLogado.IdUsuario, artista.IdArtista);
                    Pausar("Você deixou de seguir o artista.");
                }
                catch (NegocioException excecao)
                {
                    Pausar(excecao.Message);
                }
                break;
        }
    }

    private void ListarMusicasCurtidas()
    {
        Console.Clear();
        Cabecalho("MÚSICAS", "CURTIDAS");
        var musicas = _listagem.ListarMusicasCurtidas(_usuarioLogado!.IdUsuario);
        Mostrar(musicas.Select((musica, indice) =>
            $"{indice + 1} - {musica.Titulo} (ID: {musica.IdMidia})"));
        Separador();
        Console.Write("ID da música para deixar de curtir (0 para voltar): ");
        if (int.TryParse(Console.ReadLine(), out var midiaId) && midiaId != 0)
        {
            try
            {
                _artistaBusiness.DescurtirMusica(_usuarioLogado.IdUsuario, midiaId);
            }
            catch (NegocioException excecao)
            {
                Console.WriteLine(excecao.Message);
            }
        }
        Pausar();
    }
    #endregion




    #region MOOD MATCH
    private static string NomeEmocao(int emocaoId)
    {
        return emocaoId switch
        {
            1 => "Alegre",
            2 => "Triste",
            3 => "Energetico",
            4 => "Motivado",
            5 => "Reflexivo",
            6 => "Preguicoso",
            7 => "Enfurecido",
            _ => "Desconhecida"
        };
    }

    private static string NomeAtividade(int atividadeId)
    {
        return atividadeId switch
        {
            1 => "Caminhando",
            2 => "Cozinhando",
            3 => "Jogando",
            4 => "Estudando",
            5 => "Relaxando",
            6 => "Trabalhando",
            _ => "Desconhecida"
        };
    }

    private void SalvarPlaylistMood(IReadOnlyList<Midia> musicas)
    {
        Console.Write("Digite o nome da playlist: ");
        var nomePlaylist = Console.ReadLine() ?? string.Empty;

        Playlist playlist;
        try
        {
            playlist = _playlistBusiness.CriarPlaylist(nomePlaylist, _usuarioLogado!.IdUsuario);
        }
        catch (NegocioException excecao)
        {
            Pausar(excecao.Message);
            return;
        }

        var musicasAdicionadas = 0;
        foreach (var musica in musicas)
        {
            try
            {
                _playlistBusiness.AdicionarMusica(_usuarioLogado.IdUsuario, playlist.IdPlaylist, musica.IdMidia);
                musicasAdicionadas++;
            }
            catch (NegocioException)
            {

            }
        }

        Pausar($"Playlist '{playlist.NomePlaylist}' salva com {musicasAdicionadas} música(s).");
    }

    #endregion




    #region PLAYLIST
    private void AdicionarMusicaAPlaylist(int artistaId)
    {
        var musicas = _listagem.ListarMusicasDoArtista(artistaId);
        var playlists = _listagem.ListarPlaylists(_usuarioLogado!.IdUsuario);
        if (playlists.Count == 0)
        {
            Pausar("Você ainda não possui playlists.");
            return;
        }

        Console.Write("ID da música: ");
        if (!int.TryParse(Console.ReadLine(), out var midiaId) ||
            !musicas.Any(musica => musica.IdMidia == midiaId))
        {
            Pausar("Música inválida para este artista.");
            return;
        }

        Console.WriteLine("Playlists disponíveis:");
        Mostrar(playlists.Select(playlist =>
            $"ID {playlist.IdPlaylist} - {playlist.NomePlaylist}"));
        Console.Write("ID da playlist: ");
        if (!int.TryParse(Console.ReadLine(), out var playlistId)) return;

        try
        {
            _playlistBusiness.AdicionarMusica(_usuarioLogado.IdUsuario, playlistId, midiaId);
            Pausar("Música adicionada à playlist.");
        }
        catch (NegocioException excecao)
        {
            Pausar(excecao.Message);
        }
    }

    private void CriarPlaylist()
    {
        Console.Write("Nome da playlist: ");
        var nome = Console.ReadLine() ?? string.Empty;

        try
        {
            _playlistBusiness.CriarPlaylist(nome, _usuarioLogado!.IdUsuario);
            Pausar("Playlist criada.");
        }
        catch (NegocioException excecao)
        {
            Pausar(excecao.Message);
        }
    }

    private void VerPlaylist(IReadOnlyList<Playlist> playlists)
    {
        Console.Write("ID da playlist (0 para voltar): ");
        if (!int.TryParse(Console.ReadLine(), out var playlistId) || playlistId == 0) return;
        if (!playlists.Any(playlist => playlist.IdPlaylist == playlistId))
        {
            Pausar("Playlist inválida.");
            return;
        }

        while (true)
        {
            Console.Clear();
            Cabecalho("MÚSICAS", "PLAYLIST");
            var musicas = _listagem.ListarMusicasDaPlaylist(playlistId);
            Mostrar(musicas.Select((musica, indice) =>
                $"{indice + 1} - {musica.Titulo} (ID: {musica.IdMidia})"));
            Console.WriteLine();
            Console.WriteLine("1 - Adicionar música");
            Console.WriteLine("2 - Remover música");
            Console.WriteLine("3 - Tocar Playlist");
            Console.WriteLine("0 - Voltar");
            Separador();
            var acao = Console.ReadLine();
            if (acao == "0") return;

            Console.Write("ID da música: ");
            if (!int.TryParse(Console.ReadLine(), out var midiaId))
            {
                Pausar("ID inválido.");
                continue;
            }

            try
            {
                if (acao == "1")
                    _playlistBusiness.AdicionarMusica(_usuarioLogado!.IdUsuario, playlistId, midiaId);
                else if (acao == "2")
                    _playlistBusiness.RemoverMusica(_usuarioLogado!.IdUsuario, playlistId, midiaId);
                else if (acao == "3")
                {
                    TocarMusicasPlayList(musicas);
                }


                Pausar("Operacao realizada.");
            }
            catch (NegocioException excecao)
            {
                Pausar(excecao.Message);
            }
        }
    }

    private void RemoverPlaylist(IReadOnlyList<Playlist> playlists)
    {
        Console.Write("ID da playlist para remover (0 para voltar): ");
        if (!int.TryParse(Console.ReadLine(), out var playlistId) || playlistId == 0) return;

        try
        {
            _playlistBusiness.RemoverPlaylist(_usuarioLogado!.IdUsuario, playlistId);
            Pausar("Playlist removida.");
        }
        catch (NegocioException excecao)
        {
            Pausar(excecao.Message);
        }
    }
    #endregion




    #region METODOS PRIVADOS
    private static void Cabecalho(string titulo, string subMenu)
    {
        Separador();
        Destaque(titulo, ConsoleColor.DarkBlue); Console.Write(" - "); Destaque(subMenu, ConsoleColor.Gray);
        Console.WriteLine();
        Separador();
    }

    private static void Separador()
    {
        Destaque("===============================", ConsoleColor.DarkGreen);
        Console.WriteLine();
    }
    private static void Destaque(string text, ConsoleColor cor)
    {
        Console.ForegroundColor = cor;
        Console.Write(text);
        Console.ResetColor();
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
            Destaque(mensagem, ConsoleColor.DarkCyan);
        Console.ReadKey();
    }
    #endregion
}