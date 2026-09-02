public class Plano
{
    private static List<Plano> _planos = new();
    public static IReadOnlyCollection<Plano> Planos => _planos.AsReadOnly();
    public int IdPlano { get; set; }
    public string Descricao { get; private set; }
    public decimal Valor { get; private set; }

    public Plano(int idPlano, string descricao, decimal valor)
    {
        this.Descricao = descricao;
        this.Valor = valor;
        this.IdPlano = idPlano;
        _planos.Add(this);
    }

    public Plano(string descricao)
    {
        Descricao = descricao;
    }

    /* 

    Exemplo
    {
    Plano PGratuito = new Plano(
    "Plano Gratuito", 00.00)

    Plano PMedio = new Plano(
    "Plano Basico", 23.90)

    Plano PPremium = new Plano(
    "Plano Premium", 40.90)
    }

    */

}