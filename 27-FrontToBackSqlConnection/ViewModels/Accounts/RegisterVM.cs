using System.ComponentModel.DataAnnotations;

namespace _27_FrontToBackSqlConnection.ViewModels.Accounts;

public class RegisterVM
{
    [Required(ErrorMessage = "Don't be empty")]
    [MaxLength(100, ErrorMessage = "Max length is 100")]
    public string UserName { get; set; } = null!;

    [Required(ErrorMessage = "Don't be empty")]
    [EmailAddress(ErrorMessage = "Email is incorrect")]
    public string Email { get; set; } = null!;

    [Required(ErrorMessage = "Don't be empty")]
    [DataType(DataType.Password)]
    [MinLength(6, ErrorMessage = "Password length must be at least 6")]
    public string Password { get; set; } = null!;

    [Required(ErrorMessage = "Don't be empty")]
    [DataType(DataType.Password)]
    [Compare(nameof(Password), ErrorMessage = "Passwords do not match")]
    public string ConfirmPassword { get; set; } = null!;

    public string? ReturnUrl { get; set; }
}
