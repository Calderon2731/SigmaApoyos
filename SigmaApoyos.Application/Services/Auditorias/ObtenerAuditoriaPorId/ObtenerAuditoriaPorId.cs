using SigmaApoyos.Application.DTOs.Auditorias;
using SigmaApoyos.Application.Interfaces.Repositories.Auditorias;
using SigmaApoyos.Application.Interfaces.Services.Auditoria.IObtenerAuditoriaPorIdService;

namespace SigmaApoyos.Application.Services.Auditorias.ObtenerAuditoriaPorId;

public sealed class ObtenerAuditoriaPorId : IObtenerAuditoriaPorIdService
{
    private readonly IObtenerAuditoriaPorIdRepository _repository;
    public ObtenerAuditoriaPorId(IObtenerAuditoriaPorIdRepository repository) => _repository = repository;
    public async Task<AuditoriaDto?> ObtenerPorIdAsync(long idAuditoria, CancellationToken cancellationToken = default)
        => await _repository.ObtenerPorIdAsync(idAuditoria, cancellationToken);
}
