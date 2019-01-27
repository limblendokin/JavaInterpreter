namespace JavaInterpreter.ConstantsFolder
{
    public class ConstantUtf8
    {
        public ConstantType ConstantType = ConstantType.Utf8;
        private ushort length;
        public ushort Length => length;

        private string value;
        public string Value => value;
        
        public ConstantUtf8(ushort length, string value)
        {
            this.length = length;
            this.value = value;
        }
    }
}
