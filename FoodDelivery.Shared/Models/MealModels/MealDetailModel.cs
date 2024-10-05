using FoodDelivery.Shared.Models.FeedbacksModels;
using FoodDelivery.Shared.Models.OrderItemsModels;
using FoodDelivery.Shared.Models.RestaurantModels;

namespace FoodDelivery.Shared.Models.MealsModels;

public class MealDetailModel
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public double Price { get; set; }
    public string MealType { get; set; }
    public int RestaurantId { get; set; }
    public List<FeedbackListModel> Feedbacks { get; set; }
}
