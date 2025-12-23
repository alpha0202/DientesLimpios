using DientesLimpios.Aplicacion.Utilidades.Mediador;
using System;
using System.Collections.Generic;
using System.Text;

namespace DientesLimpios.Aplicacion.CasosDeUso.Consultorios.Comandos.ActualizarConsultorio
{
    public class ComandoActualizarConsultorio : IRequest
    {

        public Guid Id { get; set; }
        public required string Nombre { get; set; }
    }
}
