using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JavaInterpreter.AttributesFolder;
using JavaInterpreter.ConstantsFolder;

namespace JavaInterpreter
{
    class Frame
    {
        Heap heap;
        private Object[] localVarArray;
        private Stack operandStack;
        private ConstantPool cp;
        
        private IFrameCommand frameCommand;
        JavaClass currentClass;
        BytecodeReader reader;
        public Frame(JavaClass javaClass, Heap h, AttributeCode codeAttribute)
        {
            this.heap = h;
            this.currentClass = javaClass;
            this.cp = javaClass.ConstantPool;
            reader = new BytecodeReader(codeAttribute.Code);
            localVarArray = new Object[codeAttribute.MaxLocals];
            operandStack = new Stack(codeAttribute.MaxStack);
        }
        public void pushArgs(Object value)
        {
            operandStack.Push(value);
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
        public IFrameCommand run()
        {
            // pushArgs
            frameCommand = null;
            while (this.pc < code.Length && frameCommand==null)
            {
                NextCommand();
            }
            return frameCommand;
        }
        private void addLocalVar(int index, Object value)
        {
            localVarArray[index] = value;
        }
        private Object getLocalVar(int index)
        {
            return localVarArray[index];
        }
        public void NextCommand()
        {
            Dictionary<uint, Action> instructionSet = new Dictionary<uint, Action>();
            instructionSet.Add(0x00, ()=> { });
            //push null ref
            instructionSet.Add(0x01, ()=> operandStack.Push(null));
            instructionSet.Add(0x02, ()=> operandStack.Push((int)-1));
            instructionSet.Add(0x03, () => operandStack.Push((int)0));
            instructionSet.Add(0x04, () => operandStack.Push((int)1));
            instructionSet.Add(0x05, () => operandStack.Push((int)2));
            instructionSet.Add(0x06, () => operandStack.Push((int)3));
            instructionSet.Add(0x07, () => operandStack.Push((int)4));
            instructionSet.Add(0x08, () => operandStack.Push((int)5));
            instructionSet.Add(0x09, () => operandStack.Push((long)0));
            instructionSet.Add(0x0a, () => operandStack.Push((long)1));
            instructionSet.Add(0x0b, () => operandStack.Push((float)0));
            instructionSet.Add(0x0c, () => operandStack.Push((float)1));
            instructionSet.Add(0x0d, () => operandStack.Push((float)2));
            instructionSet.Add(0x0e, () => operandStack.Push((double)0));
            instructionSet.Add(0x0f, () => operandStack.Push((double)1));
            instructionSet.Add(0x10, () => operandStack.Push(reader.ReadByte()));
            instructionSet.Add(0x11, () => operandStack.Push(reader.ReadShort()));
            // ldc
            instructionSet.Add(0x12, () => );
            //ldc_w
            instructionSet.Add(0x13, () =>);
            //ldc2_w
            instructionSet.Add(0x14, () =>);
            instructionSet.Add(0x15, () =>operandStack.Push((int)getLocalVar(reader.ReadInt())));
            instructionSet.Add(0x16, () => operandStack.Push((long)getLocalVar(reader.ReadInt())));
            instructionSet.Add(0x17, () => operandStack.Push((float)getLocalVar(reader.ReadInt())));
            instructionSet.Add(0x18, () => operandStack.Push((double)getLocalVar(reader.ReadInt())));
            //aload
            instructionSet.Add(0x19, () => operandStack.Push((int)getLocalVar(reader.ReadInt())));
            instructionSet.Add(0x1a, () => operandStack.Push((int)getLocalVar(0)));
            instructionSet.Add(0x1b, () => operandStack.Push((int)getLocalVar(1)));
            instructionSet.Add(0x1c, () => operandStack.Push((int)getLocalVar(2)));
            instructionSet.Add(0x1d, () => operandStack.Push((int)getLocalVar(4)));
            instructionSet.Add(0x1e, () => operandStack.Push((long)getLocalVar(0)));
            instructionSet.Add(0x1f, () => operandStack.Push((long)getLocalVar(1)));
            instructionSet.Add(0x20, () => operandStack.Push((long)getLocalVar(2)));
            instructionSet.Add(0x21, () => operandStack.Push((long)getLocalVar(3)));
            instructionSet.Add(0x22, () => operandStack.Push((float)getLocalVar(0)));
            instructionSet.Add(0x23, () => operandStack.Push((float)getLocalVar(1)));
            instructionSet.Add(0x24, () => operandStack.Push((float)getLocalVar(2)));
            instructionSet.Add(0x25, () => operandStack.Push((float)getLocalVar(3)));
            instructionSet.Add(0x26, () => operandStack.Push((double)getLocalVar(0)));
            instructionSet.Add(0x27, () => operandStack.Push((double)getLocalVar(1)));
            instructionSet.Add(0x28, () => operandStack.Push((double)getLocalVar(2)));
            instructionSet.Add(0x29, () => operandStack.Push((double)getLocalVar(3)));
            //aload 4 times
            instructionSet.Add(0x2a, () => );
            instructionSet.Add(0x2b, () =>);
            instructionSet.Add(0x2c, () =>);
            instructionSet.Add(0x2d, () =>);
            instructionSet.Add(0x2e, () =>);
            instructionSet.Add(0x2f, () =>);
            instructionSet.Add(0x30, () =>);
            instructionSet.Add(0x31, () =>);
            instructionSet.Add(0x32, () =>);
            instructionSet.Add(0x33, () =>);
            instructionSet.Add(0x34, () =>);
            instructionSet.Add(0x35, () =>);
            instructionSet.Add(0x36, () => addLocalVar(reader.ReadInt(), operandStack.Pop()));
            instructionSet.Add(0x37, () => addLocalVar(reader.ReadInt(), operandStack.Pop()));
            instructionSet.Add(0x38, () => addLocalVar(reader.ReadInt(), operandStack.Pop()));
            instructionSet.Add(0x39, () => addLocalVar(reader.ReadInt(), operandStack.Pop()));
            // astore
            instructionSet.Add(0x3a, () =>);
            instructionSet.Add(0x3b, () => addLocalVar(0, operandStack.Pop()));
            instructionSet.Add(0x3c, () => addLocalVar(1, operandStack.Pop()));
            instructionSet.Add(0x3d, () => addLocalVar(2, operandStack.Pop()));
            instructionSet.Add(0x3e, () => addLocalVar(3, operandStack.Pop()));
            instructionSet.Add(0x3f, () => addLocalVar(0, operandStack.Pop()));
            instructionSet.Add(0x40, () => addLocalVar(1, operandStack.Pop()));
            instructionSet.Add(0x41, () => addLocalVar(2, operandStack.Pop()));
            instructionSet.Add(0x42, () => addLocalVar(3, operandStack.Pop()));
            instructionSet.Add(0x43, () => addLocalVar(0, operandStack.Pop()));
            instructionSet.Add(0x44, () => addLocalVar(1, operandStack.Pop()));
            instructionSet.Add(0x45, () => addLocalVar(2, operandStack.Pop()));
            instructionSet.Add(0x46, () => addLocalVar(3, operandStack.Pop()));
            instructionSet.Add(0x47, () => addLocalVar(0, operandStack.Pop()));
            instructionSet.Add(0x48, () => addLocalVar(1, operandStack.Pop()));
            instructionSet.Add(0x49, () => addLocalVar(2, operandStack.Pop()));
            instructionSet.Add(0x4a, () => addLocalVar(3, operandStack.Pop()));
            // astore 3 times
            instructionSet.Add(0x4b, () =>);
            instructionSet.Add(0x4c, () =>);
            instructionSet.Add(0x4d, () =>);
            instructionSet.Add(0x4e, () =>);
            instructionSet.Add(0x4f, () =>);
            instructionSet.Add(0x50, () =>);
            instructionSet.Add(0x51, () =>);
            instructionSet.Add(0x52, () =>);
            instructionSet.Add(0x53, () =>);
            instructionSet.Add(0x54, () =>);
            instructionSet.Add(0x55, () =>);
            instructionSet.Add(0x56, () =>);
            instructionSet.Add(0x57, () => operandStack.Pop());
            instructionSet.Add(0x58, () => operandStack.Pop());
            instructionSet.Add(0x59, () => 
            {
                Object tmp = operandStack.Pop();
                operandStack.Push(tmp);
                operandStack.Push(tmp);
            });
            instructionSet.Add(0x5a, () =>
            {
                var tmp1 = operandStack.Pop();
                var tmp2 = operandStack.Pop();
                operandStack.Push(tmp1);
                operandStack.Push(tmp2);
                operandStack.Push(tmp1);
            });
            instructionSet.Add(0x5b, () =>
            {
                var tmp1 = operandStack.Pop();
                var tmp2 = operandStack.Pop();
                var tmp3 = operandStack.Pop();
                operandStack.Push(tmp1);
                operandStack.Push(tmp3);
                operandStack.Push(tmp2);
                operandStack.Push(tmp1);
            });
            instructionSet.Add(0x5c, () =>
            {
                var tmp1 = operandStack.Pop();
                var tmp2 = operandStack.Pop();
                operandStack.Push(tmp2);
                operandStack.Push(tmp1);
                operandStack.Push(tmp2);
                operandStack.Push(tmp1);
            });
            instructionSet.Add(0x5d, () =>
            {
                var tmp1 = operandStack.Pop();
                var tmp2 = operandStack.Pop();
                var tmp3 = operandStack.Pop();
                operandStack.Push(tmp2);
                operandStack.Push(tmp1);
                operandStack.Push(tmp3);
                operandStack.Push(tmp2);
                operandStack.Push(tmp1);
            });
            instructionSet.Add(0x5e, () =>
            {
                var tmp1 = operandStack.Pop();
                var tmp2 = operandStack.Pop();
                var tmp3 = operandStack.Pop();
                var tmp4 = operandStack.Pop();
                operandStack.Push(tmp2);
                operandStack.Push(tmp1);
                operandStack.Push(tmp4);
                operandStack.Push(tmp3);
                operandStack.Push(tmp2);
                operandStack.Push(tmp1);
            });
            instructionSet.Add(0x5f, () =>
            {
                var tmp1 = operandStack.Pop();
                var tmp2 = operandStack.Pop();
                operandStack.Push(tmp1);
                operandStack.Push(tmp2);
            });
            instructionSet.Add(0x60, () => operandStack.Push((int)operandStack.Pop()+(int)operandStack.Pop()));
            instructionSet.Add(0x61, () => operandStack.Push((long)operandStack.Pop() + (long)operandStack.Pop()));
            instructionSet.Add(0x62, () => operandStack.Push((float)operandStack.Pop() + (float)operandStack.Pop()));
            instructionSet.Add(0x63, () => operandStack.Push((double)operandStack.Pop() + (double)operandStack.Pop()));
            instructionSet.Add(0x64, () => operandStack.Push(-(int)operandStack.Pop() + (int)operandStack.Pop()));
            instructionSet.Add(0x65, () => operandStack.Push(-(long)operandStack.Pop() + (long)operandStack.Pop()));
            instructionSet.Add(0x66, () => operandStack.Push(-(float)operandStack.Pop() + (float)operandStack.Pop()));
            instructionSet.Add(0x67, () => operandStack.Push(-(double)operandStack.Pop() + (double)operandStack.Pop()));
            instructionSet.Add(0x68, () => operandStack.Push((int)operandStack.Pop() * (int)operandStack.Pop()));
            instructionSet.Add(0x69, () => operandStack.Push((long)operandStack.Pop() * (long)operandStack.Pop()));
            instructionSet.Add(0x6a, () => operandStack.Push((float)operandStack.Pop() * (float)operandStack.Pop()));
            instructionSet.Add(0x6b, () => operandStack.Push((double)operandStack.Pop() * (double)operandStack.Pop()));
            instructionSet.Add(0x6c, () =>
            {
                var tmp = operandStack.Pop();
                operandStack.Push((int)operandStack.Pop() / (int)tmp);
            });
            instructionSet.Add(0x6d, () =>
            {
                var tmp = operandStack.Pop();
                operandStack.Push((long)operandStack.Pop() / (long)tmp);
            });
            instructionSet.Add(0x6e, () =>
            {
                var tmp = operandStack.Pop();
                operandStack.Push((float)operandStack.Pop() / (float)tmp);
            });
            instructionSet.Add(0x6f, () =>
            {
                var tmp = operandStack.Pop();
                operandStack.Push((double)operandStack.Pop() / (double)tmp);
            });
            instructionSet.Add(0x70, () =>
            {
                var tmp = operandStack.Pop();
                operandStack.Push((int)operandStack.Pop() % (int)tmp);
            });
            instructionSet.Add(0x71, () =>
            {
                var tmp = operandStack.Pop();
                operandStack.Push((long)operandStack.Pop() % (long)tmp);
            });
            instructionSet.Add(0x72, () =>
            {
                var tmp = operandStack.Pop();
                operandStack.Push((float)operandStack.Pop() % (float)tmp);
            });
            instructionSet.Add(0x73, () =>
            {
                var tmp = operandStack.Pop();
                operandStack.Push((double)operandStack.Pop() % (double)tmp);
            });
            instructionSet.Add(0x74, () =>operandStack.Push(-(int)operandStack.Pop()));
            instructionSet.Add(0x75, () => operandStack.Push(-(long)operandStack.Pop()));
            instructionSet.Add(0x76, () => operandStack.Push(-(float)operandStack.Pop()));
            instructionSet.Add(0x77, () => operandStack.Push(-(double)operandStack.Pop()));
            instructionSet.Add(0x78, () =>
            {
                var tmp = operandStack.Pop();
                operandStack.Push((int)operandStack.Pop() << (int)tmp);
            });
            instructionSet.Add(0x79, () =>
            {
                var tmp = operandStack.Pop();
                operandStack.Push((long)operandStack.Pop() << (int)tmp);
            });
            instructionSet.Add(0x7a, () =>
            {
                var tmp = operandStack.Pop();
                operandStack.Push((int)operandStack.Pop() >> (int)tmp);
            });
            instructionSet.Add(0x7b, () =>
            {
                var tmp = operandStack.Pop();
                operandStack.Push((long)operandStack.Pop() >> (int)tmp);
            });
            instructionSet.Add(0x7c, () =>
            {
                var tmp = operandStack.Pop();
                operandStack.Push((int)((uint)operandStack.Pop() >> (int)tmp));
            });
            instructionSet.Add(0x7d, () =>
            {
                var tmp = operandStack.Pop();
                operandStack.Push((int)((ulong)operandStack.Pop() >> (int)tmp));
            });
            instructionSet.Add(0x7e, () =>operandStack.Push((int)operandStack.Pop()&(int)operandStack.Pop()));
            instructionSet.Add(0x7f, () => operandStack.Push((long)operandStack.Pop() & (long)operandStack.Pop()));
            instructionSet.Add(0x80, () => operandStack.Push((int)operandStack.Pop() | (int)operandStack.Pop()));
            instructionSet.Add(0x81, () => operandStack.Push((long)operandStack.Pop() | (long)operandStack.Pop()));
            instructionSet.Add(0x82, () => operandStack.Push((int)operandStack.Pop() ^ (int)operandStack.Pop()));
            instructionSet.Add(0x83, () => operandStack.Push((int)operandStack.Pop() ^ (int)operandStack.Pop()));
            // TODO: check for sign
            instructionSet.Add(0x84, () =>
            {
                var tmp = reader.ReadInt();
                addLocalVar(tmp, (int)getLocalVar((int)tmp) + reader.ReadInt());
            });
            instructionSet.Add(0x85, () => operandStack.Push(Convert.ToInt64((int)operandStack.Pop())));
            instructionSet.Add(0x86, () => operandStack.Push(Convert.ToSingle((int)operandStack.Pop())));
            instructionSet.Add(0x87, () => operandStack.Push(Convert.ToDouble((int)operandStack.Pop())));
            instructionSet.Add(0x88, () => operandStack.Push(Convert.ToInt32((long)operandStack.Pop())));
            instructionSet.Add(0x89, () => operandStack.Push(Convert.ToSingle((long)operandStack.Pop())));
            instructionSet.Add(0x8a, () => operandStack.Push(Convert.ToDouble((long)operandStack.Pop())));
            instructionSet.Add(0x8b, () => operandStack.Push(Convert.ToInt32((float)operandStack.Pop())));
            instructionSet.Add(0x8c, () => operandStack.Push(Convert.ToInt64((float)operandStack.Pop())));
            instructionSet.Add(0x8d, () => operandStack.Push(Convert.ToDouble((float)operandStack.Pop())));
            instructionSet.Add(0x8e, () => operandStack.Push(Convert.ToInt32((double)operandStack.Pop())));
            instructionSet.Add(0x8f, () => operandStack.Push(Convert.ToInt64((double)operandStack.Pop())));
            instructionSet.Add(0x90, () => operandStack.Push(Convert.ToSingle((double)operandStack.Pop())));
            instructionSet.Add(0x91, () => operandStack.Push(Convert.ToByte((int)operandStack.Pop())));
            instructionSet.Add(0x92, () => operandStack.Push(Convert.ToChar((int)operandStack.Pop())));
            instructionSet.Add(0x93, () => operandStack.Push(Convert.ToInt16((int)operandStack.Pop())));
            instructionSet.Add(0x94, () =>
            {
                var tmp1 = operandStack.Pop();
                var tmp2 = operandStack.Pop();
                operandStack.Push((long)tmp1 > (long)tmp2 ? 1 : (long)tmp1 < (long)tmp2 ? -1 : 0);
            });
            instructionSet.Add(0x95, () =>
            {
                var tmp1 = operandStack.Pop();
                var tmp2 = operandStack.Pop();
                operandStack.Push((float)tmp1 > (float)tmp2 ? 1 : (float)tmp1 < (float)tmp2 ? -1 : 0);
            });
            instructionSet.Add(0x96, () =>
            {
                var tmp1 = operandStack.Pop();
                var tmp2 = operandStack.Pop();
                operandStack.Push((float)tmp1 > (float)tmp2 ? 1 : (float)tmp1 < (float)tmp2 ? -1 : 0);
            });
            instructionSet.Add(0x97, () =>
            {
                var tmp1 = operandStack.Pop();
                var tmp2 = operandStack.Pop();
                operandStack.Push((double)tmp1 > (double)tmp2 ? 1 : (double)tmp1 < (double)tmp2 ? -1 : 0);
            });
            instructionSet.Add(0x98, () =>
            {
                var tmp1 = operandStack.Pop();
                var tmp2 = operandStack.Pop();
                operandStack.Push((double)tmp1 > (double)tmp2 ? 1 : (double)tmp1 < (double)tmp2 ? -1 : 0);
            });
            instructionSet.Add(0x99, () =>
            instructionSet.Add(0x9a, () =>
            instructionSet.Add(0x9b, () =>
            instructionSet.Add(0x9c, () =>
            instructionSet.Add(0x9d, () =>
            instructionSet.Add(0x9e, () =>
            instructionSet.Add(0x9f, () =>
            instructionSet.Add(0xa0, () =>
            instructionSet.Add(0xa1, () =>
            instructionSet.Add(0xa2, () =>
            instructionSet.Add(0xa3, () =>
            instructionSet.Add(0xa4, () =>
            instructionSet.Add(0xa5, () =>
            instructionSet.Add(0xa6, () =>
            instructionSet.Add(0xa7, () =>
            instructionSet.Add(0xa8, () =>
            instructionSet.Add(0xa9, () =>
            instructionSet.Add(0xaa, () =>
            instructionSet.Add(0xab, () =>
            instructionSet.Add(0xac, () =>
            instructionSet.Add(0xad, () =>
            instructionSet.Add(0xae, () =>
            instructionSet.Add(0xaf, () =>
            instructionSet.Add(0xb0, () =>
            instructionSet.Add(0xb1, () =>
            instructionSet.Add(0xb2, () =>
            instructionSet.Add(0xb3, () =>
            instructionSet.Add(0xb4, () =>
            instructionSet.Add(0xb5, () =>
            instructionSet.Add(0xb6, () =>
            instructionSet.Add(0xb7, () =>
            instructionSet.Add(0xb8, () =>
            instructionSet.Add(0xb9, () =>
            instructionSet.Add(0xba, () =>
            instructionSet.Add(0xbb, () =>
            instructionSet.Add(0xbc, () =>
            instructionSet.Add(0xbd, () =>
            instructionSet.Add(0xbe, () =>
            instructionSet.Add(0xbf, () =>
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
                // bipush
                case 0x10:
                    operandStack.Push((int)getOperand(1));
                    break;
                // sipush
                case 0x11:
                    operandStack.Push((int)getOperand(2));
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
                            operandStack.Push(cp.getConstantUtf8(cp.getConstantString((int)tmp1).StringIndex).Value);
                            break;
                        // int 
                        case 0x03:
                            operandStack.Push(cp.getConstantInteger((int)tmp1).Value);
                            break;
                        // float
                        case 0x04:
                            operandStack.Push(cp.getConstantFloat((int)tmp1).Value);
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
                    operandStack.Push((int)getLocalVar((int)getOperand(1)));
                    break;
                // lload
                case 0x16:
                    operandStack.Push((long)getLocalVar((int)getOperand(1)));
                    break;
                // fload
                case 0x17:
                    operandStack.Push((float)getLocalVar((int)getOperand(1)));
                    break;
                // dload
                case 0x18:
                    operandStack.Push((double)getLocalVar((int)getOperand(1)));
                    break;
                // aload
                case 0x19:
                    operandStack.Push(getLocalVar((int)getOperand(1)));
                    break;
                // iload_0
                case 0x1a:
                    operandStack.Push((int)getLocalVar(0));
                    break;
                // iload_1
                case 0x1b:
                    operandStack.Push((int)getLocalVar(1));
                    break;
                // iload_2
                case 0x1c:
                    operandStack.Push((int)getLocalVar(2));
                    break;
                // iload_3
                case 0x1d:
                    operandStack.Push((int)getLocalVar(3));
                    break;
                // lload_0
                case 0x1e:
                    operandStack.Push((long)getLocalVar(0));
                    break;
                // lload_1
                case 0x1f:
                    operandStack.Push((long)getLocalVar(1));
                    break;
                // lload_2
                case 0x20:
                    operandStack.Push((long)getLocalVar(2));
                    break;
                // lload_3
                case 0x21:
                    operandStack.Push((long)getLocalVar(3));
                    break;
                // fload_0
                case 0x22:
                    operandStack.Push((float)getLocalVar(0));
                    break;
                // fload_1
                case 0x23:
                    operandStack.Push((float)getLocalVar(1));
                    break;
                // fload_2
                case 0x24:
                    operandStack.Push((float)getLocalVar(2));
                    break;
                // fload_3
                case 0x25:
                    operandStack.Push((float)getLocalVar(3));
                    break;
                // dload_0
                case 0x26:
                    operandStack.Push((double)getLocalVar(0));
                    break;
                // dload_1
                case 0x27:
                    operandStack.Push((double)getLocalVar(1));
                    break;
                // dload_2
                case 0x28:
                    operandStack.Push((double)getLocalVar(2));
                    break;
                // dload_3
                case 0x29:
                    operandStack.Push((double)getLocalVar(3));
                    break;
                // aload_0
                case 0x2a:
                    operandStack.Push(getLocalVar(0));
                    break;
                // aload_1
                case 0x2b:
                    operandStack.Push(getLocalVar(1));
                    break;
                // aload_2
                case 0x2c:
                    operandStack.Push(getLocalVar(2));
                    break;
                // aload_3
                case 0x2d:
                    operandStack.Push(getLocalVar(3));
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
                    operandStack.Push(tmp1);
                    operandStack.Push(tmp1);
                    break;
                // dup_x1
                case 0x5a:
                    tmp1 = pop();
                    tmp2 = pop();
                    operandStack.Push(tmp1);
                    operandStack.Push(tmp2);
                    operandStack.Push(tmp1);
                    break;
                // dup_x2
                case 0x5b:
                    tmp1 = pop();
                    tmp2 = pop();
                    tmp3 = pop();
                    operandStack.Push(tmp1);
                    operandStack.Push(tmp3);
                    operandStack.Push(tmp2);
                    operandStack.Push(tmp1);
                    break;
                // dup2
                case 0x5c:
                    tmp1 = pop();
                    tmp2 = pop();
                    operandStack.Push(tmp2);
                    operandStack.Push(tmp1);
                    operandStack.Push(tmp2);
                    operandStack.Push(tmp1);
                    break;
                // dup2_x1
                case 0x5d:
                    tmp1 = pop();
                    tmp2 = pop();
                    tmp3 = pop();
                    operandStack.Push(tmp2);
                    operandStack.Push(tmp1);
                    operandStack.Push(tmp3);
                    operandStack.Push(tmp2);
                    operandStack.Push(tmp1);
                    break;
                // dup2_x2;
                case 0x5e:
                    tmp1 = pop();
                    tmp2 = pop();
                    tmp3 = pop();
                    tmp4 = pop();
                    operandStack.Push(tmp2);
                    operandStack.Push(tmp1);
                    operandStack.Push(tmp4);
                    operandStack.Push(tmp3);
                    operandStack.Push(tmp2);
                    operandStack.Push(tmp1);
                    break;
                // swap
                case 0x5f:
                    tmp1 = pop();
                    tmp2 = pop();
                    operandStack.Push(tmp1);
                    operandStack.Push(tmp2);
                    break;
                // iadd
                case 0x60:
                    operandStack.Push((int)pop() + (int)pop());
                    break;
                // ladd
                case 0x61:
                    operandStack.Push((long)pop() + (long)pop());
                    break;
                // fadd
                case 0x62:
                    operandStack.Push((float)pop() + (float)pop());
                    break;
                // dadd
                case 0x63:
                    operandStack.Push((double)pop() + (double)pop());
                    break;
                // isub
                case 0x64:
                    operandStack.Push(-(int)pop() + (int)pop());
                    break;
                // lsub
                case 0x65:
                    operandStack.Push(-(long)pop() + (long)pop());
                    break;
                // fsub
                case 0x66:
                    operandStack.Push(-(float)pop() + (float)pop());
                    break;
                // dsub
                case 0x67:
                    operandStack.Push(-(double)pop() + (double)pop());
                    break;
                // imul
                case 0x68:
                    operandStack.Push((int)pop() * (int)pop());
                    break;
                // lmul
                case 0x69:
                    operandStack.Push((long)pop() * (long)pop());
                    break;
                // fmul
                case 0x6a:
                    operandStack.Push((float)pop() * (float)pop());
                    break;
                // dmul
                case 0x6b:
                    operandStack.Push((double)pop() * (double)pop());
                    break;
                // idiv
                case 0x6c:
                    tmp1 = pop();
                    operandStack.Push((int)pop() / (int)tmp1);
                    break;
                // ldiv
                case 0x6d:
                    tmp1 = pop();
                    operandStack.Push((long)pop() / (long)tmp1);
                    break;
                // fdiv
                case 0x6e:
                    tmp1 = pop();
                    operandStack.Push((float)pop() / (float)tmp1);
                    break;
                // ddiv
                case 0x6f:
                    tmp1 = pop();
                    operandStack.Push((double)pop() / (double)tmp1);
                    break;
                // irem
                case 0x70:
                    tmp1 = pop();
                    operandStack.Push((int)pop() % (int)tmp1);
                    break;
                // lrem
                case 0x71:
                    tmp1 = pop();
                    operandStack.Push((long)pop() % (long)tmp1);
                    break;
                // frem
                case 0x72:
                    tmp1 = pop();
                    operandStack.Push((float)pop() % (float)tmp1);
                    break;
                // drem 
                case 0x73:
                    tmp1 = pop();
                    operandStack.Push((double)pop() % (double)tmp1);
                    break;
                // ineg
                case 0x74:
                    operandStack.Push(-(int)pop());
                    break;
                // lneg
                case 0x75:
                    operandStack.Push(-(long)pop());
                    break;
                // fneg
                case 0x76:
                    operandStack.Push(-(float)pop());
                    break;
                // dneg
                case 0x77:
                    operandStack.Push(-(double)pop());
                    break;
                // ishl
                case 0x78:
                    tmp1 = pop();
                    operandStack.Push((int)pop() << (int)tmp1);
                    break;
                // lshl
                case 0x79:
                    tmp1 = pop();
                    operandStack.Push((long)pop() << (int)pop());
                    break;
                // ishr
                case 0x7a:
                    tmp1 = pop();
                    operandStack.Push((int)pop() >> (int)tmp1);
                    break;
                // lshr
                case 0x7b:
                    tmp1 = pop();
                    operandStack.Push((long)pop() >> (int)pop());
                    break;
                // iushr
                case 0x7c:
                    tmp1 = pop();
                    operandStack.Push((int)((uint)pop() >> (int)pop()));
                    break;
                // lushr
                case 0x7d:
                    tmp1 = pop();
                    operandStack.Push((long)((ulong)pop() >> (int)pop()));
                    break;
                // iand
                case 0x7e:
                    operandStack.Push((int)pop() & (int)pop());
                    break;
                // land
                case 0x7f:
                    operandStack.Push((long)pop() & (long)pop());
                    break;
                // ior
                case 0x80:
                    operandStack.Push((int)pop() | (int)pop());
                    break;
                // lor
                case 0x81:
                    operandStack.Push((long)pop() | (long)pop());
                    break;
                // ixor
                case 0x82:
                    operandStack.Push((int)pop() ^ (int)pop());
                    break;
                // lxor
                case 0x83:
                    operandStack.Push((long)pop() ^ (long)pop());
                    break;
                // iinc 
                // TODO: check for sign
                case 0x84:
                    tmp1 = (int)getOperand(1);
                    addLocalVar((int)tmp1, (int)getLocalVar((int)tmp1) + (int)getOperand(1));
                    break;
                // i2l
                case 0x85:
                    operandStack.Push(Convert.ToInt64((int)pop()));
                    break;
                // i2f
                case 0x86:
                    operandStack.Push(Convert.ToSingle((int)pop()));
                    break;
                // i2d
                case 0x87:
                    operandStack.Push(Convert.ToDouble((int)pop()));
                    break;
                // l2i
                case 0x88:
                    operandStack.Push(Convert.ToInt32((long)pop()));
                    break;
                // l2f
                case 0x89:
                    operandStack.Push(Convert.ToSingle((long)pop()));
                    break;
                // l2d
                case 0x8a:
                    operandStack.Push(Convert.ToDouble((long)pop()));
                    break;
                // f2i
                case 0x8b:
                    operandStack.Push(Convert.ToInt32((float)pop()));
                    break;
                // f2l
                case 0x8c:
                    operandStack.Push(Convert.ToInt64((float)pop()));
                    break;
                // f2d
                case 0x8d:
                    operandStack.Push(Convert.ToDouble((float)pop()));
                    break;
                // d2i
                case 0x8e:
                    operandStack.Push(Convert.ToInt32((double)pop()));
                    break;
                // d2l
                case 0x8f:
                    operandStack.Push(Convert.ToInt64((double)pop()));
                    break;
                // d2f
                case 0x90:
                    operandStack.Push(Convert.ToSingle((double)pop()));
                    break;
                // i2b
                case 0x91:
                    operandStack.Push(Convert.ToByte((int)pop()));
                    break;
                // i2c
                case 0x92:
                    operandStack.Push(Convert.ToChar((int)pop()));
                    break;
                // i2s
                case 0x93:
                    operandStack.Push(Convert.ToInt16((int)pop()));
                    break;
                // lcmp
                case 0x94:
                    tmp1 = pop();
                    tmp2 = pop();
                    operandStack.Push((long)tmp1 > (long)tmp2 ? 1 : (long)tmp1 < (long)tmp2 ? -1 : 0);
                    break;
                // fcmpl
                case 0x95:
                    tmp1 = pop();
                    tmp2 = pop();
                    operandStack.Push((float)tmp1 > (float)tmp2 ? 1 : (float)tmp1 < (float)tmp2 ? -1 : 0);
                    break;
                // fcmpg
                case 0x96:
                    tmp1 = pop();
                    tmp2 = pop();
                    operandStack.Push((float)tmp1 > (float)tmp2 ? 1 : (float)tmp1 < (float)tmp2 ? -1 : 0);
                    break;
                // dcmpl
                case 0x97:
                    tmp1 = pop();
                    tmp2 = pop();
                    operandStack.Push((double)tmp1 > (double)tmp2 ? 1 : (double)tmp1 < (double)tmp2 ? -1 : 0);
                    break;
                // dcmpg
                case 0x98:
                    tmp1 = pop();
                    tmp2 = pop();
                    operandStack.Push((double)tmp1 > (double)tmp2 ? 1 : (double)tmp1 < (double)tmp2 ? -1 : 0);
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
                    operandStack.Push((uint)pc);
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
                        operandStack.Push(field.Value);
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
                            operandStack.Push(classInstance.fields[i]);
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
                    operandStack.Push(heap.addObject(cp.getConstantClass((int)getOperand(2)), cp));
                    break;
                // newarray
                case 0xbc:
                    operandStack.Push(heap.addArray((int)getOperand(1), (int)pop()));
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
            String methodArgs = cp.getConstantUtf8(heap.loadedClasses.ElementAt(classIndex).Methods.ElementAt(methodIndex).DescriptorIndex).Value;
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