using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab1
{
    class JavaClass
    {
        private byte[] magic;
        private byte[] minor_version;
        private byte[] major_version;
        private byte[] constant_pool_count;

        public short ConstantPoolCount
        {
            get
            {
                return (short)(constant_pool_count[0] * 256 + constant_pool_count[1]);
            }
        }

        private ConstantPool constant_pool;
        public ConstantPool cp
        {
            get
            {
                return constant_pool;
            }
        }

        private byte[] access_flags;
        public short AccessFlags
        {
            get
            {
                return (short)(access_flags[0] * 256 + access_flags[1]);
            }
        }

        private String thisClassName;
        public String ThisClassName
        {
            get
            {
                return thisClassName;
            }
        }
        private byte[] this_class;
        public short ThisClass
        {
            get
            {
                return (short)(this_class[0] * 256 + this_class[1]);
            }
        }

        private byte[] super_class;
        public short SuperClass
        {
            get
            {
                return (short)(super_class[0] * 256 + super_class[1]);
            }
        }
        private byte[] interfaces_count;
        public short InterfacesCount
        {
            get
            {
                return (short)(interfaces_count[0] * 256 + interfaces_count[1]);
            }
        }

        private Interfaces interfaces;

        private byte[] fields_count;
        public short FieldsCount
        {
            get
            {
                return (short)(fields_count[0] * 256 + fields_count[1]);
            }
        }

        private List<Field> fields;
        public List<Field> Fields
        {
            get
            {
                return fields;
            }
        }

        private byte[] methods_count;
        public short MethodsCount
        {
            get
            {
                return (short)(methods_count[0] * 256 + methods_count[1]);
            }
        }

        private List<Method> methods;
        public List<Method> Methods
        {
            get
            {
                return methods;
            }
        }
        public Method GetMethod(int index)
        {
            return methods.ElementAt(index);
        }

        private byte[] attributes_count;
        public short AttributesCount
        {
            get
            {
                return (short)(attributes_count[0] * 256 + attributes_count[1]);
            }
        }

        private Attributes attributes;


        private JavaClass()
        {

        }
        /// <summary>
        /// Перенос данных из *.class файла в память
        /// </summary>
        /// <param name="bytecode"></param>
        public JavaClass(byte[] bytecode)
        {
            int curIndex = 0;
            // ca fe ba be
            magic = readBytes(4, ref curIndex, bytecode);
            
            // Версии 
            minor_version = readBytes(2, ref curIndex, bytecode); ;
            major_version = readBytes(2, ref curIndex, bytecode);
            constant_pool_count = readBytes(2, ref curIndex, bytecode);

            constant_pool = new ConstantPool(bytecode, ConstantPoolCount, ref curIndex);

            access_flags = readBytes(2, ref curIndex, bytecode);
            // Индекс констанnы class текущего класса в пуле констант
            this_class = readBytes(2, ref curIndex, bytecode);
            super_class = readBytes(2, ref curIndex, bytecode);
            interfaces_count = readBytes(2, ref curIndex, bytecode);

            interfaces = new Interfaces(bytecode, InterfacesCount, ref curIndex, ref constant_pool);

            fields_count = readBytes(2, ref curIndex, bytecode);
            fields = new List<Field>();

            for(int i = 0; i < FieldsCount; i++)
            {
                fields.Add(new Field(bytecode, FieldsCount, ref curIndex, ref constant_pool));
            }

            methods_count = readBytes(2, ref curIndex, bytecode);
            methods = new List<Method>();

            for(short i = 0; i < MethodsCount; i++)
            {
                methods.Add(new Method(bytecode, MethodsCount, ref curIndex, ref constant_pool));
            }

            attributes_count = readBytes(2, ref curIndex, bytecode);

            attributes = new Attributes(bytecode, AttributesCount, ref curIndex, ref constant_pool);

            thisClassName = constant_pool.getConstantUtf8(constant_pool.getConstantClass(ThisClass).NameIndex).Value;
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