using ErrorOr;
using FoodDelivery.Contracts;
using Inventory.Domain.Common.Enums;
using Inventory.Domain.Order;
using Inventory.Domain.Order.OrderItem;
using Inventory.Domain.Order.OrderItem.ValueObjects;
using Inventory.Domain.Order.ValueObjects;
using Inventory.Features.Services;

namespace Inventory.Features.Orders.CreateOrder;

public class CreateOrderOrderCompletedMessageHandler(IRepository<Order> _repository)
{
    public async Task<ErrorOr<Success>> Handle(OrderCompletedMessage message)
    {
        var orderAddress = OrderAddress.Create(
            State.Czechia,
            message.CompletedOrder.OrderAddress.City,
            message.CompletedOrder.OrderAddress.Code,
            message.CompletedOrder.OrderAddress.StreetAndNumber);

        var order = Order.Create(
            OrderId.Create(message.CompletedOrder.OrderId), 
            orderAddress, 
            message.CompletedOrder.OrderItems.Select(oi =>
                OrderItem.Create(
                    OrderItemGoods.Create(oi.GoodsId), 
                    OrderItemAmount.Create(oi.Amount).Value,
                    OrderItemPrice.Create(oi.CzkPrice, Currency.Czk).Value)
            )
        );

        await _repository.InsertAsync(order);
        await _repository.CommitAsync();
        
        return Result.Success;
    }
}