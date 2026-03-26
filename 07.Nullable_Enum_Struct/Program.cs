using System;
using _07.Nullable_Enum_Struct.Enums;
using _07.Nullable_Enum_Struct.Models;

namespace _07.Nullable_Enum_Struct;
internal class Program
{
    static void Main(string[] args)
    {
        DrinkOrder order1 = new DrinkOrder(101, "Ali", DrinkType.Coffee, DrinkSize.Medium);
        DrinkOrder order2 = new DrinkOrder(102, "Leyla", DrinkType.Tea, DrinkSize.Large);
        DrinkOrder order3 = new DrinkOrder(103, "Vüqar", DrinkType.Juice, DrinkSize.Small);

        order1.DisplayOrder();
        order1.UpdateStatus(OrderStatus.Preparing);
        order1.UpdateStatus(OrderStatus.Ready);
        order1.UpdateStatus(OrderStatus.Delivered);
        Console.WriteLine();

        order2.DisplayOrder();
        order2.UpdateStatus(OrderStatus.Ready);
        Console.WriteLine();

        order3.DisplayOrder();

        Console.WriteLine("DrinkType dəyərləri");
        foreach (DrinkType drink in Enum.GetValues(typeof(DrinkType)))
        {
            Console.WriteLine(drink);
        }
        Console.WriteLine();

        Console.WriteLine("DrinkSize dəyərləri");
        foreach (DrinkSize size in Enum.GetValues(typeof(DrinkSize)))
        {
            Console.WriteLine(size);
        }
        Console.WriteLine();

        Console.WriteLine("OrderStatus dəyərləri");
        foreach (OrderStatus status in Enum.GetValues(typeof(OrderStatus)))
        {
            Console.WriteLine(status);
        }
        Console.WriteLine();

        Console.WriteLine("ToString");
        Console.WriteLine(DrinkType.Coffee.ToString());
        Console.WriteLine(DrinkSize.Large.ToString());
        Console.WriteLine();

        Console.WriteLine("Parse");
        DrinkType parsedDrink = (DrinkType)Enum.Parse(typeof(DrinkType), "Tea");
        DrinkSize parsedSize = (DrinkSize)Enum.Parse(typeof(DrinkSize), "Medium");

        Console.WriteLine(parsedDrink);
        Console.WriteLine(parsedSize);
        Console.WriteLine();

        decimal totalAmount = order1.Price + order2.Price + order3.Price;

        Console.WriteLine("Statistika");
        Console.WriteLine("Ümumi sifariş sayı: 3");
        Console.WriteLine($"Birinci sifarişin qiyməti: {order1.Price} AZN");
        Console.WriteLine($"İkinci sifarişin qiyməti: {order2.Price} AZN");
        Console.WriteLine($"Üçüncü sifarişin qiyməti: {order3.Price} AZN");
        Console.WriteLine($"Ümumi məbləğ: {totalAmount} AZN");
    }
}

