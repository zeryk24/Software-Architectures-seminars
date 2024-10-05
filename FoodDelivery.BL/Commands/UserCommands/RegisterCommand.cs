using FoodDelivery.Shared.Models.RestaurantModels;
using FoodDelivery.Shared.Models.UserModels;
using MediatR;

namespace FoodDelivery.BL.Commands.UserCommands;

public record RegisterCommand(RegisterModel RegisterModel) : IRequest<UserManagerResponseModel>;

