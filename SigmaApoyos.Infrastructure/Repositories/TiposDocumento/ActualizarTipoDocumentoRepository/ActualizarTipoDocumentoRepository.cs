using Microsoft.EntityFrameworkCore;
using SigmaApoyos.Application.DTOs.TiposDocumento;
using SigmaApoyos.Application.Interfaces.Repositories.TiposDocumento;
using SigmaApoyos.Infrastructure.Persistence;

namespace SigmaApoyos.Infrastructure.Repositories.TiposDocumento.ActualizarTipoDocumentoRepository;

public class ActualizarTipoDocumentoRepository : IActualizarTipoDocumentoRepository
{
    private readonly ApplicationDbContext _context;
    public ActualizarTipoDocumentoRepository(ApplicationDbContext context) => _context = context;
    public async Task ActualizarAsync(UpdateTipoDocumentoDto dto, CancellationToken cancellationToken = default)
    {
        var tipo = await _context.TiposDocumento.FirstAsync(x => x.IdTipoDocumento == dto.IdTipoDocumento, cancellationToken);
        tipo.Tipo = dto.Tipo;
        await _context.SaveChangesAsync(cancellationToken);
    }
}
