using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab1
{
    class BytecodeFileReader
    {
        public JavaClass Read(String path)
        {
            if (File.Exists(path))
            {
                byte[] bytecode = File.ReadAllBytes(path);
                return new JavaClass(bytecode);
            }
            else throw new FileNotFoundException();
        }
    }
}
