using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JavaInterpreter.ConstantsFolder
{
    class ConstantPoolInitializer
    {
        public static ConstantPool ReadConstantPool(BytecodeReader reader, ushort constantPoolCount)
        {
            // TODO: check if long and double constants reading right
            ConstantPool constantPool = new ConstantPool();
            int a = 0;
            var tagDictionary = new Dictionary<int, Action>();
            tagDictionary.Add(1, () =>
            {
                ushort length = reader.ReadUShort();
                String value = reader.ReadString(length);
                constantPool.AddConstantUtf8(new ConstantUtf8(length, value));
            });
            tagDictionary.Add(3, () => constantPool.AddConstantInteger(reader.ReadInt()));
            tagDictionary.Add(4, () => constantPool.AddConstantFloat(reader.ReadFloat()));
            tagDictionary.Add(5, () => constantPool.AddConstantLong(reader.ReadLong()));
            tagDictionary.Add(6, () => constantPool.AddConstantDouble(reader.ReadDouble()));
            tagDictionary.Add(7, () => constantPool.AddConstantClass(new ConstantClass(reader.ReadUShort())));
            tagDictionary.Add(8, () => constantPool.AddConstantString(new ConstantString(reader.ReadUShort())));
            tagDictionary.Add(9, () => constantPool.AddConstantFieldRef(new ConstantFieldRef(reader.ReadUShort(), reader.ReadUShort())));
            tagDictionary.Add(10, () => constantPool.AddConstantMethodRef(new ConstantMethodRef(reader.ReadUShort(), reader.ReadUShort())));
            tagDictionary.Add(11, () => constantPool.AddConstantInterfaceMethodRef(new ConstantInterfaceMethodRef(reader.ReadUShort(), reader.ReadUShort())));
            tagDictionary.Add(12, () => constantPool.AddConstantNameAndType(new ConstantNameAndType(reader.ReadUShort(), reader.ReadUShort())));
            tagDictionary.Add(15, () => constantPool.AddConstantMethodHandle(new ConstantMethodHandle(reader.ReadByte(), reader.ReadUShort())));
            tagDictionary.Add(16, () => constantPool.AddConstantMethodType(new ConstantMethodType(reader.ReadUShort())));
            tagDictionary.Add(18, () => constantPool.AddConstantInvokeDynamic(new ConstantInvokeDynamic(reader.ReadUShort(), reader.ReadUShort())));
            tagDictionary.Add(19, () => constantPool.AddConstantModule(new ConstantModule(reader.ReadUShort())));
            tagDictionary.Add(20, () => constantPool.AddConstantPackage(new ConstantPackage(reader.ReadUShort())));
            Action createConstant;
            for (int i = 0; i < constantPoolCount; i++)
            {
                if (tagDictionary.TryGetValue(reader.ReadByte(), out createConstant))
                    createConstant.Invoke();
                else
                    throw new KeyNotFoundException("Constant type not recognized");
            }
            return constantPool;
        }
    }
}
