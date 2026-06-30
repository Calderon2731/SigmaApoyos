using SigmaApoyos.Application.Interfaces.Repositories.Expedientes.IEliminarExpedienteRepository;
using SigmaApoyos.Domain.Entities;
using SigmaApoyos.Infrastructure.Persistence;
using System;
using System.Collections.Generic;
using System.Text;

namespace SigmaApoyos.Infrastructure.Repositories.Expedientes.EliminarExpedienteRepository
{
    public class EliminarExpedienteRepository : IEliminarExpedienteRepository
    {
        private readonly ApplicationDbContext _context;

        public EliminarExpedienteRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task EliminarAsync(Expediente expediente, CancellationToken cancellationToken = default)
        {
            _context.Expedientes.Remove(expediente);
            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}
