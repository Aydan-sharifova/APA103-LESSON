using System;
using _05_Abstract_class__Polymorphism__ForEach.Models;

namespace _05_Abstract_class__Polymorphism__ForEach;

internal class Program
{
    static void Main(string[] args)
    {
        List<Car> cars = new List<Car>()
            {
                new Car("Mercedes", "E200", 2023, "10-AA-010", 4, 500, true, 220),
                new Car("BMW", "320i", 2022, "10-BB-700", 4, 480, true, 235),
                new Car("Toyota", "Camry", 2021, "10-OO-100", 4, 524, true, 210)
            };

        List<Motorcycle> motorcycles = new List<Motorcycle>()
            {
                new Motorcycle("Yamaha", "R1", 2023, "10-MM-111", 998, false, 299, "Sport"),
                new Motorcycle("Harley-Davidson", "Street Glide", 2022, "10-MM-222", 1868, true, 180, "Cruiser")
            };

        List<Truck> trucks = new List<Truck>()
            {
                new Truck("MAN", "TGX", 2020, "10-TT-111", 18, 3, 12, 120),
                new Truck("Volvo", "FH16", 2021, "10-TT-222", 25, 4, 18, 110)
            };

        Console.WriteLine("----- CARS -----");
        foreach (var car in cars)
        {
            car.ShowCarInfo();
            Console.WriteLine($"Fuel Cost for 500 km: {car.CalculateFuelCost(500)} AZN");
            Console.WriteLine();
        }

        Console.WriteLine("----- MOTORCYCLES -----");
        foreach (var motorcycle in motorcycles)
        {
            motorcycle.ShowMotorcycleInfo();
            Console.WriteLine($"Fuel Cost for 300 km: {motorcycle.CalculateFuelCost(300)} AZN");
            Console.WriteLine();
        }

        Console.WriteLine("----- TRUCKS -----");
        foreach (var truck in trucks)
        {
            truck.ShowTruckInfo();
            Console.WriteLine($"Fuel Cost for 800 km: {truck.CalculateFuelCost(800)} AZN");
            Console.WriteLine();
        }

        Console.WriteLine("----- LOAD CARGO TO FIRST TRUCK -----");
        trucks[0].LoadCargo(5);
        Console.WriteLine($"New Fuel Cost for 800 km: {trucks[0].CalculateFuelCost(800)} AZN");
        Console.WriteLine();

        Console.WriteLine("----- STATISTICS -----");
        int totalVehicles = cars.Count + motorcycles.Count + trucks.Count;
        Console.WriteLine($"Total Vehicle Count: {totalVehicles}");

        List<int> maxSpeeds = new List<int>();

        foreach (var car in cars)
        {
            maxSpeeds.Add(car.MaxSpeed);
        }

        foreach (var motorcycle in motorcycles)
        {
            maxSpeeds.Add(motorcycle.MaxSpeed);
        }

        foreach (var truck in trucks)
        {
            maxSpeeds.Add(truck.MaxSpeed);
        }

        double averageMaxSpeed = maxSpeeds.Average();
        Console.WriteLine($"Average Max Speed: {averageMaxSpeed}");

        string mostExpensiveVehicle = "";
        double highestFuelCost = 0;

        foreach (var car in cars)
        {
            double cost = car.CalculateFuelCost(500);
            if (cost > highestFuelCost)
            {
                highestFuelCost = cost;
                mostExpensiveVehicle = $"{car.Brand} {car.Model}";
            }
        }

        foreach (var motorcycle in motorcycles)
        {
            double cost = motorcycle.CalculateFuelCost(300);
            if (cost > highestFuelCost)
            {
                highestFuelCost = cost;
                mostExpensiveVehicle = $"{motorcycle.Brand} {motorcycle.Model}";
            }
        }

        foreach (var truck in trucks)
        {
            double cost = truck.CalculateFuelCost(800);
            if (cost > highestFuelCost)
            {
                highestFuelCost = cost;
                mostExpensiveVehicle = $"{truck.Brand} {truck.Model}";
            }
        }

        Console.WriteLine($"Most Expensive Fuel Cost Vehicle: {mostExpensiveVehicle}");
        Console.WriteLine($"Highest Fuel Cost: {highestFuelCost} AZN");
    }
}

