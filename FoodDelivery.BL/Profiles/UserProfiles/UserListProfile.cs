using AutoMapper;
using FoodDelivery.DAL.EFCore.Entities;
using FoodDelivery.Shared.Models.RestaurantModels;
using FoodDelivery.Shared.Models.UserModels;

namespace FoodDelivery.BL.Profiles.UserProfiles; 
internal class UserListProfile : Profile 
{
    public UserListProfile()
    {
        CreateMap<UserEntity, UserListModel>();
    }
}