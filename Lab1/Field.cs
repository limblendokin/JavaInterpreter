using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JavaInterpreter
{
    class Field
    {
        private uint accessFlags;
        public uint AccessFlags { get => accessFlags; }

        private uint nameIndex;
        public uint NameIndex { get => nameIndex; }

        private uint descriptorIndex;
        public uint DescriptorIndex { get => descriptorIndex; }

        private Object _value;
        public Object Value { get => _value; set => _value = value; }

        private ushort attributesCount;
        public ushort AttributesCount { get => attributesCount; }

        private Attributes attributes;

        private String thisFieldName;
        public String ThisFieldName => thisFieldName;
        /// <summary>
        /// Вызов конструктора считывает данные об определенном поле и его атрибуты
        /// </summary>
        /// <param name="bytecode"></param>
        /// <param name="fields_count"></param>
        /// <param name="curIndex"></param>
        /// <param name="cp"></param>
        public Field(ushort accessFlags, ushort nameIndex, ushort descriptorIndex, ushort attributesCount, Attributes attributes, String thisFieldName)
        {
            this.accessFlags = accessFlags;
            this.nameIndex = nameIndex;
            this.descriptorIndex = descriptorIndex;
            this.attributesCount = attributesCount;
            this.attributes = attributes;
            this.thisFieldName = thisFieldName;
        }
    }
}
