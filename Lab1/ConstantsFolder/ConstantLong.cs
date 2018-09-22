using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab1.ConstantsFolder
{
    class ConstantLong
    {
        private long value;
        public long Value => value;

        public ConstantLong(long value)
        {
            this.value = value;
        } 
    }
}
