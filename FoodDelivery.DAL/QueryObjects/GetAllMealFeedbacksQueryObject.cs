using FoodDelivery.DAL.Database;
using FoodDelivery.DAL.EFCore.QueryObjects.Base;
using FoodDelivery.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FoodDelivery.DAL.EFCore.Database;
using FoodDelivery.DAL.EFCore.Entities;
using FoodDelivery.DAL.Infrastructure.QueryObjects.Interfaces;
using FoodDelivery.DAL.Infrastructure.QueryObjects.Interfaces.Base;
using Microsoft.EntityFrameworkCore;
using FoodDelivery.DAL.EFCore.QueryObjects.QueryObjectExceptions;

namespace FoodDelivery.DAL.EFCore.QueryObjects;

public class GetAllMealFeedbacksQueryObject : EFCoreQueryObject<FeedbackEntity>, IGetAllMealFeedbacksQueryObject<FeedbackEntity>
{
    private bool _isFilterUsed = false;

    public GetAllMealFeedbacksQueryObject(ApplicationDbContext dbContext) : base(dbContext) { }

    public IQueryObject<FeedbackEntity> UseFilter(int mealId)
    {
        Filter(feedback => feedback.MealId == mealId);
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