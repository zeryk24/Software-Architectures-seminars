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

public class LoginCommandHandler : CommandHandler<LoginCommand, UserManagerResponseModel>, IRequestHandler<LoginCommand, UserManagerResponseModel>
{
    private UserManager<UserEntity> _userManager;
    private IConfiguration _configuration;

    public LoginCommandHandler(IUnitOfWorkProvider<IEFCoreUnitOfWork> unitOfWorkProvider, IMapper mapper, UserManager<UserEntity> userManager, IConfiguration configuration) : base(unitOfWorkProvider, mapper)
    {
        _userManager = userManager;
        _configuration = configuration;
    }

    public override async Task<UserManagerResponseModel> Handle(LoginCommand request, CancellationToken cancellationToken)
    {
        var user = await _userManager.FindByEmailAsync(request.LoginModel.Email);

        if (user == null)
            return new UserManagerResponseModel
            {
                Message = "There is no user with that Email address",
                IsSucces = false
            };

        var result = await _userManager.CheckPasswordAsync(user, request.LoginModel.Password);

        if (!result)
            return new UserManagerResponseModel
            {
                Message = "Invalid password",
                IsSucces = false
            };

        var claims = new List<Claim>()
        {
                new (ClaimTypes.Email, request.LoginModel.Email),
                new (ClaimTypes.NameIdentifier, user.Id.ToString())
        };

        var userRoles = await _userManager.GetRolesAsync(user);
        claims.AddRange(userRoles.Select(userRole => new Claim(ClaimTypes.Role, userRole)));

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["AuthSettings:Key"]));

        var token = new JwtSecurityToken(
            claims: claims,
            expires: DateTime.Now.AddDays(30),
            signingCredentials: new SigningCredentials(key, SecurityAlgorithms.HmacSha256));

        string tokenAsString = new JwtSecurityTokenHandler().WriteToken(token);
        return new UserManagerResponseModel
        {
            Message = tokenAsString,
            IsSucces = true,
            ExpireDate = token.ValidTo
        };
    }
}
