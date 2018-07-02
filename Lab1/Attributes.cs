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
        /// <summary>
        /// При вызове конструктора происходит создание атрибута, детали которого считываются из байткода, а затем его добавление в attributesTable 
        /// </summary>
        /// <param name="bytecode"></param>
        /// <param name="attributesCount"></param>
        /// <param name="curIndex"></param>
        /// <param name="cp"></param>
        public Attributes(List<AttributeSuper> attributesTable)
        {
            this.attributesTable = attributesTable;
        }
        public static Attributes Create(byte[] code, ushort attributesCount, ref int curIndex, ConstantPool cp)
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
