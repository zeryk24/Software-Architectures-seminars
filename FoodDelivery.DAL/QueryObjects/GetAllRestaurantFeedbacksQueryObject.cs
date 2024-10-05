using FoodDelivery.DAL.Database;
using FoodDelivery.DAL.EFCore.Database;
using FoodDelivery.DAL.EFCore.Entities;
using FoodDelivery.DAL.EFCore.QueryObjects.Base;
using FoodDelivery.DAL.EFCore.QueryObjects.QueryObjectExceptions;
using FoodDelivery.DAL.Entities;
using FoodDelivery.DAL.Infrastructure.QueryObjects.Interfaces;
using FoodDelivery.DAL.Infrastructure.QueryObjects.Interfaces.Base;

namespace FoodDelivery.DAL.EFCore.QueryObjects;

public class GetAllRestaurantFeedbacksQueryObject : EFCoreQueryObject<FeedbackEntity>, IGetAllRestaurantFeedbacksQueryObject<FeedbackEntity>
{
    private bool _isFilterUsed = false;

    public GetAllRestaurantFeedbacksQueryObject(ApplicationDbContext dbContext) : base(dbContext) { }

    public IQueryObject<FeedbackEntity> UseFilter(int restaurantId)
    {
        Filter(feedback => feedback.RestaurantId == restaurantId);
        _isFilterUsed = true;
        return this;
    }

    public override Task<IEnumerable<FeedbackEntity>> ExecuteAsync()
    {
        if (!_isFilterUsed)
            throw new FilterNotUsedException("Filter was not used, use UseFilter() method first.");

        return base.ExecuteAsync();
    }
}