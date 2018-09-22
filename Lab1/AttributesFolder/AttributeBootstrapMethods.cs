using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JavaInterpreter.AttributesFolder
{
    class AttributeBootstrapMethods : AttributeSuper
    {
        // TODO: realization 
        public AttributeBootstrapMethods(ushort attributeNameIndex, uint attributeLength) : base(attributeNameIndex, attributeLength)
        {

        }
        public static AttributeBootstrapMethods Create(byte[] code, ref int curIndex, ConstantPool cp)
        {
            ushort attributeNameIndex = Helper.ToUShort(code, ref curIndex);

            uint attributeLength = Helper.ToUInt(code, ref curIndex);

            return new AttributeBootstrapMethods(attributeNameIndex, attributeLength);
        }
    }
}
