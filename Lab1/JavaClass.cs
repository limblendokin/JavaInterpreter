using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lab1.AttributesFolder;
using Lab1.ConstantsFolder;

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
        public JavaClass (byte[] bytecode)
        {
            int curIndex = 0;
            // ca fe ba be
            uint magic = Helper.ToUInt(bytecode, ref curIndex);

            // Версии 
            ushort minorVersion = Helper.ToUShort(bytecode, ref curIndex);
            ushort majorVersion = Helper.ToUShort(bytecode, ref curIndex);
            ushort constantPoolCount = Helper.ToUShort(bytecode, ref curIndex);

            var constantPool = ReadConstantPool(bytecode, constantPoolCount, ref curIndex);

            ushort accessFlags = Helper.ToUShort(bytecode, ref curIndex);
            // Индекс констанnы class текущего класса в пуле констант
            ushort thisClass = Helper.ToUShort(bytecode, ref curIndex);
            ushort superClass = Helper.ToUShort(bytecode, ref curIndex);
            ushort interfacesCount = Helper.ToUShort(bytecode, ref curIndex);

            var interfaces = ReadInterfaces(bytecode, interfacesCount, ref curIndex, constantPool);

            ushort fieldsCount = Helper.ToUShort(bytecode, ref curIndex);
            var fields = new List<Field>();

            for (int i = 0; i < fieldsCount; i++)
            {
                fields.Add(ReadField(bytecode, fieldsCount, ref curIndex, constantPool));
            }

            ushort methodsCount = Helper.ToUShort(bytecode, ref curIndex);
            var methods = new List<Method>();

            for (short i = 0; i < methodsCount; i++)
            {
                methods.Add(ReadMethod(bytecode, methodsCount, ref curIndex, constantPool));
            }

            ushort attributesCount = Helper.ToUShort(bytecode, ref curIndex);

            var attributes = ReadAttributes(bytecode, attributesCount, ref curIndex, constantPool);

            String thisClassName = constantPool.getConstantUtf8(constantPool.getConstantClass(thisClass).NameIndex).Value;

            
        }
        private static ConstantPool ReadConstantPool(byte[] code, ushort constantPoolCount, ref int curIndex)
        {
            int constIndex = 0;
            var cp = new List<Constant>();
            cp.Add(null);
            var constantClasses = new List<ConstantClass>();
            while (constIndex < constantPoolCount - 1)
            {
                switch (code[curIndex])
                {
                    // CONSTANT_Class
                    case 0x07:
                        cp.Add(ConstantClass.Create(code, ref curIndex));
                        constantClasses.Add((ConstantClass)cp.Last());
                        constIndex++;
                        break;

                    // CONSTANT_Fieldref
                    case 0x09:
                        cp.Add(ConstantFieldRef.Create(code, ref curIndex));
                        constIndex++;
                        break;

                    //CONSTANT_Methodref
                    case 0x0a:
                        cp.Add(ConstantMethodRef.Create(code, ref curIndex));
                        constIndex++;
                        break;

                    //CONSTANT_InterfaceMethodref
                    case 0x0b:
                        cp.Add(ConstantInterfaceMethodRef.Create(code, ref curIndex));
                        constIndex++;
                        break;

                    //CONSTANT_String
                    case 0x08:
                        cp.Add(ConstantString.Create(code, ref curIndex));
                        constIndex++;
                        break;

                    //CONSTANT_Integer
                    case 0x03:
                        cp.Add(ConstantInteger.Create(code, ref curIndex));
                        constIndex++;
                        break;

                    //CONSTANT_Float
                    case 0x04:
                        cp.Add(ConstantFloat.Create(code, ref curIndex));
                        constIndex++;
                        break;

                    //CONSTANT_Long
                    case 0x05:
                        cp.Add(ConstantLong.Create(code, ref curIndex));
                        constIndex++;
                        break;

                    //CONSTANT_Double
                    case 0x06:
                        cp.Add(ConstantDouble.Create(code, ref curIndex));
                        constIndex++;
                        break;

                    //CONSTANT_NameAndType
                    case 0x0c:
                        cp.Add(ConstantNameAndType.Create(code, ref curIndex));
                        constIndex++;
                        break;

                    //CONSTANT_Utf8
                    case 0x01:
                        cp.Add(ConstantUtf8.Create(code, ref curIndex));
                        constIndex++;
                        break;

                    //CONSTANT_MethodHandle
                    case 0x0f:
                        cp.Add(ConstantMethodHandle.Create(code, ref curIndex));
                        constIndex++;
                        break;

                    //CONSTANT_MethodType
                    case 0x10:
                        cp.Add(ConstantMethodType.Create(code, ref curIndex));
                        constIndex++;
                        break;

                    //CONSTANT_InvokeDynamic
                    case 0x12:
                        cp.Add(ConstantInvokeDynamic.Create(code, ref curIndex));
                        constIndex++;
                        break;

                    //CONSTANT_Module
                    case 0x13:
                        cp.Add(ConstantModule.Create(code, ref curIndex));
                        constIndex++;
                        break;

                    //CONSTANT_Package
                    case 0x14:
                        cp.Add(ConstantPackage.Create(code, ref curIndex));
                        constIndex++;
                        break;

                    default:
                        Console.WriteLine("No const type");
                        break;
                }
            }
            return new ConstantPool(cp, constantClasses);
        }
        private static Interfaces ReadInterfaces(byte[] code, ushort interfacesCount, ref int curIndex, ConstantPool constantPool)
        {
            return new Interfaces();
        }
        private static Field ReadField(byte[] code, ushort fieldsCount, ref int curIndex, ConstantPool cp)
        {
            ushort accessFlags = Helper.ToUShort(code, ref curIndex);

            ushort nameIndex = Helper.ToUShort(code, ref curIndex);

            ushort descriptorIndex = Helper.ToUShort(code, ref curIndex);

            ushort attributesCount = Helper.ToUShort(code, ref curIndex);

            var attributes = ReadAttributes(code, attributesCount, ref curIndex, cp);

            String thisFieldName = cp.getConstantUtf8(nameIndex).Value;

            return new Field(accessFlags, nameIndex, descriptorIndex, attributesCount, attributes, thisFieldName);
        }
        // methodCount var not needed
        private static Method ReadMethod(byte[] code, ushort methodsCount, ref int curIndex, ConstantPool cp)
        {
            ushort accessFlags = Helper.ToUShort(code, ref curIndex);

            ushort nameIndex = Helper.ToUShort(code, ref curIndex);

            ushort descriptorIndex = Helper.ToUShort(code, ref curIndex);

            ushort attributesCount = Helper.ToUShort(code, ref curIndex);

            var attributes = ReadAttributes(code, attributesCount, ref curIndex, cp);

            String thisMethodName = cp.getConstantUtf8(nameIndex).Value;

            return new Method(accessFlags, nameIndex, descriptorIndex, attributesCount, attributes, thisMethodName);
        }
        private static Attributes ReadAttributes(byte[] code, ushort attributesCount, ref int curIndex, ConstantPool cp)
        {
            /*List<AttributeSuper> attributesTable = new List<AttributeSuper>();
            var dict = new Dictionary<String, Action>();
            dict.Add("Code", () => attributesTable.Add(AttributeCode.Create(code, ref curIndex, ref cp)));
            for(int i = 0; i < attributesCount; i++)
            {

            }*/
            short attributeIndex = 0;
            var attributesTable = new List<AttributeSuper>();
            int curAttributeNameIndex;
            while (attributesCount > attributeIndex)
            {
                curAttributeNameIndex = code[curIndex] * 0x100 + code[curIndex + 1];
                switch (cp.getConstantUtf8(curAttributeNameIndex).Value)
                {
                    case "Code":
                        attributesTable.Add(AttributeCode.Create(code, ref curIndex, cp));
                        attributeIndex++;
                        break;

                    case "ConstantValue":
                        // TODO: not skipping bytes
                        Console.WriteLine("ConstantValue attribute was created");
                        attributesTable.Add(AttributeSuper.Create(code, ref curIndex));
                        curIndex += (int)attributesTable.Last().AttributeLength;
                        attributeIndex++;
                        break;

                    case "StackMapTable":
                        // TODO: not skipping bytes
                        Console.WriteLine("StackMapTable attribute was created");
                        attributesTable.Add(AttributeSuper.Create(code, ref curIndex));
                        curIndex += (int)attributesTable.Last().AttributeLength;
                        attributeIndex++;
                        break;

                    case "BootstrapMethods":
                        // TODO: not skipping bytes
                        Console.WriteLine("BootstrapMethods attribute was created");
                        attributesTable.Add(AttributeSuper.Create(code, ref curIndex));
                        curIndex += (int)attributesTable.Last().AttributeLength;
                        attributeIndex++;
                        break;

                    case "LineNumberTable":
                        attributesTable.Add(AttributeLineNumberTable.Create(code, ref curIndex, cp));
                        attributeIndex++;
                        break;

                    case "SourceFile":
                        attributesTable.Add(AttributeSourceFile.Create(code, ref curIndex, cp));
                        attributeIndex++;
                        break;

                    default:
                        attributeIndex++;
                        Console.WriteLine("no attribute found");
                        break;
                }
            }
            return new Attributes(attributesTable);
        }
    }
}