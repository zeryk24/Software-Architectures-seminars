namespace FoodDelivery.Shared.Models.OrderItemsModels;

public class OrderItemListModel
{
    public int Id { get; set; }
    public double UnitPrice { get; set; }
    public int Amount { get; set; }
    public int MealId { get; set; }
}
