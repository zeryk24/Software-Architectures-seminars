using AutoMapper;
using FoodDelivery.DAL.EFCore.Entities;
using FoodDelivery.DAL.Entities;
using FoodDelivery.Shared.Models.AddressModels;

namespace FoodDelivery.BL.Profiles.AddressProfiles;
internal class AddressUpdateProfile : Profile
{
	public AddressUpdateProfile()
	{
        CreateMap<AddressUpdateModel, AddressEntity>();
    }
}
