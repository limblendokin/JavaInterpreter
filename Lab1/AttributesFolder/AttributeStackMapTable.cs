using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JavaInterpreter.AttributesFolder
{
    class AttributeStackMapTable : AttributeSuper
    {
        private ushort numberOfEntries;
        public ushort NumberOfEntries { get => numberOfEntries; }
        
        public AttributeStackMapTable(ushort attributeNameIndex, uint attributeLength, ushort numberOfEntries) : base(attributeNameIndex, attributeLength)
        {

        }
        public static AttributeStackMapTable Create(byte[] code, ref int curIndex, ConstantPool cp)
        {
            ushort attributeNameIndex = Helper.ToUShort(code, ref curIndex);

            uint attributeLength = Helper.ToUInt(code, ref curIndex);

            ushort numberOfEntries = Helper.ToUShort(code, ref curIndex);

            return new AttributeStackMapTable(attributeNameIndex, attributeLength, numberOfEntries);
        }
    }
}
