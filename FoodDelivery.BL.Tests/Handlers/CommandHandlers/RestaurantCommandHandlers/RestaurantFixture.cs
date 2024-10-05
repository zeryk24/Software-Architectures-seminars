using FoodDelivery.DAL.EFCore.Entities;
using FoodDelivery.Shared.Models.RestaurantModels;

namespace FoodDelivery.BL.Tests.Handlers.CommandHandlers.RestaurantCommandHandlers;

public class RestaurantFixture
{
    public RestaurantEntity RestaurantEntity { get; set; }
    public RestaurantCreateModel RestaurantCreateModel { get; set; }
    public RestaurantDetailModel RestaurantDetailModel { get; set; }
    public RestaurantUpdateModel RestaurantUpdateModel { get; set; }

    public RestaurantFixture()
    {
        RestaurantEntity = new RestaurantEntity
        {
            Id = 1,
            Name = "hello",
            Disabled = false
        };

        RestaurantCreateModel = new RestaurantCreateModel
        {
            Name = "hello",
        };

        RestaurantDetailModel = new RestaurantDetailModel
        {
            Id = 1,
            Name = "hello",
            Disabled = false,
        };

        RestaurantUpdateModel = new RestaurantUpdateModel
        {
            Id = 1,
            Name = "hello",
        };
    }
    
}