using FluentAssertions;
using FoodDelivery.DAL.Database;
using FoodDelivery.DAL.EFCore.Database;
using FoodDelivery.DAL.EFCore.Entities;
using FoodDelivery.DAL.EFCore.QueryObjects;
using FoodDelivery.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace FoodDelivery.DAL.EFCore.Tests.QueryObjects;

public class GetAllRestaurantMealsQueryTests
{
    private readonly DbContextOptions<ApplicationDbContext> _options;
    
    public GetAllRestaurantMealsQueryTests()
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
            Price = 10.5,
            RestaurantId = 1
        },
        new()
        {
            Id = 2,
            Name = "Steak and parmesan bagel",
            Description = "A warm bagel filled with steak and parmesan",
            MealType = "Steak",
            Price = 15.20,
            RestaurantId = 1
        },
        new()
        {
            Id = 3,
            Name = "Tuna and lemon penne",
            Description = "Fresh egg tubular pasta in a sauce made from tuna and tangy lemon",
            MealType = "Pasta",
            Price = 8.60,
            RestaurantId = 1
        },
        new()
        {
            Id = 4,
            Name = "Sausage and chilli burger",
            Description = "Succulent burger made from chunky sausage and spicy chilli, served in a roll",
            MealType = "Burger",
            Price = 16.50,
            RestaurantId = 2
        },
        new()
        {
            Id = 5,
            Name = "Grouse and lettuce bagel",
            Description = "A warm bagel filled with grouse and romaine lettuce",
            MealType = "Bagel",
            Price = 5.10,
            RestaurantId = 2
        },
        new()
        {
            Id = 6,
            Name = "Tofu and squash sausages",
            Description = "Sizzling sausages made from smoked tofu and pattypan squash, served in a roll",
            MealType = "Vegan",
            Price = 7.10,
            RestaurantId = 2
        },
        new()
        {
            Id = 7,
            Name = "Strawberry and pepper soup",
            Description = "Fresh strawberries and sweet pepper combined into smooth soup",
            MealType = "Soup",
            Price = 8.00,
            RestaurantId = 2
        },
        new()
        {
            Id = 8,
            Name = "Sole and durian salad",
            Description = "Sole and fresh durian served on a bed of lettuce",
            MealType = "Salad",
            Price = 10.50,
            RestaurantId = 3
        },
        new()
        {
            Id = 9,
            Name = "Pasta salad with garlic dressing",
            Description = "A mouth-watering pasta salad served with garlic dressing",
            MealType = "Pasta salad",
            Price = 11.30,
            RestaurantId = 4
        },
        new()
        {
            Id = 10,
            Name = "Pesto and spinach pasta",
            Description = "Fresh egg pasta in a sauce made from green pesto and baby spinach",
            MealType = "Pasta",
            Price = 8.00,
            RestaurantId = 4
        }
        });
        dbContext.SaveChanges();
    }
    
    [Fact]
    public async void GetAllRestaurantMeals_ValidRestaurantId_ValidMeals()
    {
        await using var dbContext = new ApplicationDbContext(_options);

        var query = new GetAllRestaurantMealsQueryObject(dbContext);
        query.UseFilter(2);

        var actual = await query
            .ExecuteAsync();

        actual.Should()
            .HaveCount(4).And
            .Satisfy(m => m.Id == 4,
                m => m.Id == 5,
                m => m.Id == 6,
                m => m.Id == 7);
    }
    
    [Fact]
    public async void GetAllRestaurantMeals_InvalidRestaurantId_Empty()
    {
        await using var dbContext = new ApplicationDbContext(_options);

        var query = new GetAllRestaurantMealsQueryObject(dbContext);
        query.UseFilter(10);

        var actual = await query
            .ExecuteAsync();

        actual.Should()
            .BeEmpty();
    }
    
    [Fact]
    public async void GetAllRestaurantMeals_ValidRestaurantIdFiltered_ValidMealsFiltered()
    {
        await using var dbContext = new ApplicationDbContext(_options);

        var query = new GetAllRestaurantMealsQueryObject(dbContext);
        query.UseFilter(2);

        var actual = await query
            .Filter(m => m.Price > 7)
            .ExecuteAsync();

        actual.Should()
            .HaveCount(3).And
            .OnlyContain(m => m.Price > 7).And
            .Satisfy(m => m.Id == 4,
                m => m.Id == 6,
                m => m.Id == 7);
    }
    
    [Fact]
    public async void GetAllRestaurantMeals_ValidRestaurantIdPaged_ValidMealsPaged()
    {
        await using var dbContext = new ApplicationDbContext(_options);

        var query = new GetAllRestaurantMealsQueryObject(dbContext);
        query.UseFilter(2);

        var actual = await query
            .Page(1, 2)
            .ExecuteAsync();

        actual.Should()
            .HaveCount(2).And
            .Satisfy(m => m.Id == 4,
                m => m.Id == 5);
    }
    
    [Fact]
    public async void GetAllRestaurantMeals_ValidRestaurantIdOrdered_ValidMealsOrdered()
    {
        await using var dbContext = new ApplicationDbContext(_options);

        var query = new GetAllRestaurantMealsQueryObject(dbContext);
        query.UseFilter(2);

        var actual = await query
            .OrderBy(m => m.Price)
            .ExecuteAsync();

        actual.Should()
            .HaveCount(4).And
            .BeInAscendingOrder(m => m.Price).And
            .Satisfy(m => m.Id == 4,
                m => m.Id == 5,
                m => m.Id == 6,
                m => m.Id == 7);
    }
}