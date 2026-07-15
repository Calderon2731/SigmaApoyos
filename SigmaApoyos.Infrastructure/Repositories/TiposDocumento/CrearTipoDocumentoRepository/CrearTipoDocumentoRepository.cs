using SigmaApoyos.Application.Interfaces.Repositories.TiposDocumento;
using SigmaApoyos.Domain.Entities;
using SigmaApoyos.Infrastructure.Persistence;

namespace SigmaApoyos.Infrastructure.Repositories.TiposDocumento.CrearTipoDocumentoRepository;

public class CrearTipoDocumentoRepository : ICrearTipoDocumentoRepository
{
    private readonly ApplicationDbContext _context;
    public CrearTipoDocumentoRepository(ApplicationDbContext context) => _context = context;
    public async Task CrearAsync(TipoDocumento tipoDocumento, CancellationToken cancellationToken = default)
    {
        await _context.TiposDocumento.AddAsync(tipoDocumento, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
    }
}
