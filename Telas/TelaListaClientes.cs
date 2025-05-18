using System.Text;
using sysvendas2.Context;

namespace sysvendas2.Telas;

static class TelaListaClientes
{
    
    static TelaListaClientes()
    {
        
    }

    public static void Show()
    {
        Console.Clear();
        Console.OutputEncoding = Encoding.UTF8;
        ShowHeader.Header("ListaClientes");
        if (DBContext.RepositorioClientes != null)
            Console.WriteLine("ID | Nome | Email | Telefone");
            foreach (var cliente in DBContext.RepositorioClientes.ObterTodos())
                {
                    Console.WriteLine($"{cliente.IdCliente} | {cliente.Nome} | {cliente.Email} | {cliente.Telefone}");
                }

        Console.WriteLine("Pressione qualquer tecla para continuar...");
        Console.ReadKey();
        TelaPrincipal.Show();
    }

}