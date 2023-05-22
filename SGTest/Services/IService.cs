using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGTest.Services
{
    internal interface IService
    {
        public void SaveToDb(string[] parsedLine, int lineNumber);
    }
}
