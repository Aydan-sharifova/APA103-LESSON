using Microsoft.AspNetCore.Mvc;
using _27_FrontToBackSqlConnection.Models;
using _27_FrontToBackSqlConnection.ViewModels;
using _27_FrontToBackSqlConnection.Data;
using Microsoft.EntityFrameworkCore;

public class HomeController: Controller
{
    private readonly AppDbContext _context;
    public HomeController(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IActionResult> Index()
    {
        List<Slider> sliders = await _context.Sliders.OrderBy(s => s.Order)
            .Where(s => !s.IsDeleted)
            .Take(2)
            .ToListAsync();

        List<Product> products = await _context.Products
            .Where(p => !p.IsDeleted)
            .Include(p => p.Category)
            .Include(p => p.ProductImages)
            .OrderByDescending(p => p.CreatedAt)
            .Take(8)
            .ToListAsync();

        HomeVM homeVm = new()
        {
            Sliders = sliders,
            Products = products
        };
        return View(homeVm);
    }

    
}
