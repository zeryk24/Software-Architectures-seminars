using FoodDelivery.BL.Commands.MealCommands;
using FoodDelivery.BL.Queries;
using FoodDelivery.BL.Queries.MealQueries;
using FoodDelivery.BL.Queries.RestaurantQueries;
using FoodDelivery.DAL.Entities;
using FoodDelivery.DAL.Infrastructure.QueryObjects.Interfaces;
using FoodDelivery.Shared.Constants;
using FoodDelivery.Shared.Models.FeedbacksModels;
using FoodDelivery.Shared.Models.MealModels;
using FoodDelivery.Shared.Models.MealsModels;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NSwag.Annotations;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace FoodDelivery.Controllers;

[Tags("FoodDelivery - Meal")]
[ApiController]
[Route("api/[controller]")]
public class MealsController : ControllerBase
{
    private const string ApiOperationBaseName = "Meal";
    private readonly IMediator _mediator;

    public MealsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    [OpenApiOperation(ApiOperationBaseName + nameof(GetAllMeals))]
    public async Task<ActionResult<List<MealListModel>>> GetAllMeals(int page, int pageSize)
    {
        return Ok(await _mediator.Send(new GetAllMealsQuery(page, pageSize)));
    }
    
    [HttpGet("{mealId}")]
    [OpenApiOperation(ApiOperationBaseName + nameof(GetMeal))]
    public async Task<ActionResult<MealDetailModel>> GetMeal(int mealId)
    {
        return Ok(await _mediator.Send(new GetMealQuery(mealId)));
    }

    [Authorize(Roles = Constants.Roles.Admin)]
    [HttpPost]
    [OpenApiOperation(ApiOperationBaseName + nameof(CreateMeal))]
    public async Task<ActionResult<MealDetailModel>> CreateMeal(MealCreateModel mealCreateModel)
    {
        var result =await _mediator.Send(new CreateMealCommand(mealCreateModel));
        return Ok(result);
    }

    [Authorize(Roles = Constants.Roles.Admin)]
    [HttpPut]
    [OpenApiOperation(ApiOperationBaseName + nameof(UpdateMeal))]
    public async Task<ActionResult<MealDetailModel>> UpdateMeal(MealUpdateModel mealUpdateModel)
    {
        return Ok(await _mediator.Send(new UpdateMealCommand(mealUpdateModel)));
    }

    [Authorize(Roles = Constants.Roles.Admin)]
    [HttpDelete("{mealId}")]
    [OpenApiOperation(ApiOperationBaseName + nameof(RemoveMeal))]
    public async Task<ActionResult<bool>> RemoveMeal(int mealId)
    {
        var result = await _mediator.Send(new RemoveMealCommand(mealId));
        return result ? Ok(result) : NotFound(result);
    }
    
    [HttpGet("by-restaurant-id/{restaurantId}")]
    [OpenApiOperation(ApiOperationBaseName + nameof(GetMealsByRestaurantId))]
    public async Task<ActionResult<List<MealListModel>>> GetMealsByRestaurantId(int restaurantId)
    {
        return Ok(await _mediator.Send(new GetAllRestaurantMealsQuery(restaurantId)));
    }
    
    [HttpGet("by-name/{name}")]
    [OpenApiOperation(ApiOperationBaseName + nameof(GetMealsByName))]
    public async Task<ActionResult<List<MealListModel>>> GetMealsByName(string name)
    {
        return Ok(await _mediator.Send(new GetMealsByNameQuery(name)));
    }
    
    [HttpGet("by-type/{type}")]
    [OpenApiOperation(ApiOperationBaseName + nameof(GetMealsByType))]
    public async Task<ActionResult<List<MealListModel>>> GetMealsByType(string type)
    {
        return Ok(await _mediator.Send(new GetMealsByTypeQuery(type)));
    }
}
