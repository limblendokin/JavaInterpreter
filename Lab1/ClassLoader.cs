using System;
using System.Collections.Generic;

namespace JavaInterpreter
{
    public class ClassLoader
    {
        private string directory;
        private Dictionary<String, JavaClass> loadedClasses;
        public ClassLoader(String directory)
        {
            this.directory = directory;
            loadedClasses = new Dictionary<string, JavaClass>();
        }
        public JavaClass LoadClass(String className)
        {
            JavaClass jc;
            if (loadedClasses.TryGetValue(className, out jc))
                return jc;
            else
            {
                jc = JavaClassInitializer.ReadJavaClass(FileBytecodeReader.Read(directory + className));
                loadedClasses.Add(jc.ThisClassName, jc);
                return jc;
            }
        }
        //public SortedDictionary<string, JavaClass> LoadClasses()
        //{

        //}
    }
}
