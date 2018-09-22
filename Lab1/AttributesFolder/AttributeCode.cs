using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JavaInterpreter.AttributesFolder
{
    class AttributeCode : AttributeSuper
    {
        private ushort maxStack;
        public ushort MaxStack { get => maxStack; }

        private ushort maxLocals;
        public ushort MaxLocals { get => maxLocals; }

        private uint codeLength;
        public uint CodeLength { get => CodeLength; }

        private byte[] code;
        public byte[] Code
        {
            get
            {
                return code;
            }
        }
        private uint exceptionTableLength;
        public uint ExceptionTableLength { get => exceptionTableLength; }

        private byte[] exceptionTable;

        private ushort attributesCount;
        public ushort AttributesCount { get => attributesCount; }

        private Attributes attributes;
        
        public AttributeCode(ushort attributeNameIndex, uint attributeLength, ushort maxStack, ushort maxLocals, uint codeLength, byte[] code, uint exceptionTableLength, byte[] exceptionTable, ushort attributesCount, Attributes attributes) : base(attributeNameIndex, attributeLength)
        {
            this.maxStack = maxStack;
            this.maxLocals = maxLocals;
            this.codeLength = codeLength;
            this.code = code;
            this.exceptionTableLength = exceptionTableLength;
            this.exceptionTable = exceptionTable;
            this.attributesCount = attributesCount;
            this.attributes = attributes;
        }
        public static AttributeCode Create(byte[] code, ref int curIndex, ConstantPool cp)
        {
            ushort attributeNameIndex = Helper.ToUShort(code, ref curIndex);
            
            uint attributesLength = Helper.ToUInt(code, ref curIndex);

            ushort maxStack = Helper.ToUShort(code, ref curIndex);

            ushort maxLocals = Helper.ToUShort(code, ref curIndex);

            uint codeLength = Helper.ToUInt(code, ref curIndex);

            byte[] bytecode = new byte[codeLength];
            Array.Copy(code, curIndex, bytecode, 0, codeLength);
            curIndex += (int)codeLength;

            ushort exceptionTableLength = Helper.ToUShort(code, ref curIndex);

            byte[] exceptionTable = new byte[exceptionTableLength*8];
            Array.Copy(code, curIndex, exceptionTable, 0, exceptionTableLength*8);
            curIndex += (int)exceptionTableLength;

            ushort attributesCount = Helper.ToUShort(code, ref curIndex);

            Attributes attributes = Attributes.Create(code, attributesCount,ref curIndex, cp);
            return new AttributeCode(attributeNameIndex, attributesLength, maxStack, maxLocals, codeLength, bytecode, exceptionTableLength, exceptionTable, attributesCount, attributes);
        }

    }
}
