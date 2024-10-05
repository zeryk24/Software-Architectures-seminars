using FluentAssertions;
using FoodDelivery.DAL.Database;
using FoodDelivery.DAL.EFCore.Database;
using FoodDelivery.DAL.EFCore.Entities;
using FoodDelivery.DAL.EFCore.Repositories.Base;
using FoodDelivery.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace FoodDelivery.DAL.EFCore.Tests;

public class EFCoreEntityRepositoryTests
{
    private readonly DbContextOptions<ApplicationDbContext> _options;

    public EFCoreEntityRepositoryTests()
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
    public async Task GetById_ValidId_ValidEntity()
    {
        await using var dbContext = new ApplicationDbContext(_options);

        var repository = new EFCoreEntityRepository<RestaurantEntity>(dbContext);

        var actual = await repository.GetByIdAsync(1);
     
        Assert.Equal("The Private Port", actual.Name);
    }
    
    [Fact]
    public async Task GetById_InvalidId_Null()
    {
        await using var dbContext = new ApplicationDbContext(_options);

        var repository = new EFCoreEntityRepository<RestaurantEntity>(dbContext);

        var actual = await repository.GetByIdAsync(12);

        actual.Should().BeNull();
    }
    
    [Fact]
    public async Task Insert_ValidInsert_EntityAdded()
    {
        await using var dbContext = new ApplicationDbContext(_options);

        var repository = new EFCoreEntityRepository<RestaurantEntity>(dbContext);

        var expected = new RestaurantEntity { Id = 12, Name = "Hello" }; 
        repository.Insert(expected);
        await dbContext.SaveChangesAsync();

        var actual = dbContext.Find<RestaurantEntity>(12);

        Assert.Equal(expected, actual);
    }
    
    [Fact]
    public async Task Insert_Null_NullReferenceException()
    {
        await using var dbContext = new ApplicationDbContext(_options);

        var repository = new EFCoreEntityRepository<RestaurantEntity>(dbContext);
        
        Assert.ThrowsAny<NullReferenceException>(() => repository.Insert(null));
    }
    
    [Fact]
    public async Task Insert_EntityWithAlreadyUsedId_ArgumentException()
    {
        await using var dbContext = new ApplicationDbContext(_options);

        var repository = new EFCoreEntityRepository<RestaurantEntity>(dbContext);
        var entity = new RestaurantEntity { Id = 1, Name = "Whatever" };
        repository.Insert(entity);
        
        await Assert.ThrowsAnyAsync<ArgumentException>(async () => await dbContext.SaveChangesAsync());
    }
    
    [Fact]
    public async Task Insert_EntityWithoutRequiredFields_DbUpdateException()
    {
        await using var dbContext = new ApplicationDbContext(_options);

        var repository = new EFCoreEntityRepository<AddressEntity>(dbContext);
        var entity = new AddressEntity { Street = "Lidicka", Number = "10"};
        repository.Insert(entity);
        
        await Assert.ThrowsAsync<DbUpdateException>(async () => await dbContext.SaveChangesAsync());
    }

    [Fact]
    public async Task Update_ValidEntity_EntityUpdated()
    {
        await using var dbContext = new ApplicationDbContext(_options);

        var repository = new EFCoreEntityRepository<RestaurantEntity>(dbContext);

        var expected = await repository.GetByIdAsync(2);
        expected.Name = "Changed";
        repository.Update(expected);
        await dbContext.SaveChangesAsync();

        var actual = dbContext.Find<RestaurantEntity>(2);
        
        Assert.Equal("Changed", actual.Name);
        Assert.Equal(expected, actual);
    }
    
    [Fact]
    public async Task Update_NotAddedEntity_DbUpdateConcurrencyException()
    {
        await using var dbContext = new ApplicationDbContext(_options);

        var repository = new EFCoreEntityRepository<RestaurantEntity>(dbContext);

        var expected = new RestaurantEntity { Name = "Changed", Id = 12};
        repository.Update(expected);
        
        await Assert.ThrowsAsync<DbUpdateConcurrencyException>(async () => await dbContext.SaveChangesAsync());
    }
    
    [Fact]
    public async Task Update_ChangeKey_InvalidOperationException()
    {
        await using var dbContext = new ApplicationDbContext(_options);

        var repository = new EFCoreEntityRepository<RestaurantEntity>(dbContext);

        var expected = await repository.GetByIdAsync(1);
        expected.Id = 2;
        Assert.Throws<InvalidOperationException>(() => repository.Update(expected));
    }

    [Fact]
    public async Task Remove_ValidId_EntityRemovedAsync()
    {
        using var dbContext = new ApplicationDbContext(_options);

        var repository = new EFCoreEntityRepository<RestaurantEntity>(dbContext);
        
        await repository.RemoveAsync(1);
        dbContext.SaveChanges();

        Assert.Null(dbContext.Find<RestaurantEntity>(1));
    }
    
    [Fact]
    public async Task Remove_InvalidId_ArgumentNullException()
    {
        using var dbContext = new ApplicationDbContext(_options);

        var repository = new EFCoreEntityRepository<RestaurantEntity>(dbContext);

        Assert.False(await repository.RemoveAsync(12));
    }
    
    [Fact]
    public async Task RemoveAsync_ValidId_EntityRemoved()
    {
        await using var dbContext = new ApplicationDbContext(_options);

        var repository = new EFCoreEntityRepository<RestaurantEntity>(dbContext);
        
        await repository.RemoveAsync(1);
        await dbContext.SaveChangesAsync();

        Assert.Null(dbContext.Find<RestaurantEntity>(1));
    }
    
    [Fact]
    public async Task RemoveAsync_InvalidId_ArgumentNullException()
    {
        await using var dbContext = new ApplicationDbContext(_options);

        var repository = new EFCoreEntityRepository<RestaurantEntity>(dbContext);
        
        Assert.False(await repository.RemoveAsync(12));
    }
    
}