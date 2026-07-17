using SigmaApoyos.Application.DTOs.Expedientes;
using SigmaApoyos.Application.Interfaces.Repositories.Documentos;
using SigmaApoyos.Application.Interfaces.Repositories.Expedientes;
using SigmaApoyos.Application.Interfaces.Services.Expediente.IObtenerExpedienteIntegralService;

namespace SigmaApoyos.Application.Services.Expedientes.ObtenerExpedienteIntegral;

public sealed class ObtenerExpedienteIntegral : IObtenerExpedienteIntegralService
{
    private readonly IObtenerExpedientePorIdRepository _obtenerExpedienteRepository;
    private readonly IObtenerDocumentosRepository _obtenerDocumentosRepository;

    public ObtenerExpedienteIntegral(
        IObtenerExpedientePorIdRepository obtenerExpedienteRepository,
        IObtenerDocumentosRepository obtenerDocumentosRepository)
    {
        _obtenerExpedienteRepository = obtenerExpedienteRepository;
        _obtenerDocumentosRepository = obtenerDocumentosRepository;
    }

    public async Task<ExpedienteIntegralDto?> ObtenerAsync(
        string identificacionEstudiante,
        CancellationToken cancellationToken = default)
    {
        var expediente = await _obtenerExpedienteRepository
            .ObtenerPorIdAsync(identificacionEstudiante, cancellationToken);

        if (expediente == null)
        {
            return null;
        }

        var documentos = await _obtenerDocumentosRepository
            .ObtenerPorExpedienteAsync(identificacionEstudiante, cancellationToken);

        return new ExpedienteIntegralDto
        {
            Expediente = expediente,
            Documentos = documentos
        };
    }
}
