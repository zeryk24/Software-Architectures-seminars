using FoodDelivery.DAL.Entities;
using FoodDelivery.DAL.Entities.Base;

namespace FoodDelivery.DAL.EFCore.Entities;

public class RestaurantEntity : Entity
{
    public string Name { get; set; }
    public bool Disabled { get; set; }
    public List<MealEntity> Meals { get; set; }
    public List<FeedbackEntity> Feedbacks { get; set; }
}
