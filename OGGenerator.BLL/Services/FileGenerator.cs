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
        public bool Generate(string type, string name)
        {
            string pathFolder = string.Format("{0}{1}", AppDomain.CurrentDomain.BaseDirectory, "models");
            string pathFolderAppConfig = string.Format("{0}{1}", ConfigurationManager.AppSettings["PastaModels"], "\\models");

            if (!Directory.Exists(pathFolderAppConfig))
            {
                Directory.CreateDirectory(pathFolderAppConfig);
            }

            string filepath = string.Format("{0}\\{1}.txt", pathFolderAppConfig, "model");
            try
            {
                if (!File.Exists(filepath))
                {
                    using (StreamWriter sw = File.CreateText(filepath))
                    {
                        sw.WriteLine("public " + type.ToLower() + " " + name + " { get; set; }");
                    }
                }
                else
                {
                    using (StreamWriter sw = File.AppendText(filepath))
                    {
                        sw.WriteLine("public " + type + " " + name + " { get; set; }");
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
