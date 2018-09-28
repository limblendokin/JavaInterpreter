using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JavaInterpreter
{
    class BytecodeFileReader
    {
        public JavaClass Read(String path)
        {
            if (File.Exists(path))
            {
                byte[] bytecode = File.ReadAllBytes(path);
                BytecodeReader reader = new BytecodeReader(bytecode);
                return JavaClassInitializer.Initialize(reader);
            }
            else throw new FileNotFoundException();
        }
    }
}
