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
            new Opcao(3, "📦 Cadastrar produto"),
            new Opcao(4, "📋 Listar produtos"),
            new Opcao(5, "📋 Buscar Cliente"),
            new Opcao(6, "📋 Adicionar Pedido"),
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
                switch (opcaoSelecionada) { 
                case 1:
                    Console.WriteLine("\nCadastrando clientes");
                    TelaCadastroCliente.Show();
                    break;
                case 2:
                    Console.WriteLine("\nListando clientes");
                    TelaListaClientes.Show();
                    break;
                case 3:
                    Console.WriteLine("\nCadastrando produtos");
                    TelaCadastroProduto.Show();
                    break;
                case 4:
                    Console.WriteLine("\nListando produtos");
                    TelaListaProdutos.Show();
                    break;
                case 5:
                    Console.WriteLine("\nBuscando clientes");
                    TelaBuscaCliente.Show();
                    break;
                case 6:
                    Console.WriteLine("\nAdicionando pedido");
                    TelaCadastroPedido.Show();
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