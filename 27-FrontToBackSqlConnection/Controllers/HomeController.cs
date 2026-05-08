using Microsoft.AspNetCore.Mvc;
using _27_FrontToBackSqlConnection.Models;
using _27_FrontToBackSqlConnection.ViewModels;
using _27_FrontToBackSqlConnection.Data;
using Microsoft.EntityFrameworkCore;

namespace _27_FrontToBackSqlConnection.Controllers;

public class HomeController: Controller
{
    private readonly AppDbContext _context;
    public HomeController(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IActionResult> Index()
    {

        //_context.AddRange();
        //_context.SaveChanges();

        List<Slider> sliders = _context.Sliders.OrderBy(s => s.Order)
            .Where(s => !s.isDeleted)
            .Take(2)
            .ToList();

        HomeVM homeVm = new()
        {
            Sliders = sliders

    };
        return View(homeVm);
    }

    
}
