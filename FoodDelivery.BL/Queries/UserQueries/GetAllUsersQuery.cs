using FoodDelivery.Shared.Models.RestaurantModels;
using FoodDelivery.Shared.Models.UserModels;
using MediatR;

namespace FoodDelivery.BL.Queries.UserQueries;

public record GetAllUsersQuery(int Page = -1, int PageSize = -1) : IRequest<List<UserListModel>>;
