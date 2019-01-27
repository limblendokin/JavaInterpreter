namespace JavaInterpreter.ConstantsFolder
{
    public class ConstantNameAndType
    {
        private ushort nameIndex;
        public ushort NameIndex => nameIndex;

        private ushort descriptorIndex;
        public ushort DescriptorIndex => descriptorIndex;
        
        public ConstantNameAndType(ushort nameIndex, ushort descriptorIndex)
        {
            this.nameIndex = nameIndex;
            this.descriptorIndex = descriptorIndex;
        }
    }
}
