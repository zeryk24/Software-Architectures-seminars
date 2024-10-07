using FoodDelivery.DAL.Entities;
using FoodDelivery.DAL.Entities.Base;
using FoodDelivery.Shared.Enums;

namespace FoodDelivery.DAL.EFCore.Entities;

public class OrderEntity : Entity
{
    public PaymentType PaymentType { get; set; }

    public int? UserId { get; set; }
    public UserEntity? User { get; set; }

    public int? RestaurantId { get; set; }
    public RestaurantEntity? Restaurant { get; set; }

    public int? AddressId { get; set; }
    public AddressEntity? Address { get; set; }

    public List<OrderItemEntity> OrderItems { get; set; }

    public bool Completed { get; set; } = false;
}
