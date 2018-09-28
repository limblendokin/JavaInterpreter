using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JavaInterpreter.ConstantsFolder;
using JavaInterpreter.AttributesFolder;
using System.IO;

namespace JavaInterpreter
{
    class Machine : IMachineFrameControl
    {
        private Stack<Frame> frames;
        Heap heap;
        private ClassLoader classLoader;
        JavaClass currentClass;
        
        public Machine()
        {
            frames = new Stack<Frame>();
        }

        public void CallMethod(string className, string methodName, List<object> pushedArgs)
        {
            currentClass = classLoader.LoadClass(className);

        }

        public void CallMethod(ObjectReference objReference, string className, string methodName, List<object> pushedArgs)
        {
            throw new NotImplementedException();
        }
        
        public object Return(object returnValue)
        {
            throw new NotImplementedException();
        }

        public uint run()
        {
            //heap = new Heap(loadedClasses);
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
