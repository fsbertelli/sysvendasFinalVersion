﻿using System.Text.Json;
using sysvendas2.Context;
using sysvendas2.Repository;
using sysvendas2.Telas;
using sysvendas2.Models;

//DBContext.RepositorioClientes = new RepositorioClienteJson("clientes.json");
//DBContext.RepositorioProdutos = new RepositorioProdutoJson("produtos.json");
//DBContext.RepositorioPedidos = new RepositorioPedidoJson("pedidos.json");

var connStr = "Host=localhost;Port=32768;Username=postgres;Password=capoeirarosa;Database=dev";
DBContext.RepositorioClientes = new RepositorioClientePostgres(connStr);
DBContext.RepositorioProdutos = new RepositorioProdutoPostgres(connStr);
DBContext.RepositorioPedidos = new RepositorioPedidoPostgres(connStr);
DBContext.RepositorioItensPedido = new RepositorioItemPedidoPostgres(connStr);

((RepositorioClientePostgres)DBContext.RepositorioClientes).CriarTabelas();
((RepositorioProdutoPostgres)DBContext.RepositorioProdutos).CriarTabelas();
((RepositorioPedidoPostgres)DBContext.RepositorioPedidos).CriarTabelas();
((RepositorioItemPedidoPostgres)DBContext.RepositorioItensPedido).CriarTabelas();
TelaPrincipal.Show();
