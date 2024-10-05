using FoodDelivery.Shared.Models.RestaurantModels;
using MediatR;

namespace FoodDelivery.BL.Queries.RestaurantQueries;

public record GetAllRestaurantsQuery(int Page = -1, int PageSize = -1) : IRequest<List<RestaurantListModel>>;

