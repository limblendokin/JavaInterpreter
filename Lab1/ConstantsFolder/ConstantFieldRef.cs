namespace JavaInterpreter.ConstantsFolder
{
    public class ConstantFieldRef
    {
        private ushort classIndex;
        public ushort ClassIndex => classIndex;
        private ushort nameAndTypeIndex;
        public ushort NameAndTypeIndex => nameAndTypeIndex;

        public ConstantFieldRef(ushort classIndex, ushort nameAndTypeIndex)
        {
            this.classIndex = classIndex;
            this.nameAndTypeIndex = nameAndTypeIndex;
        }
    }
}
