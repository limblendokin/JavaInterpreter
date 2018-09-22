using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JavaInterpreter.ConstantsFolder
{
    class ConstantFloat
    {
        private float value;
        public float Value => value;

        public ConstantFloat(float value)
        {
            this.value = value;
        }
    }
}
