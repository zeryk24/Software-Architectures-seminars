using AutoMapper;
using FoodDelivery.BL.Commands.UserCommands;
using FoodDelivery.BL.Handlers.CommandHandlers.Base;
using FoodDelivery.DAL.EFCore.UnitOfWork.Base;
using FoodDelivery.DAL.Entities;
using FoodDelivery.DAL.Infrastructure.UnitOfWork.Interfaces;
using FoodDelivery.Shared.Models.UserModels;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using FoodDelivery.DAL.EFCore.Entities;
using FoodDelivery.DAL.EFCore.UnitOfWork;

namespace FoodDelivery.BL.Handlers.CommandHandlers.UserCommandHandlers;

public class ChangePasswordCommandHandler : CommandHandler<ChangePasswordCommand, UserManagerResponseModel>, IRequestHandler<ChangePasswordCommand, UserManagerResponseModel>
{
    private UserManager<UserEntity> _userManager;

    public ChangePasswordCommandHandler(IUnitOfWorkProvider<IEFCoreUnitOfWork> unitOfWorkProvider, IMapper mapper, UserManager<UserEntity> userManager) : base(unitOfWorkProvider, mapper)
    {
        _userManager = userManager;
    }

    public override async Task<UserManagerResponseModel> Handle(ChangePasswordCommand request, CancellationToken cancellationToken)
    {
        var user = await _userManager.GetUserAsync(request.User);
        
        if (request.UserChangePasswordModel.NewPassword != request.UserChangePasswordModel.ConfirmPassword)
        {
            return new UserManagerResponseModel
            {
                Message = "Confirm password doesn't match the password",
                IsSucces = false
            };
        }
        
        var result = await _userManager.ChangePasswordAsync(user, request.UserChangePasswordModel.CurrentPassword,
            request.UserChangePasswordModel.NewPassword);

        if (!result.Succeeded)
        {
            return new UserManagerResponseModel
            {
                Message = "Password not changed.",
                IsSucces = false,
                Errors = result.Errors.Select(e => e.Description)
            };
        }

        return new UserManagerResponseModel
        {
            Message = "Password changed.",
            IsSucces = true
        };
    }
}
