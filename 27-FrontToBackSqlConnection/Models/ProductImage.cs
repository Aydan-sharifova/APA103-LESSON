namespace _27_FrontToBackSqlConnection.Models;

public class ProductImage : BaseEntity
{
    public required string Image { get; set; }
    public bool? IsPrimary { get; set; }
    public int ProductId { get; set; }
    public Product Product { get; set; } = null!;
}
