using AutoMapper;
using FoodDelivery.DAL.EFCore.Entities;
using FoodDelivery.DAL.Entities;
using FoodDelivery.Shared.Models.MealModels;
using FoodDelivery.Shared.Models.MealsModels;

namespace FoodDelivery.BL.Profiles.MealProfiles;

internal class MealCreateProfile : Profile
{
	public MealCreateProfile()
	{
		CreateMap<MealCreateModel, MealEntity>();
	}
}
