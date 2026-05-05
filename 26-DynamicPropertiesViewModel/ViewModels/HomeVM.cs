using _26_DynamicPropertiesViewModel.Models;

namespace _26_DynamicPropertiesViewModel.ViewModels;

public class HomeVM
{
    public string LessonTitle { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public List<Student> Students { get; set; } = new();
    public List<Teacher> Teachers { get; set; } = new();
}
