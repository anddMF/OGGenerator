using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OGGenerator.CrossCutting
{
    public class Logger
    {
        public enum logType { DEBUG, INFO, WARN, ERROR, FATAL }

        public static void WriteToLogFile(logType typeLog, string messageErro, string method)
        {

            #region Folder ops
            string pathFolder = string.Format("{0}{1}", AppDomain.CurrentDomain.BaseDirectory, "logs");
            if (!Directory.Exists(pathFolder))
            {
                Directory.CreateDirectory(pathFolder);
            }
            #endregion

            string filepath = string.Format("{0}\\{1}.txt", pathFolder, "logs");

            if (!File.Exists(filepath))
            {
                #region CreateFile
                CreateFileLog(typeLog, messageErro, filepath, method);
                #endregion
            }
            else
            {
                #region Size and Append
                if (ConvertBytesToMegabytes(File.ReadAllBytes(filepath).Length) >= 20.0)
                {
                    string[] txtList = Directory.GetFiles(pathFolder, "*.txt");
                    foreach (string f in txtList)
                    {
                        File.Delete(f);
                    }

                    CreateFileLog(typeLog, messageErro, filepath, method);
                }
                else
                {
                    #region Append
                    using (StreamWriter sw = File.AppendText(filepath))
                    {
                        sw.WriteLine(string.Format("{0} [{1}]: {2} {3}", DateTime.Now, typeLog, messageErro, method));
                    }
                    #endregion
                }
                #endregion
            }
        }

        private static void CreateFileLog(logType typeLog, string messageErro, string filepath, string method)
        {
            using (StreamWriter sw = File.CreateText(filepath))
            {
                sw.WriteLine(string.Format("{0} [{1}]: {2} {3}", DateTime.Now, typeLog, messageErro, method));
            }
        }

        private static double ConvertBytesToMegabytes(long bytes)
        {
            return (bytes / 1024f) / 1024f;
        }
    }
}
