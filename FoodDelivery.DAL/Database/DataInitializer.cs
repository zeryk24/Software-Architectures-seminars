using FoodDelivery.DAL.EFCore.Entities;
using FoodDelivery.DAL.Entities;
using FoodDelivery.Shared.Constants;
using FoodDelivery.Shared.Enums;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace FoodDelivery.DAL.Database;

public static class DataInitializer
{
    private static AddressEntity _customerAddress = new()
        { Id = 1, City = "New York", Street = "My Drive", Number = "3818", PostalCode = "10016" };

    private static List<RestaurantEntity> _restaurants = new List<RestaurantEntity>
    {
        new () { Id = 1, Disabled = false, Name = "The Private Port"},
        new () { Id = 2, Disabled = false, Name = "The Sailing Stranger"},
        new () { Id = 3, Disabled = false, Name = "The Juniper Angel"},
        new () { Id = 4, Disabled = false, Name = "The Mountain Courtyard"},
        new () { Id = 5, Disabled = false, Name = "The Mountain Lantern"},
        new () { Id = 6, Disabled = false, Name = "Indigo"},
        new () { Id = 7, Disabled = false, Name = "The Peacock"},
        new () { Id = 8, Disabled = false, Name = "Sapphire"},
        new () { Id = 9, Disabled = false, Name = "The Nightingale"},
        new () { Id = 10,Disabled = false, Name = "Mumbles"}
    };

    private static List<MealEntity> _meals = new List<MealEntity>
    {
        new()
        {
            Id = 1,
            Name = "Broccoli and pancetta fusilli",
            Description = "Fresh egg pasta in a sauce made from fresh broccoli and smoked pancetta",
            MealType = "Pasta",
            Price = 10.5,
            RestaurantId = _restaurants[0].Id
        },
        new()
        {
            Id = 2,
            Name = "Steak and parmesan bagel",
            Description = "A warm bagel filled with steak and parmesan",
            MealType = "Steak",
            Price = 15.20,
            RestaurantId = _restaurants[1].Id
        },
        new()
        {
            Id = 3,
            Name = "Tuna and lemon penne",
            Description = "Fresh egg tubular pasta in a sauce made from tuna and tangy lemon",
            MealType = "Pasta",
            Price = 8.60,
            RestaurantId = _restaurants[2].Id
        },
        new()
        {
            Id = 4,
            Name = "Sausage and chilli burger",
            Description = "Succulent burger made from chunky sausage and spicy chilli, served in a roll",
            MealType = "Burger",
            Price = 16.50,
            RestaurantId = _restaurants[3].Id
        },
        new()
        {
            Id = 5,
            Name = "Grouse and lettuce bagel",
            Description = "A warm bagel filled with grouse and romaine lettuce",
            MealType = "Bagel",
            Price = 5.10,
            RestaurantId = _restaurants[4].Id
        },
        new()
        {
            Id = 6,
            Name = "Tofu and squash sausages",
            Description = "Sizzling sausages made from smoked tofu and pattypan squash, served in a roll",
            MealType = "Vegan",
            Price = 7.10,
            RestaurantId = _restaurants[5].Id
        },
        new()
        {
            Id = 7,
            Name = "Strawberry and pepper soup",
            Description = "Fresh strawberries and sweet pepper combined into smooth soup",
            MealType = "Soup",
            Price = 8.00,
            RestaurantId = _restaurants[6].Id
        },
        new()
        {
            Id = 8,
            Name = "Sole and durian salad",
            Description = "Sole and fresh durian served on a bed of lettuce",
            MealType = "Salad",
            Price = 10.50,
            RestaurantId = _restaurants[7].Id
        },
        new()
        {
            Id = 9,
            Name = "Pasta salad with garlic dressing",
            Description = "A mouth-watering pasta salad served with garlic dressing",
            MealType = "Pasta salad",
            Price = 11.30,
            RestaurantId = _restaurants[8].Id
        },
        new()
        {
            Id = 10,
            Name = "Pesto and spinach pasta",
            Description = "Fresh egg pasta in a sauce made from green pesto and baby spinach",
            MealType = "Pasta",
            Price = 8.00,
            RestaurantId = _restaurants[9].Id
        }
    };

    private static List<OrderEntity> _orders = new List<OrderEntity>
    {
        new () { Id = 1, UserId = 2, AddressId = 1, PaymentType = PaymentType.Card, RestaurantId = 1},
        new () { Id = 2, UserId = 2, AddressId = 1, PaymentType = PaymentType.Card, RestaurantId = 2},
        new () { Id = 3, UserId = 2, AddressId = 1, PaymentType = PaymentType.Card, RestaurantId = 3},
        new () { Id = 4, UserId = 2, AddressId = 1, PaymentType = PaymentType.Card, RestaurantId = 4},
        new () { Id = 5, UserId = 2, AddressId = 1, PaymentType = PaymentType.Card, RestaurantId = 5},
        new () { Id = 6, UserId = 2, AddressId = 1, PaymentType = PaymentType.Cash, RestaurantId = 6},
        new () { Id = 7, UserId = 2, AddressId = 1, PaymentType = PaymentType.Cash, RestaurantId = 7},
        new () { Id = 8, UserId = 2, AddressId = 1, PaymentType = PaymentType.Cash, RestaurantId = 8},
        new () { Id = 9, UserId = 2, AddressId = 1, PaymentType = PaymentType.Cash, RestaurantId = 9},
        new () { Id = 10, UserId = 2 , AddressId = 1, PaymentType = PaymentType.Coupon, RestaurantId = 10},
     };

    private static List<OrderItemEntity> _orderItems = new List<OrderItemEntity>
    {
        new() { Id = 1, MealId = _meals[0].Id, Amount = 2, UnitPrice = _meals[0].Price, OrderId = _orders[0].Id },
        new() { Id = 2, MealId = _meals[2].Id, Amount = 3, UnitPrice = _meals[2].Price, OrderId = _orders[0].Id },
        new() { Id = 3, MealId = _meals[1].Id, Amount = 1, UnitPrice = _meals[1].Price, OrderId = _orders[1].Id },
        new() { Id = 4, MealId = _meals[2].Id, Amount = 1, UnitPrice = _meals[2].Price, OrderId = _orders[1].Id },
        new() { Id = 5, MealId = _meals[3].Id, Amount = 2, UnitPrice = _meals[3].Price, OrderId = _orders[1].Id },
        new() { Id = 6, MealId = _meals[0].Id, Amount = 1, UnitPrice = _meals[0].Price, OrderId = _orders[2].Id },
        new() { Id = 7, MealId = _meals[9].Id, Amount = 1, UnitPrice = _meals[9].Price, OrderId = _orders[2].Id },
        new() { Id = 8, MealId = _meals[6].Id, Amount = 5, UnitPrice = _meals[6].Price, OrderId = _orders[3].Id },
        new() { Id = 9, MealId = _meals[4].Id, Amount = 2, UnitPrice = _meals[4].Price, OrderId = _orders[4].Id },
        new() { Id = 10, MealId = _meals[8].Id, Amount = 4, UnitPrice = _meals[8].Price, OrderId = _orders[5].Id },
        new() { Id = 11, MealId = _meals[6].Id, Amount = 1, UnitPrice = _meals[6].Price, OrderId = _orders[6].Id },
        new() { Id = 12, MealId = _meals[7].Id, Amount = 4, UnitPrice = _meals[7].Price, OrderId = _orders[7].Id },
        new() { Id = 13, MealId = _meals[8].Id, Amount = 3, UnitPrice = _meals[8].Price, OrderId = _orders[8].Id },
        new() { Id = 14, MealId = _meals[5].Id, Amount = 1, UnitPrice = _meals[5].Price, OrderId = _orders[8].Id },
        new() { Id = 15, MealId = _meals[0].Id, Amount = 1, UnitPrice = _meals[0].Price, OrderId = _orders[9].Id }
    };

    private static List<FeedbackEntity> _feedbacks = new List<FeedbackEntity>
    {
        new () { Id = 1, Rating = 1, UserId =  2, MealId = _meals[0].Id, Description = "Too bad... Didn't like it at all." },
        new () { Id = 2, Rating = 2, UserId =  2, MealId = _meals[1].Id, Description = "Eh..." },
        new () { Id = 3, Rating = 3, UserId =  2, MealId = _meals[2].Id, Description = "Not great, not terrible." },
        new () { Id = 4, Rating = 4, UserId =  2, MealId = _meals[3].Id, Description = "Liked it a lot!!" },
        new () { Id = 5, Rating = 5, UserId =  2, MealId = _meals[4].Id, Description = "Awesome! Best thing I ever eaten!" },
        new () { Id = 6, Rating = 5, UserId =  2, MealId = _meals[5].Id, Description = "Awesome! Best thing I ever eaten!" },
        new () { Id = 7, Rating = 4, UserId =  2, MealId = _meals[6].Id, Description = "Liked it a lot!!" },
        new () { Id = 8, Rating = 3, UserId =  2, MealId = _meals[7].Id, Description = "Not great, not terrible." },
        new () { Id = 9, Rating = 2, UserId =  2, MealId = _meals[8].Id, Description = "Eh..." },
        new () { Id = 10, Rating = 1, UserId = 2, MealId = _meals[9].Id, Description = "Too bad... Didn't like it at all." },
        new () { Id = 11, Rating = 1, UserId = 2, RestaurantId = _restaurants[0].Id, Description = "Really bad place." },
        new () { Id = 12, Rating = 2, UserId = 2, RestaurantId = _restaurants[1].Id, Description = "Won't come again..." },
        new () { Id = 13, Rating = 3, UserId = 2, RestaurantId = _restaurants[2].Id, Description = "Could have been better." },
        new () { Id = 14, Rating = 4, UserId = 2, RestaurantId = _restaurants[3].Id, Description = "Great place!" },
        new () { Id = 15, Rating = 5, UserId = 2, RestaurantId = _restaurants[4].Id, Description = "My favorite place! I highly recommend!" },
        new () { Id = 16, Rating = 5, UserId = 2, RestaurantId = _restaurants[5].Id, Description = "My favorite place! I highly recommend!" },
        new () { Id = 17, Rating = 4, UserId = 2, RestaurantId = _restaurants[6].Id, Description = "Great place!" },
        new () { Id = 18, Rating = 3, UserId = 2, RestaurantId = _restaurants[7].Id, Description = "Could have been better." },
        new () { Id = 19, Rating = 2, UserId = 2, RestaurantId = _restaurants[8].Id, Description = "Won't come again..." },
        new () { Id = 20, Rating = 1, UserId = 2, RestaurantId = _restaurants[9].Id, Description = "Really bad place." },
    };

    public static void InitializeRoles(this ModelBuilder modelBuilder)
    {   
        modelBuilder.Entity<IdentityRole<int>>().HasData(
            new IdentityRole<int>(Constants.Roles.Admin)
            {
                Id = 1,
                NormalizedName = "ADMIN"
            },
            new IdentityRole<int>(Constants.Roles.Customer)
            {
                Id = 2,
                NormalizedName = "CUSTOMER"
            });
    }
    
    public static void InitializeDefaultUsers(this ModelBuilder modelBuilder)
    {
        var admin = CreateUser(1, "Admin", "Admin", "admin@admin.com", "admin");
        var customer = CreateUser(2, "Emil", "Zelený", "emil@example.com", "p@Ssword1234");
        customer.AddressId = 1;

        modelBuilder.Entity<UserEntity>().HasData(admin, customer);

        modelBuilder.Entity<IdentityUserRole<int>>().HasData(
            new IdentityUserRole<int>
            {
                RoleId = 1,
                UserId = 1
            },
            new IdentityUserRole<int>
            {
                RoleId = 2,
                UserId = 2}
            );
    }

    private static UserEntity CreateUser(int id, string Name, string Surname, string mail, string password)
    {
        var user = new UserEntity
        {
            Id = id,
            Name = Name,
            Surname = Surname,
            Email = mail,
            NormalizedEmail = mail.ToUpper(),
            UserName = mail,
            NormalizedUserName = mail.ToUpper(),
            EmailConfirmed = true,
            SecurityStamp = Guid.NewGuid().ToString("N").ToUpper()
        };
        
        var passHash = new PasswordHasher<UserEntity>().HashPassword(user, password);
        user.PasswordHash = passHash;

        return user;
    }


    public static void Seed(this ModelBuilder modelBuilder)
    {
        modelBuilder.InitializeRoles();
        modelBuilder.InitializeDefaultUsers();

        modelBuilder.Entity<AddressEntity>().HasData(_customerAddress);
        modelBuilder.Entity<RestaurantEntity>().HasData(_restaurants);
        modelBuilder.Entity<MealEntity>().HasData(_meals);
        modelBuilder.Entity<UserEntity>();
        modelBuilder.Entity<OrderEntity>().HasData(_orders);
        modelBuilder.Entity<OrderItemEntity>().HasData(_orderItems);
        modelBuilder.Entity<FeedbackEntity>().HasData(_feedbacks);

    }
}