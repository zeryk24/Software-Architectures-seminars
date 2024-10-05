using AutoMapper;
using FoodDelivery.BL.Handlers.QueryHandlers.Base;
using FoodDelivery.BL.Queries.MealQueries;
using FoodDelivery.DAL.EFCore.Entities;
using FoodDelivery.DAL.Entities;
using FoodDelivery.DAL.Infrastructure.QueryObjects.Interfaces;
using FoodDelivery.Shared.Models.FeedbacksModels;
using MediatR;

namespace FoodDelivery.BL.Handlers.QueryHandlers.MealQueryHandlers;

public class GetAllMealFeedbackQueryHandler : QueryHandler<GetAllMealFeedbacksQuery, List<FeedbackListModel>>, IRequestHandler<GetAllMealFeedbacksQuery, List<FeedbackListModel>>
{
    private IGetAllMealFeedbacksQueryObject<FeedbackEntity> _getAllMealFeedbacksQueryObject;
    public GetAllMealFeedbackQueryHandler(IGetAllMealFeedbacksQueryObject<FeedbackEntity> getAllMealFeedbacksQueryObject, IMapper mapper) : base(mapper)
    {
        _getAllMealFeedbacksQueryObject = getAllMealFeedbacksQueryObject;
    }

    public override async Task<List<FeedbackListModel>> Handle(GetAllMealFeedbacksQuery request, CancellationToken cancellationToken)
    {
        var feedbacks = await _getAllMealFeedbacksQueryObject.UseFilter(request.MealId).ExecuteAsync();
        return _mapper.Map<ICollection<FeedbackListModel>>(feedbacks).ToList();
    }
}
