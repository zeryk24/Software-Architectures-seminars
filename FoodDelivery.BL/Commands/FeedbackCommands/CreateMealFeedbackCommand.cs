using System.Security.Claims;
using FoodDelivery.Shared.Models.FeedbackModels;
using FoodDelivery.Shared.Models.FeedbacksModels;
using MediatR;

namespace FoodDelivery.BL.Commands.FeedbackCommands;

public record CreateMealFeedbackCommand(MealFeedbackCreateModel MealFeedbackCreateModel,
    ClaimsPrincipal User) : IRequest<FeedbackDetailModel>;
