using FoodDelivery.Shared.Models.MealsModels;
using MediatR;

namespace FoodDelivery.BL.Queries.MealQueries;

public record GetMealsByNameQuery(string Name) : IRequest<List<MealListModel>>;

