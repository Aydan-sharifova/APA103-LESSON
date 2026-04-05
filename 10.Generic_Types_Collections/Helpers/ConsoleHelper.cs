using System;
namespace _10.Generic_Types_Collections.Helpers
{
    public static class ConsoleHelper
    {
        public static void PrintHeader(string title)
        {
            Console.WriteLine();
            Console.WriteLine(new string('=', 60));
            Console.WriteLine(title);
            Console.WriteLine(new string('=', 60));
        }

        public static void PrintSubHeader(string title)
        {
            Console.WriteLine();
            Console.WriteLine(new string('-', 40));
            Console.WriteLine(title);
            Console.WriteLine(new string('-', 40));
        }
    }
}

