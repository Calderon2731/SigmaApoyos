using Microsoft.EntityFrameworkCore;
using SigmaApoyos.Application.DTOs.Estados;
using SigmaApoyos.Application.Interfaces.Repositories.Estados;
using SigmaApoyos.Infrastructure.Persistence;

namespace SigmaApoyos.Infrastructure.Repositories.Estados.ActualizarEstadoRepository;

public class ActualizarEstadoRepository : IActualizarEstadoRepository
{
    private readonly ApplicationDbContext _context;
    public ActualizarEstadoRepository(ApplicationDbContext context) => _context = context;
    public async Task ActualizarAsync(UpdateEstadoDto dto, CancellationToken cancellationToken = default)
    {
        var estado = await _context.Estados.FirstAsync(x => x.IdEstado == dto.IdEstado, cancellationToken);
        estado.Nombre = dto.Nombre;
        await _context.SaveChangesAsync(cancellationToken);
    }
}
