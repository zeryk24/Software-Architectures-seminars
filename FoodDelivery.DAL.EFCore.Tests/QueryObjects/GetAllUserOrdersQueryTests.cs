using FluentAssertions;
using FoodDelivery.DAL.Database;
using FoodDelivery.DAL.EFCore.Database;
using FoodDelivery.DAL.EFCore.Entities;
using FoodDelivery.DAL.EFCore.QueryObjects;
using FoodDelivery.DAL.Entities;
using FoodDelivery.Shared.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace FoodDelivery.DAL.EFCore.Tests.QueryObjects;

public class GetAllUserOrdersQueryTests
{
    private readonly DbContextOptions<ApplicationDbContext> _options;
    
    public GetAllUserOrdersQueryTests()
    {
        var serviceProvider = new ServiceCollection()
            .AddEntityFrameworkInMemoryDatabase()
            .BuildServiceProvider();

        _options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase($"test_db_{Guid.NewGuid()}")
            .UseInternalServiceProvider(serviceProvider)
            .Options;
        using var dbContext = new ApplicationDbContext(_options);
        dbContext.AddRange(new List<OrderEntity>
        {
            new () { Id = 1, UserId = 1, PaymentType = PaymentType.Card },
            new () { Id = 2, UserId = 1, PaymentType = PaymentType.Card },
            new () { Id = 3, UserId = 2, PaymentType = PaymentType.Card },
            new () { Id = 4, UserId = 3, PaymentType = PaymentType.Card },
            new () { Id = 5, UserId = 3, PaymentType = PaymentType.Card },
            new () { Id = 6, UserId = 3, PaymentType = PaymentType.Cash },
            new () { Id = 7, UserId = 3, PaymentType = PaymentType.Cash },
            new () { Id = 8, UserId = 3, PaymentType = PaymentType.Cash },
            new () { Id = 9, UserId = 3, PaymentType = PaymentType.Cash },
            new () { Id = 10, UserId = 4, PaymentType = PaymentType.Coupon }
        });
        dbContext.SaveChanges();
    }
    
    [Fact]
    public async void GetAllUserOrders_ValidUserId_ValidOrders()
    {
        await using var dbContext = new ApplicationDbContext(_options);

        var query = new GetAllUserOrdersQueryObject(dbContext);
        query.UseFilter(3);

        var actual = await query
            .ExecuteAsync();

        actual.Should()
            .HaveCount(6).And
            .OnlyContain(o => o.Id >= 4 && o.Id <= 9);
    }
    
    [Fact]
    public async void GetAllUserOrders_InvalidUserId_Empty()
    {
        await using var dbContext = new ApplicationDbContext(_options);

        var query = new GetAllUserOrdersQueryObject(dbContext);
        query.UseFilter(10);

        var actual = await query
            .ExecuteAsync();

        actual.Should()
            .BeEmpty();
    }
    
    [Fact]
    public async void GetAllUserOrders_ValidUserIdFiltered_ValidOrdersFiltered()
    {
        await using var dbContext = new ApplicationDbContext(_options);

        var query = new GetAllUserOrdersQueryObject(dbContext);
        query.UseFilter(3);

        var actual = await query
            .Filter(o => o.PaymentType == PaymentType.Card)
            .ExecuteAsync();

        actual.Should()
            .HaveCount(2).And
            .Satisfy(o => o.Id == 4,
                o => o.Id == 5);
    }   
    
    [Fact]
    public async void GetAllUserOrders_ValidUserIdPaged_ValidOrdersPaged()
    {
        await using var dbContext = new ApplicationDbContext(_options);

        var query = new GetAllUserOrdersQueryObject(dbContext);
        query.UseFilter(3);

        var actual = await query
            .Page(2, 3)
            .ExecuteAsync();

        actual.Should()
            .HaveCount(3).And
            .Satisfy(o => o.Id == 7,
                o => o.Id == 8,
                o => o.Id == 9).And
            .OnlyContain(o => o.PaymentType == PaymentType.Cash);
    }  
    
    [Fact]
    public async void GetAllUserOrders_ValidUserIdOrderedDescending_ValidOrdersOrderedDescending()
    {
        await using var dbContext = new ApplicationDbContext(_options);

        var query = new GetAllUserOrdersQueryObject(dbContext);
        query.UseFilter(3);

        var actual = await query
            .OrderBy(o => o.Id, false)
            .ExecuteAsync();

        actual.Should()
            .HaveCount(6).And
            .BeInDescendingOrder(o => o.Id);
    }
}