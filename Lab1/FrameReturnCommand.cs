using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JavaInterpreter
{
    class FrameReturnCommand : IFrameCommand
    {
        Object returnValue;
        Machine machine;
        public FrameReturnCommand(Machine machine, Object returnValue)
        {
            this.machine = machine;
            this.returnValue = returnValue;
        }
        public void Execute()
        {
            machine.Return(returnValue);
        }
        
    }
}
