using System.Security.Claims;
using FoodDelivery.Shared.Models.AddressModels;
using MediatR;

namespace FoodDelivery.BL.Commands.AddressCommands;

public record UpdateCustomerAddressCommand(AddressUpdateModel AddressUpdateModel, ClaimsPrincipal User) : IRequest<AddressDetailModel>;
