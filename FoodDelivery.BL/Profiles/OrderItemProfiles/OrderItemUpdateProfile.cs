using AutoMapper;
using FoodDelivery.DAL.EFCore.Entities;
using FoodDelivery.DAL.Entities;
using FoodDelivery.Shared.Models.OrderItemsModels;

namespace FoodDelivery.BL.Profiles.OrderItemProfiles; 
internal class OrderItemUpdateProfile : Profile
{
	public OrderItemUpdateProfile()
	{
        CreateMap<OrderItemUpdateModel, OrderItemEntity>();
    }
}
