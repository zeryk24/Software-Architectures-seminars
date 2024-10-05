using AutoMapper;
using FoodDelivery.BL.Commands.OrderItemCommands;
using FoodDelivery.BL.Handlers.CommandHandlers.Base;
using FoodDelivery.DAL.EFCore.UnitOfWork;
using FoodDelivery.DAL.Infrastructure.UnitOfWork.Interfaces;
using MediatR;

namespace FoodDelivery.BL.Handlers.CommandHandlers.OrderItemCommandHandlers;

public class RemoveOrderItemCommandHandler : CommandHandler<RemoveOrderItemCommand, bool>, IRequestHandler<RemoveOrderItemCommand, bool>
{
    public RemoveOrderItemCommandHandler(IUnitOfWorkProvider<IEFCoreUnitOfWork> unitOfWorkProvider, IMapper mapper) : base(unitOfWorkProvider, mapper) { }

    public override async Task<bool> Handle(RemoveOrderItemCommand request, CancellationToken cancellationToken)
    {
        using var unitOfWork = _unitOfWorkProvider.Create();
        var result = await unitOfWork.OrderItemRepository.RemoveAsync(request.Id);

        if (!result)
        {
            return false;
        }
        
        await unitOfWork.Commit();
        return true;
    }
}
