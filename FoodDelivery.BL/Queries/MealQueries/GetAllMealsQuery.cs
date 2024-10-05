using FoodDelivery.Shared.Models.MealsModels;
using MediatR;

namespace FoodDelivery.BL.Queries.MealQueries;

public record GetAllMealsQuery(int Page = -1, int PageSize = -1) : IRequest<List<MealListModel>>;

