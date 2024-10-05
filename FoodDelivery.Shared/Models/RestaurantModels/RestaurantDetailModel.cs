using FoodDelivery.Shared.Models.FeedbacksModels;
using FoodDelivery.Shared.Models.MealsModels;

namespace FoodDelivery.Shared.Models.RestaurantModels;

public class RestaurantDetailModel
{
    public int Id { get; set; }
    public string Name { get; set; }
    public bool Disabled { get; set; }
    public List<MealListModel> Meals { get; set; }
    public List<FeedbackListModel> Feedbacks { get; set; }
}
