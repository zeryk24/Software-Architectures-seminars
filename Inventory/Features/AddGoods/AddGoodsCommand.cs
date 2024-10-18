using Inventory.Domain.Goods;
using Inventory.Features.Services;
using ErrorOr;

namespace Inventory.Features.AddGoods;

public record AddGoodsCommand(string Name, int Amount)
{
    public record Result(AddedGoods AddedGoods);

    public record AddedGoods(Guid Id, string Name, int Amount);
}

public class AddGoodsCommandHandler(IRepository<Goods> _repository)
{
    public async Task<ErrorOr<AddGoodsCommand.Result>> HandleAsync(AddGoodsCommand command)
    {
        var goods = Goods.Create(command.Name, command.Amount);

        if (goods.IsError)
            return goods.Errors;
        
        var addedGoods = await _repository.InsertAsync(goods.Value);
        await _repository.CommitAsync();
        
        return new AddGoodsCommand.Result(
            new(
                addedGoods.Id.Value, 
                addedGoods.Name.Value, 
                addedGoods.Amount.UnitsAmount
            )
        );
    }
}