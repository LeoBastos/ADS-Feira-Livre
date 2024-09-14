using ads.feira.domain.Entity.Cupons;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ads.feira.Infra.Mappings.Cupons
{
    public class CuponMappings : IEntityTypeConfiguration<Cupon>
    {
        public void Configure(EntityTypeBuilder<Cupon> builder)
        {
            builder.HasKey(c => c.Id);
            builder.Property(c => c.Id)
                   .HasMaxLength(36)
                   .ValueGeneratedOnAdd();
            builder.Property(c => c.Name).IsRequired().HasMaxLength(100);
            builder.Property(c => c.Code).IsRequired().HasMaxLength(50);
            builder.Property(c => c.Description).IsRequired().HasMaxLength(500);
            builder.Property(c => c.Expiration).IsRequired();
            builder.Property(c => c.Discount).IsRequired().HasColumnType("decimal(18,2)");
            builder.Property(c => c.DiscountType).IsRequired();

            builder.HasMany(c => c.Products)
                .WithMany(p => p.AvailableCoupons)
                .UsingEntity(j => j.ToTable("ProductCupons"));

            builder.HasMany(c => c.Stores)
                .WithMany(s => s.AvailableCupons)
                .UsingEntity(j => j.ToTable("StoreCupons"));

            builder.HasMany(c => c.Stores)
                 .WithMany(s => s.AvailableCupons);

            builder.HasMany(c => c.Products)
                .WithMany(p => p.AvailableCoupons);           
        }
    }
}



