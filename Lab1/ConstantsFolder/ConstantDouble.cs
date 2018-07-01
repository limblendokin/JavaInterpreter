using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab1.ConstantsFolder
{
    class ConstantDouble : Constant
    {
        private double value;
        private ConstantDouble(byte tag, double value) : base(tag)
        {
            this.value = value;
        }
        public new static ConstantDouble Create(byte[] code, ref int curIndex)
        {
            byte tag = code[curIndex++];

            double value = BitConverter.ToDouble(code, curIndex);
            curIndex += 8;

            return new ConstantDouble(tag, value);
        }
    }
}
