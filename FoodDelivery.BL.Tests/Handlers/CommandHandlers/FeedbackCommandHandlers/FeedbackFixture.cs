using FoodDelivery.DAL.EFCore.Entities;
using FoodDelivery.Shared.Models.FeedbackModels;
using FoodDelivery.Shared.Models.FeedbacksModels;

namespace FoodDelivery.BL.Tests.Handlers.CommandHandlers.FeedbackCommandHandlers;

public class FeedbackFixture
{
    public FeedbackEntity MealFeedbackEntity { get; set; }
    public FeedbackEntity RestaurantFeedbackEntity { get; set; }
    public MealFeedbackCreateModel MealFeedbackCreateModel { get; set; }
    public RestaurantFeedbackCreateModel RestaurantFeedbackCreateModel { get; set; }

    public FeedbackDetailModel MealFeedbackDetailModel { get; set; }
    public FeedbackDetailModel RestaurantFeedbackDetailModel { get; set; }

    public FeedbackFixture()
    {
        MealFeedbackEntity = new FeedbackEntity
        {
            Id = 1,
            UserId = 1,
            MealId = 1,
            Rating = 3,
            Description = "Not great, not terrible.",
        };
        
        RestaurantFeedbackEntity = new FeedbackEntity
        {
            Id = 2,
            UserId = 1,
            RestaurantId = 1,
            Rating = 3,
            Description = "Not great, not terrible.",
        };

        MealFeedbackCreateModel = new MealFeedbackCreateModel
        {
            MealId = 1,
            Rating = 3,
            Description = "Not great, not terrible."
        };
        
        RestaurantFeedbackCreateModel = new RestaurantFeedbackCreateModel
        {
            RestaurantId = 1,
            Rating = 3,
            Description = "Not great, not terrible."
        };

        MealFeedbackDetailModel = new FeedbackDetailModel
        {
            Id = 1,
            UserId = 1,
            MealId = 1,
            Rating = 3,
            Description = "Not great, not terrible.",
        };
        
        MealFeedbackDetailModel = new FeedbackDetailModel
        {
            Id = 2,
            UserId = 1,
            RestaurantId = 1,
            Rating = 3,
            Description = "Not great, not terrible.",
        };
    }
}