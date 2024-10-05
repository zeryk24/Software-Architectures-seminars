using AutoMapper;
using FoodDelivery.DAL.EFCore.Entities;
using FoodDelivery.DAL.Entities;
using FoodDelivery.Shared.Models.AddressModels;

namespace FoodDelivery.BL.Profiles.AddressProfiles;

internal class AddressDetailProfile : Profile
{
	public AddressDetailProfile()
	{
        CreateMap<AddressEntity, AddressDetailModel>().ReverseMap();
    }
}
