using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lab1.ConstantsFolder;

namespace Lab1
{
    class ConstantPool
    {
        private List<Constant> cp;
        private List<ConstantClass> constantClasses;
        // getting constants
        public List<ConstantClass> GetConstantClasses() => constantClasses;
        public Constant getConstant(int index)
        {
            return cp.ElementAt(index);
        }
        public ConstantClass getConstantClass(int index)
        {
            return (ConstantClass)cp.ElementAt(index);
        }
        public ConstantDouble getConstantDouble(int index)
        {
            return (ConstantDouble)cp.ElementAt(index);
        }
        public ConstantFieldRef getConstantFieldRef(int index)
        {
            return (ConstantFieldRef)cp.ElementAt(index);
        }
        public ConstantFloat getConstantFloat(int index)
        {
            return (ConstantFloat)cp.ElementAt(index);
        }
        public ConstantInteger getConstantInteger(int index)
        {
            return (ConstantInteger)cp.ElementAt(index);
        }
        public ConstantInterfaceMethodRef getConstantInterfaceMethodRef(int index)
        {
            return (ConstantInterfaceMethodRef)cp.ElementAt(index);
        }
        public ConstantInvokeDynamic getConstantInvokeDynamic(int index)
        {
            return (ConstantInvokeDynamic)cp.ElementAt(index);
        }
        public ConstantLong getConstantLong(int index)
        {
            return (ConstantLong)cp.ElementAt(index);
        }
        public ConstantMethodHandle getConstantMethodHandle(int index)
        {
            return (ConstantMethodHandle)cp.ElementAt(index);
        }
        public ConstantMethodRef getConstantMethodRef(int index)
        {
            return (ConstantMethodRef)cp.ElementAt(index);
        }
        public ConstantMethodType getConstantMethodType(int index)
        {
            return (ConstantMethodType)cp.ElementAt(index);
        }
        public ConstantModule getConstantModule(int index)
        {
            return (ConstantModule)cp.ElementAt(index);
        }
        public ConstantNameAndType getConstantNameAndType(int index)
        {
            return (ConstantNameAndType)cp.ElementAt(index);
        }
        public ConstantPackage getConstantPackage(int index)
        {
            return (ConstantPackage)cp.ElementAt(index);
        }
        public ConstantString getConstantString(int index)
        {
            return (ConstantString)cp.ElementAt(index);
        }
        public ConstantUtf8 getConstantUtf8(int index)
        {
            return (ConstantUtf8)cp.ElementAt(index);
        }

        private ConstantPool()
        {

        }
        /// <summary>
        /// Вызов конеструктора формирует пул констант данного *.class файла
        /// </summary>
        /// <param name="bytecode"></param>
        /// <param name="constant_pool_count"></param>
        /// <param name="curIndex"></param>
        public ConstantPool(byte[] bytecode, short constant_pool_count, ref int curIndex)
        {
            int constIndex = 0;
            cp = new List<Constant>();
            cp.Add(null);
            constantClasses = new List<ConstantClass>();
            while (constIndex < constant_pool_count-1)
            {
                switch (bytecode[curIndex])
                {
                    // CONSTANT_Class
                    case 0x07:
                        cp.Add(ConstantClass.Create(bytecode, ref curIndex));
                        constantClasses.Add((ConstantClass)cp.Last());
                        constIndex++;
                        break;

                    // CONSTANT_Fieldref
                    case 0x09:
                        cp.Add(ConstantFieldRef.Create(bytecode, ref curIndex));
                        constIndex++;
                        break;

                    //CONSTANT_Methodref
                    case 0x0a:
                        cp.Add(ConstantMethodRef.Create(bytecode, ref curIndex));
                        constIndex++;
                        break;

                    //CONSTANT_InterfaceMethodref
                    case 0x0b:
                        cp.Add(ConstantInterfaceMethodRef.Create(bytecode, ref curIndex));
                        constIndex++;
                        break;

                    //CONSTANT_String
                    case 0x08:
                        cp.Add(ConstantString.Create(bytecode, ref curIndex));
                        constIndex++;
                        break;

                    //CONSTANT_Integer
                    case 0x03:
                        cp.Add(ConstantInteger.Create(bytecode, ref curIndex));
                        constIndex++;
                        break;

                    //CONSTANT_Float
                    case 0x04:
                        cp.Add(ConstantFloat.Create(bytecode, ref curIndex));
                        constIndex++;
                        break;

                    //CONSTANT_Long
                    case 0x05:
                        cp.Add(ConstantLong.Create(bytecode, ref curIndex));
                        constIndex++;
                        break;

                    //CONSTANT_Double
                    case 0x06:
                        cp.Add(ConstantDouble.Create(bytecode, ref curIndex));
                        constIndex++;
                        break;

                    //CONSTANT_NameAndType
                    case 0x0c:
                        cp.Add(ConstantNameAndType.Create(bytecode, ref curIndex));
                        constIndex++;
                        break;

                    //CONSTANT_Utf8
                    case 0x01:
                        cp.Add(ConstantUtf8.Create(bytecode, ref curIndex));
                        constIndex++;
                        break;

                    //CONSTANT_MethodHandle
                    case 0x0f:
                        cp.Add(ConstantMethodHandle.Create(bytecode, ref curIndex));
                        constIndex++;
                        break;

                    //CONSTANT_MethodType
                    case 0x10:
                        cp.Add(ConstantMethodType.Create(bytecode, ref curIndex));
                        constIndex++;
                        break;

                    //CONSTANT_InvokeDynamic
                    case 0x12:
                        cp.Add(ConstantInvokeDynamic.Create(bytecode, ref curIndex));
                        constIndex++;
                        break;

                    //CONSTANT_Module
                    case 0x13:
                        cp.Add(ConstantModule.Create(bytecode, ref curIndex));
                        constIndex++;
                        break;

                    //CONSTANT_Package
                    case 0x14:
                        cp.Add(ConstantPackage.Create(bytecode, ref curIndex));
                        constIndex++;
                        break;

                    default:
                        Console.WriteLine("No const type");
                        break;
                }
            }
        }
    }
}
