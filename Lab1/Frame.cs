using System;
using System.Collections.Generic;
using System.Linq;
using JavaInterpreter.AttributesFolder;
using JavaInterpreter.ConstantsFolder;

namespace JavaInterpreter
{
    public class Frame
    {
        private uint pc;
        Heap heap;
        private Object[] localVarArray;
        private Stack<Object> operandStack;
        private ConstantPool cp;
        private JavaClass currentClass;
        public Frame(JavaClass javaClass, Heap h)
        {
            this.heap = h;
            this.currentClass = javaClass;
            this.cp = javaClass.ConstantPool;
            ushort maxLocals = (currentClass.Attributes.AttributesTable.Find(m=>m is AttributeCode) as AttributeCode).MaxLocals;
            localVarArray = new Object[maxLocals];
            ushort maxStack = (currentClass.Attributes.AttributesTable.Find(m => m is AttributeCode) as AttributeCode).MaxStack;
            operandStack = new Stack<object>(maxStack);
        }
        public void PushArgs(List<object> values)
        {
            // also should push this into local var 1
            int j = 0;
            for(int i = values.Count - 1; i >= 0; i--, j++)
            {
                AddLocalVar(j, values.ElementAt(i));
            }
        }
        public object Run(List<object> args)
        {
            if (args != null && args.Count > 0)
            {
                operandStack.Push(args);
            }
            while (this.pc < code.Length)
            {
                NextCommand();
            }
            return operandStack.Pop();
        }
        private void AddLocalVar(int index, Object value)
        {
            localVarArray[index] = value;
        }
        private Object GetLocalVar(int index)
        {
            return localVarArray[index];
        }
        private Object GetOperand(int length)
        {
            int value = code[pc];
            pc++;

            for (int i = 1; i<length; i++, pc++)
            {
                value = value * 256;
                value = value + code[pc];
            }
            return value;
        }
        public void NextCommand()
        {
            Object tmp1 = 0;
            Object tmp2 = 0;
            Object tmp3 = 0;
            Object tmp4 = 0;
            ConstantFieldRef cfr;
            ConstantMethodRef cmr;
            ClassEntity classInstance;
            List<Object> args = new List<object>();
            String str;
            Field field;
            // Выполнение инструкции по коду инструкции
            switch (code[pc++])
            {
                // nop
                case 0x00:
                    break;
                // aconst_null
                case 0x01:
                    operandStack.Push(null);
                    break;
                // iconst_m1
                case 0x02:
                    operandStack.Push((int)-1);
                    break;
                // iconst_0
                case 0x03:
                    operandStack.Push((int)0);
                    break;
                // iconst_1
                case 0x04:
                    operandStack.Push((int)1);
                    break;
                // iconst_2
                case 0x05:
                    operandStack.Push((int)2);
                    break;
                // iconst_3
                case 0x06:
                    operandStack.Push((int)3);
                    break;
                // iconst_4
                case 0x07:
                    operandStack.Push((int)4);
                    break;
                // iconst_5
                case 0x08:
                    operandStack.Push((int)5);
                    break;
                // lconst_0
                case 0x09:
                    operandStack.Push((long)0);
                    break;
                // lconst_1
                case 0x0a:
                    operandStack.Push((long)1);
                    break;
                // fconst_0
                case 0x0b:
                    operandStack.Push((float)0);
                    break;
                // fconst_1
                case 0x0c:
                    operandStack.Push((float)1);
                    break;
                // fconst_2
                case 0x0d:
                    operandStack.Push((float)2);
                    break;
                // dconst_0
                case 0x0e:
                    operandStack.Push((double)0);
                    break;
                // dconst_1
                case 0x0f:
                    operandStack.Push((double)1);
                    break;
                // bioperandStack.Push
                case 0x10:
                    operandStack.Push((int)GetOperand(1));
                    break;
                // sioperandStack.Push
                case 0x11:
                    operandStack.Push((int)GetOperand(2));
                    break;
                // ldc
                // TODO: Class, MethodHandle, MethodType
                case 0x12:
                    tmp1 = (int)GetOperand(1);
                    switch (cp.getConstant((int)tmp1).Tag)
                    {
                        //String
                        // TODO: check if implemented correctly 
                        case 0x08:
                            operandStack.Push(cp.GetConstantUtf8(cp.GetConstantString((int)tmp1).StringIndex).Value);
                            break;
                        // int 
                        case 0x03:
                            operandStack.Push(cp.GetConstantInteger((int)tmp1).Value);
                            break;
                        // float
                        case 0x04:
                            operandStack.Push(cp.GetConstantFloat((int)tmp1).Value);
                            break;
                        // Class
                        case 0x07:
                            break;
                        // methodType
                        case 0x10:
                            break;
                        // methodHanlde
                        case 0x0f:
                            break;
                    }
                    break;
                // ldc_w
                // TODO: ldc_w
                case 0x13:
                    break;
                // ldc2_w
                //TODO: ldc2_w
                case 0x14:
                    operandStack.Push(null);
                    break;
                // iload
                case 0x15:
                    operandStack.Push((int)GetLocalVar((int)GetOperand(1)));
                    break;
                // lload
                case 0x16:
                    operandStack.Push((long)GetLocalVar((int)GetOperand(1)));
                    break;
                // fload
                case 0x17:
                    operandStack.Push((float)GetLocalVar((int)GetOperand(1)));
                    break;
                // dload
                case 0x18:
                    operandStack.Push((double)GetLocalVar((int)GetOperand(1)));
                    break;
                // aload
                case 0x19:
                    operandStack.Push(GetLocalVar((int)GetOperand(1)));
                    break;
                // iload_0
                case 0x1a:
                    operandStack.Push((int)GetLocalVar(0));
                    break;
                // iload_1
                case 0x1b:
                    operandStack.Push((int)GetLocalVar(1));
                    break;
                // iload_2
                case 0x1c:
                    operandStack.Push((int)GetLocalVar(2));
                    break;
                // iload_3
                case 0x1d:
                    operandStack.Push((int)GetLocalVar(3));
                    break;
                // lload_0
                case 0x1e:
                    operandStack.Push((long)GetLocalVar(0));
                    break;
                // lload_1
                case 0x1f:
                    operandStack.Push((long)GetLocalVar(1));
                    break;
                // lload_2
                case 0x20:
                    operandStack.Push((long)GetLocalVar(2));
                    break;
                // lload_3
                case 0x21:
                    operandStack.Push((long)GetLocalVar(3));
                    break;
                // fload_0
                case 0x22:
                    operandStack.Push((float)GetLocalVar(0));
                    break;
                // fload_1
                case 0x23:
                    operandStack.Push((float)GetLocalVar(1));
                    break;
                // fload_2
                case 0x24:
                    operandStack.Push((float)GetLocalVar(2));
                    break;
                // fload_3
                case 0x25:
                    operandStack.Push((float)GetLocalVar(3));
                    break;
                // dload_0
                case 0x26:
                    operandStack.Push((double)GetLocalVar(0));
                    break;
                // dload_1
                case 0x27:
                    operandStack.Push((double)GetLocalVar(1));
                    break;
                // dload_2
                case 0x28:
                    operandStack.Push((double)GetLocalVar(2));
                    break;
                // dload_3
                case 0x29:
                    operandStack.Push((double)GetLocalVar(3));
                    break;
                // aload_0
                case 0x2a:
                    operandStack.Push(GetLocalVar(0));
                    break;
                // aload_1
                case 0x2b:
                    operandStack.Push(GetLocalVar(1));
                    break;
                // aload_2
                case 0x2c:
                    operandStack.Push(GetLocalVar(2));
                    break;
                // aload_3
                case 0x2d:
                    operandStack.Push(GetLocalVar(3));
                    break;
                // iaload
                // TODO: arrayref in heap zone
                case 0x2e:
                    break;
                // laload
                // faload
                // daload
                // aaload
                // baload
                // caload
                // saload
                // istore
                case 0x36:
                    AddLocalVar((int)GetOperand(1), operandStack.Pop());
                    break;
                // lstore
                case 0x37:
                    AddLocalVar((int)GetOperand(1), operandStack.Pop());
                    break;
                // fstore
                case 0x38:
                    AddLocalVar((int)GetOperand(1), operandStack.Pop());
                    break;
                // dstore
                case 0x39:
                    AddLocalVar((int)GetOperand(1), operandStack.Pop());
                    break;
                // astore
                case 0x3a:
                    AddLocalVar((int)GetOperand(1), operandStack.Pop());
                    break;
                // istore_0
                case 0x3b:
                    AddLocalVar(0, operandStack.Pop());
                    break;
                // istore_1
                case 0x3c:
                    AddLocalVar(1, operandStack.Pop());
                    break;
                // istore_2
                case 0x3d:
                    AddLocalVar(2, operandStack.Pop());
                    break;
                // istore_3
                case 0x3e:
                    AddLocalVar(3, operandStack.Pop());
                    break;
                // lstore_0
                case 0x3f:
                    AddLocalVar(0, operandStack.Pop());
                    break;
                // lstore_1
                case 0x40:
                    AddLocalVar(1, operandStack.Pop());
                    break;
                // lstore_2
                case 0x41:
                    AddLocalVar(2, operandStack.Pop());
                    break;
                // lstore_3
                case 0x42:
                    AddLocalVar(3, operandStack.Pop());
                    break;
                // fstore_0
                case 0x43:
                    AddLocalVar(0, operandStack.Pop());
                    break;
                // fstore_1
                case 0x44:
                    AddLocalVar(1, operandStack.Pop());
                    break;
                // fstore_2
                case 0x45:
                    AddLocalVar(2, operandStack.Pop());
                    break;
                // fstore_3
                case 0x46:
                    AddLocalVar(3, operandStack.Pop());
                    break;
                // dstore_0
                case 0x47:
                    AddLocalVar(0, operandStack.Pop());
                    break;
                // dstore_1
                case 0x48:
                    AddLocalVar(1, operandStack.Pop());
                    break;
                // dstore_2
                case 0x49:
                    AddLocalVar(2, operandStack.Pop());
                    break;
                // dstore_3
                case 0x4a:
                    AddLocalVar(3, operandStack.Pop());
                    break;
                // astore_0
                case 0x4b:
                    AddLocalVar(0, operandStack.Pop());
                    break;
                // astore_1
                case 0x4c:
                    AddLocalVar(1, operandStack.Pop());
                    break;
                // astore_2
                case 0x4d:
                    AddLocalVar(2, operandStack.Pop());
                    break;
                // astore_3
                case 0x4e:
                    AddLocalVar(3, operandStack.Pop());
                    break;
                // TODO: implement storing after heap
                // lastore
                // fastore
                // dastore
                // aastore
                // bastore
                // castore
                // sastore

                // operandStack.Pop
                case 0x57:
                    operandStack.Pop();
                    break;
                // operandStack.Pop2
                case 0x58:
                    operandStack.Pop();
                    break;
                // dup
                case 0x59:
                    tmp1 = operandStack.Pop();
                    operandStack.Push(tmp1);
                    operandStack.Push(tmp1);
                    break;
                // dup_x1
                case 0x5a:
                    tmp1 = operandStack.Pop();
                    tmp2 = operandStack.Pop();
                    operandStack.Push(tmp1);
                    operandStack.Push(tmp2);
                    operandStack.Push(tmp1);
                    break;
                // dup_x2
                case 0x5b:
                    tmp1 = operandStack.Pop();
                    tmp2 = operandStack.Pop();
                    tmp3 = operandStack.Pop();
                    operandStack.Push(tmp1);
                    operandStack.Push(tmp3);
                    operandStack.Push(tmp2);
                    operandStack.Push(tmp1);
                    break;
                // dup2
                case 0x5c:
                    tmp1 = operandStack.Pop();
                    tmp2 = operandStack.Pop();
                    operandStack.Push(tmp2);
                    operandStack.Push(tmp1);
                    operandStack.Push(tmp2);
                    operandStack.Push(tmp1);
                    break;
                // dup2_x1
                case 0x5d:
                    tmp1 = operandStack.Pop();
                    tmp2 = operandStack.Pop();
                    tmp3 = operandStack.Pop();
                    operandStack.Push(tmp2);
                    operandStack.Push(tmp1);
                    operandStack.Push(tmp3);
                    operandStack.Push(tmp2);
                    operandStack.Push(tmp1);
                    break;
                // dup2_x2;
                case 0x5e:
                    tmp1 = operandStack.Pop();
                    tmp2 = operandStack.Pop();
                    tmp3 = operandStack.Pop();
                    tmp4 = operandStack.Pop();
                    operandStack.Push(tmp2);
                    operandStack.Push(tmp1);
                    operandStack.Push(tmp4);
                    operandStack.Push(tmp3);
                    operandStack.Push(tmp2);
                    operandStack.Push(tmp1);
                    break;
                // swap
                case 0x5f:
                    tmp1 = operandStack.Pop();
                    tmp2 = operandStack.Pop();
                    operandStack.Push(tmp1);
                    operandStack.Push(tmp2);
                    break;
                // iadd
                case 0x60:
                    operandStack.Push((int)operandStack.Pop() + (int)operandStack.Pop());
                    break;
                // ladd
                case 0x61:
                    operandStack.Push((long)operandStack.Pop() + (long)operandStack.Pop());
                    break;
                // fadd
                case 0x62:
                    operandStack.Push((float)operandStack.Pop() + (float)operandStack.Pop());
                    break;
                // dadd
                case 0x63:
                    operandStack.Push((double)operandStack.Pop() + (double)operandStack.Pop());
                    break;
                // isub
                case 0x64:
                    operandStack.Push(-(int)operandStack.Pop() + (int)operandStack.Pop());
                    break;
                // lsub
                case 0x65:
                    operandStack.Push(-(long)operandStack.Pop() + (long)operandStack.Pop());
                    break;
                // fsub
                case 0x66:
                    operandStack.Push(-(float)operandStack.Pop() + (float)operandStack.Pop());
                    break;
                // dsub
                case 0x67:
                    operandStack.Push(-(double)operandStack.Pop() + (float)operandStack.Pop());
                    break;
                // imul
                case 0x68:
                    operandStack.Push((int)operandStack.Pop() * (int)operandStack.Pop());
                    break;
                // lmul
                case 0x69:
                    operandStack.Push((long)operandStack.Pop() * (long)operandStack.Pop());
                    break;
                // fmul
                case 0x6a:
                    operandStack.Push((float)operandStack.Pop() * (float)operandStack.Pop());
                    break;
                // dmul
                case 0x6b:
                    operandStack.Push((double)operandStack.Pop() * (double)operandStack.Pop());
                    break;
                // idiv
                case 0x6c:
                    tmp1 = operandStack.Pop();
                    operandStack.Push((int)operandStack.Pop() / (int)tmp1);
                    break;
                // ldiv
                case 0x6d:
                    tmp1 = operandStack.Pop();
                    operandStack.Push((long)operandStack.Pop() / (long)tmp1);
                    break;
                // fdiv
                case 0x6e:
                    tmp1 = operandStack.Pop();
                    operandStack.Push((float)operandStack.Pop() / (float)tmp1);
                    break;
                // ddiv
                case 0x6f:
                    tmp1 = operandStack.Pop();
                    operandStack.Push((double)operandStack.Pop() / (double)tmp1);
                    break;
                // irem
                case 0x70:
                    tmp1 = operandStack.Pop();
                    operandStack.Push((int)operandStack.Pop() % (int)tmp1);
                    break;
                // lrem
                case 0x71:
                    tmp1 = operandStack.Pop();
                    operandStack.Push((long)operandStack.Pop() % (long)tmp1);
                    break;
                // frem
                case 0x72:
                    tmp1 = operandStack.Pop();
                    operandStack.Push((float)operandStack.Pop() % (float)tmp1);
                    break;
                // drem 
                case 0x73:
                    tmp1 = operandStack.Pop();
                    operandStack.Push((double)operandStack.Pop() % (double)tmp1);
                    break;
                // ineg
                case 0x74:
                    operandStack.Push(-(int)operandStack.Pop());
                    break;
                // lneg
                case 0x75:
                    operandStack.Push(-(long)operandStack.Pop());
                    break;
                // fneg
                case 0x76:
                    operandStack.Push(-(float)operandStack.Pop());
                    break;
                // dneg
                case 0x77:
                    operandStack.Push(-(double)operandStack.Pop());
                    break;
                // ishl
                case 0x78:
                    tmp1 = operandStack.Pop();
                    operandStack.Push((int)operandStack.Pop() << (int)tmp1);
                    break;
                // lshl
                case 0x79:
                    tmp1 = operandStack.Pop();
                    operandStack.Push((long)operandStack.Pop() << (int)operandStack.Pop());
                    break;
                // ishr
                case 0x7a:
                    tmp1 = operandStack.Pop();
                    operandStack.Push((int)operandStack.Pop() >> (int)tmp1);
                    break;
                // lshr
                case 0x7b:
                    tmp1 = operandStack.Pop();
                    operandStack.Push((long)operandStack.Pop() >> (int)operandStack.Pop());
                    break;
                // iushr
                case 0x7c:
                    tmp1 = operandStack.Pop();
                    operandStack.Push((int)((uint)operandStack.Pop() >> (int)operandStack.Pop()));
                    break;
                // lushr
                case 0x7d:
                    tmp1 = operandStack.Pop();
                    operandStack.Push((long)((ulong)operandStack.Pop() >> (int)operandStack.Pop()));
                    break;
                // iand
                case 0x7e:
                    operandStack.Push((int)operandStack.Pop() & (int)operandStack.Pop());
                    break;
                // land
                case 0x7f:
                    operandStack.Push((long)operandStack.Pop() & (long)operandStack.Pop());
                    break;
                // ior
                case 0x80:
                    operandStack.Push((int)operandStack.Pop() | (int)operandStack.Pop());
                    break;
                // lor
                case 0x81:
                    operandStack.Push((long)operandStack.Pop() | (long)operandStack.Pop());
                    break;
                // ixor
                case 0x82:
                    operandStack.Push((int)operandStack.Pop() ^ (int)operandStack.Pop());
                    break;
                // lxor
                case 0x83:
                    operandStack.Push((long)operandStack.Pop() ^ (long)operandStack.Pop());
                    break;
                // iinc 
                // TODO: check for sign
                case 0x84:
                    tmp1 = (int)GetOperand(1);
                    AddLocalVar((int)tmp1, (int)GetLocalVar((int)tmp1) + (int)GetOperand(1));
                    break;
                // i2l
                case 0x85:
                    operandStack.Push(Convert.ToInt64((int)operandStack.Pop()));
                    break;
                // i2f
                case 0x86:
                    operandStack.Push(Convert.ToSingle((int)operandStack.Pop()));
                    break;
                // i2d
                case 0x87:
                    operandStack.Push(Convert.ToDouble((int)operandStack.Pop()));
                    break;
                // l2i
                case 0x88:
                    operandStack.Push(Convert.ToInt32((long)operandStack.Pop()));
                    break;
                // l2f
                case 0x89:
                    operandStack.Push(Convert.ToSingle((long)operandStack.Pop()));
                    break;
                // l2d
                case 0x8a:
                    operandStack.Push(Convert.ToDouble((long)operandStack.Pop()));
                    break;
                // f2i
                case 0x8b:
                    operandStack.Push(Convert.ToInt32((float)operandStack.Pop()));
                    break;
                // f2l
                case 0x8c:
                    operandStack.Push(Convert.ToInt64((float)operandStack.Pop()));
                    break;
                // f2d
                case 0x8d:
                    operandStack.Push(Convert.ToDouble((float)operandStack.Pop()));
                    break;
                // d2i
                case 0x8e:
                    operandStack.Push(Convert.ToInt32((double)operandStack.Pop()));
                    break;
                // d2l
                case 0x8f:
                    operandStack.Push(Convert.ToInt64((double)operandStack.Pop()));
                    break;
                // d2f
                case 0x90:
                    operandStack.Push(Convert.ToSingle((double)operandStack.Pop()));
                    break;
                // i2b
                case 0x91:
                    operandStack.Push(Convert.ToByte((int)operandStack.Pop()));
                    break;
                // i2c
                case 0x92:
                    operandStack.Push(Convert.ToChar((int)operandStack.Pop()));
                    break;
                // i2s
                case 0x93:
                    operandStack.Push(Convert.ToInt16((int)operandStack.Pop()));
                    break;
                // lcmp
                case 0x94:
                    tmp1 = operandStack.Pop();
                    tmp2 = operandStack.Pop();
                    operandStack.Push((long)tmp1 > (long)tmp2 ? 1 : (long)tmp1 < (long)tmp2 ? -1 : 0);
                    break;
                // fcmpl
                case 0x95:
                    tmp1 = operandStack.Pop();
                    tmp2 = operandStack.Pop();
                    operandStack.Push((float)tmp1 > (float)tmp2 ? 1 : (float)tmp1 < (float)tmp2 ? -1 : 0);
                    break;
                // fcmpg
                case 0x96:
                    tmp1 = operandStack.Pop();
                    tmp2 = operandStack.Pop();
                    operandStack.Push((float)tmp1 > (float)tmp2 ? 1 : (float)tmp1 < (float)tmp2 ? -1 : 0);
                    break;
                // dcmpl
                case 0x97:
                    tmp1 = operandStack.Pop();
                    tmp2 = operandStack.Pop();
                    operandStack.Push((double)tmp1 > (double)tmp2 ? 1 : (double)tmp1 < (double)tmp2 ? -1 : 0);
                    break;
                // dcmpg
                case 0x98:
                    tmp1 = operandStack.Pop();
                    tmp2 = operandStack.Pop();
                    operandStack.Push((double)tmp1 > (double)tmp2 ? 1 : (double)tmp1 < (double)tmp2 ? -1 : 0);
                    break;
                // ifeq
                case 0x99:
                    if ((int)operandStack.Pop() == 0)
                    {
                        pc = ((uint)GetOperand(1) << 8) | (uint)GetOperand(1);
                    }
                    break;
                // ifne
                case 0x9a:
                    if ((int)operandStack.Pop() != 0)
                    {
                        pc = ((uint)GetOperand(1) << 8) | (uint)GetOperand(1);
                    }
                    break;
                // iflt
                case 0x9b:
                    if ((int)operandStack.Pop() < 0)
                    {
                        pc = ((uint)GetOperand(1) << 8) | (uint)GetOperand(1);
                    }
                    break;
                // ifge
                case 0x9c:
                    if ((int)operandStack.Pop() >= 0)
                    {
                        pc = ((uint)GetOperand(1) << 8) | (uint)GetOperand(1);
                    }
                    break;
                // ifgt
                case 0x9d:
                    if ((int)operandStack.Pop() > 0)
                    {
                        pc = ((uint)GetOperand(1) << 8) | (uint)GetOperand(1);
                    }
                    break;
                // ifle
                case 0x9e:
                    if ((int)operandStack.Pop() <= 0)
                    {
                        pc = ((uint)GetOperand(1) << 8) | (uint)GetOperand(1);
                    }
                    break;
                // if_icmpeq
                case 0x9f:
                    if ((int)operandStack.Pop() == (int)operandStack.Pop())
                    {
                        pc = ((uint)GetOperand(1) << 8) | (uint)GetOperand(1);
                    }
                    break;
                // if_icmpne
                case 0xa0:
                    if ((int)operandStack.Pop() != (int)operandStack.Pop())
                    {
                        pc = ((uint)GetOperand(1) << 8) | (uint)GetOperand(1);
                    }
                    break;
                // if_icmplt
                case 0xa1:
                    if ((int)operandStack.Pop() < (int)operandStack.Pop())
                    {
                        pc = ((uint)GetOperand(1) << 8) | (uint)GetOperand(1);
                    }
                    break;
                // if_icmpge
                case 0xa2:
                    if ((int)operandStack.Pop() >= (int)operandStack.Pop())
                    {
                        pc = ((uint)GetOperand(1) << 8) | (uint)GetOperand(1);
                    }
                    break;
                // if_icmpgt
                case 0xa3:
                    if ((int)operandStack.Pop() > (int)operandStack.Pop())
                    {
                        pc = ((uint)GetOperand(1) << 8) | (uint)GetOperand(1);
                    }
                    break;
                // if_icmple
                case 0xa4:
                    if ((int)operandStack.Pop() <= (int)operandStack.Pop())
                    {
                        pc = ((uint)GetOperand(1) << 8) | (uint)GetOperand(1);
                    }
                    break;

                // TODO: check reference type
                // if_acmpeq
                case 0xa5:
                    if ((uint)operandStack.Pop() == (uint)operandStack.Pop())
                    {
                        pc = ((uint)GetOperand(1) << 8) | (uint)GetOperand(1);
                    }
                    break;
                // if_acmpne
                case 0xa6:
                    if ((uint)operandStack.Pop() != (uint)operandStack.Pop())
                    {
                        pc = ((uint)GetOperand(1) << 8) | (uint)GetOperand(1);
                    }
                    break;
                // goto
                case 0xa7:
                    pc = ((uint)GetOperand(1) << 8) | (uint)GetOperand(1);
                    break;
                // jsr
                case 0xa8:
                    operandStack.Push((uint)pc);
                    pc = ((uint)GetOperand(1) << 8) | (uint)GetOperand(1);
                    break;
                // ret
                case 0xa9:
                    pc = (uint)GetLocalVar((int)GetOperand(1));
                    break;

                //TODO: understand documentation
                // tableswitch
                // lookupswitch

                // ireturn
                case 0xac:
                    msg = new Message(operandStack.Pop());
                    break;
                // lreturn
                case 0xad:
                    msg = new Message(operandStack.Pop());
                    break;
                // freturn
                case 0xae:
                    msg = new Message(operandStack.Pop());
                    break;
                // dreturn
                case 0xaf:
                    msg = new Message(operandStack.Pop());
                    break;
                // areturn
                case 0xb0:
                    msg = new Message(operandStack.Pop());
                    break;
                // return
                case 0xb1:
                    msg = new Message(null);
                    break;
                // getstatic
                case 0xb2:
                    if (heap.TryFindStaticField(cp, (int)GetOperand(2), out field))
                    {
                        operandStack.Push(field.Value);
                    }
                    else
                    {
                        Console.WriteLine("putstatic: static field not found");
                    }
                    break;
                // putstatic
                case 0xb3:
                    if (heap.TryFindStaticField(cp, (int)GetOperand(2), out field))
                    {
                        field.Value = operandStack.Pop();
                    }
                    else
                    {
                        Console.WriteLine("putstatic: static field not found");
                    }
                    break;
                // getfield
                case 0xb4:
                    cfr = cp.GetConstantFieldRef((int)GetOperand(2));
                    str = cp.GetConstantUtf8(cp.GetConstantClass(cfr.ClassIndex).NameIndex).Value;
                    classInstance = heap.GetInstance((ObjectReference)operandStack.Pop());
                    for (int i = 0; i < classInstance.fieldNames.Length; i++)
                    {
                        if (classInstance.fieldNames[i] == str)
                        {
                            operandStack.Push(classInstance.fields[i]);
                            break;
                        }
                    }
                    break;
                // putfield
                case 0xb5:
                    tmp1 = operandStack.Pop();
                    cfr = cp.GetConstantFieldRef((int)GetOperand(2));
                    str = cp.GetConstantUtf8(cp.GetConstantClass(cfr.ClassIndex).NameIndex).Value;
                    classInstance = heap.GetInstance((ObjectReference)operandStack.Pop());
                    for (int i = 0; i < classInstance.fieldNames.Length; i++)
                    {
                        if (classInstance.fieldNames.ElementAt(i) == str)
                        {
                            classInstance.fields[i] = tmp1;
                            break;
                        }
                    }
                    break;
                // invokevirtual
                case 0xb6:
                    break;
                // invokespecial
                case 0xb7:
                    cmr = cp.GetConstantMethodRef((int)GetOperand(2));
                    str = resolveClassName(cmr);
                    tmp1 = heap.GetLoadedClassIndex(str);
                    tmp2 = heap.GetLoadedClassMethodIndex((int)tmp1, resolveMethodName(cmr));
                    msg = new Message((int)tmp1, (int)tmp2, getArgs((int)tmp1, (int)tmp2));
                    break;
                // invokestatic
                case 0xb8:
                    cmr = cp.GetConstantMethodRef((int)GetOperand(2));
                    str = resolveClassName(cmr);
                    tmp1 = heap.GetLoadedClassIndex(str);
                    tmp2 = heap.GetLoadedClassMethodIndex((int)tmp1, resolveMethodName(cmr));
                    msg = new Message((int)tmp1, (int)tmp2, getArgs((int)tmp1, (int)tmp2));
                    break;
                // invokeinterface
                case 0xb9:
                    
                    break;
                // invokedynamic
                case 0xba:
                    Console.WriteLine("dynamic method invoked");
                    break;
                // new
                case 0xbb:
                    operandStack.Push(heap.AddObject(cp.GetConstantClass((int)GetOperand(2)), cp));
                    break;
                // newarray
                case 0xbc:
                    operandStack.Push(heap.addArray((int)GetOperand(1), (int)operandStack.Pop()));
                    break;
                // anewarray
                // arraylength
                // athrow
                // checkcast
                // instanceof
                // monitorenter
                // monitorexit
                // wide
                // multianewarray
                // ifnull
                // ifnonnull
                // goto_w
                // jsr_w
                // breakpoint



                default:
                    Console.WriteLine();
                    break;
            }
        }
        private String resolveClassName(ConstantMethodRef constantMethodRef)
        {
            return cp.GetConstantUtf8(cp.GetConstantClass(constantMethodRef.ClassIndex).NameIndex).Value;
        }
        private String resolveMethodName(ConstantMethodRef constantMethodRef)
        {
            return cp.GetConstantUtf8(cp.GetConstantNameAndType(constantMethodRef.NameAndTypeIndex).NameIndex).Value;
        }
        private List<Object> getArgs(int classIndex, int methodIndex)
        {
            String methodArgs = cp.GetConstantUtf8(heap.loadedClasses.ElementAt(classIndex).Methods.ElementAt(methodIndex).DescriptorIndex).Value;
            int argsCount = 0;
            for(int i = 1; i < methodArgs.Length; i++)
            {
                if(methodArgs[i] == 'L')
                {
                    argsCount++;
                    do
                    {
                        i++;
                    } while (methodArgs[i] != ';');
                }
                else if (methodArgs[i] == ')')
                {
                    break;
                }
                argsCount++;
            }
            List<object> list = new List<object>();
            for (int i = 0; i < argsCount; i++)
            {
                list.Add(operandStack.Pop());
            }
            return list;
        }
    }
}