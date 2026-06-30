using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SigmaApoyos.Domain.Entities;
using SigmaApoyos.Infrastructure.Identity;

namespace SigmaApoyos.Infrastructure.Persistence;

public class ApplicationDbContext(DbContextOptions options)
    : IdentityDbContext<ApplicationUser>(options)
{
    public DbSet<Estado> Estados => Set<Estado>();

    public DbSet<TipoAdecuacion> TiposAdecuacion => Set<TipoAdecuacion>();

    public DbSet<TipoDocumento> TiposDocumento => Set<TipoDocumento>();

    public DbSet<Expediente> Expedientes => Set<Expediente>();

    public DbSet<Documento> Documentos => Set<Documento>();

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        builder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
    }
}
