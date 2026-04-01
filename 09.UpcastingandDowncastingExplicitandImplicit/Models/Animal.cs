using System;
namespace _09.UpcastingandDowncastingExplicitandImplicit.Models
{
	public class Animal
	{
        public string Name { get; set; }

        public Animal(string name)
        {
            Name = name;
        }

        public void Eat()
        {
            Console.WriteLine($"{Name} yemək yeyir.");
        }
    }
}

