using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JavaInterpreter.AttributesFolder
{
    class AttributeSourceFile : AttributeSuper
    {
        private ushort sourceFileIndex;
        public AttributeSourceFile(ushort attributeNameIndex, uint attributeLength, ushort sourceFileIndex) : base(attributeNameIndex, attributeLength)
        {
            this.sourceFileIndex = sourceFileIndex;
        }
        public static AttributeSourceFile Create(byte[] code, ref int curIndex, ConstantPool cp)
        {
            ushort attributeNameIndex = Helper.ToUShort(code, ref curIndex);

            uint attributeLength = Helper.ToUInt(code, ref curIndex);

            ushort sourceFileIndex = Helper.ToUShort(code, ref curIndex);

            return new AttributeSourceFile(attributeNameIndex, attributeLength, sourceFileIndex);
        }
    }
}
