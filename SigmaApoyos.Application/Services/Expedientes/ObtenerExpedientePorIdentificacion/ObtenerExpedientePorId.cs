using SigmaApoyos.Application.DTOs.Expedientes;
using SigmaApoyos.Application.Interfaces.Repositories.Expedientes;
using SigmaApoyos.Application.Interfaces.Services.Expediente.IObtenerExpedientePorIdService;
using System;
using System.Collections.Generic;
using System.Text;

namespace SigmaApoyos.Application.Services.Expedientes.ObtenerExpedientePorIdentificacion
{
    public class ObtenerExpedientePorId : IObtenerExpedientePorIdService
    {
        private readonly IObtenerExpedientePorIdRepository _ObtenerExpedientePorIdrepository;

        public ObtenerExpedientePorId(IObtenerExpedientePorIdRepository ObtenerExpedientePorIdrepository)
        {
            _ObtenerExpedientePorIdrepository = ObtenerExpedientePorIdrepository;
        }

        public async Task<ExpedienteDto?> ObtenerPorIdentificacionAsync(string identificacion, CancellationToken cancellationToken = default)
        {
            return await _ObtenerExpedientePorIdrepository.ObtenerPorIdAsync(identificacion, cancellationToken);
        }
    }
}
