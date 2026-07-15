using SigmaApoyos.Application.DTOs.Auditorias;

namespace SigmaApoyos.Application.Interfaces.Services.Auditoria.IRegistrarAuditoriaService;

public interface IRegistrarAuditoriaService
{
    Task RegistrarAsync(RegistrarAuditoriaDto dto, CancellationToken cancellationToken = default);
}
