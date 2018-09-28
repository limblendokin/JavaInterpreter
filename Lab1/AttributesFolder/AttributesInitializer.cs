using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JavaInterpreter.AttributesFolder
{
    class AttributesInitializer
    {
        public static Attributes ReadAttributes(BytecodeReader reader, ushort attributesCount)
        {
            Attributes attributes = new Attributes();
            Dictionary<String, Action> attributeNameDictionary = new Dictionary<string, Action>();
            attributeNameDictionary.Add("Code", () =>
            {
                ushort attributeNameIndex = reader.ReadUShort();
                uint attributeLength = reader.ReadUInt();
                ushort maxStack = reader.ReadUShort();
                ushort maxLocals = reader.ReadUShort();
                uint codeLength = reader.ReadUInt();
                byte[] code = reader.ReadBytes(codeLength);
                ushort exceptionTableLength = reader.ReadUShort();
                byte[] exceptionTable = reader.ReadBytes((uint)exceptionTableLength * 8);
                ushort codeAttributesCount = reader.ReadUShort();
                Attributes codeAttributes = ReadAttributes(reader, codeAttributesCount);
                attributes.AddAttributeCode(attributeNameIndex, attributeLength,
                    maxStack, maxLocals, codeLength, code, exceptionTableLength, 
                    exceptionTable, attributesCount, attributes);
            });
            short attributeIndex = 0;
            var attributesTable = new List<AttributeSuper>();
            int curAttributeNameIndex;
            while (attributesCount > attributeIndex)
            {
                curAttributeNameIndex = bytecode[index] * 0x100 + bytecode[index + 1];
                switch (cp.getConstantUtf8(curAttributeNameIndex).Value)
                {
                    case "Code":
                        attributesTable.Add(AttributeCode.Create(bytecode, cp));
                        attributeIndex++;
                        break;

                    case "ConstantValue":
                        // TODO: not skipping bytes
                        Console.WriteLine("ConstantValue attribute was created");
                        attributesTable.Add(AttributeSuper.Create(bytecode));
                        index += (int)attributesTable.Last().AttributeLength;
                        attributeIndex++;
                        break;

                    case "StackMapTable":
                        // TODO: not skipping bytes
                        Console.WriteLine("StackMapTable attribute was created");
                        attributesTable.Add(AttributeSuper.Create(bytecode));
                        index += (int)attributesTable.Last().AttributeLength;
                        attributeIndex++;
                        break;

                    case "BootstrapMethods":
                        // TODO: not skipping bytes
                        Console.WriteLine("BootstrapMethods attribute was created");
                        attributesTable.Add(AttributeSuper.Create(bytecode));
                        index += (int)attributesTable.Last().AttributeLength;
                        attributeIndex++;
                        break;

                    case "LineNumberTable":
                        attributesTable.Add(AttributeLineNumberTable.Create(bytecode, cp));
                        attributeIndex++;
                        break;

                    case "SourceFile":
                        attributesTable.Add(AttributeSourceFile.Create(bytecode, cp));
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
