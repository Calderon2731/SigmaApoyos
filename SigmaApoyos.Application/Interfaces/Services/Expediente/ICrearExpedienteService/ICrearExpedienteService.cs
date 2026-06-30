using SigmaApoyos.Application.DTOs.Expedientes;
using System;
using System.Collections.Generic;
using System.Text;

namespace SigmaApoyos.Application.Interfaces.Services.Expediente.ICrearExpedienteService
{
    public interface ICrearExpedienteService
    {
        Task CrearAsync(CrearExpedienteDto dto, CancellationToken cancellationToken = default);
    }
}
