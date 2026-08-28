using System;
using System.Collections.Generic;
using System.Text;

namespace SigmaApoyos.Application.DTOs.Correos
{
    public class CorreoDto
    {
        public List<string> Destinatarios { get; set; } = [];

        public string Asunto { get; set; } = string.Empty;

        public string CuerpoHtml { get; set; } = string.Empty;

    }
}
