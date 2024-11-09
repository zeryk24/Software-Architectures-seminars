using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Packing.Domain.Box;
using Packing.Domain.Box.ValueObjects;

namespace Packing.Infrastructure.Persistence.Configurations;

public class BoxesConfigurations : IEntityTypeConfiguration<Box>
{
    public void Configure(EntityTypeBuilder<Box> builder)
    {
        ConfigureOrdersTable(builder); 
    }

    private void ConfigureOrdersTable(EntityTypeBuilder<Box> builder)
    {
        builder.ToTable(nameof(Box) + "es");

        builder.HasKey(b => b.Id);
        
        builder.Property(b => b.Id)
            .HasColumnName(nameof(BoxId))
            .ValueGeneratedNever()
            .HasConversion(
                boxId => boxId.Value,
                box => BoxId.Create(box)
            );

        builder.OwnsOne(b => b.Order);
    }
}