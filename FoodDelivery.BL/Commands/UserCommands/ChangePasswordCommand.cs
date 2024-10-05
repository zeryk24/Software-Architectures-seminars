using System.Security.Claims;
using FoodDelivery.Shared.Models.UserModels;
using MediatR;

namespace FoodDelivery.BL.Commands.UserCommands;

public record ChangePasswordCommand(UserChangePasswordModel UserChangePasswordModel, ClaimsPrincipal User) : IRequest<UserManagerResponseModel>;