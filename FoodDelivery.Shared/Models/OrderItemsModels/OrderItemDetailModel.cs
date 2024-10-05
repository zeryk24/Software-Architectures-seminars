using FoodDelivery.Shared.Models.MealsModels;
using FoodDelivery.Shared.Models.OrderModels;

namespace FoodDelivery.Shared.Models.OrderItemsModels;

public class OrderItemDetailModel
{
    public int Id { get; set; }
    public double UnitPrice { get; set; }
    public int Amount { get; set; }

    public int OrderId { get; set; }
    public OrderDetailModel Order { get; set; }

    public int MealId { get; set; }
    public MealDetailModel Meal { get; set; }
}
