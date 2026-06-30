using Microsoft.EntityFrameworkCore;
using SigmaApoyos.Application.Interfaces.Repositories.Expedientes;
using SigmaApoyos.Application.Interfaces.Repositories.Expedientes.IActualizarExpedienteRepository;
using SigmaApoyos.Application.Interfaces.Repositories.Expedientes.IEliminarExpedienteRepository;
using SigmaApoyos.Domain.Entities;
using SigmaApoyos.Infrastructure.Persistence;

namespace SigmaApoyos.Infrastructure.Repositories;

public class ExpedienteRepository
{
    private readonly ApplicationDbContext _context;

    public ExpedienteRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Expediente?> ObtenerPorIdAsync(string identificacionEstudiante, CancellationToken cancellationToken = default)
    {
        return await _context.Expedientes
            .Include(x => x.TipoAdecuacion)
            .Include(x => x.Estado)
            .FirstOrDefaultAsync(x => x.IdentificacionEstudiante == identificacionEstudiante, cancellationToken);
    }

    public async Task<IReadOnlyList<Expediente>> ObtenerTodosAsync(CancellationToken cancellationToken = default)
    {
        return await _context.Expedientes
            .Include(x => x.TipoAdecuacion)
            .Include(x => x.Estado)
            .AsNoTracking()
            .ToListAsync(cancellationToken);
    }

    public async Task CrearAsync(Expediente expediente, CancellationToken cancellationToken = default)
    {
        await _context.Expedientes.AddAsync(expediente, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task ActualizarAsync(Expediente expediente, CancellationToken cancellationToken = default)
    {
        _context.Expedientes.Update(expediente);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task EliminarAsync(Expediente expediente, CancellationToken cancellationToken = default)
    {
        _context.Expedientes.Remove(expediente);
        await _context.SaveChangesAsync(cancellationToken);
    }
}