using OGGenerator.BLL.Services;
using OGGenerator.Domain.Services;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OGGenerator.ConsoleOps
{
    class Program
    {
        static void Main(string[] args)
        {
            #region compile
            string continuar = "y";
            Console.WriteLine("===================== Bem vindo =====================");
            Console.WriteLine("Verifique se preencheu a connstring e a pasta para os arquivos no App.Config \n *os logs estarao na mesma pasta*");
            Console.WriteLine("Enter para continuar");
            Console.ReadKey();
            do
            {
                continuar = "";
                bool sair = false;
                int menu = 0;
                while (!sair)
                {
                    menu = DoOptions();

                    if (menu < 1 && menu > 3)
                    {
                        Console.WriteLine("==! Opcao invalida");
                    }
                    else
                    {
                        switch (menu)
                        {
                            case 1:
                                Console.WriteLine("opcao 1");
                                break;
                            default:
                                Console.WriteLine("==! Conseguiu fazer merda, parabéns");
                                break;
                        }
                    }

                }

                Console.Write("\n=>Insira a table: ");
                var tableName = Console.ReadLine();

                IModelGenerator svc = new ModelGenerator();
                svc.Generate(tableName, CommandType.Text);

                Console.ReadKey();

                while (continuar != "y" && continuar != "n")
                    continuar = DoContinue();

            } while (continuar == "y");

            #endregion
        }

        

        public static string DoContinue()
        {
            string _continuar = "y";
            Console.WriteLine("\n==> Deseja continuar? (y/n)");
            _continuar = Console.ReadLine();

            if (_continuar != "y" && _continuar != "n")
                Console.WriteLine("\n==! Opcao inválida");

            return _continuar;
        }

        public static int DoOptions()
        {
            int option = 0;

            while (option < 1 && option > 3)
            {
                try
                {
                    Console.WriteLine("==> Escolha a operação desejada: ");
                    Console.WriteLine("\t 1 - Listar todas as tabelas do DB");
                    Console.WriteLine("\t 2 - Digitar a tabela desejada");
                    Console.WriteLine("\t 3 - Gerar model de todas as tables do DB");
                    option = Convert.ToInt32(Console.ReadLine());

                    return option;
                }
                catch (Exception ex)
                {
                    Console.WriteLine("==! Erro de conversão: " + ex.Message);
                    return 0;
                }
            }

            return 0;
        }
    }
}
