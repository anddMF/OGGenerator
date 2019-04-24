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
        public void Generate(string table, CommandType type, Dictionary<string, object> param = null)
        {
            var conn = new OGGenerator.Infrastructure.ConnectionFactory();
            var fileSvc = new FileGenerator();

            try
            {
                if (type == CommandType.Text)
                {
                    SqlDataReader result = conn.GetReader("select top 1 * from " + table, type);
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
                else if (type == CommandType.StoredProcedure)
                {
                    SqlDataReader result = conn.GetReader(table, type, param);
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

                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i <= dt.Rows.Count - 1; i++)
                    {
                        Console.WriteLine(dt.Rows[i][0].ToString());
                    }
                }
                else
                {
                    Console.WriteLine("\n==! Nenhuma tabela foi encontrada");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Logger.WriteToLogFile(Logger.logType.ERROR, ex.Message, "ModelGenerator.GetAll()");
            }
        }

        public void GenerateFromProc(string procName)
        {
            var conn = new OGGenerator.Infrastructure.ConnectionFactory();
            var parametros = new Dictionary<string, object>();
            try
            {
                SqlDataReader result = conn.GetReader($"SELECT PARAMETER_NAME, DATA_TYPE FROM INFORMATION_SCHEMA.PARAMETERS WHERE SPECIFIC_NAME='{procName}'", CommandType.Text);

                DataTable dt = new DataTable();
                dt.Load(result);

                if (dt.Rows.Count > 0)
                {
                    object param = "";
                    Console.WriteLine("\n==> Numero de parametros: " + dt.Rows.Count);
                    for (int i = 0; i <= dt.Rows.Count - 1; i++)
                    {
                        var nome = dt.Rows[i][0].ToString();
                        var tipo = dt.Rows[i][1].ToString();
                        Console.WriteLine("\t==> Nome do atributo: " + nome + ", tipo: " + tipo + "\n\t==> Digite o parametro que deseja enviar: ");

                        //switch (tipo)
                        //{
                        //    case tipo == "int" || tipo == "integer":
                        //        param = Convert.ToInt32(Console.ReadLine());
                        //        break;
                        //    default:
                        //        Console.WriteLine("\n==! Esse tipo de parametro ainda nao foi mapeado");
                        //        break;
                        //}

                        if (tipo == "int" || tipo == "integer")
                            param = Convert.ToInt32(Console.ReadLine());

                        else if (tipo == "date" || tipo == "datetime")
                            param = Convert.ToDateTime(Console.ReadLine());

                        else if (tipo == "varchar" || tipo == "nvarchar")
                            param = Console.ReadLine();

                        else if (tipo == "decimal")
                            param = Convert.ToDecimal(Console.ReadLine());

                        else if (tipo == "bit")
                            param = Convert.ToBoolean(Console.ReadLine());

                        parametros.Add(nome, param);
                    }
                    Console.WriteLine("\n==| Gerando model...");
                    Generate(procName, CommandType.StoredProcedure, parametros);
                }
                else
                {
                    Console.WriteLine("\n==! Nenhuma tabela foi encontrada");
                }
            } 
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message + "Esse tipo ainda não foi mapeado, mande o arquivo de log ou o nome do tipo para o email andrew.moraes.ferreira@everis.com");
                Logger.WriteToLogFile(Logger.logType.ERROR, ex.Message, "ModelGenerator.GenerateFromProc()");
            }
        }
    }
}
