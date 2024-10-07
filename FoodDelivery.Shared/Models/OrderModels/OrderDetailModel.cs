using FoodDelivery.Shared.Enums;
using FoodDelivery.Shared.Models.AddressModels;
using FoodDelivery.Shared.Models.OrderItemsModels;
using FoodDelivery.Shared.Models.RestaurantModels;
using FoodDelivery.Shared.Models.UserModels;

namespace FoodDelivery.Shared.Models.OrderModels;

public class OrderDetailModel
{
    public int Id { get; set; }
    public PaymentType PaymentType { get; set; }

    public int UserId { get; set; }
    public int RestaurantId { get; set; }
    public string RestaurantName { get; set; }

    public AddressDetailModel Address { get; set; }

    public List<OrderItemListModel> OrderItems { get; set; }
    
    public bool Completed { get; set; }
}
