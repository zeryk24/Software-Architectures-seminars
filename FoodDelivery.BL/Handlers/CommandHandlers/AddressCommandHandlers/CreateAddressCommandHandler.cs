using AutoMapper;
using FoodDelivery.BL.Commands.AddressCommands;
using FoodDelivery.BL.Commands.MealCommands;
using FoodDelivery.BL.Handlers.CommandHandlers.Base;
using FoodDelivery.DAL.EFCore.Entities;
using FoodDelivery.DAL.EFCore.UnitOfWork;
using FoodDelivery.DAL.EFCore.UnitOfWork.Base;
using FoodDelivery.DAL.Entities;
using FoodDelivery.DAL.Infrastructure.UnitOfWork.Interfaces;
using FoodDelivery.Shared.Models.AddressModels;
using FoodDelivery.Shared.Models.MealsModels;
using MediatR;

namespace FoodDelivery.BL.Handlers.CommandHandlers.AddressCommandHandlers;

public class CreateAddressCommandHandler : CommandHandler<CreateAddressCommand, AddressDetailModel>, IRequestHandler<CreateAddressCommand, AddressDetailModel>
{
    public CreateAddressCommandHandler(IUnitOfWorkProvider<IEFCoreUnitOfWork> unitOfWorkProvider, IMapper mapper) : base(unitOfWorkProvider, mapper) { }

    public override async Task<AddressDetailModel> Handle(CreateAddressCommand request, CancellationToken cancellationToken)
    {
        var addressEntity = _mapper.Map<AddressEntity>(request.AddressCreateModel);

        using var unitOfWork = _unitOfWorkProvider.Create();
        unitOfWork.AddressRepository.Insert(addressEntity);
        await unitOfWork.Commit();

        return _mapper.Map<AddressDetailModel>(addressEntity);
    }
}
