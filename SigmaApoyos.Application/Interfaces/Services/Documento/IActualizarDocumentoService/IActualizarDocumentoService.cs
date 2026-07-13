using SigmaApoyos.Application.DTOs.Documentos;

namespace SigmaApoyos.Application.Interfaces.Services.Documento.IActualizarDocumentoService;

public interface IActualizarDocumentoService
{
    Task<UpdateDocumentoDto?> ObtenerParaEditarAsync(int idDocumento, CancellationToken cancellationToken = default);

    Task<bool> ActualizarAsync(UpdateDocumentoDto dto, CancellationToken cancellationToken = default);
}
