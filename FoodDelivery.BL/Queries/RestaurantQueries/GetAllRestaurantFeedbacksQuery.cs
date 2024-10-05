using FoodDelivery.Shared.Models.FeedbacksModels;
using MediatR;

namespace FoodDelivery.BL.Queries.RestaurantQueries;

public record GetAllRestaurantFeedbacksQuery(int RestaurantId) : IRequest<List<FeedbackListModel>>;
