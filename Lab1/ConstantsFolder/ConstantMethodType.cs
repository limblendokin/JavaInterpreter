namespace JavaInterpreter.ConstantsFolder
{
    public class ConstantMethodType
    {
        private ushort descriptorIndex;
        public ushort DescriptorIndex => descriptorIndex;
        

        public ConstantMethodType(ushort descriptorIndex)
        {
            this.descriptorIndex = descriptorIndex;
        }
    }
}
