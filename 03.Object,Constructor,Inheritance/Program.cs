using System;

namespace _03.Object_Constructor_Inheritance.Models
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Student student1 = new Student(
                "Aydan", "Sharifova", 20, "aydan@gmail.com", "P001",
                "S1001", "İnformasiya texnologiyaları", 88.5, 2
            );

            Student student2 = new Student(
                "Hamid", "Aliyev", 21, "hamid@gmail.com", "P002",
                "S1002", "Kompüter elmləri", 92.0, 3
            );

            Student student3 = new Student(
                "Elcan", "Aliyev", 20, "elcan@gmail.com", "P003",
                "S1003", "Riyaziyyat", 68.5, 1
            );

            Teacher teacher1 = new Teacher(
                "Said", "Nuraliyev", 36, "said@gmail.com", "T001",
                "Proqramlaşdırma", "C#", 800m, 15
            );

            Teacher teacher2 = new Teacher(
                "Leyla", "Badalzade", 25, "leyla@gmail.com", "T002",
                "Şəbəkə texnologiyaları", "Computer Networks", 900m, 8
            );

            Administrator admin = new Administrator(
                "Taleh", "Shirinov", 60, "kamal@gmail.com", "A001",
                "Dekan", "İnformasiya Texnologiyaları Fakültəsi", 5
            );

            student1.ShowStudentInfo();
            Console.WriteLine($"Təqaüd: {student1.CalculateScholarship()} AZN");
            Console.WriteLine();

            student2.ShowStudentInfo();
            Console.WriteLine($"Təqaüd: {student2.CalculateScholarship()} AZN");
            Console.WriteLine();

            student3.ShowStudentInfo();
            Console.WriteLine($"Təqaüd: {student3.CalculateScholarship()} AZN");
            Console.WriteLine();

            teacher1.ShowTeacherInfo();
            Console.WriteLine($"Maaş: {teacher1.CalculateSalary()} AZN");
            Console.WriteLine();

            teacher2.ShowTeacherInfo();
            Console.WriteLine($"Maaş: {teacher2.CalculateSalary()} AZN");
            Console.WriteLine();

            admin.ShowAdminInfo();
            Console.WriteLine();

            admin.GrantAccess(student1);
            Console.WriteLine();

            int totalScholarship =
                student1.CalculateScholarship() +
                student2.CalculateScholarship() +
                student3.CalculateScholarship();

            decimal totalSalary =
                teacher1.CalculateSalary() +
                teacher2.CalculateSalary();

            Console.WriteLine("----- Statistika -----");
            Console.WriteLine($"Ümumi təqaüd xərci: {totalScholarship} AZN");
            Console.WriteLine($"Ümumi maaş xərci: {totalSalary} AZN");
        }
    }
}