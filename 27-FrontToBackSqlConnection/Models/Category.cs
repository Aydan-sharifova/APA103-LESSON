namespace _27_FrontToBackSqlConnection.Models;

public class Category : BaseEntity
{
    public required string Name { get; set; }
    public List<Product> Products { get; set; } = [];
}
