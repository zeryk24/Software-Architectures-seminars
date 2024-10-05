using AutoMapper;
using FoodDelivery.BL.Handlers.QueryHandlers.Base;
using FoodDelivery.BL.Queries.AddressQueries;
using FoodDelivery.BL.Queries.MealQueries;
using FoodDelivery.DAL.EFCore.Entities;
using FoodDelivery.DAL.Entities;
using FoodDelivery.DAL.Infrastructure.QueryObjects.Interfaces.Base;
using FoodDelivery.Shared.Models.AddressModels;
using FoodDelivery.Shared.Models.MealsModels;
using MediatR;

namespace FoodDelivery.BL.Handlers.QueryHandlers.AddressQueryHandlers;

public class GetAllAddressesQueryHandler : QueryHandler<GetAllAddressesQuery, List<AddressListModel>>, IRequestHandler<GetAllAddressesQuery, List<AddressListModel>>
{
    private readonly IQueryObject<AddressEntity> _addressQueryObject;

    public GetAllAddressesQueryHandler(IMapper mapper, IQueryObject<AddressEntity> addressQueryObject) : base(mapper)
    {
        _addressQueryObject = addressQueryObject;
    }

    public override async Task<List<AddressListModel>> Handle(GetAllAddressesQuery request, CancellationToken cancellationToken)
    {
        if (request.Page > 0 && request.PageSize > 0)
            _addressQueryObject.Page(request.Page, request.PageSize);
        var addresses = await _addressQueryObject.ExecuteAsync();
        return _mapper.Map<ICollection<AddressListModel>>(addresses).ToList();
    }
}
