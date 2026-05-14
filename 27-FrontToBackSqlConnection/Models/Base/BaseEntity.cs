using System.ComponentModel.DataAnnotations.Schema;

namespace _27_FrontToBackSqlConnection.Models;

public abstract class BaseEntity
{
    public int Id { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    [Column("isDeleted")]
    public bool IsDeleted { get; set; }
}
