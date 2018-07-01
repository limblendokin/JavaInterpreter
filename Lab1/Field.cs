using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab1
{
    class Field
    {
        private byte[] access_flags;
        public int AccessFlags
        {
            get
            {
                return access_flags[0]*0x100+access_flags[1];
            }
        }
        private byte[] name_index;
        public int NameIndex
        {
            get
            {
                return name_index[0] * 256 + name_index[1];
            }
        }
        private byte[] descriptor_index;
        public int DescriptorIndex
        {
            get
            {
                return descriptor_index[0] * 256 + descriptor_index[1];
            }
        }
        private Object value;
        public Object Value
        {
            get
            {
                return value;
            }
            set
            {
                this.value = value;
            }
        }
        private byte[] attributes_count;
        public short AttributesCount
        {
            get
            {
                short output = attributes_count[0];
                for (int i = 1; i < 2; i++)
                {
                    output *= 256;
                    output += attributes_count[i];
                }
                return output;
            }
        }
        private Attributes attributes;
        private Field()
        {

        }
        /// <summary>
        /// Вызов конструктора считывает данные об определенном поле и его атрибуты
        /// </summary>
        /// <param name="bytecode"></param>
        /// <param name="fields_count"></param>
        /// <param name="curIndex"></param>
        /// <param name="cp"></param>
        public Field(byte[] bytecode, short fields_count, ref int curIndex, ref ConstantPool cp)
        {
            access_flags = readBytes(2, ref curIndex, bytecode);
            name_index = readBytes(2, ref curIndex, bytecode);
            descriptor_index = readBytes(2, ref curIndex, bytecode);
            attributes_count = readBytes(2, ref curIndex, bytecode);
            attributes = new Attributes(bytecode, AttributesCount, ref curIndex, ref cp);
        }
        public byte[] readBytes(uint count, ref int curIndex, byte[] bytecode)
        {
            byte[] output = new byte[count];
            for (int i = 0; i < count; i++)
            {
                output[i] = bytecode[curIndex++];
            }
            return output;
        }
    }
}
