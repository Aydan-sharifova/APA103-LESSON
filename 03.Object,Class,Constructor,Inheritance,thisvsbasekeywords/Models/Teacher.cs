using System;

namespace _03.Object_Constructor_Inheritance.Models
{
    public class Teacher : Person
    {
        public string Department { get; set; }
        public string MainSubject { get; set; }
        public decimal BaseSalary { get; set; }
        public int ExperienceYears { get; set; }

        public Teacher(
            string firstName,
            string lastName,
            int age,
            string email,
            string id,
            string department,
            string mainSubject,
            decimal baseSalary,
            int experienceYears
        ) : base(firstName, lastName, age, email, id)
        {
            this.Department = department;
            this.MainSubject = mainSubject;
            this.BaseSalary = baseSalary;
            this.ExperienceYears = experienceYears;
        }

        public void ShowTeacherInfo()
        {
            Console.WriteLine("----- Müəllim Məlumatı -----");
            ShowBasicInfo();
            Console.WriteLine($"Kafedra: {Department}");
            Console.WriteLine($"Əsas fənn: {MainSubject}");
            Console.WriteLine($"Baza maaş: {BaseSalary} AZN");
            Console.WriteLine($"Təcrübə ili: {ExperienceYears}");
        }

        public decimal CalculateSalary()
        {
            return BaseSalary + (ExperienceYears * 50);
        }
    }
}