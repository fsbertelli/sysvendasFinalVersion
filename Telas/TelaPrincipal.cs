namespace sysvendas2.Telas;
using System.Text;
using sysvendas2.Models;

public class TelaPrincipal : ShowHeader
{
    public static List<Opcao> opcoes;
    static TelaPrincipal()
    {
        opcoes = new List<Opcao>
        {
            new Opcao(1, "😺 Cadastrar cliente"),
            new Opcao(2, "📖 Listar clientes"),
            new Opcao(3, "🔍 Buscar Cliente"),
            new Opcao(4, "📋 Listar produtos"),
            new Opcao(5, "📦 Cadastrar produto"),
            new Opcao(6, "🛒 Cadastrar Pedido"),
            new Opcao(7, "✅  Concluir Pedido"),
            new Opcao(8, "📊 Listar Pedidos"),
            new Opcao(9, "📝 Listar Itens de Pedido"),
            new Opcao(0, "🚪 Sair")
        };
    }
    public static void Show()
    {
        while (true)
        {
            Console.Clear();
            Console.OutputEncoding = Encoding.UTF8;
            ShowHeader.Header("Principal");
            foreach (var opt in opcoes)
            {
                Console.WriteLine($"{opt.Id} - {opt.Descricao}");
            }

            Console.WriteLine("\nDigite a opção desejada:");

            if (int.TryParse(Console.ReadLine(), out int opcaoSelecionada))
            {
                switch (opcaoSelecionada)
                {
                    case 1:
                        TelaCadastroCliente.Show();
                        break;
                    case 2:
                        TelaListaClientes.Show();
                        break;
                    case 3:
                        TelaBuscaCliente.Show();
                        break;
                    case 4:
                        TelaListaProdutos.Show();
                        break;
                    case 5:
                        TelaCadastroProduto.Show();
                        break;
                    case 6:
                        TelaCadastroPedido.Show();
                        break;
                    case 7:
                        TelaConcluirPedido.Show();
                        break;
                    case 8:
                        TelaListaPedidos.Show();
                        break;
                    case 9:
                        TelaListaItensPedido.Show();
                        break;
                    case 0:
                        Console.WriteLine("\nSaindo...");
                        return;
                    default:
                        Console.WriteLine("\nOpção inválida.");
                        break;
                }
            }
            else
            {
                Console.WriteLine("\nEntrada inválida. Digite um número.");
            }
            Console.WriteLine("Pressione qualquer tecla para continuar...");
            Console.ReadKey();
        }
    }
}