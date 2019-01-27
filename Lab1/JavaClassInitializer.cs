using System;
using System.Collections.Generic;
using System.Linq;
using JavaInterpreter.ConstantsFolder;
using JavaInterpreter.AttributesFolder;

namespace JavaInterpreter
{
    public class JavaClassInitializer
    {
        public static JavaClass ReadJavaClass(byte[] bytecode)
        {
            BytecodeReader reader = new BytecodeReader(bytecode);

            // ca fe ba be
            uint magic = reader.ReadUInt();

            // Версии 
            ushort minorVersion = reader.ReadUShort();
            ushort majorVersion = reader.ReadUShort();
            ushort constantPoolCount = reader.ReadUShort();

            ConstantPool constantPool = ReadConstantPool(reader, constantPoolCount);

            ushort accessFlags = reader.ReadUShort();
            // Индекс констанnы class текущего класса в пуле констант
            ushort thisClass = reader.ReadUShort();
            ushort superClass = reader.ReadUShort();
            ushort interfacesCount = reader.ReadUShort();

            Interfaces interfaces = ReadInterfaces(reader, interfacesCount, constantPool);

            ushort fieldsCount = reader.ReadUShort();
            List<Field> fields = new List<Field>();

            for (int i = 0; i < fieldsCount; i++)
            {
                fields.Add(ReadField(reader, fieldsCount, constantPool));
            }

            ushort methodsCount = reader.ReadUShort();
            List<Method> methods = new List<Method>();

            for (short i = 0; i < methodsCount; i++)
            {
                methods.Add(ReadMethod(reader, methodsCount, constantPool));
            }

            ushort attributesCount = reader.ReadUShort();

            Attributes attributes = ReadAttributes(reader, attributesCount, constantPool);

            return new JavaClass(magic, minorVersion, majorVersion, constantPoolCount, constantPool, accessFlags,
                thisClass, superClass, interfacesCount, interfaces, fieldsCount, fields, methodsCount,
                methods, attributesCount, attributes);
        }

        private static ConstantPool ReadConstantPool(BytecodeReader reader, ushort constantPoolCount)
        {
            // TODO: check if long and double constants reading right
            ConstantPool constantPool = new ConstantPool();
            var tagDictionary = new Dictionary<int, Action>
            {
                {
                    1,
                    () =>
                {
                    ushort length = reader.ReadUShort();
                    String value = reader.ReadString(length);
                    constantPool.AddConstantUtf8(new ConstantUtf8(length, value));
                }
                },
                { 3, () => constantPool.AddConstantInteger(reader.ReadInt()) },
                { 4, () => constantPool.AddConstantFloat(reader.ReadFloat()) },
                { 5, () => constantPool.AddConstantLong(reader.ReadLong()) },
                { 6, () => constantPool.AddConstantDouble(reader.ReadDouble()) },
                { 7, () => constantPool.AddConstantClass(new ConstantClass(reader.ReadUShort())) },
                { 8, () => constantPool.AddConstantString(new ConstantString(reader.ReadUShort())) },
                { 9, () => constantPool.AddConstantFieldRef(new ConstantFieldRef(reader.ReadUShort(), reader.ReadUShort())) },
                { 10, () => constantPool.AddConstantMethodRef(new ConstantMethodRef(reader.ReadUShort(), reader.ReadUShort())) },
                { 11, () => constantPool.AddConstantInterfaceMethodRef(new ConstantInterfaceMethodRef(reader.ReadUShort(), reader.ReadUShort())) },
                { 12, () => constantPool.AddConstantNameAndType(new ConstantNameAndType(reader.ReadUShort(), reader.ReadUShort())) },
                { 15, () => constantPool.AddConstantMethodHandle(new ConstantMethodHandle(reader.ReadByte(), reader.ReadUShort())) },
                { 16, () => constantPool.AddConstantMethodType(new ConstantMethodType(reader.ReadUShort())) },
                { 18, () => constantPool.AddConstantInvokeDynamic(new ConstantInvokeDynamic(reader.ReadUShort(), reader.ReadUShort())) },
                { 19, () => constantPool.AddConstantModule(new ConstantModule(reader.ReadUShort())) },
                { 20, () => constantPool.AddConstantPackage(new ConstantPackage(reader.ReadUShort())) }
            };
            for (int i = 0; i < constantPoolCount; i++)
            {
                if (tagDictionary.TryGetValue(reader.ReadByte(), out Action createConstant))
                    createConstant.Invoke();
                else
                    throw new KeyNotFoundException("Constant type not recognized");
            }
            return constantPool;
        }
        private static Interfaces ReadInterfaces(BytecodeReader reader, ushort interfacesCount, ConstantPool constantPool)
        {
            return new Interfaces();
        }
        private static Field ReadField(BytecodeReader reader, ushort fieldsCount, ConstantPool cp)
        {
            ushort accessFlags = reader.ReadUShort();

            ushort nameIndex = reader.ReadUShort();

            ushort descriptorIndex = reader.ReadUShort();

            ushort attributesCount = reader.ReadUShort();

            var attributes = ReadAttributes(reader, attributesCount, cp);

            String thisFieldName = cp.GetConstantUtf8(nameIndex).Value;

            return new Field(accessFlags, nameIndex, descriptorIndex, attributesCount, attributes, thisFieldName);
        }
        // methodCount var not needed
        private static Method ReadMethod(BytecodeReader reader, ushort methodsCount, ConstantPool cp)
        {
            ushort accessFlags = reader.ReadUShort();

            ushort nameIndex = reader.ReadUShort();

            ushort descriptorIndex = reader.ReadUShort();

            ushort attributesCount = reader.ReadUShort();

            var attributes = ReadAttributes(reader, attributesCount, cp);

            String thisMethodName = cp.GetConstantUtf8(nameIndex).Value;

            return new Method(accessFlags, nameIndex, descriptorIndex, attributesCount, attributes, thisMethodName);
        }
        private static Attributes ReadAttributes(BytecodeReader reader, ushort attributesCount, ConstantPool cp)
        {
            var attributesTable = new List<object>();
            Dictionary<string, Action> nameActionDictionary = new Dictionary<string, Action>
            {
                { "Code", () => attributesTable.Add(ReadAttributeCode(reader, cp)) },
                { "ConstantValue", () => attributesTable.Add(ReadAttributeConstantValue(reader)) },
                { "StackMapTable", () => attributesTable.Add(ReadAttributeStackMapTable(reader)) },
                { "BootstrapMethods", () => attributesTable.Add(ReadAttributeBootstrapMethods(reader)) },
                { "LineNumberTable", () => attributesTable.Add(ReadAttributeLineNumberTable(reader)) },
                { "SourceFile", () => attributesTable.Add(ReadAttributeSourceFile(reader)) }
            };
            for (int i = 0; i < attributesCount; i++)
            {
                if (nameActionDictionary.TryGetValue(cp.GetConstantUtf8(reader.ReadInt()).Value, out Action readAttribute))
                    readAttribute.Invoke();
                else
                    throw new KeyNotFoundException("Attribute type not recognized");
            }
            return new Attributes(attributesTable);
        }
        private static AttributeCode ReadAttributeCode(BytecodeReader reader, ConstantPool cp)
        {
            ushort attributeNameIndex = reader.ReadUShort();

            uint attributesLength = reader.ReadUInt();

            ushort maxStack = reader.ReadUShort();

            ushort maxLocals = reader.ReadUShort();

            uint codeLength = reader.ReadUInt();

            // typecasting could be bad
            byte[] bytecode = reader.ReadArray((int)codeLength);

            ushort exceptionTableLength = reader.ReadUShort();

            byte[] exceptionTable = reader.ReadArray(exceptionTableLength * 8);

            ushort attributesCount = reader.ReadUShort();

            Attributes attributes = ReadAttributes(reader, attributesCount, cp);
            return new AttributeCode(attributeNameIndex, attributesLength, maxStack, maxLocals, 
                codeLength, bytecode, exceptionTableLength, exceptionTable, attributesCount, attributes);
        }
        private static AttributeBootstrapMethods ReadAttributeBootstrapMethods(BytecodeReader reader)
        {
            ushort attributeNameIndex = reader.ReadUShort();

            uint attributeLength = reader.ReadUInt();

            return new AttributeBootstrapMethods(attributeNameIndex, attributeLength);
        }
        private static AttributeConstantValue ReadAttributeConstantValue(BytecodeReader reader)
        {
            ushort attributeNameIndex = reader.ReadUShort();

            uint attributeLength = reader.ReadUInt();

            return new AttributeConstantValue(attributeNameIndex, attributeLength);
        }
        private static AttributeLineNumberTable ReadAttributeLineNumberTable(BytecodeReader reader)
        {
            ushort attributeNameIndex = reader.ReadUShort();

            uint attributeLength = reader.ReadUInt();

            ushort lineNumberTableLength = reader.ReadUShort();

            List<byte[]> startPC = new List<byte[]>();
            List<byte[]> lineNumber = new List<byte[]>();

            for (int i = 0; i < lineNumberTableLength; i++)
            {
                startPC.Add(reader.ReadArray(2));
                lineNumber.Add(reader.ReadArray(2));
            }
            return new AttributeLineNumberTable(attributeNameIndex, attributeLength, 
                lineNumberTableLength, startPC, lineNumber);
        }
        private static AttributeSourceFile ReadAttributeSourceFile(BytecodeReader reader)
        {
            ushort attributeNameIndex = reader.ReadUShort();

            uint attributeLength = reader.ReadUInt();

            ushort sourceFileIndex = reader.ReadUShort();

            return new AttributeSourceFile(attributeNameIndex, attributeLength, sourceFileIndex);
        }
        private static AttributeStackMapTable ReadAttributeStackMapTable(BytecodeReader reader)
        {
            ushort attributeNameIndex = reader.ReadUShort();

            uint attributeLength = reader.ReadUInt();

            ushort numberOfEntries = reader.ReadUShort();

            return new AttributeStackMapTable(attributeNameIndex, attributeLength, numberOfEntries);
        }
    }
}
