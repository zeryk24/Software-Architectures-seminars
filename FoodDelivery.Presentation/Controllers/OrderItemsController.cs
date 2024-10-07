using FoodDelivery.BL.Commands.AddressCommands;
using FoodDelivery.BL.Commands.OrderItemCommands;
using FoodDelivery.BL.Queries.AddressQueries;
using FoodDelivery.BL.Queries.OrderItemQueries;
using FoodDelivery.Shared.Constants;
using FoodDelivery.Shared.Models.AddressModels;
using FoodDelivery.Shared.Models.OrderItemsModels;
using FoodDelivery.Shared.Models.OrderModels;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NSwag.Annotations;

namespace FoodDelivery.Controllers;

[Tags("FoodDelivery - OrderItem")]
[ApiController]
[Route("api/[controller]")]
public class OrderItemsController : ControllerBase
{
    private const string ApiOperationBaseName = "OrderItem";
    private readonly IMediator _mediator;

    public OrderItemsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [Authorize(Roles = Constants.Roles.Admin)]
    [HttpGet]
    [OpenApiOperation(ApiOperationBaseName + nameof(GetAllOrderItems))]
    public async Task<ActionResult<List<OrderItemListModel>>> GetAllOrderItems(int page, int pageSize)
    {
        return Ok(await _mediator.Send(new GetAllOrderItemsQuery(page, pageSize)));
    }

    [Authorize(Roles = Constants.Roles.Customer)]
    [HttpPost]
    [OpenApiOperation(ApiOperationBaseName + nameof(CreateOrderItem))]
    public async Task<ActionResult<OrderDetailModel>> CreateOrderItem(OrderItemCreateModel orderItemCreateModel)
    {
        var result = await _mediator.Send(new CreateOrderItemCommand(orderItemCreateModel, User));

        return result.Id switch
        {
            -403 => Forbid(),
            -400 => BadRequest(),
            _ => Ok(result)
        };
    }
    
    [Authorize(Roles = Constants.Roles.Customer)]
    [HttpPut]
    [OpenApiOperation(ApiOperationBaseName + nameof(UpdateOrderItem))]
    public async Task<ActionResult<OrderItemDetailModel>> UpdateOrderItem(OrderItemUpdateModel orderItemUpdateModel)
    {
        var result = await _mediator.Send(new UpdateOrderItemCommand(orderItemUpdateModel, User));

        return result.Id switch
        {
            -403 => Forbid(),
            -400 => BadRequest(),
            _ => Ok(result)
        };
    }

    [Authorize(Roles = Constants.Roles.Customer)]
    [HttpDelete("{orderItemId}")]
    [OpenApiOperation(ApiOperationBaseName + nameof(RemoveOrderItem))]
    public async Task<ActionResult<bool>> RemoveOrderItem(int orderItemId)
    {
        var result = await _mediator.Send(new RemoveOrderItemCommand(orderItemId));
        return result ? Ok(result) : BadRequest(result);
    }
}