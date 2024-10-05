using AutoMapper;
using FoodDelivery.BL.Handlers.QueryHandlers.Base;
using FoodDelivery.BL.Queries.OrderItemQueries;
using FoodDelivery.BL.Queries.OrderQueries;
using FoodDelivery.DAL.EFCore.Entities;
using FoodDelivery.DAL.Entities;
using FoodDelivery.DAL.Infrastructure.QueryObjects.Interfaces.Base;
using FoodDelivery.Shared.Models.MealsModels;
using FoodDelivery.Shared.Models.OrderItemsModels;
using FoodDelivery.Shared.Models.OrderModels;
using MediatR;

namespace FoodDelivery.BL.Handlers.QueryHandlers.OrderItemQueryHandlers;

public class GetAllOrderItemsQueryHandler : QueryHandler<GetAllOrderItemsQuery, List<OrderItemListModel>>, IRequestHandler<GetAllOrderItemsQuery, List<OrderItemListModel>>
{
    private readonly IQueryObject<OrderItemEntity> _orderItemQueryObject;

    public GetAllOrderItemsQueryHandler(IMapper mapper, IQueryObject<OrderItemEntity> orderItemQueryObject) : base(mapper)
    {
        _orderItemQueryObject = orderItemQueryObject;
    }

    public override async Task<List<OrderItemListModel>> Handle(GetAllOrderItemsQuery request, CancellationToken cancellationToken)
    {
        if (request.Page > 0 && request.PageSize > 0)
            _orderItemQueryObject.Page(request.Page, request.PageSize);

        var orderItems = await _orderItemQueryObject.ExecuteAsync();
        return _mapper.Map<ICollection<OrderItemListModel>>(orderItems).ToList();
    }
}
