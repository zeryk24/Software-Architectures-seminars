using AutoMapper;
using FoodDelivery.BL.Handlers.QueryHandlers.Base;
using FoodDelivery.BL.Queries.MealQueries;
using FoodDelivery.BL.Queries.RestaurantQueries;
using FoodDelivery.DAL.EFCore.Entities;
using FoodDelivery.DAL.EFCore.QueryObjects;
using FoodDelivery.DAL.Entities;
using FoodDelivery.DAL.Infrastructure.QueryObjects.Interfaces;
using FoodDelivery.Shared.Models.FeedbacksModels;
using FoodDelivery.Shared.Models.MealsModels;
using MediatR;

namespace FoodDelivery.BL.Handlers.QueryHandlers.RestaurantQueryHandlers;

public class GetAllRestaurantMealsQueryHandler : QueryHandler<GetAllRestaurantMealsQuery, List<MealListModel>>, IRequestHandler<GetAllRestaurantMealsQuery, List<MealListModel>>
{
    private IGetAllRestaurantMealsQueryObject<MealEntity> _getAllRestaurantMealsQueryObject;
    public GetAllRestaurantMealsQueryHandler(IGetAllRestaurantMealsQueryObject<MealEntity> getAllRestaurantMealsQueryObject, IMapper mapper) : base(mapper)
    {
        _getAllRestaurantMealsQueryObject = getAllRestaurantMealsQueryObject;
    }

    public override async Task<List<MealListModel>> Handle(GetAllRestaurantMealsQuery request, CancellationToken cancellationToken)
    {
        var meals = await _getAllRestaurantMealsQueryObject.UseFilter(request.RestaurantId).ExecuteAsync();
        return _mapper.Map<ICollection<MealListModel>>(meals).ToList();
    }
}
