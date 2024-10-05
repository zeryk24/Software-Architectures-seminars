using System.ComponentModel.DataAnnotations;

namespace FoodDelivery.Shared.Models.UserModels;

public class UserChangePasswordModel
{
    [Required]
    [StringLength(50, MinimumLength = 5)]
    public string CurrentPassword { get; set; }
    [Required]
    [StringLength(50, MinimumLength = 5)]
    public string NewPassword { get; set; }
    [Required]
    [StringLength(50, MinimumLength = 5)]
    public string ConfirmPassword { get; set; }
}