using AutoMapper;
using FoodDelivery.BL.Handlers.QueryHandlers.Base;
using FoodDelivery.BL.Queries.MealQueries;
using FoodDelivery.BL.Queries.RestaurantQueries;
using FoodDelivery.DAL.EFCore.Entities;
using FoodDelivery.DAL.EFCore.QueryObjects;
using FoodDelivery.DAL.Entities;
using FoodDelivery.DAL.Infrastructure.QueryObjects.Interfaces;
using FoodDelivery.Shared.Models.FeedbacksModels;
using MediatR;

namespace FoodDelivery.BL.Handlers.QueryHandlers.RestaurantQueryHandlers;

public class GetAllRestaurantFeedbacksQueryHandler : QueryHandler<GetAllRestaurantFeedbacksQuery, List<FeedbackListModel>>, IRequestHandler<GetAllRestaurantFeedbacksQuery, List<FeedbackListModel>>
{
    private IGetAllRestaurantFeedbacksQueryObject<FeedbackEntity> _getAllRestaurantFeedbacksQueryObject;
    public GetAllRestaurantFeedbacksQueryHandler(IGetAllRestaurantFeedbacksQueryObject<FeedbackEntity> getAllRestaurantFeedbacksQueryObject, IMapper mapper) : base(mapper)
    {
        _getAllRestaurantFeedbacksQueryObject = getAllRestaurantFeedbacksQueryObject;
    }

    public override async Task<List<FeedbackListModel>> Handle(GetAllRestaurantFeedbacksQuery request, CancellationToken cancellationToken)
    {
        var feedbacks = await _getAllRestaurantFeedbacksQueryObject.UseFilter(request.RestaurantId).ExecuteAsync();
        return _mapper.Map<ICollection<FeedbackListModel>>(feedbacks).ToList();
    }
}
