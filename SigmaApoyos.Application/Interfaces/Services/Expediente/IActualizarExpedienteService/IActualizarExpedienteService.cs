using SigmaApoyos.Application.DTOs.Expedientes;
using System;
using System.Collections.Generic;
using System.Text;

namespace SigmaApoyos.Application.Interfaces.Services.Expediente.IActualizarExpedienteService
{
    public interface IActualizarExpedienteService
    {
        Task ActualizarAsync(UpdateExpedienteDto dto, CancellationToken cancellationToken = default);
    }
}
