namespace JavaInterpreter.AttributesFolder
{
    public class AttributeSuper
    {
        protected ushort attributeNameIndex;
        public ushort AttributeNameIndex { get => attributeNameIndex; }

        protected uint attributeLength;
        public uint AttributeLength { get => attributeLength; }

        public AttributeSuper(ushort attributeNameIndex, uint attributeLength)
        {
            this.attributeNameIndex = attributeNameIndex;
            this.attributeLength = attributeLength;
        }
    }
}
