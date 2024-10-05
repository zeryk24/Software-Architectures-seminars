using AutoMapper;
using FoodDelivery.BL.Handlers.QueryHandlers.Base;
using FoodDelivery.BL.Queries.MealQueries;
using FoodDelivery.DAL.EFCore.Entities;
using FoodDelivery.DAL.EFCore.QueryObjects;
using FoodDelivery.DAL.Entities;
using FoodDelivery.DAL.Infrastructure.QueryObjects.Interfaces;
using FoodDelivery.Shared.Models.MealsModels;
using MediatR;

namespace FoodDelivery.BL.Handlers.QueryHandlers.MealQueryHandlers;

public class GetMealsByNameQueryHandler : QueryHandler<GetMealsByNameQuery, List<MealListModel>>, IRequestHandler<GetMealsByNameQuery, List<MealListModel>>
{
    private IGetMealsByNameQueryObject<MealEntity> _getMealsByNameQueryObject;
    public GetMealsByNameQueryHandler(IGetMealsByNameQueryObject<MealEntity> getMealsByNameQueryObject, IMapper mapper) : base(mapper)
    {
        _getMealsByNameQueryObject = getMealsByNameQueryObject;
    }

    public override async Task<List<MealListModel>> Handle(GetMealsByNameQuery request, CancellationToken cancellationToken)
    {
        var meals = await _getMealsByNameQueryObject.UseFilter(request.Name).ExecuteAsync();
        return _mapper.Map<ICollection<MealListModel>>(meals).ToList();
    }
}
