using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab1.ConstantsFolder
{
    class ConstantLong : Constant
    {
        private long value;
        public long Value { get => value; }

        public ConstantLong(byte tag, long value) : base(tag)
        {
            this.value = value;
        }
        public new static ConstantLong Create(byte[]code, ref int curIndex)
        {
            byte tag = code[curIndex++];

            long value = BitConverter.ToInt64(code, curIndex);
            curIndex += 8;

            return new ConstantLong(tag, value);
        } 
    }
}
