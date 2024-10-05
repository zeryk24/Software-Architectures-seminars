using FoodDelivery.Shared.Models.AddressModels;
using FoodDelivery.Shared.Models.MealsModels;
using MediatR;

namespace FoodDelivery.BL.Commands.AddressCommands;

public record CreateAddressCommand(AddressCreateModel AddressCreateModel) : IRequest<AddressDetailModel>;

