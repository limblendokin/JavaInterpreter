using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lab1.AttributesFolder;
namespace Lab1
{
    class Attributes
    {
        private List<AttributeSuper> attributesTable;
        public List<AttributeSuper> AttributesTable
        {
            get
            {
                return attributesTable;
            }
        }
        private Attributes()
        {

        }
        /// <summary>
        /// При вызове конструктора происходит создание атрибута, детали которого считываются из байткода, а затем его добавление в attributesTable 
        /// </summary>
        /// <param name="bytecode"></param>
        /// <param name="attributes_count"></param>
        /// <param name="curIndex"></param>
        /// <param name="cp"></param>
        public Attributes(byte[] bytecode, short attributes_count, ref int curIndex, ref ConstantPool cp)
        {
            short attributeIndex = 0;
            attributesTable = new List<AttributeSuper>();
            int curAttributeNameIndex;
            while (attributes_count > attributeIndex)
            {
                curAttributeNameIndex = bytecode[curIndex] * 0x100 + bytecode[curIndex+1];
                switch (cp.getConstantUtf8(curAttributeNameIndex).Value)
                {
                    case "Code":
                        attributesTable.Add(AttributeCode.Create(bytecode, ref curIndex, ref cp));
                        attributeIndex++;
                        break;

                    case "ConstantValue":
                        // TODO: not skipping bytes
                        Console.WriteLine("ConstantValue attribute was created");
                        attributesTable.Add(AttributeSuper.Create(bytecode, ref curIndex));
                        curIndex += (int)attributesTable.Last().AttributeLength;
                        attributeIndex++;
                        break;

                    case "StackMapTable":
                        // TODO: not skipping bytes
                        Console.WriteLine("StackMapTable attribute was created");
                        attributesTable.Add(AttributeSuper.Create(bytecode, ref curIndex));
                        curIndex += (int)attributesTable.Last().AttributeLength;
                        attributeIndex++;
                        break;

                    case "BootstrapMethods":
                        // TODO: not skipping bytes
                        Console.WriteLine("BootstrapMethods attribute was created");
                        attributesTable.Add(AttributeSuper.Create(bytecode, ref curIndex));
                        curIndex += (int)attributesTable.Last().AttributeLength;
                        attributeIndex++;
                        break;

                    case "LineNumberTable":
                        attributesTable.Add(AttributeLineNumberTable.Create(bytecode, ref curIndex, ref cp));
                        attributeIndex++;
                        break;

                    case "SourceFile":
                        attributesTable.Add(AttributeSourceFile.Create(bytecode, ref curIndex, ref cp));
                        attributeIndex++;
                        break;

                    default:
                        attributeIndex++;
                        Console.WriteLine("no attribute found");
                        break;
                }
            }
        }
    }
}
