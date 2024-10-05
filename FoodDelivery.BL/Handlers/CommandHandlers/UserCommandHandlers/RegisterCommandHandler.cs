using AutoMapper;
using FoodDelivery.BL.Commands.UserCommands;
using FoodDelivery.BL.Handlers.CommandHandlers.Base;
using FoodDelivery.DAL.EFCore.UnitOfWork.Base;
using FoodDelivery.DAL.Entities;
using FoodDelivery.DAL.Infrastructure.UnitOfWork.Interfaces;
using FoodDelivery.Shared.Models.UserModels;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System.Text;
using FoodDelivery.DAL.EFCore.Entities;
using FoodDelivery.DAL.EFCore.UnitOfWork;
using FoodDelivery.Shared.Constants;

namespace FoodDelivery.BL.Handlers.CommandHandlers.UserCommandHandlers;

public class RegisterCommandHandler : CommandHandler<RegisterCommand, UserManagerResponseModel>, IRequestHandler<RegisterCommand, UserManagerResponseModel>
{
    private UserManager<UserEntity> _userManager;


    public RegisterCommandHandler(IUnitOfWorkProvider<IEFCoreUnitOfWork> unitOfWorkProvider, IMapper mapper, UserManager<UserEntity> userManager) : base(unitOfWorkProvider, mapper)
    {
        _userManager = userManager;
    }

    public override async Task<UserManagerResponseModel> Handle(RegisterCommand request, CancellationToken cancellationToken)
    {
        if (request.RegisterModel == null)
            throw new NullReferenceException("Register Model is null");

        if (request.RegisterModel.Password != request.RegisterModel.ConfirmPassword)
            return new UserManagerResponseModel
            {
                Message = "Confirm password doesn't match the password",
                IsSucces = false
            };
        
        var identityUser = new UserEntity()
        {
            Name = request.RegisterModel.Name,
            Surname = request.RegisterModel.Surname,
            Email = request.RegisterModel.Email,
            UserName = request.RegisterModel.Email,
            Address = request.RegisterModel.Role.Contains(Constants.Roles.Customer) ? new AddressEntity
            {
                City = "",
                Number = "",
                PostalCode = "",
                Street = ""
            } : null
        };

        var userResult = await _userManager.CreateAsync(identityUser, request.RegisterModel.Password);

        if (userResult.Succeeded)
        {
            var currentUser = await _userManager.FindByNameAsync(identityUser.UserName);
            var role = request.RegisterModel.Role ?? Constants.Roles.Customer;
            var roleresult = await _userManager.AddToRoleAsync(currentUser, role);

            if (!roleresult.Succeeded)
            {
                return new UserManagerResponseModel
                {
                    Message = "User wasn't created (adding roles failed)",
                    IsSucces = false,
                    Errors = userResult.Errors.Select(e => e.Description)
                };
            }

            return new UserManagerResponseModel
            {
                Message = "User created",
                IsSucces = true
            };
        }

        return new UserManagerResponseModel
        {
            Message = "User wasn't created",
            IsSucces = false,
            Errors = userResult.Errors.Select(e => e.Description)
        };
    }
}
