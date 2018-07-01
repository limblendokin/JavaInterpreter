﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab1
{
    class Program
    {
        static void Main(string[] args)
        {
            string className;
            // Считывание файла по пути заданному пользователем или default пути 
            Console.WriteLine("Provide class name or just hit enter to use default:");
            className = Console.ReadLine();
            uint exitCode = 0;

            if (className == "")
            {
                className = "TestingClassLoading";
                Console.Write(className);
            }
            Machine m = new Machine(className);
            exitCode = m.run();
            Console.WriteLine("Program ended with code " + exitCode);
            Console.ReadKey();
            
        }
    }
}
