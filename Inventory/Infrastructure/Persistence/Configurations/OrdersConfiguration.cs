using Inventory.Domain.Order;
using Inventory.Domain.Order.OrderItem;
using Inventory.Domain.Order.OrderItem.ValueObjects;
using Inventory.Domain.Order.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Inventory.Infrastructure.Persistence.Configurations;

public class OrdersConfiguration : IEntityTypeConfiguration<Order>
{
    public void Configure(EntityTypeBuilder<Order> builder)
    {
        ConfigureOrdersTable(builder);
        ConfigureOrderItemsTable(builder);
    }

    private void ConfigureOrdersTable(EntityTypeBuilder<Order> builder)
    {
        builder.ToTable(nameof(Order) + "s");
        
        builder.HasKey(o => o.Id);

        builder.Property(o => o.Id)
            .HasColumnName(nameof(OrderId))
            .ValueGeneratedNever()
            .HasConversion(
                orderId => orderId.Value,
                order => OrderId.Create(order)
            );

        builder.OwnsOne(o => o.Address);
    }

    private void ConfigureOrderItemsTable(EntityTypeBuilder<Order> builder)
    {
        builder.OwnsMany(o => o.OrderItems, oib =>
        {
            oib.ToTable(nameof(OrderItem) + "s");

            oib.HasKey(oi => oi.Id);

            oib.Property(oi => oi.Id)
                .HasColumnName(nameof(OrderId))
                .ValueGeneratedNever()
                .HasConversion(
                    orderItemId => orderItemId.Value,
                    orderItem => OrderItemId.Create(orderItem)
                );

            oib.WithOwner().HasForeignKey(nameof(OrderItemId));
            
            oib.OwnsOne(oi => oi.Goods);
            oib.OwnsOne(oi => oi.Amount);
            oib.OwnsOne(oi => oi.Price);
        });
    }
}