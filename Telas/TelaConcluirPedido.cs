using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using sysvendas2.Context;

namespace sysvendas2.Telas
{
    public class TelaConcluirPedido : ShowHeader
    {
        public static void Show()
        {
            Console.Clear();
            ShowHeader.Header("ConcluirPedido");

            var pedidos = DBContext.RepositorioPedidos.ObterTodos();
            var pendentes = pedidos.Where(p => p.Status.ToLower() != "concluido").ToList();

            if (pendentes.Count == 0)
            {
                Console.WriteLine("Nenhum pedido pendente para concluir!");
                Console.WriteLine("Pressione qualquer tecla para voltar.");
                Console.ReadKey();
                TelaPrincipal.Show();
                return;
            }

            Console.WriteLine("Pedidos pendentes:");
            foreach (var p in pendentes)
            {
                Console.WriteLine($"{p.IdPedido} | Cliente: {p.Cliente.Nome} | Status: {p.Status} | Total: R$ {p.Total:F2}");
            }
            Console.Write("\nDigite o ID do pedido que deseja concluir: ");
            if (int.TryParse(Console.ReadLine(), out int idPedido))
            {
                var pedido = pendentes.FirstOrDefault(p => p.IdPedido == idPedido);
                if (pedido != null)
                {
                    DBContext.RepositorioPedidos.AtualizarStatus(idPedido, "Concluido");
                    Console.WriteLine("Pedido concluído com sucesso!");
                }
                else
                {
                    Console.WriteLine("Pedido não encontrado ou já concluído.");
                }
            }
            else
            {
                Console.WriteLine("ID inválido!");
            }

            Console.WriteLine("Pressione qualquer tecla para voltar.");
            Console.ReadKey();
            TelaPrincipal.Show();
        }
    }
}
