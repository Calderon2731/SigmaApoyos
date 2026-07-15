using SigmaApoyos.Application.DTOs.Auditorias;
using SigmaApoyos.Application.Interfaces.Repositories.Auditorias;
using SigmaApoyos.Application.Interfaces.Services.Auditoria.IRegistrarAuditoriaService;

namespace SigmaApoyos.Application.Services.Auditorias.RegistrarAuditoria;

public sealed class RegistrarAuditoria : IRegistrarAuditoriaService
{
    private readonly IRegistrarAuditoriaRepository _repository;
    public RegistrarAuditoria(IRegistrarAuditoriaRepository repository) => _repository = repository;

    public async Task RegistrarAsync(RegistrarAuditoriaDto dto, CancellationToken cancellationToken = default)
    {
        await _repository.RegistrarAsync(new SigmaApoyos.Domain.Entities.Auditoria
        {
            UsuarioId = dto.UsuarioId,
            UsuarioNombre = string.IsNullOrWhiteSpace(dto.UsuarioNombre) ? "Sistema" : dto.UsuarioNombre,
            Accion = dto.Accion,
            Entidad = dto.Entidad,
            RegistroId = dto.RegistroId,
            FechaUtc = DateTime.UtcNow,
            DireccionIp = dto.DireccionIp,
            Ruta = dto.Ruta,
            Descripcion = dto.Descripcion
        }, cancellationToken);
    }
}
