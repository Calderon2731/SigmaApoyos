using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SigmaApoyos.Domain.Entities;

namespace SigmaApoyos.Infrastructure.Persistence.Configurations;

public class TipoDocumentoConfiguration : IEntityTypeConfiguration<TipoDocumento>
{
    public void Configure(EntityTypeBuilder<TipoDocumento> builder)
    {
        builder.ToTable("TIPO_DOCUMENTOS");
        builder.HasKey(x => x.IdTipoDocumento);

        builder.Property(x => x.IdTipoDocumento).HasColumnName("ID_TIPO_DOCUMENTO");
        builder.Property(x => x.Tipo).HasColumnName("TIPO").HasMaxLength(150).IsRequired();
    }
}
