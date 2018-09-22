using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lab1.ConstantsFolder;

namespace Lab1
{
    class DependenciesLoader
    {
        public void ResolveReferences(List<JavaClass> javaClasses)
        {
            List<Constant> cp;
            ConstantClass constClass;
            String name;
            Dictionary<String, JavaClass> resolvedReferences = new Dictionary<string, JavaClass>();
            foreach(JavaClass jc in javaClasses)
            {
                cp = jc.ConstantPool.cp;
                foreach(Constant c in cp)
                {
                    if(c is ConstantClass)
                    {
                        constClass = (ConstantClass) c;
                        constClass.NameIndex
                    }
                }
            }
        }
        
    }
}
