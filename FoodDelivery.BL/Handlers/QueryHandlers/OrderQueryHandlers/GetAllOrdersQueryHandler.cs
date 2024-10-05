using AutoMapper;
using FoodDelivery.BL.Handlers.QueryHandlers.Base;
using FoodDelivery.BL.Queries.MealQueries;
using FoodDelivery.BL.Queries.OrderQueries;
using FoodDelivery.DAL.EFCore.Entities;
using FoodDelivery.DAL.Entities;
using FoodDelivery.DAL.Infrastructure.QueryObjects.Interfaces.Base;
using FoodDelivery.Shared.Models.MealsModels;
using FoodDelivery.Shared.Models.OrderModels;
using MediatR;

namespace FoodDelivery.BL.Handlers.QueryHandlers.OrderQueryHandlers;

public class GetAllOrdersQueryHandler : QueryHandler<GetAllOrdersQuery, List<OrderListModel>>, IRequestHandler<GetAllOrdersQuery, List<OrderListModel>>
{
    private readonly IQueryObject<OrderEntity> _orderQueryObject;

    public GetAllOrdersQueryHandler(IMapper mapper, IQueryObject<OrderEntity> orderQueryObject) : base(mapper)
    {
        _orderQueryObject = orderQueryObject;
    }

    public override async Task<List<OrderListModel>> Handle(GetAllOrdersQuery request, CancellationToken cancellationToken)
    {
        if (request.Page > 0 && request.PageSize > 0)
            _orderQueryObject.Page(request.Page, request.PageSize);

        var orders = await _orderQueryObject.ExecuteAsync();
        return _mapper.Map<ICollection<OrderListModel>>(orders).ToList();
    }
}
