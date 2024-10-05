using AutoMapper;
using FoodDelivery.DAL.EFCore.Entities;
using FoodDelivery.Shared.Models.OrderItemsModels;

namespace FoodDelivery.BL.Profiles.OrderItemProfiles;
internal class OrderItemDetailProfile : Profile
{
	public OrderItemDetailProfile()
	{
        CreateMap<OrderItemEntity, OrderItemDetailModel>().ReverseMap();
    }
}
