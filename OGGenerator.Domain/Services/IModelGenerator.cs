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
        void DoGenerator(string table, CommandType type);
    }
}
