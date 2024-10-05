namespace FoodDelivery.Shared.Models.FeedbackModels;

public class MealFeedbackCreateModel
{
    public int MealId { get; set; }
    public int Rating { get; set; }
    public string Description { get; set; }
}
