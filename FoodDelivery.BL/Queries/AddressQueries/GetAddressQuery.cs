using FoodDelivery.Shared.Models.AddressModels;
using MediatR;

namespace FoodDelivery.BL.Queries.AddressQueries;

public record GetAddressQuery(int AddressId) : IRequest<AddressDetailModel>;

