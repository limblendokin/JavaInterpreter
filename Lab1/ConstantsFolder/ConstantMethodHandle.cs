using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab1.ConstantsFolder
{
    class ConstantMethodHandle : Constant
    {
        private byte referenceKind;

        private ushort referenceIndex;
        public ushort ReferenceIndex { get => referenceIndex; }
        
        public ConstantMethodHandle(byte tag, byte referenceKind, ushort referenceIndex) : base(tag)
        {
            this.referenceKind = referenceKind;
            this.referenceIndex = referenceIndex;
        }
        public new static ConstantMethodHandle Create(byte[] code, ref int curIndex)
        {
            byte tag = code[curIndex++];

            byte referenceKind = code[curIndex++];

            ushort referenceIndex = Helper.ToUShort(code, ref curIndex);

            return new ConstantMethodHandle(tag, referenceKind, referenceIndex);
        }
    }
}
