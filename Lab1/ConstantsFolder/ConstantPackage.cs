using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab1.ConstantsFolder
{
    class ConstantPackage
    {
        private ushort nameIndex;
        public ushort NameIndex => nameIndex;
        public ConstantPackage(ushort nameIndex)
        {
            this.nameIndex = nameIndex;
        }
    }
}
