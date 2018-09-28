using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JavaInterpreter.ConstantsFolder
{
    class ConstantClass
    {
        private ushort nameIndex;
        public ushort NameIndex => nameIndex;
        
        public ConstantClass(ushort nameIndex)
        {
            this.nameIndex = nameIndex;
        }
        
    }
}
