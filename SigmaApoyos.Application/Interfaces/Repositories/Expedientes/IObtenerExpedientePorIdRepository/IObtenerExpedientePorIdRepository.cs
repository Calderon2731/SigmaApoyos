using SigmaApoyos.Application.DTOs.Expedientes;
using SigmaApoyos.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace SigmaApoyos.Application.Interfaces.Repositories.Expedientes;

public interface IObtenerExpedientePorIdRepository
{
    Task<ExpedienteDto?> ObtenerPorIdAsync(string identificacionEstudiante, CancellationToken cancellationToken = default);
}
