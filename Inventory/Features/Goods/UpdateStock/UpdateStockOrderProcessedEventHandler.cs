using ErrorOr;
using Inventory.Domain.Goods;
using Inventory.Domain.Goods.ValueObjects;
using Inventory.Domain.Order.DomainEvents;
using Inventory.Features.Services;

namespace Inventory.Features.Goods.UpdateStock;

public class UpdateStockOrderProcessedEventHandler(IQueryObject<Domain.Goods.Goods> _queryObject)
{
    public async Task<ErrorOr<Success>> Handle(OrderProcessedDomainEvent domainEvent)
    {
        var errors = new List<Error>();
        var goodsIds = domainEvent.Order.OrderItems.Select(item => GoodsId.Create(item.Goods.Id)).ToList();
        
        var goodsList = await _queryObject.Filter(g => goodsIds.Contains(g.Id)).ExecuteAsync();
        var goodsDict = goodsList.ToDictionary(g => g.Id);

        foreach (var orderItem in domainEvent.Order.OrderItems)
        {
            if (!goodsDict.TryGetValue(GoodsId.Create(orderItem.Goods.Id), out var goods))
            {
                errors.Add(Error.Validation(GoodsErrors.GoodsNotFound, orderItem.Goods.Id.ToString()));
                continue;
            }

            goods.DecreaseAmount(orderItem.Amount.Value);
        }

        if (errors.Any())
            return errors;

        return Result.Success;
    }
}