using AutoMapper;
using FoodDelivery.BL.Handlers.QueryHandlers.Base;
using FoodDelivery.BL.Queries.MealQueries;
using FoodDelivery.DAL.EFCore.Entities;
using FoodDelivery.DAL.Entities;
using FoodDelivery.DAL.Infrastructure.QueryObjects.Interfaces.Base;
using FoodDelivery.Shared.Models.FeedbacksModels;
using FoodDelivery.Shared.Models.MealsModels;
using MediatR;

namespace FoodDelivery.BL.Handlers.QueryHandlers.MealQueryHandlers;

public class GetAllMealsQueryHandler : QueryHandler<GetAllMealsQuery, List<MealListModel>>, IRequestHandler<GetAllMealsQuery, List<MealListModel>>
{
    private readonly IQueryObject<MealEntity> _mealQueryObject;

    public GetAllMealsQueryHandler(IMapper mapper, IQueryObject<MealEntity> mealQueryObject) : base(mapper)
    {
        _mealQueryObject = mealQueryObject;
    }

    public override async Task<List<MealListModel>> Handle(GetAllMealsQuery request, CancellationToken cancellationToken)
    {
        if (request.Page > 0 && request.PageSize > 0)
            _mealQueryObject.Page(request.Page, request.PageSize);

        var meals = await _mealQueryObject.ExecuteAsync();
        return _mapper.Map<ICollection<MealListModel>>(meals).ToList();
    }
}
