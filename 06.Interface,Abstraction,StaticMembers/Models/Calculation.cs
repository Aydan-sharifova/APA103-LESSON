using System;
using _06.Interface__Abstraction__Static_Members.İnterfaces;

namespace _06.Interface__Abstraction__Static_Members.Models

{
    public class Calculation : ICalculation
    {
        public double Calculate(double num1, double num2, string operation)
        {
            switch (operation)
            {
                case "+":
                    return num1 + num2;

                case "-":
                    return num1 - num2;

                case "*":
                    return num1 * num2;

                case "/":
                    if (num2 == 0)
                    {
                        Console.WriteLine("0-a bölmək olmaz");
                        return 0;
                    }
                    return num1 / num2;

                default:
                    Console.WriteLine("Yanlış əməl daxil edilib");
                    return 0;
            }
        }
    }
}
