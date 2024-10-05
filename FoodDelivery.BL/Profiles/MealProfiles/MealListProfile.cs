using AutoMapper;
using FoodDelivery.DAL.EFCore.Entities;
using FoodDelivery.DAL.Entities;
using FoodDelivery.Shared.Models.MealsModels;

namespace FoodDelivery.BL.Profiles.MealProfiles;

internal class MealListProfile : Profile
{
	public MealListProfile()
	{
		CreateMap<MealEntity, MealListModel>();
	}
}
