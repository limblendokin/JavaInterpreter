using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lab1.AttributesFolder;
using Lab1.ConstantsFolder;

namespace Lab1
{
    class Frame
    {
        private uint pc;
        Heap heap;
        private Object[] localVarArray;
        private Object[] opStack;
        private int opStackPointer;
        private ConstantPool cp;
        private byte[] code;
        private Message msg;
        public Object pushedValue;
        JavaClass currentClass;
        private Frame()
        {

        }
        public Frame(JavaClass javaClass, Heap h, AttributeCode codeAttribute)
        {
            this.heap = h;
            this.currentClass = javaClass;
            this.cp = javaClass.cp;
            opStackPointer = -1;
            this.code = codeAttribute.Code;
            localVarArray = new Object[codeAttribute.MaxLocals];
            opStack = new Object[codeAttribute.MaxStack];
        }
        public void pushArgs(Object value)
        {
            push(value);
        }
        public void pushArgs(List<Object> values)
        {
            // also should push this into local var 1
            int j = 0;
            for(int i = values.Count - 1; i >= 0; i--, j++)
            {
                addLocalVar(j, values.ElementAt(i));
            }
        }
        public Message run()
        {
            if (pushedValue != null)
            {
                push(pushedValue);
            }
            pushedValue = null;
            msg = null;
            while (this.pc < code.Length && msg==null)
            {
                NextCommand();
            }
            return msg;
        }
        private void addLocalVar(int index, Object value)
        {
            localVarArray[index] = value;
        }
        private Object getLocalVar(int index)
        {
            return localVarArray[index];
        }
        private void push(Object value)
        {
            opStack[++opStackPointer] = value;
        }
        private Object pop()
        {
            return opStack[opStackPointer--];
        }
        private Object getOperand(int length)
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
                    push(null);
                    break;
                // iconst_m1
                case 0x02:
                    push((int)-1);
                    break;
                // iconst_0
                case 0x03:
                    push((int)0);
                    break;
                // iconst_1
                case 0x04:
                    push((int)1);
                    break;
                // iconst_2
                case 0x05:
                    push((int)2);
                    break;
                // iconst_3
                case 0x06:
                    push((int)3);
                    break;
                // iconst_4
                case 0x07:
                    push((int)4);
                    break;
                // iconst_5
                case 0x08:
                    push((int)5);
                    break;
                // lconst_0
                case 0x09:
                    push((long)0);
                    break;
                // lconst_1
                case 0x0a:
                    push((long)1);
                    break;
                // fconst_0
                case 0x0b:
                    push((float)0);
                    break;
                // fconst_1
                case 0x0c:
                    push((float)1);
                    break;
                // fconst_2
                case 0x0d:
                    push((float)2);
                    break;
                // dconst_0
                case 0x0e:
                    push((double)0);
                    break;
                // dconst_1
                case 0x0f:
                    push((double)1);
                    break;
                // bipush
                case 0x10:
                    push((int)getOperand(1));
                    break;
                // sipush
                case 0x11:
                    push((int)getOperand(2));
                    break;
                // ldc
                // TODO: Class, MethodHandle, MethodType
                case 0x12:
                    tmp1 = (int)getOperand(1);
                    switch (cp.getConstant((int)tmp1).Tag)
                    {
                        //String
                        // TODO: check if implemented correctly 
                        case 0x08:
                            push(cp.getConstantUtf8(cp.getConstantString((int)tmp1).StringIndex).Value);
                            break;
                        // int 
                        case 0x03:
                            push(cp.getConstantInteger((int)tmp1).Value);
                            break;
                        // float
                        case 0x04:
                            push(cp.getConstantFloat((int)tmp1).Value);
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
                    push(null);
                    break;
                // iload
                case 0x15:
                    push((int)getLocalVar((int)getOperand(1)));
                    break;
                // lload
                case 0x16:
                    push((long)getLocalVar((int)getOperand(1)));
                    break;
                // fload
                case 0x17:
                    push((float)getLocalVar((int)getOperand(1)));
                    break;
                // dload
                case 0x18:
                    push((double)getLocalVar((int)getOperand(1)));
                    break;
                // aload
                case 0x19:
                    push(getLocalVar((int)getOperand(1)));
                    break;
                // iload_0
                case 0x1a:
                    push((int)getLocalVar(0));
                    break;
                // iload_1
                case 0x1b:
                    push((int)getLocalVar(1));
                    break;
                // iload_2
                case 0x1c:
                    push((int)getLocalVar(2));
                    break;
                // iload_3
                case 0x1d:
                    push((int)getLocalVar(3));
                    break;
                // lload_0
                case 0x1e:
                    push((long)getLocalVar(0));
                    break;
                // lload_1
                case 0x1f:
                    push((long)getLocalVar(1));
                    break;
                // lload_2
                case 0x20:
                    push((long)getLocalVar(2));
                    break;
                // lload_3
                case 0x21:
                    push((long)getLocalVar(3));
                    break;
                // fload_0
                case 0x22:
                    push((float)getLocalVar(0));
                    break;
                // fload_1
                case 0x23:
                    push((float)getLocalVar(1));
                    break;
                // fload_2
                case 0x24:
                    push((float)getLocalVar(2));
                    break;
                // fload_3
                case 0x25:
                    push((float)getLocalVar(3));
                    break;
                // dload_0
                case 0x26:
                    push((double)getLocalVar(0));
                    break;
                // dload_1
                case 0x27:
                    push((double)getLocalVar(1));
                    break;
                // dload_2
                case 0x28:
                    push((double)getLocalVar(2));
                    break;
                // dload_3
                case 0x29:
                    push((double)getLocalVar(3));
                    break;
                // aload_0
                case 0x2a:
                    push(getLocalVar(0));
                    break;
                // aload_1
                case 0x2b:
                    push(getLocalVar(1));
                    break;
                // aload_2
                case 0x2c:
                    push(getLocalVar(2));
                    break;
                // aload_3
                case 0x2d:
                    push(getLocalVar(3));
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
                    addLocalVar((int)getOperand(1), pop());
                    break;
                // lstore
                case 0x37:
                    addLocalVar((int)getOperand(1), pop());
                    break;
                // fstore
                case 0x38:
                    addLocalVar((int)getOperand(1), pop());
                    break;
                // dstore
                case 0x39:
                    addLocalVar((int)getOperand(1), pop());
                    break;
                // astore
                case 0x3a:
                    addLocalVar((int)getOperand(1), pop());
                    break;
                // istore_0
                case 0x3b:
                    addLocalVar(0, pop());
                    break;
                // istore_1
                case 0x3c:
                    addLocalVar(1, pop());
                    break;
                // istore_2
                case 0x3d:
                    addLocalVar(2, pop());
                    break;
                // istore_3
                case 0x3e:
                    addLocalVar(3, pop());
                    break;
                // lstore_0
                case 0x3f:
                    addLocalVar(0, pop());
                    break;
                // lstore_1
                case 0x40:
                    addLocalVar(1, pop());
                    break;
                // lstore_2
                case 0x41:
                    addLocalVar(2, pop());
                    break;
                // lstore_3
                case 0x42:
                    addLocalVar(3, pop());
                    break;
                // fstore_0
                case 0x43:
                    addLocalVar(0, pop());
                    break;
                // fstore_1
                case 0x44:
                    addLocalVar(1, pop());
                    break;
                // fstore_2
                case 0x45:
                    addLocalVar(2, pop());
                    break;
                // fstore_3
                case 0x46:
                    addLocalVar(3, pop());
                    break;
                // dstore_0
                case 0x47:
                    addLocalVar(0, pop());
                    break;
                // dstore_1
                case 0x48:
                    addLocalVar(1, pop());
                    break;
                // dstore_2
                case 0x49:
                    addLocalVar(2, pop());
                    break;
                // dstore_3
                case 0x4a:
                    addLocalVar(3, pop());
                    break;
                // astore_0
                case 0x4b:
                    addLocalVar(0, pop());
                    break;
                // astore_1
                case 0x4c:
                    addLocalVar(1, pop());
                    break;
                // astore_2
                case 0x4d:
                    addLocalVar(2, pop());
                    break;
                // astore_3
                case 0x4e:
                    addLocalVar(3, pop());
                    break;
                // TODO: implement storing after heap
                // lastore
                // fastore
                // dastore
                // aastore
                // bastore
                // castore
                // sastore

                // pop
                case 0x57:
                    pop();
                    break;
                // pop2
                case 0x58:
                    pop();
                    break;
                // dup
                case 0x59:
                    tmp1 = pop();
                    push(tmp1);
                    push(tmp1);
                    break;
                // dup_x1
                case 0x5a:
                    tmp1 = pop();
                    tmp2 = pop();
                    push(tmp1);
                    push(tmp2);
                    push(tmp1);
                    break;
                // dup_x2
                case 0x5b:
                    tmp1 = pop();
                    tmp2 = pop();
                    tmp3 = pop();
                    push(tmp1);
                    push(tmp3);
                    push(tmp2);
                    push(tmp1);
                    break;
                // dup2
                case 0x5c:
                    tmp1 = pop();
                    tmp2 = pop();
                    push(tmp2);
                    push(tmp1);
                    push(tmp2);
                    push(tmp1);
                    break;
                // dup2_x1
                case 0x5d:
                    tmp1 = pop();
                    tmp2 = pop();
                    tmp3 = pop();
                    push(tmp2);
                    push(tmp1);
                    push(tmp3);
                    push(tmp2);
                    push(tmp1);
                    break;
                // dup2_x2;
                case 0x5e:
                    tmp1 = pop();
                    tmp2 = pop();
                    tmp3 = pop();
                    tmp4 = pop();
                    push(tmp2);
                    push(tmp1);
                    push(tmp4);
                    push(tmp3);
                    push(tmp2);
                    push(tmp1);
                    break;
                // swap
                case 0x5f:
                    tmp1 = pop();
                    tmp2 = pop();
                    push(tmp1);
                    push(tmp2);
                    break;
                // iadd
                case 0x60:
                    push((int)pop() + (int)pop());
                    break;
                // ladd
                case 0x61:
                    push((long)pop() + (long)pop());
                    break;
                // fadd
                case 0x62:
                    push((float)pop() + (float)pop());
                    break;
                // dadd
                case 0x63:
                    push((double)pop() + (double)pop());
                    break;
                // isub
                case 0x64:
                    push(-(int)pop() + (int)pop());
                    break;
                // lsub
                case 0x65:
                    push(-(long)pop() + (long)pop());
                    break;
                // fsub
                case 0x66:
                    push(-(float)pop() + (float)pop());
                    break;
                // dsub
                case 0x67:
                    push(-(double)pop() + (float)pop());
                    break;
                // imul
                case 0x68:
                    push((int)pop() * (int)pop());
                    break;
                // lmul
                case 0x69:
                    push((long)pop() * (long)pop());
                    break;
                // fmul
                case 0x6a:
                    push((float)pop() * (float)pop());
                    break;
                // dmul
                case 0x6b:
                    push((double)pop() * (double)pop());
                    break;
                // idiv
                case 0x6c:
                    tmp1 = pop();
                    push((int)pop() / (int)tmp1);
                    break;
                // ldiv
                case 0x6d:
                    tmp1 = pop();
                    push((long)pop() / (long)tmp1);
                    break;
                // fdiv
                case 0x6e:
                    tmp1 = pop();
                    push((float)pop() / (float)tmp1);
                    break;
                // ddiv
                case 0x6f:
                    tmp1 = pop();
                    push((double)pop() / (double)tmp1);
                    break;
                // irem
                case 0x70:
                    tmp1 = pop();
                    push((int)pop() % (int)tmp1);
                    break;
                // lrem
                case 0x71:
                    tmp1 = pop();
                    push((long)pop() % (long)tmp1);
                    break;
                // frem
                case 0x72:
                    tmp1 = pop();
                    push((float)pop() % (float)tmp1);
                    break;
                // drem 
                case 0x73:
                    tmp1 = pop();
                    push((double)pop() % (double)tmp1);
                    break;
                // ineg
                case 0x74:
                    push(-(int)pop());
                    break;
                // lneg
                case 0x75:
                    push(-(long)pop());
                    break;
                // fneg
                case 0x76:
                    push(-(float)pop());
                    break;
                // dneg
                case 0x77:
                    push(-(double)pop());
                    break;
                // ishl
                case 0x78:
                    tmp1 = pop();
                    push((int)pop() << (int)tmp1);
                    break;
                // lshl
                case 0x79:
                    tmp1 = pop();
                    push((long)pop() << (int)pop());
                    break;
                // ishr
                case 0x7a:
                    tmp1 = pop();
                    push((int)pop() >> (int)tmp1);
                    break;
                // lshr
                case 0x7b:
                    tmp1 = pop();
                    push((long)pop() >> (int)pop());
                    break;
                // iushr
                case 0x7c:
                    tmp1 = pop();
                    push((int)((uint)pop() >> (int)pop()));
                    break;
                // lushr
                case 0x7d:
                    tmp1 = pop();
                    push((long)((ulong)pop() >> (int)pop()));
                    break;
                // iand
                case 0x7e:
                    push((int)pop() & (int)pop());
                    break;
                // land
                case 0x7f:
                    push((long)pop() & (long)pop());
                    break;
                // ior
                case 0x80:
                    push((int)pop() | (int)pop());
                    break;
                // lor
                case 0x81:
                    push((long)pop() | (long)pop());
                    break;
                // ixor
                case 0x82:
                    push((int)pop() ^ (int)pop());
                    break;
                // lxor
                case 0x83:
                    push((long)pop() ^ (long)pop());
                    break;
                // iinc 
                // TODO: check for sign
                case 0x84:
                    tmp1 = (int)getOperand(1);
                    addLocalVar((int)tmp1, (int)getLocalVar((int)tmp1) + (int)getOperand(1));
                    break;
                // i2l
                case 0x85:
                    push(Convert.ToInt64((int)pop()));
                    break;
                // i2f
                case 0x86:
                    push(Convert.ToSingle((int)pop()));
                    break;
                // i2d
                case 0x87:
                    push(Convert.ToDouble((int)pop()));
                    break;
                // l2i
                case 0x88:
                    push(Convert.ToInt32((long)pop()));
                    break;
                // l2f
                case 0x89:
                    push(Convert.ToSingle((long)pop()));
                    break;
                // l2d
                case 0x8a:
                    push(Convert.ToDouble((long)pop()));
                    break;
                // f2i
                case 0x8b:
                    push(Convert.ToInt32((float)pop()));
                    break;
                // f2l
                case 0x8c:
                    push(Convert.ToInt64((float)pop()));
                    break;
                // f2d
                case 0x8d:
                    push(Convert.ToDouble((float)pop()));
                    break;
                // d2i
                case 0x8e:
                    push(Convert.ToInt32((double)pop()));
                    break;
                // d2l
                case 0x8f:
                    push(Convert.ToInt64((double)pop()));
                    break;
                // d2f
                case 0x90:
                    push(Convert.ToSingle((double)pop()));
                    break;
                // i2b
                case 0x91:
                    push(Convert.ToByte((int)pop()));
                    break;
                // i2c
                case 0x92:
                    push(Convert.ToChar((int)pop()));
                    break;
                // i2s
                case 0x93:
                    push(Convert.ToInt16((int)pop()));
                    break;
                // lcmp
                case 0x94:
                    tmp1 = pop();
                    tmp2 = pop();
                    push((long)tmp1 > (long)tmp2 ? 1 : (long)tmp1 < (long)tmp2 ? -1 : 0);
                    break;
                // fcmpl
                case 0x95:
                    tmp1 = pop();
                    tmp2 = pop();
                    push((float)tmp1 > (float)tmp2 ? 1 : (float)tmp1 < (float)tmp2 ? -1 : 0);
                    break;
                // fcmpg
                case 0x96:
                    tmp1 = pop();
                    tmp2 = pop();
                    push((float)tmp1 > (float)tmp2 ? 1 : (float)tmp1 < (float)tmp2 ? -1 : 0);
                    break;
                // dcmpl
                case 0x97:
                    tmp1 = pop();
                    tmp2 = pop();
                    push((double)tmp1 > (double)tmp2 ? 1 : (double)tmp1 < (double)tmp2 ? -1 : 0);
                    break;
                // dcmpg
                case 0x98:
                    tmp1 = pop();
                    tmp2 = pop();
                    push((double)tmp1 > (double)tmp2 ? 1 : (double)tmp1 < (double)tmp2 ? -1 : 0);
                    break;
                // ifeq
                case 0x99:
                    if ((int)pop() == 0)
                    {
                        pc = ((uint)getOperand(1) << 8) | (uint)getOperand(1);
                    }
                    break;
                // ifne
                case 0x9a:
                    if ((int)pop() != 0)
                    {
                        pc = ((uint)getOperand(1) << 8) | (uint)getOperand(1);
                    }
                    break;
                // iflt
                case 0x9b:
                    if ((int)pop() < 0)
                    {
                        pc = ((uint)getOperand(1) << 8) | (uint)getOperand(1);
                    }
                    break;
                // ifge
                case 0x9c:
                    if ((int)pop() >= 0)
                    {
                        pc = ((uint)getOperand(1) << 8) | (uint)getOperand(1);
                    }
                    break;
                // ifgt
                case 0x9d:
                    if ((int)pop() > 0)
                    {
                        pc = ((uint)getOperand(1) << 8) | (uint)getOperand(1);
                    }
                    break;
                // ifle
                case 0x9e:
                    if ((int)pop() <= 0)
                    {
                        pc = ((uint)getOperand(1) << 8) | (uint)getOperand(1);
                    }
                    break;
                // if_icmpeq
                case 0x9f:
                    if ((int)pop() == (int)pop())
                    {
                        pc = ((uint)getOperand(1) << 8) | (uint)getOperand(1);
                    }
                    break;
                // if_icmpne
                case 0xa0:
                    if ((int)pop() != (int)pop())
                    {
                        pc = ((uint)getOperand(1) << 8) | (uint)getOperand(1);
                    }
                    break;
                // if_icmplt
                case 0xa1:
                    if ((int)pop() < (int)pop())
                    {
                        pc = ((uint)getOperand(1) << 8) | (uint)getOperand(1);
                    }
                    break;
                // if_icmpge
                case 0xa2:
                    if ((int)pop() >= (int)pop())
                    {
                        pc = ((uint)getOperand(1) << 8) | (uint)getOperand(1);
                    }
                    break;
                // if_icmpgt
                case 0xa3:
                    if ((int)pop() > (int)pop())
                    {
                        pc = ((uint)getOperand(1) << 8) | (uint)getOperand(1);
                    }
                    break;
                // if_icmple
                case 0xa4:
                    if ((int)pop() <= (int)pop())
                    {
                        pc = ((uint)getOperand(1) << 8) | (uint)getOperand(1);
                    }
                    break;

                // TODO: check reference type
                // if_acmpeq
                case 0xa5:
                    if ((uint)pop() == (uint)pop())
                    {
                        pc = ((uint)getOperand(1) << 8) | (uint)getOperand(1);
                    }
                    break;
                // if_acmpne
                case 0xa6:
                    if ((uint)pop() != (uint)pop())
                    {
                        pc = ((uint)getOperand(1) << 8) | (uint)getOperand(1);
                    }
                    break;
                // goto
                case 0xa7:
                    pc = ((uint)getOperand(1) << 8) | (uint)getOperand(1);
                    break;
                // jsr
                case 0xa8:
                    push((uint)pc);
                    pc = ((uint)getOperand(1) << 8) | (uint)getOperand(1);
                    break;
                // ret
                case 0xa9:
                    pc = (uint)getLocalVar((int)getOperand(1));
                    break;

                //TODO: understand documentation
                // tableswitch
                // lookupswitch

                // ireturn
                case 0xac:
                    msg = new Message(pop());
                    break;
                // lreturn
                case 0xad:
                    msg = new Message(pop());
                    break;
                // freturn
                case 0xae:
                    msg = new Message(pop());
                    break;
                // dreturn
                case 0xaf:
                    msg = new Message(pop());
                    break;
                // areturn
                case 0xb0:
                    msg = new Message(pop());
                    break;
                // return
                case 0xb1:
                    msg = new Message(null);
                    break;
                // getstatic
                case 0xb2:
                    if (heap.tryFindStaticField(cp, (int)getOperand(2), out field))
                    {
                        push(field.Value);
                    }
                    else
                    {
                        Console.WriteLine("putstatic: static field not found");
                    }
                    break;
                // putstatic
                case 0xb3:
                    if (heap.tryFindStaticField(cp, (int)getOperand(2), out field))
                    {
                        field.Value = pop();
                    }
                    else
                    {
                        Console.WriteLine("putstatic: static field not found");
                    }
                    break;
                // getfield
                case 0xb4:
                    cfr = cp.getConstantFieldRef((int)getOperand(2));
                    str = cp.getConstantUtf8(cp.getConstantClass(cfr.ClassIndex).NameIndex).Value;
                    classInstance = heap.GetInstance((ObjectReference)pop());
                    for (int i = 0; i < classInstance.fieldNames.Length; i++)
                    {
                        if (classInstance.fieldNames[i] == str)
                        {
                            push(classInstance.fields[i]);
                            break;
                        }
                    }
                    break;
                // putfield
                case 0xb5:
                    tmp1 = pop();
                    cfr = cp.getConstantFieldRef((int)getOperand(2));
                    str = cp.getConstantUtf8(cp.getConstantClass(cfr.ClassIndex).NameIndex).Value;
                    classInstance = heap.GetInstance((ObjectReference)pop());
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
                    cmr = cp.getConstantMethodRef((int)getOperand(2));
                    str = resolveClassName(cmr);
                    tmp1 = heap.getLoadedClassIndex(str);
                    tmp2 = heap.getLoadedClassMethodIndex((int)tmp1, resolveMethodName(cmr));
                    msg = new Message((int)tmp1, (int)tmp2, getArgs((int)tmp1, (int)tmp2));
                    break;
                // invokestatic
                case 0xb8:
                    cmr = cp.getConstantMethodRef((int)getOperand(2));
                    str = resolveClassName(cmr);
                    tmp1 = heap.getLoadedClassIndex(str);
                    tmp2 = heap.getLoadedClassMethodIndex((int)tmp1, resolveMethodName(cmr));
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
                    push(heap.addObject(cp.getConstantClass((int)getOperand(2)), cp));
                    break;
                // newarray
                case 0xbc:
                    push(heap.addArray((int)getOperand(1), (int)pop()));
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
            return cp.getConstantUtf8(cp.getConstantClass(constantMethodRef.ClassIndex).NameIndex).Value;
        }
        private String resolveMethodName(ConstantMethodRef constantMethodRef)
        {
            return cp.getConstantUtf8(cp.getConstantNameAndType(constantMethodRef.NameAndTypeIndex).NameIndex).Value;
        }
        private List<Object> getArgs(int classIndex, int methodIndex)
        {
            String methodArgs = cp.getConstantUtf8(heap.loadedClasses.ElementAt(classIndex).Methods.ElementAt(methodIndex).Descriptor_index).Value;
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
                list.Add(pop());
            }
            return list;
        }
    }
}