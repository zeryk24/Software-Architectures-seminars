using System.Security.Claims;
using FoodDelivery.Shared.Models.FeedbacksModels;
using MediatR;

namespace FoodDelivery.BL.Commands.FeedbackCommands;

public record RemoveFeedbackCommand(int Id, ClaimsPrincipal User) : IRequest<bool>;
