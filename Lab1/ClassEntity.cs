using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab1
{
    class ClassEntity
    {
        JavaClass thisClass;
        public Object[] fields;
        public String[] fieldDescriptors;
        public String[] fieldNames;
        public String thisClassName;
        public ClassEntity(JavaClass jc)
        {
            thisClass = jc;
            thisClassName = jc.ThisClassName;
            List<Object> fieldsList = new List<object>();
            List<String> fieldNamesList = new List<string>();
            List<String> fieldDescriptorsList = new List<string>();
            // init fields and save descriptors
            foreach(Field f in jc.Fields)
            {
                // check if static
                if (f.AccessFlags != 8)
                {
                    fieldNamesList.Add(jc.cp.getConstantUtf8(f.NameIndex).Value);
                    fieldDescriptorsList.Add(jc.cp.getConstantUtf8(f.DescriptorIndex).Value);
                    fieldsList.Add(f.Value);
                }
            }
            fields = fieldsList.ToArray();
            fieldNames = fieldNamesList.ToArray();
            fieldDescriptors = fieldDescriptorsList.ToArray();
        }
    }
}
