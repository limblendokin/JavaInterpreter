using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab1.AttributesFolder
{
    class AttributeSourceFile : AttributeSuper
    {
        private ushort sourceFileIndex;
        public AttributeSourceFile(ushort attributeNameIndex, uint attributeLength, ushort sourceFileIndex) : base(attributeNameIndex, attributeLength)
        {
            this.sourceFileIndex = sourceFileIndex;
        }
        public static AttributeSourceFile Create(byte[] code, ref int curIndex, ref ConstantPool cp)
        {
            ushort attributeNameIndex = Helper.ToUShort(code, ref curIndex);
            curIndex += 2;
            uint attributeLength = Helper.ToUInt(code, ref curIndex);
            curIndex += 4;
            ushort sourceFileIndex = Helper.ToUShort(code, ref curIndex);
            curIndex += 2;
            return new AttributeSourceFile(attributeNameIndex, attributeLength, sourceFileIndex);
        }
    }
}
