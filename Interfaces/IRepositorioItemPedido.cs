using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using sysvendas2.Models;

namespace sysvendas2.Interfaces
{
    public interface IRepositorioItemPedido
    {
        void Adicionar(ItemPedido itemPedido);
        List<ItemPedido> ObterTodos();
        List<ItemPedido> ObterPorPedido(int idPedido);
        void AtualizarEstoque(int idProduto, int quantidade);
    }
}
