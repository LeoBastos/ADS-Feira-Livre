using ads.feira.domain.Entity.Cupons;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ads.feira.Infra.Mappings.Cupons
{
    public class CuponMappings : IEntityTypeConfiguration<Cupon>
    {
        public void Configure(EntityTypeBuilder<Cupon> builder)
        {
            builder.ToTable("Cupons");
        }
    }
}

