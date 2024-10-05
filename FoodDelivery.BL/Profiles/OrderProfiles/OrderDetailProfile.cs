using AutoMapper;
using FoodDelivery.DAL.EFCore.Entities;
using FoodDelivery.Shared.Models.OrderModels;

namespace FoodDelivery.BL.Profiles.OrderProfiles; 
internal class OrderDetailProfile : Profile
{
	public OrderDetailProfile()
	{
        CreateMap<OrderEntity, OrderDetailModel>().ReverseMap();
    }
}
