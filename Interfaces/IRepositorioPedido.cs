namespace sysvendas2.Interfaces;
using sysvendas2.Models;

public interface IRepositorioPedido
{
    void Adicionar(Pedido pedido);
    List<Pedido> ObterTodos();
    void AtualizarStatus(int idPedido, string novoStatus);
}