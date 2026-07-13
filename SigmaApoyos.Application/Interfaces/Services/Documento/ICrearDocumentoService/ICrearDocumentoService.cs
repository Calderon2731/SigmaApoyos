using SigmaApoyos.Application.DTOs.Documentos;

namespace SigmaApoyos.Application.Interfaces.Services.Documento.ICrearDocumentoService;

public interface ICrearDocumentoService
{
    Task<bool> CrearAsync(CrearDocumentoDto dto, CancellationToken cancellationToken = default);
}
