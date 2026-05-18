using System.ComponentModel.DataAnnotations;

namespace _27_FrontToBackSqlConnection.Areas.AdminPanel.ViewModels.Products;

public class ProductUpdateVM
{
    [Required(ErrorMessage = "Don't be empty")]
    [MaxLength(100, ErrorMessage = "Max length is 100")]
    public string Name { get; set; } = null!;

    [Required(ErrorMessage = "Don't be empty")]
    public string Description { get; set; } = null!;

    [Required(ErrorMessage = "Don't be empty")]
    [MaxLength(100, ErrorMessage = "Max length is 100")]
    public string SKU { get; set; } = null!;

    [Range(0.01, double.MaxValue, ErrorMessage = "Price must be greater than 0")]
    public decimal Price { get; set; }

    [Range(1, int.MaxValue, ErrorMessage = "Select a category")]
    public int CategoryId { get; set; }

    public string Image { get; set; } = null!;
    public string? HoverImage { get; set; }
    public IFormFile? Photo { get; set; }
    public IFormFile? HoverPhoto { get; set; }
    public bool IsFeatured { get; set; }
    public bool IsNew { get; set; }
    public bool IsBestSeller { get; set; }
}
