using System;
using System.Collections.Generic;

namespace Lab1
{
    class Message
    {
        public bool isVoid;
        public Object returnValue;
        public int classIndex;
        public int methodIndex;
        public List<Object> args;
        public ObjectReference objRef;
        private Message()
        {

        }
        
        public Message(Object returnValue)
        {
            this.returnValue = returnValue;
            isVoid = false;
            if (returnValue == null)
            {
                isVoid = true;
            }
        }
        public Message(int classIndex, int methodIndex, ObjectReference objRef, List<Object> args)
        {
            this.classIndex = classIndex;
            this.methodIndex = methodIndex;
            this.objRef = objRef;
            this.args = args;
            
        }
        public Message(int classIndex, int methodIndex, List<Object> args)
        {
            this.classIndex = classIndex;
            this.methodIndex = methodIndex;
            this.args = args;
        }
    }

}
