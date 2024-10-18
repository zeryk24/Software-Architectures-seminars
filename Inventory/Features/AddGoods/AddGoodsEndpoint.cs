using ErrorOr;
using Microsoft.AspNetCore.Http;
using Wolverine;
using Wolverine.Http;

namespace Inventory.Features.AddGoods;

public class AddGoodsEndpoint
{
    [Tags("Inventory - Goods")]
    [WolverinePost("/addGoods")]
    public static async Task<IResult> AddGoodsAsync(Request request, IMessageBus sender)
    {
        var command = new AddGoodsCommand(request.Name, request.Amount);

        var result = await sender.InvokeAsync<ErrorOr<AddGoodsCommand.Result>>(command);

        return result.Match(
            value => Results.Ok(
                new Response(
                    new(
                        value.AddedGoods.Id,
                        value.AddedGoods.Name,
                        value.AddedGoods.Amount
                    )
                )
            ),
            errors => Results.BadRequest(errors.Select(e => e.Code))
        );
    }


    public record Request(string Name, int Amount);

    public record Response(Response.AddedGoods Goods)
    {
        public record AddedGoods(Guid Id, string Name, int Amount);
    }

}