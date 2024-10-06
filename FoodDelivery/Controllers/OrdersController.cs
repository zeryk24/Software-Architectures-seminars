using FoodDelivery.BL.Commands.AddressCommands;
using FoodDelivery.BL.Commands.OrderCommands;
using FoodDelivery.BL.Queries.AddressQueries;
using FoodDelivery.BL.Queries.OrderQueries;
using FoodDelivery.BL.Queries.UserQueries;
using FoodDelivery.DAL.EFCore.Entities;
using FoodDelivery.DAL.Infrastructure.UnitOfWork.Interfaces;
using FoodDelivery.DAL.Infrastructure.UnitOfWork.Interfaces.Base;
using FoodDelivery.Shared.Constants;
using FoodDelivery.Shared.Enums;
using FoodDelivery.Shared.Models.AddressModels;
using FoodDelivery.Shared.Models.OrderModels;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using NSwag.Annotations;

namespace FoodDelivery.Controllers;

[ApiController]
[Route("api/[controller]")]
public class OrdersController : ControllerBase
{
    private const string ApiOperationBaseName = "Order";
    private readonly IMediator _mediator;

    public OrdersController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [Authorize(Roles = Constants.Roles.Admin)]
    [HttpGet]
    [OpenApiOperation(ApiOperationBaseName + nameof(GetAllOrders))]
    public async Task<ActionResult<List<OrderListModel>>> GetAllOrders(int page, int pageSize)
    {
        return Ok(await _mediator.Send(new GetAllOrdersQuery(page, pageSize)));
    }
    
    [Authorize]
    [HttpGet("{orderId}")]
    [OpenApiOperation(ApiOperationBaseName + nameof(GetOrder))]
    public async Task<ActionResult<OrderDetailModel>> GetOrder(int orderId)
    {
        var result = await _mediator.Send(new GetOrderQuery(orderId, User));

        return result.Id switch
        {
            -403 => Forbid(),
            -400 => BadRequest(),
            _ => Ok(result)
        };
    }
    
    [Authorize(Roles = Constants.Roles.Customer)]
    [HttpPost]
    [OpenApiOperation(ApiOperationBaseName + nameof(CreateOrder))]
    public async Task<ActionResult<OrderListModel>> CreateOrder(int restaurantId)
    {
        return Ok(await _mediator.Send(new CreateOrderCommand(restaurantId, User)));
    }
    
    [Authorize(Roles = Constants.Roles.Customer)]
    [HttpPut("{orderId}/Address")]
    [OpenApiOperation(ApiOperationBaseName + nameof(UpdateOrderAddress))]
    public async Task<ActionResult<AddressDetailModel>> UpdateOrderAddress(int orderId, AddressUpdateModel addressUpdateModel)
    {
        var result = await _mediator.Send(new UpdateOrderAddressCommand(orderId, addressUpdateModel, User));

        return result.Id switch
        {
            -403 => Forbid(),
            -400 => BadRequest(),
            _ => Ok(result)
        };
    }

    [Authorize(Roles = Constants.Roles.Customer)]
    [HttpPut("{orderId}/Payment")]
    [OpenApiOperation(ApiOperationBaseName + nameof(UpdatePaymentType))]
    public async Task<ActionResult<OrderDetailModel>> UpdatePaymentType(int orderId, PaymentType paymentType)
    {
        var result = await _mediator.Send(new ChangeOrderPaymentTypeCommand(orderId, paymentType, User));
        
        return result.Id switch
        {
            -403 => Forbid(),
            -400 => BadRequest(),
            _ => Ok(result)
        };
    }
    
    [Authorize(Roles = Constants.Roles.Admin)]
    [HttpDelete("{orderId}")]
    [OpenApiOperation(ApiOperationBaseName + nameof(RemoveOrder))]
    public async Task<ActionResult<bool>> RemoveOrder(int orderId)
    {
        var result = await _mediator.Send(new RemoveOrderCommand(orderId));
        return result ? Ok(result) : BadRequest(result);
    }
    
    [Authorize(Roles = Constants.Roles.Customer)]
    [HttpPost("OrderCompleted")]
    [OpenApiOperation(ApiOperationBaseName + nameof(OrderCompleted))]
    public async Task<ActionResult<bool>> OrderCompleted(int orderId)
    {
        
        return Ok(await _mediator.Send(new OrderCompletedCommand(orderId, User)));
    }
    
    [Authorize(Roles = Constants.Roles.Customer)]
    [HttpGet("User")]
    [OpenApiOperation(ApiOperationBaseName + nameof(GetOrdersForCustomer))]
    public async Task<ActionResult<List<OrderListModel>>> GetOrdersForCustomer()
    {
        return Ok(await _mediator.Send(new GetAllUserOrdersQuery(User)));
    }
    
    [Authorize(Roles = Constants.Roles.Admin)]
    [HttpGet("by-restaurant-id/{restaurantId}")]
    [OpenApiOperation(ApiOperationBaseName + nameof(GetOrdersByRestaurantId))]
    public async Task<ActionResult<List<OrderListModel>>> GetOrdersByRestaurantId(int restaurantId)
    {
        return Ok(await _mediator.Send(new GetOrdersByRestaurantIdQuery(restaurantId)));
    }
}