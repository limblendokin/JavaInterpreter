namespace JavaInterpreter.ConstantsFolder
{
    public class ConstantClass
    {
        private ushort nameIndex;
        public ushort NameIndex => nameIndex;
        
        public ConstantClass(ushort nameIndex)
        {
            this.nameIndex = nameIndex;
        }
        
    }
}
