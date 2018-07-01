using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lab1.AttributesFolder;

namespace Lab1
{
    class Method
    {
        private byte[] access_flags;
        public short Access_flags
        {
            get
            {
                return (short)(access_flags[0] * 256 + access_flags[1]);
            }
        }

        private String thisMethodName;
        public String ThisMethodName
        {
            get
            {
                return thisMethodName;
            }
        }
        private byte[] name_index;
        public short Name_index
        {
            get
            {
                return (short)(name_index[0] * 256 + name_index[1]);
            }
        }

        private byte[] descriptor_index;
        public short Descriptor_index
        {
            get
            {
                return (short)(descriptor_index[0] * 256 + descriptor_index[1]);
            }
        }

        private byte[] attributes_count;
        public short Attributes_count
        {
            get
            {
                return (short)(attributes_count[0] * 256 + attributes_count[1]);
            }
        }

        private Attributes attributes;
        public bool TryGetCodeAttribute(out AttributeCode attributeCode)
        {
            attributeCode = null;
            for(int i = 0; i<Attributes_count; i++)
            {
                // TODO: constant pool fetching
                if (attributes.AttributesTable.ElementAt(i).GetType().ToString() == "Lab1.AttributesFolder.AttributeCode")
                {
                    attributeCode = (AttributeCode)attributes.AttributesTable.ElementAt(i);
                    return true;
                }
            }
            return false;
        }

        private Method()
        {

        }


        public Method(byte[] bytecode, short methods_count, ref int curIndex, ref ConstantPool constant_pool)
        {
            access_flags = readBytes(2, ref curIndex, bytecode);
            name_index = readBytes(2, ref curIndex, bytecode);
            descriptor_index = readBytes(2, ref curIndex, bytecode);
            attributes_count = readBytes(2, ref curIndex, bytecode);

            attributes = new Attributes(bytecode, Attributes_count, ref curIndex, ref constant_pool);

            thisMethodName = constant_pool.getConstantUtf8(Name_index).Value;
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
