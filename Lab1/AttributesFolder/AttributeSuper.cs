using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JavaInterpreter.AttributesFolder
{
    class AttributeSuper
    {
        protected ushort attributeNameIndex;
        public ushort AttributeNameIndex { get => attributeNameIndex; }

        protected uint attributeLength;
        public uint AttributeLength { get => attributeLength; }

        public AttributeSuper(ushort attributeNameIndex, uint attributeLength)
        {
            this.attributeNameIndex = attributeNameIndex;
            this.attributeLength = attributeLength;
        }
        public static AttributeSuper Create(byte[] code, ref int curIndex)
        {
            ushort attributeNameIndex = Helper.ToUShort(code, ref curIndex);

            uint attributeLength = Helper.ToUInt(code, ref curIndex);

            return new AttributeSuper(attributeNameIndex, attributeLength);
        }
    }
}
