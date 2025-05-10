using Dapper;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using sysvendas2.Interfaces;
using sysvendas2.Models;

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
            var sql = @"
        CREATE TABLE IF NOT EXISTS Produtos (
            Sku TEXT PRIMARY KEY,
            Nome TEXT NOT NULL,
            PrecoUnit DOUBLE PRECISION NOT NULL,
            Desc TEXT NOT NULL
        );";
            using var conn = new NpgsqlConnection(_connStr);
            conn.Execute(sql);
        }
        public void Adicionar(Produto produto)
        {
            var sql = "INSERT INTO Produtos (Sku, Nome, PrecoUnit, Desc) VALUES (@Sku, @Nome, @PrecoUnit, @Desc)";
            using var conn = new NpgsqlConnection(_connStr);
            conn.Execute(sql, produto);
        }

        public List<Produto> ObterTodos()
        {
            var sql = "SELECT * FROM Produtos ORDER BY Nome";
            using var conn = new NpgsqlConnection(_connStr);
            return conn.Query<Produto>(sql).ToList();
        }
        
        public Produto ObterProduto(string sku)
        {
            var sql = "SELECT * FROM Produtos WHERE Sku = @Sku";
            using var conn = new NpgsqlConnection(_connStr);
            return conn.QueryFirstOrDefault<Produto>(sql, new { Sku = sku });
        }
    }

}

