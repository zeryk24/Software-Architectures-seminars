using FoodDelivery.DAL.Database;
using FoodDelivery.DAL.EFCore.Database;
using FoodDelivery.DAL.EFCore.Entities;
using FoodDelivery.DAL.EFCore.QueryObjects.Base;
using FoodDelivery.DAL.EFCore.QueryObjects.QueryObjectExceptions;
using FoodDelivery.DAL.Entities;
using FoodDelivery.DAL.Infrastructure.QueryObjects.Interfaces;
using FoodDelivery.DAL.Infrastructure.QueryObjects.Interfaces.Base;

namespace FoodDelivery.DAL.EFCore.QueryObjects;

public class GetAllUserOrdersQueryObject : EFCoreQueryObject<OrderEntity>, IGetAllUserOrdersQueryObject<OrderEntity>
{
    private bool _isFilterUsed = false;

    public GetAllUserOrdersQueryObject(ApplicationDbContext dbContext) : base(dbContext) { }

    public IQueryObject<OrderEntity> UseFilter(int userId)
    {
        Filter(order => order.UserId == userId);
        _isFilterUsed = true;
        return this;
    }

    public override Task<IEnumerable<OrderEntity>> ExecuteAsync()
    {
        if (!_isFilterUsed)
            throw new FilterNotUsedException("Filter was not used, use UseFilter() method first.");

        return base.ExecuteAsync();
    }
}
