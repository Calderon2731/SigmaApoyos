using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SigmaApoyos.Domain.Entities;

namespace SigmaApoyos.Infrastructure.Persistence.Configurations;

public class AuditoriaConfiguration : IEntityTypeConfiguration<Auditoria>
{
    public void Configure(EntityTypeBuilder<Auditoria> builder)
    {
        builder.ToTable("AUDITORIA_TB");
        builder.HasKey(x => x.IdAuditoria);

        builder.Property(x => x.IdAuditoria).HasColumnName("ID_AUDITORIA");
        builder.Property(x => x.UsuarioId).HasColumnName("USUARIO_ID").HasMaxLength(450);
        builder.Property(x => x.UsuarioNombre).HasColumnName("USUARIO_NOMBRE").HasMaxLength(256).IsRequired();
        builder.Property(x => x.Accion).HasColumnName("ACCION").HasMaxLength(50).IsRequired();
        builder.Property(x => x.Entidad).HasColumnName("ENTIDAD").HasMaxLength(100).IsRequired();
        builder.Property(x => x.RegistroId).HasColumnName("REGISTRO_ID").HasMaxLength(450).IsRequired();
        builder.Property(x => x.ValoresAnteriores).HasColumnName("VALORES_ANTERIORES");
        builder.Property(x => x.ValoresNuevos).HasColumnName("VALORES_NUEVOS");
        builder.Property(x => x.FechaUtc).HasColumnName("FECHA_UTC").HasColumnType("datetime2").IsRequired();
        builder.Property(x => x.DireccionIp).HasColumnName("DIRECCION_IP").HasMaxLength(45);
        builder.Property(x => x.Ruta).HasColumnName("RUTA").HasMaxLength(500);
        builder.Property(x => x.Descripcion).HasColumnName("DESCRIPCION").HasMaxLength(500).IsRequired();

        builder.HasIndex(x => x.FechaUtc);
        builder.HasIndex(x => x.UsuarioId);
        builder.HasIndex(x => new { x.Entidad, x.RegistroId });
    }
}
