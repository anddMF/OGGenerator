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
                IModelGenerator svc = new ModelGenerator();
                while (!sair)
                {
                    menu = DoOptions();

                    if (menu < 1 || menu > 4)
                    {
                        Console.WriteLine("\n==! Opcao invalida");
                    }
                    else
                    {
                        switch (menu)
                        {
                            case 1:
                                Console.WriteLine("\n==| Opcao 1");
                                Console.WriteLine("==| Executando...\n");
                                 
                                svc.GetAll();
                                Console.ReadKey();
                                sair = true;
                                break;
                            case 2:
                                Console.WriteLine("\n==| Opcao 2");
                                Console.Write("\n=>Insira a table: ");
                                var tableName = Console.ReadLine();
                                 
                                svc.Generate(tableName, CommandType.Text);

                                Console.ReadKey();
                                sair = true;
                                break;
                            case 3:
                                Console.WriteLine("\n==| Opcao 3");
                                Console.WriteLine("==| Executando...\n");

                                svc.GenerateAll();

                                Console.ReadKey();
                                sair = true;
                                break;
                            case 4:
                                Console.WriteLine("\n==| Opcao 4");
                                Console.Write("\n=>Insira o nome da proc: ");
                                var procName = Console.ReadLine();
                                svc.GenerateFromProc(procName);

                                Console.ReadKey();
                                sair = true;
                                break;
                            default:
                                Console.WriteLine("\n==! Conseguiu fazer merda, parabéns");
                                break;
                        }
                    }

                }

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

            return _continuar.ToLower();
        }

        public static int DoOptions()
        {
            int option = 0;
            try
            {
                Console.WriteLine("\n\n==> Escolha a operacao desejada: ");
                Console.WriteLine("\t 1 - Listar todas as tabelas do DB");
                Console.WriteLine("\t 2 - Digitar a tabela desejada");
                Console.WriteLine("\t 3 - Gerar model de todas as tables do DB");
                Console.WriteLine("\t 4 - Digitar o nome da proc para consultar parametros");
                option = Convert.ToInt32(Console.ReadLine());

                return option;
            }
            catch (Exception ex)
            {
                Console.WriteLine("\n==! Erro de conversao: " + ex.Message);
                return 0;
            }
        }
    }
}
