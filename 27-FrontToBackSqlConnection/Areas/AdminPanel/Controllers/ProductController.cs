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
            .Include(p => p.ProductTags)
            .ThenInclude(pt => pt.Tag)
            .OrderByDescending(p => p.CreatedAt)
            .ToListAsync();

        return View(products);
    }

    public async Task<IActionResult> Create()
    {
        await LoadProductSelectsAsync();

        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Create(ProductCreateVM productCreateVM)
    {
        await ValidateProductCreateAsync(productCreateVM);

        if (!ModelState.IsValid)
        {
            await LoadProductSelectsAsync(productCreateVM.CategoryId, productCreateVM.TagIds);
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
            IsBestSeller = productCreateVM.IsBestSeller,
            ProductTags = productCreateVM.TagIds
                .Distinct()
                .Select(tagId => new ProductTag { TagId = tagId })
                .ToList(),
            ProductImages = await CreateProductImagesAsync(productCreateVM.AdditionalPhotos)
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
            .Include(p => p.ProductTags)
            .ThenInclude(pt => pt.Tag)
            .FirstOrDefaultAsync(p => p.Id == id);

        if (product is null) return NotFound();

        return View(product);
    }

    public async Task<IActionResult> Update(int? id)
    {
        if (id is null || id < 1) return BadRequest();

        Product? product = await _context.Products
            .Where(p => !p.IsDeleted)
            .Include(p => p.ProductImages)
            .Include(p => p.ProductTags)
            .FirstOrDefaultAsync(p => p.Id == id);

        if (product is null) return NotFound();

        ProductUpdateVM productUpdateVM = new()
        {
            Id = product.Id,
            Name = product.Name,
            Description = product.Description,
            SKU = product.SKU,
            Price = product.Price,
            CategoryId = product.CategoryId,
            Image = product.Image,
            HoverImage = product.HoverImage,
            ProductImages = product.ProductImages
                .Where(pi => !pi.IsDeleted)
                .OrderByDescending(pi => pi.CreatedAt)
                .ToList(),
            IsFeatured = product.IsFeatured,
            IsNew = product.IsNew,
            IsBestSeller = product.IsBestSeller,
            TagIds = product.ProductTags.Select(pt => pt.TagId).ToList()
        };

        await LoadProductSelectsAsync(product.CategoryId, productUpdateVM.TagIds);

        return View(productUpdateVM);
    }

    [HttpPost]
    public async Task<IActionResult> Update(int? id, ProductUpdateVM productUpdateVM)
    {
        if (id is null || id < 1) return BadRequest();

        Product? product = await _context.Products
            .Where(p => !p.IsDeleted)
            .Include(p => p.ProductImages)
            .Include(p => p.ProductTags)
            .FirstOrDefaultAsync(p => p.Id == id);

        if (product is null) return NotFound();

        await ValidateProductUpdateAsync(productUpdateVM, product.Id);

        if (!ModelState.IsValid)
        {
            productUpdateVM.Id = product.Id;
            productUpdateVM.Image = product.Image;
            productUpdateVM.HoverImage = product.HoverImage;
            productUpdateVM.ProductImages = product.ProductImages
                .Where(pi => !pi.IsDeleted)
                .OrderByDescending(pi => pi.CreatedAt)
                .ToList();
            await LoadProductSelectsAsync(productUpdateVM.CategoryId, productUpdateVM.TagIds);
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
        UpdateProductTags(product, productUpdateVM.TagIds);

        await _context.SaveChangesAsync();

        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Delete(int? id)
    {
        if (id is null || id < 1) return BadRequest();

        Product? product = await _context.Products
            .Where(p => !p.IsDeleted)
            .Include(p => p.ProductImages)
            .Include(p => p.ProductTags)
            .FirstOrDefaultAsync(p => p.Id == id);

        if (product is null) return NotFound();

        DeleteProductImages(product);

        _context.Products.Remove(product);
        await _context.SaveChangesAsync();

        return RedirectToAction(nameof(Index));
    }

    [HttpGet]
    public async Task<IActionResult> GetAdditionalPhotos(int? id)
    {
        if (id is null || id < 1) return BadRequest();

        Product? product = await _context.Products
            .Where(p => !p.IsDeleted)
            .Include(p => p.ProductImages)
            .FirstOrDefaultAsync(p => p.Id == id);

        if (product is null) return NotFound();

        var additionalPhotos = product.ProductImages
            .Where(pi => !pi.IsDeleted)
            .OrderByDescending(pi => pi.CreatedAt)
            .Select(pi => new
            {
                pi.Id,
                pi.Image,
                ImageUrl = Url.Content($"~/images/{pi.Image}")
            });

        return Json(additionalPhotos);
    }

    [HttpPost]
    public async Task<IActionResult> CreateAdditionalPhotos(int? id, List<IFormFile>? photos)
    {
        if (id is null || id < 1) return BadRequest();

        Product? product = await _context.Products
            .Where(p => !p.IsDeleted)
            .Include(p => p.ProductImages)
            .FirstOrDefaultAsync(p => p.Id == id);

        if (product is null) return NotFound();

        string? validationError = GetPhotoCollectionValidationError(photos, true);
        if (validationError is not null)
        {
            TempData["AdditionalPhotoError"] = validationError;
            return RedirectToAction(nameof(Update), new { id = product.Id });
        }

        product.ProductImages.AddRange(await CreateProductImagesAsync(photos));
        await _context.SaveChangesAsync();

        TempData["AdditionalPhotoSuccess"] = "Additional photos added successfully.";

        return RedirectToAction(nameof(Update), new { id = product.Id });
    }

    [HttpPost]
    public async Task<IActionResult> UpdateAdditionalPhoto(int? id, IFormFile? photo)
    {
        if (id is null || id < 1) return BadRequest();

        ProductImage? productImage = await _context.ProductImages
            .Include(pi => pi.Product)
            .FirstOrDefaultAsync(pi => !pi.IsDeleted && pi.Id == id && !pi.Product.IsDeleted);

        if (productImage is null) return NotFound();

        string? validationError = GetPhotoValidationError(photo);
        if (validationError is not null)
        {
            TempData["AdditionalPhotoError"] = validationError;
            return RedirectToAction(nameof(Update), new { id = productImage.ProductId });
        }

        string newImage = await photo!.CreateFile(_env.WebRootPath, "images");
        productImage.Image.DeleteFile(_env.WebRootPath, "images");
        productImage.Image = newImage;

        await _context.SaveChangesAsync();

        TempData["AdditionalPhotoSuccess"] = "Additional photo updated successfully.";

        return RedirectToAction(nameof(Update), new { id = productImage.ProductId });
    }

    [HttpGet]
    public async Task<IActionResult> DeleteAdditionalPhoto(int? id)
    {
        if (id is null || id < 1) return BadRequest();

        ProductImage? productImage = await _context.ProductImages
            .Include(pi => pi.Product)
            .FirstOrDefaultAsync(pi => !pi.IsDeleted && pi.Id == id && !pi.Product.IsDeleted);

        if (productImage is null) return NotFound();

        int productId = productImage.ProductId;

        productImage.Image.DeleteFile(_env.WebRootPath, "images");
        _context.ProductImages.Remove(productImage);
        await _context.SaveChangesAsync();

        TempData["AdditionalPhotoSuccess"] = "Additional photo deleted successfully.";

        return RedirectToAction(nameof(Update), new { id = productId });
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

    private async Task LoadTagsAsync(IEnumerable<int>? selectedTagIds = null)
    {
        HashSet<int> selectedIds = selectedTagIds?.ToHashSet() ?? [];

        List<Tag> tags = await _context.Tags
            .Where(t => !t.IsDeleted)
            .ToListAsync();

        ViewBag.Tags = tags
            .Select(t => new SelectListItem
            {
                Value = t.Id.ToString(),
                Text = t.Name,
                Selected = selectedIds.Contains(t.Id)
            })
            .ToList();
    }

    private async Task LoadProductSelectsAsync(int? selectedCategoryId = null, IEnumerable<int>? selectedTagIds = null)
    {
        await LoadCategoriesAsync(selectedCategoryId);
        await LoadTagsAsync(selectedTagIds);
    }

    private async Task ValidateProductCreateAsync(ProductCreateVM productCreateVM)
    {
        if (!ModelState.IsValid) return;

        await ValidateCategoryAsync(productCreateVM.CategoryId);
        await ValidateTagsAsync(productCreateVM.TagIds);
        await ValidateSkuAsync(productCreateVM.SKU);
        ValidatePhoto(productCreateVM.Photo, nameof(ProductCreateVM.Photo));
        ValidatePhoto(productCreateVM.HoverPhoto, nameof(ProductCreateVM.HoverPhoto), false);
        ValidatePhotos(productCreateVM.AdditionalPhotos, nameof(ProductCreateVM.AdditionalPhotos));
    }

    private async Task ValidateProductUpdateAsync(ProductUpdateVM productUpdateVM, int productId)
    {
        if (!ModelState.IsValid) return;

        await ValidateCategoryAsync(productUpdateVM.CategoryId);
        await ValidateTagsAsync(productUpdateVM.TagIds);
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

    private async Task ValidateTagsAsync(IEnumerable<int>? tagIds)
    {
        List<int> selectedTagIds = tagIds?
            .Where(tagId => tagId > 0)
            .Distinct()
            .ToList() ?? [];

        if (selectedTagIds.Count == 0) return;

        int existingTagCount = await _context.Tags
            .CountAsync(t => !t.IsDeleted && selectedTagIds.Contains(t.Id));

        if (existingTagCount != selectedTagIds.Count)
        {
            ModelState.AddModelError("TagIds", "Select valid tags");
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
        string? validationError = GetPhotoValidationError(photo, isRequired);

        if (validationError is not null)
        {
            ModelState.AddModelError(propertyName, validationError);
        }
    }

    private void ValidatePhotos(IEnumerable<IFormFile>? photos, string propertyName)
    {
        if (photos is null) return;

        foreach (IFormFile photo in photos.Where(HasFile))
        {
            ValidatePhoto(photo, propertyName, false);
        }
    }

    private string? GetPhotoValidationError(IFormFile? photo, bool isRequired = true)
    {
        if (!HasFile(photo))
        {
            return isRequired ? "Don't be empty" : null;
        }

        IFormFile validPhoto = photo!;

        if (!validPhoto.CheckFileType("image/"))
        {
            return "File type is incorrect!";
        }

        if (!validPhoto.CheckFileSize(FileSize.MB, 2))
        {
            return "File size must be less than 2mb!";
        }

        return null;
    }

    private string? GetPhotoCollectionValidationError(IEnumerable<IFormFile>? photos, bool isRequired = false)
    {
        List<IFormFile> selectedPhotos = photos?
            .Where(HasFile)
            .ToList() ?? [];

        if (selectedPhotos.Count == 0)
        {
            return isRequired ? "Select at least one photo." : null;
        }

        foreach (IFormFile photo in selectedPhotos)
        {
            string? validationError = GetPhotoValidationError(photo);

            if (validationError is not null)
            {
                return validationError;
            }
        }

        return null;
    }

    private static bool HasFile(IFormFile? file)
    {
        return file is not null && file.Length > 0;
    }

    private async Task<List<ProductImage>> CreateProductImagesAsync(IEnumerable<IFormFile>? photos)
    {
        List<ProductImage> productImages = [];

        if (photos is null) return productImages;

        foreach (IFormFile photo in photos.Where(HasFile))
        {
            productImages.Add(new ProductImage
            {
                Image = await photo.CreateFile(_env.WebRootPath, "images"),
                IsPrimary = false
            });
        }

        return productImages;
    }

    private void UpdateProductTags(Product product, IEnumerable<int> tagIds)
    {
        List<int> selectedTagIds = tagIds
            .Where(tagId => tagId > 0)
            .Distinct()
            .ToList();
        List<ProductTag> existingProductTags = product.ProductTags.ToList();

        _context.ProductTags.RemoveRange(existingProductTags.Where(pt => !selectedTagIds.Contains(pt.TagId)));

        foreach (int tagId in selectedTagIds)
        {
            if (existingProductTags.Any(pt => pt.TagId == tagId)) continue;

            product.ProductTags.Add(new ProductTag { TagId = tagId });
        }
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
