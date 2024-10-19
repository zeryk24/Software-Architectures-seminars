using Microsoft.AspNetCore.Http;
using Wolverine;
using Wolverine.Http;
using ErrorOr;

namespace Inventory.Features.Goods.ModifyGoods;

public class ModifyGoodsEndpoint
{
    [Tags("Inventory - Goods")]
    [WolverinePost("/modifyGoods")]
    public static async Task<IResult> ModifyGoodsAsync(Request request, IMessageBus sender)
    {
        var command = new ModifyGoodsCommand(request.GoodsId, request.Name, request.Amount);

        var result = await sender.InvokeAsync<ErrorOr<ModifyGoodsCommand.Result>>(command);

        return result.Match(
            value => Results.Ok(
                new Response(
                    new(
                        value.Goods.Id,
                        value.Goods.Name,
                        value.Goods.Amount
                    )
                )
            ),
            errors => Results.BadRequest(errors.Select(e => e.Code))
        );
    }

    
    public record Request(Guid GoodsId, string Name, int Amount);

    public record Response(Response.ModifiedGoods Goods)
    {
        public record ModifiedGoods(Guid Id, string Name, int Amount);
    }
}