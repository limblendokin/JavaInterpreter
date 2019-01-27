using System;
using System.Collections.Generic;

namespace JavaInterpreter
{
    public class FrameReturn : IFrameState
    {
        private Machine machine;
        private Object returnValue;
        public FrameReturn(Machine machine, Object returnValue)
        {
            this.machine = machine;
            this.returnValue = returnValue;
        }
        public void Execute()
        {
            machine.FinishCurrentFrame();
            machine.PushArgs(new List<Object> { returnValue });
        }
    }
}
