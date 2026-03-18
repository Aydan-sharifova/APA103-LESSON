using System;
namespace _05_Abstract_class__Polymorphism__ForEach.Models
{
    public class Motorcycle : Vehicle
    {
        public int EngineCapacity;
        public bool HasSidecar;
        public int MaxSpeed;
        public string Type;

        public Motorcycle(string brand, string model, int year, string plateNumber, int engineCapacity, bool hasSidecar, int maxSpeed, string type)
            : base(brand, model, year, plateNumber)
        {
            this.EngineCapacity = engineCapacity;
            this.HasSidecar = hasSidecar;
            this.MaxSpeed = maxSpeed;
            this.Type = type;
        }

        public void ShowMotorcycleInfo()
        {
            Console.WriteLine("Motorcycle Info:");
            ShowBasicInfo();
            Console.WriteLine($"Engine Capacity: {EngineCapacity} cc");
            Console.WriteLine($"Has Sidecar: {HasSidecar}");
            Console.WriteLine($"Max Speed: {MaxSpeed} km/h");
            Console.WriteLine($"Type: {Type}");
        }

        public double CalculateFuelCost(double distance)
        {
            return (distance / 100) * 4 * 1.50;
        }
    }
}

