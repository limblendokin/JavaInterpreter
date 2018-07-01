using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab1.ConstantsFolder
{
    class ConstantModule : Constant
    {
        private ushort nameIndex;
        public ushort NameIndex { get => nameIndex; }

        public ConstantModule(byte tag, ushort nameIndex) : base(tag)
        {
            this.nameIndex = nameIndex;
        }
        public new static ConstantModule Create(byte[] code, ref int curIndex)
        {
            byte tag = code[curIndex++];

            ushort nameIndex = Helper.ToUShort(code, ref curIndex);

            return new ConstantModule(tag, nameIndex);
        }
    }
}
