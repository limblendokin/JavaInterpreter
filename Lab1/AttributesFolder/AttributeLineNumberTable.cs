using System.Collections.Generic;

namespace JavaInterpreter.AttributesFolder
{
    public class AttributeLineNumberTable
    {
        private uint lineNumberTableLength;
        public uint LineNumberTableLength => lineNumberTableLength;

        private List<byte[]> startPC;
        private List<byte[]> lineNumber;

        public AttributeLineNumberTable(ushort attributeNameIndex, uint attributeLength, uint lineNumberTableLength, List<byte[]> startPC, List<byte[]> lineNumber)
        {
            this.lineNumberTableLength = lineNumberTableLength;
            this.startPC = startPC;
            this.lineNumber = lineNumber;
        }
    }
}
