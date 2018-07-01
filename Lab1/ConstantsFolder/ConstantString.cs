using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab1.ConstantsFolder
{
    class ConstantString : Constant
    {
        private ushort stringIndex;
        public ushort StringIndex { get => stringIndex; }
        public ConstantString(byte tag, ushort stringIndex) : base(tag)
        {
            this.stringIndex = stringIndex;
        }
        public new static ConstantString Create(byte[] code, ref int curIndex)
        {
            byte tag = code[curIndex++];

            ushort stringIndex = Helper.ToUShort(code, ref curIndex);

            return new ConstantString(tag, stringIndex);
        }
    }
}
