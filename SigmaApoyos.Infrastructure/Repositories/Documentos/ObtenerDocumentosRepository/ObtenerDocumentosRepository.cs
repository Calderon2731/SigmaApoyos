using Microsoft.EntityFrameworkCore;
using SigmaApoyos.Application.DTOs.Documentos;
using SigmaApoyos.Application.Interfaces.Repositories.Documentos;
using SigmaApoyos.Infrastructure.Persistence;

namespace SigmaApoyos.Infrastructure.Repositories.Documentos.ObtenerDocumentosRepository;

public class ObtenerDocumentosRepository : IObtenerDocumentosRepository
{
    private readonly ApplicationDbContext _context;

    public ObtenerDocumentosRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IReadOnlyList<DocumentoDto>> ObtenerTodosAsync(CancellationToken cancellationToken = default)
    {
        return await _context.Documentos
            .AsNoTracking()
            .Include(x => x.TipoDocumento)
            .Include(x => x.Estado)
            .Select(documento => new DocumentoDto
            {
                IdDocumento = documento.IdDocumento,
                IdentificacionEstudiante = documento.IdentificacionEstudiante,
                TipoDocumentoId = documento.TipoDocumentoId,
                TipoDocumento = documento.TipoDocumento != null ? documento.TipoDocumento.Tipo : string.Empty,
                UsuarioId = documento.UsuarioId,
                Consecutivo = documento.Consecutivo,
                RutaArchivo = documento.RutaArchivo,
                FechaSubida = documento.FechaSubida,
                IdEstado = documento.IdEstado,
                Estado = documento.Estado != null ? documento.Estado.Nombre : string.Empty
            })
            .ToListAsync(cancellationToken);
    }
}
