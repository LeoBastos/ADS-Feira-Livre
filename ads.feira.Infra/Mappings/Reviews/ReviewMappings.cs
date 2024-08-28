using ads.feira.domain.Entity.Reviews;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ads.feira.Infra.Mappings.Reviews
{
    public class ReviewMappings : IEntityTypeConfiguration<Review>
    {
        public void Configure(EntityTypeBuilder<Review> builder)
        {      
            builder.HasKey(r => r.Id);
            builder.Property(r => r.Id).ValueGeneratedOnAdd();
            builder.Property(r => r.UserId).IsRequired();
            builder.Property(r => r.ReviewContent).IsRequired().HasMaxLength(300);
            builder.Property(r => r.StoreId).IsRequired();
            builder.Property(r => r.Rate).IsRequired();

            builder.HasOne(r => r.Store)
                .WithMany(s => s.Reviews)
                .HasForeignKey(r => r.StoreId)
                .OnDelete(DeleteBehavior.Restrict);
            
            builder.HasOne(r => r.User)
                .WithMany(u => u.Reviews)
                .HasForeignKey(r => r.UserId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}