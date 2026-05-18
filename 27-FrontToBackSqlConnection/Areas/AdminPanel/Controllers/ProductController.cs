using _27_FrontToBackSqlConnection.Areas.AdminPanel.ViewModels.Products;
using _27_FrontToBackSqlConnection.Data;
using _27_FrontToBackSqlConnection.Models;
using _27_FrontToBackSqlConnection.Utilities.Enums;
using _27_FrontToBackSqlConnection.Utilities.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace _27_FrontToBackSqlConnection.Areas.AdminPanel.Controllers;

[Area("AdminPanel")]
public class ProductController : Controller
{
    private readonly AppDbContext _context;
    private readonly IWebHostEnvironment _env;

    public ProductController(AppDbContext context, IWebHostEnvironment env)
    {
        _context = context;
        _env = env;
    }

    public async Task<IActionResult> Index()
    {
        List<Product> products = await _context.Products
            .Where(p => !p.IsDeleted)
            .Include(p => p.Category)
            .OrderByDescending(p => p.CreatedAt)
            .ToListAsync();

        return View(products);
    }

    public async Task<IActionResult> Create()
    {
        await LoadCategoriesAsync();

        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Create(ProductCreateVM productCreateVM)
    {
        await ValidateProductCreateAsync(productCreateVM);

        if (!ModelState.IsValid)
        {
            await LoadCategoriesAsync(productCreateVM.CategoryId);
            return View(productCreateVM);
        }

        string? hoverImage = null;

        if (HasFile(productCreateVM.HoverPhoto))
        {
            hoverImage = await productCreateVM.HoverPhoto!.CreateFile(_env.WebRootPath, "images");
        }

        Product product = new()
        {
            Name = productCreateVM.Name.Trim(),
            Description = productCreateVM.Description.Trim(),
            SKU = productCreateVM.SKU.Trim(),
            Price = productCreateVM.Price,
            CategoryId = productCreateVM.CategoryId,
            Image = await productCreateVM.Photo.CreateFile(_env.WebRootPath, "images"),
            HoverImage = hoverImage,
            IsFeatured = productCreateVM.IsFeatured,
            IsNew = productCreateVM.IsNew,
            IsBestSeller = productCreateVM.IsBestSeller
        };

        await _context.Products.AddAsync(product);
        await _context.SaveChangesAsync();

        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Detail(int? id)
    {
        if (id is null || id < 1) return BadRequest();

        Product? product = await _context.Products
            .Where(p => !p.IsDeleted)
            .Include(p => p.Category)
            .Include(p => p.ProductImages)
            .FirstOrDefaultAsync(p => p.Id == id);

        if (product is null) return NotFound();

        return View(product);
    }

    public async Task<IActionResult> Update(int? id)
    {
        if (id is null || id < 1) return BadRequest();

        Product? product = await _context.Products
            .Where(p => !p.IsDeleted)
            .FirstOrDefaultAsync(p => p.Id == id);

        if (product is null) return NotFound();

        ProductUpdateVM productUpdateVM = new()
        {
            Name = product.Name,
            Description = product.Description,
            SKU = product.SKU,
            Price = product.Price,
            CategoryId = product.CategoryId,
            Image = product.Image,
            HoverImage = product.HoverImage,
            IsFeatured = product.IsFeatured,
            IsNew = product.IsNew,
            IsBestSeller = product.IsBestSeller
        };

        await LoadCategoriesAsync(product.CategoryId);

        return View(productUpdateVM);
    }

    [HttpPost]
    public async Task<IActionResult> Update(int? id, ProductUpdateVM productUpdateVM)
    {
        if (id is null || id < 1) return BadRequest();

        Product? product = await _context.Products
            .Where(p => !p.IsDeleted)
            .FirstOrDefaultAsync(p => p.Id == id);

        if (product is null) return NotFound();

        await ValidateProductUpdateAsync(productUpdateVM, product.Id);

        if (!ModelState.IsValid)
        {
            productUpdateVM.Image = product.Image;
            productUpdateVM.HoverImage = product.HoverImage;
            await LoadCategoriesAsync(productUpdateVM.CategoryId);
            return View(productUpdateVM);
        }

        if (HasFile(productUpdateVM.Photo))
        {
            string newImage = await productUpdateVM.Photo!.CreateFile(_env.WebRootPath, "images");
            product.Image.DeleteFile(_env.WebRootPath, "images");
            product.Image = newImage;
        }

        if (HasFile(productUpdateVM.HoverPhoto))
        {
            string newHoverImage = await productUpdateVM.HoverPhoto!.CreateFile(_env.WebRootPath, "images");
            product.HoverImage?.DeleteFile(_env.WebRootPath, "images");
            product.HoverImage = newHoverImage;
        }

        product.Name = productUpdateVM.Name.Trim();
        product.Description = productUpdateVM.Description.Trim();
        product.SKU = productUpdateVM.SKU.Trim();
        product.Price = productUpdateVM.Price;
        product.CategoryId = productUpdateVM.CategoryId;
        product.IsFeatured = productUpdateVM.IsFeatured;
        product.IsNew = productUpdateVM.IsNew;
        product.IsBestSeller = productUpdateVM.IsBestSeller;

        await _context.SaveChangesAsync();

        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Delete(int? id)
    {
        if (id is null || id < 1) return BadRequest();

        Product? product = await _context.Products
            .Where(p => !p.IsDeleted)
            .Include(p => p.ProductImages)
            .FirstOrDefaultAsync(p => p.Id == id);

        if (product is null) return NotFound();

        DeleteProductImages(product);

        _context.Products.Remove(product);
        await _context.SaveChangesAsync();

        return RedirectToAction(nameof(Index));
    }

    private async Task LoadCategoriesAsync(int? selectedCategoryId = null)
    {
        ViewBag.Categories = await _context.Categories
            .Where(c => !c.IsDeleted)
            .Select(c => new SelectListItem
            {
                Value = c.Id.ToString(),
                Text = c.Name,
                Selected = selectedCategoryId == c.Id
            })
            .ToListAsync();
    }

    private async Task ValidateProductCreateAsync(ProductCreateVM productCreateVM)
    {
        if (!ModelState.IsValid) return;

        await ValidateCategoryAsync(productCreateVM.CategoryId);
        await ValidateSkuAsync(productCreateVM.SKU);
        ValidatePhoto(productCreateVM.Photo, nameof(ProductCreateVM.Photo));
        ValidatePhoto(productCreateVM.HoverPhoto, nameof(ProductCreateVM.HoverPhoto), false);
    }

    private async Task ValidateProductUpdateAsync(ProductUpdateVM productUpdateVM, int productId)
    {
        if (!ModelState.IsValid) return;

        await ValidateCategoryAsync(productUpdateVM.CategoryId);
        await ValidateSkuAsync(productUpdateVM.SKU, productId);
        ValidatePhoto(productUpdateVM.Photo, nameof(ProductUpdateVM.Photo), false);
        ValidatePhoto(productUpdateVM.HoverPhoto, nameof(ProductUpdateVM.HoverPhoto), false);
    }

    private async Task ValidateCategoryAsync(int categoryId)
    {
        bool categoryExists = await _context.Categories
            .AnyAsync(c => !c.IsDeleted && c.Id == categoryId);

        if (!categoryExists)
        {
            ModelState.AddModelError("CategoryId", "Select a category");
        }
    }

    private async Task ValidateSkuAsync(string sku, int? productId = null)
    {
        bool skuExists = await _context.Products
            .AnyAsync(p => !p.IsDeleted
                && (!productId.HasValue || p.Id != productId.Value)
                && p.SKU.Trim() == sku.Trim());

        if (skuExists)
        {
            ModelState.AddModelError("SKU", "SKU already exists!");
        }
    }

    private void ValidatePhoto(IFormFile? photo, string propertyName, bool isRequired = true)
    {
        if (!HasFile(photo))
        {
            if (isRequired)
            {
                ModelState.AddModelError(propertyName, "Don't be empty");
            }

            return;
        }

        IFormFile validPhoto = photo!;

        if (!validPhoto.CheckFileType("image/"))
        {
            ModelState.AddModelError(propertyName, "File type is incorrect!");
            return;
        }

        if (!validPhoto.CheckFileSize(FileSize.MB, 2))
        {
            ModelState.AddModelError(propertyName, "File size must be less than 2mb!");
        }
    }

    private static bool HasFile(IFormFile? file)
    {
        return file is not null && file.Length > 0;
    }

    private void DeleteProductImages(Product product)
    {
        HashSet<string> imageNames = new() { product.Image };

        if (!string.IsNullOrWhiteSpace(product.HoverImage))
        {
            imageNames.Add(product.HoverImage);
        }

        foreach (ProductImage productImage in product.ProductImages)
        {
            imageNames.Add(productImage.Image);
        }

        foreach (string imageName in imageNames)
        {
            imageName.DeleteFile(_env.WebRootPath, "images");
        }
    }
}
