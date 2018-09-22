using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JavaInterpreter.ConstantsFolder;

namespace JavaInterpreter
{
    class ConstantPool
    {
        public enum ConstantType
        {
            Utf8 = 1,
            Integer = 3,
            Float = 4,
            Long = 5,
            Double = 6,
            Class = 7,
            String = 8,
            FieldRef = 9,
            MethodRef = 10,
            InterfaceMethodRef = 11,
            NameAndType = 12,
            MethodHandle = 15,
            MethodType = 16,
            InvokeDynamic = 18,
            Module = 19,
            Package = 20
        };
        private List<ConstantClass> constantClasses;
        private List<ConstantDouble> constantDoubles;
        private List<ConstantFieldRef> constantFieldRefs;
        private List<ConstantFloat> constantFloats;
        private List<ConstantInteger> constantIntegers;
        private List<ConstantInterfaceMethodRef> constantInterfaceMethodRefs;
        private List<ConstantInvokeDynamic> constantInvokeDynamics;
        private List<ConstantLong> constantLongs;
        private List<ConstantMethodHandle> constantMethodHandles;
        private List<ConstantMethodRef> constantMethodRefs;
        private List<ConstantMethodType> constantMethodTypes;
        private List<ConstantModule> constantModules;
        private List<ConstantNameAndType> constantNameAndTypes;
        private List<ConstantPackage> constantPackages;
        private List<ConstantString> constantStrings;
        private List<ConstantUtf8> constantUtf8s;

        int counter;
        private Dictionary<int, int> router;
        // getting constants
        private int FindIndexInCollection(int index)
        {
            int indexInCollection;
            if (router.TryGetValue(index, out indexInCollection))
                return indexInCollection;
            else
                throw new KeyNotFoundException();
        }

        public ConstantClass getConstantClass(int index)
        {
            return constantClasses.ElementAt(FindIndexInCollection(index));
        }
        public ConstantDouble getConstantDouble(int index)
        {
            return constantDoubles.ElementAt(FindIndexInCollection(index));
        }
        public ConstantFieldRef getConstantFieldRef(int index)
        {
            return constantFieldRefs.ElementAt(FindIndexInCollection(index));
        }
        public ConstantFloat getConstantFloat(int index)
        {
            return constantFloats.ElementAt(FindIndexInCollection(index));
        }
        public ConstantInteger getConstantInteger(int index)
        {
            return constantIntegers.ElementAt(FindIndexInCollection(index));
        }
        public ConstantInterfaceMethodRef getConstantInterfaceMethodRef(int index)
        {
            return constantInterfaceMethodRefs.ElementAt(FindIndexInCollection(index));
        }
        public ConstantInvokeDynamic getConstantInvokeDynamic(int index)
        {
            return constantInvokeDynamics.ElementAt(FindIndexInCollection(index));
        }
        public ConstantLong getConstantLong(int index)
        {
            return constantLongs.ElementAt(FindIndexInCollection(index));
        }
        public ConstantMethodHandle getConstantMethodHandle(int index)
        {
            return constantMethodHandles.ElementAt(FindIndexInCollection(index));
        }
        public ConstantMethodRef getConstantMethodRef(int index)
        {
            return constantMethodRefs.ElementAt(FindIndexInCollection(index));
        }
        public ConstantMethodType getConstantMethodType(int index)
        {
            return constantMethodTypes.ElementAt(FindIndexInCollection(index));
        }
        public ConstantModule getConstantModule(int index)
        {
            return constantModules.ElementAt(FindIndexInCollection(index));
        }
        public ConstantNameAndType getConstantNameAndType(int index)
        {
            return constantNameAndTypes.ElementAt(FindIndexInCollection(index));
        }
        public ConstantPackage getConstantPackage(int index)
        {
            return constantPackages.ElementAt(FindIndexInCollection(index));
        }
        public ConstantString getConstantString(int index)
        {
            return constantStrings.ElementAt(FindIndexInCollection(index));
        }
        public ConstantUtf8 getConstantUtf8(int index)
        {
            return constantUtf8s.ElementAt(FindIndexInCollection(index));
        }
        public void AddConstantClass(ConstantClass constantClass)
        {
            constantClasses.Add(constantClass);
            router.Add(counter++, constantClasses.Count);
        }
        public void AddConstantDouble(ConstantDouble constantDouble)
        {
            constantDoubles.Add(constantDouble);
            router.Add(counter++, constantDoubles.Count);
        }
        public void AddConstantFieldRef(ConstantFieldRef constantFieldRef)
        {
            constantFieldRefs.Add(constantFieldRef);
            router.Add(counter++, constantFieldRefs.Count);
        }
        public void AddConstantFloat(ConstantFloat constantFloat)
        {
            constantFloats.Add(constantFloat);
            router.Add(counter++, constantFloats.Count);
        }
        public void AddConstantInteger(ConstantInteger constantInteger)
        {
            constantIntegers.Add(constantInteger);
            router.Add(counter++, constantIntegers.Count);
        }
        public void AddConstantInterfaceMethodRef(ConstantInterfaceMethodRef constantInterfaceMethodRef)
        {
            constantInterfaceMethodRefs.Add(constantInterfaceMethodRef);
            router.Add(counter++, constantInterfaceMethodRefs.Count);
        }
        public void AddConstantInvokeDynamic(ConstantInvokeDynamic constantInvokeDynamic)
        {
            constantInvokeDynamics.Add(constantInvokeDynamic);
            router.Add(counter++, constantInvokeDynamics.Count);
        }
        public void AddConstantLong(ConstantLong  constantLong)
        {
            constantLongs.Add(constantLong);
            router.Add(counter++, constantLongs.Count);
        }
        public void AddConstantMethodHandle(ConstantMethodHandle constantMethodHandle)
        {
            constantMethodHandles.Add(constantMethodHandle);
            router.Add(counter++, constantMethodHandles.Count);
        }
        public void AddConstantMethodRef(ConstantMethodRef constantMethodRef)
        {
            constantMethodRefs.Add(constantMethodRef);
            router.Add(counter++, constantMethodRefs.Count);
        }
        public void AddConstantMethodType(ConstantMethodType constantMethodType)
        {
            constantMethodTypes.Add(constantMethodType);
            router.Add(counter++, constantMethodTypes.Count);
        }
        public void AddConstantModule(ConstantModule constantModule)
        {
            constantModules.Add(constantModule);
            router.Add(counter++, constantModules.Count);
        }
        public void AddConstantNameAndType(ConstantNameAndType constantNameAndType)
        {
            constantNameAndTypes.Add(constantNameAndType);
            router.Add(counter++, constantNameAndTypes.Count);
        }
        public void AddConstantPackage(ConstantPackage constantPackage)
        {
            constantPackages.Add(constantPackage);
            router.Add(counter++, constantPackages.Count);
        }
        public void AddConstantString(ConstantString constantString)
        {
            constantStrings.Add(constantString);
            router.Add(counter++, constantStrings.Count);
        }
        public void AddConstantUtf8(ConstantUtf8 constantUtf8)
        {
            constantUtf8s.Add(constantUtf8);
            router.Add(counter++, constantUtf8s.Count);
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
            router = new Dictionary<int, int>();
            constantClasses = new List<ConstantClass>();
            constantDoubles = new List<ConstantDouble>();
            constantFieldRefs = new List<ConstantFieldRef>();
            constantFloats = new List<ConstantFloat>();
            constantIntegers = new List<ConstantInteger>();
            constantInterfaceMethodRefs = new List<ConstantInterfaceMethodRef>();
            constantInvokeDynamics = new List<ConstantInvokeDynamic>();
            constantLongs = new List<ConstantLong>();
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
