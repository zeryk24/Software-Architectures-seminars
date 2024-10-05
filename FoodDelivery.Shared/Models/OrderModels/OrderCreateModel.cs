using FoodDelivery.Shared.Enums;

namespace FoodDelivery.Shared.Models.OrderModels;

public class OrderCreateModel
{
    public int UserId { get; set; }
    public int RestaurantId { get; set; }
}
