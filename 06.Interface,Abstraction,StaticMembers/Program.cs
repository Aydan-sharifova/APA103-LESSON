
using _06.Interface__Abstraction__Static_Members.İnterfaces;
using _06.Interface__Abstraction__Static_Members.Models;

internal class Program
{
    static void Main(string[] args)
    {
        ICalculation calculation = new Calculation();

        Console.WriteLine("=== Sadə Kalkulyator ===");

        Console.Write("Birinci ədədi daxil edin: ");
        double num1 = Convert.ToDouble(Console.ReadLine());

        Console.Write("Əməli daxil edin (+, -, *, /): ");
        string operation = Console.ReadLine();

        Console.Write("İkinci ədədi daxil edin: ");
        double num2 = Convert.ToDouble(Console.ReadLine());

        try
        {
            double result = calculation.Calculate(num1, num2, operation);
            Console.WriteLine("Nəticə: " + result);
        }
        catch (Exception ex)
        {
            Console.WriteLine("Xəta: " + ex.Message);
        }
    }
}