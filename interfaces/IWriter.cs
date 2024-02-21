using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NumberParser.interfaces
{
    interface IFileWriter
    {
        string Extension { get; }

        string[] Content { get; }

        void WriteToFile();
    }

}
