using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab1.AttributesFolder
{
    class AttributeBootstrapMethods : AttributeSuper
    {
        // TODO: realization 
        public AttributeBootstrapMethods(ushort attributeNameIndex, uint attributeLength) : base(attributeNameIndex, attributeLength)
        {

        }
        public static AttributeBootstrapMethods Create(byte[] code, ref int curIndex, ref ConstantPool cp)
        {
            ushort attributeNameIndex = Helper.ToUShort(code, ref curIndex);
            curIndex += 2;

            uint attributeLength = Helper.ToUInt(code, ref curIndex);
            curIndex += 4;

            return new AttributeBootstrapMethods(attributeNameIndex, attributeLength);
        }
    }
}
