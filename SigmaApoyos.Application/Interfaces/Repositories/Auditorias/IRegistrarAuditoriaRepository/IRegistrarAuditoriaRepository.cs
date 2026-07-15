using SigmaApoyos.Domain.Entities;

namespace SigmaApoyos.Application.Interfaces.Repositories.Auditorias;

public interface IRegistrarAuditoriaRepository
{
    Task RegistrarAsync(Auditoria auditoria, CancellationToken cancellationToken = default);
}
