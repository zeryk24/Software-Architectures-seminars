using FoodDelivery.Shared.Models.AddressModels;
using MediatR;

namespace FoodDelivery.BL.Queries.AddressQueries;

public record GetAllAddressesQuery(int Page = -1, int PageSize = -1) : IRequest<List<AddressListModel>>;

