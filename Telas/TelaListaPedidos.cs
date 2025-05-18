using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using sysvendas2.Context;

namespace sysvendas2.Telas
{
    public class TelaListaPedidos
    {
        public static void Show()
        {
            Console.Clear();
            Console.OutputEncoding = Encoding.UTF8;
            ShowHeader.Header("ListaPedidos");

            if (DBContext.RepositorioPedidos != null)
            {
                var pedidos = DBContext.RepositorioPedidos.ObterTodos();

                if (pedidos != null && pedidos.Count > 0)
                {
                    Console.WriteLine("ID | Data | Cliente | Status | Total");
                    Console.WriteLine("-----------------------------------------");

                    foreach (var pedido in pedidos)
                    {
                        Console.WriteLine($"{pedido.IdPedido} | {pedido.Data:dd/MM/yyyy HH:mm} | {pedido.Cliente.Nome} | {pedido.Status} | R$ {pedido.Total:F2}");
                    }

                    Console.WriteLine("\nPara ver detalhes de um pedido, digite o ID (ou 0 para voltar):");
                    if (int.TryParse(Console.ReadLine(), out int idPedido) && idPedido > 0)
                    {
                        var pedido = pedidos.FirstOrDefault(p => p.IdPedido == idPedido);
                        if (pedido != null)
                        {
                            ExibirDetalhesPedido(pedido);
                        }
                        else
                        {
                            Console.WriteLine($"Pedido #{idPedido} não encontrado!");
                        }
                    }
                }
                else
                {
                    Console.WriteLine("\nNenhum pedido cadastrado!");
                }
            }
            else
            {
                Console.WriteLine("\nFalha ao acessar o banco!");
            }

            Console.WriteLine("\nPressione qualquer tecla para continuar...");
            Console.ReadKey();
            TelaPrincipal.Show();
        }

        private static void ExibirDetalhesPedido(Models.Pedido pedido)
        {
            Console.Clear();
            Console.WriteLine($"\n--- Detalhes do Pedido #{pedido.IdPedido} ---");
            Console.WriteLine($"Data: {pedido.Data:dd/MM/yyyy HH:mm}");
            Console.WriteLine($"Cliente: {pedido.Cliente.Nome} (ID: {pedido.Cliente.IdCliente})");
            Console.WriteLine($"Status: {pedido.Status}");
            Console.WriteLine($"Total: R$ {pedido.Total:F2}");

            if (DBContext.RepositorioItensPedido != null)
            {
                var itens = DBContext.RepositorioItensPedido.ObterPorPedido(pedido.IdPedido);

                if (itens != null && itens.Count > 0)
                {
                    Console.WriteLine("\n--- Itens do Pedido ---");
                    Console.WriteLine("Produto | Quantidade | Preço Unit. | Desconto | Subtotal");
                    Console.WriteLine("------------------------------------------------------");

                    foreach (var item in itens)
                    {
                        Console.WriteLine($"{item.Produto.Nome} | {item.Quantidade} | R$ {item.PrecoUnit:F2} | {item.Desconto}% | R$ {item.SubTotal:F2}");
                    }
                }
                else
                {
                    Console.WriteLine("\nNenhum item encontrado para este pedido!");
                }
            }
            else
            {
                Console.WriteLine("\nFalha ao acessar o banco!");
            }

            Console.WriteLine("\nPressione qualquer tecla para voltar...");
            Console.ReadKey();
        }
    }
}
