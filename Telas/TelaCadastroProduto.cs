using System.Text;
using sysvendas2.Context;
using sysvendas2.Models;

namespace sysvendas2.Telas;

public class TelaCadastroProduto : ShowHeader
{
    //public static List<Produto> produtos;
    
    static TelaCadastroProduto()
    {
       // produtos = new List<Produto>(); 
    }

    public static void Show()
    {
        Console.Clear();
        Console.OutputEncoding = Encoding.UTF8;
        ShowHeader.Header("CadastroProduto");
        
        string nomeProduto = "";
        string skuProduto = "";
        string descProduto = "";
        int qtyProduto = 0;
        
        Console.WriteLine("\nDigite o SKU do Produto:");
        skuProduto = Console.ReadLine();
        
        Console.WriteLine("Digite o Nome do Produto:");
        nomeProduto = Console.ReadLine();
        
        Console.WriteLine("Digite a Descricao do Produto:");
        descProduto = Console.ReadLine();
        
        Console.WriteLine("Digite o Preco Unitário do Produto:");
        double.TryParse(Console.ReadLine(), out double _precoUnitProduto);
        
        Console.WriteLine("Digite a Quantidade do Produto:");
        int.TryParse(Console.ReadLine(), out qtyProduto);

        Produto produto = new Produto(skuProduto, nomeProduto, _precoUnitProduto, descProduto, qtyProduto);
        //produtos.Add(produto);
        DBContext.RepositorioProdutos.Adicionar(produto);
        
        Console.WriteLine("Pressione qualquer tecla para continuar...");
        Console.ReadKey();
        TelaPrincipal.Show();
    }

}
