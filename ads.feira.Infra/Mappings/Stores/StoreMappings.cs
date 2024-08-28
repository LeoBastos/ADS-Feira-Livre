using ads.feira.domain.Entity.Identity;
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
            builder.Property(s => s.Id).ValueGeneratedOnAdd();
            builder.Property(s => s.StoreOwner).IsRequired().HasMaxLength(100);
            builder.Property(s => s.Name).IsRequired().HasMaxLength(100);
            builder.Property(s => s.CategoryId).IsRequired();
            builder.Property(s => s.Description).IsRequired().HasMaxLength(500);
            builder.Property(s => s.Assets).HasMaxLength(250);
            builder.Property(s => s.StoreNumber).IsRequired().HasMaxLength(50);
            builder.Property(s => s.HasDebt).IsRequired();
            builder.Property(s => s.Locations).IsRequired().HasMaxLength(500);

            builder.HasOne(s => s.Category)
                .WithMany(c => c.Stores)
                .HasForeignKey(s => s.CategoryId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(s => s.Products)
                .WithOne(p => p.Store)
                .HasForeignKey(p => p.StoreId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(s => s.Reviews)
                .WithOne(r => r.Store)
                .HasForeignKey(r => r.StoreId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(s => s.Users)
           .WithMany(u => u.Stores)
           .UsingEntity<Dictionary<string, object>>(
               "UserStores",
               j => j.HasOne<CognitoUser>().WithMany().HasForeignKey("UserId"),
               j => j.HasOne<Store>().WithMany().HasForeignKey("StoreId"),
               j =>
               {
                   j.HasKey("StoreId", "UserId");
                   j.ToTable("UserStores");
               });
        }
    }
}
