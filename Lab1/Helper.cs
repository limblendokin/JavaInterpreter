using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JavaInterpreter
{
    class Helper
    {
        public static uint ToUInt(byte[] array, ref int index)
        {
            uint output;
            if (BitConverter.IsLittleEndian)
                output = BitConverter.ToUInt32(array.Skip(index).Take(4).Reverse().ToArray(), 0);
            else
                output = BitConverter.ToUInt32(array, index);
            index += 4;
            return output;
        }
        public static ushort ToUShort(byte[] array, ref int index)
        {
            ushort output;
            if (BitConverter.IsLittleEndian)
                output = BitConverter.ToUInt16(array.Skip(index).Take(2).Reverse().ToArray(), 0);
            else
                output = BitConverter.ToUInt16(array, index);
            index += 2;
            return output;
        }
        public static long ToLong(byte[] array, ref int index)
        {
            long output;
            if (BitConverter.IsLittleEndian)
                output = BitConverter.ToInt64(array.Skip(index).Take(8).Reverse().ToArray(), 0);
            else
                output = BitConverter.ToInt64(array, index);
            index += 8;
            return output;
        }
        public static int ToInt(byte[] array, ref int index)
        {
            int output;
            if (BitConverter.IsLittleEndian)
                output = BitConverter.ToInt32(array.Skip(index).Take(4).Reverse().ToArray(), 0);
            else
                output = BitConverter.ToInt32(array, index);
            index += 4;
            return output;
        }
    }
}
