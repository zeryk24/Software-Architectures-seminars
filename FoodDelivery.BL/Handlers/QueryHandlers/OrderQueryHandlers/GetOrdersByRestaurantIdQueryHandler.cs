using AutoMapper;
using FoodDelivery.BL.Handlers.QueryHandlers.Base;
using FoodDelivery.BL.Queries.MealQueries;
using FoodDelivery.BL.Queries.OrderQueries;
using FoodDelivery.DAL.EFCore.Entities;
using FoodDelivery.DAL.Entities;
using FoodDelivery.DAL.Infrastructure.QueryObjects.Interfaces;
using FoodDelivery.DAL.Infrastructure.QueryObjects.Interfaces.Base;
using FoodDelivery.Shared.Models.MealsModels;
using FoodDelivery.Shared.Models.OrderModels;
using MediatR;

namespace FoodDelivery.BL.Handlers.QueryHandlers.OrderQueryHandlers;

public class GetOrdersByRestaurantIdQueryHandler : QueryHandler<GetOrdersByRestaurantIdQuery, List<OrderListModel>>, IRequestHandler<GetOrdersByRestaurantIdQuery, List<OrderListModel>>
{
    private readonly IGetOrdersByRestaurantId<OrderEntity> _orderQueryObject;

    public GetOrdersByRestaurantIdQueryHandler(IMapper mapper, IGetOrdersByRestaurantId<OrderEntity> orderQueryObject) : base(mapper)
    {
        _orderQueryObject = orderQueryObject;
    }

    public override async Task<List<OrderListModel>> Handle(GetOrdersByRestaurantIdQuery request, CancellationToken cancellationToken)
    {
        _orderQueryObject.UseFilter(request.RestaurantId);
        if (request.Page > 0 && request.PageSize > 0)
            _orderQueryObject.Page(request.Page, request.PageSize);
        
        var orders = await _orderQueryObject.ExecuteAsync();
        return _mapper.Map<ICollection<OrderListModel>>(orders).ToList();
    }
}
