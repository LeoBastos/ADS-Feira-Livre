using ads.feira.domain.Entity.Cupons;
using ads.feira.domain.Entity.Accounts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ads.feira.Infra.Mappings.Accounts
{
    public class CognitoUserMappings : IEntityTypeConfiguration<CognitoUser>
    {
        public void Configure(EntityTypeBuilder<CognitoUser> builder)
        {
            builder.HasKey(u => u.Id);

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
                .WithOne()
                .HasForeignKey(s => s.StoreOwnerId);
        }
    }


}
