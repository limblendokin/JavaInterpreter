using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab1.ConstantsFolder
{
    class Constant
    {
        protected byte tag;
        public byte Tag
        {
            get
            {
                return tag;
            }
        }
        public Constant(byte tag)
        {
            this.tag = tag;
        }
        public static Constant Create(byte[] code, ref int curIndex)
        {
            return new Constant(code[curIndex++]);
        }
       
    }

    

}
