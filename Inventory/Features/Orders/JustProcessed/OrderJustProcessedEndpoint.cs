using ErrorOr;
using Microsoft.AspNetCore.Http;
using Wolverine;
using Wolverine.Http;

namespace Inventory.Features.Orders.JustProcessed;

public class OrderJustProcessedEndpoint
{
    [Tags("Inventory - Order")]
    [WolverinePost("/orderJustProcessed")]
    public static async Task<IResult> AddGoodsAsync(Request request, IMessageBus sender)
    {
        var command = new OrderJustProcessedCommand(request.OrderId);

        var result = await sender.InvokeAsync<ErrorOr<OrderJustProcessedCommand.Result>>(command);
        
        return result.Match(
            value => Results.Ok(
                new Response(
                    value.ProcessedAt
                )
            ),
            errors => Results.BadRequest(errors.Select(e => e.Code))
        );
    }

    public record Request(Guid OrderId);

    public record Response(DateTime ProcessedAt);
}