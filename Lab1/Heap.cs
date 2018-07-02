using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lab1.ConstantsFolder;

namespace Lab1
{
    class Heap
    {
        public List<JavaClass> loadedClasses;
        public List<ClassEntity> classInstances;
        private List<Object> arrays;
        public Heap(List<JavaClass> loadedClasses)
        {
            classInstances = new List<ClassEntity>();
            arrays = new List<object>();
            this.loadedClasses = loadedClasses;
        }
        /*
        public ObjectReference addObject(JavaClass jc)
        {
            this.classInstances.Add(new ClassEntity(jc));
            return new ObjectReference(ObjectReference.type.Class, this.classInstances.Count);
        }
        */
        public ObjectReference addArray(int type, int count)
        {
            switch (type)
            {
                case 4:
                    arrays.Add(new bool[count]);
                    break;
                case 5:
                    arrays.Add(new char[count]);
                    break;
                case 6:
                    arrays.Add(new float[count]);
                    break;
                case 7:
                    arrays.Add(new double[count]);
                    break;
                case 8:
                    arrays.Add(new byte[count]);
                    break;
                case 9:
                    arrays.Add(new short[count]);
                    break;
                case 10:
                    arrays.Add(new int[count]);
                    break;
                case 11:
                    arrays.Add(new long[count]);
                    break;
            }
            return new ObjectReference(ObjectReference.type.Array, arrays.Count - 1);
        }
        public ObjectReference addObject(ConstantClass classContstant, ConstantPool cp)
        {
            foreach(JavaClass currentClass in loadedClasses)
            {
                if(currentClass.ThisClassName == cp.getConstantUtf8(classContstant.NameIndex).Value)
                {
                    classInstances.Add(new ClassEntity(currentClass));
                    return new ObjectReference(ObjectReference.type.ClassInstance, classInstances.Count - 1);
                }
            }
            return null;
        }
        public ClassEntity GetInstance(ObjectReference objectReference)
        {
            return classInstances.ElementAt(objectReference.Index);
        }
        public bool tryFindStaticField(ConstantPool cp, int constantIndex, out Field field)
        {
            String className = cp.getConstantUtf8(cp.getConstantClass(cp.getConstantFieldRef(constantIndex).ClassIndex).NameIndex).Value;
            String fieldName = cp.getConstantUtf8(cp.getConstantNameAndType(cp.getConstantFieldRef(constantIndex).NameAndTypeIndex).NameIndex).Value;
            field = loadedClasses.First(x => x.ThisClassName == className).Fields.First(x => x.ThisFieldName == fieldName);
            if (field != null)
                return true;
            else
                return false;
        }
        public int getLoadedClassIndex(String className)
        {
            return loadedClasses.FindIndex(x=>x.ThisClassName == className);
        }
        public int getLoadedClassMethodIndex(int classIndex, String methodName)
        {
            return loadedClasses.ElementAt(classIndex).Methods.FindIndex(x => x.ThisMethodName == methodName);
        }
        //public ObjectReference addArray() { }
    }
}
