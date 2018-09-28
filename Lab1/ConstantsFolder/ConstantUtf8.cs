using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JavaInterpreter.ConstantsFolder
{
    class ConstantUtf8
    {
        public ConstantType ConstantType = ConstantType.Utf8;
        private ushort length;
        public ushort Length => length;

        private String value;
        public String Value => value;
        
        public ConstantUtf8(ushort length, String value)
        {
            this.length = length;
            this.value = value;
        }
    }
}
