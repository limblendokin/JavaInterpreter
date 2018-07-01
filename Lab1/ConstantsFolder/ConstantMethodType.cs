using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab1.ConstantsFolder
{
    class ConstantMethodType : Constant
    {
        private ushort descriptorIndex;
        public ushort DescriptorIndex { get => descriptorIndex; }
        

        public ConstantMethodType(byte tag, ushort descriptorIndex) : base(tag)
        {
            this.descriptorIndex = descriptorIndex;
        }
        public new static ConstantMethodType Create(byte[] code, ref int curIndex)
        {
            byte tag = code[curIndex++];

            ushort descriptorIndex = Helper.ToUShort(code, ref curIndex);

            return new ConstantMethodType(tag, descriptorIndex);
        }
    }
}
