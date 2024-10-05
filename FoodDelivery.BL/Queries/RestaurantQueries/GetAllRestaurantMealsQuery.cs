using FoodDelivery.Shared.Models.MealsModels;
using MediatR;

namespace FoodDelivery.BL.Queries.RestaurantQueries;

public record GetAllRestaurantMealsQuery(int RestaurantId) : IRequest<List<MealListModel>>;

