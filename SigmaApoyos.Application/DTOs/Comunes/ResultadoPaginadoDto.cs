namespace SigmaApoyos.Application.DTOs.Comunes;

public class ResultadoPaginadoDto<T>
{
    public IReadOnlyList<T> Registros { get; set; } = [];

    public int PaginaActual { get; set; }

    public int TotalPaginas { get; set; }

    public int TotalRegistros { get; set; }

    public bool TienePaginaAnterior => PaginaActual > 1;

    public bool TienePaginaSiguiente => PaginaActual < TotalPaginas;
}
