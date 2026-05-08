namespace _27_FrontToBackSqlConnection.Models;

public class Product : BaseEntity
{
    public required string Name { get; set; }
    public required string Description { get; set; }
    public required string SKU { get; set; }
    public required string Image { get; set; }
    public string? HoverImage { get; set; }
    public decimal Price { get; set; }
    public int CategoryId { get; set; }
    public Category Category { get; set; } = null!;
    public List<ProductImage> ProductImages { get; set; } = [];
    public bool IsFeatured { get; set; }
    public bool IsNew { get; set; }
    public bool IsBestSeller { get; set; }
}
