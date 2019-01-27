namespace JavaInterpreter.ConstantsFolder
{
    public class ConstantMethodRef
    {
        private ushort classIndex;
        public ushort ClassIndex => classIndex;

        private ushort nameAndTypeIndex;
        public ushort NameAndTypeIndex => nameAndTypeIndex;
        

        public ConstantMethodRef(ushort classIndex, ushort nameAndTypeIndex)
        {
            this.classIndex = classIndex;
            this.nameAndTypeIndex = nameAndTypeIndex;
        }
    }
}
