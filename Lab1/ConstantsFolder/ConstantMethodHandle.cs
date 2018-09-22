using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JavaInterpreter.ConstantsFolder
{
    class ConstantMethodHandle
    {
        private byte referenceKind;

        private ushort referenceIndex;
        public ushort ReferenceIndex => referenceIndex;
        
        public ConstantMethodHandle(byte referenceKind, ushort referenceIndex)
        {
            this.referenceKind = referenceKind;
            this.referenceIndex = referenceIndex;
        }
    }
}
