using ErrorOr;
using Inventory.Features.Services;

namespace Inventory.Features.Goods.AddGoods;

public record AddGoodsCommand(string Name, int Amount)
{
    public record Result(AddedGoods Goods);

    public record AddedGoods(Guid Id, string Name, int Amount);
}

public class AddGoodsCommandHandler(IRepository<Domain.Goods.Goods> _repository)
{
    public async Task<ErrorOr<AddGoodsCommand.Result>> HandleAsync(AddGoodsCommand command)
    {
        var goods = Domain.Goods.Goods.Create(command.Name, command.Amount);

        if (goods.IsError)
            return goods.Errors;
        
        var addedGoods = await _repository.InsertAsync(goods.Value);
        await _repository.CommitAsync();
        
        return new AddGoodsCommand.Result(
            new(
                addedGoods.Id.Value, 
                addedGoods.Name.Value, 
                addedGoods.Amount.Value
            )
        );
    }
}