using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JavaInterpreter.ConstantsFolder
{
    class ConstantMethodType
    {
        private ushort descriptorIndex;
        public ushort DescriptorIndex => descriptorIndex;
        

        public ConstantMethodType(ushort descriptorIndex)
        {
            this.descriptorIndex = descriptorIndex;
        }
    }
}
