using FoodDelivery.Shared.Models.MealsModels;
using FoodDelivery.Shared.Models.RestaurantModels;
using FoodDelivery.Shared.Models.UserModels;

namespace FoodDelivery.Shared.Models.FeedbacksModels;

public class FeedbackDetailModel
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public UserDetailModel User { get; set; }
    public int MealId { get; set; }
    public MealDetailModel Meal { get; set; }
    public int RestaurantId { get; set; }
    public RestaurantDetailModel Restaurant { get; set; }
    public int Rating { get; set; }
    public string Description { get; set; }
}
