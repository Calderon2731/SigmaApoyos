using Microsoft.EntityFrameworkCore;
using SigmaApoyos.Application.DTOs.Estados;
using SigmaApoyos.Application.Interfaces.Repositories.Estados;
using SigmaApoyos.Infrastructure.Persistence;

namespace SigmaApoyos.Infrastructure.Repositories.Estados.ObtenerEstadosRepository;

public class ObtenerEstadosRepository : IObtenerEstadosRepository
{
    private readonly ApplicationDbContext _context;
    public ObtenerEstadosRepository(ApplicationDbContext context) => _context = context;
    public async Task<IReadOnlyList<EstadoDto>> ObtenerTodosAsync(CancellationToken cancellationToken = default)
        => await _context.Estados.AsNoTracking().OrderBy(x => x.Nombre)
            .Select(x => new EstadoDto { IdEstado = x.IdEstado, Nombre = x.Nombre }).ToListAsync(cancellationToken);
}
