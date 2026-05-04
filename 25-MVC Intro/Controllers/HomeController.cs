using Microsoft.AspNetCore.Mvc;

namespace _25_MVC_Intro.Controllers;

public class HomeController : Controller
{
    public IActionResult Index()
    {
        return View();
    }

    public IActionResult About()
    {
        return View();
    }
}
