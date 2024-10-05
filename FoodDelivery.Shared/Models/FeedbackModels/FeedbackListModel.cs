namespace FoodDelivery.Shared.Models.FeedbacksModels;

public class FeedbackListModel
{
    public int Id { get; set; }
    public int Rating { get; set; }
    public string Description { get; set; }
    public int UserId { get; set; }
}
