using Microsoft.EntityFrameworkCore;
using SigmaApoyos.Application.DTOs.Auditorias;
using SigmaApoyos.Application.DTOs.Comunes;
using SigmaApoyos.Application.Interfaces.Repositories.Auditorias;
using SigmaApoyos.Infrastructure.Persistence;

namespace SigmaApoyos.Infrastructure.Repositories.Auditorias.ObtenerAuditoriasRepository;

public class ObtenerAuditoriasRepository : IObtenerAuditoriasRepository
{
    private const int RegistrosPorPagina = 10;
    private readonly ApplicationDbContext _context;

    public ObtenerAuditoriasRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<ResultadoPaginadoDto<AuditoriaDto>> ObtenerTodosAsync(
        FiltroAuditoriaDto filtro,
        CancellationToken cancellationToken = default)
    {
        var consulta = _context.Auditorias
            .AsNoTracking()
            .AsQueryable();

        if (!string.IsNullOrWhiteSpace(filtro.Usuario))
        {
            string usuario = filtro.Usuario.Trim();
            consulta = consulta.Where(auditoria => auditoria.UsuarioNombre.Contains(usuario));
        }

        if (!string.IsNullOrWhiteSpace(filtro.Accion))
        {
            string accion = filtro.Accion.Trim();
            consulta = consulta.Where(auditoria => auditoria.Accion.Contains(accion));
        }

        if (!string.IsNullOrWhiteSpace(filtro.Entidad))
        {
            string entidad = filtro.Entidad.Trim();
            consulta = consulta.Where(auditoria => auditoria.Entidad.Contains(entidad));
        }

        if (filtro.FechaDesde.HasValue)
        {
            DateTime fechaDesde = filtro.FechaDesde.Value.Date;
            consulta = consulta.Where(auditoria => auditoria.FechaUtc >= fechaDesde);
        }

        if (filtro.FechaHasta.HasValue)
        {
            DateTime fechaHasta = filtro.FechaHasta.Value.Date.AddDays(1);
            consulta = consulta.Where(auditoria => auditoria.FechaUtc < fechaHasta);
        }

        int totalRegistros = await consulta.CountAsync(cancellationToken);
        int totalPaginas = Math.Max(1, (int)Math.Ceiling(totalRegistros / (double)RegistrosPorPagina));
        int paginaActual = Math.Clamp(filtro.Pagina, 1, totalPaginas);

        var auditorias = await consulta
            .OrderByDescending(auditoria => auditoria.FechaUtc)
            .Skip((paginaActual - 1) * RegistrosPorPagina)
            .Take(RegistrosPorPagina)
            .Select(auditoria => new AuditoriaDto
            {
                IdAuditoria = auditoria.IdAuditoria,
                UsuarioId = auditoria.UsuarioId,
                UsuarioNombre = auditoria.UsuarioNombre,
                Accion = auditoria.Accion,
                Entidad = auditoria.Entidad,
                RegistroId = auditoria.RegistroId,
                ValoresAnteriores = auditoria.ValoresAnteriores,
                ValoresNuevos = auditoria.ValoresNuevos,
                FechaUtc = auditoria.FechaUtc,
                DireccionIp = auditoria.DireccionIp,
                Ruta = auditoria.Ruta,
                Descripcion = auditoria.Descripcion
            })
            .ToListAsync(cancellationToken);

        return new ResultadoPaginadoDto<AuditoriaDto>
        {
            Registros = auditorias,
            PaginaActual = paginaActual,
            TotalPaginas = totalPaginas,
            TotalRegistros = totalRegistros
        };
    }
}
