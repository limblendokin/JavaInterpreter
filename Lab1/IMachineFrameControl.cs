using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JavaInterpreter
{
    interface IMachineFrameControl
    {
        void CallMethod(String className, String methodName, List<Object> pushedArgs);
        void CallMethod(ObjectReference objReference, String className, String methodName, List<Object> pushedArgs);
        Object Return(Object returnValue);
    }
}
