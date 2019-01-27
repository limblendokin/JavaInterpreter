using System;
using System.IO;

namespace JavaInterpreter
{
    public static class FileBytecodeReader
    {
        public static byte[] Read(String path)
        {
            if (File.Exists(path))
            {
                byte[] bytecode = File.ReadAllBytes(path);
                return bytecode;
            }
            else throw new FileNotFoundException();
        }
    }
}
