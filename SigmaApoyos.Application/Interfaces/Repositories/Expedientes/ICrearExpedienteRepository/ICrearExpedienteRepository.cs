using SigmaApoyos.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace SigmaApoyos.Application.Interfaces.Repositories.Expedientes;

public interface ICrearExpedienteRepository
{
    Task CrearAsync(Expediente expediente, CancellationToken cancellationToken = default);
}
