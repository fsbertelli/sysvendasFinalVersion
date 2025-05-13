using System.Text;
using sysvendas2.Context;
using sysvendas2.Models;

namespace sysvendas2.Telas;

static class TelaCadastroCliente
{
    //public static List<Cliente> clientes;
    
    static TelaCadastroCliente()
    {
        //clientes = new List<Cliente>(); 
    }

    public static void Show()
    {
        Console.Clear();
        Console.OutputEncoding = Encoding.UTF8;
        ShowHeader.Header("CadastroCliente");
        
        string nomeCliente = "";
        string emailCliente = "";
        string telefoneCliente = "";
        int idCliente = 0;
        
        Console.WriteLine("\nDigite o nome do cliente:");
        nomeCliente = Console.ReadLine();
        
        Console.WriteLine("\nDigite o email do cliente:");
        emailCliente = Console.ReadLine();
        
        Console.WriteLine("\nDigite o telefone do cliente:");
        telefoneCliente = Console.ReadLine();

        Cliente cliente = new Cliente(idCliente, nomeCliente, emailCliente, telefoneCliente);
        DBContext.RepositorioClientes.Adicionar(cliente);
        
        Console.WriteLine("Pressione qualquer tecla para continuar...");
        Console.ReadKey();
        TelaPrincipal.Show();
    }

}

