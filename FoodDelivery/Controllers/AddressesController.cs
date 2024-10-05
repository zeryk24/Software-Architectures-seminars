using FoodDelivery.BL.Commands.AddressCommands;
using FoodDelivery.BL.Queries.AddressQueries;
using FoodDelivery.DAL.EFCore.Entities;
using FoodDelivery.Shared.Constants;
using FoodDelivery.Shared.Models.AddressModels;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using NSwag.Annotations;

namespace FoodDelivery.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AddressesController : ControllerBase
{
    private const string ApiOperationBaseName = "Address";
    private readonly IMediator _mediator;

    public AddressesController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [Authorize(Roles = Constants.Roles.Admin)]
    [HttpGet]
    [OpenApiOperation(ApiOperationBaseName + nameof(GetAllAddresses))]
    public async Task<ActionResult<List<AddressListModel>>> GetAllAddresses(int page, int pageSize)
    {
        return Ok(await _mediator.Send(new GetAllAddressesQuery(page, pageSize)));
    }
    
    [Authorize]
    [HttpGet("{addressId}")]
    [OpenApiOperation(ApiOperationBaseName + nameof(GetAddress))]
    public async Task<ActionResult<List<AddressListModel>>> GetAddress(int addressId)
    {
        
        return Ok(await _mediator.Send(new GetAddressQuery(addressId)));
    }
}