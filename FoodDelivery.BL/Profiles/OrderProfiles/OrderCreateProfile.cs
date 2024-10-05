using AutoMapper;
using FoodDelivery.DAL.EFCore.Entities;
using FoodDelivery.Shared.Models.OrderModels;

namespace FoodDelivery.BL.Profiles.OrderProfiles;

internal class OrderCreateProfile : Profile
{
	public OrderCreateProfile()
	{
        CreateMap<OrderCreateModel, OrderEntity>();
    }
}