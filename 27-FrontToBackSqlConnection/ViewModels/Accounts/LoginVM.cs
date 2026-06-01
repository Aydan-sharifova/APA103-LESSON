using System.ComponentModel.DataAnnotations;

namespace _27_FrontToBackSqlConnection.ViewModels.Accounts;

public class LoginVM
{
    [Required(ErrorMessage = "Don't be empty")]
    public string UserNameOrEmail { get; set; } = null!;

    [Required(ErrorMessage = "Don't be empty")]
    [DataType(DataType.Password)]
    public string Password { get; set; } = null!;

    public bool RememberMe { get; set; }

    public string? ReturnUrl { get; set; }
}
