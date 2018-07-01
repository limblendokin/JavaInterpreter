using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab1.ConstantsFolder
{
    class ConstantUtf8 : Constant
    {
        private ushort length;
        public ushort Length { get => length; }

        private String value;
        public String Value { get => value; }
        
        public ConstantUtf8(byte tag, ushort length, String value) : base(tag)
        {
            this.length = length;
            this.value = value;
        }
        public new static ConstantUtf8 Create(byte[] code, ref int curIndex)
        {
            byte tag = code[curIndex++];

            ushort length = Helper.ToUShort(code, ref curIndex);

            // Encoding.UTF8.GetString(bytes)
            String value = Encoding.UTF8.GetString(code.Skip(curIndex).Take(length).ToArray());
            curIndex += length;

            return new ConstantUtf8(tag, length, value);
        }
    }
}
