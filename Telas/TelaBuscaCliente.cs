using System.Text;
using sysvendas2.Context;

namespace sysvendas2.Telas;

public class TelaBuscaCliente : ShowHeader
{
    static TelaBuscaCliente()
    {
    }

    public static void Show()
    {
        Console.Clear();
        Console.OutputEncoding = Encoding.UTF8;
        ShowHeader.Header("BuscaCliente");
        Console.WriteLine("Selecione uma opção:");
        Console.WriteLine("1 - Buscar cliente por ID");
        Console.WriteLine("2 - Buscar cliente por Nome");
        Console.WriteLine("0 - Voltar ao menu principal");
        if (int.TryParse(Console.ReadLine(), out int opcao))
        {
            switch (opcao)
            {
                case 1:
                    ShowCustomerById();
                    break;
                case 2:
                    ShowCustomerByName();
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
    public static void ShowCustomerById()
    {
        Console.Clear();
        Console.OutputEncoding = Encoding.UTF8;
        ShowHeader.Header("BuscaCliente");

        Console.Write("Digite o ID do cliente que deseja buscar: ");
        int id = Console.Read();
        var cliente = DBContext.RepositorioClientes?.ObterClienteId(id);
        if (cliente != null)
        {
            Console.WriteLine("\nCliente encontrado:");
            Console.WriteLine($"ID: {cliente.IdCliente}");
            Console.WriteLine($"Nome: {cliente.Nome}");
            Console.WriteLine($"Email: {cliente.Email}");
            Console.WriteLine($"Telefone: {cliente.Telefone}");
        }
        else
        {
            Console.WriteLine("\n❌ Cliente não encontrado.");
        }

    }
    public static void ShowCustomerByName()
    {
        Console.Clear();
        Console.OutputEncoding = Encoding.UTF8;
        ShowHeader.Header("BuscaCliente");

        Console.Write("Digite o Nome do cliente que deseja buscar: ");
        string nome = Console.ReadLine();
        var cliente = DBContext.RepositorioClientes?.ObterClienteNome(nome);

            if (cliente != null)
            {
                Console.WriteLine("\nCliente encontrado:");
                Console.WriteLine($"ID: {cliente.IdCliente}");
                Console.WriteLine($"Nome: {cliente.Nome}");
                Console.WriteLine($"Email: {cliente.Email}");
                Console.WriteLine($"Telefone: {cliente.Telefone}");
        }
            else
            {
                Console.WriteLine("\n❌ Cliente não encontrado.");
            }
        

        Console.WriteLine("\nPressione qualquer tecla para continuar...");
        Console.ReadKey();
        TelaPrincipal.Show();
    }
}