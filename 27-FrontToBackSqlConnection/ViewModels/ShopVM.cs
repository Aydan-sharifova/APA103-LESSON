using _27_FrontToBackSqlConnection.Models;

namespace _27_FrontToBackSqlConnection.ViewModels;

public class ShopVM
{
    public List<Product> Products { get; set; } = [];
    public int ProductCount { get; set; }
}
