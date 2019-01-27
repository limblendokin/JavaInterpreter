namespace JavaInterpreter.AttributesFolder
{
    public class AttributeSourceFile
    {
        private ushort sourceFileIndex;
        public AttributeSourceFile(ushort attributeNameIndex, uint attributeLength, ushort sourceFileIndex)
        {
            this.sourceFileIndex = sourceFileIndex;
        }
    }
}
