using AutoMapper;
using FoodDelivery.DAL.EFCore.Entities;
using FoodDelivery.Shared.Models.OrderModels;

namespace FoodDelivery.BL.Profiles.OrderProfiles;

internal class OrderChangePaymentTypeProfile : Profile
{
    public OrderChangePaymentTypeProfile()
    {
        CreateMap<OrderChangePaymentTypeModel, OrderEntity>();
    }
}