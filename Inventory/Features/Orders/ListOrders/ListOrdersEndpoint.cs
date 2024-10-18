using ErrorOr;
using Microsoft.AspNetCore.Http;
using Wolverine;
using Wolverine.Http;

namespace Inventory.Features.Orders.ListOrders;

public class ListOrdersEndpoint
{
    [Tags("Inventory - Order")]
    [WolverinePost("/listOrders")]
    public static async Task<IResult> ListOrdersAsync(Request request, IMessageBus sender)
    {
        var query = new ListOrdersQuery(request.Page, request.PageSize);

        var result = await sender.InvokeAsync<ErrorOr<ListOrdersQuery.Result>>(query);
        
        return result.Match(
            value => Results.Ok(
                new Response(
                    value.Orders.Select(o => new Response.Order(o.Id, o.ItemsCount))
                )
            ),
            errors => Results.BadRequest(errors.Select(e => e.Code))
        );

    }
    
    public record Request(int Page, int PageSize);

    public record Response(IEnumerable<Response.Order> Orders)
    {
        public record Order(Guid Id, int ItemsCount);
    }
}