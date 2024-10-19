using ErrorOr;
using Inventory.Domain.Goods;
using Inventory.Domain.Goods.ValueObjects;
using Inventory.Features.Services;

namespace Inventory.Features.Goods.ModifyGoods;

public record ModifyGoodsCommand(Guid GoodsId, string Name, int Amount)
{
    public record Result(ModifiedGoods Goods);

    public record ModifiedGoods(Guid Id, string Name, int Amount);
}

public class ModifyGoodsCommandHandler(IQueryObject<Domain.Goods.Goods> _queryObject)
{
    public async Task<ErrorOr<ModifyGoodsCommand.Result>> Handle(ModifyGoodsCommand command)
    {
        var goods = (await _queryObject.Filter(g => g.Id == GoodsId.Create(command.GoodsId)).ExecuteAsync())
            .SingleOrDefault();

        if (goods is null)
            return Error.Validation(GoodsErrors.GoodsNotFound);

        var result = goods.Modify(command.Name, command.Amount);

        if (result.IsError)
            return result.Errors;

        return new ModifyGoodsCommand.Result(new(goods.Id.Value, goods.Name.Value, goods.Amount.Value));
    }
}