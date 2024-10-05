using FluentAssertions;
using FoodDelivery.DAL.Database;
using FoodDelivery.DAL.EFCore.Database;
using FoodDelivery.DAL.EFCore.Entities;
using FoodDelivery.DAL.EFCore.QueryObjects.Base;
using FoodDelivery.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace FoodDelivery.DAL.EFCore.Tests.QueryObjects;

public class BaseQueryObjectTests
{
    private readonly DbContextOptions<ApplicationDbContext> _options;

    public BaseQueryObjectTests()
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
    public async void Filter_OneValidPredicate_Filtered()
    {
        await using var dbContext = new ApplicationDbContext(_options);

        var query = new EFCoreQueryObject<RestaurantEntity>(dbContext);
        var actual = await query
            .Filter(r => r.Name.StartsWith("The"))
            .ExecuteAsync();

        actual.Should()
            .HaveCount(7).And
            .OnlyContain(r => r.Name.StartsWith("The"));
    }
    
    [Fact]
    public async void Filter_OneValidPredicateOrdered_FilteredOrdered()
    {
        await using var dbContext = new ApplicationDbContext(_options);

        var query = new EFCoreQueryObject<RestaurantEntity>(dbContext);
        var actual = await query
            .Filter(r => r.Name.StartsWith("The"))
            .OrderBy(r => r.Name)
            .ExecuteAsync();

        actual.Should()
            .HaveCount(7).And
            .OnlyContain(r => r.Name.StartsWith("The")).And
            .BeInAscendingOrder(r => r.Name);
    }
    
    [Fact]
    public async void Filter_OneValidPredicatePaged_FilteredPaged()
    {
        await using var dbContext = new ApplicationDbContext(_options);

        var query = new EFCoreQueryObject<RestaurantEntity>(dbContext);
        var actual = await query
            .Filter(r => r.Name.StartsWith("The"))
            .Page(2,3)
            .ExecuteAsync();

        actual.Should()
            .HaveCount(3).And
            .Satisfy(r => r.Name == "The Mountain Courtyard",
                r => r.Name == "The Mountain Lantern",
                r => r.Name == "The Peacock");
    }
    
    [Fact]
    public async void Filter_OneValidPredicateOrderedDescending_FilteredOrderedDescending()
    {
        await using var dbContext = new ApplicationDbContext(_options);

        var query = new EFCoreQueryObject<RestaurantEntity>(dbContext);
        var actual = await query
            .Filter(r => r.Name.StartsWith("The"))
            .OrderBy(r => r.Name, false)
            .ExecuteAsync();

        actual.Should()
            .HaveCount(7).And
            .OnlyContain(r => r.Name.StartsWith("The")).And
            .BeInDescendingOrder(r => r.Name);
    }
    
    [Fact]
    public async void Filter_OneValidPredicateOrderedPaged_FilteredOrderedPaged()
    {
        await using var dbContext = new ApplicationDbContext(_options);

        var query = new EFCoreQueryObject<RestaurantEntity>(dbContext);
        var actual = await query
            .Filter(r => r.Name.StartsWith("The"))
            .OrderBy(r => r.Name)
            .Page(3, 2)
            .ExecuteAsync();

        actual.Should()
            .HaveCount(2).And
            .OnlyContain(r => r.Name.StartsWith("The")).And
            .BeInAscendingOrder(r => r.Name).And
            .OnlyContain(r => r.Name.StartsWith("The P"));
    }
    
    [Fact]
    public async void Filter_OneValidPredicateOrderedDescendingPaged_FilteredOrderedDescendingPaged()
    {
        await using var dbContext = new ApplicationDbContext(_options);

        var query = new EFCoreQueryObject<RestaurantEntity>(dbContext);
        var actual = await query
            .Filter(r => r.Name.StartsWith("The"))
            .OrderBy(r => r.Name, false)
            .Page(3, 2)
            .ExecuteAsync();

        actual.Should()
            .HaveCount(2).And
            .OnlyContain(r => r.Name.StartsWith("The")).And
            .BeInDescendingOrder(r => r.Name).And
            .OnlyContain(r => r.Name.StartsWith("The Mountain"));
    }
    
    [Fact]
    public async void Filter_InvalidPredicate_Empty()
    {
        await using var dbContext = new ApplicationDbContext(_options);

        var query = new EFCoreQueryObject<RestaurantEntity>(dbContext);
        var actual = await query
            .Filter(r => r.Name.StartsWith("Whatever"))
            .ExecuteAsync();

        actual.Should().BeEmpty();
    }
    
    [Fact]
    public async void Page_ValidPage_Paged()
    {
        await using var dbContext = new ApplicationDbContext(_options);

        var query = new EFCoreQueryObject<RestaurantEntity>(dbContext);
        var actual = await query
            .Page(4, 3)
            .ExecuteAsync();

        actual.Should()
            .ContainSingle().And
            .Satisfy(r => r.Name == "Mumbles");
    }
    
    [Fact]
    public async void Page_ValidPageOrdered_PagedOrdered()
    {
        await using var dbContext = new ApplicationDbContext(_options);

        var query = new EFCoreQueryObject<RestaurantEntity>(dbContext);
        var actual = await query
            .Page(3, 3)
            .OrderBy(r => r.Name)
            .ExecuteAsync();

        actual.Should()
            .HaveCount(3).And
            .Satisfy(r => r.Name == "The Peacock",
                r => r.Name == "Sapphire",
                r => r.Name == "The Nightingale").And
            .BeInAscendingOrder(r => r.Name);
    }
    
    [Fact]
    public async void Page_ValidPageFiltered_PagedFiltered()
    {
        await using var dbContext = new ApplicationDbContext(_options);

        var query = new EFCoreQueryObject<RestaurantEntity>(dbContext);
        var actual = await query
            .Page(3, 3)
            .Filter(r => r.Name.StartsWith("The"))
            .ExecuteAsync();

        actual.Should()
            .HaveCount(2).And
            .Satisfy(r => r.Name == "The Peacock",
                r => r.Name == "The Nightingale");
    }
    
    [Fact]
    public async void Page_ValidPageOrderedFiltered_PagedOrderedFiltered()
    {
        await using var dbContext = new ApplicationDbContext(_options);

        var query = new EFCoreQueryObject<RestaurantEntity>(dbContext);
        var actual = await query
            .Page(3, 3)
            .OrderBy(r => r.Name)
            .Filter(r => r.Name.StartsWith("The"))
            .ExecuteAsync();

        actual.Should()
            .HaveCount(2).And
            .Satisfy(r => r.Name == "The Peacock",
                r => r.Name == "The Nightingale").And
            .BeInAscendingOrder(r => r.Name);
    }
    
    [Fact]
    public async void Page_ValidPageOrderedDescendingFiltered_PagedOrderedDescendingFiltered()
    {
        await using var dbContext = new ApplicationDbContext(_options);

        var query = new EFCoreQueryObject<RestaurantEntity>(dbContext);
        var actual = await query
            .Page(3, 3)
            .OrderBy(r => r.Name, false)
            .Filter(r => r.Name.StartsWith("The"))
            .ExecuteAsync();

        actual.Should()
            .HaveCount(2).And
            .Satisfy(r => r.Name == "The Peacock",
                r => r.Name == "The Nightingale").And
            .BeInDescendingOrder(r => r.Name);
    }
    
    [Fact]
    public async void Page_InvalidPage_Empty()
    {
        await using var dbContext = new ApplicationDbContext(_options);

        var query = new EFCoreQueryObject<RestaurantEntity>(dbContext);
        var actual = await query
            .Page(10, 3)
            .ExecuteAsync();

        actual.Should().BeEmpty();
    }
    
    [Fact]
    public async void OrderBy_ValidOrderBy_Ordered()
    {
        await using var dbContext = new ApplicationDbContext(_options);

        var query = new EFCoreQueryObject<RestaurantEntity>(dbContext);
        var actual = await query
            .OrderBy(r => r.Name)
            .ExecuteAsync();

        actual.Should()
            .HaveCount(10).And
            .BeInAscendingOrder(r => r.Name);
    }
    
    [Fact]
    public async void OrderBy_ValidOrderByDescending_OrderedDescending()
    {
        await using var dbContext = new ApplicationDbContext(_options);

        var query = new EFCoreQueryObject<RestaurantEntity>(dbContext);
        var actual = await query
            .OrderBy(r => r.Name, false)
            .ExecuteAsync();

        actual.Should()
            .HaveCount(10).And
            .BeInDescendingOrder(r => r.Name);
    }
    
    [Fact]
    public async void OrderBy_ValidOrderByFiltered_OrderedFiltered()
    {
        await using var dbContext = new ApplicationDbContext(_options);

        var query = new EFCoreQueryObject<RestaurantEntity>(dbContext);
        var actual = await query
            .OrderBy(r => r.Name)
            .Filter(r => r.Name.StartsWith("The"))
            .ExecuteAsync();

        actual.Should()
            .HaveCount(7).And
            .BeInAscendingOrder(r => r.Name);
    }
    
    [Fact]
    public async void OrderBy_ValidOrderByPaged_OrderedPaged()
    {
        await using var dbContext = new ApplicationDbContext(_options);

        var query = new EFCoreQueryObject<RestaurantEntity>(dbContext);
        var actual = await query
            .OrderBy(r => r.Name)
            .Page(4,3)
            .ExecuteAsync();

        actual.Should()
            .ContainSingle().And
            .Satisfy(r => r.Name == "The Sailing Stranger");
    }
    
    [Fact]
    public async void OrderBy_ValidOrderByDescendingFiltered_OrderedDescendingFiltered()
    {
        await using var dbContext = new ApplicationDbContext(_options);

        var query = new EFCoreQueryObject<RestaurantEntity>(dbContext);
        var actual = await query
            .OrderBy(r => r.Name, false)
            .Filter(r => r.Name.StartsWith("The"))
            .ExecuteAsync();

        actual.Should()
            .HaveCount(7).And
            .BeInDescendingOrder(r => r.Name);
    }
    
    [Fact]
    public async void OrderBy_ValidOrderByFilteredPaged_OrderedFilteredPaged()
    {
        await using var dbContext = new ApplicationDbContext(_options);

        var query = new EFCoreQueryObject<RestaurantEntity>(dbContext);
        var actual = await query
            .OrderBy(r => r.Name)
            .Filter(r => r.Name.StartsWith("The"))
            .Page(2, 3)
            .ExecuteAsync();

        actual.Should()
            .HaveCount(3).And
            .BeInAscendingOrder(r => r.Name).And
            .Satisfy(r => r.Name == "The Nightingale",
                r => r.Name == "The Peacock",
                r => r.Name == "The Private Port");
    }
    
    [Fact]
    public async void OrderBy_ValidOrderByDescendingFilteredPaged_OrderedDescendingFilteredPaged()
    {
        await using var dbContext = new ApplicationDbContext(_options);

        var query = new EFCoreQueryObject<RestaurantEntity>(dbContext);
        var actual = await query
            .OrderBy(r => r.Name, false)
            .Filter(r => r.Name.StartsWith("The"))
            .Page(3, 3)
            .ExecuteAsync();

        actual.Should()
            .ContainSingle().And
            .Satisfy(r => r.Name == "The Juniper Angel");
    }
}