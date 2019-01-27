using System.Collections.Generic;
using System.Linq;
using JavaInterpreter.ConstantsFolder;

namespace JavaInterpreter
{
    public class ConstantPool
    {
        private List<ConstantClass> constantClasses;
        private List<double> constantDoubles;
        private List<ConstantFieldRef> constantFieldRefs;
        private List<float> constantFloats;
        private List<int> constantIntegers;
        private List<ConstantInterfaceMethodRef> constantInterfaceMethodRefs;
        private List<ConstantInvokeDynamic> constantInvokeDynamics;
        private List<long> constantLongs;
        private List<ConstantMethodHandle> constantMethodHandles;
        private List<ConstantMethodRef> constantMethodRefs;
        private List<ConstantMethodType> constantMethodTypes;
        private List<ConstantModule> constantModules;
        private List<ConstantNameAndType> constantNameAndTypes;
        private List<ConstantPackage> constantPackages;
        private List<ConstantString> constantStrings;
        private List<ConstantUtf8> constantUtf8s;

        int counter;
        private List<int> router;
        //getting constants
        private int FindIndexInCollection(int index)
        {
            return router.ElementAt(index);
        }

        public ConstantClass GetConstantClass(int index)
        {
            return constantClasses.ElementAt(FindIndexInCollection(index));
        }
        public double GetConstantDouble(int index)
        {
            return constantDoubles.ElementAt(FindIndexInCollection(index));
        }
        public ConstantFieldRef GetConstantFieldRef(int index)
        {
            return constantFieldRefs.ElementAt(FindIndexInCollection(index));
        }
        public float GetConstantFloat(int index)
        {
            return constantFloats.ElementAt(FindIndexInCollection(index));
        }
        public int GetConstantInteger(int index)
        {
            return constantIntegers.ElementAt(FindIndexInCollection(index));
        }
        public ConstantInterfaceMethodRef GetConstantInterfaceMethodRef(int index)
        {
            return constantInterfaceMethodRefs.ElementAt(FindIndexInCollection(index));
        }
        public ConstantInvokeDynamic GetConstantInvokeDynamic(int index)
        {
            return constantInvokeDynamics.ElementAt(FindIndexInCollection(index));
        }
        public long GetConstantLong(int index)
        {
            return constantLongs.ElementAt(FindIndexInCollection(index));
        }
        public ConstantMethodHandle GetConstantMethodHandle(int index)
        {
            return constantMethodHandles.ElementAt(FindIndexInCollection(index));
        }
        public ConstantMethodRef GetConstantMethodRef(int index)
        {
            return constantMethodRefs.ElementAt(FindIndexInCollection(index));
        }
        public ConstantMethodType GetConstantMethodType(int index)
        {
            return constantMethodTypes.ElementAt(FindIndexInCollection(index));
        }
        public ConstantModule GetConstantModule(int index)
        {
            return constantModules.ElementAt(FindIndexInCollection(index));
        }
        public ConstantNameAndType GetConstantNameAndType(int index)
        {
            return constantNameAndTypes.ElementAt(FindIndexInCollection(index));
        }
        public ConstantPackage GetConstantPackage(int index)
        {
            return constantPackages.ElementAt(FindIndexInCollection(index));
        }
        public ConstantString GetConstantString(int index)
        {
            return constantStrings.ElementAt(FindIndexInCollection(index));
        }
        public ConstantUtf8 GetConstantUtf8(int index)
        {
            return constantUtf8s.ElementAt(FindIndexInCollection(index));
        }
        public void AddConstantClass(ConstantClass constantClass)
        {
            constantClasses.Add(constantClass);
            router.Add(constantClasses.Count);
        }
        public void AddConstantDouble(double constantDouble)
        {
            constantDoubles.Add(constantDouble);
            router.Add(constantDoubles.Count);
        }
        public void AddConstantFieldRef(ConstantFieldRef constantFieldRef)
        {
            constantFieldRefs.Add(constantFieldRef);
            router.Add(constantFieldRefs.Count);
        }
        public void AddConstantFloat(float constantFloat)
        {
            constantFloats.Add(constantFloat);
            router.Add(constantFloats.Count);
        }
        public void AddConstantInteger(int constantInteger)
        {
            constantIntegers.Add(constantInteger);
            router.Add(constantIntegers.Count);
        }
        public void AddConstantInterfaceMethodRef(ConstantInterfaceMethodRef constantInterfaceMethodRef)
        {
            constantInterfaceMethodRefs.Add(constantInterfaceMethodRef);
            router.Add(constantInterfaceMethodRefs.Count);
        }
        public void AddConstantInvokeDynamic(ConstantInvokeDynamic constantInvokeDynamic)
        {
            constantInvokeDynamics.Add(constantInvokeDynamic);
            router.Add(constantInvokeDynamics.Count);
        }
        public void AddConstantLong(long constantLong)
        {
            constantLongs.Add(constantLong);
            router.Add(constantLongs.Count);
        }
        public void AddConstantMethodHandle(ConstantMethodHandle constantMethodHandle)
        {
            constantMethodHandles.Add(constantMethodHandle);
            router.Add(constantMethodHandles.Count);
        }
        public void AddConstantMethodRef(ConstantMethodRef constantMethodRef)
        {
            constantMethodRefs.Add(constantMethodRef);
            router.Add(constantMethodRefs.Count);
        }
        public void AddConstantMethodType(ConstantMethodType constantMethodType)
        {
            constantMethodTypes.Add(constantMethodType);
            router.Add(constantMethodTypes.Count);
        }
        public void AddConstantModule(ConstantModule constantModule)
        {
            constantModules.Add(constantModule);
            router.Add(constantModules.Count);
        }
        public void AddConstantNameAndType(ConstantNameAndType constantNameAndType)
        {
            constantNameAndTypes.Add(constantNameAndType);
            router.Add(constantNameAndTypes.Count);
        }
        public void AddConstantPackage(ConstantPackage constantPackage)
        {
            constantPackages.Add(constantPackage);
            router.Add(constantPackages.Count);
        }
        public void AddConstantString(ConstantString constantString)
        {
            constantStrings.Add(constantString);
            router.Add(constantStrings.Count);
        }
        public void AddConstantUtf8(ConstantUtf8 constantUtf8)
        {
            constantUtf8s.Add(constantUtf8);
            router.Add(constantUtf8s.Count);
        }

        public void AddConstant<T>(T constant)
        {

        }
        /// <summary>
        /// Вызов конструктора формирует пул констант данного *.class файла
        /// </summary>
        /// <param name="bytecode"></param>
        /// <param name="constant_pool_count"></param>
        /// <param name="curIndex"></param>
        public ConstantPool()
        {
            counter = 1;
            router = new List<int>();
            constantClasses = new List<ConstantClass>();
            constantDoubles = new List<double>();
            constantFieldRefs = new List<ConstantFieldRef>();
            constantFloats = new List<float>();
            constantIntegers = new List<int>();
            constantInterfaceMethodRefs = new List<ConstantInterfaceMethodRef>();
            constantInvokeDynamics = new List<ConstantInvokeDynamic>();
            constantLongs = new List<long>();
            constantMethodHandles = new List<ConstantMethodHandle>();
            constantMethodRefs = new List<ConstantMethodRef>();
            constantMethodTypes = new List<ConstantMethodType>();
            constantModules = new List<ConstantModule>();
            constantNameAndTypes = new List<ConstantNameAndType>();
            constantPackages = new List<ConstantPackage>();
            constantStrings = new List<ConstantString>();
            constantUtf8s = new List<ConstantUtf8>();
        }
    }
}
