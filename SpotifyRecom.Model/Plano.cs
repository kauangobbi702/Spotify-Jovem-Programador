public class Plano
{
    public int IdPlano { get; set; }
    public string Descricao { get; private set; }
    public decimal Valor { get; private set; }
    public List<Usuario> Usuarios { get; set;}

}