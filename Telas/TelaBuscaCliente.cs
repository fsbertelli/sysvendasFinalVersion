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

        Console.Write("Digite o Nome do cliente que deseja buscar: ");
        string nome = Console.ReadLine();
            var cliente = DBContext.RepositorioClientes?.ObterClienteNome(nome);

            if (cliente != null)
            {
                Console.WriteLine("\nCliente encontrado:");
                Console.WriteLine($"ID: {cliente.IdCliente}");
                Console.WriteLine($"Nome: {cliente.Nome}");
                Console.WriteLine($"Email: {cliente.Email}");
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