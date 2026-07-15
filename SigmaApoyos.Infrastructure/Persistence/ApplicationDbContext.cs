using System.Security.Claims;
using System.Text.Json;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using SigmaApoyos.Domain.Entities;
using SigmaApoyos.Infrastructure.Identity;

namespace SigmaApoyos.Infrastructure.Persistence;

public class ApplicationDbContext(DbContextOptions options, IHttpContextAccessor httpContextAccessor)
    : IdentityDbContext<ApplicationUser>(options)
{
    private static readonly HashSet<string> PropiedadesSensibles = new(StringComparer.OrdinalIgnoreCase)
    {
        "PasswordHash",
        "SecurityStamp",
        "ConcurrencyStamp",
        "AuthenticatorKey"
    };

    public DbSet<Estado> Estados => Set<Estado>();
    public DbSet<TipoAdecuacion> TiposAdecuacion => Set<TipoAdecuacion>();
    public DbSet<TipoDocumento> TiposDocumento => Set<TipoDocumento>();
    public DbSet<Expediente> Expedientes => Set<Expediente>();
    public DbSet<Documento> Documentos => Set<Documento>();
    public DbSet<Auditoria> Auditorias => Set<Auditoria>();

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        builder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
    }

    public override int SaveChanges()
    {
        var pendientes = PrepararAuditorias();
        if (pendientes.Count == 0) return base.SaveChanges();

        using var transaction = Database.CurrentTransaction == null ? Database.BeginTransaction() : null;
        var resultado = base.SaveChanges();
        Auditorias.AddRange(pendientes.Select(CrearAuditoria));
        base.SaveChanges();
        transaction?.Commit();
        return resultado;
    }

    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        var pendientes = PrepararAuditorias();
        if (pendientes.Count == 0) return await base.SaveChangesAsync(cancellationToken);

        await using var transaction = Database.CurrentTransaction == null
            ? await Database.BeginTransactionAsync(cancellationToken)
            : null;

        var resultado = await base.SaveChangesAsync(cancellationToken);
        await Auditorias.AddRangeAsync(pendientes.Select(CrearAuditoria), cancellationToken);
        await base.SaveChangesAsync(cancellationToken);
        if (transaction != null) await transaction.CommitAsync(cancellationToken);
        return resultado;
    }

    private List<AuditoriaPendiente> PrepararAuditorias()
    {
        ChangeTracker.DetectChanges();
        var httpContext = httpContextAccessor.HttpContext;
        var usuarioId = httpContext?.User.FindFirstValue(ClaimTypes.NameIdentifier);
        var usuarioNombre = httpContext?.User.Identity?.Name ?? "Sistema";
        var direccionIp = httpContext?.Connection.RemoteIpAddress?.ToString();
        var ruta = httpContext?.Request.Path.Value;
        var pendientes = new List<AuditoriaPendiente>();

        foreach (var entry in ChangeTracker.Entries().Where(x => EsEntidadAuditable(x.Entity)))
        {
            if (entry.State is not (EntityState.Added or EntityState.Modified or EntityState.Deleted)) continue;

            var accion = entry.State switch
            {
                EntityState.Added => "Crear",
                EntityState.Modified => "Editar",
                EntityState.Deleted => "Eliminar",
                _ => string.Empty
            };

            var propiedades = entry.Properties
                .Where(x => !PropiedadesSensibles.Contains(x.Metadata.Name))
                .Where(x => entry.State != EntityState.Modified || x.IsModified)
                .ToList();

            if (entry.State == EntityState.Modified && propiedades.Count == 0) continue;

            var anteriores = entry.State == EntityState.Added
                ? null
                : Serializar(propiedades.ToDictionary(x => x.Metadata.Name, x => x.OriginalValue));

            var nuevos = entry.State == EntityState.Deleted
                ? null
                : Serializar(propiedades.ToDictionary(x => x.Metadata.Name, x => x.CurrentValue));

            var entidad = ObtenerNombreEntidad(entry.Entity);
            pendientes.Add(new AuditoriaPendiente(
                entry,
                accion,
                entidad,
                ObtenerRegistroId(entry, entry.State == EntityState.Deleted),
                anteriores,
                nuevos,
                usuarioId,
                usuarioNombre,
                direccionIp,
                ruta,
                entry.State == EntityState.Added));
        }

        return pendientes;
    }

    private static Auditoria CrearAuditoria(AuditoriaPendiente pendiente)
    {
        var registroId = pendiente.ActualizarRegistroId
            ? ObtenerRegistroId(pendiente.Entry, false)
            : pendiente.RegistroId;

        return new Auditoria
        {
            UsuarioId = pendiente.UsuarioId,
            UsuarioNombre = pendiente.UsuarioNombre,
            Accion = pendiente.Accion,
            Entidad = pendiente.Entidad,
            RegistroId = registroId,
            ValoresAnteriores = pendiente.ValoresAnteriores,
            ValoresNuevos = pendiente.ActualizarRegistroId
                ? Serializar(pendiente.Entry.Properties
                    .Where(x => !PropiedadesSensibles.Contains(x.Metadata.Name))
                    .ToDictionary(x => x.Metadata.Name, x => x.CurrentValue))
                : pendiente.ValoresNuevos,
            FechaUtc = DateTime.UtcNow,
            DireccionIp = pendiente.DireccionIp,
            Ruta = pendiente.Ruta,
            Descripcion = $"{pendiente.Accion} de {pendiente.Entidad}"
        };
    }

    private static bool EsEntidadAuditable(object entity)
    {
        return entity is Expediente
            or Documento
            or Estado
            or TipoAdecuacion
            or TipoDocumento
            or ApplicationUser
            or IdentityUserRole<string>;
    }

    private static string ObtenerNombreEntidad(object entity)
    {
        return entity switch
        {
            Expediente => "Expediente",
            Documento => "Documento",
            Estado => "Estado",
            TipoAdecuacion => "Tipo de adecuación",
            TipoDocumento => "Tipo de documento",
            ApplicationUser => "Usuario",
            IdentityUserRole<string> => "Rol de usuario",
            _ => entity.GetType().Name
        };
    }

    private static string ObtenerRegistroId(EntityEntry entry, bool usarValorOriginal)
    {
        var llave = entry.Metadata.FindPrimaryKey();
        if (llave == null) return string.Empty;

        return string.Join(" | ", llave.Properties.Select(propiedad =>
        {
            var propertyEntry = entry.Property(propiedad.Name);
            return (usarValorOriginal ? propertyEntry.OriginalValue : propertyEntry.CurrentValue)?.ToString() ?? string.Empty;
        }));
    }

    private static string? Serializar(Dictionary<string, object?> valores)
        => valores.Count == 0 ? null : JsonSerializer.Serialize(valores);

    private sealed record AuditoriaPendiente(
        EntityEntry Entry,
        string Accion,
        string Entidad,
        string RegistroId,
        string? ValoresAnteriores,
        string? ValoresNuevos,
        string? UsuarioId,
        string UsuarioNombre,
        string? DireccionIp,
        string? Ruta,
        bool ActualizarRegistroId);
}
