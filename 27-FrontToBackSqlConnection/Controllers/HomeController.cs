using System.Diagnostics;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using _27_FrontToBackSqlConnection.Models;
using _27_FrontToBackSqlConnection.ViewModels;
using _27_FrontToBackSqlConnection.Data;

namespace _27_FrontToBackSqlConnection.Controllers;

public class HomeController: Controller
{
    private readonly AppDbContext _context;
    public HomeController(AppDbContext context)
    {
        _context = context;
    }

    List<Slider> _sliders = new List<Slider>
    {
        new Slider{Id=1,Title="Title-1",Subtitle="Subtitle-1",Desc="Beatiful Roses",Image="1-1-524x617.png",Order=1,isDeleted=true},
        new Slider{Id=2,Title="Title-2",Subtitle="Subtitle-2",Desc="Beatiful Roses",Image="1-2-524x617.png",Order=2,isDeleted=false},
        new Slider{Id=3,Title="Title-3",Subtitle="Subtitle-3",Desc="Beatiful Roses",Image="1-3-524x617.png",Order=3,isDeleted=true}
    };

  

    public IActionResult Index()
    {
        HomeVM homeVm = new()
        {
            Sliders = _sliders
            .OrderBy(s => s.Order)
            .Where(s=>!s.isDeleted)
            .Take(2)
            .ToList(),
        };
        
        return View(homeVm);
    }

    
}

