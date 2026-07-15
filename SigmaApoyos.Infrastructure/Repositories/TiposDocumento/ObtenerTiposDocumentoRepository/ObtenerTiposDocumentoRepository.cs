using Microsoft.EntityFrameworkCore;
using SigmaApoyos.Application.DTOs.TiposDocumento;
using SigmaApoyos.Application.Interfaces.Repositories.TiposDocumento;
using SigmaApoyos.Infrastructure.Persistence;

namespace SigmaApoyos.Infrastructure.Repositories.TiposDocumento.ObtenerTiposDocumentoRepository;

public class ObtenerTiposDocumentoRepository : IObtenerTiposDocumentoRepository
{
    private readonly ApplicationDbContext _context;
    public ObtenerTiposDocumentoRepository(ApplicationDbContext context) => _context = context;
    public async Task<IReadOnlyList<TipoDocumentoDto>> ObtenerTodosAsync(CancellationToken cancellationToken = default)
        => await _context.TiposDocumento.AsNoTracking().OrderBy(x => x.Tipo)
            .Select(x => new TipoDocumentoDto { IdTipoDocumento = x.IdTipoDocumento, Tipo = x.Tipo }).ToListAsync(cancellationToken);
}
