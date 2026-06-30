using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SigmaApoyos.Domain.Entities;

namespace SigmaApoyos.Infrastructure.Persistence.Configurations;

public class TipoAdecuacionConfiguration : IEntityTypeConfiguration<TipoAdecuacion>
{
    public void Configure(EntityTypeBuilder<TipoAdecuacion> builder)
    {
        builder.ToTable("TIPO_ADECUACION_TB");
        builder.HasKey(x => x.IdTipoAdecuacion);

        builder.Property(x => x.IdTipoAdecuacion).HasColumnName("ID_TIPO_ADECUACION");
        builder.Property(x => x.Nombre).HasColumnName("NOMBRE").HasMaxLength(150).IsRequired();
    }
}
