using FoodDelivery.Shared.Models.MealsModels;
using MediatR;

namespace FoodDelivery.BL.Queries.MealQueries;

public record GetMealsByTypeQuery(string MealType) : IRequest<List<MealListModel>>;

