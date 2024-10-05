using Microsoft.AspNetCore.Identity;

namespace FoodDelivery.DAL.EFCore.Entities;

public class UserEntity : IdentityUser<int>
{
    public string Name { get; set; }
    public string Surname { get; set; }
    public int? AddressId { get; set; }
    public AddressEntity? Address { get; set; }

    public List<OrderEntity> Orders { get; set; }
}