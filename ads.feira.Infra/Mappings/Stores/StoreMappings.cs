using ads.feira.domain.Entity.Accounts;
using ads.feira.domain.Entity.Stores;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ads.feira.Infra.Mappings.Stores
{
    public class StoreMappings : IEntityTypeConfiguration<Store>
    {
        public void Configure(EntityTypeBuilder<Store> builder)
        {
            builder.HasKey(s => s.Id);
            builder.Property(s => s.StoreOwnerId).IsRequired();
            builder.Property(s => s.Name).IsRequired().HasMaxLength(100);
            builder.Property(s => s.Description).IsRequired();
            builder.Property(s => s.Assets).HasMaxLength(250);
            builder.Property(s => s.StoreNumber).IsRequired();
            builder.Property(s => s.HasDebt).IsRequired();
            builder.Property(s => s.Locations).IsRequired();           

            builder.HasOne(s => s.Category)
                .WithMany(c => c.Stores)
                .HasForeignKey(s => s.CategoryId);

            builder.HasMany(s => s.Products)
                .WithOne(p => p.Store)
                .HasForeignKey(p => p.StoreId);

            builder.HasMany(s => s.Reviews)
                .WithOne(r => r.Store)
                .HasForeignKey(r => r.StoreId);

            builder.HasMany(s => s.AvailableCupons)
                .WithMany(c => c.Stores);
        }
    }
}
