using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab1
{
    class JavaClass
    {
        private uint magic;
        private ushort minorVersion;
        private ushort majorVersion;
        private ushort constantPoolCount;

        public ushort ConstantPoolCount => constantPoolCount;

        private ConstantPool constantPool;
        public ConstantPool ConstantPool => constantPool;

        private ushort accessFlags;
        public ushort AccessFlags => accessFlags;

        private String thisClassName;
        public String ThisClassName => thisClassName;

        private ushort thisClass;
        public ushort ThisClass => thisClass;

        private ushort superClass;
        public ushort SuperClass => superClass;

        private ushort interfacesCount;
        public ushort InterfacesCount => interfacesCount;

        private Interfaces interfaces;

        private ushort fieldsCount;
        public ushort FieldsCount => fieldsCount;

        private List<Field> fields;
        public List<Field> Fields => fields;

        private ushort methodsCount;
        public ushort MethodsCount => methodsCount;

        private List<Method> methods;
        public List<Method> Methods => methods;

        public Method GetMethod(int index)
        {
            return methods.ElementAt(index);
        }

        private ushort attributesCount;
        public ushort AttributesCount => attributesCount;

        private Attributes attributes;
        
        /// <summary>
        /// Перенос данных из *.class файла в память
        /// </summary>
        /// <param name="bytecode"></param>
        public JavaClass(uint magic, ushort minorVersion, ushort majorVersion, ushort constantPoolCount,
                         ConstantPool cp, ushort accessFlags, ushort thisClass,ushort superClass,
                         ushort interfacesCount, Interfaces interfaces, ushort fieldsCount, List<Field> fields,
                         ushort methodsCount, List<Method> methods, ushort attributesCount, Attributes attributes,
                         String thisClassName)
        {
            this.magic = magic;
            this.minorVersion = minorVersion;
            this.majorVersion = majorVersion;
            this.constantPoolCount = constantPoolCount;
            this.constantPool = cp;
            this.accessFlags = accessFlags;
            this.thisClass = thisClass;
            this.superClass = superClass;
            this.interfacesCount = interfacesCount;
            this.interfaces = interfaces;
            this.fieldsCount = fieldsCount;
            this.fields = fields;
            this.methodsCount = methodsCount;
            this.methods = methods;
            this.attributesCount = attributesCount;
            this.attributes = attributes;
            this.thisClassName = thisClassName;
        }
        public static JavaClass Create(byte[] code)
        {
            int curIndex = 0;
            // ca fe ba be
            uint magic = Helper.ToUInt(code, ref curIndex);

            // Версии 
            ushort minorVersion = Helper.ToUShort(code, ref curIndex);
            ushort majorVersion = Helper.ToUShort(code, ref curIndex);
            ushort constantPoolCount = Helper.ToUShort(code, ref curIndex);

            var constantPool = ConstantPool.Create(code, constantPoolCount, ref curIndex);

            ushort accessFlags = Helper.ToUShort(code, ref curIndex);
            // Индекс констанnы class текущего класса в пуле констант
            ushort thisClass = Helper.ToUShort(code, ref curIndex);
            ushort superClass = Helper.ToUShort(code, ref curIndex);
            ushort interfacesCount = Helper.ToUShort(code, ref curIndex);

            var interfaces = Interfaces.Create(code, interfacesCount, ref curIndex, constantPool);

            ushort fieldsCount = Helper.ToUShort(code, ref curIndex);
            var fields = new List<Field>();

            for (int i = 0; i < fieldsCount; i++)
            {
                fields.Add(Field.Create(code, fieldsCount, ref curIndex, constantPool));
            }

            ushort methodsCount = Helper.ToUShort(code, ref curIndex);
            var methods = new List<Method>();

            for (short i = 0; i < methodsCount; i++)
            {
                methods.Add(Method.Create(code, methodsCount, ref curIndex, constantPool));
            }

            ushort attributesCount = Helper.ToUShort(code, ref curIndex);

            var attributes = Attributes.Create(code, attributesCount, ref curIndex, constantPool);

            String thisClassName = constantPool.getConstantUtf8(constantPool.getConstantClass(thisClass).NameIndex).Value;

            return new JavaClass(magic, minorVersion, majorVersion, constantPoolCount,
                                 constantPool, accessFlags, thisClass, superClass,
                                 interfacesCount, interfaces, fieldsCount, fields,
                                 methodsCount, methods, attributesCount, attributes,
                                 thisClassName);
        }
    }
}