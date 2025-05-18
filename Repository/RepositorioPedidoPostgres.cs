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
    public class RepositorioPedidoPostgres : IRepositorioPedido
    {
        private readonly string _connStr;

        public RepositorioPedidoPostgres(string connStr)
        {
            _connStr = connStr;
        }

        public void CriarTabelas()
        {
            try
            {
                var sql = @"
                CREATE TABLE IF NOT EXISTS 
                Pedidos (
                IdPedido SERIAL PRIMARY KEY,
                Data TIMESTAMP NOT NULL,
                IdCliente INT NOT NULL,
                Status TEXT NOT NULL,
                Total DOUBLE PRECISION NOT NULL,
                FOREIGN KEY (IdCliente) REFERENCES Clientes(IdCliente)
            );";
                using var conn = new NpgsqlConnection(_connStr);
                conn.Execute(sql);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erro ao criar tabela de pedidos!");
                Console.WriteLine("Pressione qualquer tecla para continuar...");
                Console.ReadKey();
                TelaPrincipal.Show();
            }
        }

        public void Adicionar(Pedido pedido)
        {
            try
            {
                if (pedido == null)
                {
                    Console.WriteLine("Pedido não pode ser nulo!");
                    return;
                }

                var sql = @"
                INSERT INTO Pedidos (Data, IdCliente, Status, Total) 
                VALUES (@Data, @IdCliente, @Status, @Total) 
                RETURNING IdPedido";

                using var conn = new NpgsqlConnection(_connStr);
                conn.Open();

                using var transaction = conn.BeginTransaction();
                try
                {
                    var parameters = new
                    {
                        Data = pedido.Data,
                        IdCliente = pedido.Cliente.IdCliente,
                        Status = pedido.Status,
                        Total = pedido.Total
                    };

                    var idPedido = conn.ExecuteScalar<int>(sql, parameters, transaction);
                    pedido.IdPedido = idPedido;

                    transaction.Commit();

                    Console.WriteLine($"\nPedido #{idPedido} cadastrado com sucesso!");
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    throw new Exception($"Erro ao adicionar pedido: {ex.Message}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao adicionar pedido: {ex.Message}");
                Console.WriteLine("Pressione qualquer tecla para continuar...");
                Console.ReadKey();
                TelaPrincipal.Show();
            }
        }

        public List<Pedido> ObterTodos()
        {
            try
            {
                var sql = @"
                SELECT p.*, c.* 
                FROM Pedidos p
                INNER JOIN Clientes c ON p.IdCliente = c.IdCliente
                ORDER BY p.Data DESC";

                using var conn = new NpgsqlConnection(_connStr);

                var pedidos = conn.Query<Pedido, Cliente, Pedido>(
                    sql,
                    (pedido, cliente) =>
                    {
                        pedido.Cliente = cliente;
                        return pedido;
                    },
                    splitOn: "IdCliente"
                ).ToList();

                return pedidos;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao buscar pedidos: {ex.Message}");
                Console.WriteLine("Pressione qualquer tecla para continuar...");
                Console.ReadKey();
                TelaPrincipal.Show();
                return new List<Pedido>();
            }
        }

        public Pedido ObterPorId(int idPedido)
        {
            try
            {
                var sql = @"
                SELECT p.*, c.* 
                FROM Pedidos p
                INNER JOIN Clientes c ON p.IdCliente = c.IdCliente
                WHERE p.IdPedido = @IdPedido";

                using var conn = new NpgsqlConnection(_connStr);

                var pedido = conn.Query<Pedido, Cliente, Pedido>(
                    sql,
                    (pedido, cliente) =>
                    {
                        pedido.Cliente = cliente;
                        return pedido;
                    },
                    new { IdPedido = idPedido },
                    splitOn: "IdCliente"
                ).FirstOrDefault();

                return pedido;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao buscar pedido #{idPedido}: {ex.Message}");
                Console.WriteLine("Pressione qualquer tecla para continuar...");
                Console.ReadKey();
                TelaPrincipal.Show();
                return null;
            }
        }

        public void AtualizarStatus(int idPedido, string novoStatus)
        {
            try
            {
                var sql = "UPDATE Pedidos SET Status = @Status WHERE IdPedido = @IdPedido";
                using var conn = new NpgsqlConnection(_connStr);
                conn.Execute(sql, new { Status = novoStatus, IdPedido = idPedido });
                Console.WriteLine($"Status do pedido #{idPedido} atualizado para '{novoStatus}'!");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao atualizar status do pedido: {ex.Message}");
            }
        }
    }
}

