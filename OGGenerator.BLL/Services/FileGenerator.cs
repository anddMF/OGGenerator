using System;
using System.Collections.Generic;
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
            if (!Directory.Exists(pathFolder))
            {
                Directory.CreateDirectory(pathFolder);
            }

            string filepath = string.Format("{0}\\{1}.txt", pathFolder, "model");

            if (!File.Exists(filepath))
            {
                using (StreamWriter sw = File.CreateText(filepath))
                {
                    sw.WriteLine("public "+ type +" "+ name +" { get; set; }");
                }
            }
            else
            {
                using (StreamWriter sw = File.AppendText(filepath))
                {
                    sw.WriteLine("public " + type + " " + name + " { get; set; }");
                }
            }

            return false;
        }
    }
}
