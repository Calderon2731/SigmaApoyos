using Microsoft.EntityFrameworkCore;
using SigmaApoyos.Application.Interfaces.Repositories.TiposAdecuacion;
using SigmaApoyos.Infrastructure.Persistence;

namespace SigmaApoyos.Infrastructure.Repositories.TiposAdecuacion.EliminarTipoAdecuacionRepository;

public class EliminarTipoAdecuacionRepository : IEliminarTipoAdecuacionRepository
{
    private readonly ApplicationDbContext _context;
    public EliminarTipoAdecuacionRepository(ApplicationDbContext context) => _context = context;
    public async Task EliminarAsync(int idTipoAdecuacion, CancellationToken cancellationToken = default)
    {
        var tipo = await _context.TiposAdecuacion.FirstAsync(x => x.IdTipoAdecuacion == idTipoAdecuacion, cancellationToken);
        _context.TiposAdecuacion.Remove(tipo);
        await _context.SaveChangesAsync(cancellationToken);
    }
}
