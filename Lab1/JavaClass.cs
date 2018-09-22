using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JavaInterpreter.AttributesFolder;
using JavaInterpreter.ConstantsFolder;

namespace JavaInterpreter
{
    class JavaClass
    {
        private byte[] code;
        private int index;
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
            index = 0;
            // ca fe ba be
            magic = ReadUInt();

            // Версии 
            minorVersion = ReadUShort();
            majorVersion = ReadUShort();
            constantPoolCount = ReadUShort();

            constantPool = new ConstantPool();
            ReadConstantPool(constantPoolCount);

            accessFlags = ReadUShort();
            // Индекс констанnы class текущего класса в пуле констант
            thisClass = ReadUShort();
            superClass = ReadUShort();
            interfacesCount = ReadUShort();

            interfaces = ReadInterfaces(interfacesCount, constantPool);

            fieldsCount = ReadUShort();
            fields = new List<Field>();

            for (int i = 0; i < fieldsCount; i++)
            {
                fields.Add(ReadField(fieldsCount, constantPool));
            }

            methodsCount = ReadUShort();
            methods = new List<Method>();

            for (short i = 0; i < methodsCount; i++)
            {
                methods.Add(ReadMethod(methodsCount, constantPool));
            }

            attributesCount = ReadUShort();

            attributes = ReadAttributes(attributesCount);

            String thisClassName = constantPool.getConstantUtf8(constantPool.getConstantClass(thisClass).NameIndex).Value;

            
        }
        private uint ReadUInt()
        {
            uint output;
            if (BitConverter.IsLittleEndian)
                output = BitConverter.ToUInt32(code.Skip(index).Take(4).Reverse().ToArray(), 0);
            else
                output = BitConverter.ToUInt32(code, index);
            index += 4;
            return output;
        }
        private ushort ReadUShort()
        {
            ushort output;
            if (BitConverter.IsLittleEndian)
                output = BitConverter.ToUInt16(code.Skip(index).Take(2).Reverse().ToArray(), 0);
            else
                output = BitConverter.ToUInt16(code, index);
            index += 2;
            return output;
        }
        private long ReadLong()
        {
            long output;
            if (BitConverter.IsLittleEndian)
                output = BitConverter.ToInt64(code.Skip(index).Take(8).Reverse().ToArray(), 0);
            else
                output = BitConverter.ToInt64(code, index);
            index += 8;
            return output;
        }
        private double ReadDouble()
        {
            double output;
            if (BitConverter.IsLittleEndian)
                output = BitConverter.ToDouble(code.Skip(index).Take(8).Reverse().ToArray(), 0);
            else
                output = BitConverter.ToDouble(code, index);
            index += 8;
            return output;
        }
        private int ReadInt()
        {
            int output;
            if (BitConverter.IsLittleEndian)
                output = BitConverter.ToInt32(code.Skip(index).Take(4).Reverse().ToArray(), 0);
            else
                output = BitConverter.ToInt32(code, index);
            index += 4;
            return output;
        }
        private float ReadFloat()
        {
            float output;
            if (BitConverter.IsLittleEndian)
                output = BitConverter.ToSingle(code.Skip(index).Take(4).Reverse().ToArray(), 0);
            else
                output = BitConverter.ToSingle(code, index);
            index += 4;
            return output;
        }
        private byte ReadByte()
        {
            return code[index++];
        }
        private ConstantUtf8 ReadConstantUtf8()
        {
            ushort length = ReadUShort();
            // Encoding.UTF8.GetString(bytes)
            String value = Encoding.UTF8.GetString(code.Skip(index).Take(length).ToArray());
            index += length;
            return new ConstantUtf8(length, value);
        }
        private void ReadConstantPool(ushort constantPoolCount)
        {
            var dict = new Dictionary<int, Action>();
            dict.Add( 1, () => constantPool.AddConstantUtf8(ReadConstantUtf8()));
            dict.Add( 3, () => constantPool.AddConstantInteger(new ConstantInteger(ReadInt())));
            dict.Add( 4, () => constantPool.AddConstantFloat(new ConstantFloat(ReadFloat())));
            dict.Add( 5, () => constantPool.AddConstantLong(new ConstantLong(ReadLong())));
            dict.Add( 6, () => constantPool.AddConstantDouble(new ConstantDouble(ReadDouble())));
            dict.Add( 7, () => constantPool.AddConstantClass(new ConstantClass(ReadUShort())));
            dict.Add( 8, () => constantPool.AddConstantString(new ConstantString(ReadUShort())));
            dict.Add( 9, () => constantPool.AddConstantFieldRef(new ConstantFieldRef(ReadUShort(), ReadUShort())));
            dict.Add(10, () => constantPool.AddConstantMethodRef(new ConstantMethodRef(ReadUShort(), ReadUShort())));
            dict.Add(11, () => constantPool.AddConstantInterfaceMethodRef(new ConstantInterfaceMethodRef(ReadUShort(), ReadUShort())));
            dict.Add(12, () => constantPool.AddConstantNameAndType(new ConstantNameAndType(ReadUShort(), ReadUShort())));
            dict.Add(15, () => constantPool.AddConstantMethodHandle(new ConstantMethodHandle(ReadByte(), ReadUShort())));
            dict.Add(16, () => constantPool.AddConstantMethodType(new ConstantMethodType(ReadUShort())));
            dict.Add(18, () => constantPool.AddConstantInvokeDynamic(new ConstantInvokeDynamic(ReadUShort(), ReadUShort())));
            dict.Add(19, () => constantPool.AddConstantModule(new ConstantModule(ReadUShort())));
            dict.Add(20, () => constantPool.AddConstantPackage(new ConstantPackage(ReadUShort())));
            Action action;
            for(int i = 0; i < ConstantPoolCount; i++)
            {
                if (dict.TryGetValue(ReadByte(), out action))
                    action.Invoke();
                else
                    throw new KeyNotFoundException("Constant type not recognized");
            }
        }
        private Interfaces ReadInterfaces(ushort interfacesCount, ConstantPool constantPool)
        {
            return new Interfaces();
        }
        private Field ReadField(ushort fieldsCount, ConstantPool cp)
        {
            ushort accessFlags = ReadUShort();

            ushort nameIndex = ReadUShort();

            ushort descriptorIndex = ReadUShort();

            ushort attributesCount = ReadUShort();

            var attributes = ReadAttributes(attributesCount);

            String thisFieldName = cp.getConstantUtf8(nameIndex).Value;

            return new Field(accessFlags, nameIndex, descriptorIndex, attributesCount, attributes, thisFieldName);
        }
        // methodCount var not needed
        private Method ReadMethod(ushort methodsCount, ConstantPool cp)
        {
            ushort accessFlags = ReadUShort();

            ushort nameIndex = ReadUShort();

            ushort descriptorIndex = ReadUShort();

            ushort attributesCount = ReadUShort();

            var attributes = ReadAttributes(attributesCount);

            String thisMethodName = cp.getConstantUtf8(nameIndex).Value;

            return new Method(accessFlags, nameIndex, descriptorIndex, attributesCount, attributes, thisMethodName);
        }
        private Attributes ReadAttributes(ushort attributesCount)
        {
            /*List<AttributeSuper> attributesTable = new List<AttributeSuper>();
            var dict = new Dictionary<String, Action>();
            dict.Add("Code", () => attributesTable.Add(AttributeCode.Create(code, ref cp)));
            for(int i = 0; i < attributesCount; i++)
            {

            }*/
            short attributeIndex = 0;
            var attributesTable = new List<AttributeSuper>();
            int curAttributeNameIndex;
            while (attributesCount > attributeIndex)
            {
                curAttributeNameIndex = code[index] * 0x100 + code[index + 1];
                switch (cp.getConstantUtf8(curAttributeNameIndex).Value)
                {
                    case "Code":
                        attributesTable.Add(AttributeCode.Create(code, cp));
                        attributeIndex++;
                        break;

                    case "ConstantValue":
                        // TODO: not skipping bytes
                        Console.WriteLine("ConstantValue attribute was created");
                        attributesTable.Add(AttributeSuper.Create(code));
                        index += (int)attributesTable.Last().AttributeLength;
                        attributeIndex++;
                        break;

                    case "StackMapTable":
                        // TODO: not skipping bytes
                        Console.WriteLine("StackMapTable attribute was created");
                        attributesTable.Add(AttributeSuper.Create(code));
                        index += (int)attributesTable.Last().AttributeLength;
                        attributeIndex++;
                        break;

                    case "BootstrapMethods":
                        // TODO: not skipping bytes
                        Console.WriteLine("BootstrapMethods attribute was created");
                        attributesTable.Add(AttributeSuper.Create(code));
                        index += (int)attributesTable.Last().AttributeLength;
                        attributeIndex++;
                        break;

                    case "LineNumberTable":
                        attributesTable.Add(AttributeLineNumberTable.Create(code, cp));
                        attributeIndex++;
                        break;

                    case "SourceFile":
                        attributesTable.Add(AttributeSourceFile.Create(code, cp));
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