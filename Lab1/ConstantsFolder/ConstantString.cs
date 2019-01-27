namespace JavaInterpreter.ConstantsFolder
{
    public class ConstantString
    {
        private ushort stringIndex;
        public ushort StringIndex => stringIndex;
        public ConstantString(ushort stringIndex)
        {
            this.stringIndex = stringIndex;
        }
    }
}
