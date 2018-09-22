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
        
    }
}
