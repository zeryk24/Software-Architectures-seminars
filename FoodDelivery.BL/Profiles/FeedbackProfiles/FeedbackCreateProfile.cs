using AutoMapper;
using FoodDelivery.DAL.EFCore.Entities;
using FoodDelivery.DAL.Entities;
using FoodDelivery.Shared.Models.FeedbackModels;
using FoodDelivery.Shared.Models.FeedbacksModels;

namespace FoodDelivery.BL.Profiles.FeedbacksProfiles;

internal class FeedbackCreateProfile : Profile
{
	public FeedbackCreateProfile()
	{
		CreateMap<MealFeedbackCreateModel, FeedbackEntity>();
	}
}
