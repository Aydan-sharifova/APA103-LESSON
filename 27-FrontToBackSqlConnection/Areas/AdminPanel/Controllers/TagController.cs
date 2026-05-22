using _27_FrontToBackSqlConnection.Data;
using _27_FrontToBackSqlConnection.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace _27_FrontToBackSqlConnection.Areas.AdminPanel.Controllers;

[Area("AdminPanel")]
public class TagController : Controller
{
    private readonly AppDbContext _context;

    public TagController(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IActionResult> Index()
    {
        List<Tag> tags = await _context.Tags
            .Where(t => !t.IsDeleted)
            .Include(t => t.ProductTags)
            .OrderByDescending(t => t.CreatedAt)
            .ToListAsync();

        return View(tags);
    }

    [HttpGet]
    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Create(Tag tag)
    {
        if (!ModelState.IsValid)
        {
            return View(tag);
        }

        bool tagExists = await _context.Tags
            .AnyAsync(t => !t.IsDeleted && t.Name.Trim() == tag.Name.Trim());

        if (tagExists)
        {
            ModelState.AddModelError(nameof(Tag.Name), "Tag already exists!");
            return View(tag);
        }

       

        await _context.Tags.AddAsync(tag);
        await _context.SaveChangesAsync();

        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Detail(int? id)
    {
        if (id is null || id < 1) return BadRequest();

        Tag? tag = await _context.Tags
            .Where(t => !t.IsDeleted)
            .Include(t => t.ProductTags)
            .FirstOrDefaultAsync(t => t.Id == id);

        if (tag is null) return NotFound();

        return View(tag);
    }

    public async Task<IActionResult> Update(int? id)
    {
        if (id is null || id < 1) return BadRequest();

        Tag? tag = await _context.Tags
            .Where(t => !t.IsDeleted)
            .FirstOrDefaultAsync(t => t.Id == id);

        if (tag is null) return NotFound();

        return View(tag);
    }

    [HttpPost]
    public async Task<IActionResult> Update(int? id, Tag tag)
    {
        if (id is null || id < 1) return BadRequest();

        Tag? existTag = await _context.Tags
            .Where(t => !t.IsDeleted)
            .FirstOrDefaultAsync(t => t.Id == id);

        if (existTag is null) return NotFound();

        if (!ModelState.IsValid)
        {
            return View(tag);
        }

        bool tagExists = await _context.Tags
            .AnyAsync(t => !t.IsDeleted && t.Id != id && t.Name.Trim() == tag.Name.Trim());

        if (tagExists)
        {
            ModelState.AddModelError(nameof(Tag.Name), "Tag already exists!");
            return View(tag);
        }

        existTag.Name = tag.Name.Trim();

        await _context.SaveChangesAsync();

        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Delete(int? id)
    {
        if (id is null || id < 1) return BadRequest();

        Tag? tag = await _context.Tags
            .Where(t => !t.IsDeleted)
            .FirstOrDefaultAsync(t => t.Id == id);

        if (tag is null) return NotFound();

        _context.Tags.Remove(tag);
        await _context.SaveChangesAsync();

        return RedirectToAction(nameof(Index));
    }
}
