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
        private String directory;
        private String entryClassName;
        public ClassLoader(String directory, String entryClassName)
        {
            this.directory = directory;
            this.entryClassName = entryClassName;
        }
        public List<JavaClass> LoadClasses()
        {
            // TODO: check if correct and optimize
            byte[] bytecode;
            String cpClassName;
            List<JavaClass> loadedClasses = new List<JavaClass>();
            List<ConstantClass> ccl = new List<ConstantClass>();
            bool isClassAlreadyLoaded = true;
            JavaClass currentLoadedClass;
            BytecodeFileReader bfr = new BytecodeFileReader();
            JavaClass jc = bfr.Read(directory + entryClassName);
            loadedClasses.Add(jc);
            for (int j = 0; j < loadedClasses.Count; j++)
            {
                currentLoadedClass = loadedClasses.ElementAt(j);
                for (int i = 0; i < currentLoadedClass.ConstantPool.GetConstantClasses().Count; i++)
                {
                    ccl = currentLoadedClass.ConstantPool.GetConstantClasses();
                    cpClassName = currentLoadedClass.ConstantPool.getConstantUtf8(currentLoadedClass.ConstantPool.GetConstantClasses().ElementAt(i).NameIndex).Value;
                    if (cpClassName != "java/lang/Object" && cpClassName != "java/lang/System")
                    {
                        isClassAlreadyLoaded = false;
                        for (int k = 0; k < loadedClasses.Count; k++)
                        {
                            if (loadedClasses.ElementAt(k).ThisClassName == cpClassName)
                            {
                                isClassAlreadyLoaded = true;
                                break;
                            }
                        }
                    }
                    if (!isClassAlreadyLoaded)
                    {
                        jc = bfr.Read(directory + cpClassName);
                        loadedClasses.Add(jc);
                    }

                }
            }
            return loadedClasses;
        }
    }
}
