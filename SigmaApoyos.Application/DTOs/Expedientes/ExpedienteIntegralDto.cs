using SigmaApoyos.Application.DTOs.Documentos;

namespace SigmaApoyos.Application.DTOs.Expedientes;

public class ExpedienteIntegralDto
{
    public ExpedienteDto Expediente { get; set; } = new();
    public IReadOnlyList<DocumentoDto> Documentos { get; set; } = new List<DocumentoDto>();
}
