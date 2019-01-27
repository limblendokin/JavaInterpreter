using System;
using System.Collections.Generic;
using System.Linq;
using JavaInterpreter.ConstantsFolder;

namespace JavaInterpreter
{
    public class Heap
    {
        public List<ClassEntity> objects;
        public Heap()
        {
            objects = new List<ClassEntity>();
        }
        
        public ObjectReference AddObject(ConstantClass classContstant, ConstantPool cp)
        {
            foreach(JavaClass currentClass in loadedClasses)
            {
                if(currentClass.ThisClassName == cp.GetConstantUtf8(classContstant.NameIndex).Value)
                {
                    objects.Add(new ClassEntity(currentClass));
                    return new ObjectReference(ObjectReference.type.ClassInstance, objects.Count - 1);
                }
            }
            return null;
        }
        public ClassEntity GetInstance(ObjectReference objectReference)
        {
            return objects.ElementAt(objectReference.Index);
        }
        public bool TryFindStaticField(ConstantPool cp, int constantIndex, out Field field)
        {
            String className = cp.GetConstantUtf8(cp.GetConstantClass(cp.GetConstantFieldRef(constantIndex).ClassIndex).NameIndex).Value;
            String fieldName = cp.GetConstantUtf8(cp.GetConstantNameAndType(cp.GetConstantFieldRef(constantIndex).NameAndTypeIndex).NameIndex).Value;
            field = loadedClasses.First(x => x.ThisClassName == className).Fields.First(x => x.ThisFieldName == fieldName);
            if (field != null)
                return true;
            else
                return false;
        }
        public int GetLoadedClassIndex(String className)
        {
            return loadedClasses.FindIndex(x=>x.ThisClassName == className);
        }
        public int GetLoadedClassMethodIndex(int classIndex, String methodName)
        {
            return loadedClasses.ElementAt(classIndex).Methods.FindIndex(x => x.ThisMethodName == methodName);
        }
        //public ObjectReference addArray() { }
    }
}
