using System.ComponentModel.DataAnnotations;

namespace AlexGuitarsShop.Domain.ViewModels;

public class RegisterViewModel
{
    [Required] [Display(Name = "Name")] public string Name { get; init; }

    [Required]
    [DataType(DataType.EmailAddress)]
    [Display(Name = "Email")]
    public string Email { get; init; }

    [Required]
    [DataType(DataType.Password)]
    [Display(Name = "Password")]
    public string Password { get; init; }

    [Required]
    [Compare("Password", ErrorMessage = "Passwords are not compared")]
    [DataType(DataType.Password)]
    [Display(Name = "Confirm password")]
    public string PasswordConfirm { get; init; }
}