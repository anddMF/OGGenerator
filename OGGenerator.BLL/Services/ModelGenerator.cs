using OGGenerator.CrossCutting;
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
        public void Generate(string table, CommandType type)
        {
            var conn = new OGGenerator.Infrastructure.ConnectionFactory();
            var fileSvc = new FileGenerator();

            try
            {
                SqlDataReader result = conn.GetReader("select top 1 * from " + table, CommandType.Text);

                DataTable dt = new DataTable();
                dt.Load(result);

                foreach (DataColumn column in dt.Columns)
                {
                    var propType = column.DataType.Name;
                    var propName = column.ColumnName;
                    fileSvc.Generate(propType, propName, table);
                    Console.WriteLine("public " + propType.ToLower() + " " + propName + " { get; set; } ");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Logger.WriteToLogFile(Logger.logType.ERROR, ex.Message, "ModelGenerator.Generate()");
            }

        }

        public void GenerateAll()
        {
            var conn = new OGGenerator.Infrastructure.ConnectionFactory();
            try
            {
                SqlDataReader result = conn.GetReader("SELECT TABLE_NAME FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_TYPE = 'BASE TABLE' ", CommandType.Text);

                DataTable dt = new DataTable();
                dt.Load(result);

                for (int i = 0; i <= dt.Rows.Count - 1; i++)
                {
                    Generate(dt.Rows[i][0].ToString(), CommandType.Text);
                }

                Console.WriteLine("\n==> Executado com sucesso");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Logger.WriteToLogFile(Logger.logType.ERROR, ex.Message, "ModelGenerator.GetAll()");
            }
        }

        public void GetAll()
        {
            var conn = new OGGenerator.Infrastructure.ConnectionFactory();
            try
            {
                SqlDataReader result = conn.GetReader("SELECT TABLE_NAME FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_TYPE = 'BASE TABLE' ", CommandType.Text);

                DataTable dt = new DataTable();
                dt.Load(result);

                for (int i = 0; i <= dt.Rows.Count - 1; i++)
                {
                    Console.WriteLine(dt.Rows[i][0].ToString());
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Logger.WriteToLogFile(Logger.logType.ERROR, ex.Message, "ModelGenerator.GetAll()");
            }
        }
        
    }
}
