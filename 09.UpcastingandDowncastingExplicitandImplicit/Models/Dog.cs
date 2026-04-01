using System;
namespace _09.UpcastingandDowncastingExplicitandImplicit.Models
{
	public class Dog : Animal
	{
        public string Breed { get; set; }

        public Dog(string name, string breed) : base(name)
        {
            Breed = breed;
        }

        public void Bark()
        {
            Console.WriteLine($"{Name} hürür.");
        }
    }
}

