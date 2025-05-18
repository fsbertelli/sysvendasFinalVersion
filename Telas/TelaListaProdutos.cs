using System.Globalization;
using System.Text;
using sysvendas2.Context;
using sysvendas2.Models;

namespace sysvendas2.Telas;

static class TelaListaProdutos
{
    public static void Show()
    {
        Console.Clear();
        Console.OutputEncoding = Encoding.UTF8;
        ShowHeader.Header("ListaProduto");
        Console.WriteLine("Selecione uma opção:");
        Console.WriteLine("1 - Listar todos os produtos");
        Console.WriteLine("2 - Buscar produto por SKU");
        Console.WriteLine("0 - Voltar ao menu principal");

        if (int.TryParse(Console.ReadLine(), out int opcao))
        {
            switch (opcao)
            {
                case 1:
                    ShowAll();
                    break;
                case 2:
                    ShowBySKU();
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

    public static void ShowBySKU()
    {
        Console.Clear();
        Console.OutputEncoding = Encoding.UTF8;
        ShowHeader.Header("ListaProduto");
        Console.WriteLine("Digite o SKU do produto:");
        String SKU = Console.ReadLine();
        if (DBContext.RepositorioProdutos != null)
        {
            Produto produto = DBContext.RepositorioProdutos.ObterProduto(SKU);
            Console.Clear();
            Console.WriteLine("SKU        | Nome            | Preço      | Quantidade | Descrição");
            Console.WriteLine("------------------------------------------------------------------");
            string preco_formatado = produto.PrecoUnit.ToString("C", CultureInfo.CurrentCulture);
            Console.WriteLine($"{produto.Sku.PadRight(10)} | {produto.Nome.PadRight(15)} | {preco_formatado.PadRight(10)} | {produto.Quantidade.ToString().PadRight(10)} | {produto.Descricao.ToString().PadRight(10)}");
        } else
        {
            Console.WriteLine("\n❌ Produto não encontrado.");

        }
    }
    public static void ShowAll()
    {
        Console.Clear();
        Console.OutputEncoding = Encoding.UTF8;
        ShowHeader.Header("ListaProduto");
        if (DBContext.RepositorioProdutos != null)
        {
            Console.WriteLine("SKU        | Nome            | Preço      | Quantidade | Descrição");
            Console.WriteLine("------------------------------------------------------------------");
            foreach (var produto in DBContext.RepositorioProdutos.ObterTodos())
            {
                string preco_formatado = produto.PrecoUnit.ToString("C", CultureInfo.CurrentCulture);
                Console.WriteLine($"{produto.Sku.PadRight(10)} | {produto.Nome.PadRight(15)} | {preco_formatado.PadRight(10)} | {produto.Quantidade.ToString().PadRight(10)} | {produto.Descricao.ToString().PadRight(10)}");
            }

            Console.WriteLine("\nPressione qualquer tecla para continuar...");
            Console.ReadKey();
            TelaPrincipal.Show();
        } else
        {
            Console.WriteLine("\n❌ Produtos não encontrado.");
        }
    }


}