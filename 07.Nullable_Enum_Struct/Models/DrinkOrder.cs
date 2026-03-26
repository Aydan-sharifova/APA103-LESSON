using System;
using _07.Nullable_Enum_Struct.Enums;

namespace _07.Nullable_Enum_Struct.Models
{
    public class DrinkOrder
    {
        public int OrderNumber { get; set; }
        public string CustomerName { get; set; }
        public DrinkType Drink { get; set; }
        public DrinkSize Size { get; set; }
        public OrderStatus Status { get; set; }
        public decimal Price { get; set; }

        public DrinkOrder(int orderNumber, string customerName, DrinkType drink, DrinkSize size)
        {
            OrderNumber = orderNumber;
            CustomerName = customerName;
            Drink = drink;
            Size = size;
            Status = OrderStatus.New;
            Price = CalculatePrice();
        }

        public decimal CalculatePrice()
        {
            switch (Drink)
            {
                case DrinkType.Coffee:
                    switch (Size)
                    {
                        case DrinkSize.Small:
                            return 3m;
                        case DrinkSize.Medium:
                            return 4m;
                        case DrinkSize.Large:
                            return 5m;
                    }
                    break;

                case DrinkType.Tea:
                    switch (Size)
                    {
                        case DrinkSize.Small:
                            return 2m;
                        case DrinkSize.Medium:
                            return 3m;
                        case DrinkSize.Large:
                            return 4m;
                    }
                    break;

                case DrinkType.Juice:
                    switch (Size)
                    {
                        case DrinkSize.Small:
                            return 4m;
                        case DrinkSize.Medium:
                            return 5m;
                        case DrinkSize.Large:
                            return 6m;
                    }
                    break;

                case DrinkType.Water:
                    switch (Size)
                    {
                        case DrinkSize.Small:
                            return 1m;
                        case DrinkSize.Medium:
                            return 1.5m;
                        case DrinkSize.Large:
                            return 2m;
                    }
                    break;
            }

            return 0m;
        }

        public void UpdateStatus(OrderStatus newStatus)
        {
            Status = newStatus;
            Console.WriteLine($"Sifariş #{OrderNumber} statusu: {newStatus}");
        }

        public void DisplayOrder()
        {
            Console.WriteLine("------ Sifariş Məlumatı ------");
            Console.WriteLine($"Sifariş nömrəsi: {OrderNumber}");
            Console.WriteLine($"Müştəri adı: {CustomerName}");
            Console.WriteLine($"İçki: {Drink}");
            Console.WriteLine($"Ölçü: {Size}");
            Console.WriteLine($"Status: {Status}");
            Console.WriteLine($"Qiymət: {Price} AZN");
            Console.WriteLine();
        }
    }
}

