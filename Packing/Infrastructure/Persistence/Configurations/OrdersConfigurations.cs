using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Packing.Domain.Order;
using Packing.Domain.Order.OrderItem;
using Packing.Domain.Order.OrderItem.ValueObjects;
using Packing.Domain.Order.ValueObjects;

namespace Packing.Infrastructure.Persistence.Configurations;

public class OrdersConfigurations : IEntityTypeConfiguration<Order>
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
        
        builder.OwnsOne(o => o.Packaged);
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

            oib.OwnsOne(oi => oi.Product);
        });
    }
}