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

public class UpdateOrderAddressCommandHandler : CommandHandler<UpdateOrderAddressCommand, AddressDetailModel>, IRequestHandler<UpdateOrderAddressCommand, AddressDetailModel>
{
    private readonly UserManager<UserEntity> _userManager;
    public UpdateOrderAddressCommandHandler(IUnitOfWorkProvider<IEFCoreUnitOfWork> unitOfWorkProvider, IMapper mapper, UserManager<UserEntity> userManager) : base(unitOfWorkProvider, mapper)
    {
        _userManager = userManager;
    }

    public override async Task<AddressDetailModel> Handle(UpdateOrderAddressCommand request, CancellationToken cancellationToken)
    {
        var addressEntity = _mapper.Map<AddressEntity>(request.AddressUpdateModel);

        using var unitOfWork = _unitOfWorkProvider.Create();
        var order = await unitOfWork.OrderRepository.GetByIdAsync(request.OrderId);

        if (order == null)
        {
            return new AddressDetailModel
            {
                Id = -400,
            };
        }

        var user = await _userManager.GetUserAsync(request.User);
        if (order.UserId != user.Id)
        {
            return new AddressDetailModel
            {
                Id = -403,
            };
        }
        
        addressEntity.Id = (int) order.AddressId;
        unitOfWork.AddressRepository.Update(addressEntity);
        await unitOfWork.Commit();

        return _mapper.Map<AddressDetailModel>(addressEntity);
    }
}
