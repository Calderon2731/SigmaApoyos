using Microsoft.EntityFrameworkCore;
using SigmaApoyos.Application.Interfaces.Repositories.TiposDocumento;
using SigmaApoyos.Domain.Entities;
using SigmaApoyos.Infrastructure.Persistence;

namespace SigmaApoyos.Infrastructure.Repositories.TiposDocumento.ObtenerTipoDocumentoPorIdRepository;

public class ObtenerTipoDocumentoPorIdRepository : IObtenerTipoDocumentoPorIdRepository
{
    private readonly ApplicationDbContext _context;
    public ObtenerTipoDocumentoPorIdRepository(ApplicationDbContext context) => _context = context;
    public async Task<TipoDocumento?> ObtenerPorIdAsync(int idTipoDocumento, CancellationToken cancellationToken = default)
        => await _context.TiposDocumento.AsNoTracking().FirstOrDefaultAsync(x => x.IdTipoDocumento == idTipoDocumento, cancellationToken);
}
