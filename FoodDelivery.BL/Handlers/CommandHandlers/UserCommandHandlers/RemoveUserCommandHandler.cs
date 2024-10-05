using AutoMapper;
using FoodDelivery.BL.Commands.UserCommands;
using FoodDelivery.BL.Handlers.CommandHandlers.Base;
using FoodDelivery.DAL.EFCore.Entities;
using FoodDelivery.DAL.EFCore.UnitOfWork;
using FoodDelivery.DAL.Infrastructure.UnitOfWork.Interfaces;
using FoodDelivery.Shared.Models.UserModels;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace FoodDelivery.BL.Handlers.CommandHandlers.UserCommandHandlers;

public class RemoveUserCommandHandler : CommandHandler<RemoveUserCommand, UserManagerResponseModel>, IRequestHandler<RemoveUserCommand, UserManagerResponseModel>
{
    private readonly UserManager<UserEntity> _userManager;

    public RemoveUserCommandHandler(IUnitOfWorkProvider<IEFCoreUnitOfWork> unitOfWorkProvider, IMapper mapper,
        UserManager<UserEntity> userManager) : base(unitOfWorkProvider, mapper)
    {
        _userManager = userManager;
    }

    public override async Task<UserManagerResponseModel> Handle(RemoveUserCommand request, CancellationToken cancellationToken)
    {

        var user = await _userManager.FindByIdAsync(request.UserId.ToString());
        var result = await _userManager.DeleteAsync(user);

        if (!result.Succeeded)
        {
            return new UserManagerResponseModel
            {
                Message = "User remove was not successful.",
                IsSucces = false,
                Errors = result.Errors.Select(e => e.Description),
            };
        }

        return new UserManagerResponseModel
        {
            Message = "User removed.",
            IsSucces = true
        };
    }
}