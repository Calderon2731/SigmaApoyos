using Microsoft.EntityFrameworkCore;
using SigmaApoyos.Application.DTOs.Documentos;
using SigmaApoyos.Application.Interfaces.Repositories.Documentos;
using SigmaApoyos.Infrastructure.Persistence;

namespace SigmaApoyos.Infrastructure.Repositories.Documentos.ObtenerDocumentoPorIdRepository;

public class ObtenerDocumentoPorIdRepository : IObtenerDocumentoPorIdRepository
{
    private readonly ApplicationDbContext _context;

    public ObtenerDocumentoPorIdRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<DocumentoDto?> ObtenerPorIdAsync(int idDocumento, CancellationToken cancellationToken = default)
    {
        return await _context.Documentos
            .AsNoTracking()
            .Include(x => x.TipoDocumento)
            .Include(x => x.Estado)
            .Where(x => x.IdDocumento == idDocumento)
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
            .FirstOrDefaultAsync(cancellationToken);
    }
}
