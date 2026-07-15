using Microsoft.EntityFrameworkCore;
using SigmaApoyos.Application.Interfaces.Repositories.TiposAdecuacion;
using SigmaApoyos.Domain.Entities;
using SigmaApoyos.Infrastructure.Persistence;

namespace SigmaApoyos.Infrastructure.Repositories.TiposAdecuacion.ObtenerTipoAdecuacionPorIdRepository;

public class ObtenerTipoAdecuacionPorIdRepository : IObtenerTipoAdecuacionPorIdRepository
{
    private readonly ApplicationDbContext _context;
    public ObtenerTipoAdecuacionPorIdRepository(ApplicationDbContext context) => _context = context;
    public async Task<TipoAdecuacion?> ObtenerPorIdAsync(int idTipoAdecuacion, CancellationToken cancellationToken = default)
        => await _context.TiposAdecuacion.AsNoTracking().FirstOrDefaultAsync(x => x.IdTipoAdecuacion == idTipoAdecuacion, cancellationToken);
}
