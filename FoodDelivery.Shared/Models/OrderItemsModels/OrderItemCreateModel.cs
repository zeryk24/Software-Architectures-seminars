namespace FoodDelivery.Shared.Models.OrderItemsModels;

public class OrderItemCreateModel
{
    public int OrderId { get; set; }
    public int MealId { get; set; }
    public int Amount { get; set; }
}
