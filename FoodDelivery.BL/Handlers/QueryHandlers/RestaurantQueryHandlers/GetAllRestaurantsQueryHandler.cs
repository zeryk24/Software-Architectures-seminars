using AutoMapper;
using FoodDelivery.BL.Handlers.QueryHandlers.Base;
using FoodDelivery.BL.Queries.MealQueries;
using FoodDelivery.BL.Queries.RestaurantQueries;
using FoodDelivery.DAL.EFCore.Entities;
using FoodDelivery.DAL.Entities;
using FoodDelivery.DAL.Infrastructure.QueryObjects.Interfaces.Base;
using FoodDelivery.Shared.Models.MealsModels;
using FoodDelivery.Shared.Models.RestaurantModels;
using MediatR;

namespace FoodDelivery.BL.Handlers.QueryHandlers.RestaurantQueryHandlers;

public class GetAllRestaurantsQueryHandler : QueryHandler<GetAllRestaurantsQuery, List<RestaurantListModel>>, IRequestHandler<GetAllRestaurantsQuery, List<RestaurantListModel>>
{
    private readonly IQueryObject<RestaurantEntity> _restaurantQueryObject;

    public GetAllRestaurantsQueryHandler(IMapper mapper, IQueryObject<RestaurantEntity> restaurantQueryObject) : base(mapper)
    {
        _restaurantQueryObject = restaurantQueryObject;
    }

    public override async Task<List<RestaurantListModel>> Handle(GetAllRestaurantsQuery request, CancellationToken cancellationToken)
    {
        if (request.Page > 0 && request.PageSize > 0)
            _restaurantQueryObject.Page(request.Page, request.PageSize);

        var restaurants = await _restaurantQueryObject.ExecuteAsync();
        return _mapper.Map<ICollection<RestaurantListModel>>(restaurants).ToList();
    }
}
