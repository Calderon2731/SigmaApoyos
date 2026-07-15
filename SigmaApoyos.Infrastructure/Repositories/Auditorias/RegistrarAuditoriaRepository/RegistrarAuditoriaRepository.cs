using SigmaApoyos.Application.Interfaces.Repositories.Auditorias;
using SigmaApoyos.Domain.Entities;
using SigmaApoyos.Infrastructure.Persistence;

namespace SigmaApoyos.Infrastructure.Repositories.Auditorias.RegistrarAuditoriaRepository;

public class RegistrarAuditoriaRepository : IRegistrarAuditoriaRepository
{
    private readonly ApplicationDbContext _context;
    public RegistrarAuditoriaRepository(ApplicationDbContext context) => _context = context;

    public async Task RegistrarAsync(Auditoria auditoria, CancellationToken cancellationToken = default)
    {
        await _context.Auditorias.AddAsync(auditoria, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
    }
}
