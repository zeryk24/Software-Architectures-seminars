using AutoMapper;
using FoodDelivery.BL.Handlers.QueryHandlers.Base;
using FoodDelivery.BL.Queries.MealQueries;
using FoodDelivery.DAL.EFCore.Entities;
using FoodDelivery.DAL.EFCore.QueryObjects;
using FoodDelivery.DAL.Entities;
using FoodDelivery.DAL.Infrastructure.QueryObjects.Interfaces;
using FoodDelivery.Shared.Models.FeedbacksModels;
using FoodDelivery.Shared.Models.MealsModels;
using MediatR;

namespace FoodDelivery.BL.Handlers.QueryHandlers.MealQueryHandlers;

internal class GetMealsByTypeQueryHandler : QueryHandler<GetMealsByTypeQuery, List<MealListModel>>, IRequestHandler<GetMealsByTypeQuery, List<MealListModel>>
{
    private IGetMealsByTypeQueryObject<MealEntity> _getMealsByTypeQueryObject;
    public GetMealsByTypeQueryHandler(IGetMealsByTypeQueryObject<MealEntity> getMealsByTypeQueryObject, IMapper mapper) : base(mapper)
    {
        _getMealsByTypeQueryObject = getMealsByTypeQueryObject;
    }

    public override async Task<List<MealListModel>> Handle(GetMealsByTypeQuery request, CancellationToken cancellationToken)
    {
        var meals = await _getMealsByTypeQueryObject.UseFilter(request.MealType).ExecuteAsync();
        return _mapper.Map<ICollection<MealListModel>>(meals).ToList();
    }
}
