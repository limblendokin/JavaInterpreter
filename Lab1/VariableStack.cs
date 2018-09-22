using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab1
{
    class OperandStack
    {
        private Object[] stack;
        private int stackPointer;
        private int maxLocals;
        public OperandStack(int maxLocals)
        {
            this.maxLocals = maxLocals;
            stackPointer = -1;
            stack = new Object[maxLocals];
        }
        public Object pop()
        {
            if (stackPointer < 0 || stackPointer > maxLocals)
                throw new IndexOutOfRangeException();
            else
                return stack[stackPointer--];
        }
        public void push(Object o)
        {
            if (stackPointer >= maxLocals)
                throw new IndexOutOfRangeException();
            else
                stack[++stackPointer] = o;
        }
    }
}
