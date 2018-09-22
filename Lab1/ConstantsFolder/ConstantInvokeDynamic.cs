using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab1.ConstantsFolder
{
    class ConstantInvokeDynamic
    {
        private ushort bootstrapMethodAttrIndex;
        public ushort BootstrapMethodAttrIndex => bootstrapMethodAttrIndex;

        private ushort nameAndTypeIndex;
        public ushort NameAndTypeIndex => nameAndTypeIndex;
        

        public ConstantInvokeDynamic(ushort bootstrapMethodAttrIndex, ushort nameAndTypeIndex)
        {
            this.bootstrapMethodAttrIndex = bootstrapMethodAttrIndex;
            this.nameAndTypeIndex = nameAndTypeIndex;
        }
    }
}
