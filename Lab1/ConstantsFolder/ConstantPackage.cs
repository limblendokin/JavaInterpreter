namespace JavaInterpreter.ConstantsFolder
{
    public class ConstantPackage
    {
        private ushort nameIndex;
        public ushort NameIndex => nameIndex;
        public ConstantPackage(ushort nameIndex)
        {
            this.nameIndex = nameIndex;
        }
    }
}
