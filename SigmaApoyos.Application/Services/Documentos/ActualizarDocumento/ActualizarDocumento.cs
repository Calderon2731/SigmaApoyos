using SigmaApoyos.Application.DTOs.Documentos;
using SigmaApoyos.Application.Interfaces.Repositories.Documentos;
using SigmaApoyos.Application.Interfaces.Repositories.Expedientes;
using SigmaApoyos.Application.Interfaces.Services.Documento.IActualizarDocumentoService;

namespace SigmaApoyos.Application.Services.Documentos.ActualizarDocumento;

public sealed class ActualizarDocumento : IActualizarDocumentoService
{
    private readonly IActualizarDocumentoRepository _actualizarDocumentoRepository;
    private readonly IObtenerDocumentoPorIdRepository _obtenerDocumentoPorIdRepository;
    private readonly IObtenerExpedientePorIdRepository _obtenerExpedientePorIdRepository;

    public ActualizarDocumento(
        IActualizarDocumentoRepository actualizarDocumentoRepository,
        IObtenerDocumentoPorIdRepository obtenerDocumentoPorIdRepository,
        IObtenerExpedientePorIdRepository obtenerExpedientePorIdRepository)
    {
        _actualizarDocumentoRepository = actualizarDocumentoRepository;
        _obtenerDocumentoPorIdRepository = obtenerDocumentoPorIdRepository;
        _obtenerExpedientePorIdRepository = obtenerExpedientePorIdRepository;
    }

    public async Task<UpdateDocumentoDto?> ObtenerParaEditarAsync(int idDocumento, CancellationToken cancellationToken = default)
    {
        var documento = await _obtenerDocumentoPorIdRepository.ObtenerPorIdAsync(idDocumento, cancellationToken);

        if (documento == null)
        {
            return null;
        }

        return new UpdateDocumentoDto
        {
            IdDocumento = documento.IdDocumento,
            IdentificacionEstudiante = documento.IdentificacionEstudiante,
            TipoDocumentoId = documento.TipoDocumentoId,
            Consecutivo = documento.Consecutivo,
            IdEstado = documento.IdEstado,
            RutaArchivo = documento.RutaArchivo
        };
    }

    public async Task<bool> ActualizarAsync(UpdateDocumentoDto dto, CancellationToken cancellationToken = default)
    {
        var documento = await _obtenerDocumentoPorIdRepository.ObtenerPorIdAsync(dto.IdDocumento, cancellationToken);

        if (documento == null)
        {
            return false;
        }

        var expediente = await _obtenerExpedientePorIdRepository.ObtenerPorIdAsync(dto.IdentificacionEstudiante, cancellationToken);

        if (expediente == null)
        {
            return false;
        }

        await _actualizarDocumentoRepository.ActualizarAsync(dto, cancellationToken);
        return true;
    }
}
