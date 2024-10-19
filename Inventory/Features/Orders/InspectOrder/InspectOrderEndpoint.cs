using ErrorOr;
using Inventory.Domain.Common.Enums;
using Microsoft.AspNetCore.Http;
using Wolverine;
using Wolverine.Http;

namespace Inventory.Features.Orders.InspectOrder;

public class InspectOrderEndpoint
{
    [Tags("Inventory - Order")]
    [WolverinePost("/inspectOrder")]
    public static async Task<IResult> InspectOrderAsync(Request request, IMessageBus sender)
    {
        var query = new InspectOrderQuery(request.OrderId);

        var result = await sender.InvokeAsync<ErrorOr<InspectOrderQuery.Result>>(query);

        return result.Match(
            value => Results.Ok(
                new Response(
                    new(
                        value.OrderInspection.Id,
                        value.OrderInspection.OrderItems.Select(oi =>
                            new Response.OrderItem(
                                oi.Id, oi.Amount,
                                new(oi.Price.Currency, oi.Price.Value),
                                oi.IsAmountAvailable
                            )
                        ),
                        new(
                            value.OrderInspection.Address.State,
                            value.OrderInspection.Address.City,
                            value.OrderInspection.Address.Code,
                            value.OrderInspection.Address.StreetAndNumber
                        ),
                        value.OrderInspection.Processed
                    )
                )
            ),
            errors => Results.BadRequest(errors.Select(e => e.Code))
        );

    }
    
    public record Request(Guid OrderId);

    public record Response(Response.Order OrderInspection)
    {
        public record Order(Guid Id, IEnumerable<OrderItem> OrderItems, Address Address, DateTime? Processed);

        public record OrderItem(Guid Id, int Amount, OrderItemPrice Price, bool IsAmountAvailable);

        public record OrderItemPrice(Currency Currency, int Value);

        public record Address(State State, string City, string Code, string StreetAndNumber);
    }
}