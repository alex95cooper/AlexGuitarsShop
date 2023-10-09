using System.ComponentModel.DataAnnotations;

namespace AlexGuitarsShop.Web.Domain.ViewModels;

public class LoginViewModel
{
    [Required(ErrorMessage = "Enter the e-mail")]
    [MaxLength(30, ErrorMessage = "The name must be less than 30 characters long")]
    [MinLength(3, ErrorMessage = "The name must be longer than 3 characters")]
    public string Email { get; init; }

    [Required(ErrorMessage = "Enter the password")]
    [DataType(DataType.Password)]
    [Display(Name = "Password")]
    public string Password { get; init; }
}