using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JavaInterpreter.AttributesFolder;

namespace JavaInterpreter
{
    public class Method
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
        public Attributes Attributes => attributes;
        

        public Method(ushort accessFlags, ushort nameIndex, ushort descriptorIndex, ushort attributesCount, Attributes attributes, String thisMethodName)
        {
            this.accessFlags = accessFlags;
            this.nameIndex = nameIndex;
            this.descriptorIndex = descriptorIndex;
            this.attributesCount = attributesCount;
            this.attributes = attributes;
            this.thisMethodName = thisMethodName;
        }

        

    }
}
