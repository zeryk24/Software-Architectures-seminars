using AutoMapper;
using FoodDelivery.BL.Commands.FeedbackCommands;
using FoodDelivery.BL.Commands.MealCommands;
using FoodDelivery.BL.Handlers.CommandHandlers.Base;
using FoodDelivery.DAL.EFCore.UnitOfWork;
using FoodDelivery.DAL.EFCore.UnitOfWork.Base;
using FoodDelivery.DAL.Infrastructure.UnitOfWork.Interfaces;
using MediatR;

namespace FoodDelivery.BL.Handlers.CommandHandlers.MealCommandHandlers;

public class RemoveMealCommandHandler : CommandHandler<RemoveMealCommand, bool>, IRequestHandler<RemoveMealCommand, bool>
{
    public RemoveMealCommandHandler(IUnitOfWorkProvider<IEFCoreUnitOfWork> unitOfWorkProvider, IMapper mapper) : base(unitOfWorkProvider, mapper) { }

    public override async Task<bool> Handle(RemoveMealCommand request, CancellationToken cancellationToken)
    {
        using var unitOfWork = _unitOfWorkProvider.Create();
        var result = await unitOfWork.MealRepository.RemoveAsync(request.Id);
        if (!result)
        {
            return false;
        }
        
        await unitOfWork.Commit();
        return true;
    }
}
