using SigmaApoyos.Application.Interfaces.Repositories.Expedientes;
using SigmaApoyos.Domain.Entities;
using SigmaApoyos.Infrastructure.Persistence;
using System;
using System.Collections.Generic;
using System.Text;

namespace SigmaApoyos.Infrastructure.Repositories.Expedientes.CrearExpedienteRepository
{
    public class CrearExpedienteRepository : ICrearExpedienteRepository
    {
        private readonly ApplicationDbContext _context;

        public CrearExpedienteRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task CrearAsync(Expediente expediente, CancellationToken cancellationToken = default)
        {
            await _context.Expedientes.AddAsync(expediente, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
        }

    }
}
