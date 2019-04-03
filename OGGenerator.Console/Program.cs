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
            var continuar = 1;
            //do
            //{
                Console.WriteLine("Insira a table: ");
                var tableName = Console.ReadLine();

                IModelGenerator svc = new ModelGenerator();
                svc.DoGenerator(tableName, CommandType.Text);

                Console.ReadKey();
            //} while ();

            #endregion
        }
    }
}
