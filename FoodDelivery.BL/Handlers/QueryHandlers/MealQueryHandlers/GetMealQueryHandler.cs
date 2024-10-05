using AutoMapper;
using FoodDelivery.BL.Handlers.QueryHandlers.Base;
using FoodDelivery.BL.Queries.MealQueries;
using FoodDelivery.DAL.EFCore.Entities;
using FoodDelivery.DAL.EFCore.UnitOfWork;
using FoodDelivery.DAL.Entities;
using FoodDelivery.DAL.Infrastructure.QueryObjects.Interfaces;
using FoodDelivery.DAL.Infrastructure.UnitOfWork.Interfaces;
using FoodDelivery.Shared.Models.FeedbacksModels;
using FoodDelivery.Shared.Models.MealsModels;
using MediatR;

namespace FoodDelivery.BL.Handlers.QueryHandlers.MealQueryHandlers;

public class GetMealQueryHandler : QueryHandler<GetMealQuery, MealDetailModel>, IRequestHandler<GetMealQuery, MealDetailModel>
{
    private readonly IUnitOfWorkProvider<IEFCoreUnitOfWork> _unitOfWorkProvider;
    private readonly IGetAllMealFeedbacksQueryObject<FeedbackEntity> _getAllMealFeedbacksQueryObject;
    
    public GetMealQueryHandler(IUnitOfWorkProvider<IEFCoreUnitOfWork> unitOfWorkProvider,
        IGetAllMealFeedbacksQueryObject<FeedbackEntity> getAllMealFeedbacksQueryObject, IMapper mapper) : base(mapper)
    {
        _unitOfWorkProvider = unitOfWorkProvider;
        _getAllMealFeedbacksQueryObject = getAllMealFeedbacksQueryObject;
    }

    public override async Task<MealDetailModel> Handle(GetMealQuery request, CancellationToken cancellationToken)
    {
        using var unitOfWork = _unitOfWorkProvider.Create();
        var meal = await unitOfWork.MealRepository.GetByIdAsync(request.mealId);
        var feedbacks  = await _getAllMealFeedbacksQueryObject.UseFilter(meal.Id).ExecuteAsync();
        meal.Feedbacks = feedbacks.ToList();

        return _mapper.Map<MealDetailModel>(meal);
    }
}