using SigmaApoyos.Application.DTOs.Auditorias;
using SigmaApoyos.Application.DTOs.Comunes;
using SigmaApoyos.Application.Interfaces.Repositories.Auditorias;
using SigmaApoyos.Application.Interfaces.Services.Auditoria.IObtenerAuditoriasService;

namespace SigmaApoyos.Application.Services.Auditorias.ObtenerAuditorias;

public sealed class ObtenerAuditorias : IObtenerAuditoriasService
{
    private readonly IObtenerAuditoriasRepository _repository;

    public ObtenerAuditorias(IObtenerAuditoriasRepository repository)
    {
        _repository = repository;
    }

    public async Task<ResultadoPaginadoDto<AuditoriaDto>> ObtenerTodosAsync(
        FiltroAuditoriaDto filtro,
        CancellationToken cancellationToken = default)
    {
        return await _repository.ObtenerTodosAsync(filtro, cancellationToken);
    }
}
