# Teacher Transcription From The Videos

Note: when I continued, the original MP4 files were no longer in `Downloads`, so I could not re-extract high-resolution frames. The text below is copied from the readable frames that were already extracted, plus the matching project files for the category part. I marked the few places that were too blurry to prove character-for-character.

## Video 1

### `ProductCreateVm.cs`

```csharp
using FrontToBack.Models;
using System.ComponentModel.DataAnnotations;

namespace FrontToBack.Areas.AdminPanel.ViewModels
{
    public class ProductCreateVm
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public decimal Price { get; set; }
        [Required]
        public string Description { get; set; }
        public string SKU { get; set; }
        [Required]
        public int CategoryId { get; set; }
        public List<Category>? Categories { get; set; }
    }
}
```

### `ProductUpdateVm.cs`

```csharp
using FrontToBack.Models;
using System.ComponentModel.DataAnnotations;

namespace FrontToBack.Areas.AdminPanel.ViewModels
{
    public class ProductUpdateVm
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public decimal Price { get; set; }
        [Required]
        public string Description { get; set; }
        public string SKU { get; set; }
        [Required]
        public int CategoryId { get; set; }
        public List<Category>? Categories { get; set; }
    }
}
```

### Category select in product create/update view

```cshtml
<div class="form-group">
    <label for="CategoryId">Category</label>
    <select asp-for="CategoryId" class="form-control">
        @foreach (Category category in Model.Categories)
        {
            <option value="@category.Id">@category.Name</option>
        }
    </select>
    <span asp-validation-for="CategoryId" class="text-danger"></span>
</div>
```

### Category select later changed to `SelectList`

```cshtml
<div class="form-group">
    <label for="CategoryId">Category</label>
    <select asp-for="CategoryId" class="form-control" asp-items="new SelectList(Model.Categories, nameof(Category.Id), nameof(Category.Name))">
        <option value="0" selected disabled>Choose Category</option>
    </select>
    <span asp-validation-for="CategoryId" class="text-danger"></span>
</div>
```

### Product create action category validation

```csharp
[HttpPost]
public async Task<IActionResult> Create(ProductCreateVm productCreateVm)
{
    productCreateVm.Categories = await _context.Categories.Where(c => !c.IsDeleted).ToListAsync();

    if (!ModelState.IsValid) return View(productCreateVm);

    bool existCategory = productCreateVm.Categories.Any(c => c.Id == productCreateVm.CategoryId);
    if (!existCategory)
    {
        ModelState.AddModelError(nameof(ProductCreateVm.CategoryId), "Category does not exist!");
        return View(productCreateVm);
    }

    Product product = new()
    {
        Name = productCreateVm.Name,
        Price = productCreateVm.Price,
        Description = productCreateVm.Description,
        SKU = productCreateVm.SKU,
        CategoryId = productCreateVm.CategoryId
    };

    await _context.Products.AddAsync(product);
    await _context.SaveChangesAsync();

    return RedirectToAction(nameof(Index));
}
```

### Product update action category validation

```csharp
[HttpPost]
public async Task<IActionResult> Update(int id, ProductUpdateVm productUpdateVm)
{
    if (id == null || id < 1) return BadRequest();

    productUpdateVm.Categories = await _context.Categories.Where(c => !c.IsDeleted).ToListAsync();

    if (!ModelState.IsValid) return View(productUpdateVm);

    Product? existProduct = await _context.Products.FirstOrDefaultAsync(p => p.Id == id);
    if (existProduct == null) return NotFound();

    bool existCategory = productUpdateVm.Categories.Any(c => c.Id == productUpdateVm.CategoryId);
    if (!existCategory)
    {
        ModelState.AddModelError(nameof(ProductUpdateVm.CategoryId), "Category does not exist!");
        return View(productUpdateVm);
    }

    existProduct.Name = productUpdateVm.Name;
    existProduct.Price = productUpdateVm.Price;
    existProduct.Description = productUpdateVm.Description;
    existProduct.SKU = productUpdateVm.SKU;
    existProduct.CategoryId = productUpdateVm.CategoryId;

    await _context.SaveChangesAsync();

    return RedirectToAction(nameof(Index));
}
```

## Video 2

### `Tag.cs`

```csharp
namespace FrontToBack.Models
{
    public class Tag
    {
    }
}
```

### `ProductTag.cs`

```csharp
namespace FrontToBack.Models
{
    public class ProductTag
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public int TagId { get; set; }
        public Product Product { get; set; }
        public Tag Tag { get; set; }
    }
}
```

### `Product.cs` additions

```csharp
public int CategoryId { get; set; }
public Category Category { get; set; }
public List<ProductImage>? ProductImages { get; set; }
public List<ProductTag>? ProductTags { get; set; }
```

### `AppDbContext.cs` additions

```csharp
public DbSet<Tag> Tags { get; set; }
public DbSet<ProductTag> ProductTags { get; set; }
```

### `DetailVM.cs`

```csharp
using FrontToBack.Models;

namespace FrontToBack.ViewModels
{
    public class DetailVM
    {
        public Product Product { get; set; }
        public List<Product> RelatedProducts { get; set; }
    }
}
```

### Detail action

```csharp
public async Task<IActionResult> Detail(int? id)
{
    if (id is null || id < 1) return BadRequest();

    Product? product = await _context.Products
        .Where(p => !p.IsDeleted)
        .Include(p => p.ProductImages)
        .Include(p => p.Category)
        .Include(p => p.ProductTags)
        .ThenInclude(pt => pt.Tag)
        .FirstOrDefaultAsync(p => p.Id == id);

    List<Product> relatedProducts = await _context.Products
        .Where(p => !p.IsDeleted)
        .Include(p => p.ProductImages.Where(pi => pi.IsPrimary == null))
        .Where(p => p.CategoryId == product.CategoryId && p.Id != id)
        .Take(4)
        .ToListAsync();
}
```

The frame then continues into creating and returning a `DetailVM`, but the lower lines were too blurry to confirm exactly.

## Video 3

### `ProductCreateVm.cs` final visible properties

```csharp
using FrontToBack.Models;
using System.ComponentModel.DataAnnotations;

namespace FrontToBack.Areas.AdminPanel.ViewModels
{
    public class ProductCreateVm
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public decimal Price { get; set; }
        [Required]
        public string Description { get; set; }
        public string SKU { get; set; }
        [Required]
        public int CategoryId { get; set; }
        public List<Category>? Categories { get; set; }
        public List<int>? TagIds { get; set; }
        public List<Tag>? Tags { get; set; }
    }
}
```

### Product create action with tags

```csharp
[HttpPost]
public async Task<IActionResult> Create(ProductCreateVm productCreateVm)
{
    productCreateVm.Categories = await _context.Categories.Where(c => !c.IsDeleted).ToListAsync();
    productCreateVm.Tags = await _context.Tags.Where(t => !t.IsDeleted).ToListAsync();

    if (!ModelState.IsValid) return View(productCreateVm);

    bool existCategory = productCreateVm.Categories.Any(c => c.Id == productCreateVm.CategoryId);
    if (!existCategory)
    {
        ModelState.AddModelError(nameof(ProductCreateVm.CategoryId), "Category does not exist!");
        return View(productCreateVm);
    }

    if (productCreateVm.TagIds is not null)
    {
        bool existTag = productCreateVm.TagIds.Any(tagId => !productCreateVm.Tags.Exists(t => t.Id == tagId));
        if (existTag)
        {
            ModelState.AddModelError(nameof(ProductCreateVm.TagIds), "Tag does not exist!");
            return View(productCreateVm);
        }
    }

    Product product = new()
    {
        Name = productCreateVm.Name,
        Price = productCreateVm.Price,
        Description = productCreateVm.Description,
        SKU = productCreateVm.SKU,
        CategoryId = productCreateVm.CategoryId.Value
    };

    if (productCreateVm.TagIds != null)
    {
        foreach (int tagId in productCreateVm.TagIds)
        {
            ProductTag productTag = new()
            {
                TagId = tagId,
                Product = product
            };

            await _context.ProductTags.AddAsync(productTag);
        }
    }

    await _context.Products.AddAsync(product);
    await _context.SaveChangesAsync();

    return RedirectToAction(nameof(Index));
}
```

### Tags select in create/update view

```cshtml
<div class="form-group">
    <label for="TagIds">Tags</label>
    <select asp-for="TagIds" class="form-control" asp-items="new SelectList(Model.Tags, nameof(Tag.Id), nameof(Tag.Name))">
        <option value="0" selected disabled>Choose Tags</option>
    </select>
    <span asp-validation-for="TagIds" class="text-danger"></span>
</div>
```

### Product update action visible tag/category part

```csharp
[HttpPost]
public async Task<IActionResult> Update(int? id, ProductUpdateVm productUpdateVm)
{
    if (id == null || id < 1) return BadRequest();

    productUpdateVM.Categories = await _context.Categories.Where(c => !c.IsDeleted).ToListAsync();

    if (!ModelState.IsValid) return View(productUpdateVM);

    Product? existProduct = await _context.Products.FirstOrDefaultAsync(p => p.Id == id);
    if (existProduct == null) return NotFound();

    bool existCategory = productUpdateVM.Categories.Any(c => c.Id == productUpdateVM.CategoryId);
    if (!existCategory)
    {
        ModelState.AddModelError(nameof(ProductUpdateVM.CategoryId), "Category does not exist!");
        return View(productUpdateVM);
    }

    if (productUpdateVM.TagIds is not null)
    {
        bool existTag = productUpdateVM.TagIds.Any(tagId => !productUpdateVM.Tags.Exists(t => t.Id == tagId));
        if (existTag)
        {
            ModelState.AddModelError(nameof(ProductUpdateVM.TagIds), "Tag does not exist!");
            return View(productUpdateVM);
        }
    }

    existProduct.Name = productUpdateVM.Name;
    existProduct.Price = productUpdateVM.Price;
    existProduct.Description = productUpdateVM.Description;
    existProduct.SKU = productUpdateVM.SKU;
    existProduct.CategoryId = productUpdateVM.CategoryId.Value;

    await _context.SaveChangesAsync();

    return RedirectToAction(nameof(Index));
}
```

In the video frames, the casing switches visually between `productUpdateVm` and `productUpdateVM` in different moments. The final low-resolution frames are not sharp enough to prove the exact casing everywhere.

