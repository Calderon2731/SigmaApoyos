using Microsoft.EntityFrameworkCore;
using SigmaApoyos.Application.Interfaces.Repositories.Documentos;
using SigmaApoyos.Infrastructure.Persistence;

namespace SigmaApoyos.Infrastructure.Repositories.Documentos.EliminarDocumentoRepository;

public class EliminarDocumentoRepository : IEliminarDocumentoRepository
{
    private readonly ApplicationDbContext _context;

    public EliminarDocumentoRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task EliminarAsync(int idDocumento, CancellationToken cancellationToken = default)
    {
        var documento = await _context.Documentos
            .FirstOrDefaultAsync(x => x.IdDocumento == idDocumento, cancellationToken);

        if (documento == null)
        {
            throw new InvalidOperationException("El documento no existe.");
        }

        _context.Documentos.Remove(documento);
        await _context.SaveChangesAsync(cancellationToken);
    }
}
