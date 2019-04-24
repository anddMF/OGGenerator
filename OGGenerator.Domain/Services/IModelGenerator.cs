using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OGGenerator.Domain.Services
{
    public interface IModelGenerator
    {
        void Generate(string table, CommandType type, Dictionary<string, object> param = null);
        void GetAll();
        void GenerateAll();
        void GenerateFromProc(string procName);
    }
}
