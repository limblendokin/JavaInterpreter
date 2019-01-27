using System;
using System.Linq;
using System.Text;

namespace JavaInterpreter
{
    public class BytecodeReader
    {
        byte[] bytecode;
        int index;
        public BytecodeReader(byte[] bytecode)
        {
            index = 0;
            this.bytecode = bytecode;
        }
        public uint ReadUInt()
        {
            uint output;
            if (BitConverter.IsLittleEndian)
                output = BitConverter.ToUInt32(bytecode.Skip(index).Take(4).Reverse().ToArray(), 0);
            else
                output = BitConverter.ToUInt32(bytecode, index);
            index += 4;
            return output;
        }
        public ushort ReadUShort()
        {
            ushort output;
            if (BitConverter.IsLittleEndian)
                output = BitConverter.ToUInt16(bytecode.Skip(index).Take(2).Reverse().ToArray(), 0);
            else
                output = BitConverter.ToUInt16(bytecode, index);
            index += 2;
            return output;
        }
        public long ReadLong()
        {
            long output;
            if (BitConverter.IsLittleEndian)
                output = BitConverter.ToInt64(bytecode.Skip(index).Take(8).Reverse().ToArray(), 0);
            else
                output = BitConverter.ToInt64(bytecode, index);
            index += 8;
            return output;
        }
        public double ReadDouble()
        {
            double output;
            if (BitConverter.IsLittleEndian)
                output = BitConverter.ToDouble(bytecode.Skip(index).Take(8).Reverse().ToArray(), 0);
            else
                output = BitConverter.ToDouble(bytecode, index);
            index += 8;
            return output;
        }
        public int ReadInt()
        {
            int output;
            if (BitConverter.IsLittleEndian)
                output = BitConverter.ToInt32(bytecode.Skip(index).Take(4).Reverse().ToArray(), 0);
            else
                output = BitConverter.ToInt32(bytecode, index);
            index += 4;
            return output;
        }
        public float ReadFloat()
        {
            float output;
            if (BitConverter.IsLittleEndian)
                output = BitConverter.ToSingle(bytecode.Skip(index).Take(4).Reverse().ToArray(), 0);
            else
                output = BitConverter.ToSingle(bytecode, index);
            index += 4;
            return output;
        }
        public byte ReadByte()
        {
            return bytecode[index++];
        }
        public string ReadString(ushort length)
        {
            string output = Encoding.UTF8.GetString(bytecode.Skip(index).Take(length).ToArray());
            index += length;
            return output;
        }
        public byte[] ReadArray(int length)
        {
            byte[] array = new byte[length];
            Array.Copy(bytecode, index, bytecode, 0, length);
            index += length;
            return array;
        }
    }
}
