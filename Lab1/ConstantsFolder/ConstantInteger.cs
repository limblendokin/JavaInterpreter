﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JavaInterpreter.ConstantsFolder
{
    class ConstantInteger
    {
        private int value;
        public int Value => value;

        public ConstantInteger(int value)
        {
            this.value = value;
        }
    }
}
