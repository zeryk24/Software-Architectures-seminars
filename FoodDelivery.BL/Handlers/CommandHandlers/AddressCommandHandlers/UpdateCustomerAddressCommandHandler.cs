using AutoMapper;
using FoodDelivery.BL.Commands.AddressCommands;
using FoodDelivery.BL.Handlers.CommandHandlers.Base;
using FoodDelivery.DAL.EFCore.Entities;
using FoodDelivery.DAL.EFCore.UnitOfWork;
using FoodDelivery.DAL.EFCore.UnitOfWork.Base;
using FoodDelivery.DAL.Entities;
using FoodDelivery.DAL.Infrastructure.UnitOfWork.Interfaces;
using FoodDelivery.Shared.Models.AddressModels;
using FoodDelivery.Shared.Models.MealsModels;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace FoodDelivery.BL.Handlers.CommandHandlers.AddressCommandHandlers;

public class UpdateCustomerAddressCommandHandler : CommandHandler<UpdateCustomerAddressCommand, AddressDetailModel>, IRequestHandler<UpdateCustomerAddressCommand, AddressDetailModel>
{
    private readonly UserManager<UserEntity> _userManager;
    
    public UpdateCustomerAddressCommandHandler(IUnitOfWorkProvider<IEFCoreUnitOfWork> unitOfWorkProvider, IMapper mapper, UserManager<UserEntity> userManager) : base(unitOfWorkProvider, mapper)
    {
        _userManager = userManager;
    }

    public override async Task<AddressDetailModel> Handle(UpdateCustomerAddressCommand request, CancellationToken cancellationToken)
    {
        var addressEntity = _mapper.Map<AddressEntity>(request.AddressUpdateModel);
        var user = await _userManager.GetUserAsync(request.User);
        addressEntity.Id = (int) user.AddressId;
        
        using var unitOfWork = _unitOfWorkProvider.Create();
        unitOfWork.AddressRepository.Update(addressEntity);
        await unitOfWork.Commit();
        
        return _mapper.Map<AddressDetailModel>(addressEntity);
    }
}