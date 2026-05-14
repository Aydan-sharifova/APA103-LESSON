using System.ComponentModel.DataAnnotations;

namespace _27_FrontToBackSqlConnection.Models;

public class Category : BaseEntity
{
    [Required(ErrorMessage = "Don't be empty")]
    [MaxLength(100, ErrorMessage = "Max length is 100")]
    public string Name { get; set; } = null!;
    public List<Product> Products { get; set; } = [];
}
