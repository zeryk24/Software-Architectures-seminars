using AutoMapper;
using FoodDelivery.DAL.EFCore.Entities;
using FoodDelivery.DAL.Entities;
using FoodDelivery.Shared.Models.MealsModels;
using FoodDelivery.Shared.Models.RestaurantModels;

namespace FoodDelivery.BL.Profiles.RestaurantProfiles; 
internal class RestaurantListProfile : Profile 
{
	public RestaurantListProfile()
	{
        CreateMap<RestaurantEntity, RestaurantListModel>();
    }
}
