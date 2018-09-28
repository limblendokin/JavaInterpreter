using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JavaInterpreter
{
    class FrameCallStaticMethodCommand : IFrameCommand
    {
        private Machine machine;
        private String className;
        private String methodName;
        private List<Object> pushedArgs;
        public FrameCallStaticMethodCommand(Machine machine, String className, String methodName, List<Object> pushedArgs)
        {
            this.machine = machine;
            this.className = className;
            this.methodName = methodName;
            this.pushedArgs = pushedArgs;
        }
        public void Execute()
        {
            machine.CallMethod(className, methodName, pushedArgs);
        }
    }

}
