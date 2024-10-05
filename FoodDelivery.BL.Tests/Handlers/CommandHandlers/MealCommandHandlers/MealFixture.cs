using FoodDelivery.DAL.EFCore.Entities;
using FoodDelivery.Shared.Models.MealModels;
using FoodDelivery.Shared.Models.MealsModels;

namespace FoodDelivery.BL.Tests.Handlers.CommandHandlers.MealCommandHandlers;

public class MealFixture
{
    public MealEntity MealEntity { get; set; }
    public MealCreateModel MealCreateModel { get; set; }
    public MealDetailModel MealDetailModel { get; set; }
    public MealUpdateModel MealUpdateModel { get; set; }

    public MealFixture()
    {
        MealEntity = new MealEntity
        {
            Id = 1,
            Name = "string",
            Description = "string",
            Price = 10.0,
            MealType = "string",
            RestaurantId = 1,
        };
        
        MealCreateModel = new MealCreateModel
        {
            Name = "string",
            Description = "string",
            Price = 10.0,
            MealType = "string",
            RestaurantId = 1,
        };
        
        MealDetailModel = new MealDetailModel
        {
            Id = 1,
            Name = "string",
            Description = "string",
            Price = 10.0,
            MealType = "string",
            RestaurantId = 1,
        };
        
        MealUpdateModel = new MealUpdateModel
        {
            Id = 1,
            Name = "string",
            Description = "string",
            Price = 10.0,
            MealType = "string",
        };
    }
}