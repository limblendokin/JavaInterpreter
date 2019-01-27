namespace JavaInterpreter.ConstantsFolder
{
    public class ConstantMethodHandle
    {
        private byte referenceKind;

        private ushort referenceIndex;
        public ushort ReferenceIndex => referenceIndex;
        
        public ConstantMethodHandle(byte referenceKind, ushort referenceIndex)
        {
            this.referenceKind = referenceKind;
            this.referenceIndex = referenceIndex;
        }
    }
}
