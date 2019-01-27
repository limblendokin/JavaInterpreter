using System;

namespace JavaInterpreter
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
                className = "AdditionWithFunction";
                Console.Write(className);
            }
            String directory = @"C:\Documents\Study\LaPP\";
            
            Machine m = new Machine(directory+className+@".class");
            exitCode = m.Run();
            Console.WriteLine("Program ended with code " + exitCode);
            Console.ReadKey();
            
        }
    }
}
