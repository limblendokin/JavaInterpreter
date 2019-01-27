using System;
using System.Collections.Generic;
using System.Linq;

namespace JavaInterpreter
{
    public class JavaClass
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
        public Attributes Attributes => attributes;

        /// <summary>
        /// Перенос данных из *.class файла в память
        /// </summary>
        /// <param name="bytecode"></param>
        public JavaClass (uint magic, ushort minorVersion, ushort majorVersion, ushort constantPoolCount,
            ConstantPool constantPool, ushort accessFlags, ushort thisClass, ushort superClass, ushort interfacesCount,
            Interfaces interfaces, ushort fieldsCount, List<Field> fields, ushort methodsCount,
            List<Method> methods, ushort attributesCount, Attributes attributes)
        {
            this.magic = magic;
            this.minorVersion = minorVersion;
            this.majorVersion = majorVersion;
            this.constantPoolCount = constantPoolCount;
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
            
            thisClassName = constantPool.GetConstantUtf8(constantPool.GetConstantClass(thisClass).NameIndex).Value;
        }
        
    }
}