using System.Globalization;
using System.Text;
using sysvendas2.Context;

namespace sysvendas2.Telas;

static class TelaListaProdutos
{
    
    public static void Show()
    {
        Console.Clear();
        Console.OutputEncoding = Encoding.UTF8;
        ShowHeader.Header("ListaProduto");
        if (DBContext.RepositorioProdutos != null)
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
    }


}