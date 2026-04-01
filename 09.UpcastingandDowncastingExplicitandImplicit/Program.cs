using _09.UpcastingandDowncastingExplicitandImplicit.Models;

namespace _09.UpcastingandDowncastingExplicitandImplicit;

class Program
{
    static void Main(string[] args)
    {

        Dog dog1 = new Dog("Toplan", "Alabay");

        // Upcasting (Implicit)
        Animal animal1 = dog1;
        animal1.Eat();

        Console.WriteLine();

        // Downcasting (Explicit)
        Dog dog2 = (Dog)animal1;
        dog2.Bark();

        

        Dog dog3 = animal1 as Dog;
        Console.WriteLine("as operatoru ilə nümunə:");

    }
}

