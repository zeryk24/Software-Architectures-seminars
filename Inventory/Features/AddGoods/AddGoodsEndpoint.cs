using Microsoft.AspNetCore.Http;
using Wolverine;
using Wolverine.Http;

namespace Inventory.Features.AddGoods;

public class AddGoodsEndpoint
{
    [Tags("Inventory - Goods")]
    [WolverinePost("/addGoods")]
    public static async Task<Response> AddGoodsAsync(Request request, IMessageBus sender)
    {
        var command = new AddGoodsCommand();

        var result = await sender.InvokeAsync<AddGoodsCommand.Result>(command);
        
        return new Response();
    }


    public record Request();

    public record Response();

}