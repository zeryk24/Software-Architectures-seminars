using AutoMapper;
using FoodDelivery.DAL.EFCore.Entities;
using FoodDelivery.DAL.Entities;
using FoodDelivery.Shared.Models.MealsModels;

namespace FoodDelivery.BL.Profiles.MealProfiles;

internal class MealUpdateProfile : Profile
{
	public MealUpdateProfile()
	{
		CreateMap<MealUpdateModel, MealEntity>();
	}
}
