using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab1.ConstantsFolder
{
    class ConstantClass : Constant
    {
        private ushort nameIndex;
        public ushort NameIndex => nameIndex;

        public ConstantClass(byte tag, ushort nameIndex) : base(tag)
        {
            this.nameIndex = nameIndex;
        }
        public new static ConstantClass Create(byte[] code, ref int curIndex)
        {
            byte tag = code[curIndex++];

            ushort nameIndex = Helper.ToUShort(code, ref curIndex);

            return new ConstantClass(tag, nameIndex);
        }
    }
}
