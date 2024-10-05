using FoodDelivery.DAL.EFCore.Entities;
using FoodDelivery.Shared.Models.OrderItemsModels;

namespace FoodDelivery.BL.Tests.Handlers.CommandHandlers.OrderItemCommandHandlers;

public class OrderItemFixture
{
    public OrderItemEntity OrderItemEntity { get; set; }
    public OrderItemCreateModel OrderItemCreateModel { get; set; }
    public OrderItemDetailModel OrderItemDetailModel { get; set; }
    public OrderItemUpdateModel OrderItemUpdateModel { get; set; }

    public OrderItemFixture()
    {
        OrderItemEntity = new OrderItemEntity
        {
            Id = 1,
            UnitPrice = 10,
            Amount = 2,
            OrderId = 1,
            MealId = 1
        };

        OrderItemCreateModel = new OrderItemCreateModel
        {
            OrderId = 1,
            MealId = 1,
            Amount = 2
        };

        OrderItemDetailModel = new OrderItemDetailModel
        {
            Id = 1,
            UnitPrice = 10,
            Amount = 2,
            OrderId = 1,
            MealId = 1,
        };
        
        OrderItemUpdateModel = new OrderItemUpdateModel
        {
            Id = 1,
            Amount = 2,
        };
    }
}