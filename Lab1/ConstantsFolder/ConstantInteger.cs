using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab1.ConstantsFolder
{
    class ConstantInteger : Constant
    {
        private int value;
        public int Value { get => value; }

        public ConstantInteger(byte tag, int value) : base(tag)
        {
            this.value = value;
        }
        public static ConstantInteger CreateConstantInteger(byte[]code, ref int curIndex)
        {
            byte tag = code[curIndex++];

            int value = BitConverter.ToInt32(code, curIndex);
            curIndex += 4;

            return new ConstantInteger(tag, value);
        }
    }
}
