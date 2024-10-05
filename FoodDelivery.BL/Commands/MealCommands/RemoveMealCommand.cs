using MediatR;

namespace FoodDelivery.BL.Commands.MealCommands;

public record RemoveMealCommand(int Id) : IRequest<bool>;
