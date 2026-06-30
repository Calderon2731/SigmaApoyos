using Microsoft.EntityFrameworkCore;
using SigmaApoyos.Application.Interfaces.Repositories.Expedientes;
using SigmaApoyos.Domain.Entities;
using SigmaApoyos.Infrastructure.Persistence;
using System;
using System.Collections.Generic;
using System.Text;

namespace SigmaApoyos.Infrastructure.Repositories.Expedientes.ObtenerExpedientesRepository
{
    public class ObtenerExpedientesRepository : IObtenerExpedientesRepository
    {
        private readonly ApplicationDbContext _context;

        public ObtenerExpedientesRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IReadOnlyList<Expediente>> ObtenerTodosAsync(CancellationToken cancellationToken = default)
        {
            return await _context.Expedientes
                .Include(x => x.TipoAdecuacion)
                .Include(x => x.Estado)
                .AsNoTracking()
                .ToListAsync(cancellationToken);
        }
    }
}
