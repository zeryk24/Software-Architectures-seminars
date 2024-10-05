using FoodDelivery.Shared.Models.MealsModels;
using MediatR;

namespace FoodDelivery.BL.Commands.MealCommands;

public record UpdateMealCommand(MealUpdateModel MealUpdateModel) : IRequest<MealDetailModel>;
