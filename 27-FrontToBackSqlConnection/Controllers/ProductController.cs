using _27_FrontToBackSqlConnection.Data;
using _27_FrontToBackSqlConnection.Models;
using _27_FrontToBackSqlConnection.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace _27_FrontToBackSqlConnection.Controllers;

public class ProductController : Controller
{
    private readonly AppDbContext _context;

    public ProductController(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IActionResult> Detail(int id)
    {
        Product? product = await _context.Products
            .Include(p => p.Category)
            .Include(p => p.ProductImages)
            .FirstOrDefaultAsync(p => p.Id == id && !p.IsDeleted);

        if (product is null)
        {
            return NotFound();
        }

        List<Product> relatedProducts = await _context.Products
            .Where(p => !p.IsDeleted && p.Id != id)
            .Include(p => p.ProductImages)
            .OrderByDescending(p => p.CreatedAt)
            .Take(4)
            .ToListAsync();

        ProductVM productVm = new()
        {
            Product = product,
            RelatedProducts = relatedProducts
        };

        return View(productVm);
    }
}
