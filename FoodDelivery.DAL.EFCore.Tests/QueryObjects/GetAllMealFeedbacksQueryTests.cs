using FluentAssertions;
using FoodDelivery.DAL.Database;
using FoodDelivery.DAL.EFCore.Database;
using FoodDelivery.DAL.EFCore.Entities;
using FoodDelivery.DAL.EFCore.QueryObjects;
using FoodDelivery.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace FoodDelivery.DAL.EFCore.Tests.QueryObjects;

public class GetAllMealFeedbacksQueryTests
{
    private readonly DbContextOptions<ApplicationDbContext> _options;
    
    public GetAllMealFeedbacksQueryTests()
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
            new () { Id = 1, Rating = 1, MealId = 1, Description = "Too bad... Didn't like it at all." },
            new () { Id = 2, Rating = 2, MealId = 2, Description = "Eh..." },
            new () { Id = 3, Rating = 3, MealId = 3, Description = "Not great, not terrible." },
            new () { Id = 4, Rating = 4, MealId = 3, Description = "Liked it a lot!!" },
            new () { Id = 5, Rating = 5, MealId = 3, Description = "Awesome! Best thing I ever eaten!" },
            new () { Id = 6, Rating = 5, MealId = 4, Description = "Awesome! Best thing I ever eaten!" },
            new () { Id = 7, Rating = 4, MealId = 4, Description = "Liked it a lot!!" },
            new () { Id = 8, Rating = 3, MealId = 5, Description = "Not great, not terrible." },
            new () { Id = 9, Rating = 2, MealId = 6, Description = "Eh..." },
            new () { Id = 10, Rating = 1, MealId = 6, Description = "Too bad... Didn't like it at all." }
        });
        dbContext.SaveChanges();
    }
    
    [Fact]
    public async void GetAllMealFeedbacks_ValidMealId_ValidFeedbacks()
    {
        await using var dbContext = new ApplicationDbContext(_options);

        var query = new GetAllMealFeedbacksQueryObject(dbContext);
        query.UseFilter(1);

        var actual = await query
            .ExecuteAsync();

        actual.Should()
            .ContainSingle().And
            .Satisfy(f => f.Id == 1 && f.Rating == 1);
    }
    
    [Fact]
    public async void GetAllMealFeedbacks_InvalidMealId_Empty()
    {
        await using var dbContext = new ApplicationDbContext(_options);

        var query = new GetAllMealFeedbacksQueryObject(dbContext);
        query.UseFilter(10);

        var actual = await query
            .ExecuteAsync();

        actual.Should()
            .BeEmpty();
    }
    
    [Fact]
    public async void GetAllMealFeedbacks_ValidMealIdFiltered_ValidFeedbacksFiltered()
    {
        await using var dbContext = new ApplicationDbContext(_options);

        var query = new GetAllMealFeedbacksQueryObject(dbContext);
        query.UseFilter(3);

        var actual = await query
            .Filter(f => f.Rating == 5)
            .ExecuteAsync();

        actual.Should()
            .ContainSingle().And
            .Satisfy(f => f.Id == 5);
    }
    
    [Fact]
    public async void GetAllMealFeedbacks_ValidMealIdPaged_ValidFeedbacksPaged()
    {
        await using var dbContext = new ApplicationDbContext(_options);

        var query = new GetAllMealFeedbacksQueryObject(dbContext);
        query.UseFilter(3);

        var actual = await query
            .Page(1, 2)
            .ExecuteAsync();

        actual.Should()
            .HaveCount(2).And
            .Satisfy(f => f.Id == 3,
                f => f.Id == 4);
    }
    
    [Fact]
    public async void GetAllMealFeedbacks_ValidMealIdOrdered_ValidFeedbacksOrdered()
    {
        await using var dbContext = new ApplicationDbContext(_options);

        var query = new GetAllMealFeedbacksQueryObject(dbContext);
        query.UseFilter(3);

        var actual = await query
            .OrderBy(f => f.Rating)
            .ExecuteAsync();

        actual.Should()
            .HaveCount(3).And
            .Satisfy(f => f.Id == 3,
                f => f.Id == 4,
                f => f.Id == 5).And
            .BeInAscendingOrder(f => f.Rating);
    }
}