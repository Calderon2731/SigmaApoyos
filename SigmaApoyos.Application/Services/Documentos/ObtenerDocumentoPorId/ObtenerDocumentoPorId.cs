using SigmaApoyos.Application.DTOs.Documentos;
using SigmaApoyos.Application.Interfaces.Repositories.Documentos;
using SigmaApoyos.Application.Interfaces.Services.Documento.IObtenerDocumentoPorIdService;

namespace SigmaApoyos.Application.Services.Documentos.ObtenerDocumentoPorId;

public sealed class ObtenerDocumentoPorId : IObtenerDocumentoPorIdService
{
    private readonly IObtenerDocumentoPorIdRepository _obtenerDocumentoPorIdRepository;

    public ObtenerDocumentoPorId(IObtenerDocumentoPorIdRepository obtenerDocumentoPorIdRepository)
    {
        _obtenerDocumentoPorIdRepository = obtenerDocumentoPorIdRepository;
    }

    public async Task<DocumentoDto?> ObtenerPorIdAsync(int idDocumento, CancellationToken cancellationToken = default)
    {
        return await _obtenerDocumentoPorIdRepository.ObtenerPorIdAsync(idDocumento, cancellationToken);
    }
}
