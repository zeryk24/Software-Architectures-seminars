using AutoMapper;
using FoodDelivery.BL.Commands.OrderCommands;
using FoodDelivery.BL.Commands.OrderItemCommands;
using FoodDelivery.BL.Handlers.CommandHandlers.Base;
using FoodDelivery.DAL.EFCore.Entities;
using FoodDelivery.DAL.EFCore.UnitOfWork;
using FoodDelivery.DAL.EFCore.UnitOfWork.Base;
using FoodDelivery.DAL.Infrastructure.UnitOfWork.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace FoodDelivery.BL.Handlers.CommandHandlers.OrderCommandHandlers;

public class RemoveOrderCommandHandler : CommandHandler<RemoveOrderCommand, bool>, IRequestHandler<RemoveOrderCommand, bool>
{
    public RemoveOrderCommandHandler(IUnitOfWorkProvider<IEFCoreUnitOfWork> unitOfWorkProvider, IMapper mapper) : base(unitOfWorkProvider, mapper) { }

    public override async Task<bool> Handle(RemoveOrderCommand request, CancellationToken cancellationToken)
    {
        using var unitOfWork = _unitOfWorkProvider.Create();
        
        var order = await unitOfWork.OrderRepository.GetByIdAsync(request.Id);
        var result = await unitOfWork.OrderRepository.RemoveAsync(request.Id);

        if (!result)
        {
            return false;
        }
        
        await unitOfWork.Commit();
        
        return true;
    }
}
