using System;
namespace _05_Abstract_class__Polymorphism__ForEach.Models
{
    public class Car : Vehicle
    {
        public int DoorsCount;
        public int TrunkCapacity;
        public bool IsAutomatic;
        public int MaxSpeed;

        public Car(string brand, string model, int year, string plateNumber, int doorsCount, int trunkCapacity, bool isAutomatic, int maxSpeed)
            : base(brand, model, year, plateNumber)
        {
            this.DoorsCount = doorsCount;
            this.TrunkCapacity = trunkCapacity;
            this.IsAutomatic = isAutomatic;
            this.MaxSpeed = maxSpeed;
        }

        public void ShowCarInfo()
        {
            Console.WriteLine("Car Info:");
            ShowBasicInfo();
            Console.WriteLine($"Doors Count: {DoorsCount}");
            Console.WriteLine($"Trunk Capacity: {TrunkCapacity} L");
            Console.WriteLine($"Is Automatic: {IsAutomatic}");
            Console.WriteLine($"Max Speed: {MaxSpeed} km/h");
        }

        public double CalculateFuelCost(double distance)
        {
            return (distance / 100) * 8 * 1.50;
        }
    }
}


