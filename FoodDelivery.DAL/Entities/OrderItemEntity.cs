using FoodDelivery.DAL.Entities;
using FoodDelivery.DAL.Entities.Base;

namespace FoodDelivery.DAL.EFCore.Entities;

public class OrderItemEntity : Entity
{
    public double UnitPrice { get; set; }   
    public int Amount { get; set; }

    public int? OrderId { get; set; }
    public OrderEntity? Order { get; set; }

    public int? MealId { get; set; }
    public MealEntity? Meal { get; set; }
}
