using FoodDelivery.Shared.Models.FeedbacksModels;
using MediatR;

namespace FoodDelivery.BL.Queries.FeedbackQueries;

public record GetAllFeedbacksQuery(int Page = -1, int PageSize = -1) : IRequest<List<FeedbackListModel>>;

