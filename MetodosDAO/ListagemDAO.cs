using MySql.Data.MySqlClient;

public class ListagemDAO
{
    public List<ItemListagem> ListarPlanos()
    {
        string sql = "SELECT id_plano, descricao, valor FROM tb_plano ORDER BY id_plano";

        return ExecutarListagem(sql, leitor => new ItemListagem
        {
            Id = leitor.GetInt32("id_plano"),
            Nome = leitor.GetString("descricao"),
            Complemento = Convert.ToString(leitor.GetDecimal("valor"))
        });
    }



    public List<ItemListagem> ListarUsuarios()
    {
        string sql = "SELECT id_usuario, email, senha FROM tb_usuario ORDER BY id_usuario";

        return ExecutarListagem(sql, leitor => new ItemListagem
        {
            Id = leitor.GetInt32("id_usuario"),
            Nome = leitor.GetString("email"),
            Complemento = leitor.GetString("senha")
        });
    }

    public List<ItemListagem> ListarArtistasDisponiveis()
    {
        string sql = "SELECT id_artista, nome FROM tb_artista ORDER BY nome";

        return ExecutarListagem(sql, leitor => new ItemListagem
        {
            Id = leitor.GetInt32("id_artista"),
            Nome = leitor.GetString("nome")
        });
    }

    public List<ItemListagem> ListarAlbunsDeArtista(int idArtista)
    {
        string sql = @"
            SELECT DISTINCT a.id_album, a.nome
            FROM tb_album a
            INNER JOIN tb_album_musica am ON am.id_album = a.id_album
            WHERE am.id_artista = @idArtista
            ORDER BY a.nome";

        return ExecutarListagem(sql, leitor => new ItemListagem
        {
            Id = leitor.GetInt32("id_album"),
            Nome = leitor.GetString("nome")
        }, ("@idArtista", idArtista));
    }

    public List<ItemListagem> ListarMusicasDeAlbum(int idAlbum)
    {
        string sql = @"
            SELECT DISTINCT m.id_musica, m.nome, m.duracao
            FROM tb_musica m
            INNER JOIN tb_album_musica am ON m.id_musica = am.id_musica
            WHERE am.id_album = @idAlbum
            ORDER BY m.id_musica;";

        return ExecutarListagem(sql, leitor => new ItemListagem
        {
            Id = leitor.GetInt32("id_musica"),
            Nome = leitor.GetString("nome"),
            Complemento = FormatarDuracao(leitor.GetTimeSpan("duracao"))
        }, ("@idAlbum", idAlbum));
    }

    public List<ItemListagem> ListarMusicasDeArtista(int idArtista)
    {
        string sql = @"
            SELECT DISTINCT m.id_musica, m.nome, m.duracao
            FROM tb_musica m
            INNER JOIN tb_artista_musica am ON am.id_musica = m.id_musica
            WHERE am.id_artista = @idArtista
            ORDER BY m.nome";

        return ExecutarListagem(sql, leitor => new ItemListagem
        {
            Id = leitor.GetInt32("id_musica"),
            Nome = leitor.GetString("nome"),
            Complemento = FormatarDuracao(leitor.GetTimeSpan("duracao"))
        }, ("@idArtista", idArtista));
    }

    public List<ItemListagem> ListarArtistasSeguidos(int idUsuario)
    {
        string sql = @"
            SELECT a.id_artista, a.nome
            FROM tb_seguidores s
            INNER JOIN tb_artista a ON a.id_artista = s.id_artista
            WHERE s.id_usuario = @idUsuario
            ORDER BY a.nome";

        return ExecutarListagem(sql, leitor => new ItemListagem
        {
            Id = leitor.GetInt32("id_artista"),
            Nome = leitor.GetString("nome")
        }, ("@idUsuario", idUsuario));
    }

    public List<ItemListagem> ListarMusicasCurtidas(int idUsuario)
    {
        string sql = @"
            SELECT m.id_musica, m.nome, m.duracao
            FROM tb_curtidas c
            INNER JOIN tb_musica m ON m.id_musica = c.id_musica
            WHERE c.id_usuario = @idUsuario
            ORDER BY m.nome";

        return ExecutarListagem(sql, leitor => new ItemListagem
        {
            Id = leitor.GetInt32("id_musica"),
            Nome = leitor.GetString("nome"),
            Complemento = FormatarDuracao(leitor.GetTimeSpan("duracao"))
        }, ("@idUsuario", idUsuario));
    }

    public List<ItemListagem> ListarPlaylists(int idUsuario)
    {
        string sql = @"
            SELECT id_playlist, nome
            FROM tb_playlist
            WHERE id_usuario = @idUsuario
            ORDER BY nome";

        return ExecutarListagem(sql, leitor => new ItemListagem
        {
            Id = leitor.GetInt32("id_playlist"),
            Nome = leitor.GetString("nome")
        }, ("@idUsuario", idUsuario));
    }

    public List<ItemListagem> ListarMusicasDePlaylist(int idPlaylist)
    {
        string sql = @"
            SELECT m.id_musica, m.nome, m.duracao
            FROM tb_playlist_musica pm
            INNER JOIN tb_musica m ON m.id_musica = pm.id_musica
            WHERE pm.id_playlist = @idPlaylist
            ORDER BY m.nome";

        return ExecutarListagem(sql, leitor => new ItemListagem
        {
            Id = leitor.GetInt32("id_musica"),
            Nome = leitor.GetString("nome"),
            Complemento = FormatarDuracao(leitor.GetTimeSpan("duracao"))
        }, ("@idPlaylist", idPlaylist));
    }

    private List<ItemListagem> ExecutarListagem(
        string sql,
        Func<MySqlDataReader, ItemListagem> montarItem,
        params (string Nome, object Valor)[] parametros)
    {
        using var conexao = ConexaoBanco.CriarConexao();
        conexao.Open();

        using var comando = new MySqlCommand(sql, conexao);

        foreach (var parametro in parametros)
        {
            comando.Parameters.AddWithValue(parametro.Nome, parametro.Valor);
        }

        using var leitor = comando.ExecuteReader();
        List<ItemListagem> itens = new();

        while (leitor.Read())
        {
            itens.Add(montarItem(leitor));
        }

        return itens;
    }

    private string FormatarDuracao(TimeSpan duracao)
    {
        return duracao.ToString(@"mm\:ss");
    }
}
