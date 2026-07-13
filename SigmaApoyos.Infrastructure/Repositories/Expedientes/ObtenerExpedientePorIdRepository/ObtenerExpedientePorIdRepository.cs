using Microsoft.EntityFrameworkCore;
using SigmaApoyos.Application.DTOs.Expedientes;
using SigmaApoyos.Application.Interfaces.Repositories.Expedientes;
using SigmaApoyos.Domain.Entities;
using SigmaApoyos.Infrastructure.Persistence;
using System;
using System.Collections.Generic;
using System.Text;

namespace SigmaApoyos.Infrastructure.Repositories.Expedientes.ObtenerExpedientePorIdRepository
{
    public class ObtenerExpedientePorIdRepository : IObtenerExpedientePorIdRepository
    {
        private readonly ApplicationDbContext _context;

        public ObtenerExpedientePorIdRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<ExpedienteDto?> ObtenerPorIdAsync(string identificacionEstudiante, CancellationToken cancellationToken)
        {
            return await _context.Expedientes
                .AsNoTracking()
                .Where(e => e.IdentificacionEstudiante == identificacionEstudiante)
                .Select(expediente => new ExpedienteDto
                {
                    IdentificacionEstudiante = expediente.IdentificacionEstudiante,
                    Nombre = expediente.Nombre,
                    PrimerApellido = expediente.PrimerApellido,
                    SegundoApellido = expediente.SegundoApellido,
                    FechaNacimiento = expediente.FechaNacimiento,
                    NombreEncargado = expediente.NombreEncargado,
                    TelefonoEncargado = expediente.TelefonoEncargado,
                    Observaciones = expediente.Observaciones,
                    IdTipoAdecuacion = expediente.IdTipoAdecuacion,
                    IdEstado = expediente.IdEstado
                })
                .FirstOrDefaultAsync(cancellationToken);
        }
    }
}
