﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JavaInterpreter.ConstantsFolder
{
    class ConstantString
    {
        private ushort stringIndex;
        public ushort StringIndex => stringIndex;
        public ConstantString(ushort stringIndex)
        {
            this.stringIndex = stringIndex;
        }
    }
}
