using OGGenerator.CrossCutting;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OGGenerator.BLL.Services
{
    public class FileGenerator
    {
        public bool Generate(string type, string name, string tableName)
        {
            string pathFolder = string.Format("{0}{1}", AppDomain.CurrentDomain.BaseDirectory, "models");
            string pathFolderAppConfig = string.Format("{0}{1}", ConfigurationManager.AppSettings["PastaModels"], "\\models");

            if (!Directory.Exists(pathFolderAppConfig))
            {
                Directory.CreateDirectory(pathFolderAppConfig);
            }

            string filepath = string.Format("{0}\\{1}.txt", pathFolderAppConfig, tableName);
            try
            {
                string lowerType = type;
                if(type == "String")
                     lowerType = type.ToLowerInvariant();

                if(type == "Boolean")
                     lowerType = "bool";

                if(type == "Int32" || type == "Int64" || type == "Int16")
                     lowerType = "int";

                if (!File.Exists(filepath))
                {
                    using (StreamWriter sw = File.CreateText(filepath))
                    {
                        sw.WriteLine("public class "+tableName+" {");
                        sw.WriteLine("\tpublic " + lowerType + " " + name + " { get; set; }");
                    }
                }
                else
                {
                    using (StreamWriter sw = File.AppendText(filepath))
                    {
                        sw.WriteLine("\tpublic " + lowerType + " " + name + " { get; set; }");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Logger.WriteToLogFile(Logger.logType.ERROR, ex.Message, "FileGenerator.Generate()");
            }
            return false;
        }
    }
}
