using System.ComponentModel.DataAnnotations.Schema;

namespace _27_FrontToBackSqlConnection.Models;

public class Slider : BaseEntity
{
    public string Image { get; set; } = null!;
    public string Title { get; set; } = null!;
    public string Subtitle { get; set; } = null!;
    [Column("Desc")]
    public string Description { get; set; } = null!;
    public int Order { get; set; }

    [NotMapped]
    public IFormFile Photo { get; set; } = null!;
}
