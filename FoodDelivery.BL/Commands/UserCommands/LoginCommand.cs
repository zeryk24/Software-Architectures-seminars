using FoodDelivery.Shared.Models.UserModels;
using MediatR;

namespace FoodDelivery.BL.Commands.UserCommands;

public record LoginCommand(LoginModel LoginModel) : IRequest<UserManagerResponseModel>;

