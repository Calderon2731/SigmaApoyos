using SigmaApoyos.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace SigmaApoyos.Application.Interfaces.Repositories.Expedientes.IEliminarExpedienteRepository
{
    public interface IEliminarExpedienteRepository
    {
        Task EliminarAsync(Expediente expediente, CancellationToken cancellationToken = default);
    }
}
