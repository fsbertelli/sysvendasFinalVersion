namespace sysvendas2.Models;

public class Produto
{
    public Produto(string sku, string nome, double precounit, string desc)
    {
        Sku = sku;
        Nome = nome;
        PrecoUnit = precounit;
        Desc = desc;
    }

    public Produto()
    {
    }
    public int IdProduto { get; set; }
    public string Sku { get; set; }
    public string Nome { get; set; }
    public double PrecoUnit { get; set; }
    public string Desc { get; set; }

}