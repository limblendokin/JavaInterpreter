using System.Collections.Generic;
using JavaInterpreter.AttributesFolder;
namespace JavaInterpreter
{
    public class Attributes
    {
        private List<object> attributesTable;
        public List<object> AttributesTable
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
        public Attributes(List<object> attributesTable)
        {
            this.attributesTable = attributesTable;
        }
        
    }
}
