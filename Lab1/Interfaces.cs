using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab1
{
    class Interfaces
    {
        // TODO: realization
        public Interfaces()
        {

        }
        public static Interfaces Create(byte[] code, ushort interfacesCount, ref int curIndex, ConstantPool constantPool)
        {
            return new Interfaces();
        }
    }
}
