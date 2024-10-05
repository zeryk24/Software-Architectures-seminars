using System.Security.Claims;
using FoodDelivery.Shared.Models.UserModels;
using MediatR;

namespace FoodDelivery.BL.Commands.UserCommands;

public record UpdateUserCommand(UserUpdateModel UserUpdateModel, ClaimsPrincipal User) : IRequest<UserManagerResponseModel>;

