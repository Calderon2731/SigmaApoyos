using SigmaApoyos.Application.Interfaces.Repositories.TiposAdecuacion;
using SigmaApoyos.Domain.Entities;
using SigmaApoyos.Infrastructure.Persistence;

namespace SigmaApoyos.Infrastructure.Repositories.TiposAdecuacion.CrearTipoAdecuacionRepository;

public class CrearTipoAdecuacionRepository : ICrearTipoAdecuacionRepository
{
    private readonly ApplicationDbContext _context;
    public CrearTipoAdecuacionRepository(ApplicationDbContext context) => _context = context;
    public async Task CrearAsync(TipoAdecuacion tipoAdecuacion, CancellationToken cancellationToken = default)
    {
        await _context.TiposAdecuacion.AddAsync(tipoAdecuacion, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
    }
}
