using System;
using System.Collections.Generic;
using System.Text;

namespace SigmaApoyos.Application.Interfaces.Services.Expediente.IEliminarExpedienteService
{
    public interface IEliminarExpedienteService
    {
        Task EliminarAsync(string identificacionEstudiante, CancellationToken cancellationToken = default);

    }
}
