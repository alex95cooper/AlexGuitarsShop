using System.ComponentModel.DataAnnotations;

namespace AlexGuitarsShop.Domain.ViewModels;

public class RegisterViewModel
{
    [Required] [Display(Name = "Name")] public string Name { get; set; }

    [Required] 
    [DataType(DataType.EmailAddress)]
    [Display(Name = "Email")] 
    public string Email { get; set; }

    [Required]
    [DataType(DataType.Password)]
    [Display(Name = "Password")]
    public string Password { get; set; }

    [Required]
    [Compare("Password", ErrorMessage = "Passwords are not compared")]
    [DataType(DataType.Password)]
    [Display(Name = "Confirm password")]
    public string PasswordConfirm { get; set; }
}