using System;
namespace _05_Abstract_class__Polymorphism__ForEach.Models
{
    public class Vehicle
    {
        public string Brand;
        public string Model;
        public int Year;
        public string PlateNumber;
        public double FuelLevel;

        public Vehicle(string brand, string model, int year, string plateNumber)
        {
            this.Brand = brand;
            this.Model = model;
            this.Year = year;
            this.PlateNumber = plateNumber;
            this.FuelLevel = 100;
        }

        public string GetVehicleInfo()
        {
            return $"Brand: {Brand}, Model: {Model}, Year: {Year}, Plate Number: {PlateNumber}, Fuel Level: {FuelLevel}";
        }

        public void ShowBasicInfo()
        {
            Console.WriteLine($"Brand: {Brand}");
            Console.WriteLine($"Model: {Model}");
            Console.WriteLine($"Year: {Year}");
            Console.WriteLine($"Plate Number: {PlateNumber}");
            Console.WriteLine($"Fuel Level: {FuelLevel}");
        }
    }
}

