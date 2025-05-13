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
    internal class RepositorioProdutoPostgres : IRepositorioProduto
    {
        private readonly string _connStr;
        public RepositorioProdutoPostgres(string connStr)
        {
            _connStr = connStr;
        }
        public void CriarTabelas()
        {
            try
            {
                var sql = @"
                    CREATE TABLE IF NOT EXISTS 
                    Produtos (
                    IdProduto SERIAL PRIMARY KEY,
                    Sku TEXT NOT NULL,
                    Nome TEXT NOT NULL,
                    PrecoUnit DOUBLE PRECISION NOT NULL,
                    Descricao TEXT NOT NULL
                );";
                using var conn = new NpgsqlConnection(_connStr);
                conn.Execute(sql);
            }
            catch (Exception ex) {
                Console.WriteLine("Erro ao criar tabelas!");
                Console.WriteLine(ex.Message);
            }
        }
        public void Adicionar(Produto produto)
        {
            if(produto == null)
            {
                Console.WriteLine("Produto não pode ser nulo!");
                return;
            }

            try
            {
                var sql = "INSERT INTO Produtos (Sku, Nome, PrecoUnit, Descricao) VALUES (@Sku, @Nome, @PrecoUnit, @Desc)";
                using var conn = new NpgsqlConnection(_connStr);
                conn.Execute(sql, produto);
                Console.WriteLine($"\nProduto {produto.Nome} cadastrado com sucesso!");
                Console.WriteLine("Pressione qualquer tecla para continuar...");
                Console.ReadKey();
                TelaPrincipal.Show();
            }
            catch (Exception ex) {
                Console.WriteLine($"Erro ao adicionar o produto {produto.Nome}");
                Console.WriteLine("Pressione qualquer tecla para continuar...");
                Console.ReadKey();
                TelaPrincipal.Show();
            }

        }
        public List<Produto> ObterTodos()
        {
            try
            {
                var sql = "SELECT * FROM Produtos ORDER BY Nome";
                using var conn = new NpgsqlConnection(_connStr);
                return conn.Query<Produto>(sql).ToList();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao listar produtos!");
                Console.WriteLine("Pressione qualquer tecla para continuar...");
                Console.ReadKey();
                TelaPrincipal.Show();
                return null;
            }
        }
        public Produto ObterProduto(string sku)
        {
            try
            {
                var sql = "SELECT * FROM Produtos WHERE Sku = @Sku";
                using var conn = new NpgsqlConnection(_connStr);
                return conn.QueryFirstOrDefault<Produto>(sql, new { Sku = sku });
            }
            catch (Exception ex) {
                Console.WriteLine($"Erro ao buscar o SKU {sku}!");
                Console.WriteLine("Pressione qualquer tecla para continuar...");
                Console.ReadKey();
                TelaPrincipal.Show();
                return null;
            }
        }
    }
}

