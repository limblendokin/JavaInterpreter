using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab1.AttributesFolder
{
    class AttributeConstantValue : AttributeSuper
    {
        //TODO: realization
        public AttributeConstantValue(ushort attributeNameIndex, uint attributeLength) : base(attributeNameIndex, attributeLength)
        {

        }
        public static AttributeConstantValue Create(byte[] code, ref int curIndex, ConstantPool cp)
        {
            ushort attributeNameIndex = Helper.ToUShort(code, ref curIndex);

            uint attributeLength = Helper.ToUInt(code, ref curIndex);

            return new AttributeConstantValue(attributeNameIndex, attributeLength);
        }
    }
}
