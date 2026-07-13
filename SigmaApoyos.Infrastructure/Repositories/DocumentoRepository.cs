using Microsoft.EntityFrameworkCore;
using SigmaApoyos.Application.Interfaces;
using SigmaApoyos.Domain.Entities;
using SigmaApoyos.Infrastructure.Persistence;

namespace SigmaApoyos.Infrastructure.Repositories;

public class DocumentoRepository(ApplicationDbContext context) : IDocumentoRepository
{
    public async Task<IReadOnlyList<Documento>> ObtenerPorExpedienteAsync(string identificacionEstudiante, CancellationToken cancellationToken = default)
    {
        return await context.Documentos
            .Include(x => x.TipoDocumento)
            .Include(x => x.Estado)
            .Where(x => x.IdentificacionEstudiante == identificacionEstudiante)
            .AsNoTracking()
            .ToListAsync(cancellationToken);
    }

    public async Task AgregarAsync(Documento documento, CancellationToken cancellationToken = default)
    {
        await context.Documentos.AddAsync(documento, cancellationToken);
        await context.SaveChangesAsync(cancellationToken);
    }
}
