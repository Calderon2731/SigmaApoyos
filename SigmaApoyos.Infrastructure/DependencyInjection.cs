using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SigmaApoyos.Application.Interfaces;
using SigmaApoyos.Application.Interfaces.Repositories;
using SigmaApoyos.Application.Interfaces.Repositories.Expedientes;
using SigmaApoyos.Application.Interfaces.Repositories.Expedientes.IActualizarExpedienteRepository;
using SigmaApoyos.Application.Interfaces.Repositories.Expedientes.IEliminarExpedienteRepository;
using SigmaApoyos.Application.Interfaces.Services;
using SigmaApoyos.Infrastructure.Persistence;
using SigmaApoyos.Infrastructure.Repositories;
using SigmaApoyos.Infrastructure.Repositories.Expedientes.ActualizarExpedienteRepository;
using SigmaApoyos.Infrastructure.Repositories.Expedientes.CrearExpedienteRepository;
using SigmaApoyos.Infrastructure.Repositories.Expedientes.EliminarExpedienteRepository;
using SigmaApoyos.Infrastructure.Repositories.Expedientes.ObtenerExpedientePorIdRepository;
using SigmaApoyos.Infrastructure.Repositories.Expedientes.ObtenerExpedientesRepository;

namespace SigmaApoyos.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("DefaultConnection")
            ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");

        services.AddDbContext<ApplicationDbContext>(options =>
            options.UseSqlServer(connectionString));

       /* inyeccion de dependencias de Expedientes*/
        services.AddScoped<IObtenerExpedientesRepository, ObtenerExpedientesRepository>();
        services.AddScoped<IObtenerExpedientePorIdRepository, ObtenerExpedientePorIdRepository>();
        services.AddScoped<ICrearExpedienteRepository, CrearExpedienteRepository>();
        services.AddScoped<IActualizarExpedienteRepository, ActualizarExpedienteRepository>();
        services.AddScoped<IEliminarExpedienteRepository, EliminarExpedienteRepository>();
        services.AddScoped<IDocumentoRepository, DocumentoRepository>();

        return services;
    }
}
