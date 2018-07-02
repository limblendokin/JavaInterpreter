using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab1
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
        
        /// <summary>
        /// Вызов конструктора считывает данные об определенном поле и его атрибуты
        /// </summary>
        /// <param name="bytecode"></param>
        /// <param name="fields_count"></param>
        /// <param name="curIndex"></param>
        /// <param name="cp"></param>
        public Field(ushort accessFlags, ushort nameIndex, ushort descriptorIndex, ushort attributesCount, Attributes attributes)
        {
            this.accessFlags = accessFlags;
            this.nameIndex = nameIndex;
            this.descriptorIndex = descriptorIndex;
            this.attributesCount = attributesCount;
            this.attributes = attributes;
        }

        public static Field Create(byte[] code, ushort fieldsCount, ref int curIndex, ConstantPool cp)
        {
            ushort accessFlags = Helper.ToUShort(code, ref curIndex);

            ushort nameIndex = Helper.ToUShort(code, ref curIndex);

            ushort descriptorIndex = Helper.ToUShort(code, ref curIndex);

            ushort attributesCount = Helper.ToUShort(code, ref curIndex);

            var attributes = Attributes.Create(code, attributesCount, ref curIndex, cp);

            return new Field(accessFlags, nameIndex, descriptorIndex, attributesCount, attributes); 
        }
    }
}
