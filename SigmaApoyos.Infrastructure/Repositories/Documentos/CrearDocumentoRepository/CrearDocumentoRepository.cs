using SigmaApoyos.Application.Interfaces.Repositories.Documentos;
using SigmaApoyos.Domain.Entities;
using SigmaApoyos.Infrastructure.Persistence;

namespace SigmaApoyos.Infrastructure.Repositories.Documentos.CrearDocumentoRepository;

public class CrearDocumentoRepository : ICrearDocumentoRepository
{
    private readonly ApplicationDbContext _context;

    public CrearDocumentoRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task CrearAsync(Documento documento, CancellationToken cancellationToken = default)
    {
        await _context.Documentos.AddAsync(documento, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
    }
}
