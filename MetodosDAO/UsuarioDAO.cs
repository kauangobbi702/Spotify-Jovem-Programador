using MySql.Data.MySqlClient;

public class UsuarioDAO
{
    
    public Usuario ValidarLogin(string email, string senha)
    {
        try
        {
            using var conexao = ConexaoBanco.CriarConexao();
            conexao.Open();
            
            string sql = @"
                SELECT u.id_usuario, COALESCE(u.nome, 'Usuário') AS nome, p.descricao AS plano_descricao
                FROM tb_usuario u
                INNER JOIN tb_plano p ON u.id_plano = p.id_plano
                WHERE u.email = @email AND u.senha = @senha;";

            using var comando = new MySqlCommand(sql, conexao);
            comando.Parameters.AddWithValue("@email", email);
            comando.Parameters.AddWithValue("@senha", senha);

            using var leitor = comando.ExecuteReader();

            if (leitor.Read())
            {
                int id = leitor.GetInt32("id_usuario");
                string nome = leitor.GetString("nome");
                string descPlano = leitor.GetString("plano_descricao");

                Plano planoUs = new(descPlano);
                var usuario = new Usuario(id, nome, planoUs);

                return usuario;
            }
        }
        catch (Exception e)
        {
            Console.WriteLine($"Erro ao validar login: {e.Message}");
        }

        return null;
    }

    public void AdicionarUsuarioDAO(Usuario usuario)
    {
        try
        {
            using var conexao =  ConexaoBanco.CriarConexao();
            conexao.Open();
            string sql = "INSERT INTO tb_usuario (nome, email, senha, id_plano) VALUES (@nome, @email, @senha, @id_plano);";

            MySqlCommand comando = new MySqlCommand(sql, conexao);
            comando.Parameters.AddWithValue("@nome", usuario.Nome);
            comando.Parameters.AddWithValue("@email", usuario.Email);
            comando.Parameters.AddWithValue("@senha", usuario.Senha);
            comando.Parameters.AddWithValue("@id_plano", usuario.Plano.IdPlano);

            comando.ExecuteNonQuery();

            Console.WriteLine("Usuario cadastrado com sucesso");
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
        }
    }

        public void AdicionarUsuarioSegArtista(Usuario usuario, Artista artista)
        {
            try
            {
                using var conexao = ConexaoBanco.CriarConexao();
                conexao.Open();
                string sql = "INSERT INTO tb_seguidores (id_artista, id_usuario) VALUES (@id_artista, @id_usuario);";

                MySqlCommand comando = new MySqlCommand(sql, conexao);
                comando.Parameters.AddWithValue("@id_artista", artista.IdArtista);
                comando.Parameters.AddWithValue("@id_usuario", usuario.IdUsuario);

                comando.ExecuteNonQuery();

                Console.WriteLine("Artista seguido com sucesso!");
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        public void DeletarUsuarioSegArtista(Usuario usuario, Artista artista)
        {
            try
            {
                using var conexao = ConexaoBanco.CriarConexao();
                conexao.Open();
                string sql = "DELETE FROM tb_seguidores WHERE id_artista = @id_artista AND id_usuario = @id_usuario;";

                MySqlCommand comando = new MySqlCommand(sql, conexao);
                comando.Parameters.AddWithValue("@id_artista", artista.IdArtista);
                comando.Parameters.AddWithValue("@id_usuario", usuario.IdUsuario);

                comando.ExecuteNonQuery();

                Console.WriteLine("Você não segue mais esse artista!");
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

    public void AdicionarMusicaCurtida(Usuario usuario, Midia midia)
    {
        try
        {
            using var conexao =  ConexaoBanco.CriarConexao();
            conexao.Open();
            string sql = "INSERT INTO tb_curtidas (id_usuario, id_musica) VALUES (@id_usuario, @id_musica);";

            MySqlCommand comando = new MySqlCommand(sql, conexao);
            comando.Parameters.AddWithValue("@id_musica", midia.IdMidia);
            comando.Parameters.AddWithValue("@id_usuario", usuario.IdUsuario);

            comando.ExecuteNonQuery();

            Console.WriteLine("Música curtida com sucesso!");
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
        }
    }

    public void DeletarMusicaCurtida(Usuario usuario, Midia midia)
    {
        try
        {
            using var conexao =  ConexaoBanco.CriarConexao();
            conexao.Open();
            string sql = "DELETE FROM tb_curtidas WHERE id_usuario = @id_usuario AND id_musica = @id_musica;";

            MySqlCommand comando = new MySqlCommand(sql, conexao);
            comando.Parameters.AddWithValue("@id_musica", midia.IdMidia);
            comando.Parameters.AddWithValue("@id_usuario", usuario.IdUsuario);

            comando.ExecuteNonQuery();

            Console.WriteLine("Você não curte mais essa música!");
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
        }
    }

    public void AdicionarPlaylist(Usuario usuario, Playlist playlist)
    {
        try
        {
            using var conexao =  ConexaoBanco.CriarConexao();
            conexao.Open();
            string sql = "INSERT INTO tb_playlist (nome, id_usuario) VALUES (@nome, @id_usuario);";

            MySqlCommand comando = new MySqlCommand(sql, conexao);
            comando.Parameters.AddWithValue("@nome", playlist.NomePlaylist);
            comando.Parameters.AddWithValue("@id_usuario", usuario.IdUsuario);

            comando.ExecuteNonQuery();

            Console.WriteLine("Playlist criada com sucesso!");
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
        }
    }

    public void DeletarPlaylist(Usuario usuario, Playlist playlist)
    {
        try
        {
            using var conexao =  ConexaoBanco.CriarConexao();
            conexao.Open();
            string sql = "DELETE FROM tb_playlist WHERE id_playlist = @id_playlist AND id_usuario = @id_usuario;";

            MySqlCommand comando = new MySqlCommand(sql, conexao);
            comando.Parameters.AddWithValue("@id_playlist", playlist.IdPlaylist);
            comando.Parameters.AddWithValue("@id_usuario", usuario.IdUsuario);

            comando.ExecuteNonQuery();

            Console.WriteLine("Playlist excluída com sucesso!");
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
        }
    }

    public void AdicionarMusicaPlaylist(Midia midia, Playlist playlist)
    {
        try
        {
            using var conexao =  ConexaoBanco.CriarConexao();
            conexao.Open();
            string sql = "INSERT INTO tb_playlist_musica (id_playlist, id_musica) VALUES (@id_playlist, @id_musica);";

            MySqlCommand comando = new MySqlCommand(sql, conexao);
            comando.Parameters.AddWithValue("@id_playlist", playlist.IdPlaylist);
            comando.Parameters.AddWithValue("@id_musica", midia.IdMidia);

            comando.ExecuteNonQuery();

            Console.WriteLine("Música adicionada com sucesso!");
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
        }
    }

    public void RemoverMusicaPlaylist(Midia midia, Playlist playlist)
    {
        try
        {
            using var conexao =  ConexaoBanco.CriarConexao();
            conexao.Open();
            string sql = "DELETE FROM tb_playlist_musica WHERE id_playlist = @id_playlist AND id_musica = @id_musica;";

            MySqlCommand comando = new MySqlCommand(sql, conexao);
            comando.Parameters.AddWithValue("@id_playlist", playlist.IdPlaylist);
            comando.Parameters.AddWithValue("@id_musica", midia.IdMidia);

            comando.ExecuteNonQuery();

            Console.WriteLine("Música removida com sucesso!");
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
        }
    }
}
