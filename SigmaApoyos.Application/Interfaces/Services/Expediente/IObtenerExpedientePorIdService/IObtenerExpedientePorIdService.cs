using SigmaApoyos.Application.DTOs.Expedientes;
using System;
using System.Collections.Generic;
using System.Text;

namespace SigmaApoyos.Application.Interfaces.Services.Expediente.IObtenerExpedientePorIdService
{
    public interface IObtenerExpedientePorIdService
    {
        Task<ExpedienteDto?> ObtenerPorIdentificacionAsync(string identificacionEstudiante, CancellationToken cancellationToken = default);
    }
}
