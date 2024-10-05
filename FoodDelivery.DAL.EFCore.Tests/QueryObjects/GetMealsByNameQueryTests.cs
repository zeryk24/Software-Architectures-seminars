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

public class GetMealsByNameQueryTests
{
    private readonly DbContextOptions<ApplicationDbContext> _options;
    
    public GetMealsByNameQueryTests()
    {
        var serviceProvider = new ServiceCollection()
            .AddEntityFrameworkInMemoryDatabase()
            .BuildServiceProvider();

        _options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase($"test_db_{Guid.NewGuid()}")
            .UseInternalServiceProvider(serviceProvider)
            .Options;
        using var dbContext = new ApplicationDbContext(_options);
        dbContext.AddRange(new List<MealEntity>
        {
            new()
        {
            Id = 1,
            Name = "Broccoli and pancetta fusilli",
            Description = "Fresh egg pasta in a sauce made from fresh broccoli and smoked pancetta",
            MealType = "Pasta",
            Price = 10.5
        },
        new()
        {
            Id = 2,
            Name = "Steak and parmesan bagel",
            Description = "A warm bagel filled with steak and parmesan",
            MealType = "Steak",
            Price = 15.20,
        },
        new()
        {
            Id = 3,
            Name = "Tuna and lemon penne",
            Description = "Fresh egg tubular pasta in a sauce made from tuna and tangy lemon",
            MealType = "Pasta",
            Price = 8.60,
        },
        new()
        {
            Id = 4,
            Name = "Sausage and chilli burger",
            Description = "Succulent burger made from chunky sausage and spicy chilli, served in a roll",
            MealType = "Burger",
            Price = 16.50,
        },
        new()
        {
            Id = 5,
            Name = "Grouse and lettuce bagel",
            Description = "A warm bagel filled with grouse and romaine lettuce",
            MealType = "Bagel",
            Price = 5.10,
        },
        new()
        {
            Id = 6,
            Name = "Tofu and squash sausages",
            Description = "Sizzling sausages made from smoked tofu and pattypan squash, served in a roll",
            MealType = "Vegan",
            Price = 7.10,
        },
        new()
        {
            Id = 7,
            Name = "Strawberry and pepper soup",
            Description = "Fresh strawberries and sweet pepper combined into smooth soup",
            MealType = "Soup",
            Price = 8.00,
        },
        new()
        {
            Id = 8,
            Name = "Sole and durian salad",
            Description = "Sole and fresh durian served on a bed of lettuce",
            MealType = "Salad",
            Price = 10.50,
        },
        new()
        {
            Id = 9,
            Name = "Pasta salad with garlic dressing",
            Description = "A mouth-watering pasta salad served with garlic dressing",
            MealType = "Pasta salad",
            Price = 11.30,
        },
        new()
        {
            Id = 10,
            Name = "Pesto and spinach pasta",
            Description = "Fresh egg pasta in a sauce made from green pesto and baby spinach",
            MealType = "Pasta",
            Price = 8.00,
        }
        });
        dbContext.SaveChanges();
    }
    
    [Fact]
    public async void GetAllMealsByName_ValidString_ValidMeals()
    {
        await using var dbContext = new ApplicationDbContext(_options);

        var query = new GetMealsByNameQueryObject(dbContext);
        query.UseFilter("pasta");

        var actual = await query
            .ExecuteAsync();

        actual.Should()
            .HaveCount(2).And
            .Satisfy(m => m.Id == 9,
                m => m.Id == 10);
    }
    
    [Fact]
    public async void GetAllMealsByName_InvalidString_Empty()
    {
        await using var dbContext = new ApplicationDbContext(_options);

        var query = new GetMealsByNameQueryObject(dbContext);
        query.UseFilter("hi there");

        var actual = await query
            .ExecuteAsync();

        actual.Should()
            .BeEmpty();
    }
    
    [Fact]
    public async void GetAllMealsByName_ValidStringOrdered_ValidMealsOrdered()
    {
        await using var dbContext = new ApplicationDbContext(_options);

        var query = new GetMealsByNameQueryObject(dbContext);
        query.UseFilter("pasta");

        var actual = await query
            .OrderBy(m => m.Price)
            .ExecuteAsync();

        actual.Should()
            .HaveCount(2).And
            .Satisfy(m => m.Id == 9,
                m => m.Id == 10).And
            .BeInAscendingOrder(m => m.Price);
    }
    
    [Fact]
    public async void GetAllMealsByName_ValidStringPaged_ValidMealsPaged()
    {
        await using var dbContext = new ApplicationDbContext(_options);

        var query = new GetMealsByNameQueryObject(dbContext);
        query.UseFilter("pasta");

        var actual = await query
            .Page(2, 1)
            .ExecuteAsync();

        actual.Should()
            .ContainSingle().And
            .Satisfy(m => m.Id == 10);
    }
    
    [Fact]
    public async void GetAllMealsByName_ValidStringFiltered_ValidMealsFiltered()
    {
        await using var dbContext = new ApplicationDbContext(_options);

        var query = new GetMealsByNameQueryObject(dbContext);
        query.UseFilter("pasta");

        var actual = await query
            .Filter(m => m.Price < 10)
            .ExecuteAsync();

        actual.Should()
            .ContainSingle().And
            .Satisfy(m => m.Id == 10);
    }
}