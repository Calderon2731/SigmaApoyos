using Microsoft.EntityFrameworkCore;
using SigmaApoyos.Application.DTOs.TiposAdecuacion;
using SigmaApoyos.Application.Interfaces.Repositories.TiposAdecuacion;
using SigmaApoyos.Infrastructure.Persistence;

namespace SigmaApoyos.Infrastructure.Repositories.TiposAdecuacion.ObtenerTiposAdecuacionRepository;

public class ObtenerTiposAdecuacionRepository : IObtenerTiposAdecuacionRepository
{
    private readonly ApplicationDbContext _context;
    public ObtenerTiposAdecuacionRepository(ApplicationDbContext context) => _context = context;
    public async Task<IReadOnlyList<TipoAdecuacionDto>> ObtenerTodosAsync(CancellationToken cancellationToken = default)
        => await _context.TiposAdecuacion.AsNoTracking().OrderBy(x => x.Nombre)
            .Select(x => new TipoAdecuacionDto { IdTipoAdecuacion = x.IdTipoAdecuacion, Nombre = x.Nombre }).ToListAsync(cancellationToken);
}
