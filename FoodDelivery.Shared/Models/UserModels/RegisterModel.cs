using System.ComponentModel.DataAnnotations;

namespace FoodDelivery.Shared.Models.UserModels;

public class RegisterModel
{
    [Required]
    [StringLength(50)]
    [EmailAddress]
    public string Email { get; set; }
    [Required]
    public string Name { get; set; }
    [Required]
    public string Surname { get; set; }
    public string Role { get; set; }
    [Required]
    [StringLength(50, MinimumLength = 5)]
    public string Password { get; set; }
    [Required]
    [StringLength(50, MinimumLength = 5)]
    public string ConfirmPassword { get; set; }
}
