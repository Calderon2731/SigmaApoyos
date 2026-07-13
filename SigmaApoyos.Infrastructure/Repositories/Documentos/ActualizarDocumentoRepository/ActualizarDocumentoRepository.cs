using Microsoft.EntityFrameworkCore;
using SigmaApoyos.Application.DTOs.Documentos;
using SigmaApoyos.Application.Interfaces.Repositories.Documentos;
using SigmaApoyos.Infrastructure.Persistence;

namespace SigmaApoyos.Infrastructure.Repositories.Documentos.ActualizarDocumentoRepository;

public class ActualizarDocumentoRepository : IActualizarDocumentoRepository
{
    private readonly ApplicationDbContext _context;

    public ActualizarDocumentoRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task ActualizarAsync(UpdateDocumentoDto dto, CancellationToken cancellationToken = default)
    {
        var documento = await _context.Documentos
            .FirstOrDefaultAsync(x => x.IdDocumento == dto.IdDocumento, cancellationToken);

        if (documento == null)
        {
            throw new InvalidOperationException("El documento no existe.");
        }

        documento.IdentificacionEstudiante = dto.IdentificacionEstudiante;
        documento.TipoDocumentoId = dto.TipoDocumentoId;
        documento.Consecutivo = dto.Consecutivo;
        documento.RutaArchivo = dto.RutaArchivo;
        documento.IdEstado = dto.IdEstado;

        _context.Documentos.Update(documento);
        await _context.SaveChangesAsync(cancellationToken);
    }
}
