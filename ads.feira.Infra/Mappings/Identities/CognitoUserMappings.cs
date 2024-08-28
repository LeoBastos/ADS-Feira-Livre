using ads.feira.domain.Entity.Cupons;
using ads.feira.domain.Entity.Identity;
using ads.feira.domain.Entity.Stores;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ads.feira.Infra.Mappings.Identities
{
    public class CognitoUserMappings : IEntityTypeConfiguration<CognitoUser>
    {
        public void Configure(EntityTypeBuilder<CognitoUser> builder)
        {
            builder.HasKey(u => u.Id);
            builder.Property(u => u.Id).ValueGeneratedOnAdd();

            builder.Property(u => u.Email)
                .IsRequired()
                .HasMaxLength(255);

            builder.Property(u => u.Name)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(u => u.Description)
                .HasMaxLength(500);

            builder.Property(u => u.Assets)
                .HasMaxLength(255);

            builder.Property(u => u.TosAccept)
                .IsRequired();

            builder.Property(u => u.PrivacyAccept)
                .IsRequired();

            builder.Property(u => u.PrivacyAccept)
               .IsRequired();

            // Configure relationships
            builder.HasMany(u => u.Reviews)
                .WithOne(r => r.User)
                .HasForeignKey(r => r.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(u => u.RedeemedCoupons)
                .WithMany()
                .UsingEntity<Dictionary<string, object>>(
                    "UserRedeemedCoupons",
                    j => j.HasOne<Cupon>().WithMany().HasForeignKey("CuponId"),
                    j => j.HasOne<CognitoUser>().WithMany().HasForeignKey("UserId"),
                    j =>
                    {
                        j.HasKey("UserId", "CuponId");
                        j.ToTable("UserRedeemedCoupons");
                    });

            builder.HasMany(u => u.Stores)
                .WithMany(s => s.Users)
                .UsingEntity<Dictionary<string, object>>(
                    "UserStores",
                    j => j.HasOne<Store>().WithMany().HasForeignKey("StoreId"),
                    j => j.HasOne<CognitoUser>().WithMany().HasForeignKey("UserId"),
                    j =>
                    {
                        j.HasKey("UserId", "StoreId");
                        j.ToTable("UserStores");
                    });
        }

    }
}
