using FoodDelivery.Shared.Enums;

namespace FoodDelivery.Shared.Models.OrderModels;

public class OrderChangePaymentTypeModel
{
    public int Id { get; set; }
    public PaymentType PaymentType { get; set; }
}