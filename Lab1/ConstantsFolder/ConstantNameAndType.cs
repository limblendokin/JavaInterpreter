using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab1.ConstantsFolder
{
    class ConstantNameAndType : Constant
    {
        private ushort nameIndex;
        public ushort NameIndex { get => nameIndex; }

        private ushort descriptorIndex;
        public ushort DescriptorIndex { get => descriptorIndex; }
        
        public ConstantNameAndType(byte tag, ushort nameIndex, ushort descriptorIndex) : base(tag)
        {
            this.nameIndex = nameIndex;
            this.descriptorIndex = descriptorIndex;
        }
        public new static ConstantNameAndType Create(byte[] code, ref int curIndex)
        {
            byte tag = code[curIndex++];

            ushort nameIndex = Helper.ToUShort(code, ref curIndex);

            ushort descriptorIndex = Helper.ToUShort(code, ref curIndex);

            return new ConstantNameAndType(tag, nameIndex, descriptorIndex);
        }
    }
}
