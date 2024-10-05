using AutoMapper;
using FoodDelivery.DAL.EFCore.Entities;
using FoodDelivery.DAL.Entities;
using FoodDelivery.Shared.Models.FeedbacksModels;
using FoodDelivery.Shared.Models.OrderItemsModels;

namespace FoodDelivery.BL.Profiles.OrderItemProfiles;

internal class OrderItemCreateProfile : Profile
{
	public OrderItemCreateProfile()
	{
        CreateMap<OrderItemCreateModel, OrderItemEntity>();
    }
}
