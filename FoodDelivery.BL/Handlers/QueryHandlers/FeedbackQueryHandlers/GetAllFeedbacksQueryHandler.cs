using AutoMapper;
using FoodDelivery.BL.Handlers.QueryHandlers.Base;
using FoodDelivery.BL.Queries.FeedbackQueries;
using FoodDelivery.BL.Queries.OrderItemQueries;
using FoodDelivery.DAL.EFCore.Entities;
using FoodDelivery.DAL.Entities;
using FoodDelivery.DAL.Infrastructure.QueryObjects.Interfaces.Base;
using FoodDelivery.Shared.Models.FeedbacksModels;
using FoodDelivery.Shared.Models.MealsModels;
using FoodDelivery.Shared.Models.OrderItemsModels;
using MediatR;

namespace FoodDelivery.BL.Handlers.QueryHandlers.FeedbackQueryHandlers;

public class GetAllFeedbacksQueryHandler : QueryHandler<GetAllFeedbacksQuery, List<FeedbackListModel>>, IRequestHandler<GetAllFeedbacksQuery, List<FeedbackListModel>>
{
    private readonly IQueryObject<FeedbackEntity> _feedbackQueryObject;

    public GetAllFeedbacksQueryHandler(IMapper mapper, IQueryObject<FeedbackEntity> feedbackQueryObject) : base(mapper)
    {
        _feedbackQueryObject = feedbackQueryObject;
    }

    public override async Task<List<FeedbackListModel>> Handle(GetAllFeedbacksQuery request, CancellationToken cancellationToken)
    {
        if (request.Page > 0 && request.PageSize > 0)
            _feedbackQueryObject.Page(request.Page, request.PageSize);

        var feedbacks = await _feedbackQueryObject.ExecuteAsync();
        return _mapper.Map<ICollection<FeedbackListModel>>(feedbacks).ToList();
    }
}
