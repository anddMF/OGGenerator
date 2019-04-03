using OGGenerator.Domain.Services;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OGGenerator.BLL.Services
{
    public class ModelGenerator : IModelGenerator
    {
        public void DoGenerator(string table, CommandType type)
        {
            var conn = new OGGenerator.Infrastructure.ConnectionFactory();
            var fileSvc = new FileGenerator();

            SqlDataReader result = conn.GetReader("select top 1 * from " + table, CommandType.Text);

            DataTable dt = new DataTable();
            dt.Load(result);

            foreach (DataColumn column in dt.Columns)
            {
                var propType = column.DataType.Name;
                var propName = column.ColumnName;
                fileSvc.Generate(propType, propName);
                Console.WriteLine("public "+ propType + " " + propName + " { get; set; } ");
            }
            Console.ReadKey();
        }
    }
}
