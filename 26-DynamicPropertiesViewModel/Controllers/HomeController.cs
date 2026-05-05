using _26_DynamicPropertiesViewModel.Models;
using _26_DynamicPropertiesViewModel.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace _26_DynamicPropertiesViewModel.Controllers;

public class HomeController : Controller
{
    private readonly List<Student> _students = new()
    {
        new Student { Id = 1, Name = "Ruad", Age = 20 },
        new Student { Id = 2, Name = "Gunel", Age = 19 },
        new Student { Id = 5, Name = "Aysu", Age = 19 }
    };

    private readonly List<Teacher> _teachers = new()
    {
        new Teacher { Id = 1, Name = "Nermin", Salary = 1800 },
        new Teacher { Id = 2, Name = "Ruslan", Salary = 2300 },
        new Teacher { Id = 3, Name = "Leyla", Salary = 2100 }
    };

    public IActionResult Index()
    {
        ViewBag.Students = _students;
        ViewData["Teachers"] = _teachers;
        TempData["Name"] = "Dynamic Properties";

        HomeVM model = new()
        {
            LessonTitle = "Dynamic Properties and ViewModel",
            Description = "ViewBag, ViewData and TempData are dynamic ways to send data. ViewModel is strongly typed and safer for real pages.",
            Students = _students,
            Teachers = _teachers
        };

        return View(model);
    }

    public IActionResult Details()
    {
        ViewBag.Students = _students;
        ViewData["Teachers"] = _teachers;
        TempData["Name"] = "Details page";

        return View();
    }

    public IActionResult CorporateSales()
    {
        return View();
    }
}
