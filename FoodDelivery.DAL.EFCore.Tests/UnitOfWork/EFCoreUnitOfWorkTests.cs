using FluentAssertions;
using FoodDelivery.DAL.Database;
using FoodDelivery.DAL.EFCore.Database;
using FoodDelivery.DAL.EFCore.Entities;
using FoodDelivery.DAL.EFCore.UnitOfWork.Base;
using FoodDelivery.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace FoodDelivery.DAL.EFCore.Tests;

public class EFCoreUnitOfWorkTests
{
    private readonly DbContextOptions<ApplicationDbContext> _options;

    public EFCoreUnitOfWorkTests()
    {
        var serviceProvider = new ServiceCollection()
            .AddEntityFrameworkInMemoryDatabase()
            .BuildServiceProvider();

        _options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase($"test_db_{Guid.NewGuid()}")
            .UseInternalServiceProvider(serviceProvider)
            .Options;
        using var dbContext = new ApplicationDbContext(_options);
        dbContext.AddRange(new List<RestaurantEntity>
        {
            new () { Id = 1, Name = "The Private Port"},
            new () { Id = 2, Name = "The Sailing Stranger"},
            new () { Id = 3, Name = "The Juniper Angel"},
            new () { Id = 4, Name = "The Mountain Courtyard"},
            new () { Id = 5, Name = "The Mountain Lantern"},
            new () { Id = 6, Name = "Indigo"},
            new () { Id = 7, Name = "The Peacock"},
            new () { Id = 8, Name = "Sapphire"},
            new () { Id = 9, Name = "The Nightingale"},
            new () { Id = 10, Name = "Mumbles"}
        });
        dbContext.SaveChanges();
    }

    [Fact]
    public async Task Commit_OneValidInsert_ValidCommit()
    {
        await using var dbContext = new ApplicationDbContext(_options);

        using var unitOfWork = new EFCoreUnitOfWork(dbContext);

        var expected = new FeedbackEntity { Description = "whatever", Rating = 1, MealId = 1 };
        unitOfWork.FeedbackRepository.Insert(expected);
        await unitOfWork.Commit();

        var actual = dbContext.Find<FeedbackEntity>(1);
        
        Assert.Equal(expected, actual);
    }
    
    [Fact]
    public async Task Commit_ManyValidInserts_ValidCommit()
    {
        await using var dbContext = new ApplicationDbContext(_options);

        using var unitOfWork = new EFCoreUnitOfWork(dbContext);

        var expected = new List<FeedbackEntity>();
        for (int i = 0; i < 25; i++)
        {
            expected.Add(new FeedbackEntity { Description = "", Rating = 1, MealId = 1 });
            unitOfWork.FeedbackRepository.Insert(expected.Last());
        }
        
        await unitOfWork.Commit();

        var actual = new List<FeedbackEntity>();
        for (int i = 0; i < 25; i++)
        {
            actual.Add(dbContext.Find<FeedbackEntity>(i + 1));
        }

        actual.Should().Equal(expected);
    }
    
    [Fact]
    public async Task Commit_OneInvalidInsertEntityWithAlreadyUsedId_ArgumentException()
    {
        await using var dbContext = new ApplicationDbContext(_options);

        using var unitOfWork = new EFCoreUnitOfWork(dbContext);

        var expected = new RestaurantEntity { Id = 1, Name = "Some name"};
        unitOfWork.RestaurantRepository.Insert(expected);
        await Assert.ThrowsAsync<ArgumentException>(async () => await unitOfWork.Commit());
    }
    
    [Fact]
    public async Task Commit_ValidGetAndUpdate10RestaurantEntities_ValidCommit()
    {
        await using var dbContext = new ApplicationDbContext(_options);

        using var unitOfWork = new EFCoreUnitOfWork(dbContext);

        var expected = new List<RestaurantEntity>();
        for (int i = 0; i < 10; i++)
        {
            expected.Add(await unitOfWork.RestaurantRepository.GetByIdAsync(i + 1));
        }

        foreach (var restaurant in expected)
        {
            restaurant.Name = "";
            unitOfWork.RestaurantRepository.Update(restaurant);
        }

        await unitOfWork.Commit();

        var actual = new List<RestaurantEntity>();
        for (int i = 0; i < 10; i++)
        {
            actual.Add(dbContext.Find<RestaurantEntity>(i + 1));
        }

        actual.Should().Equal(expected);
    }

    [Fact]
    public async Task Commit_InvalidUpdateNotAddedEntity_ArgumentException()
    {
        await using var dbContext = new ApplicationDbContext(_options);

        using var unitOfWork = new EFCoreUnitOfWork(dbContext);
        unitOfWork.RestaurantRepository.Update(new RestaurantEntity { Id = 20, Name = "whatever"});
        
        await Assert.ThrowsAsync<DbUpdateConcurrencyException>(async () => await unitOfWork.Commit());
    }
    
    [Fact]
    public async Task Commit_InvalidInsertEntityWithoutRequiredFields_DbUpdateException()
    {
        await using var dbContext = new ApplicationDbContext(_options);
        using var unitOfWork = new EFCoreUnitOfWork(dbContext);
        unitOfWork.AddressRepository.Insert(new AddressEntity { Street = "Lidicka", Number = "10"});
        
        await Assert.ThrowsAsync<DbUpdateException>(async () => await unitOfWork.Commit());
    }
}