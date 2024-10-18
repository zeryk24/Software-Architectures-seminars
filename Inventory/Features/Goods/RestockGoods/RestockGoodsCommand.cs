using Inventory.Features.Services;
using ErrorOr;
using Inventory.Domain.Goods;
using Inventory.Domain.Goods.ValueObjects;

namespace Inventory.Features.Goods.RestockGoods;

public record RestockGoodsCommand(Guid GoodsId, int Amount)
{
    public record Result(RestockedGoods Goods);
    
    public record RestockedGoods(Guid Id, string Name, int Amount);
}

public class RestockGoodsCommandHandler(
    IQueryObject<Domain.Goods.Goods> _queryObject,
    IRepository<Domain.Goods.Goods> _repository
    )
{
    public async Task<ErrorOr<RestockGoodsCommand.Result>> HandleAsync(RestockGoodsCommand command)
    {
        var goods = (await _queryObject.Filter(g => g.Id == GoodsId.Create(command.GoodsId)).ExecuteAsync()).SingleOrDefault();

        if (goods is null)
            return Error.Validation(GoodsErrors.GoodsNotFound);

        var result = goods.Amount.Restock(command.Amount);

        if (result.IsError)
            return result.Errors;
        
        _repository.Update(goods);
        await _repository.CommitAsync();

        return new RestockGoodsCommand.Result(
            new(
                goods.Id.Value,
                goods.Name.Value,
                goods.Amount.UnitsAmount
            )
        );
    }
}