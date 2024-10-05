using AutoMapper;
using FoodDelivery.DAL.EFCore.Entities;
using FoodDelivery.DAL.Entities;
using FoodDelivery.Shared.Models.MealsModels;
using FoodDelivery.Shared.Models.OrderItemsModels;

namespace FoodDelivery.BL.Profiles.OrderItemProfiles;
internal class OrderItemListProfile : Profile
{
	public OrderItemListProfile()
	{
        CreateMap<OrderItemEntity, OrderItemListModel>();
    }
}
