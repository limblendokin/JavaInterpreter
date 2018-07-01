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
            field = null;
            String className = cp.getConstantUtf8(cp.getConstantClass(cp.getConstantFieldRef(constantIndex).ClassIndex).NameIndex).Value;
            String fieldName = cp.getConstantUtf8(cp.getConstantNameAndType(cp.getConstantFieldRef(constantIndex).NameAndTypeIndex).NameIndex).Value;
            foreach (JavaClass j in loadedClasses)
            {
                if (j.ThisClassName == className)
                {
                    foreach (Field f in j.Fields)
                    {
                        if (j.cp.getConstantUtf8(f.NameIndex).Value == fieldName)
                        {
                            field = f;
                            return true;
                        }
                    }
                }
            }
            return false;
        }
        public int getLoadedClassIndex(String className)
        {
            for (int i = 0; i<loadedClasses.Count; i++)
            {
                if(className == loadedClasses.ElementAt(i).ThisClassName)
                {
                    return i;
                }
            }
            return 0;
        }
        public int getLoadedClassMethodIndex(int classIndex, String methodName)
        {
            for(int i = 0; i < loadedClasses.ElementAt(classIndex).Methods.Count; i++)
            {
                if(loadedClasses.ElementAt(classIndex).Methods.ElementAt(i).ThisMethodName == methodName)
                {
                    return i;
                }
            }
            return 0;
        }
        //public ObjectReference addArray() { }
    }
}
