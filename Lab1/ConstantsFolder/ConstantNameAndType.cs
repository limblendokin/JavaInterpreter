using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab1.ConstantsFolder
{
    class ConstantNameAndType
    {
        private ushort nameIndex;
        public ushort NameIndex => nameIndex;

        private ushort descriptorIndex;
        public ushort DescriptorIndex => descriptorIndex;
        
        public ConstantNameAndType(ushort nameIndex, ushort descriptorIndex)
        {
            this.nameIndex = nameIndex;
            this.descriptorIndex = descriptorIndex;
        }
    }
}
