using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JavaInterpreter.AttributesFolder;

namespace JavaInterpreter
{
    class Method
    {
        private ushort accessFlags;
        public ushort AccessFlags { get => accessFlags; }

        private String thisMethodName;
        public String ThisMethodName { get => thisMethodName; }

        private ushort nameIndex;
        public ushort NameIndex { get => nameIndex; }

        private ushort descriptorIndex;
        public ushort DescriptorIndex { get => descriptorIndex; }

        private ushort attributesCount;
        public ushort AttributesCount { get => attributesCount; }

        private Attributes attributes;
        public bool TryGetCodeAttribute(out AttributeCode attributeCode)
        {
            attributeCode = attributes.AttributesTable.First(x => x.GetType().ToString() == "Lab1.AttributesFolder.AttributeCode") as AttributeCode;
            if (attributeCode != null)
                return true;
            else
                return false;
        }
        

        public Method(ushort accessFlags, ushort nameIndex, ushort descriptorIndex, ushort attributesCount, Attributes attributes, String thisMethodName)
        {
            this.accessFlags = accessFlags;
            this.nameIndex = nameIndex;
            this.descriptorIndex = descriptorIndex;
            this.attributesCount = attributesCount;
            this.attributes = attributes;
            this.thisMethodName = thisMethodName;
        }

        public static Method Create(byte[] code, ushort methodsCount, ref int curIndex, ConstantPool cp)
        {
            ushort accessFlags = Helper.ToUShort(code, ref curIndex);

            ushort nameIndex = Helper.ToUShort(code, ref curIndex);

            ushort descriptorIndex = Helper.ToUShort(code, ref curIndex);

            ushort attributesCount = Helper.ToUShort(code, ref curIndex);

            var attributes = Attributes.Create(code, attributesCount, ref curIndex, cp);

            String thisMethodName = cp.getConstantUtf8(nameIndex).Value;

            return new Method(accessFlags, nameIndex, descriptorIndex, attributesCount, attributes, thisMethodName);
        }

    }
}
