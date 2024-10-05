using AutoMapper;
using FoodDelivery.DAL.EFCore.Entities;
using FoodDelivery.DAL.Entities;
using FoodDelivery.Shared.Models.FeedbacksModels;

namespace FoodDelivery.BL.Profiles.FeedbacksProfiles;

internal class FeedbackDetailProfile : Profile
{
	public FeedbackDetailProfile()
	{
        CreateMap<FeedbackEntity, FeedbackDetailModel>().ReverseMap();
    }
}
