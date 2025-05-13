using sysvendas2.Interfaces;
using sysvendas2.Models;
using Dapper;
using Npgsql;
using sysvendas2.Telas;

namespace sysvendas2.Repository;

public class RepositorioClientePostgres : IRepositorioCliente
{
    private readonly string _connStr;

    public RepositorioClientePostgres(string connStr)
    {
        _connStr = connStr;
    }
    
    public void CriarTabelas()
    {
        try
        {
            var sql = @"
                CREATE TABLE IF NOT EXISTS 
                Clientes (
                IdCliente SERIAL PRIMARY KEY,
                Nome TEXT NOT NULL,
                Email TEXT NOT NULL,
                Telefone TEXT NOT NULL
            );";
            using var conn = new NpgsqlConnection(_connStr);
            conn.Execute(sql);
        } catch (Exception ex)
        {
            Console.WriteLine($"Erro ao criar tabelas! ");
            Console.WriteLine("Pressione qualquer tecla para continuar...");
            Console.ReadKey();
            TelaPrincipal.Show();
        }

    }
    
    public void Adicionar(Cliente cliente)
    {
        try
        {
            if (cliente == null)
            {
                Console.WriteLine("Cliente não pode ser nulo!");
                return;
            }
            var sql = "INSERT INTO Clientes (Nome, Email, Telefone) VALUES (@Nome, @Email, @Telefone)";
            using var conn = new NpgsqlConnection(_connStr);
            conn.Execute(sql, cliente);
            Console.WriteLine($"\nCliente {cliente.Nome} cadastrado com sucesso!");
            Console.WriteLine("Pressione qualquer tecla para continuar...");
            Console.ReadKey();
            TelaPrincipal.Show();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Erro ao adicionar cliente {cliente.Nome}! ");
            Console.WriteLine("Pressione qualquer tecla para continuar...");
            Console.ReadKey();
            TelaPrincipal.Show();
        }

    }

    public List<Cliente> ObterTodos()
    {
        try
        {
            var sql = "SELECT * FROM Clientes ORDER BY Nome";
            using var conn = new NpgsqlConnection(_connStr);
            return conn.Query<Cliente>(sql).ToList();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Erro ao buscar clientes! ");
            Console.WriteLine("Pressione qualquer tecla para continuar...");
            Console.ReadKey();
            TelaPrincipal.Show();
            return null;
        }
    }

    public Cliente? ObterClienteId(int idcliente)
    {
        try
        {
            throw new NotImplementedException();
        } catch (Exception ex)
        {
            Console.WriteLine($"Erro ao obter cliente com id {idcliente}! ");
            Console.WriteLine("Pressione qualquer tecla para continuar...");
            Console.ReadKey();
            TelaPrincipal.Show();
            return null;
        }
    }

    public Cliente? ObterClienteNome(string nome)
    {
        try
        {
            var sql = "SELECT * FROM Clientes WHERE Nome ILIKE @Nome";
            using var conn = new NpgsqlConnection(_connStr);
            return conn.QueryFirstOrDefault<Cliente>(sql, new { Nome = $"%{nome}%" });

        } catch (Exception ex)
        {
            Console.WriteLine($"Erro ao buscar cliente {nome}");
            Console.WriteLine("Pressione qualquer tecla para continuar...");
            Console.ReadKey();
            TelaPrincipal.Show();
            return null;
        }

    }
}