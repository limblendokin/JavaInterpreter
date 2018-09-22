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
        
        public Machine(List<JavaClass> loadedClasses)
        {
            // Считывание классов, встречающихся в пуле констант исходного *.class файла 
            this.loadedClasses = loadedClasses;
            frames = new List<Frame>();
        }
        public uint run()
        {
            heap = new Heap(loadedClasses);
            AttributeCode codeAttribute;
            int frameStackPointer = frames.Count - 1;
            Message msg;
            // TODO: run <clinit> for all loaded classes
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
    }
}
