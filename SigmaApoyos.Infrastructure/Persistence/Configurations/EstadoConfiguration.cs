using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SigmaApoyos.Domain.Entities;

namespace SigmaApoyos.Infrastructure.Persistence.Configurations;

public class EstadoConfiguration : IEntityTypeConfiguration<Estado>
{
    public void Configure(EntityTypeBuilder<Estado> builder)
    {
        builder.ToTable("ESTADO_TB");
        builder.HasKey(x => x.IdEstado);

        builder.Property(x => x.IdEstado).HasColumnName("ID_ESTADO");
        builder.Property(x => x.Nombre).HasColumnName("ESTADO").HasMaxLength(100).IsRequired();
    }
}
