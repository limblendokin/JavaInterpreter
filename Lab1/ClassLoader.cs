using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JavaInterpreter.ConstantsFolder;

namespace JavaInterpreter
{
    class ClassLoader
    {
        private Dictionary<String, JavaClass> loadedClasses;
        private String directory;
        public ClassLoader(String directory)
        {
            this.directory = directory;
        }
        public JavaClass LoadClass(String className)
        {
            JavaClass currentClass;
            if (loadedClasses.TryGetValue(className, out currentClass))
                return currentClass;
            else
            {
                BytecodeFileReader bfr = new BytecodeFileReader();
                currentClass = bfr.Read(directory + className);
                loadedClasses.Add(className, currentClass);
                return currentClass;
            }
        }
    }
}
