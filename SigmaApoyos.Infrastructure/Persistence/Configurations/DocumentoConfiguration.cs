using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SigmaApoyos.Domain.Entities;

namespace SigmaApoyos.Infrastructure.Persistence.Configurations;

public class DocumentoConfiguration : IEntityTypeConfiguration<Documento>
{
    public void Configure(EntityTypeBuilder<Documento> builder)
    {
        builder.ToTable("DOCUMENTOS");
        builder.HasKey(x => x.IdDocumento);

        builder.Property(x => x.IdDocumento).HasColumnName("ID_DOCUMENTO");
        builder.Property(x => x.IdentificacionEstudiante).HasColumnName("IDENTIFICACION_ESTUDIANTE").HasMaxLength(20).IsRequired();
        builder.Property(x => x.TipoDocumentoId).HasColumnName("TIPO_DOCUMENTO").IsRequired();
        builder.Property(x => x.UsuarioId).HasColumnName("USUARIO_ID").HasMaxLength(450).IsRequired();
        builder.Property(x => x.Consecutivo).HasColumnName("CONSECUTIVO").HasMaxLength(50).IsRequired();
        builder.Property(x => x.RutaArchivo).HasColumnName("RUTA_ARCHIVO").HasMaxLength(500).IsRequired();
        builder.Property(x => x.FechaSubida).HasColumnName("FECHA_SUBIDA").IsRequired();
        builder.Property(x => x.IdEstado).HasColumnName("ID_ESTADO").IsRequired();

        builder.HasOne(x => x.Expediente)
            .WithMany(x => x.Documentos)
            .HasForeignKey(x => x.IdentificacionEstudiante)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(x => x.TipoDocumento)
            .WithMany(x => x.Documentos)
            .HasForeignKey(x => x.TipoDocumentoId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(x => x.Estado)
            .WithMany(x => x.Documentos)
            .HasForeignKey(x => x.IdEstado)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
