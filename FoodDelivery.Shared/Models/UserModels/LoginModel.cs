using System.ComponentModel.DataAnnotations;

namespace FoodDelivery.Shared.Models.UserModels;

public class LoginModel
{
    [Required]
    [StringLength(50)]
    [EmailAddress]
    public string Email { get; set; }

    [Required]
    [StringLength(50, MinimumLength = 5)]
    public string Password { get; set; }
}
