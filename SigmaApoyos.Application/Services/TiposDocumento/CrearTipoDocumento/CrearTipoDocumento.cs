using SigmaApoyos.Application.DTOs.TiposDocumento;
using SigmaApoyos.Application.Interfaces.Repositories.TiposDocumento;
using SigmaApoyos.Application.Interfaces.Services.TipoDocumento.ICrearTipoDocumentoService;
using SigmaApoyos.Domain.Entities;

namespace SigmaApoyos.Application.Services.TiposDocumento.CrearTipoDocumento;

public sealed class CrearTipoDocumento : ICrearTipoDocumentoService
{
    private readonly ICrearTipoDocumentoRepository _repository;
    public CrearTipoDocumento(ICrearTipoDocumentoRepository repository) => _repository = repository;
    public async Task<bool> CrearAsync(CrearTipoDocumentoDto dto, CancellationToken cancellationToken = default)
    {
        await _repository.CrearAsync(new TipoDocumento { Tipo = dto.Tipo.Trim() }, cancellationToken);
        return true;
    }
}
