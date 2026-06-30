using SigmaApoyos.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace SigmaApoyos.Application.Interfaces.Repositories.Expedientes;
    public interface IObtenerExpedientesRepository
    {
        Task<IReadOnlyList<Expediente>> ObtenerTodosAsync(CancellationToken cancellationToken = default);
    }
