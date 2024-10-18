using ErrorOr;
using Microsoft.AspNetCore.Http;
using Wolverine;
using Wolverine.Http;

namespace Inventory.Features.Goods.RestockGoods;

public class RestockGoodsEndpoint
{
    [Tags("Inventory - Goods")]
    [WolverinePost("/restockGoods")]
    public static async Task<IResult> RestockGoodsAsync(Request request, IMessageBus sender)
    {
        var command = new RestockGoodsCommand(request.GoodsId, request.Amount);

        var result = await sender.InvokeAsync<ErrorOr<RestockGoodsCommand.Result>>(command);

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

    public record Request(Guid GoodsId, int Amount);

    public record Response(Response.RestockedGoods Goods)
    {
        public record RestockedGoods(Guid Id, string Name, int Amount);
    }
}