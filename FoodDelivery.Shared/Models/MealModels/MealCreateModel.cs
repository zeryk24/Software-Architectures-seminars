namespace FoodDelivery.Shared.Models.MealModels;

public class MealCreateModel
{
    public int RestaurantId { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public double Price { get; set; }
    public string MealType { get; set; }

}
