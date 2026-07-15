using Microsoft.EntityFrameworkCore;
using SigmaApoyos.Application.Interfaces.Repositories.TiposDocumento;
using SigmaApoyos.Infrastructure.Persistence;

namespace SigmaApoyos.Infrastructure.Repositories.TiposDocumento.EliminarTipoDocumentoRepository;

public class EliminarTipoDocumentoRepository : IEliminarTipoDocumentoRepository
{
    private readonly ApplicationDbContext _context;
    public EliminarTipoDocumentoRepository(ApplicationDbContext context) => _context = context;
    public async Task EliminarAsync(int idTipoDocumento, CancellationToken cancellationToken = default)
    {
        var tipo = await _context.TiposDocumento.FirstAsync(x => x.IdTipoDocumento == idTipoDocumento, cancellationToken);
        _context.TiposDocumento.Remove(tipo);
        await _context.SaveChangesAsync(cancellationToken);
    }
}
