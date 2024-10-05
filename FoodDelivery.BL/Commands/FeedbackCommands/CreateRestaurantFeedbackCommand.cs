using System.Security.Claims;
using FoodDelivery.Shared.Models.FeedbacksModels;
using MediatR;

namespace FoodDelivery.BL.Commands.FeedbackCommands;

public record CreateRestaurantFeedbackCommand(RestaurantFeedbackCreateModel RestaurantFeedbackCreateModel,
    ClaimsPrincipal User) : IRequest<FeedbackDetailModel>;
