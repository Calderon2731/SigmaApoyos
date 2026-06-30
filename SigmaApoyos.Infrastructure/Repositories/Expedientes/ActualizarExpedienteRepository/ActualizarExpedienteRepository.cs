using SigmaApoyos.Application.Interfaces.Repositories.Expedientes.IActualizarExpedienteRepository;
using SigmaApoyos.Domain.Entities;
using SigmaApoyos.Infrastructure.Persistence;
using System;
using System.Collections.Generic;
using System.Text;

namespace SigmaApoyos.Infrastructure.Repositories.Expedientes.ActualizarExpedienteRepository
{
    public class ActualizarExpedienteRepository : IActualizarExpedienteRepository
    {
        private readonly ApplicationDbContext _context;

        public ActualizarExpedienteRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task ActualizarAsync(Expediente expediente, CancellationToken cancellationToken = default)
        {
            _context.Expedientes.Update(expediente);
            await _context.SaveChangesAsync(cancellationToken);
        }

    }
}
