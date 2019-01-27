﻿namespace JavaInterpreter
{
    public class ObjectReference
    {
        public enum type { ClassInstance, Array};
        private type orType;
        private int index;
        public int Index
        {
            get
            {
                return index;
            }
        }
        public ObjectReference(type t, int index)
        {
            orType = t;
            this.index = index;
        }
    }
}
