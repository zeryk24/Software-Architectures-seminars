using AutoMapper;
using FoodDelivery.DAL.EFCore.Entities;
using FoodDelivery.DAL.Entities;
using FoodDelivery.Shared.Models.FeedbacksModels;

namespace FoodDelivery.BL.Profiles.FeedbacksProfiles;

public class FeedbacksListProfile : Profile
{
	public FeedbacksListProfile()
	{
        CreateMap<FeedbackEntity, FeedbackListModel>();
    }
}
