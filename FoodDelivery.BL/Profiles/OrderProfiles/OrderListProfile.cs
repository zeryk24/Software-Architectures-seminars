using AutoMapper;
using FoodDelivery.DAL.EFCore.Entities;
using FoodDelivery.DAL.Entities;
using FoodDelivery.Shared.Models.MealsModels;
using FoodDelivery.Shared.Models.OrderModels;

namespace FoodDelivery.BL.Profiles.OrderProfiles; 
internal class OrderListProfile : Profile
{
	public OrderListProfile()
	{
        CreateMap<OrderEntity, OrderListModel>();
    }
}
