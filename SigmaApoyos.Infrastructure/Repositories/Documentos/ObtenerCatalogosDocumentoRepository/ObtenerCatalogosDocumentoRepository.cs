using Microsoft.EntityFrameworkCore;
using SigmaApoyos.Application.DTOs.Comunes;
using SigmaApoyos.Application.Interfaces.Repositories.Documentos;
using SigmaApoyos.Infrastructure.Persistence;

namespace SigmaApoyos.Infrastructure.Repositories.Documentos.ObtenerCatalogosDocumentoRepository;

public class ObtenerCatalogosDocumentoRepository : IObtenerCatalogosDocumentoRepository
{
    private readonly ApplicationDbContext _context;

    public ObtenerCatalogosDocumentoRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IReadOnlyList<OpcionDto>> ObtenerTiposDocumentoAsync(CancellationToken cancellationToken = default)
    {
        return await _context.TiposDocumento
            .AsNoTracking()
            .OrderBy(x => x.Tipo)
            .Select(x => new OpcionDto
            {
                Id = x.IdTipoDocumento,
                Nombre = x.Tipo
            })
            .ToListAsync(cancellationToken);
    }

    public async Task<IReadOnlyList<OpcionDto>> ObtenerEstadosAsync(CancellationToken cancellationToken = default)
    {
        return await _context.Estados
            .AsNoTracking()
            .OrderBy(x => x.Nombre)
            .Select(x => new OpcionDto
            {
                Id = x.IdEstado,
                Nombre = x.Nombre
            })
            .ToListAsync(cancellationToken);
    }
}
