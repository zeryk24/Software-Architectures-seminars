using Inventory.Domain.Goods;
using Inventory.Domain.Goods.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Inventory.Infrastructure.Persistence.Configurations;

public class GoodsConfiguration : IEntityTypeConfiguration<Goods>
{
    public void Configure(EntityTypeBuilder<Goods> builder)
    {
        ConfigureGoodsTable(builder);
    }

    private void ConfigureGoodsTable(EntityTypeBuilder<Goods> builder)
    {
        builder.ToTable(nameof(Goods));

        builder.HasKey(g => g.Id);
        
        builder.Property(g => g.Id)
            .HasColumnName(nameof(GoodsId))
            .ValueGeneratedNever()
            .HasConversion(
                goodsId => goodsId.Value,
                goods => GoodsId.Create(goods)
            );

        builder.OwnsOne(g => g.Name);
        builder.OwnsOne(g => g.Amount);
    }
}