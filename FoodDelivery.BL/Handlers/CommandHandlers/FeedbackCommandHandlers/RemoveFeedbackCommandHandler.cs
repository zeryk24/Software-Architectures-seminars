using AutoMapper;
using FoodDelivery.BL.Commands.FeedbackCommands;
using FoodDelivery.BL.Handlers.CommandHandlers.Base;
using FoodDelivery.DAL.EFCore.Entities;
using FoodDelivery.DAL.EFCore.UnitOfWork;
using FoodDelivery.DAL.EFCore.UnitOfWork.Base;
using FoodDelivery.DAL.Infrastructure.UnitOfWork.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace FoodDelivery.BL.Handlers.CommandHandlers.FeedbackCommandHandlers;

public class RemoveFeedbackCommandHandler : CommandHandler<RemoveFeedbackCommand, bool>, IRequestHandler<RemoveFeedbackCommand, bool>
{
    private readonly UserManager<UserEntity> _userManager;
    public RemoveFeedbackCommandHandler(IUnitOfWorkProvider<IEFCoreUnitOfWork> unitOfWorkProvider, IMapper mapper,
        UserManager<UserEntity> userManager) : base(unitOfWorkProvider, mapper)
    {
        _userManager = userManager;
    }

    public override async Task<bool> Handle(RemoveFeedbackCommand request, CancellationToken cancellationToken)
    {
        using var unitOfWork = _unitOfWorkProvider.Create();
        var feedbackEntity = await unitOfWork.FeedbackRepository.GetByIdAsync(request.Id);
        if (feedbackEntity == null)
        {
            return false;
        }
        
        var currentUser = await _userManager.GetUserAsync(request.User);
        if (feedbackEntity.UserId != currentUser.Id)
        {
            return false;
        }
        
        await unitOfWork.FeedbackRepository.RemoveAsync(request.Id);
        await unitOfWork.Commit();
        return true;
    }
}
