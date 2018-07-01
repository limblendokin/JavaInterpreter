using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab1.ConstantsFolder
{
    class ConstantFloat : Constant
    {
        private float value;
        public float Value { get => value; }

        public ConstantFloat(byte tag, float value) : base(tag)
        {
            this.value = value;
        }
        public new static ConstantFloat Create(byte[] code, ref int curIndex)
        {
            byte tag = code[curIndex++];

            float value = BitConverter.ToSingle(code, curIndex);
            curIndex += 4;

            return new ConstantFloat(tag, value);
        }
    }
}
