using Microsoft.IdentityModel.Protocols.Configuration;
using MySql.Data.MySqlClient;

public class Menu
{
    private Usuario _usuarioLogado;
    private readonly ListagemDAO _listagemDAO = new();

    UsuarioDAO usuarioDAO = new();
    public void Iniciar()
    {
        while (true)
        {
            Console.Clear();
            Console.WriteLine("===== SPOTIFEI =====");
            Console.WriteLine("1 - Entrar");
            Console.WriteLine("2 - Criar Conta");
            Console.WriteLine("0 - Sair");

            string opcao = Console.ReadLine();

            switch (opcao)
            {
                case "1":
                    _usuarioLogado = Entrar();

                    if (_usuarioLogado != null)
                    {
                        //MenuListagensBanco();
                        MenuUsuario();
                    }
                    break;
                
                case "2":
                    CadastrarUsuario();
                    break;

                case "0":
                    return;

                default:
                    Console.WriteLine("Opção inválida.");
                    Console.ReadKey();
                    break;
            }
        }
    }

    private Usuario Entrar()
    {
        Console.Clear();

        Console.WriteLine("Digite seu e-mail:");
        string email = Console.ReadLine();

        Console.WriteLine("Digite sua senha:");
        string senha = Console.ReadLine();

        Usuario usuario = new UsuarioDAO().ValidarLogin(email, senha);

        if (usuario == null)
        {
            Console.WriteLine("E-mail ou senha inválidos.");
            Console.ReadKey();
            return null;
        }


        Console.WriteLine($"Bem-vindo {usuario.Nome}!");
        Console.WriteLine("Pressione qualquer tecla para continuar...");
        Console.ReadKey();

        return usuario;
    }

    private void CadastrarUsuario()
    {
        Console.Clear();

        Console.WriteLine("Nome:");
        string nome = Console.ReadLine();

        Console.WriteLine("Email:");
        string email = Console.ReadLine();

        Console.WriteLine("Senha:");
        string senha = Console.ReadLine();

        var planos = Plano.Planos.ToList();

        Console.WriteLine("\nEscolha um plano:");

        var planosDisponiveis = _listagemDAO.ListarPlanos();

        MostrarItens(_listagemDAO.ListarPlanos());

        string opcao = Console.ReadLine();

        if (int.TryParse(opcao, out int planoEscolhido) && planoEscolhido >= 1 && planoEscolhido <= planosDisponiveis.Count)
        {
            var planoParaSalvar = planosDisponiveis[planoEscolhido - 1];
            var planoDecimal = Convert.ToDecimal(planoParaSalvar);
            var planoObjeto = new Plano(planoParaSalvar.Id, planoParaSalvar.Nome, planoDecimal);
            Usuario usuario = new Usuario(
                nome,
                email,
                senha,
                planoObjeto
            );

            usuarioDAO.AdicionarUsuarioDAO(usuario);

            Console.WriteLine("Usuário cadastrado com sucesso!");
        }
        else
        {
            Console.WriteLine("Plano inválido.");
        }
        Console.ReadKey();
    }
    private void MenuUsuario()
    {
        while (_usuarioLogado != null)
        {
            Console.Clear();
            Console.WriteLine($"Usuário: {_usuarioLogado.Nome}");
            Console.WriteLine($"Plano: {_usuarioLogado.Plano.Descricao}");
            Console.WriteLine();
            Console.WriteLine("1 - Ver Artistas");
            Console.WriteLine("2 - Ver Biblioteca");
            Console.WriteLine("3 - Ver Playlists");
            Console.WriteLine("0 - Sair da Conta");

            string opcao = Console.ReadLine();

            switch (opcao)
            {
                case "1":
                    //MostrarArtistas();
                    ListarArtistasDisponiveisBanco();
                    Console.WriteLine("O que você gostaria de fazer?");
                    Console.WriteLine("1 - Seguir um artista | 2 - Ver os álbuns de um artista | 3 - Sair");
                    string seguirOuVer = Console.ReadLine();

                    switch (seguirOuVer)
                    {
                        case "1":
                            SeguirArtistaDAO();
                        break;

                        case "2":
                            InteragirAlbunsArtistaDAO();
                        break;

                        case "3":
                        break;

                        default:
                            Console.WriteLine("Opção inválida. Por favor, insira um número válido");
                        break;
                    }
                    break;
                    
                case "2":
                    MostrarBibliotecaDAO();
                    AcoesBibliotecaDAO();
                    break;

                case "3":
                    ListarPlaylistsBanco();
                    Console.WriteLine("O que você gostaria de fazer?");
                    Console.WriteLine("1 - Acessar uma playlist | 2 - Excluir uma playlist | 3 - Criar uma playlist | 4 - Voltar");
                    string qualOpcao = Console.ReadLine();

                    switch (qualOpcao)
                    {
                        case "1":
                            ListarMusicasDePlaylistBanco();
                        break;

                        case "2":
                            DeletarPlaylistDAO();
                        break;

                        case "3":
                            CriarPlaylistDao();
                        break;

                        case "4":

                        break;

                        default:
                            Console.WriteLine("Opção inválida");
                        break;
                    }
                    break;

                case "0":
                    _usuarioLogado = null;
                    break;
            }
        }
    }


    private void MostrarArtistas()
    {
        Console.Clear();
        var artistas = Artista.Artistas.ToList();
        
        
        for (int i = 0; i < artistas.Count; i++)
        {
            Console.WriteLine($"{i + 1} - {artistas[i].Nome}");
        }

    }

    private void SeguirArtista()
    {
        var artistas = Artista.Artistas.ToList();

        Console.WriteLine("Digite o número do artista que gostaria de seguir:");
            
        if (int.TryParse(Console.ReadLine(), out int artistaSeguir))
        {
            if (artistaSeguir > 0 && artistaSeguir <= artistas.Count)
            {
                if (!_usuarioLogado.ArtistasSeguidos.Contains(artistas[artistaSeguir - 1]))
                {
                        _usuarioLogado.SegueArtista(artistas[artistaSeguir - 1]);

                        new UsuarioDAO().AdicionarUsuarioSegArtista(_usuarioLogado, artistas[artistaSeguir-1]);
                        Console.WriteLine("Pressione qualquer tecla para continuar");
                        Console.ReadKey();
                }
                else
                {
                    Console.WriteLine("Você já segue esse artista");
                }
                
            }
            else
            {
                Console.WriteLine("Número inserido inválido");
            }
        }
        else
        {
            Console.WriteLine("Erro! Por favor, insira um número válido");
        }  
    }

#region Métodos DAO
    private void SeguirArtistaDAO()
    {
        // 1. Busca os artistas direto do banco (precisa retornar uma List<ItemListagem>)
        List<ItemListagem> artistasDisponiveis = _listagemDAO.ListarArtistasDisponiveis();

        Console.WriteLine("\nDigite o número do artista que gostaria de seguir:");
        
        if (int.TryParse(Console.ReadLine(), out int indiceEscolhido))
        {
            // Valida se o índice digitado faz sentido na lista
            if (indiceEscolhido > 0 && indiceEscolhido <= artistasDisponiveis.Count)
            {
                // Pega o objeto correspondente à escolha do usuário
                var artistaSelecionado = artistasDisponiveis[indiceEscolhido - 1];

                // 2. Busca a lista de quem ele JÁ segue no banco
                List<ItemListagem> artistasSeguidos = _listagemDAO.ListarArtistasSeguidos(_usuarioLogado.IdUsuario);

                // 3. Verifica se o ID do artista selecionado já está na lista de seguidos
                bool jaSegue = artistasSeguidos.Any(a => a.Id == artistaSelecionado.Id);

                if (!jaSegue)
                {
                    // Instancia o DAO para salvar no banco
                    var usuarioDAO = new UsuarioDAO();
                    
                    // Reaproveitamos o método que você já criou! 
                    // Se o seu método pede os objetos completos, podemos criá-los temporariamente:
                    var artistaObjeto = new Artista(artistaSelecionado.Id, artistaSelecionado.Nome);

                    usuarioDAO.AdicionarUsuarioSegArtista(_usuarioLogado, artistaObjeto);

                    Console.WriteLine("\nPressione qualquer tecla para continuar...");
                    Console.ReadKey();
                }
                else
                {
                    Console.WriteLine("Você já segue esse artista!");
                    Console.WriteLine("\nPressione qualquer tecla para continuar...");
                    Console.ReadKey();
                }
            }
            else
            {
                Console.WriteLine("Número inserido inválido.");
                Console.WriteLine("\nPressione qualquer tecla para continuar...");
                Console.ReadKey();
            }
        }
        else
        {
            Console.WriteLine("Erro! Por favor, insira um número válido.");
            Console.WriteLine("\nPressione qualquer tecla para continuar...");
            Console.ReadKey();
        }  
    }

    private void InteragirAlbunsArtistaDAO()
    {
        Console.WriteLine("Gostaria de ver os álbuns de qual artista?");
        List<ItemListagem> artistasDisponiveis = _listagemDAO.ListarArtistasDisponiveis();

        if (!int.TryParse(Console.ReadLine(), out int qualArtista) || qualArtista <= 0 || qualArtista > artistasDisponiveis.Count)
        {
            Console.WriteLine("Opção inválida ou artista não existe! Retornando...");
            Console.ReadKey();
            return; // Sai do método se o artista for inválido
        }

        // Busca o artista selecionado
        var artistaEscolhido = artistasDisponiveis[qualArtista - 1];
        var artistaObjeto = new Artista(artistaEscolhido.Id, artistaEscolhido.Nome);
        ListarAlbumArtistaEspecifico(artistaObjeto);

        // Obtém a lista de álbuns daquele artista específico
        var albunsArtista = _listagemDAO.ListarAlbunsDeArtista(artistaObjeto.IdArtista);

        if (albunsArtista.Count == 0)
        {
            Console.WriteLine("Este artista ainda não possui álbuns cadastrados.");
            Console.ReadKey();
            return;
        }

        Console.WriteLine("\nGostaria de ver as músicas de qual álbum?");

        if (!int.TryParse(Console.ReadLine(), out int qualAlbum) || qualAlbum <= 0 || qualAlbum > albunsArtista.Count)
        {
            Console.WriteLine("Opção inválida ou álbum não existe! Retornando...");
            Console.ReadKey();
            return; // Sai do método se o álbum for inválido
        }

        var albumEscolhido = albunsArtista[qualAlbum - 1];
        var albumObjeto = new Album(albumEscolhido.Id, albumEscolhido.Nome);

        ListarMusicasDeAlbum(albumObjeto);

        var musicasDoAlbum = _listagemDAO.ListarMusicasDeAlbum(albumEscolhido.Id);

        Console.WriteLine("\nO que gostaria de fazer?\n1 - Curtir uma música | 2 - Adicionar música à playlist | 0 - Voltar");
        string qualOpcao = Console.ReadLine();

        if (qualOpcao == "0")
        {
            return;
        }

        if (qualOpcao == "1")
        {
            Console.WriteLine("Digite o número da música que gostaria de curtir:");

            if (int.TryParse(Console.ReadLine(), out int musicaEscolhida))
            {
                if (musicaEscolhida > 0 && musicaEscolhida <= musicasDoAlbum.Count)
                {
                    
                    var musicaParaCurtir = musicasDoAlbum[musicaEscolhida - 1];
                    var musicaObjeto = new Midia(musicaParaCurtir.Id);

                    bool jaCurte = _listagemDAO.ListarMusicasCurtidas(_usuarioLogado.IdUsuario).Any(m => m.Id == musicaParaCurtir.Id);
                    
                    if (!jaCurte)
                    {
                        new UsuarioDAO().AdicionarMusicaCurtida(_usuarioLogado, musicaObjeto);
                        Console.WriteLine("Pressione qualquer tecla para continuar");
                        Console.ReadKey();
                    }
                    else
                    {
                        Console.WriteLine("Você já curte essa música!");
                        Console.WriteLine("Pressione qualquer tecla para continuar...");
                        Console.ReadKey();
                    }
                }
                else
                {
                    Console.WriteLine("Número escolhido inválido!");
                    Console.ReadKey();
                }
            }    
            else
            {
                Console.WriteLine("Erro! Por favor, insira um número válido");
                Console.ReadKey();
            }
        }        

        if (qualOpcao == "2")
        {
            Console.WriteLine("Digite o número da música que gostaria de adicionar:");
            
            if(int.TryParse(Console.ReadLine(), out int musicaEscolhida) && musicaEscolhida > 0 && musicaEscolhida <= musicasDoAlbum.Count())
            {
                var musicaParaAdd = musicasDoAlbum[musicaEscolhida - 1];
                var musicaObjeto = new Midia(musicaParaAdd.Id);
                var playlistsUsuario = _listagemDAO.ListarPlaylists(_usuarioLogado.IdUsuario);

                if (playlistsUsuario.Count == 0)
                {
                    Console.WriteLine("Você não possui nenhuma playlist criada para adicionar músicas!");
                    Console.WriteLine("Pressione qualquer tecla para continuar...");
                    Console.ReadKey();
                    return; 
                }

                Console.WriteLine("Gostaria de adicionar essa música a qual playlist?");

                for (int i = 0; i < playlistsUsuario.Count; i++)
                {
                    Console.WriteLine($"{i + 1} - {playlistsUsuario[i].Nome}");
                }

                if (int.TryParse(Console.ReadLine(), out int playlistEscolhida))
                {
                    if (playlistEscolhida > 0 && playlistEscolhida <= playlistsUsuario.Count())
                    {
                        var playlistParaAdd = playlistsUsuario[playlistEscolhida - 1];
                        var playlistObjeto = new Playlist(playlistParaAdd.Id);

                        List<ItemListagem> musicaDaPlaylist = _listagemDAO.ListarMusicasDePlaylist(playlistParaAdd.Id);
                        var jaTem = musicaDaPlaylist.Any(m => m.Id == musicaParaAdd.Id);

                        if (!jaTem)
                        {
                            new UsuarioDAO().AdicionarMusicaPlaylist(musicaObjeto, playlistObjeto);
                            
                            Console.WriteLine("Pressione qualquer tecla para continuar...");
                            Console.ReadKey();
                        }
                        else
                        {
                            Console.WriteLine("Essa música já pertence a essa playlist!");
                            Console.WriteLine("Pressione qualquer tecla para continuar...");
                            Console.ReadKey();
                        }
                    }
                    else
                    {
                        Console.WriteLine("Número escolhido inválido!");
                        Console.WriteLine("Pressione qualquer tecla para continuar...");
                        Console.ReadKey();
                    }
                }
                else
                {
                    Console.WriteLine("Número escolhido inválido!");
                    Console.WriteLine("Pressione qualquer tecla para continuar...");
                    Console.ReadKey();
                }
            }
            else
            {
            Console.WriteLine("Número escolhido inválido!");
            Console.WriteLine("Pressione qualquer tecla para continuar...");
            Console.ReadKey();
            }
        }
    }

    private void MostrarBibliotecaDAO()
    {
        Console.Clear();
        Console.WriteLine("========== SUA BIBLIOTECA =========");

        var playlists = _usuarioLogado.PlaylistsUsuario.ToList();

        ListarPlaylistsBanco();

        Console.WriteLine("");

        ListarArtistasSeguidosBanco();

        Console.WriteLine("");

        ListarMusicasCurtidasBanco();

    }

    private void AcoesBibliotecaDAO()
    {
        Console.WriteLine("\nO que você gostaria de fazer?");
        Console.WriteLine("1 - Acessar uma playlist");
        Console.WriteLine("2 - Deletar uma playlist");
        Console.WriteLine("3 - Visitar a página de um artista");
        Console.WriteLine("4 - Deixar de seguir um artista");
        Console.WriteLine("5 - Deixar de curtir uma música");
        Console.WriteLine("0 - Voltar ao menu principal");
        
        string qualEscolha = Console.ReadLine();

        switch (qualEscolha)
        {
            case "1":
                ListarMusicasDePlaylistBanco();
            
            break;  

            case "2":
                ListarPlaylistsBanco();
                DeletarPlaylistDAO();
            break;

            case "3":
                ListarArtistasDisponiveisBanco();
                Console.WriteLine("O que você gostaria de fazer?");
                Console.WriteLine("1 - Seguir um artista | 2 - Ver os álbuns de um artista | 3 - Sair");
                string seguirOuVer = Console.ReadLine();

                switch (seguirOuVer)
                {
                    case "1":
                        SeguirArtistaDAO();
                    break;

                    case "2":
                        InteragirAlbunsArtistaDAO();
                    break;

                    case "3":
                    break;

                    default:
                        Console.WriteLine("Opção inválida. Por favor, insira um número válido");
                    break;
                }
            break;

            case "4":
                ListarArtistasSeguidosBanco();
                DeixarDeSeguirDAO();
            break;

            case "5":
                ListarMusicasCurtidasBanco();
                DeixarDeCurtirDAO();
            break;

            case "0":
            break;
        }
    }

    private void CriarPlaylistDao()
    {
        Console.WriteLine("Digite o nome da playlis que gostaria de criar:");
        string nomePlaylist = Console.ReadLine();

        var playlistObjeto = new Playlist(nomePlaylist);

        new UsuarioDAO().AdicionarPlaylist(_usuarioLogado, playlistObjeto);

        Console.WriteLine("Pressione qualquer tecla para continuar...");
        Console.ReadKey();
    }

    private void DeletarPlaylistDAO()
    {
        Console.WriteLine("Qual playlist gostaria de deletar?");

        var playlists = _listagemDAO.ListarPlaylists(_usuarioLogado.IdUsuario);

        if (!int.TryParse(Console.ReadLine(), out int qualPlaylist) || qualPlaylist <= 0 || qualPlaylist > playlists.Count())
        {
            Console.WriteLine("Entrada ou playlist não existe! retornando...");
            Console.ReadKey();
            return;
        }

        var playlistEscolhida = playlists[qualPlaylist - 1];
        var musicasPlaylist = _listagemDAO.ListarMusicasDePlaylist(playlistEscolhida.Id);

        var playlistObjeto = new Playlist(playlistEscolhida.Id);

        if (musicasPlaylist != null && musicasPlaylist.Count > 0) 
        {
            Console.WriteLine("Você não pode deletar uma playlist que contém musicas!");
            Console.WriteLine("Pressione qualquer tecla para continuar...");
            Console.ReadKey();
        }
        else
        {
            new UsuarioDAO().DeletarPlaylist(_usuarioLogado, playlistObjeto);

            Console.WriteLine("Pressione qualquer tecla para continuar...");
            Console.ReadKey();
        }
    }
    
    private void DeixarDeSeguirDAO()
    {
        Console.WriteLine("\nQual artista você gostaria de deixar de seguir?");

        var artistasSeguidos = _listagemDAO.ListarArtistasSeguidos(_usuarioLogado.IdUsuario);

        if (int.TryParse(Console.ReadLine(), out int artistaEscolhido))
        {
            if (artistaEscolhido > 0 && artistaEscolhido <= artistasSeguidos.Count)
            {
                var artistaParaDel = artistasSeguidos[artistaEscolhido - 1];
                var artistaObjeto = new Artista(artistaParaDel.Id);

                new UsuarioDAO().DeletarUsuarioSegArtista(_usuarioLogado, artistaObjeto);

                Console.WriteLine("Pressione qualquer tecla para continuar...");
                Console.ReadKey();
            }
            else
            {
                Console.WriteLine("Entrada inválida! Digite um dos números disponíveis.");
                Console.WriteLine("Pressione qualquer tecla para continuar...");
                Console.ReadKey();
            }
        }
        else
        {
            Console.WriteLine("Entrada inválida! Digite um dos números válido.");
            Console.WriteLine("Pressione qualquer tecla para continuar...");
            Console.ReadKey();
        }
    }

    private void DeixarDeCurtirDAO()
    {
        Console.WriteLine("Qual música você gostaria de deixar de curtir?");

        var musicasCurtidas = _listagemDAO.ListarMusicasCurtidas(_usuarioLogado.IdUsuario);

        if (int.TryParse(Console.ReadLine(), out int musicaEscolhida))
        {
            if (musicaEscolhida > 0 && musicaEscolhida <= musicasCurtidas.Count)
            {
                var musicaParaDel = musicasCurtidas[musicaEscolhida - 1];

                var musicaObjeto = new Midia(musicaParaDel.Id);

                new UsuarioDAO().DeletarMusicaCurtida(_usuarioLogado, musicaObjeto);

                Console.WriteLine("Pressione qualquer tecla para continuar...");
                Console.ReadKey();
            }
            else
            {
                Console.WriteLine("Entrada inválida! Digite um dos números disponíveis");
                Console.WriteLine("Pressione qualquer tecla para continuar...");
                Console.ReadKey();
            }
        }
        else
        {
            Console.WriteLine("Entrada inválida! Digite um número válido");
            Console.WriteLine("Pressione qualquer tecla para continuar...");
            Console.ReadKey();
        }
    }

    private void InteragirAlbunsArtista()
    {
        Console.WriteLine("Gostaria de ver os álbuns de qual artista?");
        var artistas = Artista.Artistas.ToList();

        if (!int.TryParse(Console.ReadLine(), out int qualArtista) || qualArtista <= 0 || qualArtista > artistas.Count)
        {
            Console.WriteLine("Opção inválida ou artista não existe! Retornando...");
            Console.ReadKey();
            return; // Sai do método se o artista for inválido
        }

        // Busca o artista selecionado
        var artistaEscolhido = artistas[qualArtista - 1];
        artistaEscolhido.MostrarAlbuns();

        // Obtém a lista de álbuns daquele artista específico
        var albunsArtista = artistaEscolhido.AlbunsArtista.ToList();

        if (albunsArtista.Count == 0)
        {
            Console.WriteLine("Este artista ainda não possui álbuns cadastrados.");
            Console.ReadKey();
            return;
        }

        Console.WriteLine("\nGostaria de ver as músicas de qual álbum?");

        if (!int.TryParse(Console.ReadLine(), out int qualAlbum) || qualAlbum <= 0 || qualAlbum > albunsArtista.Count)
        {
            Console.WriteLine("Opção inválida ou álbum não existe! Retornando...");
            Console.ReadKey();
            return; // Sai do método se o álbum for inválido
        }

        var albumEscolhido = albunsArtista[qualAlbum - 1];
        albumEscolhido.MostrarMidias();

        var musicasDoAlbum = albumEscolhido.MidiasAlbum.ToList();

        Console.WriteLine("\nO que gostaria de fazer?\n1 - Curtir uma música | 2 - Adicionar música à playlist | 0 - Voltar");
        string qualOpcao = Console.ReadLine();

        if (qualOpcao == "0")
        {
            return;
        }

        if (qualOpcao == "1")
        {
            Console.WriteLine("Digite o número da música que gostaria de curtir:");

            if (int.TryParse(Console.ReadLine(), out int musicaEscolhida))
            {
                if (musicaEscolhida > 0 && musicaEscolhida <= musicasDoAlbum.Count)
                {
                    var musicaParaCurtir = musicasDoAlbum[musicaEscolhida - 1];
                    _usuarioLogado.CurteMusica(musicaParaCurtir); // Removido o ";" duplicado que estava no seu código
                    Console.WriteLine($"Música '{musicaParaCurtir.Titulo}' curtida com sucesso!");

                    new UsuarioDAO().AdicionarMusicaCurtida(_usuarioLogado, musicaParaCurtir);
                    Console.WriteLine("Pressione qualquer tecla para continuar");
                    Console.ReadKey();
                }
                else
                {
                    Console.WriteLine("Número escolhido inválido!");
                    Console.ReadKey();
                }
            }    
            else
            {
                Console.WriteLine("Erro! Por favor, insira um número válido");
                Console.ReadKey();
            }
        }        

        if (qualOpcao == "2")
        {
            Console.WriteLine("Digite o número da música que gostaria de adicionar:");
            
            if(int.TryParse(Console.ReadLine(), out int musicaEscolhida) && musicaEscolhida > 0 && musicaEscolhida <= musicasDoAlbum.Count())
            {
                var musicaParaAdd = musicasDoAlbum[musicaEscolhida - 1];
                var playlistUsuario = _usuarioLogado.PlaylistsUsuario.ToList();
                Console.WriteLine("Gostaria de adicionar essa música a qual playlist?");

                for (int i = 0; i < _usuarioLogado.PlaylistsUsuario.Count; i++)
                {
                    Console.WriteLine($"{i+1} | {playlistUsuario[i].NomePlaylist}");
                }

                int playlistEscolhida = int.Parse(Console.ReadLine());

                playlistUsuario[playlistEscolhida - 1].AddMusPlaylist(musicaParaAdd);

                new UsuarioDAO().AdicionarMusicaPlaylist(musicaParaAdd, playlistUsuario[playlistEscolhida - 1]);
            }
        }

    }
#endregion
    
#region Métodos base sistema
    private void DeixarDeSeguir()
    {
        Console.WriteLine("Qual artista você gostaria de deixar de seguir?");

        var artistas = _usuarioLogado.ArtistasSeguidos.ToList();

        for (int i = 0; i < artistas.Count; i++)
        {
            Console.WriteLine($"{i+1} - {artistas[i].Nome}");
        }

        if (int.TryParse(Console.ReadLine(), out int artistaEscolhido))
        {
            if (artistaEscolhido > 0 && artistaEscolhido <= artistas.Count)
            {
                _usuarioLogado.DeixaSeguirArtista(artistas[artistaEscolhido - 1]);
            }
        }
    }

    private void DeixarDeCurtir()
    {
        Console.WriteLine("Qual música você gostaria de deixar de curtir?");

        var musicas = _usuarioLogado.MusicasCurtidas.ToList();

        for (int i = 0; i < musicas.Count; i++)
        {
            Console.WriteLine($"{i+1} - {musicas[i].Titulo}");
        }

        if (int.TryParse(Console.ReadLine(), out int musicaEscolhida))
        {
            if (musicaEscolhida > 0 && musicaEscolhida <= musicas.Count)
            {
                _usuarioLogado.DescurteMusica(musicas[musicaEscolhida - 1]);
            }
        }
    }

    private void MostrarBiblioteca()
    {
        Console.Clear();
        Console.WriteLine("===== SUA BIBLIOTECA =====");

        var playlists = _usuarioLogado.PlaylistsUsuario.ToList();

        Console.WriteLine("Suas Playlists:");
        
        for (int i = 0; i < playlists.Count; i++)
        {
            Console.WriteLine($"{i+1} - {playlists[i].NomePlaylist}");
        }

        Console.WriteLine("");

        var artistas = _usuarioLogado.ArtistasSeguidos.ToList();

        Console.WriteLine("Artistas que você segue:");

        for (int i = 0; i < artistas.Count; i++)
        {
            Console.WriteLine($"{i+1} - {artistas[i].Nome}");
        }

        Console.WriteLine("");

        var musicas = _usuarioLogado.MusicasCurtidas.ToList();

        Console.WriteLine("Suas músicas curtidas:");

        for (int i = 0; i < musicas.Count; i++)
        {
            Console.WriteLine($"{i+1} - {musicas[i].Titulo}");
        }

    }

    private void AcoesBiblioteca()
    {
        Console.WriteLine("\nO que você gostaria de fazer?");
        Console.WriteLine("1 - Acessar uma playlist");
        Console.WriteLine("2 - Deletar uma playlist");
        Console.WriteLine("3 - Visitar a página de um artista");
        Console.WriteLine("4 - Deixar de seguir um artista");
        Console.WriteLine("5 - Deixar de curtir uma música");
        Console.WriteLine("0 - Voltar ao menu principal");
        
        string qualEscolha = Console.ReadLine();

        switch (qualEscolha)
        {
            case "1":
            ListarMusicasDePlaylistBanco();
            
            break;  

            case "2":
            MostrarPlaylists();
            DeletarPlaylist();
            break;

            case "3":
            InteragirAlbunsArtista();
            break;

            case "4":
            DeixarDeSeguir();
            break;

            case "5":
            DeixarDeCurtir();
            break;

            case "0":
            break;
        }
    }

    private void FluxoPlaylists()
    {
        Console.Clear();
        // 1. Tenta mostrar as playlists se houver alguma
        if (_usuarioLogado.PlaylistsUsuario != null && _usuarioLogado.PlaylistsUsuario.Count > 0) 
        {
            MostrarPlaylists();
        }
        else
        {
            Console.WriteLine("Você ainda não tem nenhuma playlist.");
        }

    }

    private void CriarPlaylist()
    {
         // 2. Pergunta sobre a criação de uma nova
        Console.WriteLine("\nGostaria de criar uma nova playlist? \n1 - Sim | 2 - Não");
        string novaPlaylist = Console.ReadLine();

        if (novaPlaylist == "1")
        {
            Console.WriteLine("Insira o nome da sua playlist:");
            _usuarioLogado.CriaPlaylist(Console.ReadLine());
        }
    }

    private void MostrarPlaylists()
    {
        Console.Clear();
        Console.WriteLine("===== SUAS PLAYLISTS =====");
        int contador = 1;
        foreach (var playlist in _usuarioLogado.PlaylistsUsuario)
        {
            Console.WriteLine($"{contador} - {playlist.NomePlaylist}");
            contador++;
        }

        
        //var playlistMusicas = _usuarioLogado.PlaylistsUsuario.ToList();

        // Console.WriteLine("Digite o número da playlist que gostaria de acessar:");
        // string qualPlaylist = Console.ReadLine();

        // if (int.TryParse(qualPlaylist, out int playlistEscolhida))
        // {
        //     if (playlistEscolhida > 0 && playlistEscolhida <= playlistMusicas.Count)
        //     {
        //         playlistMusicas[playlistEscolhida - 1].MostraPlaylist();
        //     }
        //     else
        //     {
                
        //     }
        // }
    }

    private void VerMusPlaylist()
    {

        var playlistsUsuario = _usuarioLogado.PlaylistsUsuario.ToList();

        if (_usuarioLogado.PlaylistsUsuario != null)
        {
            Console.WriteLine("\nGostaria de ver as músicas de qual playlist?");
            
            if (int.TryParse(Console.ReadLine(), out int qualPlaylist))
            {
                if (qualPlaylist > 0 && qualPlaylist <= playlistsUsuario.Count)
                {

                    var playlistSelecionada = playlistsUsuario[qualPlaylist - 1];

                    playlistSelecionada.MostraPlaylist();

                    Console.WriteLine("O que gostaria de fazer?");
                    Console.WriteLine("1 - Remover uma música da playlist | 2 - Voltar");
                    string removerOuVoltar = Console.ReadLine();

                    if (removerOuVoltar == "1")
                    {
                        Console.WriteLine("\nDigite o número da música que gostaria de excluir:");

                        var musicasDaPlaylist = playlistSelecionada.Midias.ToList();

                        if (int.TryParse(Console.ReadLine(), out int musicaEscolhida))
                        {
                            if (musicaEscolhida > 0 && musicaEscolhida <= musicasDaPlaylist.Count)
                            {
                                Midia midiaParaRemover = musicasDaPlaylist[musicaEscolhida - 1];

                                playlistSelecionada.RemoveMusPlaylist(midiaParaRemover);
                            }
                        }
                    }
                }
                else
                {
                    Console.WriteLine("Opção inválida! Essa playlist não existe na lista.");
                }
            }
            else
            {
                Console.WriteLine("Por favor, digite um número válido.");
            }
        }
    }

    private void EscolhePlaylist()
    {
        Console.WriteLine("Qual playlist gostaria de acessar?");
        var Playlists = _usuarioLogado.PlaylistsUsuario.ToList();

    }

    private void DeletarPlaylist()
    {
        Console.WriteLine("Qual playlist gostaria de deletar?");

        var playlists = _usuarioLogado.PlaylistsUsuario.ToList();

        if (!int.TryParse(Console.ReadLine(), out int qualPlaylist) || qualPlaylist <= 0 || qualPlaylist > playlists.Count())
        {
            Console.WriteLine("Entrada ou playlist não existe! retornando...");
            Console.ReadKey();
            return;
        }

        var playlistEscolhida = playlists[qualPlaylist - 1];

        _usuarioLogado.DeletaPlaylist(playlistEscolhida);

        Console.WriteLine("Playlist deletada com sucesso!");
        Console.ReadKey();
    }
#endregion

#region Métodos listagem
    private void MenuListagensBanco()
    {
        while (true)
        {
            Console.Clear();
            Console.WriteLine("===== LISTAGENS DO BANCO =====");
            Console.WriteLine("1 - Lista de artistas disponiveis");
            Console.WriteLine("2 - Lista de albuns de um artista");
            Console.WriteLine("3 - Lista de musicas de um artista");
            Console.WriteLine("4 - Lista de artistas seguidos");
            Console.WriteLine("5 - Lista de musicas curtidas");
            Console.WriteLine("6 - Lista de playlists");
            Console.WriteLine("7 - Lista de musicas de uma playlist");
            Console.WriteLine("0 - Voltar");

            string opcao = Console.ReadLine();

            try
            {
                switch (opcao)
                {
                    case "1":
                        ListarArtistasDisponiveisBanco();
                        break;

                    case "2":
                        ListarAlbunsDeArtistaBanco();
                        break;

                    case "3":
                        ListarMusicasDeArtistaBanco();
                        break;

                    case "4":
                        ListarArtistasSeguidosBanco();
                        break;

                    case "5":
                        ListarMusicasCurtidasBanco();
                        break;

                    case "6":
                        ListarPlaylistsBanco();
                        break;

                    case "7":
                        ListarMusicasDePlaylistBanco();
                        break;

                    case "0":
                        return;

                    default:
                        Console.WriteLine("Opcao invalida.");
                        Console.ReadKey();
                        break;
                }
            }
            catch (Exception erro)
            {
                Console.WriteLine($"Erro ao acessar o banco: {erro.Message}");
                Console.ReadKey();
            }
        }
    }

    private void ListarArtistasDisponiveisBanco()
    {
        Console.Clear();
        Console.WriteLine("===== ARTISTAS DISPONIVEIS =====");
        MostrarItens(_listagemDAO.ListarArtistasDisponiveis());
        //Console.ReadKey();
    }

    private void ListarMusicasDeAlbum(Album album)
    {
        Console.WriteLine($"===== {album.Nome} =====");
        MostrarItens(_listagemDAO.ListarMusicasDeAlbum(album.IdAlbum));
    }

    private void ListarAlbumArtistaEspecifico(Artista artista)
    {
        Console.WriteLine($"===== ALBUNS DE {artista.Nome} =====");
        MostrarItens(_listagemDAO.ListarAlbunsDeArtista(artista.IdArtista));
    }
    private void ListarAlbunsDeArtistaBanco()
    {
        Console.Clear();
        Console.WriteLine("===== ALBUNS DE UM ARTISTA =====");

        var artista = EscolherArtistaBanco();

        if (artista == null)
        {
            return;
        }

        Console.Clear();
        Console.WriteLine($"===== ALBUNS DE {artista.Nome} =====");
        MostrarItens(_listagemDAO.ListarAlbunsDeArtista(artista.Id));
        Console.ReadKey();
    }

    private void ListarMusicasDeArtistaBanco()
    {
        Console.Clear();
        Console.WriteLine("===== MUSICAS DE UM ARTISTA =====");

        var artista = EscolherArtistaBanco();

        if (artista == null)
        {
            return;
        }

        Console.Clear();
        Console.WriteLine($"===== MUSICAS DE {artista.Nome} =====");
        MostrarItens(_listagemDAO.ListarMusicasDeArtista(artista.Id));
        Console.ReadKey();
    }

    private void ListarArtistasSeguidosBanco()
    {
        Console.WriteLine("======== ARTISTAS SEGUIDOS ========");
        MostrarItens(_listagemDAO.ListarArtistasSeguidos(_usuarioLogado.IdUsuario));
    }

    private void ListarMusicasCurtidasBanco()
    {
        Console.WriteLine("========= MUSICAS CURTIDAS ========");
        MostrarItens(_listagemDAO.ListarMusicasCurtidas(_usuarioLogado.IdUsuario));
    }

    private void ListarPlaylistsBanco()
    {
        Console.WriteLine("============ PLAYLISTS ============");
        MostrarItens(_listagemDAO.ListarPlaylists(_usuarioLogado.IdUsuario));
    }

    private void ListarMusicasDePlaylistBanco()
    {
        var playlists = _listagemDAO.ListarPlaylists(_usuarioLogado.IdUsuario);

        Console.WriteLine("\nDigite o numero da playlist que gostaria de ver:");

        if (!int.TryParse(Console.ReadLine(), out int opcao) || opcao <= 0 || opcao > playlists.Count)
        {
            Console.WriteLine("Playlist invalida.");
            Console.WriteLine("Pressione qualquer tecla para continuar...");
            Console.ReadKey();
            return;
        }

        var playlist = playlists[opcao - 1];
        var playlistObjeto = new Playlist(playlist.Id);

        Console.Clear();
        Console.WriteLine($"===== MUSICAS DE {playlist.Nome} =====");
        MostrarItens(_listagemDAO.ListarMusicasDePlaylist(playlist.Id));

        var musicasPlaylist = _listagemDAO.ListarMusicasDePlaylist(playlist.Id);
        
        Console.WriteLine("\nO que gostaria de fazer?\n1 - Excluir uma música | 2 - Voltar");
        string excluirOuVoltar = Console.ReadLine();
        if (excluirOuVoltar == "1")
        {
            Console.WriteLine("Digite o número da música que gostaria de excluir:");
            if (int.TryParse(Console.ReadLine(), out int qualMusica))
            {
                if (qualMusica > 0 && qualMusica <= musicasPlaylist.Count)
                {
                    var musicaEscolhida = musicasPlaylist[qualMusica - 1];
                    var musicaObjeto = new Midia(musicaEscolhida.Id);

                    new UsuarioDAO().RemoverMusicaPlaylist(musicaObjeto, playlistObjeto);

                    Console.WriteLine("Pressione qualquer tecla para continuar...");
                    Console.ReadKey();
                }
                else
                {
                    Console.WriteLine("Entrada inválida. Digite um dos números disponíveis.");
                    Console.WriteLine("Pressione qualquer tecla para continuar...");
                    Console.ReadKey();
                }
            }
            else
            {
                Console.WriteLine("Entrada inválida. Digite um número válido");
                Console.WriteLine("Pressione qualquer tecla para continuar...");
                Console.ReadKey();
            }
        }
        return;
    }

    private ItemListagem? EscolherArtistaBanco()
    {
        var artistas = _listagemDAO.ListarArtistasDisponiveis();

        if (artistas.Count == 0)
        {
            Console.WriteLine("Nenhum artista encontrado.");
            Console.ReadKey();
            return null;
        }

        MostrarItens(artistas);
        Console.WriteLine("\nDigite o numero do artista:");

        if (!int.TryParse(Console.ReadLine(), out int opcao) || opcao <= 0 || opcao > artistas.Count)
        {
            Console.WriteLine("Artista invalido.");
            Console.ReadKey();
            return null;
        }

        return artistas[opcao - 1];
    }

    private ItemListagem? EscolherPlaylistBanco()
    {
        var playlists = _listagemDAO.ListarPlaylists(_usuarioLogado.IdUsuario);

        if (playlists.Count == 0)
        {
            Console.WriteLine("Nenhuma playlist encontrada.");
            Console.ReadKey();
            return null;
        }

        MostrarItens(playlists);
        Console.WriteLine("\nDigite o numero da playlist:");

        if (!int.TryParse(Console.ReadLine(), out int opcao) || opcao <= 0 || opcao > playlists.Count)
        {
            Console.WriteLine("Playlist invalida.");
            Console.ReadKey();
            return null;
        }

        return playlists[opcao - 1];
    }

    private void MostrarItens(List<ItemListagem> itens)
    {
        if (itens.Count == 0)
        {
            Console.WriteLine("Nenhum item encontrado.");
            return;
        }

        for (int i = 0; i < itens.Count; i++)
        {
            string complemento = string.IsNullOrWhiteSpace(itens[i].Complemento) ? "" : $" - {itens[i].Complemento}";
            Console.WriteLine($"{i + 1} - {itens[i].Nome}{complemento}");
        }
    }}
#endregion