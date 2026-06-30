using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SigmaApoyos.Domain.Entities;

namespace SigmaApoyos.Infrastructure.Persistence.Configurations;

public class ExpedienteConfiguration : IEntityTypeConfiguration<Expediente>
{
    public void Configure(EntityTypeBuilder<Expediente> builder)
    {
        builder.ToTable("EXPEDIENTES_TB");
        builder.HasKey(x => x.IdentificacionEstudiante);

        builder.Property(x => x.IdentificacionEstudiante).HasColumnName("IDENTIFICACION_ESTUDIANTE").HasMaxLength(20);
        builder.Property(x => x.Nombre).HasColumnName("NOMBRE").HasMaxLength(100).IsRequired();
        builder.Property(x => x.PrimerApellido).HasColumnName("PRIMER_APELLIDO").HasMaxLength(100).IsRequired();
        builder.Property(x => x.SegundoApellido).HasColumnName("SEGUNDO_APELLIDO").HasMaxLength(100).IsRequired();
        builder.Property(x => x.FechaNacimiento).HasColumnName("FECHA_NACIMIENTO").IsRequired();
        builder.Property(x => x.NombreEncargado).HasColumnName("NOMBRE_ENCARGADO").HasMaxLength(150).IsRequired();
        builder.Property(x => x.TelefonoEncargado).HasColumnName("TELEFONO_ENCARGADO").HasMaxLength(30).IsRequired();
        builder.Property(x => x.Observaciones).HasColumnName("OBSERVACIONES").HasMaxLength(1000).IsRequired();
        builder.Property(x => x.IdTipoAdecuacion).HasColumnName("ID_TIPO_ADECUACION").IsRequired();
        builder.Property(x => x.IdEstado).HasColumnName("ID_ESTADO").IsRequired();

        builder.HasOne(x => x.TipoAdecuacion)
            .WithMany(x => x.Expedientes)
            .HasForeignKey(x => x.IdTipoAdecuacion)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(x => x.Estado)
            .WithMany(x => x.Expedientes)
            .HasForeignKey(x => x.IdEstado)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
