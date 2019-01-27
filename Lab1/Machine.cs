using System;
using System.Collections.Generic;
using System.Linq;
using JavaInterpreter.AttributesFolder;

namespace JavaInterpreter
{
    public class Machine
    {
        private Stack<Frame> frames;
        private int frameStackPointer;

        private Heap heap;

        private ClassLoader classLoader;
        private JavaClass currentClass;
        
        public Machine(string directory, string mainClassName)
        {
            // Считывание классов, встречающихся в пуле констант исходного *.class файла 
            ClassLoader classLoader = new ClassLoader(directory);
            currentClass = classLoader.LoadClass(mainClassName);
            this.classLoader = classLoader;
            frames = new Stack<Frame>();
            heap = new Heap();
        }
        
        public uint Run()
        {
            Frame frame = new Frame(currentClass, heap);
            object returnValue = frame.Run(null);
            return 0;
        }
    }
}
