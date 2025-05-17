using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sysvendas2.Telas
{
    public class ShowHeader
    {
        public static void Header(String Titulo)
        {
            switch (Titulo)
            {
                case "Principal":
                    Console.WriteLine("========================================");
                    Console.WriteLine("=========== 🔥 SYSVENDAS 2 🔥 ==========");
                    Console.WriteLine("========================================\n");
                    break;
                case "BuscaCliente":
                    Console.WriteLine("=======================================");
                    Console.WriteLine("====== 🔎 BUSCA DE CLIENTE 🔍 ========");
                    Console.WriteLine("=======================================\n");
                    break;
                case "CadastroCliente":
                    Console.WriteLine("=============================================");
                    Console.WriteLine("====== 🧑‍🦳 CADASTRO DE CLIENTES 🧑‍🦳 ======");
                    Console.WriteLine("=============================================\n");
                    break;
                case "ListaCliente":
                    Console.WriteLine("=============================================");
                    Console.WriteLine("======= 🧑‍🦳 LISTA DE CLIENTES 🧑‍🦳 ========");
                    Console.WriteLine("=============================================\n");
                    break;
                case "CadastroProduto":
                    Console.WriteLine("=============================================");
                    Console.WriteLine("====== 🧑‍🦳 CADASTRO DE PRODUTOS 🧑‍🦳 ======");
                    Console.WriteLine("=============================================\n");
                    break;
                case "ListaProduto":
                    Console.WriteLine("=============================================");
                    Console.WriteLine("======= 🧑‍🦳 LISTA DE PRODUTOS 🧑‍🦳 ========");
                    Console.WriteLine("=============================================\n");
                    break;

            }

        }
    }
}
