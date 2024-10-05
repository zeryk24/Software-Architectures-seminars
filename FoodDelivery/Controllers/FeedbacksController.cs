using FoodDelivery.BL.Commands.FeedbackCommands;
using FoodDelivery.BL.Queries.FeedbackQueries;
using FoodDelivery.BL.Queries.MealQueries;
using FoodDelivery.BL.Queries.RestaurantQueries;
using FoodDelivery.Shared.Constants;
using FoodDelivery.Shared.Models.FeedbackModels;
using FoodDelivery.Shared.Models.FeedbacksModels;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NSwag.Annotations;

namespace FoodDelivery.Controllers;

[ApiController]
[Route("api/[controller]")]
public class FeedbacksController : ControllerBase
{
    private const string ApiOperationBaseName = "Feedback";
    private readonly IMediator _mediator;

    public FeedbacksController(IMediator mediator)
    {
        _mediator = mediator;
    }
    
    [Authorize(Roles = Constants.Roles.Admin)]
    [HttpGet]
    [OpenApiOperation(ApiOperationBaseName + nameof(GetAllFeedbacks))]
    public async Task<ActionResult<List<FeedbackListModel>>> GetAllFeedbacks(int page, int pageSize)
    {
        return Ok(await _mediator.Send(new GetAllFeedbacksQuery(page, pageSize)));
    }
    
    [Authorize(Roles = Constants.Roles.Customer)]
    [HttpPost("Meal")]
    [OpenApiOperation(ApiOperationBaseName + nameof(CreateMealFeedback))]
    public async Task<ActionResult<FeedbackDetailModel>> CreateMealFeedback(MealFeedbackCreateModel mealFeedbackCreateModel)
    {
        return Ok(await _mediator.Send(new CreateMealFeedbackCommand(mealFeedbackCreateModel, User)));
    }
    
    [Authorize(Roles = Constants.Roles.Customer)]
    [HttpPost("Restaurant")]
    [OpenApiOperation(ApiOperationBaseName + nameof(CreateRestaurantFeedback))]
    public async Task<ActionResult<FeedbackDetailModel>> CreateRestaurantFeedback(RestaurantFeedbackCreateModel restaurantFeedbackCreateModel)
    {
        return Ok(await _mediator.Send(new CreateRestaurantFeedbackCommand(restaurantFeedbackCreateModel, User)));
    }

    [Authorize(Roles = Constants.Roles.Customer)]
    [HttpDelete("{feedbackId}")]
    [OpenApiOperation(ApiOperationBaseName + nameof(RemoveFeedback))]
    public async Task<ActionResult<bool>> RemoveFeedback(int feedbackId)
    {
        var result = await _mediator.Send(new RemoveFeedbackCommand(feedbackId, User));
        return result ? Ok(result) : BadRequest();
    }
    
    [HttpGet("Meal/{mealId}")]
    [OpenApiOperation(ApiOperationBaseName + nameof(GetFeedbacksByMealId))]
    public async Task<ActionResult<List<FeedbackListModel>>> GetFeedbacksByMealId(int mealId)
    {
        return Ok(await _mediator.Send(new GetAllMealFeedbacksQuery(mealId)));
    }
    
    [HttpGet("Restaurant/{restaurantId}")]
    [OpenApiOperation(ApiOperationBaseName + nameof(GetFeedbacksByRestaurantId))]
    public async Task<ActionResult<List<FeedbackListModel>>> GetFeedbacksByRestaurantId(int restaurantId)
    {
        return Ok(await _mediator.Send(new GetAllRestaurantFeedbacksQuery(restaurantId)));
    }
}