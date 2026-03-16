using System;

namespace _03.Object_Constructor_Inheritance.Models
{
    public class Administrator : Person
    {
        public string Position { get; set; }
        public string Department { get; set; }
        public int AccessLevel { get; set; }

        public Administrator(
            string firstName,
            string lastName,
            int age,
            string email,
            string id,
            string position,
            string department,
            int accessLevel
        ) : base(firstName, lastName, age, email, id)
        {
            this.Position = position;
            this.Department = department;
            this.AccessLevel = accessLevel;
        }

        public void ShowAdminInfo()
        {
            Console.WriteLine("----- İdarəçi Məlumatı -----");
            ShowBasicInfo();
            Console.WriteLine($"Vəzifə: {Position}");
            Console.WriteLine($"Şöbə/Kafedra: {Department}");
            Console.WriteLine($"Giriş səviyyəsi: {AccessLevel}");
        }

        public void GrantAccess(Student student)
        {
            Console.WriteLine($"{GetFullName()} tərəfindən {student.GetFullName()} adlı tələbəyə sistem girişi verildi.");
        }
    }
}

