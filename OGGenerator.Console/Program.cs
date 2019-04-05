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

                Console.Write("\n=>Insira a table: ");
                var tableName = Console.ReadLine();

                IModelGenerator svc = new ModelGenerator();
                svc.DoGenerator(tableName, CommandType.Text);

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
    }
}
