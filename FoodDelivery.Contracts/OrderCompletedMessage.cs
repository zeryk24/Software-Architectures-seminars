namespace FoodDelivery.Contracts;

public record OrderCompletedMessage(OrderCompletedMessage.Order CompletedOrder)
{
    public record Order(Guid OrderId, IEnumerable<OrderItem> OrderItems, Address OrderAddress);
    
    public record OrderItem(Guid GoodsId, int Amount, double CzkPrice);

    public record Address(string City, string Code, string StreetAndNumber);
}

