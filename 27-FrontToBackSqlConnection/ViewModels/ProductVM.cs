using _27_FrontToBackSqlConnection.Models;

namespace _27_FrontToBackSqlConnection.ViewModels;

public class ProductVM
{
    public Product Product { get; set; } = null!;
    public List<Product> RelatedProducts { get; set; } = [];
}
