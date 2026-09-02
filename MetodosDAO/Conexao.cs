using MySql.Data.MySqlClient;

public static class ConexaoBanco
{
    private const string ConnectionStringPadrao = "Server=localhost;Port=3306;Database=db_spotifei_teste;Uid=root;Pwd=;";

    public static MySqlConnection CriarConexao()
    {
        string? connectionString = Environment.GetEnvironmentVariable("SPOTIFEI_DB");

        if (string.IsNullOrWhiteSpace(connectionString))
        {
            connectionString = ConnectionStringPadrao;
        }

        return new MySqlConnection(connectionString);
    }
}
