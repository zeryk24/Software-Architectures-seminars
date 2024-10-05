using AutoMapper;
using FoodDelivery.BL.Commands.AddressCommands;
using FoodDelivery.BL.Commands.MealCommands;
using FoodDelivery.BL.Handlers.CommandHandlers.Base;
using FoodDelivery.DAL.EFCore.UnitOfWork;
using FoodDelivery.DAL.EFCore.UnitOfWork.Base;
using FoodDelivery.DAL.Infrastructure.UnitOfWork.Interfaces;
using MediatR;

namespace FoodDelivery.BL.Handlers.CommandHandlers.AddressCommandHandlers;

public class RemoveAddressCommandHandler : CommandHandler<RemoveAddressCommand, bool>, IRequestHandler<RemoveAddressCommand, bool>
{
    public RemoveAddressCommandHandler(IUnitOfWorkProvider<IEFCoreUnitOfWork> unitOfWorkProvider, IMapper mapper) : base(unitOfWorkProvider, mapper) { }

    public override async Task<bool> Handle(RemoveAddressCommand request, CancellationToken cancellationToken)
    {
        using var unitOfWork = _unitOfWorkProvider.Create();
        await unitOfWork.AddressRepository.RemoveAsync(request.Id);
        await unitOfWork.Commit();
        return true;
    }
}
