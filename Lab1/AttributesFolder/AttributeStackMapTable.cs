using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab1.AttributesFolder
{
    class AttributeStackMapTable : AttributeSuper
    {
        private ushort numberOfEntries;
        public ushort NumberOfEntries { get => numberOfEntries; }
        
        public AttributeStackMapTable(ushort attributeNameIndex, uint attributeLength, ushort numberOfEntries) : base(attributeNameIndex, attributeLength)
        {

        }
        public static AttributeStackMapTable Create(byte[] code, ref int curIndex, ref ConstantPool cp)
        {
            ushort attributeNameIndex = Helper.ToUShort(code, ref curIndex);
            curIndex += 2;
            uint attributeLength = Helper.ToUInt(code, ref curIndex);
            curIndex += 4;
            ushort numberOfEntries = Helper.ToUShort(code, ref curIndex);
            curIndex += 2;
            return new AttributeStackMapTable(attributeNameIndex, attributeLength, numberOfEntries);
        }
    }
}
