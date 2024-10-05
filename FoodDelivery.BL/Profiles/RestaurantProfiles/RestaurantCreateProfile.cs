﻿using AutoMapper;
using FoodDelivery.DAL.EFCore.Entities;
using FoodDelivery.DAL.Entities;
using FoodDelivery.Shared.Models.RestaurantModels;

namespace FoodDelivery.BL.Profiles.RestaurantProfiles; 
internal class RestaurantCreateProfile : Profile
{
	public RestaurantCreateProfile()
	{
		CreateMap<RestaurantCreateModel, RestaurantEntity>();
	} 
}
