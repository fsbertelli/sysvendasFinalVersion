namespace sysvendas2.Models;

public class ItemPedido
{
    public ItemPedido() { }

    public ItemPedido(int idItemPedido, Pedido pedido, Produto produto, int quantidade, double precoUnit, int desconto)
    {
        IdItemPedido = idItemPedido;
        Pedido = pedido;
        Produto = produto;
        Quantidade = quantidade;
        PrecoUnit = precoUnit;
        Desconto = desconto;
        CalcularSubTotal();
    }

    public int IdItemPedido { get; set; }
    public Pedido Pedido { get; set; }
    public Produto Produto { get; set; }
    public int Quantidade { get; set; }
    public double PrecoUnit { get; set; }
    public int Desconto { get; set; }
    public double SubTotal { get; set; }

    public void CalcularSubTotal()
    {
        SubTotal = (PrecoUnit * Quantidade) * (1 - Desconto / 100.0);
    }
}