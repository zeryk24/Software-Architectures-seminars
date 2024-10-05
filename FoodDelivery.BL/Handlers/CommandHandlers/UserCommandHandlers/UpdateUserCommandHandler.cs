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

public class UpdateUserCommandHandler : CommandHandler<UpdateUserCommand, UserManagerResponseModel>, IRequestHandler<UpdateUserCommand, UserManagerResponseModel>
{
    private readonly UserManager<UserEntity> _userManager;

    public UpdateUserCommandHandler(IUnitOfWorkProvider<IEFCoreUnitOfWork> unitOfWorkProvider, IMapper mapper,
        UserManager<UserEntity> userManager) : base(unitOfWorkProvider, mapper)
    {
        _userManager = userManager;
    }

    public override async Task<UserManagerResponseModel> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
    {
        var user = await _userManager.GetUserAsync(request.User);
        user.Name = request.UserUpdateModel.Name;
        user.Surname = request.UserUpdateModel.SurName;
        var result = await _userManager.UpdateAsync(user);

        if (!result.Succeeded)
        {
            return new UserManagerResponseModel
            {
                Message = "User details not updated.",
                IsSucces = false,
                Errors = result.Errors.Select(e => e.Description),
            };
        }

        return new UserManagerResponseModel
        {
            Message = "User details updated",
            IsSucces = true
        };
    }
}