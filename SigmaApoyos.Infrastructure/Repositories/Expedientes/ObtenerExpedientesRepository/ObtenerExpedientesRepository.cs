using Microsoft.EntityFrameworkCore;
using SigmaApoyos.Application.DTOs.Comunes;
using SigmaApoyos.Application.DTOs.Expedientes;
using SigmaApoyos.Application.Interfaces.Repositories.Expedientes;
using SigmaApoyos.Infrastructure.Persistence;

namespace SigmaApoyos.Infrastructure.Repositories.Expedientes.ObtenerExpedientesRepository;

public class ObtenerExpedientesRepository : IObtenerExpedientesRepository
{
    private const int RegistrosPorPagina = 10;
    private readonly ApplicationDbContext _context;

    public ObtenerExpedientesRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<ResultadoPaginadoDto<ExpedienteDto>> ObtenerTodosAsync(
        FiltroExpedienteDto filtro,
        CancellationToken cancellationToken = default)
    {
        var consulta = _context.Expedientes
            .AsNoTracking()
            .AsQueryable();

        if (!string.IsNullOrWhiteSpace(filtro.Identificacion))
        {
            string identificacion = filtro.Identificacion.Trim();
            consulta = consulta.Where(expediente =>
                expediente.IdentificacionEstudiante == identificacion);
        }

        if (filtro.IdEstado.HasValue)
        {
            consulta = consulta.Where(expediente => expediente.IdEstado == filtro.IdEstado.Value);
        }

        if (filtro.IdTipoAdecuacion.HasValue)
        {
            consulta = consulta.Where(expediente =>
                expediente.IdTipoAdecuacion == filtro.IdTipoAdecuacion.Value);
        }

        int totalRegistros = await consulta.CountAsync(cancellationToken);
        int totalPaginas = Math.Max(1, (int)Math.Ceiling(totalRegistros / (double)RegistrosPorPagina));
        int paginaActual = Math.Clamp(filtro.Pagina, 1, totalPaginas);

        var expedientes = await consulta
            .OrderBy(expediente => expediente.IdentificacionEstudiante)
            .Skip((paginaActual - 1) * RegistrosPorPagina)
            .Take(RegistrosPorPagina)
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
            .ToListAsync(cancellationToken);

        return new ResultadoPaginadoDto<ExpedienteDto>
        {
            Registros = expedientes,
            PaginaActual = paginaActual,
            TotalPaginas = totalPaginas,
            TotalRegistros = totalRegistros
        };
    }
}
