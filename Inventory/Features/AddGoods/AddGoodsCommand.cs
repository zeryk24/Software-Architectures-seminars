namespace Inventory.Features.AddGoods;

public record AddGoodsCommand()
{
    public record Result();
}

public class AddGoodsCommandHandler
{
    public async Task<AddGoodsCommand.Result> HandleAsync(AddGoodsCommand command)
    {
        
        
        return new AddGoodsCommand.Result();
    }
}