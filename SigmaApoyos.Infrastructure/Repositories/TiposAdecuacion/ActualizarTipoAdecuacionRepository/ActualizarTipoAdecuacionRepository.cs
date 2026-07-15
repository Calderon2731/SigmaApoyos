using Microsoft.EntityFrameworkCore;
using SigmaApoyos.Application.DTOs.TiposAdecuacion;
using SigmaApoyos.Application.Interfaces.Repositories.TiposAdecuacion;
using SigmaApoyos.Infrastructure.Persistence;

namespace SigmaApoyos.Infrastructure.Repositories.TiposAdecuacion.ActualizarTipoAdecuacionRepository;

public class ActualizarTipoAdecuacionRepository : IActualizarTipoAdecuacionRepository
{
    private readonly ApplicationDbContext _context;
    public ActualizarTipoAdecuacionRepository(ApplicationDbContext context) => _context = context;
    public async Task ActualizarAsync(UpdateTipoAdecuacionDto dto, CancellationToken cancellationToken = default)
    {
        var tipo = await _context.TiposAdecuacion.FirstAsync(x => x.IdTipoAdecuacion == dto.IdTipoAdecuacion, cancellationToken);
        tipo.Nombre = dto.Nombre;
        await _context.SaveChangesAsync(cancellationToken);
    }
}
