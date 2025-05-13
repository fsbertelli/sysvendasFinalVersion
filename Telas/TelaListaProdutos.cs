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
            foreach (var produto in DBContext.RepositorioProdutos.ObterTodos())
            {
                string preco_formatado = produto.PrecoUnit.ToString("C", CultureInfo.CurrentCulture);
                Console.WriteLine($"{produto.Sku}: {produto.Nome} - {preco_formatado} - {produto.Desc}dev");
            }

        Console.WriteLine("Pressione qualquer tecla para continuar...");
        Console.ReadKey();
        TelaPrincipal.Show();
    }


}