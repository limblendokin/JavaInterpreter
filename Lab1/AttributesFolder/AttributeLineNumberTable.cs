using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab1.AttributesFolder
{
    class AttributeLineNumberTable : AttributeSuper
    {
        private uint lineNumberTableLength;
        public uint LineNumberTableLength { get => lineNumberTableLength; }

        private List<byte[]> startPC;
        private List<byte[]> lineNumber;

        public AttributeLineNumberTable(ushort attributeNameIndex, uint attributeLength, uint lineNumberTableLength, List<byte[]> startPC, List<byte[]> lineNumber) : base(attributeNameIndex, attributeLength)
        {
            this.lineNumberTableLength = lineNumberTableLength;
            this.startPC = startPC;
            this.lineNumber = lineNumber;
        }
        public static AttributeLineNumberTable Create(byte[] code, ref int curIndex, ref ConstantPool cp)
        {
            ushort attributeNameIndex = Helper.ToUShort(code, ref curIndex);
            curIndex += 2;
            ushort attributeLength = Helper.ToUShort(code, ref curIndex);
            curIndex += 2;
            uint lineNumberTableLength = Helper.ToUInt(code, ref curIndex);
            curIndex += 4;
            List<byte[]> startPC = new List<byte[]>();
            byte[] tmp = new byte[2];
            
            List<byte[]> lineNumber = new List<byte[]>();
            for(int i = 0; i < lineNumberTableLength; i++)
            {
                Array.Copy(code, curIndex, tmp, 0, 2);
                curIndex += 2;
                startPC.Add(tmp);
                Array.Copy(code, curIndex, tmp, 0, 2);
                curIndex += 2;
                lineNumber.Add(tmp);
            }
            return new AttributeLineNumberTable(attributeNameIndex, attributeLength, lineNumberTableLength, startPC, lineNumber);
        }
    }
}
