namespace JavaInterpreter.ConstantsFolder
{
    public class ConstantModule
    {
        private ushort nameIndex;
        public ushort NameIndex => nameIndex;

        public ConstantModule(ushort nameIndex)
        {
            this.nameIndex = nameIndex;
        }
    }
}
