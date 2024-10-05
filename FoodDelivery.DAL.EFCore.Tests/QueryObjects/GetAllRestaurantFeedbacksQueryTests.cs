using FluentAssertions;
using FoodDelivery.DAL.Database;
using FoodDelivery.DAL.EFCore.Database;
using FoodDelivery.DAL.EFCore.Entities;
using FoodDelivery.DAL.EFCore.QueryObjects;
using FoodDelivery.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace FoodDelivery.DAL.EFCore.Tests.QueryObjects;

public class GetAllRestaurantFeedbacksQueryTests
{
    private readonly DbContextOptions<ApplicationDbContext> _options;
    
    public GetAllRestaurantFeedbacksQueryTests()
    {
        var serviceProvider = new ServiceCollection()
            .AddEntityFrameworkInMemoryDatabase()
            .BuildServiceProvider();

        _options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase($"test_db_{Guid.NewGuid()}")
            .UseInternalServiceProvider(serviceProvider)
            .Options;
        using var dbContext = new ApplicationDbContext(_options);
        dbContext.AddRange(new List<FeedbackEntity>
        {
            new () { Id = 11, Rating = 1, RestaurantId = 1, Description = "Really bad place." },
            new () { Id = 12, Rating = 2, RestaurantId = 1, Description = "Won't come again..." },
            new () { Id = 13, Rating = 3, RestaurantId = 1, Description = "Could have been better." },
            new () { Id = 14, Rating = 4, RestaurantId = 1, Description = "Great place!" },
            new () { Id = 15, Rating = 5, RestaurantId = 2, Description = "My favorite place! I highly recommend!" },
            new () { Id = 16, Rating = 5, RestaurantId = 2, Description = "My favorite place! I highly recommend!" },
            new () { Id = 17, Rating = 4, RestaurantId = 3, Description = "Great place!" },
            new () { Id = 18, Rating = 3, RestaurantId = 4, Description = "Could have been better." },
            new () { Id = 19, Rating = 2, RestaurantId = 5, Description = "Won't come again..." },
            new () { Id = 20, Rating = 1, RestaurantId = 5, Description = "Really bad place."}
        });
        dbContext.SaveChanges();
    }
    
    [Fact]
    public async void GetAllRestaurantFeedbacks_ValidRestaurantId_ValidFeedbacks()
    {
        await using var dbContext = new ApplicationDbContext(_options);

        var query = new GetAllRestaurantFeedbacksQueryObject(dbContext);
        query.UseFilter(2);

        var actual = await query
            .ExecuteAsync();

        actual.Should()
            .HaveCount(2).And
            .Satisfy(f => f.Id == 15,
                f => f.Id == 16);
    }
    
    [Fact]
    public async void GetAllRestaurantFeedbacks_InvalidRestaurantId_Empty()
    {
        await using var dbContext = new ApplicationDbContext(_options);

        var query = new GetAllRestaurantFeedbacksQueryObject(dbContext);
        query.UseFilter(24);

        var actual = await query
            .ExecuteAsync();

        actual.Should()
            .BeEmpty();
    }
    
    [Fact]
    public async void GetAllRestaurantFeedbacks_ValidRestaurantIdFiltered_ValidFeedbacksFiltered()
    {
        await using var dbContext = new ApplicationDbContext(_options);

        var query = new GetAllRestaurantFeedbacksQueryObject(dbContext);
        query.UseFilter(1);

        var actual = await query
            .Filter(f => f.Rating == 1 || f.Rating == 2)
            .ExecuteAsync();

        actual.Should()
            .HaveCount(2).And
            .Satisfy(f => f.Id == 11,
                f => f.Id == 12);
    }
    
    [Fact]
    public async void GetAllRestaurantFeedbacks_ValidRestaurantIdPaged_ValidFeedbacksPaged()
    {
        await using var dbContext = new ApplicationDbContext(_options);

        var query = new GetAllRestaurantFeedbacksQueryObject(dbContext);
        query.UseFilter(1);

        var actual = await query
            .Page(2, 2)
            .ExecuteAsync();

        actual.Should()
            .HaveCount(2).And
            .Satisfy(f => f.Id == 13,
                f => f.Id == 14);
    }
    
    [Fact]
    public async void GetAllRestaurantFeedbacks_ValidRestaurantIdOrdered_ValidFeedbacksOrdered()
    {
        await using var dbContext = new ApplicationDbContext(_options);

        var query = new GetAllRestaurantFeedbacksQueryObject(dbContext);
        query.UseFilter(1);

        var actual = await query
            .OrderBy(f => f.Rating)
            .ExecuteAsync();

        actual.Should()
            .HaveCount(4).And
            .BeInAscendingOrder(f => f.Rating).And
            .Satisfy(f => f.Id == 11,
                f => f.Id == 12,
                f => f.Id == 13,
                f => f.Id == 14);
    }
}