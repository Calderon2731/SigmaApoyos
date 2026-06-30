using SigmaApoyos.Application.DTOs.Expedientes;
using System;
using System.Collections.Generic;
using System.Text;

namespace SigmaApoyos.Application.Interfaces.Services.Expediente.IObtenerExpedienteService
{
    public interface IObtenerExpedienteService
    {
     Task<IReadOnlyList<ExpedienteDto>> ObtenerTodosAsync(CancellationToken cancellationToken = default);
    }
}
