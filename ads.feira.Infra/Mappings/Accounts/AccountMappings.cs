using ads.feira.domain.Entity.Accounts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ads.feira.Infra.Mappings.Accounts
{
    public class AccountMappings : IEntityTypeConfiguration<Account>
    {
        public void Configure(EntityTypeBuilder<Account> builder)
        {
            builder.Property(u => u.UserType)
                 .HasConversion<string>();

            builder.Property(u => u.Name)
                .HasMaxLength(250);

            builder.Property(u => u.Assets)
                .HasMaxLength(250);          

            builder.HasMany(u => u.Stores)
                .WithOne(s => s.StoreOwner)
                .HasForeignKey(s => s.StoreOwnerId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(u => u.FavoriteStores)
                .WithMany()
                .UsingEntity(j => j.ToTable("UserFavoriteStores"));

            builder.HasMany(u => u.Reviews)
                .WithOne()
                .HasForeignKey("UserId")
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(u => u.RedeemedCoupons)
                .WithMany()
                .UsingEntity(j => j.ToTable("UserRedeemedCoupons"));
        }
    }


}
