using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JavaInterpreter.AttributesFolder;
namespace JavaInterpreter
{
    class Attributes
    {
        private List<AttributeBootstrapMethods> attributeBootstrapMethods;
        private List<AttributeCode> attributeCodes;
        private List<AttributeConstantValue> attributeConstantValues;
        private List<AttributeLineNumberTable> attributeLineNumberTables;
        private List<AttributeSourceFile> attributeSourceFiles;
        private List<AttributeStackMapTable> attributeStackMapTables;
        
        private List<int> router;
        private int FindIndexInCollection(int index)
        {
            return router.ElementAt(index);
        }

        public AttributeBootstrapMethods GetAttributeBootstrapMethods(int index)
        {
            return attributeBootstrapMethods.ElementAt(FindIndexInCollection(index));
        }
        public AttributeCode GetAttributeCode(int index)
        {
            return attributeCodes.ElementAt(FindIndexInCollection(index));
        }
        public AttributeConstantValue GetAttributeConstantValue(int index)
        {
            return attributeConstantValues.ElementAt(FindIndexInCollection(index));
        }
        public AttributeLineNumberTable GetAttributeLineNumberTable(int index)
        {
            return attributeLineNumberTables.ElementAt(FindIndexInCollection(index));
        }
        public AttributeSourceFile GetAttributeSourceFile(int index)
        {
            return attributeSourceFiles.ElementAt(FindIndexInCollection(index));
        }
        public AttributeStackMapTable GetAttributeStackMapTable(int index)
        {
            return attributeStackMapTables.ElementAt(FindIndexInCollection(index));
        }
        public void AddAttributeBootstrapMethod(AttributeBootstrapMethods attributeBootstrapMethod)
        {
            attributeBootstrapMethods.Add(attributeBootstrapMethod);
            router.Add(attributeBootstrapMethods.Count - 1);
        }
        public void AddAttributeCode(AttributeCode attributeCode)
        {
            attributeCodes.Add(attributeCode);
            router.Add(attributeCodes.Count - 1);
        }
        public void AddAttributeConstantValue(AttributeConstantValue attributeConstantValue)
        {
            attributeConstantValues.Add(attributeConstantValue);
            router.Add(attributeConstantValues.Count - 1);
        }
        public void AddAttributeLineNumberTable(AttributeLineNumberTable attributeLineNumberTable)
        {
            attributeLineNumberTables.Add(attributeLineNumberTable);
            router.Add(attributeLineNumberTables.Count - 1);
        }
        public void AddAttributeSourceFile(AttributeSourceFile attributeSourceFile)
        {
            attributeSourceFiles.Add(attributeSourceFile);
            router.Add(attributeSourceFiles.Count - 1);
        }
        public void AddAttributeStackMapTable(AttributeStackMapTable attributeStackMapTable)
        {
            attributeStackMapTables.Add(attributeStackMapTable);
            router.Add(attributeStackMapTables.Count - 1);
        }
        
        /// <summary>
        /// При вызове конструктора происходит создание атрибута, детали которого считываются из байткода, а затем его добавление в attributesTable 
        /// </summary>
        /// <param name="bytecode"></param>
        /// <param name="attributesCount"></param>
        /// <param name="curIndex"></param>
        /// <param name="cp"></param>
        public Attributes()
        {
            attributeBootstrapMethods = new List<AttributeBootstrapMethods>();
            attributeCodes = new List<AttributeCode>();
            attributeConstantValues = new List<AttributeConstantValue>();
            attributeLineNumberTables = new List<AttributeLineNumberTable>();
            attributeSourceFiles = new List<AttributeSourceFile>();
            attributeStackMapTables = new List<AttributeStackMapTable>();
            router = new List<int>();
        }
        
    }
}
