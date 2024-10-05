using FoodDelivery.BL.Commands.AddressCommands;
using FoodDelivery.BL.Commands.UserCommands;
using FoodDelivery.BL.Queries.AddressQueries;
using FoodDelivery.BL.Queries.UserQueries;
using FoodDelivery.DAL.EFCore.Entities;
using FoodDelivery.Shared.Constants;
using FoodDelivery.Shared.Models.AddressModels;
using FoodDelivery.Shared.Models.UserModels;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using NSwag.Annotations;

namespace FoodDelivery.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UsersController : ControllerBase
{
    private const string ApiOperationBaseName = "Users";
    private readonly IMediator _mediator;

    public UsersController(IMediator mediator)
    {
        _mediator = mediator;
    }
    
    [HttpGet]
    [OpenApiOperation(ApiOperationBaseName + nameof(GetAllUsers))]
    public async Task<ActionResult<List<UserListModel>>> GetAllUsers(int page, int pageSize)
    {
        return Ok(await _mediator.Send(new GetAllUsersQuery(page, pageSize)));
    }

    [HttpPost("register")]
    [OpenApiOperation(ApiOperationBaseName + nameof(Register))]
    public async Task<ActionResult<UserManagerResponseModel>> Register(RegisterModel registerModel)
    {

        if (registerModel.Role.ToLower().Contains(Constants.Roles.Admin))
        {
            return Forbid();
        }

        var result = await _mediator.Send(new RegisterCommand(registerModel));

        return result.IsSucces ? Ok(result) : BadRequest(result);
    }

    [HttpPost("login")]
    [OpenApiOperation(ApiOperationBaseName + nameof(Login))]
    public async Task<ActionResult<UserManagerResponseModel>> Login(LoginModel loginModel)
    {
        var result = await _mediator.Send(new LoginCommand(loginModel));

        return result.IsSucces ? Ok(result) : BadRequest(result);
    }
    
    [Authorize(Roles = Constants.Roles.Admin)]
    [HttpPost("registerAdmin")]
    [OpenApiOperation(ApiOperationBaseName + nameof(Register))]
    public async Task<ActionResult<UserManagerResponseModel>> Register(AdminRegisterModel adminRegisterModel)
    {
        var result = await _mediator.Send(new RegisterCommand(adminRegisterModel));

        return result.IsSucces ? Ok(result) : BadRequest(result);
    }
    
    [Authorize]
    [HttpPut("password")]
    [OpenApiOperation(ApiOperationBaseName + nameof(ChangePassword))]
    public async Task<ActionResult<UserManagerResponseModel>> ChangePassword(UserChangePasswordModel userChangePasswordModel)
    {
        var result = await _mediator.Send(new ChangePasswordCommand(userChangePasswordModel, User));

        return result.IsSucces ? Ok(result) : BadRequest(result);
    }
    
    [Authorize(Roles = Constants.Roles.Customer)]
    [HttpPut("address")]
    [OpenApiOperation(ApiOperationBaseName + nameof(ChangeAddress))]
    public async Task<ActionResult<UserManagerResponseModel>> ChangeAddress(AddressUpdateModel addressUpdateModel)
    {
        var result = await _mediator.Send(new UpdateCustomerAddressCommand(addressUpdateModel, User));

        return Ok(result);
    }
    
    [Authorize(Roles = Constants.Roles.Admin)]
    [HttpDelete]
    [OpenApiOperation(ApiOperationBaseName + nameof(RemoveUser))]
    public async Task<ActionResult<UserManagerResponseModel>> RemoveUser(int userId)
    {
        var result = await _mediator.Send(new RemoveUserCommand(userId));

        return result.IsSucces ? Ok(result) : BadRequest(result);
    }
    
    [Authorize]
    [HttpPut("details")]
    [OpenApiOperation(ApiOperationBaseName + nameof(UpdateUserDetails))]
    public async Task<ActionResult<UserManagerResponseModel>> UpdateUserDetails(UserUpdateModel userUpdateModel)
    {
        var result = await _mediator.Send(new UpdateUserCommand(userUpdateModel, User));

        return result.IsSucces ? Ok(result) : BadRequest(result);
    }
}