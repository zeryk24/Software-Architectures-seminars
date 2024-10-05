namespace FoodDelivery.Shared.Models.FeedbacksModels;

public class RestaurantFeedbackCreateModel
{
    public int RestaurantId { get; set; }
    public int Rating { get; set; }
    public string Description { get; set; }
}