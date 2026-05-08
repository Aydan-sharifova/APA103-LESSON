namespace _27_FrontToBackSqlConnection.Models;

public class Slider: BaseEntity
{
	public required string Title { get; set; } 
	public required string Subtitle { get; set; } 
	public required string Desc {get; set; }
	public required string Image {get; set; }
	public int Order { get; set; }

}
