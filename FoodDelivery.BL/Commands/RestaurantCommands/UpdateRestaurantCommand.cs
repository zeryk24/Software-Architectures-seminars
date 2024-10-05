using System.Security.Claims;
using FoodDelivery.DAL.EFCore.Entities;
using FoodDelivery.Shared.Models.RestaurantModels;
using MediatR;

namespace FoodDelivery.BL.Commands.RestaurantCommands;

public record UpdateRestaurantCommand(RestaurantUpdateModel RestaurantUpdateModel) : IRequest<RestaurantDetailModel>;

