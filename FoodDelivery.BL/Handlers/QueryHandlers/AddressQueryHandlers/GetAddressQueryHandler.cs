using AutoMapper;
using FoodDelivery.BL.Handlers.QueryHandlers.Base;
using FoodDelivery.BL.Queries.AddressQueries;
using FoodDelivery.BL.Queries.MealQueries;
using FoodDelivery.DAL.EFCore.Entities;
using FoodDelivery.DAL.EFCore.UnitOfWork;
using FoodDelivery.DAL.Entities;
using FoodDelivery.DAL.Infrastructure.QueryObjects.Interfaces.Base;
using FoodDelivery.DAL.Infrastructure.UnitOfWork.Interfaces;
using FoodDelivery.DAL.Infrastructure.UnitOfWork.Interfaces.Base;
using FoodDelivery.Shared.Models.AddressModels;
using FoodDelivery.Shared.Models.MealsModels;
using MediatR;

namespace FoodDelivery.BL.Handlers.QueryHandlers.AddressQueryHandlers;

public class GetAddressQueryHandler : QueryHandler<GetAddressQuery, AddressDetailModel>, IRequestHandler<GetAddressQuery, AddressDetailModel>
{
    protected readonly IMapper _mapper;
    private readonly IUnitOfWorkProvider<IEFCoreUnitOfWork> _unitOfWorkProvider;

    public GetAddressQueryHandler(IMapper mapper, IUnitOfWorkProvider<IEFCoreUnitOfWork> unitOfWorkProvider) : base(mapper)
    {
        _mapper = mapper;
        _unitOfWorkProvider = unitOfWorkProvider;
    }

    public override async Task<AddressDetailModel> Handle(GetAddressQuery request, CancellationToken cancellationToken)
    {
        using var unitOfWork = _unitOfWorkProvider.Create();
        var result = await unitOfWork.AddressRepository.GetByIdAsync(request.AddressId);

        return _mapper.Map<AddressDetailModel>(result);
    }
}
