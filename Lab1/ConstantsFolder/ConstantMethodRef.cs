using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab1.ConstantsFolder
{
    class ConstantMethodRef
    {
        private ushort classIndex;
        public ushort ClassIndex => classIndex;

        private ushort nameAndTypeIndex;
        public ushort NameAndTypeIndex => nameAndTypeIndex;
        

        public ConstantMethodRef(ushort classIndex, ushort nameAndTypeIndex)
        {
            this.classIndex = classIndex;
            this.nameAndTypeIndex = nameAndTypeIndex;
        }
    }
}
