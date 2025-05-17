namespace sysvendas2.Models;

public class Produto
{
    public Produto(string sku, string nome, double precounit, string descricao, int quantidade)
    {
        Sku = sku;
        Nome = nome;
        PrecoUnit = precounit;
        Descricao = descricao;
        Quantidade = quantidade;
    }

    public Produto()
    {
    }
    public int IdProduto { get; set; }
    public string Sku { get; set; }
    public string Nome { get; set; }
    public double PrecoUnit { get; set; }
    public string Descricao { get; set; }
    public int Quantidade { get; set; }

}