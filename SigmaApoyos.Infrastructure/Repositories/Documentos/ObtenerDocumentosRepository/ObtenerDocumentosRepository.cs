using Microsoft.EntityFrameworkCore;
using SigmaApoyos.Application.DTOs.Comunes;
using SigmaApoyos.Application.DTOs.Documentos;
using SigmaApoyos.Application.Interfaces.Repositories.Documentos;
using SigmaApoyos.Infrastructure.Persistence;

namespace SigmaApoyos.Infrastructure.Repositories.Documentos.ObtenerDocumentosRepository;

public class ObtenerDocumentosRepository : IObtenerDocumentosRepository
{
    private const int RegistrosPorPagina = 10;
    private readonly ApplicationDbContext _context;

    public ObtenerDocumentosRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<ResultadoPaginadoDto<DocumentoDto>> ObtenerTodosAsync(
        FiltroDocumentoDto filtro,
        CancellationToken cancellationToken = default)
    {
        var consulta = _context.Documentos
            .AsNoTracking()
            .AsQueryable();

        if (filtro.TipoDocumentoId.HasValue)
        {
            consulta = consulta.Where(documento =>
                documento.TipoDocumentoId == filtro.TipoDocumentoId.Value);
        }

        if (filtro.IdEstado.HasValue)
        {
            consulta = consulta.Where(documento => documento.IdEstado == filtro.IdEstado.Value);
        }

        if (filtro.Fecha.HasValue)
        {
            DateTime fechaInicio = filtro.Fecha.Value.Date;
            DateTime fechaFin = fechaInicio.AddDays(1);
            consulta = consulta.Where(documento =>
                documento.FechaSubida >= fechaInicio && documento.FechaSubida < fechaFin);
        }

        int totalRegistros = await consulta.CountAsync(cancellationToken);
        int totalPaginas = Math.Max(1, (int)Math.Ceiling(totalRegistros / (double)RegistrosPorPagina));
        int paginaActual = Math.Clamp(filtro.Pagina, 1, totalPaginas);

        var documentos = await consulta
            .OrderByDescending(documento => documento.FechaSubida)
            .Skip((paginaActual - 1) * RegistrosPorPagina)
            .Take(RegistrosPorPagina)
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

        return new ResultadoPaginadoDto<DocumentoDto>
        {
            Registros = documentos,
            PaginaActual = paginaActual,
            TotalPaginas = totalPaginas,
            TotalRegistros = totalRegistros
        };
    }

    public async Task<IReadOnlyList<DocumentoDto>> ObtenerPorExpedienteAsync(
        string identificacionEstudiante,
        CancellationToken cancellationToken = default)
    {
        return await _context.Documentos
            .AsNoTracking()
            .Where(documento => documento.IdentificacionEstudiante == identificacionEstudiante)
            .OrderByDescending(documento => documento.FechaSubida)
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
