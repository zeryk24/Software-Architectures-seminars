using FoodDelivery.DAL.Entities;
using FoodDelivery.DAL.Entities.Base;

namespace FoodDelivery.DAL.EFCore.Entities;

public class MealEntity : Entity
{
    public string Name { get; set; }
    public string Description { get; set; }
    public double Price { get; set; }
    public string MealType { get; set; }

    public List<OrderItemEntity> OrderItems { get; set; }
    public List<FeedbackEntity> Feedbacks { get; set; }

    public int? RestaurantId { get; set; }
    public RestaurantEntity? Restaurant { get; set; }
}
