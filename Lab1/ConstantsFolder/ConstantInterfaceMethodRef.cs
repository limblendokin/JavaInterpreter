namespace JavaInterpreter.ConstantsFolder
{
    public class ConstantInterfaceMethodRef
    {
        private ushort classIndex;
        public ushort ClassIndex => classIndex;

        private ushort nameAndTypeIndex;
        public ushort NameAndTypeIndex => nameAndTypeIndex;
        
        public ConstantInterfaceMethodRef(ushort classIndex, ushort nameAndTypeIndex)
        {
            this.classIndex = classIndex;
            this.nameAndTypeIndex = nameAndTypeIndex;
        }
    }
}
