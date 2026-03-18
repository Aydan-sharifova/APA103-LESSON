using System;
namespace _05_Abstract_class__Polymorphism__ForEach.Models
{
    public class Truck : Vehicle
    {
        public double CargoCapacity;
        public int AxleCount;
        public double CurrentLoad;
        public int MaxSpeed;

        public Truck(string brand, string model, int year, string plateNumber, double cargoCapacity, int axleCount, double currentLoad, int maxSpeed)
            : base(brand, model, year, plateNumber)
        {
            this.CargoCapacity = cargoCapacity;
            this.AxleCount = axleCount;
            this.CurrentLoad = currentLoad;
            this.MaxSpeed = maxSpeed;
        }

        public void ShowTruckInfo()
        {
            Console.WriteLine("Truck Info:");
            ShowBasicInfo();
            Console.WriteLine($"Cargo Capacity: {CargoCapacity} ton");
            Console.WriteLine($"Axle Count: {AxleCount}");
            Console.WriteLine($"Current Load: {CurrentLoad} ton");
            Console.WriteLine($"Max Speed: {MaxSpeed} km/h");
        }

        public void LoadCargo(double weight)
        {
            if (CurrentLoad + weight <= CargoCapacity)
            {
                CurrentLoad += weight;
                Console.WriteLine($"{weight} ton cargo loaded successfully.");
            }
            else
            {
                Console.WriteLine("Cargo cannot be loaded. Capacity exceeded.");
            }
        }

        public double CalculateFuelCost(double distance)
        {
            return (distance / 100) * (25 + CurrentLoad * 2) * 1.80;
        }
    }
}

