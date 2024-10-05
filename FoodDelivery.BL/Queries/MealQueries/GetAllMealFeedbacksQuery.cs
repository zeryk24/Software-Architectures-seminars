using FoodDelivery.Shared.Models.FeedbacksModels;
using MediatR;

namespace FoodDelivery.BL.Queries.MealQueries;

public record GetAllMealFeedbacksQuery(int MealId) : IRequest<List<FeedbackListModel>>;
