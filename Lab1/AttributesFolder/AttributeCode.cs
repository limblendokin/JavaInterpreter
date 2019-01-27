namespace JavaInterpreter.AttributesFolder
{
    public class AttributeCode
    {
        private ushort maxStack;
        public ushort MaxStack => maxStack;

        private ushort maxLocals;
        public ushort MaxLocals => maxLocals;

        private uint codeLength;
        public uint CodeLength => CodeLength;

        private byte[] code;
        public byte[] Code => code;

        private uint exceptionTableLength;
        public uint ExceptionTableLength => exceptionTableLength;

        private byte[] exceptionTable;

        private ushort attributesCount;
        public ushort AttributesCount => attributesCount;

        private Attributes attributes;
        
        public AttributeCode(ushort attributeNameIndex, uint attributeLength, ushort maxStack, ushort maxLocals, uint codeLength, byte[] code, uint exceptionTableLength, byte[] exceptionTable, ushort attributesCount, Attributes attributes)
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

    }
}
