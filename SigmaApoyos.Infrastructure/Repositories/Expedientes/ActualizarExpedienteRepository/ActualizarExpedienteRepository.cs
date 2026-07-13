using Microsoft.EntityFrameworkCore;
using SigmaApoyos.Application.DTOs.Expedientes;
using SigmaApoyos.Application.Interfaces.Repositories.Expedientes.IActualizarExpedienteRepository;
using SigmaApoyos.Infrastructure.Persistence;

namespace SigmaApoyos.Infrastructure.Repositories.Expedientes.ActualizarExpedienteRepository
{
    public class ActualizarExpedienteRepository : IActualizarExpedienteRepository
    {
        private readonly ApplicationDbContext _context;

        public ActualizarExpedienteRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task ActualizarAsync(UpdateExpedienteDto dto, CancellationToken cancellationToken = default)
        {
            var expediente = await _context.Expedientes
                .FirstOrDefaultAsync(x => x.IdentificacionEstudiante == dto.IdentificacionEstudiante, cancellationToken);

            if (expediente == null)
            {
                throw new InvalidOperationException("El expediente no existe.");
            }

            expediente.Nombre = dto.Nombre;
            expediente.PrimerApellido = dto.PrimerApellido;
            expediente.SegundoApellido = dto.SegundoApellido;
            expediente.FechaNacimiento = dto.FechaNacimiento.ToDateTime(TimeOnly.MinValue);
            expediente.NombreEncargado = dto.NombreEncargado;
            expediente.TelefonoEncargado = dto.TelefonoEncargado;
            expediente.Observaciones = dto.Observaciones;
            expediente.IdTipoAdecuacion = dto.IdTipoAdecuacion;
            expediente.IdEstado = dto.IdEstado;

            _context.Expedientes.Update(expediente);
            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}

