using Dapper;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using sysvendas2.Interfaces;
using sysvendas2.Models;
using sysvendas2.Telas;

namespace sysvendas2.Repository
{
    public class RepositorioItemPedidoPostgres : IRepositorioItemPedido
    {
        private readonly string _connStr;

        public RepositorioItemPedidoPostgres(string connStr)
        {
            _connStr = connStr;
        }

        public void CriarTabelas()
        {
            try
            {
                var sql = @"
                CREATE TABLE IF NOT EXISTS 
                ItensPedido (
                IdItemPedido SERIAL PRIMARY KEY,
                IdPedido INT NOT NULL,
                IdProduto INT NOT NULL,
                Quantidade INT NOT NULL,
                PrecoUnit DOUBLE PRECISION NOT NULL,
                Desconto INT NOT NULL,
                SubTotal DOUBLE PRECISION NOT NULL,
                FOREIGN KEY (IdPedido) REFERENCES Pedidos(IdPedido),
                FOREIGN KEY (IdProduto) REFERENCES Produtos(IdProduto)
            );";
                using var conn = new NpgsqlConnection(_connStr);
                conn.Execute(sql);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erro ao criar tabela de itens de pedido!");
                Console.WriteLine(ex.Message);
                Console.WriteLine("Pressione qualquer tecla para continuar...");
                Console.ReadKey();
                TelaPrincipal.Show();
            }
        }

        public void Adicionar(ItemPedido itemPedido)
        {
            try
            {
                if (itemPedido == null)
                {
                    Console.WriteLine("Item de pedido não pode ser nulo!");
                    return;
                }

                if (!VerificarEstoque(itemPedido.Produto.IdProduto, itemPedido.Quantidade))
                {
                    Console.WriteLine($"Estoque insuficiente para o produto {itemPedido.Produto.Nome}!");
                    return;
                }

                var sql = @"
                INSERT INTO ItensPedido (IdPedido, IdProduto, Quantidade, PrecoUnit, Desconto, SubTotal) 
                VALUES (@IdPedido, @IdProduto, @Quantidade, @PrecoUnit, @Desconto, @SubTotal)";

                using var conn = new NpgsqlConnection(_connStr);
                conn.Open();

                using var transaction = conn.BeginTransaction();
                try
                {
                    var parameters = new
                    {
                        IdPedido = itemPedido.Pedido.IdPedido,
                        IdProduto = itemPedido.Produto.IdProduto,
                        itemPedido.Quantidade,
                        itemPedido.PrecoUnit,
                        itemPedido.Desconto,
                        itemPedido.SubTotal
                    };

                    conn.Execute(sql, parameters, transaction);

                    AtualizarEstoque(itemPedido.Produto.IdProduto, itemPedido.Quantidade, transaction, conn);

                    transaction.Commit();
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    throw new Exception($"Erro ao adicionar item ao pedido: {ex.Message}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao adicionar item ao pedido: {ex.Message}");
                Console.WriteLine("Pressione qualquer tecla para continuar...");
                Console.ReadKey();
                TelaPrincipal.Show();
            }
        }

        public List<ItemPedido> ObterTodos()
        {
            try
            {
                var sql = @"
                SELECT i.*, p.*, pr.*
                FROM ItensPedido i
                INNER JOIN Pedidos p ON i.IdPedido = p.IdPedido
                INNER JOIN Produtos pr ON i.IdProduto = pr.IdProduto";

                using var conn = new NpgsqlConnection(_connStr);

                var itens = conn.Query<ItemPedido, Pedido, Produto, ItemPedido>(
                    sql,
                    (item, pedido, produto) => {
                        item.Pedido = pedido;
                        item.Produto = produto;
                        return item;
                    },
                    splitOn: "IdPedido,IdProduto"
                ).ToList();

                return itens;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao buscar itens de pedido: {ex.Message}");
                Console.WriteLine("Pressione qualquer tecla para continuar...");
                Console.ReadKey();
                TelaPrincipal.Show();
                return new List<ItemPedido>();
            }
        }

        public List<ItemPedido> ObterPorPedido(int idPedido)
        {
            try
            {
                var sql = @"
                SELECT i.*, p.*, pr.*
                FROM ItensPedido i
                INNER JOIN Pedidos p ON i.IdPedido = p.IdPedido
                INNER JOIN Produtos pr ON i.IdProduto = pr.IdProduto
                WHERE i.IdPedido = @IdPedido";

                using var conn = new NpgsqlConnection(_connStr);

                var itens = conn.Query<ItemPedido, Pedido, Produto, ItemPedido>(
                    sql,
                    (item, pedido, produto) => {
                        item.Pedido = pedido;
                        item.Produto = produto;
                        return item;
                    },
                    new { IdPedido = idPedido },
                    splitOn: "IdPedido,IdProduto"
                ).ToList();

                return itens;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao buscar itens do pedido #{idPedido}: {ex.Message}");
                Console.WriteLine("Pressione qualquer tecla para continuar...");
                Console.ReadKey();
                TelaPrincipal.Show();
                return new List<ItemPedido>();
            }
        }

        public void AtualizarEstoque(int idProduto, int quantidade)
        {
            using var conn = new NpgsqlConnection(_connStr);
            conn.Open();
            using var transaction = conn.BeginTransaction();

            try
            {
                AtualizarEstoque(idProduto, quantidade, transaction, conn);
                transaction.Commit();
            }
            catch (Exception ex)
            {
                transaction.Rollback();
                Console.WriteLine($"Erro ao atualizar estoque: {ex.Message}");
                Console.WriteLine("Pressione qualquer tecla para continuar...");
                Console.ReadKey();
                TelaPrincipal.Show();
            }
        }

        private void AtualizarEstoque(int idProduto, int quantidade, NpgsqlTransaction transaction, NpgsqlConnection conn)
        {
            var sql = @"
            UPDATE Produtos 
            SET Quantidade = Quantidade - @Quantidade 
            WHERE IdProduto = @IdProduto";

            conn.Execute(sql, new { IdProduto = idProduto, Quantidade = quantidade }, transaction);
        }

        private bool VerificarEstoque(int idProduto, int quantidade)
        {
            try
            {
                var sql = "SELECT Quantidade FROM Produtos WHERE IdProduto = @IdProduto";
                using var conn = new NpgsqlConnection(_connStr);

                int estoqueAtual = conn.ExecuteScalar<int>(sql, new { IdProduto = idProduto });

                return estoqueAtual >= quantidade;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao verificar estoque: {ex.Message}");
                return false;
            }
        }
    }
}
