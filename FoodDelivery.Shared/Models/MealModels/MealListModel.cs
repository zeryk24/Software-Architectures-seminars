namespace FoodDelivery.Shared.Models.MealsModels;

public class MealListModel
{
    public int Id { get; set; }
    public int RestaurantId { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public double Price { get; set; }
    public string MealType { get; set; }

}
