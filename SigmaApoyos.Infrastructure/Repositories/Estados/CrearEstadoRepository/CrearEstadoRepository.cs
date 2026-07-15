using SigmaApoyos.Application.Interfaces.Repositories.Estados;
using SigmaApoyos.Domain.Entities;
using SigmaApoyos.Infrastructure.Persistence;

namespace SigmaApoyos.Infrastructure.Repositories.Estados.CrearEstadoRepository;

public class CrearEstadoRepository : ICrearEstadoRepository
{
    private readonly ApplicationDbContext _context;
    public CrearEstadoRepository(ApplicationDbContext context) => _context = context;
    public async Task CrearAsync(Estado estado, CancellationToken cancellationToken = default)
    {
        await _context.Estados.AddAsync(estado, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
    }
}
