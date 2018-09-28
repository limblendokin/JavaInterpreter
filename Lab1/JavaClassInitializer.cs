using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JavaInterpreter.ConstantsFolder;

namespace JavaInterpreter
{
    class JavaClassInitializer
    {
        private BytecodeReader reader;
        public JavaClassInitializer()
        {
        }
        public static JavaClass Initialize(BytecodeReader reader)
        {
            uint magic = reader.ReadUInt();
            ushort minorVersion = reader.ReadUShort();
            ushort majorVersion = reader.ReadUShort();
            ushort constantPoolCount = reader.ReadUShort();
            ConstantPool constantPool = ConstantPoolInitializer.ReadConstantPool(reader, constantPoolCount);
            ushort accessFlags = reader.ReadUShort();
            ushort thisClass = reader.ReadUShort();
            ushort superClass = reader.ReadUShort();
            ushort interfacesCount = reader.ReadUShort();
            Interfaces interfaces = Interfaces.ReadInterfaces(reader, interfacesCount);
            ushort fieldsCount = reader.ReadUShort();
            //TODO: Field reader
            List<Field> fields = new List<Field>();
            ushort methodsCount = reader.ReadUShort();
            List<Method> methods = new List<Method>();
            ushort attributesCount = reader.ReadUShort();
            Attributes attributes = Attributes.Initialize(reader);
            JavaClass jc = new JavaClass(magic, minorVersion, majorVersion, constantPoolCount, constantPool,
                accessFlags, thisClass, superClass, interfacesCount, interfaces, fieldsCount, fields,
                methodsCount, methods, attributesCount, attributes);
            return jc;
        }
        
        private Interfaces ReadInterfaces(ushort interfacesCount, ConstantPool constantPool)
        {
            return new Interfaces();
        }
        private Field ReadField(ushort fieldsCount, ConstantPool cp)
        {
            ushort accessFlags = reader.ReadUShort();

            ushort nameIndex = reader.ReadUShort();

            ushort descriptorIndex = reader.ReadUShort();

            ushort attributesCount = reader.ReadUShort();

            var attributes = ReadAttributes(attributesCount);

            String thisFieldName = cp.getConstantUtf8(nameIndex).Value;

            return new Field(accessFlags, nameIndex, descriptorIndex, attributesCount, attributes, thisFieldName);
        }
        // methodCount var not needed
        private Method ReadMethod(ushort methodsCount, ConstantPool cp)
        {
            ushort accessFlags = reader.ReadUShort();

            ushort nameIndex = reader.ReadUShort();

            ushort descriptorIndex = reader.ReadUShort();

            ushort attributesCount = reader.ReadUShort();

            var attributes = new Attributes();
            ReadAttributes(attributesCount, attributes);


            String thisMethodName = cp.getConstantUtf8(nameIndex).Value;

            return new Method(accessFlags, nameIndex, descriptorIndex, attributesCount, attributes, thisMethodName);
        }
        
    }
}
