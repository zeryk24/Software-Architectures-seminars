using ErrorOr;
using Inventory.Domain.Order;
using Inventory.Features.Services;

namespace Inventory.Features.Orders.ListOrders;

public record ListOrdersQuery(int Page, int PageSize)
{
    public record Result(IEnumerable<Order> Orders);
    
    public record Order(Guid Id, int ItemsCount);
}

public class ListOrdersQueryHandler(IQueryObject<Order> _queryObject)
{
    public async Task<ErrorOr<ListOrdersQuery.Result>> Handle(ListOrdersQuery query)
    {
        var orders = await _queryObject.Page(query.Page, query.PageSize).ExecuteAsync();

        var result = orders.Select(o => new ListOrdersQuery.Order(o.Id.Value, o.OrderItems.Count));

        return new ListOrdersQuery.Result(result);
    }
}
