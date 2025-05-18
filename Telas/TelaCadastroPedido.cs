using System.Text;
using sysvendas2.Context;
using sysvendas2.Models;

namespace sysvendas2.Telas;

public class TelaCadastroPedido : ShowHeader
{
    public static void Show()
    {
        Console.Clear();
        Console.OutputEncoding = Encoding.UTF8;
        ShowHeader.Header("CadastroPedido");

        Cliente cliente = SelecionarCliente();
        if (cliente == null)
        {
            Console.WriteLine("\nOperação cancelada!");
            Console.WriteLine("Pressione qualquer tecla para continuar...");
            Console.ReadKey();
            TelaPrincipal.Show();
            return;
        }

        string statusPedido = "Pendente";
        Console.WriteLine($"Status: {statusPedido}");

        int idPedido = 0;
        DateTime dataPedido = DateTime.Now;

        Pedido pedido = new Pedido(idPedido, dataPedido, cliente, statusPedido);
        pedido.Items = new List<ItemPedido>();

        bool adicionarMaisItens = true;
        while (adicionarMaisItens)
        {
            bool itemAdicionado = AdicionarItemAoPedido(pedido);
            if (itemAdicionado)
            {
                Console.Write("\nDeseja adicionar mais itens ao pedido? (s/n): ");
                adicionarMaisItens = Console.ReadLine().ToLower() == "s";
            }
            else
            {
                Console.Write("\nTentar adicionar outro item? (s/n): ");
                adicionarMaisItens = Console.ReadLine().ToLower() == "s";
            }
        }

        if (pedido.Items.Count == 0)
        {
            Console.WriteLine("\nPedido não possui itens! Operação cancelada.");
            Console.WriteLine("Pressione qualquer tecla para continuar...");
            Console.ReadKey();
            TelaPrincipal.Show();
            return;
        }

        pedido.Total = pedido.Items.Sum(item => item.SubTotal);

        Console.WriteLine($"\nTotal do pedido: R$ {pedido.Total:F2}");
        Console.Write("Confirmar cadastro do pedido? (s/n): ");
        if (Console.ReadLine().ToLower() != "s")
        {
            Console.WriteLine("\nOperação cancelada!");
            Console.WriteLine("Pressione qualquer tecla para continuar...");
            Console.ReadKey();
            TelaPrincipal.Show();
            return;
        }

        DBContext.RepositorioPedidos.Adicionar(pedido);

        foreach (var item in pedido.Items)
        {
            item.Pedido = pedido; 
            DBContext.RepositorioItensPedido.Adicionar(item);
        }

        Console.WriteLine("\nPedido cadastrado com sucesso!");
        ExibeResumoPedido(pedido);

        Console.WriteLine("\nPressione qualquer tecla para voltar ao menu principal...");
        Console.ReadKey();
        TelaPrincipal.Show();
    }

    private static Cliente SelecionarCliente()
    {
        Console.WriteLine("\n--- Selecione um Cliente ---");
        var clientes = DBContext.RepositorioClientes.ObterTodos();

        if (clientes == null || clientes.Count == 0)
        {
            Console.WriteLine("Nenhum cliente cadastrado!");
            return null;
        }

        foreach (var c in clientes)
        {
            Console.WriteLine($"{c.IdCliente}: {c.Nome}");
        }

        Console.Write("\nDigite o ID do cliente: ");
        if (int.TryParse(Console.ReadLine(), out int idCliente))
        {
            return clientes.FirstOrDefault(c => c.IdCliente == idCliente);
        }

        return null;
    }

    private static bool AdicionarItemAoPedido(Pedido pedido)
    {
        Console.WriteLine("\n--- Adicionar Item ao Pedido ---");

        var produtos = DBContext.RepositorioProdutos.ObterTodos();
        if (produtos == null || produtos.Count == 0)
        {
            Console.WriteLine("Nenhum produto cadastrado!");
            return false;
        }

        Console.WriteLine("\nProdutos disponíveis:");
        foreach (var p in produtos)
        {
            Console.WriteLine($"SKU: {p.Sku} | {p.Nome} | Preço: R$ {p.PrecoUnit:F2} | Estoque: {p.Quantidade}");
        }

        Console.Write("\nDigite o SKU do produto: ");
        string sku = Console.ReadLine();

        Produto produto = DBContext.RepositorioProdutos.ObterProduto(sku);
        if (produto == null)
        {
            Console.WriteLine($"\nProduto com SKU {sku} não encontrado!");
            return false;
        }

        Console.WriteLine($"Estoque disponível: {produto.Quantidade}");
        Console.Write("Digite a quantidade: ");
        if (!int.TryParse(Console.ReadLine(), out int quantidade) || quantidade <= 0)
        {
            Console.WriteLine("Quantidade inválida!");
            return false;
        }

        if (quantidade > produto.Quantidade)
        {
            Console.WriteLine($"\nEstoque insuficiente! Disponível: {produto.Quantidade}");
            return false;
        }

        double precoUnit = produto.PrecoUnit;
        Console.WriteLine($"Preço unitário: R$ {precoUnit:F2}");

        Console.Write("Digite o desconto (%): ");
        if (!int.TryParse(Console.ReadLine(), out int desconto) || desconto < 0 || desconto > 100)
        {
            desconto = 0;
            Console.WriteLine("Desconto inválido ou fora dos limites! Usando 0%.");
        }

        ItemPedido item = new ItemPedido
        {
            Pedido = pedido,
            Produto = produto,
            Quantidade = quantidade,
            PrecoUnit = precoUnit,
            Desconto = desconto
        };

        item.CalcularSubTotal();

        pedido.Items.Add(item);

        Console.WriteLine($"\nItem adicionado! Subtotal: R$ {item.SubTotal:F2}");
        return true;
    }

    private static void ExibeResumoPedido(Pedido pedido)
    {
        Console.WriteLine("\n--- Resumo do Pedido ---");
        Console.WriteLine($"ID: {pedido.IdPedido}");
        Console.WriteLine($"Data: {pedido.Data}");
        Console.WriteLine($"Cliente: {pedido.Cliente.Nome} (ID: {pedido.Cliente.IdCliente})");
        Console.WriteLine($"Status: {pedido.Status}");

        Console.WriteLine("\n--- Itens do Pedido ---");
        foreach (var item in pedido.Items)
        {
            Console.WriteLine($"- {item.Produto.Nome} (SKU: {item.Produto.Sku})");
            Console.WriteLine($"  Qtd: {item.Quantidade} | Preço: R$ {item.PrecoUnit:F2} | Desconto: {item.Desconto}%");
            Console.WriteLine($"  Subtotal: R$ {item.SubTotal:F2}");
        }

        Console.WriteLine($"\nTotal do Pedido: R$ {pedido.Total:F2}");
    }
}