using ErrorOr;
using Packing.Application.Services;
using Packing.Domain.Order.OrderItem;
using Packing.Domain.Order.OrderItem.ValueObjects;
using Packing.Domain.Order.ValueObjects;

namespace Packing.Application.Order;

public record CreateOrderCommand(CreateOrderCommand.Order NewOrder)
{
    public record Result();
    
    public record Order(Guid Id, IEnumerable<OrderItem> OrderItems);
    public record OrderItem(Guid Id, Guid GoodsId, int Amount);
}

public class CreateOrderCommandHandler(IRepository<Domain.Order.Order> _repository)
{
    public async Task<ErrorOr<CreateOrderCommand.Result>> Handle(CreateOrderCommand command)
    {
        var order = Domain.Order.Order.Create(
            OrderId.Create(command.NewOrder.Id),
            command.NewOrder.OrderItems.Select(oi => OrderItem.Create(
                    oi.Id,
                    oi.GoodsId, oi.Amount
                ).Value
            )
        );

        await _repository.InsertAsync(order);
        await _repository.CommitAsync();

        return new CreateOrderCommand.Result();
    }
}