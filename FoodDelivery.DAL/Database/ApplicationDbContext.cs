using FoodDelivery.DAL.Database;
using FoodDelivery.DAL.EFCore.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace FoodDelivery.DAL.EFCore.Database;

public class ApplicationDbContext : IdentityDbContext<UserEntity,IdentityRole<int>,int>
{
    public ApplicationDbContext()
    {

    }

    public ApplicationDbContext(DbContextOptions options) : base(options) { }
    
    // //only for migrations
    // protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    // {
    //     if (!optionsBuilder.IsConfigured)
    //     {
    //         
    //         optionsBuilder.LogTo(Console.WriteLine);
    //     }
    // }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.Seed();
    }
}
