using FoodDelivery.Shared.Models.MealsModels;
using MediatR;

namespace FoodDelivery.BL.Queries.MealQueries;

public record GetMealQuery(int mealId) : IRequest<MealDetailModel>;