using SigmaApoyos.Application.DTOs.Correos;
using System;
using System.Collections.Generic;
using System.Text;

namespace SigmaApoyos.Application.Interfaces.Services.Correo.ICorreoService
{
    public interface ICorreoService
    {
        Task EnviarAsync(CorreoDto correo, CancellationToken cancellationToken = default);
    }
}
