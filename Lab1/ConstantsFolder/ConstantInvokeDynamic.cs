using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab1.ConstantsFolder
{
    class ConstantInvokeDynamic : Constant
    {
        private ushort bootstrapMethodAttrIndex;
        public ushort BootstrapMethodAttrIndex { get => bootstrapMethodAttrIndex; }

        private ushort nameAndTypeIndex;
        public ushort NameAndTypeIndex { get => nameAndTypeIndex; }
        

        public ConstantInvokeDynamic(byte tag, ushort bootstrapMethodAttrIndex, ushort nameAndTypeIndex) : base(tag)
        {
            this.bootstrapMethodAttrIndex = bootstrapMethodAttrIndex;
            this.nameAndTypeIndex = nameAndTypeIndex;
        }
        public new static ConstantInvokeDynamic Create(byte[] code, ref int curIndex)
        {
            byte tag = code[curIndex++];

            ushort bootstrapMethodAttrIndex = Helper.ToUShort(code, ref curIndex);

            ushort nameAndTypeIndex = Helper.ToUShort(code, ref curIndex);

            return new ConstantInvokeDynamic(tag, bootstrapMethodAttrIndex, nameAndTypeIndex);
        }
    }
}
