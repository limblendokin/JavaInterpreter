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
        public void AddConstant()
        {

        }
    }
}
