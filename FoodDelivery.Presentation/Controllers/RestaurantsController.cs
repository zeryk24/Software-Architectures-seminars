using FoodDelivery.BL.Commands.RestaurantCommands;
using FoodDelivery.BL.Queries.RestaurantQueries;
using FoodDelivery.DAL.EFCore.Entities;
using FoodDelivery.Shared.Constants;
using FoodDelivery.Shared.Models.MealsModels;
using FoodDelivery.Shared.Models.RestaurantModels;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using NSwag.Annotations;

namespace FoodDelivery.Controllers;

[Tags("FoodDelivery - Restaurant")]
[ApiController]
[Route("api/[controller]")]
public class RestaurantsController : ControllerBase
{
    private const string ApiOperationBaseName = "Restaurant";
    private readonly IMediator _mediator;

    public RestaurantsController(IMediator mediator)
    {
        _mediator = mediator;
    }
    
    [HttpGet]
    [OpenApiOperation(ApiOperationBaseName + nameof(GetAllRestaurants))]
    public async Task<ActionResult<List<RestaurantListModel>>> GetAllRestaurants(int page, int pageSize)
    {
        return Ok(await _mediator.Send(new GetAllRestaurantsQuery(page, pageSize)));
    }
    
    [HttpGet("{restaurantId}/Meals")]
    [OpenApiOperation(ApiOperationBaseName + nameof(GetMealsByRestaurantId))]
    public async Task<ActionResult<List<MealListModel>>> GetMealsByRestaurantId(int restaurantId)
    {
        return Ok(await _mediator.Send(new GetAllRestaurantMealsQuery(restaurantId)));
    }

    [Authorize(Roles = Constants.Roles.Admin)]
    [HttpPost]
    [OpenApiOperation(ApiOperationBaseName + nameof(CreateRestaurant))]
    public async Task<ActionResult<RestaurantDetailModel>> CreateRestaurant(RestaurantCreateModel restaurantCreateModel)
    {
        return Ok(await _mediator.Send(new CreateRestaurantCommand(restaurantCreateModel)));
    }

    [Authorize(Roles = Constants.Roles.Admin)]
    [HttpPut]
    [OpenApiOperation(ApiOperationBaseName + nameof(UpdateRestaurant))]
    public async Task<ActionResult<RestaurantDetailModel>> UpdateRestaurant(RestaurantUpdateModel restaurantUpdateModel)
    {
        var result = await _mediator.Send(new UpdateRestaurantCommand(restaurantUpdateModel));
        return result.Id switch
        {
            -400 => BadRequest(),
            _ => Ok(result)
        };
    }

    [Authorize(Roles = Constants.Roles.Admin)]
    [HttpDelete("{restaurantId}")]
    [OpenApiOperation(ApiOperationBaseName + nameof(RemoveRestaurant))]
    public async Task<ActionResult<bool>> RemoveRestaurant(int restaurantId)
    {
        var result = await _mediator.Send(new RemoveRestaurantCommand(restaurantId));
        return result ? Ok(result) : BadRequest();
    }
}
