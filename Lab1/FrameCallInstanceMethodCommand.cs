using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JavaInterpreter
{
    class FrameCallInstanceMethodCommand : IFrameCommand
    {
        private Machine machine;
        private ObjectReference objReference;
        private String className;
        private String methodName;
        private List<Object> pushedArgs;
        public FrameCallInstanceMethodCommand(Machine machine, ObjectReference objReference, String className, String methodName, List<Object> pushedArgs)
        {
            this.machine = machine;
            this.objReference = objReference;
            this.className = className;
            this.methodName = methodName;
            this.pushedArgs = pushedArgs;
        }

        public void Execute()
        {
            machine.CallMethod(objReference, className, methodName, pushedArgs);
        }
    }
}
