using ErrorOr;
using Inventory.Domain.Order;
using Inventory.Domain.Order.ValueObjects;
using Inventory.Features.Services;

namespace Inventory.Features.Orders.JustProcessed;

public record OrderJustProcessedCommand(Guid OrderId)
{
    public record Result(DateTime ProcessedAt);
}

public class OrderJustProcessedCommandHandler(
    IQueryObject<Order> _queryObject,
    IRepository<Order> _repository,
    IDateTimeProvider _dateTimeProvider)
{
    public async Task<ErrorOr<OrderJustProcessedCommand.Result>> Handle(OrderJustProcessedCommand command)
    {
        var order = (await _queryObject.Filter(o => o.Id == OrderId.Create(command.OrderId)).ExecuteAsync())
            .SingleOrDefault();

        if (order is null)
            return Error.Validation(OrderErrors.OrderNotFound);

        var now = _dateTimeProvider.UtcNow;
        var result =  order.JustProcessed(now);

        if (result.IsError)
            return result.Errors;
        
        _repository.Update(order);
        await _repository.CommitAsync();

        return new OrderJustProcessedCommand.Result(now);
    }
}