using Microsoft.EntityFrameworkCore;
using SigmaApoyos.Application.Interfaces.Repositories.Expedientes;
using SigmaApoyos.Domain.Entities;
using SigmaApoyos.Infrastructure.Persistence;
using System;
using System.Collections.Generic;
using System.Text;

namespace SigmaApoyos.Infrastructure.Repositories.Expedientes.ObtenerExpedientePorIdRepository
{
    public class ObtenerExpedientePorIdRepository : IObtenerExpedientePorIdRepository
    {
        private readonly ApplicationDbContext _context;

        public ObtenerExpedientePorIdRepository(ApplicationDbContext context)
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
    }
}
