using FoodDelivery.DAL.Database;
using FoodDelivery.DAL.EFCore.Database;
using FoodDelivery.DAL.EFCore.Entities;
using FoodDelivery.DAL.EFCore.QueryObjects.Base;
using FoodDelivery.DAL.EFCore.QueryObjects.QueryObjectExceptions;
using FoodDelivery.DAL.Entities;
using FoodDelivery.DAL.Infrastructure.QueryObjects.Interfaces;
using FoodDelivery.DAL.Infrastructure.QueryObjects.Interfaces.Base;

namespace FoodDelivery.DAL.EFCore.QueryObjects;

public class GetMealsByNameQueryObject : EFCoreQueryObject<MealEntity>, IGetMealsByNameQueryObject<MealEntity>
{
    private bool _isFilterUsed = false;

    public GetMealsByNameQueryObject(ApplicationDbContext dbContext) : base(dbContext) { }

    public IQueryObject<MealEntity> UseFilter(string mealName)
    {
        Filter(meal => meal.Name.ToLower().Contains(mealName.ToLower()));
        _isFilterUsed = true;
        return this;
    }

    public override Task<IEnumerable<MealEntity>> ExecuteAsync()
    {
        if (!_isFilterUsed)
            throw new FilterNotUsedException("Filter was not used, use UseFilter() method first.");

        return base.ExecuteAsync();
    }
}