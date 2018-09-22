using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JavaInterpreter
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
                    fieldNamesList.Add(jc.ConstantPool.getConstantUtf8((int)f.NameIndex).Value);
                    fieldDescriptorsList.Add(jc.ConstantPool.getConstantUtf8((int)f.DescriptorIndex).Value);
                    fieldsList.Add(f.Value);
                }
            }
            fields = fieldsList.ToArray();
            fieldNames = fieldNamesList.ToArray();
            fieldDescriptors = fieldDescriptorsList.ToArray();
        }
    }
}
