using Microsoft.EntityFrameworkCore;
using SigmaApoyos.Application.DTOs.Auditorias;
using SigmaApoyos.Application.Interfaces.Repositories.Auditorias;
using SigmaApoyos.Infrastructure.Persistence;

namespace SigmaApoyos.Infrastructure.Repositories.Auditorias.ObtenerAuditoriasRepository;

public class ObtenerAuditoriasRepository : IObtenerAuditoriasRepository
{
    private readonly ApplicationDbContext _context;
    public ObtenerAuditoriasRepository(ApplicationDbContext context) => _context = context;

    public async Task<IReadOnlyList<AuditoriaDto>> ObtenerTodosAsync(CancellationToken cancellationToken = default)
    {
        return await _context.Auditorias.AsNoTracking()
            .OrderByDescending(x => x.FechaUtc)
            .Take(500)
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
            .ToListAsync(cancellationToken);
    }
}
