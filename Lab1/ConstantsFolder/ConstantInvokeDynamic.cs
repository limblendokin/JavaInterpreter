namespace JavaInterpreter.ConstantsFolder
{
    public class ConstantInvokeDynamic
    {
        private ushort bootstrapMethodAttrIndex;
        public ushort BootstrapMethodAttrIndex => bootstrapMethodAttrIndex;

        private ushort nameAndTypeIndex;
        public ushort NameAndTypeIndex => nameAndTypeIndex;
        

        public ConstantInvokeDynamic(ushort bootstrapMethodAttrIndex, ushort nameAndTypeIndex)
        {
            this.bootstrapMethodAttrIndex = bootstrapMethodAttrIndex;
            this.nameAndTypeIndex = nameAndTypeIndex;
        }
    }
}
