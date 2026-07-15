using Microsoft.EntityFrameworkCore;
using SigmaApoyos.Application.Interfaces.Repositories.Estados;
using SigmaApoyos.Infrastructure.Persistence;

namespace SigmaApoyos.Infrastructure.Repositories.Estados.EliminarEstadoRepository;

public class EliminarEstadoRepository : IEliminarEstadoRepository
{
    private readonly ApplicationDbContext _context;
    public EliminarEstadoRepository(ApplicationDbContext context) => _context = context;
    public async Task EliminarAsync(int idEstado, CancellationToken cancellationToken = default)
    {
        var estado = await _context.Estados.FirstAsync(x => x.IdEstado == idEstado, cancellationToken);
        _context.Estados.Remove(estado);
        await _context.SaveChangesAsync(cancellationToken);
    }
}
