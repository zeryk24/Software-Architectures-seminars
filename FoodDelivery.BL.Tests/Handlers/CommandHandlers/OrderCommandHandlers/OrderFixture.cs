using FoodDelivery.DAL.EFCore.Entities;
using FoodDelivery.Shared.Enums;
using FoodDelivery.Shared.Models.AddressModels;
using FoodDelivery.Shared.Models.OrderModels;

namespace FoodDelivery.BL.Tests.Handlers.CommandHandlers.OrderCommandHandlers;

public class OrderFixture
{
    public OrderEntity OrderEntity { get; set; }
    public OrderDetailModel OrderDetailModel { get; set; }

    public OrderListModel OrderListModel { get; set; }

    public OrderFixture()
    {
        OrderEntity = new OrderEntity
        {
            PaymentType = PaymentType.Card,
            UserId = 1,
            RestaurantId = 1,
            AddressId = 1,
        };

        OrderDetailModel = new OrderDetailModel
        {
            Id = 1,
            PaymentType = PaymentType.Card,
            UserId = 1,
            RestaurantId = 1,
            Address = new AddressDetailModel
            {
                Id = 1,
                City = "",
                Number = "",
                PostalCode = "",
                Street = ""
            }
        };

        OrderListModel = new OrderListModel
        {
            Id = 1,
            PaymentType = PaymentType.Card,
            UserId = 1,
            AddressId = 1,
        };
    }
}