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
        //debug public
        public List<Constant> cp;
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
        /// Вызов конструктора формирует пул констант данного *.class файла
        /// </summary>
        /// <param name="bytecode"></param>
        /// <param name="constant_pool_count"></param>
        /// <param name="curIndex"></param>
        public ConstantPool(List<Constant> constantPool, List<ConstantClass> constantClasses)
        {
            this.cp = constantPool;
            this.constantClasses = constantClasses;
        }
        
    }
}
