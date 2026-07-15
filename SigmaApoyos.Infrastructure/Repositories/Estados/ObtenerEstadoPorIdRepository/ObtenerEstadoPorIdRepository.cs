using Microsoft.EntityFrameworkCore;
using SigmaApoyos.Application.Interfaces.Repositories.Estados;
using SigmaApoyos.Domain.Entities;
using SigmaApoyos.Infrastructure.Persistence;

namespace SigmaApoyos.Infrastructure.Repositories.Estados.ObtenerEstadoPorIdRepository;

public class ObtenerEstadoPorIdRepository : IObtenerEstadoPorIdRepository
{
    private readonly ApplicationDbContext _context;
    public ObtenerEstadoPorIdRepository(ApplicationDbContext context) => _context = context;
    public async Task<Estado?> ObtenerPorIdAsync(int idEstado, CancellationToken cancellationToken = default)
        => await _context.Estados.AsNoTracking().FirstOrDefaultAsync(x => x.IdEstado == idEstado, cancellationToken);
}
