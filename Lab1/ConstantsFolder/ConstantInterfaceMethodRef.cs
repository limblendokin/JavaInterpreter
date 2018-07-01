using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab1.ConstantsFolder
{
    class ConstantInterfaceMethodRef : Constant
    {
        private ushort classIndex;
        public ushort ClassIndex { get => classIndex; }

        private ushort nameAndTypeIndex;
        public ushort NameAndTypeIndex { get => nameAndTypeIndex; }
        
        public ConstantInterfaceMethodRef(byte tag, ushort classIndex, ushort nameAndTypeIndex) : base(tag)
        {
            this.classIndex = classIndex;
            this.nameAndTypeIndex = nameAndTypeIndex;
        }
        public new static ConstantInterfaceMethodRef Create(byte[] code, ref int curIndex)
        {
            byte tag = code[curIndex++];

            ushort classIndex= Helper.ToUShort(code, ref curIndex);

            ushort nameAndTypeIndex = Helper.ToUShort(code, ref curIndex);

            return new ConstantInterfaceMethodRef(tag, classIndex, nameAndTypeIndex);
        }
        
    }
}
