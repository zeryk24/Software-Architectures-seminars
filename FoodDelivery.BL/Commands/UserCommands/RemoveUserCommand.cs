using FoodDelivery.Shared.Models.UserModels;
using MediatR;

namespace FoodDelivery.BL.Commands.UserCommands;

public record RemoveUserCommand(int UserId) : IRequest<UserManagerResponseModel>;