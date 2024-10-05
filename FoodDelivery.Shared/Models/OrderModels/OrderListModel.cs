using FoodDelivery.Shared.Enums;
using FoodDelivery.Shared.Models.OrderItemsModels;

namespace FoodDelivery.Shared.Models.OrderModels;

public class OrderListModel
{
    public int Id { get; set; }
    public PaymentType PaymentType { get; set; }
    public int UserId { get; set; }
    public int AddressId { get; set; }
    public List<OrderItemListModel> OrderItems { get; set; }
}
