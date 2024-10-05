using FoodDelivery.DAL.Entities.Base;

namespace FoodDelivery.DAL.EFCore.Entities;

public class FeedbackEntity : Entity
{
    public int Rating { get; set; }
    public string Description { get; set; }

    public int? MealId { get; set; }
    public MealEntity? Meal { get; set; }

    public int? RestaurantId { get; set; }
    public RestaurantEntity? Restaurant { get; set; }

    public int? UserId { get; set; }
    public UserEntity? User { get; set; }
}
