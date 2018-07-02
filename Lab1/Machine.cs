using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lab1.ConstantsFolder;
using Lab1.AttributesFolder;
using System.IO;

namespace Lab1
{
    class Machine
    {
        private List<Frame> frames;
        private int frameStackPointer;
        Heap heap;

        List<JavaClass> loadedClasses;
        JavaClass currentClass;

        private Machine()
        {

        }
        public Machine(String className)
        {
            // Считывание классов, встречающихся в пуле констант исходного *.class файла 
            loadedClasses = new List<JavaClass>();
            // 
            loadClasses(className);
            frames = new List<Frame>();
        }
        public void loadClasses(String className)
        {
            byte[] bytecode;
            String cpClassName;
            List<ConstantClass> ccl = new List<ConstantClass>();
            bool isClassAlreadyLoaded = true;
            JavaClass currentLoadedClass;
            if (tryGetBytecode(className, out bytecode))
            {
                loadedClasses.Add(JavaClass.Create(bytecode));
            }
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
                        for (int k= 0; k < loadedClasses.Count; k++)
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
                        if (tryGetBytecode(cpClassName, out bytecode))
                        {
                            loadedClasses.Add(JavaClass.Create(bytecode));
                            Console.WriteLine("Class " + cpClassName + " loaded");
                        }
                        else
                        {
                            Console.WriteLine("Class " + cpClassName + " not found");
                        }
                    }
                    
                }
            }
        }
        public uint run()
        {
            heap = new Heap(loadedClasses);
            AttributeCode codeAttribute;
            int frameStackPointer = frames.Count - 1;
            Message msg;
            // run <clinit> for all loaded classes
            currentClass = loadedClasses.First();
            
            if (currentClass.Methods.First(m => m.ThisMethodName == "main").TryGetCodeAttribute(out codeAttribute))
            {
                frames.Add(new Frame(currentClass, heap, codeAttribute));
                frameStackPointer++;
            }
                
                
            while (frameStackPointer >= 0)
            {
                msg = frames.Last().run();
                if(msg.returnValue!=null || msg.isVoid)
                {
                    frames.RemoveAt(frameStackPointer--);
                    if (frameStackPointer >= 0 && !msg.isVoid)
                    {
                        frames.Last().pushArgs(msg.returnValue);
                    }
                }
                else 
                {
                    if (loadedClasses.ElementAt(msg.classIndex).Methods.ElementAt(msg.methodIndex).TryGetCodeAttribute(out codeAttribute))
                    {
                        frames.Add(new Frame(loadedClasses.ElementAt(msg.classIndex), heap, codeAttribute));
                        frames.Last().pushArgs(msg.args);
                        frameStackPointer++;
                    }
                }
            }

            return 0;
        }
        

        private bool tryGetBytecode(String className, out byte[] bytecode)
        {
            String path = @"D:\Documents\Study\ЯиПП\" + className + ".class";
            if (File.Exists(path))
            {
                bytecode = File.ReadAllBytes(path);
                return true;
            }
            else
            {
                bytecode = null;
                return false;
            }
        }
    }
}
