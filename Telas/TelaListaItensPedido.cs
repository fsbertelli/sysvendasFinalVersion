using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using sysvendas2.Context;

namespace sysvendas2.Telas
{
    public class TelaListaItensPedido
    {
        public static void Show()
        {
            Console.Clear();
            Console.OutputEncoding = Encoding.UTF8;
            ShowHeader.Header("ListaItensPedido");

            Console.WriteLine("Selecione uma opção:");
            Console.WriteLine("1 - Listar todos os itens de todos os pedidos");
            Console.WriteLine("2 - Listar itens de um pedido específico");
            Console.WriteLine("0 - Voltar ao menu principal");

            if (int.TryParse(Console.ReadLine(), out int opcao))
            {
                switch (opcao)
                {
                    case 1:
                        ListarTodosItens();
                        break;
                    case 2:
                        ListarItensPorPedido();
                        break;
                    case 0:
                        TelaPrincipal.Show();
                        return;
                    default:
                        Console.WriteLine("Opção inválida!");
                        break;
                }
            }
            else
            {
                Console.WriteLine("Entrada inválida!");
            }

            Console.WriteLine("\nPressione qualquer tecla para continuar...");
            Console.ReadKey();
            TelaPrincipal.Show();
        }

        private static void ListarTodosItens()
        {
            if (DBContext.RepositorioItensPedido != null)
            {
                var itens = DBContext.RepositorioItensPedido.ObterTodos();

                if (itens != null && itens.Count > 0)
                {
                    Console.WriteLine("ID | Pedido | Produto | Quantidade | Preço Unit. | Desconto | Subtotal");
                    Console.WriteLine("------------------------------------------------------------------");

                    foreach (var item in itens)
                    {
                        Console.WriteLine($"{item.IdItemPedido} | #{item.Pedido.IdPedido} | {item.Produto.Nome} | {item.Quantidade} | R$ {item.PrecoUnit:F2} | {item.Desconto}% | R$ {item.SubTotal:F2}");
                    }
                }
                else
                {
                    Console.WriteLine("\nNenhum item de pedido cadastrado!");
                }
            }
            else
            {
                Console.WriteLine("\nFalha ao acessar o banco!");
            }
        }

        private static void ListarItensPorPedido()
        {
            Console.WriteLine("\nDigite o ID do pedido:");
            if (int.TryParse(Console.ReadLine(), out int idPedido))
            {
                if (DBContext.RepositorioItensPedido != null)
                {
                    var itens = DBContext.RepositorioItensPedido.ObterPorPedido(idPedido);

                    if (itens != null && itens.Count > 0)
                    {
                        Console.WriteLine($"\n--- Itens do Pedido #{idPedido} ---");
                        Console.WriteLine("ID | Produto | Quantidade | Preço Unit. | Desconto | Subtotal");
                        Console.WriteLine("--------------------------------------------------------");

                        foreach (var item in itens)
                        {
                            Console.WriteLine($"{item.IdItemPedido} | {item.Produto.Nome} | {item.Quantidade} | R$ {item.PrecoUnit:F2} | {item.Desconto}% | R$ {item.SubTotal:F2}");
                        }

                        double total = itens.Sum(i => i.SubTotal);
                        Console.WriteLine($"\nTotal do Pedido: R$ {total:F2}");
                    }
                    else
                    {
                        Console.WriteLine($"\nNenhum item encontrado para o pedido #{idPedido}!");
                    }
                }
                else
                {
                    Console.WriteLine("\nFalha ao acessar o banco!");
                }
            }
            else
            {
                Console.WriteLine("\nID de pedido inválido!");
            }
        }
    }
}
