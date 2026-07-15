using Microsoft.EntityFrameworkCore;
using SigmaApoyos.Application.DTOs.Auditorias;
using SigmaApoyos.Application.Interfaces.Repositories.Auditorias;
using SigmaApoyos.Infrastructure.Persistence;

namespace SigmaApoyos.Infrastructure.Repositories.Auditorias.ObtenerAuditoriaPorIdRepository;

public class ObtenerAuditoriaPorIdRepository : IObtenerAuditoriaPorIdRepository
{
    private readonly ApplicationDbContext _context;
    public ObtenerAuditoriaPorIdRepository(ApplicationDbContext context) => _context = context;

    public async Task<AuditoriaDto?> ObtenerPorIdAsync(long idAuditoria, CancellationToken cancellationToken = default)
    {
        return await _context.Auditorias.AsNoTracking()
            .Where(x => x.IdAuditoria == idAuditoria)
            .Select(x => new AuditoriaDto
            {
                IdAuditoria = x.IdAuditoria,
                UsuarioId = x.UsuarioId,
                UsuarioNombre = x.UsuarioNombre,
                Accion = x.Accion,
                Entidad = x.Entidad,
                RegistroId = x.RegistroId,
                ValoresAnteriores = x.ValoresAnteriores,
                ValoresNuevos = x.ValoresNuevos,
                FechaUtc = x.FechaUtc,
                DireccionIp = x.DireccionIp,
                Ruta = x.Ruta,
                Descripcion = x.Descripcion
            })
            .FirstOrDefaultAsync(cancellationToken);
    }
}
